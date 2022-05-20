using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirle.Def;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Newtonsoft.Json;

namespace Mirle.WebAPI.U2NMMA30.Function
{
    public class EmptyShelfQuery
    {
        private WebApiConfig _config = new WebApiConfig();

        public EmptyShelfQuery(WebApiConfig Config)
        {
            _config = Config;
        }       
        public bool funReport(EmptyShelfQuery_WCS info, ref EmptyShelfQuery_WMS response)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriTraceLog_CV(strJson);
                string sLink = $"http://{_config.IP}/EMPTY_SHELF_QUERY";
                clsWriLog.Log.FunWriTraceLog_CV($"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriTraceLog_CV(re);
                response = (EmptyShelfQuery_WMS)Newtonsoft.Json.Linq.JObject.Parse(re).ToObject(typeof(EmptyShelfQuery_WMS));

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
