using Mirle.Extensions;
using Mirle.Stocker.R46YP320.Signal;

namespace Mirle.STKC.R46YP320.Simulator
{
    public class EQPortSimulator
    {
        private readonly EQPortSignal _signal;
        private readonly int _eqNo;

        public EQPortSimulator(EQPortSignal signal, int eqNo)
        {
            _signal = signal;
            _eqNo = eqNo;
        }

        public void Initial()
        {
            _signal.POnline.SetOn();
        }

        public void SetPriorityUpdateOn()
        {
            _signal.PriorityUp.SetOn();
        }

        public void SetPriorityUpdateOff()
        {
            _signal.PriorityUp.SetOff();
        }

        public void SetLoadRequest()
        {
            _signal.L_REQ.SetOn();
            _signal.U_REQ.SetOff();
        }

        public void SetUnloadRequest()
        {
            _signal.U_REQ.SetOn();
            _signal.L_REQ.SetOff();
        }

        public void SetNoRequest()
        {
            _signal.L_REQ.SetOff();
            _signal.U_REQ.SetOff();
        }

        public void SetOnline()
        {
            _signal.POnline.SetOn();
        }

        public void SetOffline()
        {
            _signal.POnline.SetOff();
        }

        public void SetPresentOn()
        {
            _signal.Carrier.SetOn();
        }

        public void SetPresentOff()
        {
            _signal.Carrier.SetOff();
        }

        public void SetCSTID(string cstid)
        {
            _signal.CarrierId.SetData(cstid.ToIntArray(20));
        }
    }
}
