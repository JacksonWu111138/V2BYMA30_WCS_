using Config.Net;
using Mirle.STKC.R46YP320.LCSShareMemory;
using Mirle.LCS.Models.Info;
using Mirle.STKC.R46YP320.Repositories;
using Mirle.STKC.R46YP320.Service;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Manager;
using Mirle.STKC.R46YP320.ViewModels;
using Mirle.STKC.R46YP320.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using MSSQLDB = Mirle.STKC.R46YP320.Repositories.MSSQL;
using ORACLEDB = Mirle.STKC.R46YP320.Repositories.Oracle;

namespace Mirle.STKC.R46YP320
{
    public class STKCHost : ISTKCHost
    {
        private readonly IUnityContainer _diContainer;

        private readonly LCSInfo _lcsInfo;
        private readonly LoggerService _loggerService;
        private readonly LCSWatchDog _lcsWatchDog;

        private readonly STKCManager _stkcManager;

        private readonly IDbService _dbService;
        private readonly LCSParameter _lcsParameter;
        private readonly LCSExecutingCMD _lcsExecutingCmd;
        private readonly TaskCommandService _taskCommandService;
        private readonly AlarmService _alarmService;
        private readonly StatusRecordService _statusRecordService;
        private readonly CycleRunService _cycleRunService;
        //UI
        private readonly MainView _mainView;
        private readonly StockerStateView stockerStateView;
        private readonly SimMainView _simMainView;

        public event EventHandler OnMainViewShowRequest;

        public STKCHost(InitialTrace initialTrace, int StockerID)
        {
            initialTrace.Show();
            var lcsini = new ConfigurationBuilder<LCSINI>()
                .UseIniFile($"Config\\STKC\\LCS{StockerID}.ini")
                .Build();
            _lcsInfo = new LCSInfo(lcsini);
            initialTrace.Trace($"Initial LCS{StockerID}.ini OK");

            _loggerService = new LoggerService(_lcsInfo.Stocker.StockerId)
            {
                CompressDay = _lcsInfo.Log.CompressDay,
                DeleteDay = _lcsInfo.Log.DeleteDay
            };
            initialTrace.Trace("Initial LoggerService OK");

            _lcsWatchDog = new LCSWatchDog(_lcsInfo.LCSHostId);
            initialTrace.Trace("Initial WatchDog OK");

            _diContainer = new UnityContainer();
            _diContainer.RegisterInstance(_lcsInfo);
            switch (_lcsInfo.Database.DatabaseType)
            {
                case DatabaseInfo.DBType.MSSQL:
                    var mssqlDbService = new MSSqlDbService(_lcsInfo.Database);
                    _dbService = mssqlDbService;
                    CreateMsSqlRepository(mssqlDbService);
                    break;

                case DatabaseInfo.DBType.Oracle_OracleClient:
                    var oracleDbService = new OracleDbService(_lcsInfo.Database);
                    _dbService = oracleDbService;
                    CreateOracleRepository(oracleDbService);
                    break;

                default:
                    throw new InvalidOperationException($"Not Support {_lcsInfo.Database.DatabaseType} DB Type!!");
            }

            initialTrace.Trace("Initial LoadShelfDefine...");
            _lcsInfo.LoadShelfDefine(_diContainer.Resolve<IShelfDefRepository>().GetAll());
            initialTrace.Trace("Initial LoadShelfDefine OK");

            initialTrace.Trace("Initial LoadPortDefine...");
            _lcsInfo.LoadPortDefine(_diContainer.Resolve<IPortDefRepository>().GetAll());
            initialTrace.Trace("Initial LoadPortDefine OK");

            initialTrace.Trace("Initial LoadAlarmDefine...");
            _lcsInfo.LoadAlarmDefine(_diContainer.Resolve<IAlarmDefRepository>().GetAll());
            initialTrace.Trace("Initial LoadAlarmDefine OK");

            initialTrace.Trace("Initial STKC Manager...");
            _lcsParameter = new LCSParameter(_lcsInfo.LCSHostId);
            _lcsExecutingCmd = new LCSExecutingCMD(_lcsInfo.Stocker.StockerId);
            _taskCommandService = new TaskCommandService(this, _loggerService,
                _diContainer.Resolve<ISnoctrlRepository>(), _diContainer.Resolve<ITaskRepository>());

            _alarmService = new AlarmService(this, _loggerService, _lcsInfo.AlarmInfos, _diContainer.Resolve<IAlarmDataRepository>());
            _stkcManager = new STKCManager(_lcsInfo, this, _loggerService, _lcsWatchDog, _alarmService,
                _taskCommandService, _lcsExecutingCmd, _lcsParameter, _diContainer.Resolve<IDataCollectionRepository>());
            var t2Stocker = _stkcManager.GetStockerController().GetStocker();
            _statusRecordService = new StatusRecordService(this, _loggerService, t2Stocker, _lcsParameter,
                _diContainer.Resolve<IUnitDefRepository>(), _diContainer.Resolve<IUnitStsLogRepository>(), _diContainer.Resolve<IMileageLogRepository>());
            Task.Delay(2500).Wait();
            _stkcManager.StartUp();
            initialTrace.Trace("Initial STKC Manager OK");      

            initialTrace.Trace("Initial UI...");
            _mainView = new MainView(this);
            stockerStateView = new StockerStateView(this);
            initialTrace.Trace("Initial UI OK");

            initialTrace.Trace("Initial Complete");
            initialTrace.Close();

            TraceLogFormat objLog = new TraceLogFormat();
            objLog.Message = "STKC (V." + Application.ProductVersion + ") Program Start !";
            _loggerService.ShowUI(0, objLog);

            if (_lcsInfo.InMemorySimulator)
            {
                _simMainView = new SimMainView(_lcsInfo);
                GetSimMainView().Show();
            }
        }

