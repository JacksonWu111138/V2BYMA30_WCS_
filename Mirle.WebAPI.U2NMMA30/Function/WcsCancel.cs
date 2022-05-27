using Mirle.Def;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.Function
{
    public class WcsCancel
    {
        private WebApiConfig _config = new WebApiConfig();
        public WcsCancel(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(WcsCancelInfo info)
        {
            try
            {
                string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strJson);
                string sLink = $"http://{_config.IP}/WCS_CANCEL";
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, re);

                return true;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }
    }
}
