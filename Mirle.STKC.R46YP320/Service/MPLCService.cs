using Mirle.MPLC;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace Mirle.STKC.R46YP320.Service
{
    public class MPLCService : IMPLCProvider
    {
        private readonly LoggerService _logger;
        private IMPLCProvider _mplc;

        private System.Timers.Timer _checkConnectionTimer = new Timer();
        public bool IsConnected => _mplc.IsConnected;

        public MPLCService(IMPLCProvider mplc, LoggerService logger)
        {
            _mplc = mplc;
            _logger = logger;

            _checkConnectionTimer.Elapsed += CheckConnection;
            _checkConnectionTimer.Interval = 2000;
            _checkConnectionTimer.Start();
        }

        private void CheckConnection(object sender, ElapsedEventArgs e)
        {
            _checkConnectionTimer.Stop();
            try
            {
                if (false == _mplc.IsConnected)
                {
                    (_mplc as IConnectable)?.ReConnect();
                }
            }
            catch (Exception ex)
            {
                TraceLog($"MPLC Connection Fail! -> {ex.Message}-{ex.StackTrace}");
                //開啟地上盤通訊失敗, 等待3秒
                Task.Delay(3000).Wait();
            }
            finally
            {
                _checkConnectionTimer.Start();
            }
        }

        public bool GetBit(string address)
        {
            try
            {
                var value = _mplc.GetBit(address);
                TraceLog($"GetDevice({address}) Success!");
                return value;
            }
            catch (Exception e)
            {
                TraceLog($"GetDevice({address}) Fail! -> {e.Message}");
                //throw e;
                return false;
            }
        }

        public void SetBitOn(string address)
        {
            try
            {
                _mplc.SetBitOn(address);
                TraceLog($"SetDevice({address}) Success! -> On");
            }
            catch (Exception e)
            {
                TraceLog($"SetDevice({address}) Fail! -> {e.Message}");
                //throw e;
            }
        }

        private void TraceLog(string msg)
        {
            _logger.WriteLog("PLCW_SysTrace.log", msg);
        }

        public void SetBitOff(string address)
        {
            try
            {
                _mplc.SetBitOff(address);
                TraceLog($"SetDevice({address}) Success! -> Off");
            }
            catch (Exception e)
            {
                TraceLog($"SetDevice({address}) Fail! -> {e.Message}");
                //throw e;
            }
        }

        public int ReadWord(string address)
        {
            try
            {
                var value = _mplc.ReadWord(address);
                TraceLog($"GetDeviceWord({address}) Success!");
                return value;
            }
            catch (Exception e)
            {
                TraceLog($"GetDeviceWord({address}) Fail! -> {e.Message}");
                //throw e;
                return 0;
            }
        }

        public void WriteWord(string address, int data)
        {
            try
            {
                _mplc.WriteWord(address, data);
                TraceLog($"WriteDeviceWord({address}) Success! -> {data}");
            }
            catch (Exception e)
            {
                TraceLog($"WriteDeviceWord({address}) Fail! -> {e.Message}");
                //throw e;
            }
        }

        public int[] ReadWords(string startAddress, int length)
        {
            try
            {
                var value = _mplc.ReadWords(startAddress, length);
                TraceLog($"GetDeviceBlock(({startAddress}) Success!");
                return value;
            }
            catch (Exception e)
            {
                TraceLog($"GetDeviceBlock(({startAddress}) Fail! -> {e.Message}");
                //throw e;
                return new int[0];
            }
        }

        public void WriteWords(string startAddress, int[] data)
        {
            try
            {
                _mplc.WriteWords(startAddress, data);
                TraceLog($"WriteDeviceBlock({startAddress}) Success! -> {string.Join(",", data)}");
            }
            catch (Exception e)
            {
                TraceLog($"WriteDeviceBlock({startAddress}) Fail! -> {e.Message}");
                //throw e;
            }
        }
    }
}