using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Mirle.Def;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Mirle.WebAPI.U2NMMA30.Function
{
    public class ShelfComplete
    {
        private WebApiConfig _config = new WebApiConfig();
        public ShelfComplete(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(ShelfCompleteInfo info)
        {
            try
            {
                string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strJson);
                string sLink = $"http://{_config.IP}/SHELF_COMPLETE";
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Debug, $"URL: {sLink}");
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
