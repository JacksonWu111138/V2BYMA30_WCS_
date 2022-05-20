using Mirle.Stocker.R46YP320;

using System.Collections.Generic;

namespace Mirle.LCS.Models.Info
{
    public class StockerInfo
    {
        //CraneNoForkNo = 11, 12, 21, 22
        private Dictionary<string, CraneInfo> _craneInfos = new Dictionary<string, CraneInfo>();

        //portIndex, EqInfo
        private Dictionary<int, EqInfo> _eqInfos = new Dictionary<int, EqInfo>();

        //portIndex, IoInfo
        private Dictionary<int, IoInfo> _ioInfos = new Dictionary<int, IoInfo>();

        //PLCPortId, PortInfo
        private Dictionary<int, PortInfo> _portInfos = new Dictionary<int, PortInfo>();

        public StockerInfo(string stockerId, LCSEnums.ControlMode controlMode, int mplcNo, bool enablePLCRawdata = true,
            int mplcTimeout = 5000, string mplcIP = "192.168.10.202", int mplcPort = 1281, bool useMCProtocol = false,
            bool enableWebApiHost = false, int webApiHostPort = 9000)
        {
            StockerId = stockerId;
            ControlMode = controlMode;
            CraneCount = (controlMode == LCSEnums.ControlMode.Single || controlMode == LCSEnums.ControlMode.TwinFork) ? 1 : 2;
            MPLCNo = mplcNo;
            EnablePLCRawdata = enablePLCRawdata;
            MPLCIP = mplcIP;
            MPLCPort = mplcPort;
            MPLCTimeout = mplcTimeout;
            UseMCProtocol = useMCProtocol;
            EnableWebApiHost = enableWebApiHost;
            WebApiHostPort = webApiHostPort;
        }

        public string StockerId { get; internal set; }
        public LCSEnums.ControlMode ControlMode { get; }
        public int MPLCNo { get; } = 1;
        public string MPLCIP { get; } = "192.168.10.202";
        public int MPLCPort { get; } = 1281;
        public int MPLCTimeout { get; } = 5000;
        public bool UseMCProtocol { get; } = false;
        public bool EnableWebApiHost { get; } = false;
        public int WebApiHostPort { get; } = 9000;

        public int CraneCount { get; } = 2;
        public int ForkCount { get; } = 2;
        public bool EnablePLCRawdata { get; }

        public IEnumerable<CraneInfo> CraneInfos => _craneInfos.Values;
        public IEnumerable<IoInfo> IoInfos => _ioInfos.Values;
        public IEnumerable<EqInfo> EqInfos => _eqInfos.Values;
        public IEnumerable<PortInfo> PortInfos => _portInfos.Values;

        public CraneInfo GetCraneInfoByNumber(int craneIndex, int forkNumber)
        {
            _craneInfos.TryGetValue($"{craneIndex}{forkNumber}", out var info);
            return info;
        }

        public CraneInfo GetCraneInfoByIndex(int index)
        {
            _craneInfos.TryGetValue($"{index}1", out var info);
            return info;
        }

        public EqInfo GetEqInfoByIndex(int index)
        {
            _eqInfos.TryGetValue(index, out var eq);
            return eq;
        }

        public IoInfo GetIoInfoByIndex(int index)
        {
            _ioInfos.TryGetValue(index, out var io);
            return io;
        }

        public PortInfo GetPortInfoByNumber(int portNumber)
        {
            _portInfos.TryGetValue(portNumber, out var port);
            return port;
        }

        public void SetCraneDefine(ICollection<KeyValuePair<string, CraneInfo>> list)
        {
            foreach (var keyValuePair in list)
            {
                _craneInfos.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public void SetPortDefine(IEnumerable<PortInfo> list)
        {
            foreach (var port in list)
            {
                try
                {
                    _portInfos.Add(port.PLCPortId, port);
                }
                catch
                {

                }
            }
        }

        public void SetIOPortDefine(IEnumerable<IoInfo> list)
        {
            foreach (var io in list)
            {
                _ioInfos.Add(io.PortTypeIndex, io);
            }
        }

        public void SetEQPortDefine(IEnumerable<EqInfo> list)
        {
            foreach (var eq in list)
            {
                _eqInfos.Add(eq.PortTypeIndex, eq);
            }
        }
    }
}