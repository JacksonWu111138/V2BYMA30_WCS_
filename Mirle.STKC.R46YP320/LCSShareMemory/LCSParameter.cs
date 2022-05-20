using Mirle.MPLC.DataType;
using System;
using System.Diagnostics;
using Mirle.Extensions;
using Mirle.MPLC.SharedMemory;
using Mirle.MPLC.DataBlocks.DeviceRange;
using Mirle.MPLC.DataBlocks;

namespace Mirle.STKC.R46YP320.LCSShareMemory
{
    public class LCSParameter : IDisposable
    {
        private readonly string _stockerId;

        public enum SCState
        {
            None = 0,
            SCInit = 1,
            Paused = 2,
            Auto = 3,
            Pausing = 4,
        }

        public enum ControlMode
        {
            NONE = 0,
            Offline = 1,
            AttemptOnline = 2,
            HostOffline = 3,
            OnlineLocal = 4,
            OnlineRemote = 5,
        }

        public enum CommunicationState
        {
            Disable = 0,
            Enable = 1,
        }

        public enum PauseReasonStatus
        {
            MCSRequest = 0,
            WorkOperation = 1,
            PeriodicalMaintenance = 2,
            ErrorRecovery = 3,
            OtherReason = 9,
        }

        public string STKID => _STKID.GetData().ToASCII();

        //public SCState SCState_Lst => _SCState_Lst.GetValue().ToEnum<SCState>();
        public SCState SCState_Cur => _SCState_Cur.GetValue().ToEnum<SCState>();

        public SCState SCState_Req => _SCState_Req.GetValue().ToEnum<SCState>();
        public ControlMode ControlMode_Lst => _ControlMode_Lst.GetValue().ToEnum<ControlMode>();
        public ControlMode ControlMode_Cur => _ControlMode_Cur.GetValue().ToEnum<ControlMode>();
        public ControlMode ControlMode_Req => _ControlMode_Req.GetValue().ToEnum<ControlMode>();
        public CommunicationState CommunicationState_Cur => _CommunicationState_Cur.GetValue().ToEnum<CommunicationState>();

        public bool IsManualOffline => _ManualOfflineFlag.GetValue() == 1;

        public PauseReasonStatus PauseReason => (PauseReasonStatus)_PauseReason.GetValue();

        //public bool IsSCStateCanPaused
        //{
        //    get => _IsSCStateCanPaused;
        //    set => _IsSCStateCanPaused = value;
        //}

        private WordBlock _STKID;

        //private Word _SCState_Lst;
        private Word _SCState_Cur;

        private Word _SCState_Req;
        private Word _ControlMode_Lst;
        private Word _ControlMode_Cur;
        private Word _ControlMode_Req;
        private Word _CommunicationState_Cur;
        private Word _ManualOfflineFlag;
        private Word _PauseReason;

        private System.Timers.Timer _CheckProcess;

        public LCSParameter(string stockerId)
        {
            _stockerId = stockerId;
            var sm = new SMReadWriter();
            var range = new DDeviceRange("D0", "D100");
            var sharedMemoryName = $@"Global\{stockerId}-LCSParameter";
            sm.AddDataBlock(new SMDataBlock(range, sharedMemoryName));

            _STKID = new WordBlock(sm, "D0", 20);
            //_SCState_Lst = new Word(sm, "D10");
            _SCState_Cur = new Word(sm, "D20");
            _SCState_Req = new Word(sm, "D30");
            _ControlMode_Lst = new Word(sm, "D40");
            _ControlMode_Cur = new Word(sm, "D50");
            _ControlMode_Req = new Word(sm, "D60");
            _CommunicationState_Cur = new Word(sm, "D70");
            _ManualOfflineFlag = new Word(sm, "D80");
            _PauseReason = new Word(sm, "D90");
        }

        public void Start()
        {
            _STKID.SetData(_stockerId.ToIntArray(20));
            //_SCState_Lst.SetValue((int)SCState.None);
            _SCState_Cur.SetValue((int)SCState.None);
            _SCState_Req.SetValue((int)SCState.None);
            _ControlMode_Lst.SetValue((int)ControlMode.Offline);
            _ControlMode_Cur.SetValue((int)ControlMode.Offline);
            _ControlMode_Req.SetValue((int)ControlMode.Offline);
            _CommunicationState_Cur.SetValue((int)CommunicationState.Disable);

            if (_CheckProcess != null)
            {
                _CheckProcess.Stop();
                _CheckProcess.Dispose();
                _CheckProcess = null;
            }
            _CheckProcess = new System.Timers.Timer();
            _CheckProcess.Elapsed += new System.Timers.ElapsedEventHandler(CheckProcess_Elapsed);
            _CheckProcess.Interval = 100;
            _CheckProcess.Start();
        }

