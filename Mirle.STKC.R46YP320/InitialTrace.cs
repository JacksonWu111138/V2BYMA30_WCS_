using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.STKC.R46YP320
{
    public class InitialTrace
    {
        private frmInitialTrace _frmTrace;

        public void Show()
        {
            _frmTrace = new frmInitialTrace();
            //_frmTrace.TopMost = true;
            _frmTrace.Show();
        }

        public void Close()
        {
            _frmTrace?.Close();
        }

        public void Trace(string msg)
        {
            _frmTrace?.Trace(msg);
        }
    }
}
