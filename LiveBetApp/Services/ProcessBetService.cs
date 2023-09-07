using LiveBetApp.Models.DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LiveBetApp.Services
{
    public class ProcessBetService
    {
        public void Execute()
        {

        }

        public string DoBet(ProcessBetModel betModel, string cookie, string url)
        {
            try
            {
                betModel.ErrorCode = "15";
                string postData = BuildPostStringData(betModel);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                byte[] datae = Encoding.UTF8.GetBytes(postData);



                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentLength = datae.Length;
                request.Headers["Cookie"] = cookie;
                string username = GetCookieValue(cookie, "_UserName");
                if (string.IsNullOrEmpty(username))
                {
                    username = GetCookieValue(cookie, "LoginName");
                }
                request.Headers["username"] = username.ToUpper();
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                //request.TransferEncoding = "chunked";
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(datae, 0, datae.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString.ToString();
            }
            catch(Exception ex)
            {
                return "";
            }

        }

        public JToken GetTicket(ProcessBetModel betModel, string cookie, string url)
        {
            try
            {
                string postData = BuildPostStringData(betModel);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                byte[] datae = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentLength = datae.Length;
                request.Headers["Cookie"] = cookie;

                string username = GetCookieValue(cookie, "_UserName");
                if (string.IsNullOrEmpty(username))
                {
                    username = GetCookieValue(cookie, "LoginName");
                }
                request.Headers["username"] = username.ToUpper();
                request.Headers["uid"] = username.ToUpper();
                request.Headers["origin"] = DataStore.Config.bong88Url;
                //request.Headers["referer"] = DataStore.Config.refererUrl;
                request.Referer = DataStore.Config.refererUrl;
                //if (request.Headers.GetValues("referer") != null)
                //{
                //    request.Headers["referer"] = DataStore.Config.refererUrl;
                //}
                //else
                //{
                //    request.Headers.Add("referer", DataStore.Config.refererUrl);
                //}
                

                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36";

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(datae, 0, datae.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JToken.Parse(responseString.ToString());
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private string BuildPostStringData(ProcessBetModel betModel)
        {
            List<System.Reflection.PropertyInfo> properties = typeof(LiveBetApp.Models.DataModels.ProcessBetModel).GetProperties().ToList();
            string result = "";

            for (int i = 0; i < properties.Count; i++)
            {
                result += ("ItemList[0][" + properties[i].Name + "]") + "=" + (properties[i].GetValue(betModel).ToString());
                result += ("&");
            }

            result += "ItemList[0][PhoneBettingSetting][IsGraphButton]=false";
            result += "&ItemList[0][PhoneBettingSetting][GraphRemark]=";
            result += "&ItemList[0][PhoneBettingSetting][AdminID]=0";
            result += "&ItemList[0][PhoneBettingSetting][HideTicket]=false";
            result += "&ItemList[0][PhoneBettingSetting][Using1X2AsiaHdp]=false";
            result += "&ItemList[0][PhoneBettingSetting][Using1X2Hdp]=false";
            result += "&ItemList[0][PhoneBettingSetting][IsKeyInDeadBallLiveScore]=true";
            result += "&ItemList[0][MMR]=";
            result += "&ItemList[0][parentMatchId]=0";

            return result;
        }

        private string GetCookieValue(string cookieString, string cookieName)
        {
            List<string> cookies = cookieString.Split(';').ToList();
            for (int i = 0; i < cookies.Count; i++)
            {
                if (cookies[i].Contains(cookieName))
                {
                    int startIndex = cookies[i].IndexOf(cookieName) + cookieName.Length + 1;
                    return cookies[i].Substring(startIndex);
                }
            }
            return "";
        }
    }
}