        public void Stop()
        {
            if (_CheckProcess != null)
            {
                _CheckProcess.Stop();
                _CheckProcess.Dispose();
                _CheckProcess = null;
            }
        }

        public void AutoRequest() => _SCState_Req.SetValue((int)SCState.Auto);

        public void PauseRequest() => _SCState_Req.SetValue((int)SCState.Paused);

        public void PauseRequest(PauseReasonStatus pauseReason)
        {
            _SCState_Req.SetValue((int)SCState.Paused);
            _PauseReason.SetValue((int)pauseReason);
        }

        public void Offline()
        {
            _ManualOfflineFlag.SetValue(0);
            _ControlMode_Req.SetValue((int)ControlMode.Offline);
        }

        public void ManualOffline()
        {
            _ManualOfflineFlag.SetValue(1);
            _ControlMode_Req.SetValue((int)ControlMode.Offline);
        }

        public void OnlineLocal()
        {
            _ManualOfflineFlag.SetValue(0);
            _ControlMode_Req.SetValue((int)ControlMode.OnlineLocal);
        }

        public void OnlineRemote()
        {
            _ManualOfflineFlag.SetValue(0);
            _ControlMode_Req.SetValue((int)ControlMode.OnlineRemote);
        }

        private void CheckProcess_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                _CheckProcess.Stop();
                //CheckProcess_SCState();
                CheckProcess_ControlMode();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}-{ex.StackTrace}");
            }
            finally
            {
                _CheckProcess.Start();
            }
        }

        //private void CheckProcess_SCState()
        //{
        //    try
        //    {
        //        if (SCState_Cur != SCState_Req)
        //        {
        //            switch (SCState_Cur)
        //            {
        //                case SCState.SCInit:
        //                    if (SCState_Req == SCState.Paused)
        //                    {
        //                        _SCState_Lst.SetValue((int)SCState.SCInit);
        //                        _SCState_Cur.SetValue((int)SCState.Paused);
        //                    }
        //                    else
        //                        _SCState_Req.SetValue((int)SCState.Paused);
        //                    break;

        //                case SCState.Paused:
        //                    if (SCState_Req == SCState.Auto)
        //                    {
        //                        _SCState_Lst.SetValue((int)SCState.Paused);
        //                        _SCState_Cur.SetValue((int)SCState.Auto);
        //                    }
        //                    break;

        //                case SCState.Auto:
        //                    if (SCState_Req == SCState.Paused)
        //                    {
        //                        _SCState_Lst.SetValue((int)SCState.Auto);
        //                        _SCState_Cur.SetValue((int)SCState.Pausing);
        //                    }
        //                    break;

        //                case SCState.Pausing:
        //                    if (SCState_Req == SCState.Auto)
        //                    {
        //                        if (_IsSCStateCanPaused)
        //                        {
        //                            _SCState_Lst.SetValue((int)SCState.Pausing);
        //                            _SCState_Cur.SetValue((int)SCState.Auto);
        //                            _IsSCStateCanPaused = false;
        //                        }
        //                    }
        //                    else if (SCState_Req == SCState.Paused)
        //                    {
        //                        _SCState_Lst.SetValue((int)SCState.Pausing);
        //                        _SCState_Cur.SetValue((int)SCState.Paused);
        //                    }
        //                    break;

        //                case SCState.None:
        //                default:
        //                    if (SCState_Req == SCState.SCInit)
        //                    {
        //                        _SCState_Lst.SetValue((int)SCState.None);
        //                        _SCState_Cur.SetValue((int)SCState.SCInit);
        //                        _SCState_Req.SetValue((int)SCState.Paused);
        //                    }
        //                    else if (SCState_Req == SCState.Auto)
        //                    {
        //                        _SCState_Lst.SetValue((int)SCState.None);
        //                        _SCState_Cur.SetValue((int)SCState.Auto);
        //                        _SCState_Req.SetValue((int)SCState.Auto);
        //                    }
        //                    else if (SCState_Req == SCState.Paused)
        //                    {
        //                        _SCState_Lst.SetValue((int)SCState.None);
        //                        _SCState_Cur.SetValue((int)SCState.Pausing);
        //                    }
        //                    break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    { string message = $"{ex.Message}\n{ex.StackTrace}"; }
        //}

        private void CheckProcess_ControlMode()
        {
            try
            {
                if (ControlMode_Cur != ControlMode_Req)
                {
                    switch (ControlMode_Cur)
                    {
                        default:
                        case ControlMode.Offline:
                            if (ControlMode_Req == ControlMode.OnlineRemote)
                            {
                                _ControlMode_Lst.SetValue((int)ControlMode.Offline);
                                _ControlMode_Cur.SetValue((int)ControlMode.OnlineRemote);

                                if (SCState_Cur == SCState.None)
                                    _SCState_Req.SetValue((int)SCState.SCInit);
                                else if (SCState_Cur == SCState.Paused || SCState_Cur == SCState.Pausing || SCState_Cur == SCState.Auto)
                                {
                                    _SCState_Cur.SetValue((int)SCState.None);
                                    _SCState_Req.SetValue((int)SCState.SCInit);
                                }
                            }
                            else if (ControlMode_Req == ControlMode.OnlineLocal)
                            {
                                _ControlMode_Lst.SetValue((int)ControlMode.Offline);
                                _ControlMode_Cur.SetValue((int)ControlMode.OnlineLocal);

                                if (SCState_Cur == SCState.None)
                                    _SCState_Req.SetValue((int)SCState.SCInit);
                                else if (SCState_Cur == SCState.Paused || SCState_Cur == SCState.Pausing || SCState_Cur == SCState.Auto)
                                {
                                    _SCState_Cur.SetValue((int)SCState.None);
                                    _SCState_Req.SetValue((int)SCState.SCInit);
                                }
                            }
                            break;

                        case ControlMode.OnlineLocal:
                            if (ControlMode_Req == ControlMode.OnlineRemote)
                            {
                                _ControlMode_Lst.SetValue((int)ControlMode.OnlineLocal);
                                _ControlMode_Cur.SetValue((int)ControlMode.OnlineRemote);

                                if (SCState_Cur == SCState.None)
                                    _SCState_Req.SetValue((int)SCState.SCInit);
                            }
                            else if (ControlMode_Req == ControlMode.Offline)
                            {
                                if (SCState_Cur == SCState.Paused)
                                {
                                    _ControlMode_Lst.SetValue((int)ControlMode.OnlineLocal);
                                    _ControlMode_Cur.SetValue((int)ControlMode.Offline);
                                }
                            }
                            else if (ControlMode_Req == ControlMode.HostOffline)
                            {
                                if (SCState_Cur == SCState.Paused)
                                {
                                    _ControlMode_Lst.SetValue((int)ControlMode.OnlineRemote);
                                    _ControlMode_Cur.SetValue((int)ControlMode.HostOffline);
                                }
                            }
                            break;

                        case ControlMode.OnlineRemote:
                            if (ControlMode_Req == ControlMode.OnlineLocal)
                            {
                                _ControlMode_Lst.SetValue((int)ControlMode.OnlineRemote);
                                _ControlMode_Cur.SetValue((int)ControlMode.OnlineLocal);

                                if (SCState_Cur == SCState.None)
                                    _SCState_Req.SetValue((int)SCState.SCInit);
                            }
                            else if (ControlMode_Req == ControlMode.Offline)
                            {
                                if (SCState_Cur == SCState.Paused)
                                {
                                    _ControlMode_Lst.SetValue((int)ControlMode.OnlineRemote);
                                    _ControlMode_Cur.SetValue((int)ControlMode.Offline);
                                }
                            }
                            else if (ControlMode_Req == ControlMode.HostOffline)
                            {
                                if (SCState_Cur == SCState.Paused)
                                {
                                    _ControlMode_Lst.SetValue((int)ControlMode.OnlineRemote);
                                    _ControlMode_Cur.SetValue((int)ControlMode.HostOffline);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            { string message = $"{ex.Message}\n{ex.StackTrace}"; }
        }

        #region IDisposable Support

        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_CheckProcess != null)
                    {
                        _CheckProcess.Stop();
                        _CheckProcess.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放非受控資源的程式碼時，才覆寫完成項。
        ~LCSParameter()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support

        public void SetCommunicationState(CommunicationState communicationState)
        {
            _CommunicationState_Cur.SetValue((int)communicationState);
        }

        public void SetHostOffine()
        {
            _ControlMode_Req.SetValue((int)ControlMode.HostOffline);
            _ControlMode_Cur.SetValue((int)ControlMode.HostOffline);
        }

        public void SetControlModeRequest(ControlMode controlMode)
        {
            _ControlMode_Req.SetValue((int)controlMode);
        }

        public void SetSCState(SCState state)
        {
            _SCState_Cur.SetValue((int)state);
        }
    }
}