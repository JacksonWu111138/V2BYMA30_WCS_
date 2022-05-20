using Mirle.LCS.Models;
using Mirle.STKC.R46YP320.Model;
using Mirle.LCS.Models.Info;
using Mirle.STKC.R46YP320.Repositories;
using Mirle.STKC.R46YP320.Service;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mirle.STKC.R46YP320.Service
{
    public class AlarmService
    {
        private readonly IAlarmDataRepository _alarmDataRepo;
        private readonly STKCHost _stkcHost;
        private readonly LCSInfo _lcsInfo;
        private readonly string _stockerId;
        private readonly LoggerService _loggerService;
        private readonly IEnumerable<AlarmDefInfo> _alarmInfos;

        public AlarmService(STKCHost stkcHost, LoggerService loggerService,
            IEnumerable<AlarmDefInfo> alarmInfos, IAlarmDataRepository alarmDataRepo)
        {
            _loggerService = loggerService;
            _alarmInfos = alarmInfos;
            _stkcHost = stkcHost;
            _lcsInfo = stkcHost.GetLCSInfo();
            _stockerId = _lcsInfo.Stocker.StockerId;
            _alarmDataRepo = alarmDataRepo;
        }

        public void InsertWarning(
            string alarmCode,
            string eqId,
            string alarmLoc,
            string commandId_LF,
            string commandId_RF,
            string cstid,
            string cstLoc,
            string source,
            string destination,
            string stockerCraneId)
        {
            try
            {
                var dto = new AlarmDataDTO()
                {
                    StockerId = _stockerId,
                    EQId = eqId,
                    AlarmCode = alarmCode,
                    StrDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    EndDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    AlarmLoc = alarmLoc,
                    CommandId = commandId_LF,
                    CommandId_RF = commandId_RF,
                    CstId = cstid,
                    CstLoc = cstLoc,
                    Source = source,
                    Dest = destination,
                    StockerCraneId = stockerCraneId,
                    MPLCAlarmIndex = "0",
                    AlarmType = AlarmTypes.LCS,
                };

                _alarmDataRepo.InsertWarning(dto);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public void SetAlarm(
            string alarmCode,
            string eqId,
            string alarmLoc,
            string commandId_LF,
            string commandId_RF,
            string cstid,
            string cstLoc,
            string source,
            string destination,
            string stockerCraneId,
            string mplcAlarmIndex,
            AlarmTypes alarmType,
            int srSeq = 0)
        {
            try
            {
                //var alarmInfo = _lcsInfo.AlarmInfos.FirstOrDefault(a => a.AlarmCode == alarmCode && a.AlarmType == alarmType);
                //if (alarmInfo != null && alarmInfo.ReportEnable == false) return;

                var dto = new AlarmDataDTO()
                {
                    StockerId = _stockerId,
                    EQId = eqId,
                    AlarmCode = alarmCode,
                    StrDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    EndDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    AlarmLoc = alarmLoc,
                    CommandId = commandId_LF,
                    CommandId_RF = commandId_RF,
                    CstId = cstid,
                    CstLoc = cstLoc,
                    Source = source,
                    Dest = destination,
                    StockerCraneId = stockerCraneId,
                    MPLCAlarmIndex = mplcAlarmIndex,
                    AlarmType = alarmType,
                    SRSEQ = srSeq.ToString(),
                };

                _alarmDataRepo.Insert(dto);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public void SetAlarm(string eqId, string alarmCode, AlarmTypes alarmType, int srSeq = 0)
        {
            try
            {
                //var alarmInfo = _lcsInfo.AlarmInfos.FirstOrDefault(a => a.AlarmCode == alarmCode && a.AlarmType == alarmType);
                //if (alarmInfo != null && alarmInfo.ReportEnable == false) return;

                var dto = new AlarmDataDTO()
                {
                    StockerId = _stockerId,
                    EQId = eqId,
                    AlarmCode = alarmCode,
                    StrDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    AlarmType = alarmType,
                    SRSEQ = srSeq.ToString(),
                };
                _alarmDataRepo.Insert(dto);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public void ClearAlarm(string eqId, string alarmCode)
        {
            try
            {
                _alarmDataRepo.UpdatePLCResetAlarmDT(_stockerId, eqId, alarmCode);
                _alarmDataRepo.UpdateClearedAlarmByEqIdAndAlarmCode(_stockerId, eqId, alarmCode);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public void ClearAllAlarm()
        {
            try
            {
                _alarmDataRepo.UpdateClearedAlarm(_stockerId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public void ClearAllAlarmByIOPort(int ioNo)
        {
            try
            {
                var eqId = _lcsInfo.Stocker.GetIoInfoByIndex(ioNo).HostEQPortId;
                SetPLCResetAlarmDTTimestamp(eqId);
                _alarmDataRepo.UpdateClearedAlarmByEqId(_stockerId, eqId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public void ClearAllAlarmByCrane(int craneNo)
        {
            try
            {
                var eqId = _lcsInfo.Stocker.GetCraneInfoByIndex(craneNo).CraneId;
                SetPLCResetAlarmDTTimestamp(eqId);
                _alarmDataRepo.UpdateClearedAlarmByEqId(_stockerId, eqId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public IEnumerable<AlarmDataDTO> GetAllCurrentAlarm()
        {
            try
            {
                return _alarmDataRepo.GetAllCurrentAlarm(_stockerId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            return new List<AlarmDataDTO>();
        }

        public IEnumerable<AlarmDataDTO> GetCurrentAlarmDescriptionByEqId(string eqId)
        {
            try
            {
                return _alarmDataRepo.GetCurrentAlarmByEqId(_stockerId, eqId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            return new List<AlarmDataDTO>();
        }

        public void SetPLCDoorClosedDTTimestamp()
        {
            try
            {
                _alarmDataRepo.UpdatePLCDoorClosedDT(_stockerId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public void SetPLCDoorOpenDTTimestamp()
        {
            try
            {
                _alarmDataRepo.UpdatePLCDoorOpenDT(_stockerId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void SetPLCResetAlarmDTTimestamp(string eqId)
        {
            try
            {
                _alarmDataRepo.UpdatePLCResetAlarmDT(_stockerId, eqId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
