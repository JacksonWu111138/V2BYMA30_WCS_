using System;
using System.Net.Http;
using Newtonsoft.Json;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Mirle.Def;
using System.Net.Http.Headers;
using System.Text;

namespace Mirle.WebAPI.U2NMMA30.Function
{
    public class PositionReport
    {
        private WebApiConfig _config = new WebApiConfig();
        public PositionReport(WebApiConfig Config)
        {
            _config = Config;
        }

        public bool FunReport(PositionReportInfo info)
        {
            try
            {
                string strJson = JsonConvert.SerializeObject(info);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strJson);
                string sLink = $"http://{_config.IP}/POSITION_REPORT";
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Debug, $"URL: {sLink}");
                string re = clsTool.HttpPost(sLink, strJson);
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, re);
                //HttpResponseMessage response = null;
                //using (HttpClient client = new HttpClient())
                //{
                //    using (var request = new HttpRequestMessage(HttpMethod.Post, sLink))
                //    {
                //        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //        //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", System.Web.HttpContext.Current.Session["WebApiAccessToken"].ToString());
                //        var data = new StringContent(strJson, Encoding.UTF8, "application/json");
                //        request.Content = data;
                //        response = client.SendAsync(request).Result;
                //        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, response.Content.ReadAsStringAsync().Result);
                //    }

                //    //var result2 = client.PostAsJsonAsync(sLink, info).Result;
                //}

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