        public STKCHost()
        {
            InitialTrace initialTrace = new InitialTrace();
            initialTrace.Show();
            var lcsini = new ConfigurationBuilder<LCSINI>()
                .UseIniFile($"Config\\STKC\\LCS1.ini")
                .Build();
            _lcsInfo = new LCSInfo(lcsini);
            initialTrace.Trace($"Initial LCS1.ini OK");

            _loggerService = new LoggerService(_lcsInfo.Stocker.StockerId)
            {
                CompressDay = _lcsInfo.Log.CompressDay,
                DeleteDay = _lcsInfo.Log.DeleteDay
            };
            initialTrace.Trace("Initial LoggerService OK");

            _lcsWatchDog = new LCSWatchDog(_lcsInfo.LCSHostId);
            initialTrace.Trace("Initial WatchDog OK");

            _diContainer = new UnityContainer();
            _diContainer.RegisterInstance(_lcsInfo);
            switch (_lcsInfo.Database.DatabaseType)
            {
                case DatabaseInfo.DBType.MSSQL:
                    var mssqlDbService = new MSSqlDbService(_lcsInfo.Database);
                    _dbService = mssqlDbService;
                    CreateMsSqlRepository(mssqlDbService);
                    break;

                case DatabaseInfo.DBType.Oracle_OracleClient:
                    var oracleDbService = new OracleDbService(_lcsInfo.Database);
                    _dbService = oracleDbService;
                    CreateOracleRepository(oracleDbService);
                    break;

                default:
                    throw new InvalidOperationException($"Not Support {_lcsInfo.Database.DatabaseType} DB Type!!");
            }

            initialTrace.Trace("Initial LoadShelfDefine...");
            _lcsInfo.LoadShelfDefine(_diContainer.Resolve<IShelfDefRepository>().GetAll());
            initialTrace.Trace("Initial LoadShelfDefine OK");

            initialTrace.Trace("Initial LoadPortDefine...");
            _lcsInfo.LoadPortDefine(_diContainer.Resolve<IPortDefRepository>().GetAll());
            initialTrace.Trace("Initial LoadPortDefine OK");

            initialTrace.Trace("Initial LoadAlarmDefine...");
            _lcsInfo.LoadAlarmDefine(_diContainer.Resolve<IAlarmDefRepository>().GetAll());
            initialTrace.Trace("Initial LoadAlarmDefine OK");

            initialTrace.Trace("Initial STKC Manager...");
            _lcsParameter = new LCSParameter(_lcsInfo.LCSHostId);
            _lcsExecutingCmd = new LCSExecutingCMD(_lcsInfo.Stocker.StockerId);
            _taskCommandService = new TaskCommandService(this, _loggerService,
                _diContainer.Resolve<ISnoctrlRepository>(), _diContainer.Resolve<ITaskRepository>());

            _alarmService = new AlarmService(this, _loggerService, _lcsInfo.AlarmInfos, _diContainer.Resolve<IAlarmDataRepository>());
            _stkcManager = new STKCManager(_lcsInfo, this, _loggerService, _lcsWatchDog, _alarmService,
                _taskCommandService, _lcsExecutingCmd, _lcsParameter, _diContainer.Resolve<IDataCollectionRepository>());
            var t2Stocker = _stkcManager.GetStockerController().GetStocker();
            _statusRecordService = new StatusRecordService(this, _loggerService, t2Stocker, _lcsParameter,
                _diContainer.Resolve<IUnitDefRepository>(), _diContainer.Resolve<IUnitStsLogRepository>(), _diContainer.Resolve<IMileageLogRepository>());
            Task.Delay(2500).Wait();
            _stkcManager.StartUp();
            initialTrace.Trace("Initial STKC Manager OK");

            initialTrace.Trace("Initial UI...");
            _mainView = new MainView(this);
            initialTrace.Trace("Initial UI OK");

            _cycleRunService = new CycleRunService(_taskCommandService);

            initialTrace.Trace("Initial Complete");
            initialTrace.Close();

            TraceLogFormat objLog = new TraceLogFormat();
            objLog.Message = "STKC (V." + Application.ProductVersion + ") Program Start !";
            _loggerService.ShowUI(0, objLog);

            if (_lcsInfo.InMemorySimulator)
            {
                _simMainView = new SimMainView(_lcsInfo);
                GetSimMainView().Show();
            }
        }

