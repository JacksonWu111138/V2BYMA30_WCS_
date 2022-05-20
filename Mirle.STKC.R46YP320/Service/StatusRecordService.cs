using Mirle.LCS;
using Mirle.LCS.Models;
using Mirle.STKC.R46YP320.Model.Define;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Repositories;
using Mirle.Logger;
using Mirle.STKC.R46YP320.Service;
using Mirle.Stocker.R46YP320;
using System;
using System.Collections.Generic;
using Mirle.STKC.R46YP320.LCSShareMemory;

namespace Mirle.STKC.R46YP320.Service
{
    public class StatusRecordService
    {
        private readonly IUnitDefRepository _unitDefRepo;
        private readonly IUnitStsLogRepository _unitStsLogRepo;
        private readonly IMileageLogRepository _mileageLogRepo;

        private readonly CSOTStocker _stocker;
        private readonly LoggerService _loggerService;
        private readonly LCSParameter _lcsParameter;
        private readonly LCSInfo _lcsInfo;
        private readonly string _stockerId;
        private readonly STKCHost _stkcHost;
        private string _lastCheckMileageDate = string.Empty;

        /// <summary>
        /// For Data Log
        /// </summary>
        private readonly Log gobjData = new Log();

        /// <summary>
        /// 定義以下相關資料記錄在DB否
        /// </summary>
        private LogState ebjCraneStsLog2DBEnable = LogState.DB;

        private LogState ebjStkModeLog2DBEnable = LogState.Txt;
        private LogState ebjIORunLog2DBEnable = LogState.Txt;

        public StatusRecordService(STKCHost stkcHost, LoggerService loggerService, CSOTStocker stocker, LCSParameter lcsParameter,
            IUnitDefRepository unitDefRepo, IUnitStsLogRepository unitStsLogRepo, IMileageLogRepository mileageLogRepo)
        {
            _unitDefRepo = unitDefRepo;
            _unitStsLogRepo = unitStsLogRepo;
            _mileageLogRepo = mileageLogRepo;

            _stocker = stocker;
            _loggerService = loggerService;
            _lcsParameter = lcsParameter;
            _lcsInfo = stkcHost.GetLCSInfo();
            _stockerId = _lcsInfo.Stocker.StockerId;
            _stkcHost = stkcHost;
        }

