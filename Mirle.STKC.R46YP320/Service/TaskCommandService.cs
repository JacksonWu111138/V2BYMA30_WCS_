using AutoMapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Repositories;
using Mirle.STKC.R46YP320.ViewModels;
using Mirle.Def;
using System;
using System.Collections.Generic;
using Mirle.Stocker.R46YP320;

namespace Mirle.STKC.R46YP320.Service
{
    public class TaskCommandService
    {
        private readonly LoggerService _loggerService;
        private readonly LCSInfo _lcsInfo;
        private readonly string _stockerId;
        private readonly STKCHost _stkcHost;
        private readonly ISnoctrlRepository _snoctrlRepo;
        private readonly ITaskRepository _taskRepo;
        private readonly IMapper _hisTaskDtoMapper;

        public List<Structure.TaskDTO> unUpdateTaskCmdList = new List<Structure.TaskDTO>();

        public TaskCommandService(STKCHost stkcHost, LoggerService loggerService, ISnoctrlRepository snoctrlRepo, ITaskRepository taskRepo)
        {
            _loggerService = loggerService;
            _snoctrlRepo = snoctrlRepo;
            _taskRepo = taskRepo;
            _lcsInfo = stkcHost.GetLCSInfo();
            _stockerId = _lcsInfo.Stocker.StockerId;
            _stkcHost = stkcHost;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<TaskDTO, HisTaskDTO>().ForMember(d => d.HisDT, opt => opt.Ignore()));
            config.AssertConfigurationIsValid();
            _hisTaskDtoMapper = config.CreateMapper();
        }

        public bool IsConnected => _taskRepo.IsConnected;
        public bool FunWriCommand_Proc(Structure.TaskDTO dto, Crane crane, CraneCmdInfo craneCmdInfo)
            => _taskRepo.FunWriCommand_Proc(dto, crane, craneCmdInfo);

