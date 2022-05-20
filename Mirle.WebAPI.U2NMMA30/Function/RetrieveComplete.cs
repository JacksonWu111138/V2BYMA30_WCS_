using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Mirle.Def;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Newtonsoft.Json;

namespace Mirle.WebAPI.U2NMMA30.Function
{
    public class RetrieveComplete
    {
        private WebApiConfig _config = new WebApiConfig();
        public RetrieveComplete(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(RetrieveCompleteInfo info)
        {
            try
            {
                string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriTraceLog_CV(strJson);
                string sLink = $"http://{_config.IP}/RETRIEVE_COMPLETE";
                clsWriLog.Log.FunWriTraceLog_CV($"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriTraceLog_CV(re);
                var info_wms = (RetrieveCompleteReturnInfo)Newtonsoft.Json.Linq.JObject.Parse(re).ToObject(typeof(RetrieveCompleteReturnInfo));

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
