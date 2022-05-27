using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Mirle.Def;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Mirle.WebAPI.U2NMMA30.Function
{
    public class ShelfReport
    {
        private WebApiConfig _config = new WebApiConfig();
        public ShelfReport(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(ShelfReportInfo info)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, strJson);
                string sLink = $"http://{_config.IP}/SHELF_REPORT";
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, re);
                var info_wms = (ShelfReportReturnInfo)Newtonsoft.Json.Linq.JObject.Parse(re).ToObject(typeof(ShelfReportReturnInfo));

                if (info_wms.returnCode == clsConstValue.ApiReturnCode.Success) return true;
                else return false;
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