        private void CreateMsSqlRepository(MSSqlDbService dbService)
        {
            _diContainer.RegisterInstance(dbService);
            _diContainer.RegisterType<IShelfDefRepository, MSSQLDB.ShelfDefRepository>();
            _diContainer.RegisterType<IPortDefRepository, MSSQLDB.PortDefRepository>();
            _diContainer.RegisterType<IAlarmDefRepository, MSSQLDB.AlarmDefRepository>();
            _diContainer.RegisterType<ISnoctrlRepository, MSSQLDB.SnoctrlRepository>();
            _diContainer.RegisterType<ITaskRepository, MSSQLDB.TaskRepository>();
            _diContainer.RegisterType<IAlarmDataRepository, MSSQLDB.AlarmDataRepository>();
            _diContainer.RegisterType<IUnitDefRepository, MSSQLDB.UnitDefRepository>();
            _diContainer.RegisterType<IUnitStsLogRepository, MSSQLDB.UnitStsLogRepository>();
            _diContainer.RegisterType<IMileageLogRepository, MSSQLDB.MileageLogRepository>();
            _diContainer.RegisterType<IFFUDefRepository, MSSQLDB.FFUDefRepository>();
            _diContainer.RegisterType<IFFUAlarmDataRepository, MSSQLDB.FFUAlarmDataRepository>();
            _diContainer.RegisterType<IDataCollectionRepository, MSSQLDB.DataCollectionRepository>();
        }

        private void CreateOracleRepository(OracleDbService dbService)
        {
            _diContainer.RegisterInstance(dbService);
            _diContainer.RegisterType<IShelfDefRepository, ORACLEDB.ShelfDefRepository>();
            _diContainer.RegisterType<IPortDefRepository, ORACLEDB.PortDefRepository>();
            _diContainer.RegisterType<IAlarmDefRepository, ORACLEDB.AlarmDefRepository>();
            _diContainer.RegisterType<ISnoctrlRepository, ORACLEDB.SnoctrlRepository>();
            _diContainer.RegisterType<ITaskRepository, ORACLEDB.TaskRepository>();
            _diContainer.RegisterType<IAlarmDataRepository, ORACLEDB.AlarmDataRepository>();
            _diContainer.RegisterType<IUnitDefRepository, ORACLEDB.UnitDefRepository>();
            _diContainer.RegisterType<IUnitStsLogRepository, ORACLEDB.UnitStsLogRepository>();
            _diContainer.RegisterType<IMileageLogRepository, ORACLEDB.MileageLogRepository>();
            _diContainer.RegisterType<IFFUDefRepository, ORACLEDB.FFUDefRepository>();
            _diContainer.RegisterType<IFFUAlarmDataRepository, ORACLEDB.FFUAlarmDataRepository>();
            _diContainer.RegisterType<IDataCollectionRepository, ORACLEDB.DataCollectionRepository>();
        }

        public LCSInfo GetLCSInfo()
        {
            return _lcsInfo;
        }

        public LCSParameter GetLCSParameter()
        {
            return _lcsParameter;
        }

        public LoggerService GetLoggerService()
        {
            return _loggerService;
        }

        public STKCManager GetSTKCManager()
        {
            return _stkcManager;
        }

        public StatusRecordService GetStatusRecordService()
        {
            return _statusRecordService;
        }
    
        public AlarmService GetAlarmService()
        {
            return _alarmService;
        }

        public TaskCommandService GetTaskCommandService()
        {
            return _taskCommandService;
        }

        public CycleRunService GetCycleRunService()
        {
            return _cycleRunService;
        }

        public bool IsDBConnected()
        {
            return _dbService.IsConnected;
        }

        public Form GetMainView()
        {
            return _mainView;
        }

        public Form GetStockerStsView()
        {
            return stockerStateView;
        }

        public Form GetSimMainView()
        {
            return _simMainView;
        }

        public void AppClosing()
        {
            try
            {
                _stkcManager.Stop();
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        public void UIShowUp()
        {
            try
            {
                _loggerService.Trace(0, "WatchDog Show up UI");
                OnMainViewShowRequest?.Invoke(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