        public void RecordMileage()
        {
            string thisHour = DateTime.Now.ToString("yyyyMMddHH");

            try
            {
                if (_lastCheckMileageDate == thisHour) return;

                var units = _unitDefRepo.GetAll(_stockerId);
                var dtos = CollectUnitMileage(units);
                _mileageLogRepo.Insert(dtos);
                _lastCheckMileageDate = thisHour;
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private List<MileageLogDTO> CollectUnitMileage(IEnumerable<UnitDefDTO> units)
        {
            var timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
            string strKeyDate = DateTime.Now.ToString("yyyyMMdd");
            List<MileageLogDTO> dtos = new List<MileageLogDTO>();
            foreach (var unit in units)
            {
                switch (unit.UnitType)
                {
                    case UnitType.RM:
                        var crane = _stocker.GetCraneById(unit.UnitIndex) as Crane;
                        if (crane == null) continue;
                        dtos.Add(new MileageLogDTO()
                        {
                            StockerId = unit.StockerId,
                            UnitId = unit.UnitId,
                            MileageType = MileageType.Travel,
                            DataValue = crane.Signal.MileageOfTravel.GetValue() * 0.1,
                            LogDT = timeStamp,
                            KeyDate = strKeyDate,
                        });

                        dtos.Add(new MileageLogDTO()
                        {
                            StockerId = unit.StockerId,
                            UnitId = unit.UnitId,
                            MileageType = MileageType.Lifter,
                            DataValue = crane.Signal.MiileageOfLifter.GetValue() * 0.1,
                            LogDT = timeStamp,
                            KeyDate = strKeyDate,
                        });

                        dtos.Add(new MileageLogDTO()
                        {
                            StockerId = unit.StockerId,
                            UnitId = unit.UnitId,
                            MileageType = MileageType.Rotate,
                            DataValue = crane.Signal.RotatingCounter.GetValue(),
                            LogDT = timeStamp,
                            KeyDate = strKeyDate,
                        });

                        var leftFork = crane.Signal.LeftFork;
                        var rightFork = crane.Signal.RightFork;
                        if (leftFork == null || rightFork == null) continue;
                        dtos.Add(new MileageLogDTO()
                        {
                            StockerId = unit.StockerId,
                            UnitId = unit.UnitId,
                            MileageType = MileageType.Fork,
                            DataValue = leftFork.ForkCounter.GetValue() + rightFork.ForkCounter.GetValue(),
                            LogDT = timeStamp,
                            KeyDate = strKeyDate,
                        });

                        break;

                    case UnitType.TRU:
                        break;

                    case UnitType.Vehicle:
                        var vehicle = (_stocker.GetIOPortById(unit.PortTypeIndex) as IOPort)?.GetVehicleById(unit.UnitIndex) as IOVehicle;
                        if (vehicle == null) continue;
                        dtos.Add(new MileageLogDTO()
                        {
                            StockerId = unit.StockerId,
                            UnitId = unit.UnitId,
                            MileageType = MileageType.Travel,
                            DataValue = vehicle.Signal.MileageTravel.GetValue() * 0.1,
                            LogDT = timeStamp,
                            KeyDate = strKeyDate,
                        });

                        break;

                    case UnitType.Lifter:
                        break;
                }
            }

            return dtos;
        }

        private StockerEnums.CraneStatus[] gstrRMLastStatus = new StockerEnums.CraneStatus[3];
        private DateTime[] _lastRMRecordTimes = new DateTime[3];

        public void funWriteRMStsDBLog(int intRMID)
        {
            try
            {
                var crane = _stocker.GetCraneById(intRMID) as Crane;
                if (crane == null) return;

                var currentSts = crane.Status;

                switch (ebjCraneStsLog2DBEnable)
                {
                    case LogState.DB://CraneStsLog for DB Process
                        if (gstrRMLastStatus[intRMID] != currentSts)
                        {
                            UpdateLastCraneStsRecord(intRMID);
                            InsertNewCraneStsRecord(intRMID, currentSts);
                            _lastRMRecordTimes[intRMID] = DateTime.Now;
                        }
                        else
                        {
                            if (_lastRMRecordTimes[intRMID] < DateTime.Now.AddHours(-1))
                            {
                                UpdateLastCraneStsRecord(intRMID);
                                InsertNewCraneStsRecord(intRMID, currentSts);
                                _lastRMRecordTimes[intRMID] = DateTime.Now;
                            }
                        }
                        break;

                    case LogState.Txt://CraneStsLog for Txt Process
                        if (gstrRMLastStatus[intRMID] != currentSts)
                        {
                            gobjData.WriteLogFile(DateTime.Now.ToString("yyyy-MM-dd_") + intRMID.ToString() + "_CraneStsLog.log",
                                _stockerId + "|" + (int)currentSts);

                            gstrRMLastStatus[intRMID] = currentSts;
                        }
                        break;

                    case LogState.Disable:
                    default:
                        gstrRMLastStatus[intRMID] = currentSts;
                        break;
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                DeleteTooLongEndDT();
            }
        }

        private void UpdateLastCraneStsRecord(int intRMID)
        {
            _unitStsLogRepo.UpdateEndDT(new UnitStsLogDTO()
            {
                StockerId = _stockerId,
                UnitId = _lcsInfo.Stocker.GetCraneInfoByIndex(intRMID).CraneId,
            });
        }

        private void InsertNewCraneStsRecord(int intRMID, StockerEnums.CraneStatus currentSts)
        {
            var dto = new UnitStsLogDTO()
            {
                StockerId = _stockerId,
                UnitId = _lcsInfo.Stocker.GetCraneInfoByIndex(intRMID).CraneId,
                StrDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            };

            //if (_lcsParameter.SCState_Cur == LCSParameter.SCState.Paused 
            //    && _lcsParameter.ControlMode_Cur == LCSParameter.ControlMode.Offline)
            //{
            //        dto.Status = (int)StockerEnums.CraneStatus.ScheduleDown;
            //}
            if (_stocker.SafetyDoorIsClosed == false)
            {
                if (currentSts == StockerEnums.CraneStatus.NONE ||
                    currentSts == StockerEnums.CraneStatus.WAITINGHOMEACTION ||
                    currentSts == StockerEnums.CraneStatus.HOMEACTION ||
                    currentSts == StockerEnums.CraneStatus.STOP ||
                    currentSts == StockerEnums.CraneStatus.MAINTAIN)
                {
                    dto.Status = (int)StockerEnums.CraneStatus.Failures;
                }
                else
                {
                    dto.Status = (int)currentSts;
                }
            }
            else
            {
                dto.Status = (int)currentSts;
            }

            _unitStsLogRepo.Insert(dto);

            gstrRMLastStatus[intRMID] = currentSts;
        }

        private void DeleteTooLongEndDT()
        {
            try
            {
                _unitStsLogRepo.DeleteEndDTIsNull();
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// V1.4.0.12-4 記錄IO Port的Run Sts ( 1 > Run=Avail ; 0 > Not Run=Not Avail)
        /// </summary>
        /// <param name="intIOIndex"></param>
        /// <param name="objAvail"></param>
        /// <returns></returns>
        public int funWriteIORunStsLog(int intIOIndex, bool signalIsOn)
        {
            int intRet = ErrorCode.Initial;

            try
            {
                switch (ebjIORunLog2DBEnable)
                {
                    case LogState.DB:
                    case LogState.Txt:
                        gobjData.WriteLogFile($"{_stockerId}_PortAvailableLog.log",
                            _stockerId + "|" + _lcsInfo.Stocker.GetIoInfoByIndex(intIOIndex)?.HostEQPortId
                            + "|" + (signalIsOn ? "ON" : "OFF"));

                        break;

                    case LogState.Disable:
                    default:
                        break;
                }
                return intRet;
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                return ErrorCode.Exception;
            }
        }
    }
}