        /// <summary>
        /// For Transfer    MANUAL + STKC ID + ZONEID + SHELFID
        /// For Scan        MANUAL + STKC ID + YYYYMMDDHHMMSS + 3碼流水號
        /// </summary>
        /// <param name="eSnoType"></param>
        /// <returns>
        /// Error or Exception > "MANUAL" + clsComDef.gstrStockerID + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        /// </returns>
        public string funTransactionNo_CMD(SNOType eSnoType)
        {
            string strCmdSno = string.Empty;
            int intTimes = 0;
            int intCmdSno = 0;

            int intFirstNum = 0;
            int intMaxSno = 0;

            try
            {
                switch (eSnoType)
                {
                    case SNOType.CommandID:
                        intMaxSno = 999;
                        intFirstNum = 1;
                        break;

                    case SNOType.TaskNo:
                    default:
                        intMaxSno = 29999;
                        intFirstNum = 1;
                        break;
                }

                do
                {
                    intTimes++;

                    var timeStamp = DateTime.Now;
                    var snoctrl = _snoctrlRepo.GetBySnotype(eSnoType.ToString().ToUpper());
                    if (int.TryParse(snoctrl?.Sno, out intCmdSno))
                    {
                        strCmdSno = (intCmdSno >= intMaxSno ? intFirstNum : intCmdSno + 1).ToString();

                        var dto = new SnoctrlDTO()
                        {
                            TrnDT = timeStamp.ToString("yyyyMMdd"),
                            Sno = strCmdSno,
                            SnoType = eSnoType.ToString().ToUpper(),
                        };
                        _snoctrlRepo.Update(dto);
                        break;
                    }
                    else
                    {
                        strCmdSno = intFirstNum.ToString();
                        var dto = new SnoctrlDTO()
                        {
                            TrnDT = timeStamp.ToString("yyyyMMdd"),
                            Sno = strCmdSno,
                            SnoType = eSnoType.ToString().ToUpper(),
                        };
                        _snoctrlRepo.Insert(dto);
                    }
                } while (intTimes < 10);

                if (eSnoType == SNOType.CommandID)
                {
                    return string.IsNullOrEmpty(strCmdSno) ?
                                "MANUAL" + _stockerId + DateTime.Now.ToString("yyyyMMddHHmmssfff") :
                                "MANUAL" + _stockerId + DateTime.Now.ToString("yyyyMMdd") + strCmdSno.PadLeft(3, '0');
                }
                else
                {
                    return string.IsNullOrEmpty(strCmdSno) ?
                                DateTime.Now.ToString("yyyyMMddHHfff") :
                                DateTime.Now.ToString("yyyyMMdd") + strCmdSno.PadLeft(5, '0');
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                if (eSnoType == SNOType.CommandID)
                {
                    return "MANUAL" + _stockerId + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                }
                else
                {
                    return DateTime.Now.ToString("yyyyMMddHHfff");
                }
            }
        }

        public string CreateNewTaskCommand(SCCommand newCmd)
        {
            var strNO = funTransactionNo_CMD(SNOType.TaskNo);
            int.TryParse(newCmd.Source.ToString("D7").Substring(2, 3), out var sourceBay);
            int.TryParse(newCmd.Destination.ToString("D7").Substring(2, 3), out var destBay);

            var dto = new Structure.TaskDTO()
            {
                StockerID = _stockerId,
                CommandID = $"STKC.{strNO}",
                TaskNo = strNO,
                CraneNo = newCmd.CraneId,
                ForkNo = newCmd.ForkNumber,
                CSTID = newCmd.CstId,
                TaskState = clsEnum.TaskState.Queue,
                CMDState = clsEnum.TaskCmdState.STKCQueue,
                TransferMode = newCmd.TransferMode,
                Source = newCmd.Source.ToString("D7"),
                SourceBay = sourceBay,
                Destination = newCmd.Destination.ToString("D7"),
                DestinationBay = destBay,
                TravelAxisSpeed = newCmd.TravelSpeed,
                LifterAxisSpeed = newCmd.LifterSpeed,
                RotateAxisSpeed = newCmd.RotateSpeed,
                ForkAxisSpeed = newCmd.ForkSpeed,
                UserID = "LCS",
                CSTType = newCmd.CstType.ToString("X2"),
                BCRReadFlag = newCmd.EnableBCRRead ? "Y" : "N",
                Priority = 50,
                QueueDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            };

            try
            {
                _taskRepo.Insert(dto);
                return strNO;
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            return string.Empty;
        }

        public void UpdateCompletedTaskCommand(Structure.TaskDTO dto)
        {
            _loggerService.Trace(dto.CraneNo, $"<CmdSno> {dto.CommandID} - UpdateCompletedTaskCommand進入...");
            //var retryTimes = 3;
            //for (int i = 0; i < retryTimes; i++)
            //{
            try
            {
                //_taskRepo.UpdateByTaskNo(dto);
                //break;
                if (_taskRepo.UpdateByTaskNo_ReturnInt(dto) > 0) 
                {
                    _loggerService.Trace(dto.CraneNo, $"<CmdSno> {dto.CommandID} - UpdateCompletedTaskCommand => 更新成功！");
                }
                else
                {
                    _loggerService.Trace(dto.CraneNo, $"NG: <CmdSno> {dto.CommandID} - UpdateCompletedTaskCommand => 更新失敗！");
                    unUpdateTaskCmdList.Add(dto);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                unUpdateTaskCmdList.Add(dto);
            }
            //}
        }

        public void UpdateTaskCommandStatus(Structure.TaskDTO dto)
        {
            try
            {
                _taskRepo.UpdateByTaskNo(dto);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public IEnumerable<Structure.TaskDTO> GetNewTaskCommands()
        {
            try
            {
                return _taskRepo.GetByTaskStateIsQueue(_stockerId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            return new List<Structure.TaskDTO>();
        }

        public IEnumerable<Structure.TaskDTO> FindToTaskCmdByTheSameCommandId(string commandId)
        {
            try
            {
                return _taskRepo.GetByCommandIdAndTransferModeIsTo(commandId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            return new List<Structure.TaskDTO>();
        }

        public IEnumerable<Structure.TaskDTO> GetByTaskNo(string taskNo)
        {
            try
            {
                return _taskRepo.GetByTaskNo(taskNo);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            return new List<Structure.TaskDTO>();
        }

        public void MoveToHisTask(Structure.TaskDTO dto)
        {
            try
            {
                _taskRepo.Delete(dto);
                var hisTadkDTO = _hisTaskDtoMapper.Map<HisTaskDTO>(dto);
                hisTadkDTO.HisDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                _taskRepo.InsertHisTask(hisTadkDTO);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }
        public void InsertHistory(string deviceID)
        {
            _taskRepo.InsertHistory(deviceID);
        }
        public void InsertHistory()
        {
            _taskRepo.InsertHistory(_stockerId);
        }
    }
}