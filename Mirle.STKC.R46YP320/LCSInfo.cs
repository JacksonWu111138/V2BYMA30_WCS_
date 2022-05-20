using Mirle.STKC.R46YP320.Model;
using Mirle.LCS.Models;
using Mirle.Stocker.R46YP320;
using Mirle.LCS.Models.Info;
using System.Collections.Generic;
using System.Linq;

namespace Mirle.STKC.R46YP320
{
    public class LCSInfo
    {
        private readonly LCSINI _lcsini;
        private Dictionary<string, AlarmDefInfo> _alarmInfos = new Dictionary<string, AlarmDefInfo>();

        public string LCSHostId { get; }
        public LogInfo Log { get; }
        public DatabaseInfo Database { get; }
        public StockerInfo Stocker { get; }
        public IEnumerable<AlarmDefInfo> AlarmInfos => _alarmInfos.Values;

        public FFUConfigInfo FFU { get; }
        public bool InMemorySimulator { get; internal set; }
        public int MaxCstIdLength { get; internal set; }

        internal LCSInfo()
        {
        }

        public LCSInfo(LCSINI lcsini)
        {
            _lcsini = lcsini;
            LCSHostId = lcsini.SystemConfig.StockerID;

            Log = new LogInfo()
            {
                CompressDay = lcsini.LogConfig.CompressDay,
                DeleteDay = lcsini.LogConfig.DeleteDay,
            };

            Database = new DatabaseInfo(
                lcsini.Database.DBMS,
                lcsini.Database.DbServer,
                lcsini.Database.DataSource,
                lcsini.Database.DbUser,
                lcsini.Database.DbPswd);

            Stocker = new StockerInfo(
                lcsini.STKCConfig.StockerID,
                (LCSEnums.ControlMode)lcsini.STKCConfig.ControlMode,
                lcsini.STKCConfig.MPLCNo, 
                lcsini.STKCConfig.WritePLCRawData != 0,
                lcsini.STKCConfig.MPLCTimeout,
                lcsini.STKCConfig.MPLCIP,
                lcsini.STKCConfig.MPLCPort,
                lcsini.STKCConfig.UseMCProtocol !=0);

            FFU = new FFUConfigInfo()
            {
                Enable = lcsini.FFUConfig.Enable != 0,
                IPAddress = lcsini.FFUConfig.IPAddress,
                Port = lcsini.FFUConfig.Port,
                Interval = lcsini.FFUConfig.Interval,
            };

            InMemorySimulator = lcsini.STKCConfig.InMemorySimulator == 1;
            MaxCstIdLength = lcsini.STKCConfig.MaxCstIdLength;
        }

        public void LoadShelfDefine(IEnumerable<ShelfDefDTO> shelfs)
        {
            var list = new Dictionary<string, CraneInfo>();
            //var cranes = shelfs.Where(s => s.StockerId == Stocker.StockerId && s.ShelfType == ShelfType.Crane);
            //var crane11 = cranes.FirstOrDefault(c => c.ShelfId == "0001001");
            //var crane12 = cranes.FirstOrDefault(c => c.ShelfId == "0001002");
            //var crane21 = cranes.FirstOrDefault(c => c.ShelfId == "0002001");
            //var crane22 = cranes.FirstOrDefault(c => c.ShelfId == "0002002");
            //if (crane11 != null) list.Add("11", new CraneInfo() { CraneId = crane11.ZoneId, CraneShelfId = crane11.ShelfId });
            //if (crane12 != null) list.Add("12", new CraneInfo() { CraneId = crane12.ZoneId, CraneShelfId = crane12.ShelfId });
            //if (crane21 != null) list.Add("21", new CraneInfo() { CraneId = crane21.ZoneId, CraneShelfId = crane21.ShelfId });
            //if (crane22 != null) list.Add("22", new CraneInfo() { CraneId = crane22.ZoneId, CraneShelfId = crane22.ShelfId });

            list.Add("11", new CraneInfo() { CraneId = "C1", CraneShelfId = Def.clsConstValue.STKC_FinishLoc.LeftFork }); //C1左Fork
            list.Add("12", new CraneInfo() { CraneId = "C1", CraneShelfId = Def.clsConstValue.STKC_FinishLoc.RightFork }); //C1右Fork

            list.Add("21", new CraneInfo() { CraneId = "C2", CraneShelfId = "0002001" }); //C2左Fork
            list.Add("22", new CraneInfo() { CraneId = "C2", CraneShelfId = "0002002" }); //C2右Fork

            Stocker.SetCraneDefine(list);
        }

        public void LoadPortDefine(IEnumerable<PortDefDTO> ports)
        {
            var portList = new List<PortInfo>();
            foreach (var port in ports)
            {
                portList.Add(new PortInfo()
                {
                    PLCPortId = port.PLCPortId,
                    HostEQPortId = port.HostEQPortId,
                    ShelfId = port.ShelfId,
                    PortType = port.PortType,
                    PortTypeIndex = port.PortTypeIndex,
                    Stage = port.Stage,
                    Vehicles = port.Vehicles,
                    NetHStnNo = port.NetHStnNo,
                    AreaSensorStnNo = port.AreaSensorStnNo,
                });
            }
            Stocker.SetPortDefine(portList);

            var ioList = new List<IoInfo>();
            foreach (var io in ports.Where(p => p.PortType == PortType.IO))
            {
                ioList.Add(new IoInfo()
                {
                    PLCPortId = io.PLCPortId,
                    HostEQPortId = io.HostEQPortId,
                    PortTypeIndex = io.PortTypeIndex,
                    Stage = io.Stage,
                    Vehicles = io.Vehicles,
                    AlarmType = io.AlarmType != 0 ? io.AlarmType : (int)AlarmTypes.IOPort,
                });
            }
            Stocker.SetIOPortDefine(ioList);

            var eqList = new List<EqInfo>();
            foreach (var eq in ports.Where(p => p.PortType == PortType.EQ))
            {
                eqList.Add(new EqInfo()
                {
                    PLCPortId = eq.PLCPortId,
                    HostEQPortId = eq.HostEQPortId,
                    PortTypeIndex = eq.PortTypeIndex,
                });
            }
            Stocker.SetEQPortDefine(eqList);
        }

        public void LoadAlarmDefine(IEnumerable<AlarmDefDTO> alarms)
        {
            foreach (var alarm in alarms)
            {
                _alarmInfos.Add(alarm.AlarmId, new AlarmDefInfo()
                {
                    AlarmType = alarm.AlarmType,
                    AlarmCode = alarm.AlarmCode,
                    AlarmId = alarm.AlarmId,
                    AlarmLevel = alarm.AlarmLevel,
                    ReportEnable = alarm.ReportEnable == "Y",
                });
            }
        }
    }
}
