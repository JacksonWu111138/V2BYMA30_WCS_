using Mirle.Logger;
using Mirle.STKC.R46YP320.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Mirle.STKC.R46YP320.Service
{
    public class LoggerService
    {
        private readonly string _stockerId;
        private readonly Log gobjLog = new Log();
        private readonly object _logLock = new object();
        public int MaxMessageCount = 100;
        private ConcurrentQueue<string> _c1Queue = new ConcurrentQueue<string>();
        private ConcurrentQueue<string> _c2Queue = new ConcurrentQueue<string>();
        private ConcurrentQueue<string> _SysQueue = new ConcurrentQueue<string>();

        public int CompressDay { get; set; }
        //{
        //    get => gobjLog.SetAutoCompressReserveDay;
        //    set
        //    {
        //        gobjLog.SetAutoCompressEnable = value > 0;
        //        gobjLog.SetAutoCompressReserveDay = value;
        //    }
        //}

        public int DeleteDay { get; set; }
        //{
        //    get => gobjLog.SetAutoDeleteReserveDay;
        //    set
        //    {
        //        gobjLog.SetAutoDeleteEnable = value > 0;
        //        gobjLog.SetAutoDeleteReserveDay = value;
        //    }
        //}

        public LoggerService(string stockerId)
        {
            _stockerId = stockerId;
            //gobjLog = new Mirle.Log(AppDomain.CurrentDomain.BaseDirectory + @"LOG\");
            //gobjLog.SetTimerEnable = false;
            //gobjLog.SetAutoCompressReserveDay = 30;
            //gobjLog.SetAutoCompressEnable = gobjLog.SetAutoCompressReserveDay > 0;
            //gobjLog.SetAutoDeleteReserveDay = 90;
            //gobjLog.SetAutoDeleteEnable = gobjLog.SetAutoCompressReserveDay > 0;
            //gobjLog.SetTimerInterval = 86400000;  //一天處理一次
            //gobjLog.SetTimerEnable = gobjLog.SetAutoCompressEnable || gobjLog.SetAutoDeleteEnable;
        }

        public void WriteLog(string fileName, string msg)
        {
            try
            {
                lock (_logLock)
                {
                    gobjLog.WriteLogFile($"{_stockerId}_{fileName}", msg);
                }
            }
            catch (Exception ex)
            {
                Error(nameof(WriteLog), $" : {msg} | {ex.Message}");
            }
        }

        public void Trace(int iRM, string strTraceString)
        {
            try
            {
                lock (_logLock)
                {
                    switch (iRM)
                    {
                        case 1:
                            gobjLog.WriteLogFile($"{_stockerId}_STKC_C1Trace.log", strTraceString);
                            break;

                        case 2:
                            gobjLog.WriteLogFile($"{_stockerId}_STKC_C2Trace.log", strTraceString);
                            break;

                        default:
                            gobjLog.WriteLogFile($"{_stockerId}_STKC_SysTrace.log", strTraceString);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Error(nameof(WriteLog), $" : {strTraceString} | {ex.Message}");
            }
        }

        public void Error(string strFunSubName, string strMsg)
        {
            try
            {
                lock (_logLock)
                {
                    gobjLog.WriteLogFile($"{_stockerId}_STKC_Exception.log",
                        strFunSubName.PadRight(30, ' ') + ":" + strMsg);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public IEnumerable<string> GetNewMessages()
        {
            while (_SysQueue.Any())
            {
                _SysQueue.TryDequeue(out var result);
                yield return result;
            }
        }

        public IEnumerable<string> GetNewMessagesByCraneId(int craneId)
        {
            if (craneId == 1)
            {
                while (_c1Queue.Any())
                {
                    _c1Queue.TryDequeue(out var result);
                    yield return result;
                }
            }
            else
            {
                while (_c2Queue.Any())
                {
                    _c2Queue.TryDequeue(out var result);
                    yield return result;
                }
            }
        }

        public void ShowUI(int iRM, TraceLogFormat objLog)
        {
            switch (iRM)
            {
                case 0:
                    AddMessage(DateTime.Now.ToString("HH:mm:ss.fff") + objLog.Message);
                    break;

                case 1:
                    AddCrane1Message(objLog.Message);
                    break;

                case 2:
                    AddCrane2Message(objLog.Message);
                    break;
            }

            Trace(iRM, objLog.CommandID + "|" + objLog.CarrierID + "|" + objLog.TaskNo + "|" + objLog.FunctionName + "|" + objLog.Message);
        }

        private void AddCrane1Message(string message)
        {
            _c1Queue.Enqueue(message);
            if (_c1Queue.Count > 100)
            {
                _c1Queue.TryDequeue(out var result);
            }
        }

        private void AddCrane2Message(string message)
        {
            _c2Queue.Enqueue(message);
            if (_c2Queue.Count > 100)
            {
                _c2Queue.TryDequeue(out var result);
            }
        }

        private void AddMessage(string message)
        {
            _SysQueue.Enqueue(message);
            if (_SysQueue.Count > 100)
            {
                _SysQueue.TryDequeue(out var result);
            }
        }
    }
}