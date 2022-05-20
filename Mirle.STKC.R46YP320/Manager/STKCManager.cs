using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mirle.Stocker.R46YP320.Extensions;
using Mirle.STKC.R46YP320.LCSShareMemory;
using Mirle.LCS.Models;
using Mirle.STKC.R46YP320.Model.Define;
using Mirle.MPLC;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Repositories;
using Mirle.STKC.R46YP320.Service;
using Mirle.Stocker.R46YP320.Events;
using Mirle.Stocker.R46YP320;
using System.Diagnostics;
using Mirle.MPLC.DataBlocks;
using Mirle.MPLC.SharedMemory;
using Mirle.MPLC.MXComponent;
using Mirle.Def;

namespace Mirle.STKC.R46YP320.Manager
{
    public class STKCManager
    {
        private readonly LCSInfo _lcsInfo;
        private readonly LoggerService _loggerService;
        private readonly LCSWatchDog _lcsWatchDog;
        private readonly STKCHost _stkcHost;
        private readonly AlarmService _alarmService;
        private readonly TaskCommandService _taskCommandService;

        private readonly CurrentExeCmdManager _currentExeCmdManager;
        private StockerController _stockerController;
        private CSOTStocker _stocker;

        private IPLCHost _plcHost;
        private IMPLCProvider _mplc;

        private readonly LCSExecutingCMD _lcsExecutingCmd;
        private readonly LCSParameter _lcsParameter;
        private readonly IDataCollectionRepository _dataCollectionRepository;

        /// <summary>
        /// 記錄上次寫MPLC MCS OnLine Bit On 的時間點，預計每 3秒 Check 1次
        /// </summary>
        private DateTime _LastWriteMPLCMCSOnLineTime = DateTime.Now;

        private readonly int _maxPortNo = 125;

        //背景作業
        private ThreadWorker _backgroudWorker;

        private ThreadWorker _newCmdWorker;
        private ThreadWorker _finishCmdWorker;

        public STKCManager(LCSInfo lcsInfo, STKCHost stkcHost, LoggerService loggerService, LCSWatchDog lcsWatchDog,
            AlarmService alarmService, TaskCommandService taskCommandService, LCSExecutingCMD lcsExecutingCmd,
            LCSParameter lcsParameter, IDataCollectionRepository dataCollectionRepository)
        {
            _lcsInfo = lcsInfo;
            _loggerService = loggerService;
            _lcsWatchDog = lcsWatchDog;
            _stkcHost = stkcHost;
            _alarmService = alarmService;
            _taskCommandService = taskCommandService;
            _lcsExecutingCmd = lcsExecutingCmd;
            _lcsParameter = lcsParameter;
            _dataCollectionRepository = dataCollectionRepository;

            if (lcsInfo.InMemorySimulator == false)
            {
                if (lcsInfo.Stocker.UseMCProtocol)
                {
                    InitialMCPLCR();
                }
                else
                {
                    InitialPLCR();
                }
            }

            _currentExeCmdManager = new CurrentExeCmdManager();

            InitialStockerController();
            _lcsParameter.AutoRequest();
        }

        public void StartUp()
        {
            _alarmService.ClearAllAlarm();
            _stockerController.Start();

            _backgroudWorker = new ThreadWorker(BackgroudProc, 1000, false);
            var crane1 = _stocker.GetCraneById(1) as Crane;
            var crane2 = _stocker.GetCraneById(2) as Crane;
            //_newCmdWorker = new ThreadWorker(NewCommandProc, 1500, false);
            _newCmdWorker = new ThreadWorker(() => NewCommandProc(crane1, crane2), 1500, false);
            _finishCmdWorker = new ThreadWorker(CheckFinishCommandProc, 200, false);
            _backgroudWorker.Start();
            _newCmdWorker.Start();
            _finishCmdWorker.Start();
        }

        public void Stop()
        {
            _stockerController.Pause();
            _backgroudWorker.Pause();
            _newCmdWorker.Pause();
            _finishCmdWorker.Pause();
            _plcHost.Stop();
            _lcsWatchDog.SetPLCRComm(LCSWatchDog.WatchDogStatus.Down);
        }

        private void InitialPLCR()
        {
            var plcHostInfo = new PLCHostInfo();
            plcHostInfo.ActLogicalStationNo = _lcsInfo.Stocker.MPLCNo;
            plcHostInfo.HostId = _lcsInfo.Stocker.StockerId;
            plcHostInfo.BlockInfos = SignalMapper4_11.SignalBlocks.Select(b =>
               new BlockInfo(b.DeviceRange, $@"Global\{_lcsInfo.Stocker.StockerId}-{b.SharedMemoryName}", b.PLCRawdataIndex));

            _plcHost = new PLCHost(plcHostInfo);
            _plcHost.Interval = 200;
            _plcHost.EnableWriteRawData = _lcsInfo.Stocker.EnablePLCRawdata;
            _plcHost.LogBaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "LOG";
            _plcHost.Start();
        }

        private void InitialMCPLCR()
        {
            var plcHostInfo = new MPLC.MCProtocol.PLCHostInfo();
            //var plcHostInfo = new Mirle.MPLC.PLCR.MCPLCHostInfo();
            plcHostInfo.IPAddress = _lcsInfo.Stocker.MPLCIP;
            plcHostInfo.Port = _lcsInfo.Stocker.MPLCPort;
            plcHostInfo.HostId = _lcsInfo.Stocker.StockerId;
            plcHostInfo.BlockInfos = SignalMapper4_11.SignalBlocks.Select(b =>
                new BlockInfo(b.DeviceRange, $@"Global\{_lcsInfo.Stocker.StockerId}-{b.SharedMemoryName}", b.PLCRawdataIndex));

            _plcHost = new MPLC.MCProtocol.PLCHost(plcHostInfo);
            //_plcHost = new Mirle.MPLC.PLCR.MCPLCHost(plcHostInfo);
            _plcHost.Interval = 200;
            _plcHost.MPLCTimeout = _lcsInfo.Stocker.MPLCTimeout;
            _plcHost.EnableWriteRawData = _lcsInfo.Stocker.EnablePLCRawdata;
            _plcHost.LogBaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "LOG";
            _plcHost.Start();
        }

        private void InitialStockerController()
        {
            var smReader = new SMReadOnlyCachedReader();
            if (_lcsInfo.InMemorySimulator == false)
            {
                if (_lcsInfo.Stocker.UseMCProtocol)
                {
                    _plcHost.EnableAutoReconnect = false;
                }
                IMPLCProvider mplcWriter = new MPLCService(_plcHost.GetMPLCProvider(), _loggerService);
                _mplc = new ReadWriteWrapper(reader: _plcHost as IMPLCProvider, writer: mplcWriter);
                foreach (var block in SignalMapper4_11.SignalBlocks)
                {
                    smReader.AddDataBlock(new SMDataBlockInt32(block.DeviceRange, $@"Global\{_lcsInfo.Stocker.StockerId}-{block.SharedMemoryName}"));
                }
            }
            else
            {
                var smWirter = new SMReadWriter();
                foreach (var block in SignalMapper4_11.SignalBlocks)
                {
                    smReader.AddDataBlock(new SMDataBlockInt32(block.DeviceRange, $@"Global\{_lcsInfo.Stocker.StockerId}-{block.SharedMemoryName}"));
                    smWirter.AddDataBlock(new SMDataBlockInt32(block.DeviceRange, $@"Global\{_lcsInfo.Stocker.StockerId}-{block.SharedMemoryName}"));
                }
                _mplc = new ReadWriteWrapper(smReader, smWirter);
            }

            var signalMapper = new SignalMapper4_11(_mplc);
            var csotStocker = new CSOTStocker(signalMapper, _lcsInfo.Stocker.ControlMode);

            _stockerController = new StockerController(_mplc, csotStocker);
            _stocker = _stockerController.GetStocker();

            //Event
            _stockerController.OnMPLCConnectionStatusChanged += STKC_OnMPLCConnectionStatusChanged;

            var stocker = _stockerController.GetStocker();
            stocker.OnAvailStatusChanged += Stocker_AvailStatusChange;
            stocker.OnKeySwitchChanged += Stocker_KeySwitchChanged;
            stocker.OnSafetyDoorClosedChanged += Stocker_SafetyDoorClosedChanged;
            stocker.OnMaintenanceModeChanged += Stocker_OnMaintenanceModeChanged;
            stocker.OnAreaSensorChanged += Stocker_OnAreaSensorChanged;
            stocker.OnDataLinkStatusChanged += Stocker_OnDataLinkStatusChanged;
            foreach (var craneItem in stocker.Cranes)
            {
                var crane = craneItem as Crane;
                crane.OnAvailStatusChanged += Crane_AvailStatusChange;
                crane.OnStatusChanged += Crane_StatusChange;
                crane.OnLocationUpdated += Crane_LocationUpdated;
                crane.OnEscapeTimeoutStatusChanged += Crane_OnEscapeTimeoutStatusChanged;
                crane.OnAlarmIndexChanged += Crane_AlarmIndexChange;
                crane.OnAlarmCleared += Crane_AlarmClear;

                crane.ReqAckController.OnScanCompleteRequest += Crane_ReqAck_ScanComplete;
                crane.ReqAckController.OnWrongCommandRequest += Crane_ReqAck_WrongCommand;
                crane.ReqAckController.OnEmptyRetrievalRequest += Crane_ReqAck_EmptyRetrieval;
                crane.ReqAckController.OnDoubleStorageRequest += Crane_ReqAck_DoubleStorage;
                crane.ReqAckController.OnEQInterlockErrorRequest += Crane_ReqAck_EQInterlockError;
                crane.ReqAckController.OnIOInterlockErrorRequest += Crane_ReqAck_IOInterlockError;
                crane.ReqAckController.OnIDReadErrorRequest += Crane_ReqAck_IDReadError;
                crane.ReqAckController.OnIDMismatchRequest += Crane_ReqAck_IDMismatch;
                //crane.ReqAckController.OnCstTypeMismatchRequest += ReqAckController_OnCstTypeMismatch;

                foreach (var forkItem in crane.Forks)
                {
                    var fork = forkItem as Fork;
                    fork.OnActive += Fork_Active;
                    fork.OnIdle += Fork_Idle;
                    fork.OnCycle1 += Fork_Cycle1;
                    fork.OnCycle2 += Fork_Cycle2;
                    fork.OnForking1 += Fork_Forking1;
                    fork.OnForking2 += Fork_Forking2;
                    fork.OnCSTPresenceChanged += Fork_CSTPresenceChange;
                    fork.OnCurrentCommandChanged += Fork_CurrentCommandChange;
                    fork.OnCompletedCommandChanged += Fork_CompletedCommandChange;
                }
            }

            foreach (var ioItem in stocker.IOPorts)
            {
                var io = ioItem as IOPort;
                io.OnInServiceChanged += IO_InServiceChange;
                io.OnBCRReadDone += IO_BCRReadDone;
                io.OnCSTWaitIn += IO_CSTWaitIn;
                io.OnCSTWaitOut += IO_CSTWaitOut;
                io.OnCSTRemoved += IO_CSTRemove;
                io.OnDirectionChanged += IO_DirectionChange;
                io.OnAlarmIndexChanged += IO_AlarmIndexChange;
                io.OnAlarmCleared += IO_AlarmClear;

                foreach (var vehicle in io.Vehicles)
                {
                    vehicle.OnLoadPresenceChanged += IO_VehicleLoadPresenceChange;
                }

                foreach (var stageItem in io.Stages)
                {
                    var stage = stageItem as IOStage;
                    stage.OnLoadPresenceChanged += IO_StageLoadPresenceChange;
                }
            }

            foreach (var eqItem in stocker.EQPorts)
            {
                var eq = eqItem as EQPort;
                eq.OnCSTPresentChanged += EQ_CSTPresentChange;
                eq.OnInServiceChanged += EQ_InServiceChange;
                eq.OnLoadRequestStatusChanged += EQ_LoadRequestStatusChange;
                eq.OnPriorityUpChanged += EQ_PriorityUpChange;
            }
        }

        public bool IsPlcConn => _mplc.IsConnected;

        public CSOTStocker GetStocker()
        {
            return _stocker;
        }

        public StockerController GetStockerController()
        {
            return _stockerController;
        }

        public CurrentExeCmdManager GetTaskCmdManager()
        {
            return _currentExeCmdManager;
        }

        #region Timer

