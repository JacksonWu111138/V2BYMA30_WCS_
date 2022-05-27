using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Mirle.Def;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Mirle.WebAPI.U2NMMA30.Function
{
    public class PickupQuery
    {
        private WebApiConfig _config = new WebApiConfig();
        public PickupQuery(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(PickupQuery_WCS info, ref PickupQuery_WMS info_wms)
        {
            try
            { 
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, strJson);
                string sLink = $"http://{_config.IP}/PICKUP_QUERY";
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, re);
                info_wms = (PickupQuery_WMS)Newtonsoft.Json.Linq.JObject.Parse(re).ToObject(typeof(PickupQuery_WMS));

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
