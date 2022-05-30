using Mirle.Def;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Newtonsoft.Json;
using System;

namespace Mirle.WebAPI.U2NMMA30.Function
{
    public class EQPStatusUpdate
    {
        private WebApiConfig _config = new WebApiConfig();
        public EQPStatusUpdate(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(EQPStatusUpdateInfo info)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strJson);
                string sLink = $"http://{_config.IP}/EQP_STATUS_UPDATE";
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
