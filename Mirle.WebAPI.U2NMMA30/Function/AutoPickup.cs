using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Mirle.Def;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Newtonsoft.Json;

namespace Mirle.WebAPI.U2NMMA30.Function
{
    public class AutoPickup
    {
        private WebApiConfig _config = new WebApiConfig();
        public AutoPickup(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(AutoPickupInfo info)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, strJson);
                string sLink = $"http://{_config.IP}/AUTO_PICKUP";
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, re);

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