        private void BackgroudProc()
        {
            try
            {
                if (_mplc.IsConnected)
                {
                    _lcsWatchDog.SetPLCRComm(LCSWatchDog.WatchDogStatus.Run);
                }
                else
                {
                    _lcsWatchDog.SetPLCRComm(LCSWatchDog.WatchDogStatus.Down);
                }

                if (_lcsWatchDog.STKCStatus == LCSWatchDog.WatchDogStatus.Show)
                {
                    _stkcHost.UIShowUp();
                }

                if (_LastWriteMPLCMCSOnLineTime.AddSeconds(3) > DateTime.Now)
                {
                    if (_lcsParameter.ControlMode_Cur == LCSParameter.ControlMode.OnlineRemote)
                    {
                        _stocker.SetMCSOnlineAsync().Wait();
                    }
                    _LastWriteMPLCMCSOnLineTime = DateTime.Now;
                }

                //記錄Crane 狀態變化
                for (int iRM = 1; iRM <= _lcsInfo.Stocker.CraneCount; iRM++)
                {
                    _stkcHost.GetStatusRecordService().funWriteRMStsDBLog(iRM);
                }

                //記錄 Mileage
                //_stkcHost.GetStatusRecordService().RecordMileage();

                //Auto Set Run when RunEnable=1
                subAutoSetRun_RM();
                subAutoSetRun_IO();

                //CheckCurrentAlarmIsClearOrNot();

                // LCSParameter Auto/Pause
                SCStateCheck();
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Auto Set Crane Run where MPLC Run Enable = 1
        /// </summary>
        private void subAutoSetRun_RM()
        {
            for (int intRM = 1; intRM <= _lcsInfo.Stocker.CraneCount; intRM++)
            {
                try
                {
                    var crane = _stocker.GetCraneById(intRM) as Crane;
                    if (crane == null)
                        continue;

                    if (_stocker.KeySwitchIsAuto == false || crane.CanAutoSetRun == false
                     || crane.Signal.Run.IsOn() || crane.Signal.RunEnable.IsOff())
                        continue;
                    crane.RequestRunAsync().Wait();
                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.Message = "LCS Auto Set Run Bit-On for Crane " + intRM.ToString() + " Success!";
                    _loggerService.ShowUI(0, objLog);
                }
                catch (Exception ex)
                {
                    _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void subAutoSetRun_IO()
        {
            //For IOPort
            foreach (var ioInfo in _lcsInfo.Stocker.IoInfos)
            {
                try
                {
                    var io = _stocker.GetIOPortById(ioInfo.PortTypeIndex) as IOPort;
                    if (io == null)
                        continue;

                    if (io.Signal.Run.IsOn() || io.Signal.RunEnable.IsOff() || io.CanAutoSetRun == false)
                        continue;
                    io.RequestRunAsync().Wait();

                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.Message = "LCS Auto Set Run Bit-On for IO Port " + ioInfo.PortTypeIndex.ToString() + " Success!";
                    _loggerService.ShowUI(0, objLog);
                }
                catch (Exception ex)
                {
                    _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void CheckCurrentAlarmIsClearOrNot()
        {
            if (_mplc.IsConnected)
            {
                var currentAlarms = _alarmService.GetAllCurrentAlarm();
                if (currentAlarms.Any())
                {
                    foreach (var crane in _stocker.Cranes.Where(crane => crane.IsAlarm == false))
                    {
                        try
                        {
                            var craneInfo = _lcsInfo.Stocker.GetCraneInfoByIndex(crane.Id);
                            if (craneInfo != null)
                            {
                                if (currentAlarms.Any(alarm => alarm.EQId == craneInfo.CraneId))
                                {
                                    _alarmService.ClearAllAlarmByCrane(crane.Id);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                        }
                    }
                }

                foreach (var alarm in currentAlarms)
                {
                    try
                    {
                        var ioInfo = _lcsInfo.Stocker.IoInfos.FirstOrDefault(io => io.HostEQPortId == alarm.EQId);
                        if (ioInfo != null)
                        {
                            var io = _stocker.GetIOPortById(ioInfo.PortTypeIndex);
                            if (io != null && io.IsAlarm == false)
                            {
                                _alarmService.ClearAllAlarmByIOPort(io.Id);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                    }
                }
            }
        }

        private void NewCommandProc(Crane crane1, Crane crane2)
        {
            try
            {
                _lcsWatchDog.SetSTKC(LCSWatchDog.WatchDogStatus.Run);

                if (_lcsParameter.SCState_Cur != LCSParameter.SCState.Auto)
                {
                    Thread.Sleep(100);
                    return;
                }

                var queueCmds = _taskCommandService.GetNewTaskCommands().OrderBy(cmd => cmd.QueueDT).ToList();

                if (queueCmds.Count == 0)
                {
                    Thread.Sleep(1000);
                    return;
                }

                if (queueCmds.First().CraneNo == 1)
                {
                    NewCommandProcByCrane(crane1, crane2, queueCmds);
                    NewCommandProcByCrane(crane2, crane1, queueCmds);
                }
                else
                {
                    NewCommandProcByCrane(crane2, crane1, queueCmds);
                    NewCommandProcByCrane(crane1, crane2, queueCmds);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name,
                    $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void NewCommandProcByCrane(Crane crane, Crane otherCrane, List<Structure.TaskDTO> queueCmds)
        {
            //if (crane.IsReadyToReceiveNewCommand() == false || otherCrane.IsSingleCraneMode) || _currentExeCmdManager.AllForkHasCommandByCrane(crane.Id))
            //{
            //    return;
            //}
            //LouisTseng 針對同取不同放 或 不同取同放 搬送效率修改
            if (crane.IsReadyToReceiveNewCommand() == false || otherCrane.IsSingleCraneMode)
            {
                return;
            }

            var firstForkCmd = queueCmds.FirstOrDefault(cmd => cmd.CraneNo == crane.Id);
            if (firstForkCmd != null && firstForkCmd.ForkNo == 1)
            {
                NewCommandProcByFork(crane, queueCmds, 1, 2);
            }
            else if (firstForkCmd != null && firstForkCmd.ForkNo == 2)
            {
                NewCommandProcByFork(crane, queueCmds, 2, 1);
            }
        }

        private void NewCommandProcByFork(Crane crane, List<Structure.TaskDTO> queueCmds, int firstForkNo, int secondForkNo)
        {
            var firstForkHasNoCmd = _currentExeCmdManager.GetCurrentCommandByFork(crane.Id, firstForkNo) == null;
            var secondForkHasNoCmd = _currentExeCmdManager.GetCurrentCommandByFork(crane.Id, secondForkNo) == null;

            var firstForkNewCmds = queueCmds.Where(cmd => cmd.CraneNo == crane.Id && cmd.ForkNo == firstForkNo).ToList();
            var firstForkNewCmd = queueCmds.FirstOrDefault(cmd => cmd.CraneNo == crane.Id && cmd.ForkNo == firstForkNo);
            var secondForkNewCmd = queueCmds.FirstOrDefault(cmd => cmd.CraneNo == crane.Id && cmd.ForkNo == secondForkNo);

            if (firstForkHasNoCmd && firstForkNewCmd != null)
            {
                CreateForkExeCmd(crane, firstForkNewCmd);
            }

            if (secondForkHasNoCmd && secondForkNewCmd != null)
            {
                var firstForkCurrentCmd = _currentExeCmdManager.GetCurrentCommandByFork(crane.Id, firstForkNo);

                if (IsSameFromToCmd_ButDifferentShelfOrPortType(secondForkNewCmd, firstForkCurrentCmd))
                {
                    return;
                }

                //if (IsFirstNextCmdNearerThanSecondNewCmd(firstForkNewCmds, firstForkNewCmd, secondForkNewCmd))
                //{
                //    return;
                //}

                if (crane.IsReadyToReceiveNewCommand() == false)
                {
                    SpinWait.SpinUntil(crane.IsReadyToReceiveNewCommand, 5000);
                    if (crane.IsReadyToReceiveNewCommand() == false)
                        return;
                }

                CreateForkExeCmd(crane, secondForkNewCmd);
            }
        }

        private static bool IsFirstNextCmdNearerThanSecondNewCmd(List<TaskDTO> firstForkNewCmds, TaskDTO firstForkCmd, TaskDTO secondForkNewCmd)
        {
            if (firstForkCmd != null && firstForkNewCmds.Count >= 2)
            {
                var firstForkNextCmd = firstForkNewCmds.Skip(1).FirstOrDefault();
                if (firstForkNextCmd != null)
                {
                    var firstCmdBay = Math.Max(firstForkCmd.SourceBay, firstForkCmd.DestinationBay);
                    var firstNextCmdBay = Math.Max(firstForkNextCmd.SourceBay, firstForkNextCmd.DestinationBay);
                    var secondCmdBay = Math.Max(secondForkNewCmd.SourceBay, secondForkNewCmd.DestinationBay);
                    if (Math.Abs(firstCmdBay - firstNextCmdBay) < Math.Abs(firstCmdBay - secondCmdBay))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsSameFromToCmd_ButDifferentShelfOrPortType(Structure.TaskDTO secondForkNewCmd, ExeCmd firstForkCurrentCmd)
        {
            if (firstForkCurrentCmd != null && firstForkCurrentCmd.TransferMode == secondForkNewCmd.TransferMode)
            {
                if (secondForkNewCmd.TransferMode == clsEnum.TaskMode.Pickup)
                {
                    int.TryParse(firstForkCurrentCmd.Source, out var firstForkSource);
                    int.TryParse(secondForkNewCmd.Source, out var secondForkSource);
                    if (IsShelfLocation(firstForkSource) && IsPortLocation(secondForkSource) ||
                        IsPortLocation(firstForkSource) && IsShelfLocation(secondForkSource))
                    {
                        return true;
                    }
                }
                else if (secondForkNewCmd.TransferMode == clsEnum.TaskMode.Deposit)
                {
                    int.TryParse(firstForkCurrentCmd.Destination, out var leftDestination);
                    int.TryParse(secondForkNewCmd.Destination, out var rightDestination);
                    if (IsShelfLocation(leftDestination) && IsPortLocation(rightDestination) ||
                        IsPortLocation(leftDestination) && IsShelfLocation(rightDestination))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsPortLocation(int forkCmdLocation)
        {
            return forkCmdLocation <= _maxPortNo;
        }

        private bool IsShelfLocation(int forkCmdLocation)
        {
            return forkCmdLocation > _maxPortNo;
        }

        private void CreateForkExeCmd(Crane crane, Structure.TaskDTO forkNewCmd)
        {
            //建立命令
            funGetCMDFromQueue(crane, new ExeCmd(forkNewCmd));
        }

        private void CheckFinishCommandProc()
        {
            CheckFinishCommandQueue();

            CheckFinishCommandByEachFork();

            DeleteSTKCCommand();
        }
        private void DeleteSTKCCommand()
        {
            try
            {
                _taskCommandService.InsertHistory(_lcsInfo.LCSHostId);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name,
                    $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        #endregion Timer

        #region TaskCommand Process

        private void funGetCMDFromQueue(Crane crane, ExeCmd exeCmd)
        {
            try
            {
                if (!_taskCommandService.IsConnected) return;

                var intRM = exeCmd.CraneNo;
                var iFork = exeCmd.ForkNumber;
                _currentExeCmdManager.AddCommand(crane.Id, exeCmd.ForkNumber, exeCmd);

                //Log Process
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.CommandID = exeCmd.CommandID;
                objLog.TaskNo = exeCmd.TaskNo;
                objLog.CarrierID = exeCmd.CSTID;
                objLog.Message = $"Will Put to MPLC CMD !({exeCmd.CraneNo},{exeCmd.CSTID},{exeCmd.TransferMode},{exeCmd.Source},{exeCmd.Destination},{exeCmd.UserID})";
                _loggerService.ShowUI(intRM, objLog);

                var craneCmdInfo = new CraneCmdInfo(Convert.ToInt32(exeCmd.TaskNo.TruncateRight(5)));
                craneCmdInfo.MaxCstIdLength = _lcsInfo.MaxCstIdLength;
                craneCmdInfo.CommandIdForEvent = exeCmd.CommandID;
                craneCmdInfo.TaskNoForEvent = exeCmd.TaskNo;
                craneCmdInfo.CarrierIDForEvent = exeCmd.CSTID;

                //Check ShelfID Length
                const int ShelfIdMaxLength = 7;
                string strTemp = string.Empty;
                int intTemp = 0;
                strTemp = (exeCmd.Source.Length == ShelfIdMaxLength)
                    ? exeCmd.Source.Substring(0, 2) + exeCmd.Source.Substring(3, 2) + exeCmd.Source.Substring(5, 2)
                    : exeCmd.Source;
                if (!int.TryParse(strTemp, out intTemp))
                    return;
                craneCmdInfo.FromLocation = intTemp;

                //Check ShelfID Length
                strTemp = (exeCmd.Destination.Length == ShelfIdMaxLength)
                    ? exeCmd.Destination.Substring(0, 2) + exeCmd.Destination.Substring(3, 2) + exeCmd.Destination.Substring(5, 2)
                    : exeCmd.Destination;
                if (!int.TryParse(strTemp, out intTemp))
                    return;
                craneCmdInfo.ToLocation = intTemp;
                int.TryParse(exeCmd.CSTType.ToUpper(), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var cstType);

                //switch (cstType)
                //{
                //    case 1:
                //        craneCmdInfo.CstType = CstType.Small;
                //        break;

                //    case 2:
                //        craneCmdInfo.CstType = CstType.Big;
                //        break;

                //    case 0xFF:
                //        craneCmdInfo.CstType = CstType.Ignore;
                //        break;

                //    default:
                //        craneCmdInfo.CstType = CstType.Ignore;
                //        break;
                //}
                craneCmdInfo.CstId = exeCmd.CSTID;
                craneCmdInfo.ForkType = iFork == 2 ? CraneCmdForkType.Right : CraneCmdForkType.Left;
                switch (exeCmd.TransferMode)
                {
                    case clsEnum.TaskMode.Move:
                        craneCmdInfo.CmdType = CraneCmdType.MOVE;
                        break;

                    case clsEnum.TaskMode.Scan:
                        craneCmdInfo.CmdType = CraneCmdType.SCAN;
                        break;

                    case clsEnum.TaskMode.Pickup:
                        craneCmdInfo.CmdType = CraneCmdType.FROM;
                        break;

                    case clsEnum.TaskMode.Deposit:
                        craneCmdInfo.CmdType = CraneCmdType.TO;
                        break;

                    case clsEnum.TaskMode.Transfer:
                        craneCmdInfo.CmdType = CraneCmdType.FROM_TO;
                        break;
                }

                if (craneCmdInfo.CmdType == CraneCmdType.FROM || craneCmdInfo.CmdType == CraneCmdType.FROM_TO)
                { craneCmdInfo.BCREnable = exeCmd.BCRReadFlag == "Y"; }

                craneCmdInfo.TravelAxisSpeed = exeCmd.TravelAxisSpeed;
                craneCmdInfo.LifterAxisSpeed = exeCmd.LifterAxisSpeed;
                craneCmdInfo.RotateAxisSpeed = exeCmd.RotateAxisSpeed;
                craneCmdInfo.ForkAxisSpeed = Convert.ToInt32(exeCmd.ForkAxisSpeed);

                var resultCheckMPLCCMD = CheckMPLCCMD(craneCmdInfo, intRM, exeCmd);

                switch (resultCheckMPLCCMD)
                {
                    case CheckResult.OK:
                        if (ErrorCode.Success != WriteCommandProc(crane, craneCmdInfo, exeCmd))
                        {
                            _currentExeCmdManager.RemoveForkCommand(intRM, iFork);
                        }
                        break;

                    case CheckResult.CannotRetrieveFromSourcePort:
                        exeCmd.SetCommandFinish(clsEnum.TaskCmdState.AbnormalFinish, CompleteCode.CannotRetrieveFromSourcePortFromSTKC_P0, "", 0, 0, 0, 0);
                        _loggerService.Trace(intRM, $"{exeCmd.CommandID}|{exeCmd.CSTID}|{exeCmd.TaskNo}| - Finish CannotRetrieveFromSourcePort Command");
                        _currentExeCmdManager.EnqueueFinishCommand(exeCmd);
                        break;

                    case CheckResult.CannotDepositToDestinationPort:
                        exeCmd.SetCommandFinish(clsEnum.TaskCmdState.AbnormalFinish, CompleteCode.CannotDepositToDestinationPortFromSTKC_P1, "", 0, 0, 0, 0);
                        _loggerService.Trace(intRM, $"{exeCmd.CommandID}|{exeCmd.CSTID}|{exeCmd.TaskNo}| - Finish CannotDepositToDestinationPort Command");
                        _currentExeCmdManager.EnqueueFinishCommand(exeCmd);
                        break;

                    case CheckResult.CannotRetrieveHasCarrierOnCrane:
                        exeCmd.SetCommandFinish(clsEnum.TaskCmdState.AbnormalFinish, CompleteCode.CannotRetrieveHasCarrierOnCraneFromSTKC_P2, "", 0, 0, 0, 0);
                        _loggerService.Trace(intRM, $"{exeCmd.CommandID}|{exeCmd.CSTID}|{exeCmd.TaskNo}| - Finish CannotRetrieveHasCarrierOnCrane Command");
                        _currentExeCmdManager.EnqueueFinishCommand(exeCmd);
                        break;

                    case CheckResult.CannotDepositNoCarrierOnCrane:
                        exeCmd.SetCommandFinish( clsEnum.TaskCmdState.AbnormalFinish, CompleteCode.CannotDepositNoCarrierOnCraneFromSTKC_P3, "", 0, 0, 0, 0);
                        _loggerService.Trace(intRM, $"{exeCmd.CommandID}|{exeCmd.CSTID}|{exeCmd.TaskNo}| - Finish CannotDepositNoCarrierOnCrane Command");
                        _currentExeCmdManager.EnqueueFinishCommand(exeCmd);
                        break;

                    case CheckResult.CannotScanHasCarrierOnCrane:
                        exeCmd.SetCommandFinish( clsEnum.TaskCmdState.AbnormalFinish, CompleteCode.CannotScanHasCarrierOnCraneFromSTKC_P4, "", 0, 0, 0, 0);
                        _loggerService.Trace(intRM, $"{exeCmd.CommandID}|{exeCmd.CSTID}|{exeCmd.TaskNo}| - Finish CannotScanHasCarrierOnCrane Command");
                        _currentExeCmdManager.EnqueueFinishCommand(exeCmd);
                        break;

                    case CheckResult.ContinueOrIgnore:
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private enum CheckResult
        {
            OK,
            CannotRetrieveFromSourcePort,
            CannotDepositToDestinationPort,
            CannotRetrieveHasCarrierOnCrane,
            CannotDepositNoCarrierOnCrane,
            CannotScanHasCarrierOnCrane,
            ContinueOrIgnore,
        }

        private CheckResult CheckMPLCCMD(CraneCmdInfo craneCmdInfo, int intRM, ExeCmd exeCmd)
        {
            string strMsg = string.Empty;
            string strEM = string.Empty;
            TraceLogFormat objLog = new TraceLogFormat();

            var crane = _stocker.GetCraneById(intRM);
            switch (craneCmdInfo.CmdType)
            {
                case CraneCmdType.MOVE:
                    return CheckResult.OK;

                case CraneCmdType.TO:
                    // 車上要有物
                    if (IsForkHasCarrier())
                    {
                        if (craneCmdInfo.ToLocation <= 120)
                        {
                            if (funChkPortSts(craneCmdInfo.ToLocation, false, ref strEM) != ErrorCode.Success)
                            {
                                return CheckResult.CannotDepositToDestinationPort;
                            }
                        }
                        return CheckResult.OK;
                    }
                    else
                    {
                        //Log Process
                        objLog.Message = exeCmd.TaskNo + " - Crane CST_Present OFF Cannot Execute To-Command!";
                        _loggerService.ShowUI(intRM, objLog);
                        return CheckResult.CannotDepositNoCarrierOnCrane;
                    }

                case CraneCmdType.FROM:
                    // 車上要無物
                    if (IsForkHasCarrier())
                    {
                        //Log Process
                        objLog.Message = exeCmd.TaskNo + " - Crane CST_Present ON Cannot Execute From-Command!";
                        _loggerService.ShowUI(intRM, objLog);
                        return CheckResult.CannotRetrieveHasCarrierOnCrane;
                    }
                    else
                    {
                        //Source
                        if (craneCmdInfo.FromLocation <= 120)
                        {
                            //Port
                            //Check Port Sts  > 'Must > InService NoAlarm NoCarrierOnPort Direction
                            if (funChkPortSts(craneCmdInfo.FromLocation, true, ref strEM) != ErrorCode.Success)
                            {
                                //Log Process
                                strMsg = exeCmd.TaskNo + " - Check Source Port State Error! ("
                                                          + exeCmd.Source + ","
                                                          + strEM + ")";
                                objLog.Message = strMsg;
                                _loggerService.ShowUI(intRM, objLog);

                                return CheckResult.CannotRetrieveFromSourcePort;
                            }
                        }
                        return CheckResult.OK;
                    }

                case CraneCmdType.FROM_TO:
                    // 車上要無物
                    if (IsForkHasCarrier())
                    {
                        //Log Process
                        objLog.Message = exeCmd.TaskNo + " - Crane CST_Present ON Cannot Execute From_To-Command!";
                        _loggerService.ShowUI(intRM, objLog);
                        return CheckResult.CannotRetrieveHasCarrierOnCrane;
                    }
                    else
                    {
                        //Source
                        if (craneCmdInfo.FromLocation <= 120)
                        {
                            //Port
                            //Check Port Sts  > 'Must > InService NoAlarm NoCarrierOnPort Direction
                            if (funChkPortSts(craneCmdInfo.FromLocation, true, ref strEM) != ErrorCode.Success)
                            {
                                //Log Process
                                objLog.Message = exeCmd.TaskNo + " - Check Source Port State Error! ("
                                                                  + exeCmd.Source + ","
                                                                  + strEM + ")";
                                _loggerService.ShowUI(intRM, objLog);

                                return CheckResult.CannotRetrieveFromSourcePort;
                            }
                        }

                        //Destination
                        if (craneCmdInfo.ToLocation <= 120)
                        {
                            //Port
                            //Check Port Sts  >
                            if (funChkPortSts(craneCmdInfo.ToLocation, false, ref strEM) != ErrorCode.Success)
                            {
                                return CheckResult.CannotDepositToDestinationPort;
                            }
                        }
                        return CheckResult.OK;
                    }

                case CraneCmdType.SCAN:
                    // 車上要無物
                    if (IsForkHasCarrier())
                    {
                        //Log Process
                        objLog.Message = exeCmd.TaskNo + " - Crane CST_Present ON Cannot Execute Scan-Command!";
                        _loggerService.ShowUI(intRM, objLog);
                        return CheckResult.CannotScanHasCarrierOnCrane;
                    }
                    return CheckResult.OK;
            }
            return CheckResult.ContinueOrIgnore;

            bool IsForkHasCarrier()
            {
                return (craneCmdInfo.ForkType == CraneCmdForkType.Left && crane.GetLeftFork().HasCarrier)
                    || (craneCmdInfo.ForkType == CraneCmdForkType.Right && crane.GetRightFork().HasCarrier);
            }
        }

        private int WriteCommandProc(Crane crane, CraneCmdInfo craneCmdInfo, ExeCmd exeCmd)
        {
            int intRet = ErrorCode.Initial;
            if (!_taskCommandService.IsConnected) return ErrorCode.Exception;
            string strEM = string.Empty;
            TraceLogFormat objLog = new TraceLogFormat();
            objLog.CommandID = exeCmd.CommandID;
            objLog.TaskNo = exeCmd.TaskNo;
            objLog.CarrierID = exeCmd.CSTID;
            objLog.Message = string.Empty;

            var craneNo = exeCmd.CraneNo;
            var forkNo = exeCmd.ForkNumber;
            try
            {
                var craneId = _lcsInfo.Stocker.GetCraneInfoByNumber(craneNo, forkNo).CraneId;
                //Write Command to MPLC DT
                exeCmd.SetInitial();
                //var writeCmdTask = crane.WriteNewCommandAsync(craneCmdInfo);
                if (_taskCommandService.FunWriCommand_Proc(exeCmd.GetTaskDTO(), crane, craneCmdInfo) == true)
                {
                    //Success
                    //Write Command to MPLC DT
                    //exeCmd.SetInitial();

                    //Update Task Database Data
                    //_taskCommandService.UpdateTaskCommandStatus(exeCmd.GetTaskDTO());

                    //清除舊的SM資料
                    _loggerService.Trace(craneNo, objLog.CommandID + "|" + objLog.CarrierID + "|" + objLog.TaskNo + "|" + " - subClearAllCmdBuf_SM");
                    _lcsExecutingCmd.GetExecutingCMD(craneNo, forkNo).Clear();

                    //Write 執行中命令到 SM
                    //將 clsDef.ExeCMDBuf 存成 String Array for Write SM
                    WriteExeCMDBufToSM(craneNo, forkNo, exeCmd);

                    objLog.Message = "Write Transfer Command to MPLC Success !";
                    _loggerService.ShowUI(craneNo, objLog);

                    return ErrorCode.Success;
                }
                else
                {
                    //Clear PLC Command Data
                    (_stocker.GetCraneById(craneNo) as Crane).ClearCommandWriteZoneAsync().Wait();

                    //Write LOG
                    objLog.Message = "Write MPLC Transfer Command Fail !";
                    _loggerService.ShowUI(craneNo, objLog);
                    return ErrorCode.PLC_WritePLCErr;
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            return ErrorCode.PLC_WritePLCErr;
        }

        /// <summary>
        /// </summary>
        /// <param name="intPort"></param>
        /// <param name="bolCSTOnPort"></param>
        /// <param name="strEM"></param>
        /// <returns></returns>
        public int funChkPortSts(int intPort, bool bolCSTOnPort, ref string strEM)
        {
            strEM = String.Empty;
            var iPortTypeIndex = 0;

            try
            {
                //Check Port Number Range
                var portInfo = _lcsInfo.Stocker.GetPortInfoByNumber(intPort);
                if (portInfo == null)
                {
                    strEM = "Port Number Out of Range !! ";
                    return TaskCheckErrorCode.ChkPortSts_PortNoOutOfRange;
                }

                iPortTypeIndex = portInfo.PortTypeIndex;
                switch (portInfo.PortType)
                {
                    case PortType.EQ:

                        return ErrorCode.Success;

                    case PortType.IO:
                        var io = _stocker.GetIOPortById(iPortTypeIndex) as IOPort;

                        //if (io.Signal.Run.IsOff())
                        //{
                        //    strEM = "Port(" + iPortTypeIndex.ToString() + ") InService OFF !! ";
                        //    return TaskCheckErrorCode.ChkPortSts_NotInService;
                        //}

                        //if (io.Signal.Fault.IsOn())
                        //{
                        //    strEM = "Port(" + iPortTypeIndex.ToString() + ") Alarm ON !! ";
                        //    return TaskCheckErrorCode.ChkPortSts_AlarmOn;
                        //}

                        if (bolCSTOnPort)
                        {
                            if (io.Signal.UnloadOK.IsOff())
                            {
                                strEM = "Port(" + iPortTypeIndex.ToString() + ") Not UnloadOK !! ";
                                return TaskCheckErrorCode.ChkPortSts_NoUDRQ;
                            }

                            if (io.Signal.OutMode.IsOn())
                            {
                                strEM = "Port(" + iPortTypeIndex.ToString() + ") OutMode != 0";
                                return TaskCheckErrorCode.ChkPortSts_DirModeErr_OutModeOn;
                            }
                            if (io.Signal.InMode.IsOff())
                            {
                                strEM = "Port(" + iPortTypeIndex.ToString() + ") InMode != 1";
                                return TaskCheckErrorCode.ChkPortSts_DirModeErr_InModeOff;
                            }
                        }
                        else
                        {
                            if (io.Signal.LoadOK.IsOff())
                            {
                                strEM = "Port(" + iPortTypeIndex.ToString() + ") Not LoadOK !! ";
                                return TaskCheckErrorCode.ChkPortSts_NoUDRQ;
                            }

                            if (io.Signal.OutMode.IsOff())
                            {
                                strEM = "Port(" + iPortTypeIndex.ToString() + ") OutMode != 1";
                                return TaskCheckErrorCode.ChkPortSts_DirModeErr_OutModeOn;
                            }
                            if (io.Signal.InMode.IsOn())
                            {
                                strEM = "Port(" + iPortTypeIndex.ToString() + ") InMode != 0";
                                return TaskCheckErrorCode.ChkPortSts_DirModeErr_InModeOff;
                            }
                        }
                        return ErrorCode.Success;

                    default:
                        strEM = "Port Type Error !! - " + portInfo.PortType;
                        return TaskCheckErrorCode.ChkPortSts_PortTypeErr;
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                return ErrorCode.Exception;
            }
        }

        private void WriteExeCMDBufToSM(int intRM, int intForkNumber, ExeCmd exeCmd)
        {
            var cmd = _lcsExecutingCmd.GetExecutingCMD(intRM, intForkNumber);
            cmd.CommandID = exeCmd.CommandID;
            cmd.TaskNo = exeCmd.TaskNo;
            cmd.CSTID = exeCmd.CSTID;
            cmd.TaskState = exeCmd.TaskState;
            cmd.CMDState = exeCmd.CMDState;
            cmd.TransferMode = exeCmd.TransferMode;
            cmd.Source = exeCmd.Source;
            cmd.SourceBay = exeCmd.SourceBay;
            cmd.Destination = exeCmd.Destination;
            cmd.DestinationBay = exeCmd.DestinationBay;
            cmd.TravelAxisSpeed = exeCmd.TravelAxisSpeed;
            cmd.LifterAxisSpeed = exeCmd.LifterAxisSpeed;
            cmd.RotateAxisSpeed = exeCmd.RotateAxisSpeed;
            cmd.ForkAxisSpeed = exeCmd.ForkAxisSpeed;
            cmd.UserID = exeCmd.UserID;
            cmd.CSTType = exeCmd.CSTType;
            cmd.BCRReadFlag = exeCmd.BCRReadFlag;
            cmd.BCRReadDT = exeCmd.BCRReadDT;
            cmd.BCRReadStatus = exeCmd.BCRReadStatus;

            cmd.Priority = exeCmd.Priority;
            cmd.QueueDT = exeCmd.QueueDT;
            cmd.InitialDT = exeCmd.InitialDT;
            cmd.ActiveDT = string.Empty;
            cmd.FinishDT = string.Empty;
            cmd.Cycle1StartDT = string.Empty;
            cmd.AtSourceDT = string.Empty;
            cmd.Fork1StartDT = string.Empty;
            cmd.CSTOnCraneDT = string.Empty;
            cmd.Cycle2StartDT = string.Empty;

            cmd.AtDestinationDT = string.Empty;
            cmd.Fork2StartDT = string.Empty;
            cmd.CSTTackOffCraneDT = string.Empty;
            cmd.FinishLocation = string.Empty;
            //cmd.EmptyCST = exeCmd.EmptyCST;
        }

        private void CheckFinishCommandQueue()
        {
            try
            {
                do
                {
                    var cmd = _currentExeCmdManager.DequeueFinishCommand();
                    if (cmd == null)
                        break;
                    _loggerService.ShowUI(cmd.CraneNo, new TraceLogFormat() { Message = $"DequeueFinishCommand(Command [{cmd.CommandID}], TaskNo [{cmd.TaskNo}], CarrierID [{cmd.CSTID}],  ActiveDT [{cmd.ActiveDT}])" });
                    FinishNormalCommand(cmd);
                } while (true);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void CheckFinishCommandByEachFork()
        {
            for (int iRM = 1; iRM <= _lcsInfo.Stocker.CraneCount; iRM++)
            {
                for (int iFork = 1; iFork <= 2; iFork++)
                {
                    try
                    {
                        ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByFork(iRM, iFork);
                        if (exeCmd != null)
                        {
                            MonitoringCommandFinish(iRM, iFork, exeCmd);
                        }
                    }
                    catch (Exception ex)
                    {
                        _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name,
                            $"{ex.Message}\n{ex.StackTrace}");
                    }
                }
            }
        }

        private void FinishNormalCommand(ExeCmd exeCmd)
        {
            var iRM = Convert.ToInt32(exeCmd.CraneNo);
            var iFork = Convert.ToInt32(exeCmd.ForkNumber);

            _loggerService.Trace(iRM, $"{exeCmd.CommandID}|{exeCmd.CSTID}|{exeCmd.TaskNo}| - Process Finish Command");

            //回收已完成命令，Update命令到DB
            subUpdateFinishedTaskCommand(exeCmd);
            //清除SharedMemory 執行中命令
            _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).Clear();

            //清除TaskCmdManager上的命令
            _currentExeCmdManager.RemoveForkCommand(iRM, iFork);
        }

        //檢查TaskCmd完成與回收處理
        private void subUpdateFinishedTaskCommand(ExeCmd exeCmd)
        {
            int intRet = ErrorCode.Initial;
            string strEM = string.Empty;
            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                var iRM = exeCmd.CraneNo;
                var iFork = exeCmd.ForkNumber;
                _taskCommandService.UpdateCompletedTaskCommand(exeCmd.GetTaskDTO());
                string ForkLocation = _lcsInfo.Stocker.GetCraneInfoByNumber(iRM, iFork).CraneShelfId;
                string strMsg = exeCmd.TaskNo + " - Send SC_TaskComplete (" + exeCmd.CommandID + "," + exeCmd.CompleteCode + "," +
                                exeCmd.CSTID + ",L:" + exeCmd.FinishLocation + ",S:" + exeCmd.Source + ",D:" + exeCmd.Destination +
                                ")";

                //Send TaskAgent TaskComplete
                //if (intRet == ErrorCode.Success)
                //{
                //    objLog.Message = strMsg;
                //    _loggerService.ShowUI(iRM, objLog);
                //}
                //else
                //{
                //    objLog.Message = exeCmd.TaskNo + " - Process Fail > " + strEM;
                //    _loggerService.ShowUI(iRM, objLog);
                //}

                //Clear SM Task Command Buffer Data
                _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).Clear();
                _loggerService.Trace(iRM,
                    objLog.CommandID + "|" + objLog.CarrierID + "|" + objLog.TaskNo + "|" + objLog.FunctionName + "|" +
                    exeCmd.TaskNo + " - Clear SM TransferCMD's TaskNo");

                //From命令異常時，多處理第二筆To命令
                //if (exeCmd.TransferMode == clsEnum.TaskMode.Pickup) //From
                //{
                //    if (exeCmd.CompleteCode != "91")
                //    {
                //        UpdDBTask_AbortSameCommandID(iRM, iFork, exeCmd);
                //    }
                //}

                //針對二重格 空出庫 Insert Alarm, Scan命令，二重格空出庫不報Alarm
                //if (exeCmd.CompleteCode == "EC" && exeCmd.TransferMode != clsEnum.TaskMode.SCAN)
                //{
                //    InsertAlarm_EC(exeCmd, exeCmd.FinishLocation);
                //    Task.Delay(1000).Wait();
                //}
                //else if (exeCmd.CompleteCode == "E2" && exeCmd.TransferMode != clsEnum.TaskMode.SCAN)
                //{
                //    InsertAlarm_E2(exeCmd, exeCmd.FinishLocation);
                //    Task.Delay(1000).Wait();
                //}
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void InsertAlarm_EC(ExeCmd exeCmd, string CarrierFinishiLocation)
        {
            _alarmService.InsertWarning(LCSAlarm.DoubleStorage_F011, _lcsInfo.Stocker.GetCraneInfoByNumber(exeCmd.CraneNo, exeCmd.ForkNumber).CraneId, exeCmd.Destination,
                   exeCmd.ForkNumber == 1 ? exeCmd.CommandID : "",
                   exeCmd.ForkNumber == 2 ? exeCmd.CommandID : "",
                   exeCmd.CSTID, CarrierFinishiLocation, exeCmd.Source, exeCmd.Destination, _lcsInfo.Stocker.GetCraneInfoByNumber(exeCmd.CraneNo, exeCmd.ForkNumber).CraneId);
        }

        private void InsertAlarm_E2(ExeCmd exeCmd, string CarrierFinishiLocation)
        {
            _alarmService.InsertWarning(LCSAlarm.EmptyRetrieval_F010, _lcsInfo.Stocker.GetCraneInfoByNumber(exeCmd.CraneNo, exeCmd.ForkNumber).CraneId, exeCmd.Source,
                exeCmd.ForkNumber == 1 ? exeCmd.CommandID : "",
                exeCmd.ForkNumber == 2 ? exeCmd.CommandID : "",
                exeCmd.CSTID, CarrierFinishiLocation, exeCmd.Source, exeCmd.Destination, _lcsInfo.Stocker.GetCraneInfoByNumber(exeCmd.CraneNo, exeCmd.ForkNumber).CraneId);
        }

        private void InsertAlarm_CommandExecuteTimeout(ExeCmd exeCmd)
        {
            var craneId = _lcsInfo.Stocker.GetCraneInfoByNumber(exeCmd.CraneNo, exeCmd.ForkNumber).CraneId;
            _alarmService.SetAlarm(craneId, LCSAlarm.CommandExecuteTimeout_F013, AlarmTypes.LCS);
            var crane = _stocker.GetCraneById(exeCmd.CraneNo);
            Task.Run(() =>
            {
                Task.Delay(2000).Wait();
                while (true)
                {
                    if (crane.Status == StockerEnums.CraneStatus.IDLE)
                    {
                        _alarmService.ClearAlarm(craneId, LCSAlarm.CommandExecuteTimeout_F013);
                        break;
                    }
                    Task.Delay(1000).Wait();
                }
            });
        }

        private int UpdDBTask_AbortSameCommandID(int iRM, int iFork, ExeCmd exeCmd)
        {
            int intRet = ErrorCode.Initial;
            string strEM = string.Empty;

            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                var sameCmds = _taskCommandService.FindToTaskCmdByTheSameCommandId(exeCmd.CommandID);
                foreach (var sameCmd in sameCmds)
                {
                    if (sameCmd.TaskNo != exeCmd.TaskNo && string.IsNullOrEmpty(sameCmd.CompleteCode))
                    {
                        sameCmd.TaskState = clsEnum.TaskState.Complete;
                        sameCmd.CMDState = exeCmd.CMDState;
                        sameCmd.FinishDT = exeCmd.FinishDT;
                        sameCmd.CompleteCode = CompleteCode.CannotExcuteFromSTKC; // "PD";

                        _taskCommandService.UpdateCompletedTaskCommand(sameCmd);

                        //傳送第二筆命令SC_TaskComplete
                        string ForkLocation = _lcsInfo.Stocker.GetCraneInfoByNumber(iRM, iFork).CraneShelfId;
                        objLog.Message = sameCmd.TaskNo + " - Send SC_TaskComplete (" + sameCmd.CommandID + "," +
                                         sameCmd.CSTID + ",L:" + sameCmd.FinishLocation + ",S:" + sameCmd.Source + ",D:" + sameCmd.Destination + ")";
                        _loggerService.ShowUI(iRM, objLog);
                    }
                }
                return intRet;
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                return ErrorCode.Exception;
            }
        }

        private void MonitoringCommandFinish(int intRM, int iFork, ExeCmd exeCmd)
        {
            if (exeCmd.IsCommandFinish())
            {
                _loggerService.ShowUI(exeCmd.CraneNo, new TraceLogFormat() { Message = $"CommandFinish(Command [{exeCmd.CommandID}], TaskNo [{exeCmd.TaskNo}], CarrierID [{exeCmd.CSTID}],  ActiveDT [{exeCmd.ActiveDT}])" });
                FinishNormalCommand(exeCmd);
            }
            else if (DateTime.Now > exeCmd.ExpectedFinishTime)
            {
                var craneShelfId = _lcsInfo.Stocker.GetCraneInfoByNumber(intRM, iFork).CraneShelfId;
                var isCstOnFork = _stocker.GetCraneById(intRM)?.GetForkById(iFork)?.HasCarrier ?? false;
                if (DateTime.Now > exeCmd.CreateTime.AddMinutes(10))
                {
                    InsertAlarm_CommandExecuteTimeout(exeCmd);
                    exeCmd.ForceCommandFinishWithRetry(craneShelfId, isCstOnFork);  //強制 PE 超時，讓Task重下命令，達成Retry機制
                    _loggerService.ShowUI(exeCmd.CraneNo, new TraceLogFormat() { Message = $"Command超時1(Command [{exeCmd.CommandID}], TaskNo [{exeCmd.TaskNo}], CarrierID [{exeCmd.CSTID}],  ActiveDT [{exeCmd.ActiveDT}])" });
                    FinishNormalCommand(exeCmd);
                }
                else if (IsForkIdleAndCraneIsNotInterferenceWaiting(intRM, iFork)
                         && IsCommandStillInTheBuffer(intRM, exeCmd.TaskNo) == false) //確認Crane訊號
                {
                    if (exeCmd.OverDueTime == DateTime.MinValue)
                    {
                        exeCmd.SetOverTime(); //加 5 秒再 Double Check 訊號
                    }
                    else if (DateTime.Now > exeCmd.OverDueTime)
                    {
                        exeCmd.ForceCommandFinishWithRetry(craneShelfId, isCstOnFork);  //強制 PE 超時，讓Task重下命令，達成Retry機制
                        _loggerService.ShowUI(exeCmd.CraneNo, new TraceLogFormat() { Message = $"Command超時2(Command [{exeCmd.CommandID}], TaskNo [{exeCmd.TaskNo}], CarrierID [{exeCmd.CSTID}],  ActiveDT [{exeCmd.ActiveDT}])" });
                        FinishNormalCommand(exeCmd);
                    }
                }
                else
                {
                    exeCmd.ResetFinishTime(); //延長超時時間 15 秒
                }
            }
        }

        private bool IsForkIdleAndCraneIsNotInterferenceWaiting(int craneNo, int ForkNo)
        {
            var crane = _stocker.GetCraneById(craneNo) as Crane;
            if (crane == null)
                return false;

            if (crane.Signal.Dual_InterferenceWaiting.IsOn())
                return false;
            if (ForkNo == 1)
            {
                return (crane.GetLeftFork() as Fork).Signal.Idle.IsOn();
            }
            else
            {
                return (crane.GetRightFork() as Fork).Signal.Idle.IsOn();
            }
        }

        private bool IsCommandStillInTheBuffer(int craneNo, string taskNo)
        {
            var crane = _stocker.GetCraneById(craneNo) as Crane;
            if (crane == null)
                return false;

            return crane.GetBufferCommands().Any(taskNo.EndsWith);
        }

        #endregion TaskCommand Process

        #region STKC Events

        private void STKC_OnMPLCConnectionStatusChanged(object sender, StockerEventArgs args)
        {
            try
            {
                if (args.MPLCIsConnected)
                {
                    _alarmService.ClearAlarm(_lcsInfo.Stocker.StockerId, LCSAlarm.MPLCDisconnection_F009);
                }
                else
                {
                    _alarmService.SetAlarm(_lcsInfo.Stocker.StockerId, LCSAlarm.MPLCDisconnection_F009, AlarmTypes.LCS);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        #endregion STKC Events

        #region Stocker Events

        private void Stocker_AvailStatusChange(object sender, StockerEventArgs args)
        {
            try
            {
                TraceLogFormat objLog = new TraceLogFormat();
                ////Write Log
                objLog.Message = "MPLC_A_STKC_STKAvailStatusChange - " + args.NewAvailStatus.ToString();
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Stocker_KeySwitchChanged(object sender, StockerEventArgs args)
        {
            if (args.KeySwitchIsOn)
            {
                _alarmService.ClearAlarm(_lcsInfo.Stocker.StockerId, LCSAlarm.KeySwitchOff_F001);
            }
            else
            {
                _alarmService.SetAlarm(_lcsInfo.Stocker.StockerId, LCSAlarm.KeySwitchOff_F001, AlarmTypes.LCS);
            }
        }

        private void Stocker_SafetyDoorClosedChanged(object sender, StockerEventArgs args)
        {
            try
            {
                //記錄 安全門開關記錄
                if (args.SafetyDoorIsClosed)
                {
                    _alarmService.SetPLCDoorClosedDTTimestamp();
                }
                else
                {
                    _alarmService.SetPLCDoorOpenDTTimestamp();
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Stocker_OnMaintenanceModeChanged(object sender, StockerEventArgs args)
        {
            if (args.MaintenanceModeIsOn == false)
            {
                _alarmService.ClearAlarm(_lcsInfo.Stocker.StockerId, LCSAlarm.Stocker_MaintenanceMode_FFFF);
            }
            else
            {
                _alarmService.SetAlarm(_lcsInfo.Stocker.StockerId, LCSAlarm.Stocker_MaintenanceMode_FFFF, AlarmTypes.LCS);
            }
        }

        private void Stocker_OnAreaSensorChanged(object sender, StockerEventArgs args)
        {
            try
            {
                foreach (var port in _lcsInfo.Stocker.PortInfos.Where(p => p.AreaSensorStnNo == args.StationId))
                {
                    var eqId = port.HostEQPortId;
                    if (args.ErrorIsOn)
                    {
                        _alarmService.SetAlarm(eqId, LCSAlarm.AreaSensorSignalError_F008, AlarmTypes.LCS, args.StationId);
                    }
                    else
                    {
                        _alarmService.ClearAlarm(eqId, LCSAlarm.AreaSensorSignalError_F008);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Stocker_OnDataLinkStatusChanged(object sender, StockerEventArgs args)
        {
            try
            {
                foreach (var port in _lcsInfo.Stocker.PortInfos.Where(p => p.NetHStnNo == args.StationId))
                {
                    var eqId = port.HostEQPortId;
                    if (args.ErrorIsOn)
                    {
                        _alarmService.SetAlarm(eqId, LCSAlarm.NetHSignalError_F007, AlarmTypes.LCS, args.StationId);
                    }
                    else
                    {
                        _alarmService.ClearAlarm(eqId, LCSAlarm.NetHSignalError_F007);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        #endregion Stocker Events

        #region Crane Events

        private void Crane_AvailStatusChange(object sender, CraneEventArgs args)
        {
            try
            {
                _loggerService.ShowUI(args.Id, new TraceLogFormat() { Message = $"Crane_AvailStatusChange 事件觸發 (NewStatus: [{args.NewStatus})]" });
                var intRMIndex = args.Id;
                foreach (var cmd in _currentExeCmdManager.GetCurrentCommandByCrane(intRMIndex))
                {
                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.CommandID = cmd.CommandID;
                    objLog.TaskNo = cmd.TaskNo;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.Message = "Crane" + intRMIndex.ToString() + " - RM_AvailStatusChange - " + args.NewAvailStatus.ToString();
                    _loggerService.ShowUI(intRMIndex, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Crane_StatusChange(object sender, CraneEventArgs args)
        {
            _loggerService.ShowUI(args.Id, new TraceLogFormat() { Message = $"Crane_StatusChange 事件觸發 (NewStatus: [{args.NewStatus})]" });
            var intRMIndex = args.Id;
            foreach (var cmd in _currentExeCmdManager.GetCurrentCommandByCrane(intRMIndex))
            {
                try
                {
                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.CommandID = cmd.CommandID;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.TaskNo = cmd.TaskNo;

                    _loggerService.ShowUI(args.Id,
                       new TraceLogFormat()
                       {
                           Message = $"Crane_StatusChange 事件 (Command [{cmd.CommandID}], TaskNo [{cmd.TaskNo}], CarrierID [{cmd.CSTID}],  ActiveDT [{cmd.ActiveDT}]) "
                       });

                    switch (args.NewStatus)
                    {
                        case StockerEnums.CraneStatus.BUSY:
                            if (string.IsNullOrEmpty(cmd.ActiveDT) ||
                                cmd.TaskState == clsEnum.TaskState.Queue ||
                                cmd.TaskState == clsEnum.TaskState.Initialize)
                            {
                                cmd.SetActive();
                                objLog.Message = "StatusChange_Active";
                                _loggerService.ShowUI(intRMIndex, objLog);

                                _lcsExecutingCmd.GetExecutingCMD(intRMIndex, cmd.ForkNumber).OnActive(cmd.TaskState, cmd.ActiveDT);
                            }
                            break;

                        case StockerEnums.CraneStatus.IDLE:
                            if (string.IsNullOrWhiteSpace(cmd.ActiveDT))
                            {
                                objLog.Message = "ActiveDT[RM:" + intRMIndex.ToString() + "] IsNullOrWhiteSpace -StatusChange_Idle";
                                _loggerService.ShowUI(intRMIndex, objLog);
                            }
                            else
                            {
                                objLog.Message = "StatusChange_Idle";
                                _loggerService.ShowUI(intRMIndex, objLog);
                            }
                            break;

                        case StockerEnums.CraneStatus.ESCAPE:
                            objLog.Message = "StatusChange_Escape(" + cmd.TaskState + "," + cmd.InitialDT + "," +
                                             cmd.ActiveDT + "," + cmd.C1StartDT + "," + cmd.C2StartDT + ")";
                            _loggerService.ShowUI(intRMIndex, objLog);

                            if (((cmd.TaskState == clsEnum.TaskState.Transferring) && (string.IsNullOrEmpty(cmd.C1StartDT) == false)) ||
                                (string.IsNullOrEmpty(cmd.C1StartDT) == false) ||
                                (string.IsNullOrEmpty(cmd.CSTOnDT) == false) ||
                                (string.IsNullOrEmpty(cmd.C2StartDT) == false))
                            {
                                //命令已完成，Escape接續在後，所以無Idle  (Active > Escape > Idle)
                                objLog.Message = "Active > Escape(=Idle)";
                                _loggerService.ShowUI(intRMIndex, objLog);
                                goto case StockerEnums.CraneStatus.IDLE;
                            }
                            else
                            {
                                objLog.Message = "Escape > Active > Idle";
                                _loggerService.ShowUI(intRMIndex, objLog);
                            }
                            break;

                        case StockerEnums.CraneStatus.Waiting:
                            cmd.SetWaiting();
                            objLog.Message = "StatusChange_Waiting";
                            _loggerService.ShowUI(intRMIndex, objLog);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void Crane_LocationUpdated(object sender, CraneEventArgs args)
        {
            _loggerService.ShowUI(args.Id, new TraceLogFormat() { Message = $"Crane_LocationUpdated 事件觸發 (NewStatus: [{args.NewStatus})]" });
            var intRMIndex = args.Id;
            foreach (var cmd in _currentExeCmdManager.GetCurrentCommandByCrane(intRMIndex))
            {
                try
                {
                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.CommandID = cmd.CommandID;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.TaskNo = cmd.TaskNo;
                    //At Source
                    if (args.T3 == 0 && string.IsNullOrEmpty(cmd.AtSourceDT))
                    {
                        cmd.SetAtSource();

                        objLog.Message = "LocationUpdateOnSource: " + args.Location;
                        _loggerService.ShowUI(intRMIndex, objLog);

                        _lcsExecutingCmd.GetExecutingCMD(intRMIndex, cmd.ForkNumber).OnAtSource(cmd.CMDState, cmd.AtSourceDT);
                    }

                    //At Destination
                    if (args.T3 != 0 && string.IsNullOrEmpty(cmd.AtDestinationDT))
                    {
                        cmd.SetAtDestination();

                        objLog.Message = "LocationUpdateOnDestination: " + args.Location;
                        _loggerService.ShowUI(intRMIndex, objLog);

                        _lcsExecutingCmd.GetExecutingCMD(intRMIndex, cmd.ForkNumber).OnAtDestinationDT(cmd.CMDState, cmd.AtDestinationDT);
                    }
                }
                catch (Exception ex)
                {
                    _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void Crane_OnEscapeTimeoutStatusChanged(object sender, CraneEventArgs args)
        {
            _loggerService.ShowUI(args.Id, new TraceLogFormat() { Message = $"Crane_OnEscapeTimeoutStatusChanged 事件觸發 (NewStatus: [{args.NewStatus})]" });
            if (args.SignalIsOn)
            {
                _alarmService.SetAlarm(_lcsInfo.Stocker.StockerId, LCSAlarm.EscapeTimeout_F012, AlarmTypes.LCS);
            }
            else
            {
                _alarmService.ClearAlarm(_lcsInfo.Stocker.StockerId, LCSAlarm.EscapeTimeout_F012);
            }
        }

        private void Crane_AlarmIndexChange(object sender, AlarmEventArgs args)
        {
            _loggerService.ShowUI(args.AlarmIndex, new TraceLogFormat() { Message = $"Crane_AlarmIndexChange 事件觸發 (AlarmCode: [{args.AlarmCode})]" });
            try
            {
                if (sender is Crane == false)
                    return;
                var crane = sender as Crane;

                var intRMIndex = crane.Id;
                var intPLCErrIndex = args.AlarmIndex;
                var alarmCode = args.AlarmCode;

                ExeCmd exeCmd_LF = _currentExeCmdManager.GetCurrentCommandByFork(intRMIndex, 1);
                var CommandID_LF = exeCmd_LF?.CommandID ?? string.Empty;
                var TaskNo_LF = exeCmd_LF?.TaskNo ?? string.Empty;
                var Source_LF = exeCmd_LF?.Source ?? string.Empty;
                var Destination_LF = exeCmd_LF?.Destination ?? string.Empty;
                var CSTID_LF = exeCmd_LF?.CSTID ?? string.Empty;

                ExeCmd exeCmd_RF = _currentExeCmdManager.GetCurrentCommandByFork(intRMIndex, 2);
                string CommandID_RF = exeCmd_RF?.CommandID ?? string.Empty;
                string TaskNo_RF = exeCmd_RF?.TaskNo ?? string.Empty;
                string Source_RF = exeCmd_RF?.Source ?? string.Empty;
                string Destination_RF = exeCmd_RF?.Destination ?? string.Empty;
                string CSTID_RF = exeCmd_RF?.CSTID ?? string.Empty;

                int intAlarmCode = 0;
                string strAlarmCode = string.Empty;
                string strMsg = string.Empty;
                string strShelfID = string.Empty;

                TraceLogFormat objLog = new TraceLogFormat();

                objLog.Message = "RM(" + intRMIndex.ToString() + ") RM_ErrorIndexChange > PLC ErrorIndex:" + intPLCErrIndex.ToString();
                _loggerService.ShowUI(intRMIndex, objLog);

                if (alarmCode == 0)
                    return;
                //Get AlarmCode fomr MPLC (40705 > "9F01")
                strAlarmCode = alarmCode.ToString("X4");
                _alarmService.ClearAlarm(_lcsInfo.Stocker.GetCraneInfoByIndex(intRMIndex).CraneId, strAlarmCode);

                strShelfID = funRMLocation2ShelfID(intRMIndex, ref strMsg);
                string strCSTLoc = (crane.GetLeftFork() as Fork).Signal.CSTPresence.IsOn() ? _lcsInfo.Stocker.GetCraneInfoByIndex(intRMIndex).CraneShelfId : strShelfID;

                if (string.IsNullOrWhiteSpace(TaskNo_LF) || string.IsNullOrWhiteSpace(TaskNo_RF))
                {
                    _alarmService.SetAlarm(strAlarmCode, _lcsInfo.Stocker.GetCraneInfoByIndex(intRMIndex).CraneId,
                        strShelfID, string.Empty, string.Empty, string.Empty, strCSTLoc, string.Empty, string.Empty,
                        _lcsInfo.Stocker.GetCraneInfoByIndex(intRMIndex).CraneId, intPLCErrIndex.ToString(), AlarmTypes.Stocker, 0);
                }
                else
                {
                    _alarmService.SetAlarm(strAlarmCode, _lcsInfo.Stocker.GetCraneInfoByIndex(intRMIndex).CraneId,
                        strShelfID, CommandID_LF, CommandID_RF, CSTID_LF, strCSTLoc, Source_LF, Destination_LF,
                        _lcsInfo.Stocker.GetCraneInfoByIndex(intRMIndex).CraneId, intPLCErrIndex.ToString(), AlarmTypes.Stocker, 0);
                }

                //var craneShelfId = $"{crane.CurrentBank:D2}{crane.CurrentBay:D3}{crane.CurrentLevel:D2}";
                //var dto = new DataCollectionDTO() { Time = DateTime.Now, Name = $"SHELF_{craneShelfId}_ALARM_{strAlarmCode}", Value = 1 };
                //_dataCollectionRepository.Insert(dto);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// V2.1.1512.16-2
        /// V2.16.1603.16-2
        /// </summary>
        /// <param name="intRMIndex"></param>
        /// <param name="strEM"></param>
        /// <returns></returns>
        private string funRMLocation2ShelfID(int intRMIndex, ref string strEM)
        {
            int gintShelfIDLength = 7;
            string strRMLocation = String.Empty;
            string strShelfID = "0".PadLeft(gintShelfIDLength, "0"[0]);

            var executingCmd = _lcsExecutingCmd.GetExecutingCMD(intRMIndex, 1);
            try
            {
                string strLogHeader = executingCmd.CommandID + "|" + executingCmd.CSTID + "|" + executingCmd.TaskNo + "|STKC|";

                var crane = _stocker.GetCraneById(intRMIndex) as Crane;
                strRMLocation = crane.Signal.Location.GetValue().ToString();

                if (crane.Signal.Dual_InterferenceWaiting.IsOn())
                {
                    _loggerService.Trace(intRMIndex, strLogHeader + executingCmd.TaskNo + " - funRMLocation2ShelfID : RMLocation=" + strRMLocation + " but Dual_InterferenceWaiting On !");

                    strRMLocation = String.IsNullOrEmpty(executingCmd.Source) ? "0" : executingCmd.Source;

                    switch (strRMLocation.Length)
                    {
                        case 5:
                            strShelfID = strRMLocation.PadLeft(6, "0"[0]);
                            strShelfID = strShelfID.Substring(0, 2) + "0" + strShelfID.Substring(2, 2) + strShelfID.Substring(4, 2);
                            break;

                        case 6:
                        default:
                            strShelfID = strRMLocation.PadLeft(gintShelfIDLength, "0"[0]);
                            break;
                    }
                }
                else
                {
                    if (Int32.Parse(strRMLocation) <= 0)
                    {
                        if (String.IsNullOrEmpty(executingCmd.CSTOnCraneDT))
                        { strRMLocation = String.IsNullOrEmpty(executingCmd.Source) ? "0" : executingCmd.Source; }
                        else
                        { strRMLocation = String.IsNullOrEmpty(executingCmd.Destination) ? "0" : executingCmd.Destination; }

                        //Write Log
                        _loggerService.Trace(intRMIndex, strLogHeader + executingCmd.TaskNo + " - funRMLocation2ShelfID : RMLocation=0,then strRMLocation=" + strRMLocation.ToString());

                        switch (strRMLocation.Length)
                        {
                            case 5:
                                strShelfID = strRMLocation.PadLeft(6, "0"[0]);
                                strShelfID = (strShelfID.Substring(0, 2) + "0" + strShelfID.Substring(2, 2) + strShelfID.Substring(4, 2));
                                break;

                            case 6:
                            default:
                                strShelfID = strRMLocation.PadLeft(gintShelfIDLength, "0"[0]);
                                break;
                        }
                    }
                    else if (Int32.Parse(strRMLocation) <= 120)
                    {
                        strShelfID = strRMLocation.PadLeft(gintShelfIDLength, "0"[0]);
                        _loggerService.Trace(intRMIndex, strLogHeader + executingCmd.TaskNo + " - funRMLocation2ShelfID : RMLocation<=120, then strRMLocation=" + strRMLocation);
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(executingCmd.CSTOnCraneDT) && executingCmd.TransferMode == clsEnum.TaskMode.Transfer)
                        {
                            //Write Log

                            _loggerService.Trace(intRMIndex, strLogHeader + executingCmd.TaskNo + " - funRMLocation2ShelfID : RMLocation=" + strRMLocation + " but CSTOnDT IsNullOrEmpty !");

                            strRMLocation = String.IsNullOrEmpty(executingCmd.Source) ? "0" : executingCmd.Source;

                            switch (strRMLocation.Length)
                            {
                                case 5:
                                    strShelfID = strRMLocation.PadLeft(6, "0"[0]);
                                    strShelfID = strShelfID.Substring(0, 2) + "0" + strShelfID.Substring(2, 2) + strShelfID.Substring(4, 2);
                                    break;

                                case 6:
                                default:
                                    strShelfID = strRMLocation.PadLeft(gintShelfIDLength, "0"[0]);
                                    break;
                            }
                        }
                        else
                        {
                            //Write Log

                            _loggerService.Trace(intRMIndex, strLogHeader + executingCmd.TaskNo + " - funRMLocation2ShelfID : RMLocation=" + strRMLocation + " but CSTOnDT =" + executingCmd.CSTOnCraneDT);

                            if (gintShelfIDLength - 1 != strRMLocation.Length)
                                switch (strRMLocation.Length)
                                {
                                    case 5:
                                        strShelfID = strRMLocation.PadLeft(6, "0"[0]);
                                        strShelfID = (strShelfID.Substring(0, 2) + "0" + strShelfID.Substring(2, 2) + strShelfID.Substring(4, 2));
                                        break;

                                    case 6:
                                        strShelfID = strRMLocation.PadLeft(gintShelfIDLength, "0"[0]);
                                        break;

                                    default:
                                        break;
                                }
                            else
                            {
                                strShelfID = strRMLocation.PadLeft(gintShelfIDLength, "0"[0]);
                            }
                        }
                    }
                }
                //Write Log

                _loggerService.Trace(intRMIndex, strLogHeader + executingCmd.TaskNo + " - funRMLocation2ShelfID : RMLocation=" + strRMLocation + ",then strShelfID=" + strShelfID);

                return strShelfID;
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                strRMLocation = executingCmd.Source;
                return strRMLocation.PadLeft(gintShelfIDLength, "0"[0]);
            }
        }

        private void Crane_AlarmClear(object sender, AlarmEventArgs args)
        {
            _loggerService.ShowUI(args.AlarmIndex, new TraceLogFormat() { Message = $"Crane_AlarmClear 事件觸發 (AlarmCode: [{args.AlarmCode})]" });
            try
            {
                if (sender is Crane crane)
                {
                    _alarmService.ClearAllAlarmByCrane(crane.Id);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        #endregion Crane Events

        #region ReqAck Events

        private void Crane_ReqAck_ScanComplete(object sender, ReqAckEventArgs args)
        {
            var intRMIndex = args.CraneId;
            foreach (var cmd in _currentExeCmdManager.GetCurrentCommandByCrane(intRMIndex))
            {
                try
                {
                    var crane = _stocker.GetCraneById(intRMIndex) as Crane;
                    if (crane != null)
                    {
                        cmd.SetBCRRead(crane.GetBCRResultByForkNoAndWait(cmd.ForkNumber, 2000));
                        //Write SM
                        _lcsExecutingCmd.GetExecutingCMD(cmd.CraneNo, cmd.ForkNumber).OnBCRRead(cmd.BCRReadDT, cmd.BCRReplyCSTID, cmd.BCRReadStatus);
                    }

                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.CommandID = cmd.CommandID;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.TaskNo = cmd.TaskNo;
                    objLog.Message = "ScanCompleteReqOn -> CSTID:" + cmd.BCRReplyCSTID;
                    _loggerService.ShowUI(intRMIndex, objLog);
                }
                catch (Exception ex)
                {
                    _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void Crane_ReqAck_WrongCommand(object sender, ReqAckEventArgs args)
        {
            TraceLogFormat objLog = new TraceLogFormat();

            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string streREQ = true ? "ON" : "OFF";

            try
            {
                var cmd = _currentExeCmdManager.GetCurrentCommandByFork(iRM, iFork);
                if (cmd == null)
                    return;
                string ForkLocation = _lcsInfo.Stocker.GetCraneInfoByNumber(iRM, iFork).CraneShelfId;

                objLog.CommandID = cmd.CommandID;
                objLog.CarrierID = cmd.CSTID;
                objLog.TaskNo = cmd.TaskNo;
                objLog.Message = "TransferRequestWrongReq_" + strFork + "_" + streREQ;
                _loggerService.ShowUI(iRM, objLog);

                var crane = _stocker.GetCraneById(iRM) as Crane;
                if (crane != null)
                {
                    var CmdStatus = new ExeCmdFinishState()
                    {
                        CommandType = cmd.TransferMode,
                        CompleteCode = "E1",
                        CstOnFork = crane.GetForkById(iFork).HasCarrier,
                        BcrResult = "",
                        //IsInCycle2 = crane.Signal.T3.GetValue() != 0 || crane.Signal.T4.GetValue() != 0,
                        IsInCycle2 = cmd.IsCstOnTheCrane_IntoCycle2,
                    };

                    var cmdResult = CmdStatus.GetResult();
                    string strFinishDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string strCMDState = ((int)cmdResult.CmdState).ToString();
                    string strFinishLocation = string.Empty;
                    switch (cmdResult.FinishLocation)
                    {
                        case FinishLocationState.OnSource:
                            strFinishLocation = cmd.Source;
                            break;

                        case FinishLocationState.OnDestination:
                            strFinishLocation = cmd.Destination;
                            break;

                        case FinishLocationState.OnCrane:
                            strFinishLocation = ForkLocation;
                            break;

                        default:
                            strFinishLocation = string.Empty;
                            break;
                    }

                    _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnForkTransferRequestWrong(cmdResult.CmdState, strFinishDT, strFinishLocation);

                    cmd.SetCommandFinish(clsEnum.TaskCmdState.AbnormalFinish, cmdResult.CompleteCode, strFinishLocation, 0, 0, 0, 0);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Crane_ReqAck_EmptyRetrieval(object sender, ReqAckEventArgs args)
        {
            TraceLogFormat objLog = new TraceLogFormat();

            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string streREQ = true ? "ON" : "OFF";

            try
            {
                var cmd = _currentExeCmdManager.GetCurrentCommandByFork(iRM, iFork);
                if (cmd != null)
                {
                    //Write Log and Show UI
                    objLog.CommandID = cmd.CommandID;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.TaskNo = cmd.TaskNo;
                    objLog.Message = "EmptyRetrievalReq_" + strFork + "_" + streREQ;
                    _loggerService.ShowUI(iRM, objLog);
                }
                else
                {
                    objLog.Message = "NoTaskNo - EmptyRetrievalReq_" + strFork + "_" + streREQ;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Crane_ReqAck_DoubleStorage(object sender, ReqAckEventArgs args)
        {
            TraceLogFormat objLog = new TraceLogFormat();

            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string streREQ = true ? "ON" : "OFF";

            try
            {
                var cmd = _currentExeCmdManager.GetCurrentCommandByFork(iRM, iFork);
                if (cmd != null)
                {
                    //Write Log and Show UI
                    objLog.CommandID = cmd.CommandID;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.TaskNo = cmd.TaskNo;
                    objLog.Message = "DoubleStorageReq_" + strFork + "_" + streREQ;
                    _loggerService.ShowUI(iRM, objLog);
                }
                else
                {
                    objLog.Message = "NoTaskNo - DoubleStorageReq_" + strFork + "_" + streREQ;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Crane_ReqAck_EQInterlockError(object sender, ReqAckEventArgs args)
        {
            TraceLogFormat objLog = new TraceLogFormat();

            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string streREQ = true ? "ON" : "OFF";

            try
            {
                var cmd = _currentExeCmdManager.GetCurrentCommandByFork(iRM, iFork);
                if (cmd != null)
                {
                    //Write Log and Show UI
                    objLog.CommandID = cmd.CommandID;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.TaskNo = cmd.TaskNo;
                    objLog.Message = "EQInterLockErrReq_" + strFork + "_" + streREQ;
                    _loggerService.ShowUI(iRM, objLog);
                }
                else
                {
                    objLog.Message = "NoTaskNo - EQInterLockErrReq_" + strFork + "_" + streREQ;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Crane_ReqAck_IOInterlockError(object sender, ReqAckEventArgs args)
        {
            TraceLogFormat objLog = new TraceLogFormat();

            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string streREQ = true ? "ON" : "OFF";

            try
            {
                var cmd = _currentExeCmdManager.GetCurrentCommandByFork(iRM, iFork);
                if (cmd != null)
                {
                    //Write Log and Show UI
                    objLog.CommandID = cmd.CommandID;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.TaskNo = cmd.TaskNo;
                    objLog.Message = "IOInterLockErrReq_" + strFork + "_" + streREQ;
                    _loggerService.ShowUI(iRM, objLog);
                }
                else
                {
                    objLog.Message = "NoTaskNo - IOInterLockErrReq_" + strFork + "_" + streREQ;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Crane_ReqAck_IDReadError(object sender, ReqAckEventArgs args)
        {
            TraceLogFormat objLog = new TraceLogFormat();
            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string streREQ = true ? "ON" : "OFF";

            try
            {
                var cmd = _currentExeCmdManager.GetCurrentCommandByFork(iRM, iFork);
                if (cmd != null)
                {
                    var crane = _stocker.GetCraneById(iRM) as Crane;
                    if (crane != null)
                    {
                        cmd.SetBCRRead(crane.GetBCRResultByForkNoAndWait(cmd.ForkNumber, 2000));
                        //Write SM
                        _lcsExecutingCmd.GetExecutingCMD(cmd.CraneNo, cmd.ForkNumber).OnBCRRead(cmd.BCRReadDT, cmd.BCRReplyCSTID, cmd.BCRReadStatus);
                    }

                    //Write Log and Show UI
                    objLog.CommandID = cmd.CommandID;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.TaskNo = cmd.TaskNo;
                    objLog.Message = "IDReadErrorReq_" + strFork + "_" + streREQ + " -> CSTID:" + cmd.BCRReplyCSTID;
                    _loggerService.ShowUI(iRM, objLog);
                }
                else
                {
                    objLog.Message = "NoTaskNo - IDReadErrorReq_" + strFork + "_" + streREQ + " -> CSTID:" + cmd.BCRReplyCSTID;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Crane_ReqAck_IDMismatch(object sender, ReqAckEventArgs args)
        {
            TraceLogFormat objLog = new TraceLogFormat();

            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string streREQ = true ? "ON" : "OFF";
            var strCSTID = string.Empty;

            try
            {
                var cmd = _currentExeCmdManager.GetCurrentCommandByFork(iRM, iFork);
                if (cmd != null)
                {
                    var crane = _stocker.GetCraneById(iRM) as Crane;
                    if (crane != null)
                    {
                        cmd.SetBCRRead(crane.GetBCRResultByForkNoAndWait(cmd.ForkNumber, 2000));
                    }
                    strCSTID = cmd.BCRReplyCSTID;

                    //Write Log and Show UI
                    objLog.CommandID = cmd.CommandID;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.TaskNo = cmd.TaskNo;
                    objLog.Message = "IDMismatchReq_" + strFork + "_" + streREQ + " -> CSTID:" + strCSTID;
                    _loggerService.ShowUI(iRM, objLog);
                }
                else
                {
                    objLog.Message = "NoTaskNo - IDMismatchReq_" + strFork + "_" + streREQ + " -> CSTID:" + strCSTID;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void ReqAckController_OnCstTypeMismatch(object sender, ReqAckEventArgs args)
        {
            TraceLogFormat objLog = new TraceLogFormat();

            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string streREQ = true ? "ON" : "OFF";

            try
            {
                var cmd = _currentExeCmdManager.GetCurrentCommandByFork(iRM, iFork);
                if (cmd != null)
                {
                    //Write Log and Show UI
                    objLog.CommandID = cmd.CommandID;
                    objLog.CarrierID = cmd.CSTID;
                    objLog.TaskNo = cmd.TaskNo;
                    objLog.Message = "CstTypeMismatchReq_" + strFork + "_" + streREQ;
                    _loggerService.ShowUI(iRM, objLog);
                }
                else
                {
                    objLog.Message = "NoTaskNo - CstTypeMismatchReq_" + strFork + "_" + streREQ;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        #endregion ReqAck Events

        #region Fork Events

        private void Fork_Active(object sender, ForkEventArgs args)
        {
            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByMplcCmdNo(args.CurrentCommandId);
                if (exeCmd != null && (string.IsNullOrEmpty(exeCmd.ActiveDT) ||
                        exeCmd.TaskState == clsEnum.TaskState.Queue ||
                        exeCmd.TaskState == clsEnum.TaskState.Initialize))
                {
                    objLog.CommandID = exeCmd.CommandID;
                    objLog.TaskNo = exeCmd.TaskNo;
                    objLog.CarrierID = exeCmd.CSTID;
                    objLog.Message = "ActiveOn_" + strFork;
                    _loggerService.ShowUI(iRM, objLog);

                    exeCmd.SetActive();

                    //Write SM
                    _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnActive(exeCmd.TaskState, exeCmd.ActiveDT);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Fork_Idle(object sender, ForkEventArgs args)
        {
            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByMplcCmdNo(args.CurrentCommandId);
                if (exeCmd != null && string.IsNullOrEmpty(exeCmd.ActiveDT))
                {
                    objLog.CommandID = exeCmd.CommandID;
                    objLog.TaskNo = exeCmd.TaskNo;
                    objLog.CarrierID = exeCmd.CSTID;
                    objLog.Message = "IdleOn_" + strFork;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Fork_Cycle1(object sender, ForkEventArgs args)
        {
            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByMplcCmdNo(args.CurrentCommandId);
                if (exeCmd != null && string.IsNullOrEmpty(exeCmd.C1StartDT))
                {
                    if (string.IsNullOrEmpty(exeCmd.ActiveDT) ||
                        exeCmd.TaskState == clsEnum.TaskState.Queue ||
                        exeCmd.TaskState == clsEnum.TaskState.Initialize)
                    {
                        exeCmd.SetActive();
                        _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnActive(exeCmd.TaskState, exeCmd.ActiveDT);
                    }

                    exeCmd.SetCycle1();
                    //Write SM
                    _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnForkCycle1(exeCmd.CMDState, exeCmd.C1StartDT);

                    objLog.CommandID = exeCmd.CommandID;
                    objLog.TaskNo = exeCmd.TaskNo;
                    objLog.CarrierID = exeCmd.CSTID;
                    objLog.Message = "Cycle1On_" + strFork;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Fork_Cycle2(object sender, ForkEventArgs args)
        {
            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByMplcCmdNo(args.CurrentCommandId);
                if (exeCmd != null && (string.IsNullOrEmpty(exeCmd.C2StartDT) ||
                        exeCmd.TaskState == clsEnum.TaskState.Queue ||
                        exeCmd.TaskState == clsEnum.TaskState.Initialize))
                {
                    if (string.IsNullOrEmpty(exeCmd.ActiveDT))
                    {
                        exeCmd.SetActive();
                        _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnActive(exeCmd.TaskState, exeCmd.ActiveDT);
                    }
                    //Write SM Cycle2StartDT
                    exeCmd.SetCycle2();
                    _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnForkCycle2(exeCmd.CMDState, exeCmd.C2StartDT);

                    objLog.CommandID = exeCmd.CommandID;
                    objLog.TaskNo = exeCmd.TaskNo;
                    objLog.CarrierID = exeCmd.CSTID;
                    objLog.Message = "Cycle2On_" + strFork;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Fork_Forking1(object sender, ForkEventArgs args)
        {
            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByMplcCmdNo(args.CurrentCommandId);
                if (exeCmd != null && string.IsNullOrEmpty(exeCmd.F1StartDT))
                {
                    //Write SM Fork1StartDT
                    exeCmd.SetForking1();
                    _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnForkForking1(exeCmd.CMDState, exeCmd.F1StartDT);

                    objLog.CommandID = exeCmd.CommandID;
                    objLog.TaskNo = exeCmd.TaskNo;
                    objLog.CarrierID = exeCmd.CSTID;
                    objLog.Message = "Forking1On_" + strFork;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Fork_Forking2(object sender, ForkEventArgs args)
        {
            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByMplcCmdNo(args.CurrentCommandId);
                if (exeCmd != null && string.IsNullOrEmpty(exeCmd.F2StartDT))
                {
                    //Write SM Fork2StartDT
                    exeCmd.SetForking2();
                    _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnForkForking2(exeCmd.CMDState, exeCmd.F2StartDT);

                    objLog.CommandID = exeCmd.CommandID;
                    objLog.TaskNo = exeCmd.TaskNo;
                    objLog.CarrierID = exeCmd.CSTID;
                    objLog.Message = "Forking2On_" + strFork;
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Fork_CSTPresenceChange(object sender, ForkEventArgs args)
        {
            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string strEM = string.Empty;
            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByFork(iRM, iFork);
                if (exeCmd != null)
                {
                    if (args.SignalIsOn)
                    {
                        exeCmd.SetCstOnCrane();
                        //Write SM CSTOnCraneDT
                        _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnCSTOnCrane(exeCmd.CMDState, exeCmd.CSTOnDT);

                        //Write Log and Show UI
                        objLog.CommandID = exeCmd.CommandID;
                        objLog.TaskNo = exeCmd.TaskNo;
                        objLog.CarrierID = exeCmd.CSTID;
                        objLog.Message = "CSTPresentChange_" + strFork + "(" + (args.SignalIsOn ? "On" : "Off") + ")";
                        _loggerService.ShowUI(iRM, objLog);

                        //Send SC_CSTOnRM to Task
                        string strSource = exeCmd.Source;
                        string strDestination = exeCmd.Destination;

                        //Write Log and Show UI
                        objLog.CommandID = exeCmd.CommandID;
                        objLog.TaskNo = exeCmd.TaskNo;
                        objLog.CarrierID = exeCmd.CSTID;
                        objLog.Message = exeCmd.TaskNo + " - Send SC_CSTOnRM-" + strFork + " (" + exeCmd.CommandID + "," +
                                         exeCmd.CSTID + ",L:" + strSource + ",S:" + strSource + ",D:" + strDestination + ")";
                        _loggerService.ShowUI(iRM, objLog);
                    }
                    else
                    {
                        exeCmd.SetCstOffCrane();
                        //Write SM CSTTackOffCraneDT
                        _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnCSTOffCrane(exeCmd.CMDState, exeCmd.CSTTakeOffDT);

                        //Write Log and Show UI
                        objLog.CommandID = exeCmd.CommandID;
                        objLog.TaskNo = exeCmd.TaskNo;
                        objLog.CarrierID = exeCmd.CSTID;
                        objLog.Message = "CSTPresentChange_" + strFork + "(" + (args.SignalIsOn ? "On" : "Off") + ")";
                        _loggerService.ShowUI(iRM, objLog);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Fork_CurrentCommandChange(object sender, ForkEventArgs args)
        {
            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string strEM = string.Empty;
            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                if (args.CurrentCommandId == "00000")
                    return;
                ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByMplcCmdNo(args.CurrentCommandId);
                if (exeCmd != null)
                {
                    if (string.IsNullOrEmpty(exeCmd.ActiveDT) ||
                        exeCmd.TaskState == clsEnum.TaskState.Queue ||
                        exeCmd.TaskState == clsEnum.TaskState.Initialize)
                    {
                        exeCmd.SetActive();
                        //Write SM
                        _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnForkCurrentCommandChanged(exeCmd.TaskState, exeCmd.ActiveDT);
                    }

                    ////Write Log
                    objLog.CommandID = exeCmd.CommandID;
                    objLog.CarrierID = exeCmd.CSTID;
                    objLog.TaskNo = exeCmd.TaskNo;
                    objLog.Message = "TransferNumberChange_" + strFork + "(" + args.CurrentCommandId + ")";
                    _loggerService.ShowUI(iRM, objLog);

                    _taskCommandService.UpdateTaskCommandStatus(exeCmd.GetTaskDTO());

                    objLog.Message = "Send SC_TaskActive (" + exeCmd.CommandID + "," +
                                     exeCmd.CSTID + ",L:" + exeCmd.Source + ",S:" + exeCmd.Source + ",D:" +
                                     exeCmd.Destination + ")";
                    _loggerService.ShowUI(iRM, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Fork_CompletedCommandChange(object sender, ForkEventArgs args)
        {
            int iRM = args.CraneId;
            int iFork = args.ForkId;
            string strFork = iFork == 1 ? "LF" : "RF";
            string completedCmdID = args.CompletedCommandId;
            string completedCode = args.CompletedCode;
            ExeCmd exeCmd = null;

            TraceLogFormat objLog = new TraceLogFormat();

            try
            {
                exeCmd = _currentExeCmdManager.GetCurrentCommandByMplcCmdNo(completedCmdID);
                if (exeCmd == null)
                {
                    return;
                }

                if (String.IsNullOrWhiteSpace(completedCmdID))
                {
                    ////Write Log
                    objLog.CommandID = exeCmd.CommandID;
                    objLog.CarrierID = exeCmd.CSTID;
                    objLog.TaskNo = exeCmd.TaskNo;
                    objLog.Message = "TaskNo is null - CompletedCommandChange_" + strFork;
                    _loggerService.ShowUI(iRM, objLog);
                    return;
                }

                ////Write Log
                objLog.CommandID = exeCmd.CommandID;
                objLog.CarrierID = exeCmd.CSTID;
                objLog.TaskNo = exeCmd.TaskNo;
                objLog.Message = "CompletedCommandChange_" + strFork + "(" + completedCmdID + "," + completedCode + ",Status:"
                                 + (_stockerController.GetStocker().GetCraneById(iRM) as Crane).Status.ToString() + ")";
                _loggerService.ShowUI(iRM, objLog);

                //Check SM have Command or not ?
                if (String.IsNullOrWhiteSpace(completedCmdID))
                {
                    //無動作中命令
                }
                else   //fro Normal Process
                {
                    var crane = _stocker.GetCraneById(iRM) as Crane;
                    //有動作中命令
                    string strFinishDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    var CmdStatus = new ExeCmdFinishState()
                    {
                        CommandType = exeCmd.TransferMode,
                        CompleteCode = completedCode,
                        CstOnFork = crane.GetForkById(iFork).HasCarrier,
                        BcrResult = "",
                        //IsInCycle2 = crane.Signal.T3.GetValue() != 0 || crane.Signal.T4.GetValue() != 0,
                        IsInCycle2 = exeCmd.IsCstOnTheCrane_IntoCycle2,
                    };

                    var cmdResult = CmdStatus.GetResult();

                    string strCompleteCode = cmdResult.CompleteCode;
                    clsEnum.TaskCmdState strCMDState = cmdResult.CmdState;
                    string ForkLocation = _lcsInfo.Stocker.GetCraneInfoByNumber(iRM, iFork).CraneShelfId;
                    string source = exeCmd.Source;
                    string destination = exeCmd.Destination;
                    string strFinishLocation = string.Empty;

                    switch (cmdResult.FinishLocation)
                    {
                        case FinishLocationState.OnSource:
                            strFinishLocation = exeCmd.Source;// clsComSubFun.funRMLocation2ShelfID(source, ref strEM);
                            break;

                        case FinishLocationState.OnDestination:
                            strFinishLocation = exeCmd.Destination;// clsComSubFun.funRMLocation2ShelfID(destination, ref strEM);
                            break;

                        case FinishLocationState.OnCrane:
                            strFinishLocation = ForkLocation;
                            break;

                        default:
                            strFinishLocation = string.Empty;
                            break;
                    }

                    //From 或 Scan 時,檢查BarCode Result
                    if (exeCmd.TransferMode == clsEnum.TaskMode.Pickup || exeCmd.TransferMode == clsEnum.TaskMode.Transfer || exeCmd.TransferMode == clsEnum.TaskMode.Scan)
                    {
                        if (string.IsNullOrWhiteSpace(args.BCRResult) == false)
                        {
                            exeCmd.SetBCRRead(args.BCRResult);

                            //Write SM
                            _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnBCRRead(exeCmd.BCRReadDT, exeCmd.BCRReplyCSTID, exeCmd.BCRReadStatus);
                        }
                    }

                    //Write SM
                    _lcsExecutingCmd.GetExecutingCMD(iRM, iFork).OnCommandFinished(cmdResult.CmdState, strFinishDT, strFinishLocation);

                    //將命令狀態設爲結束
                    exeCmd.SetCommandFinish(strCMDState, strCompleteCode, strFinishLocation,
                        crane.Signal.T1.GetValue(), crane.Signal.T2.GetValue(), crane.Signal.T3.GetValue(), crane.Signal.T4.GetValue());
                    _loggerService.Trace(iRM, $"{exeCmd.CommandID}|{exeCmd.CSTID}|{exeCmd.TaskNo}| - SetCommandFinish");
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        #endregion Fork Events

        #region IO Events

        private void IO_InServiceChange(object sender, IOEventArgs args)
        {
            try
            {
                var intIOIndex = args.IOId;
                _stkcHost.GetStatusRecordService().funWriteIORunStsLog(intIOIndex, args.SignalIsOn);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IO_BCRReadDone(object sender, IOEventArgs args)
        {
            try
            {
                var intIOIndex = args.IOId;
                var strCSTID = args.CstId;

                //Log Process
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = _lcsInfo.Stocker.GetIoInfoByIndex(intIOIndex).HostEQPortId + "(" + intIOIndex.ToString() + ") - PortBCRReadDone -> CSTID:" + strCSTID;

                objLog.CarrierID = strCSTID;
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IO_CSTWaitIn(object sender, IOEventArgs args)
        {
            try
            {
                var intIOIndex = args.IOId;
                var strCSTID = args.CstId;

                //Write Log and Show UI
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = _lcsInfo.Stocker.GetIoInfoByIndex(intIOIndex).HostEQPortId + "(" + intIOIndex.ToString() + ") - CST WaitIn! ID:" + strCSTID;
                objLog.CarrierID = strCSTID;
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IO_CSTWaitOut(object sender, IOEventArgs args)
        {
            try
            {
                var intIOIndex = args.IOId;
                //Log Process
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = _lcsInfo.Stocker.GetIoInfoByIndex(intIOIndex).HostEQPortId + "(" + intIOIndex.ToString() + ") - MPLC_A_IO_PortCSTWaitOut !";

                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IO_CSTRemove(object sender, IOEventArgs args)
        {
            try
            {
                //Log Process
                var intIOIndex = args.IOId;
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = _lcsInfo.Stocker.GetIoInfoByIndex(intIOIndex).HostEQPortId + "(" + intIOIndex.ToString() + ") - MPLC_A_IO_PortCSTRemove !";

                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IO_DirectionChange(object sender, IOEventArgs args)
        {
            try
            {
                //Log Process
                var intIOIndex = args.IOId;
                var eDir = args.NewDirection;
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = _lcsInfo.Stocker.GetIoInfoByIndex(intIOIndex)?.HostEQPortId + "(" + intIOIndex.ToString() + ") - MPLC_A_IO_PortDirectionChange > " + eDir.ToString();

                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IO_VehicleLoadPresenceChange(object sender, IOVehicleEventArgs args)
        {
            try
            {
                var intIOIndex = args.IOPortId;
                var intVehicleNo = args.VehicleId;
                var strCSTID = args.CstId;
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = _lcsInfo.Stocker.GetIoInfoByIndex(intIOIndex).HostEQPortId + "(" + intIOIndex.ToString() + "," + intVehicleNo.ToString() + ") - MPLC_A_IO_PortVehiclePresenceChanged > CSTID:" + strCSTID;
                objLog.CarrierID = strCSTID;
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IO_StageLoadPresenceChange(object sender, IOStageEventArgs args)
        {
            try
            {
                var intIOIndex = args.IOPortId;
                var strCSTID = args.CstId;
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = _lcsInfo.Stocker.GetIoInfoByIndex(intIOIndex).HostEQPortId + "(" + intIOIndex.ToString() + $") - MPLC_A_IO_PortP{args.StageId}LoadPresenceChanged > CSTID:" + strCSTID + ",LoadPresence:" + (args.SignalIsOn ? "On" : "Off");
                objLog.CarrierID = strCSTID;
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IO_AlarmIndexChange(object sender, AlarmEventArgs args)
        {
            try
            {
                if (sender is IOPort == false)
                    return;
                var io = sender as IOPort;

                var intIOIndex = io.Id;
                var alarmCode = args.AlarmCode;
                var intPLCErrIndex = args.AlarmIndex;

                //Get AlarmCode fomr IOPort
                var strAlarmCode = alarmCode.ToString("X4");
                var cstidP1 = (io.GetStageById(1) as IOStage).CstId;

                var ioInfo = _lcsInfo.Stocker.GetIoInfoByIndex(intIOIndex);

                _alarmService.ClearAlarm(ioInfo.HostEQPortId, strAlarmCode);

                _alarmService.SetAlarm(strAlarmCode, ioInfo.HostEQPortId,
                    string.Empty, string.Empty, string.Empty, cstidP1, string.Empty, string.Empty, string.Empty,
                    string.Empty, intPLCErrIndex.ToString(), (AlarmTypes)ioInfo.AlarmType, 0);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IO_AlarmClear(object sender, AlarmEventArgs args)
        {
            try
            {
                if (sender is IOPort io)
                {
                    _alarmService.ClearAllAlarmByIOPort(io.Id);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        #endregion IO Events

        #region EQ Events

        private void EQ_CSTPresentChange(object sender, EQEventArgs args)
        {
            try
            {
                var eq = _lcsInfo.Stocker.GetEqInfoByIndex(args.EQId);
                if (eq != null)
                {
                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.Message = eq.HostEQPortId + "(" + args.EQId.ToString() + ") - MPLC_A_EQ_PortCSTPresent-" + (args.SignalIsOn ? "On" : "Off");
                    _loggerService.ShowUI(0, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void EQ_InServiceChange(object sender, EQEventArgs args)
        {
            try
            {
                var eq = _lcsInfo.Stocker.GetEqInfoByIndex(args.EQId);
                if (eq != null)
                {
                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.Message = eq.HostEQPortId + "(" + args.EQId.ToString() + ") - MPLC_A_EQ_PortServiceChange-" + (args.SignalIsOn ? "On" : "Off");
                    _loggerService.ShowUI(0, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void EQ_LoadRequestStatusChange(object sender, EQEventArgs args)
        {
            try
            {
                var eq = _lcsInfo.Stocker.GetEqInfoByIndex(args.EQId);
                if (eq != null)
                {
                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.Message = eq.HostEQPortId + "(" + args.EQId.ToString() + ") - MPLC_A_EQ_PortStatusChange-" + args.NewLoadRequestStatus.ToString();
                    _loggerService.ShowUI(0, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void EQ_PriorityUpChange(object sender, EQEventArgs args)
        {
            try
            {
                var eq = _lcsInfo.Stocker.GetEqInfoByIndex(args.EQId);
                if (eq != null)
                {
                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.Message = eq.HostEQPortId + "(" + args.EQId.ToString() + ") - MPLC_A_EQ_PortPriorityUp-" + (args.SignalIsOn ? "On" : "Off");
                    _loggerService.ShowUI(0, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        #endregion EQ Events

        private void SCStateCheck()
        {
            var scReq = _lcsParameter.SCState_Req;
            var scCur = _lcsParameter.SCState_Cur;
            if (scReq != scCur)
            {
                var stockerCanPaused = _stocker.Status != StockerEnums.StockerStatus.RUN;

                if (scReq == LCSParameter.SCState.Auto)
                {
                    _lcsParameter.SetSCState(LCSParameter.SCState.Auto);
                    Task.Delay(100).Wait();

                }
                else if (scReq == LCSParameter.SCState.Paused && scCur == LCSParameter.SCState.Auto)
                {
                    _lcsParameter.SetSCState(LCSParameter.SCState.Pausing);
                    Task.Delay(200).Wait();
                    Task.Run(() =>
                    {
                        while (_stocker.Status == StockerEnums.StockerStatus.RUN || _lcsParameter.SCState_Cur == LCSParameter.SCState.Paused)
                        {
                            Task.Delay(50).Wait();
                        }
                        Task.Delay(2000).Wait();
                        _lcsParameter.SetSCState(LCSParameter.SCState.Paused);
                    });
                }
                else if (scReq == LCSParameter.SCState.Paused && scCur != LCSParameter.SCState.Pausing)
                {
                    _lcsParameter.SetSCState(LCSParameter.SCState.Paused);
                }
                else
                {
                    _lcsParameter.PauseRequest();
                }
            }
        }
    }
}
