using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Mirle.Def;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Newtonsoft.Json;

namespace Mirle.WebAPI.U2NMMA30.Function
{
    public class PutawayCheck
    {
        private WebApiConfig _config = new WebApiConfig();
        public PutawayCheck(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(PutawayCheckInfo info)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strJson);
                string sLink = $"http://{_config.IP}/PUTAWAY_CHECK";
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Debug, $"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, re);
                var info_wms = (ReturnPutawayCheckInfo)Newtonsoft.Json.Linq.JObject.Parse(re).ToObject(typeof(ReturnPutawayCheckInfo));

                if (info.checkOnly == clsEnum.WmsApi.IsComplete.Y.ToString())
                {
                    if (info_wms.returnCode == clsConstValue.ApiReturnCode.Success) return true;
                    else return false;
                }
                else return true;
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
