using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320
{
    public interface ISTKCHost
    {
        Form GetMainView();

        void AppClosing();

        event EventHandler OnMainViewShowRequest;
    }
}
