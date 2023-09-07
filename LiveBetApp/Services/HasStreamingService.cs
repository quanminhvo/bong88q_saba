using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services
{
    public class HasStreamingService
    {
        private void ExecuteCore(int milisecond)
        {
            if (DataStore.Config == null) return;
            if (DataStore.Config.cookie == null || DataStore.Config.cookie.Length == 0) return;
            if (DataStore.Config.bong88Url == null || DataStore.Config.bong88Url.Length == 0) return;

            while (true)
            {
                try
                {
                    JToken data = GetStreamingData(DataStore.Config.cookie, DataStore.Config.getHasStreaming);
                    if (data != null)
                    {
                        if (data["Data"] != null)
                        {
                            JToken dataSoccer = data["Data"].Where(item => item["SportID"].ToString() == "1").FirstOrDefault();
                            if (dataSoccer != null && dataSoccer["matchs"] != null)
                            {
                                //DataStore.Matchs.Values.Where(item => item.LivePeriod >= 0).ToList().ForEach(c => c.HasStreaming = false);

                                for (int i = 0; i < dataSoccer["matchs"].Count(); i++)
                                {
                                    if (dataSoccer["matchs"][i]["Matchid"] != null)
                                    {
                                        long matchId = ((long)dataSoccer["matchs"][i]["Matchid"]);
                                        if (DataStore.Matchs.ContainsKey(matchId))
                                        {
                                            DataStore.Matchs[matchId].HasStreaming = true;
                                            if (DataStore.Matchs[matchId].TimeHasStreaming == DateTime.MinValue)
                                            {
                                                DataStore.Matchs[matchId].TimeHasStreaming = DateTime.Now;
                                            }

                                            if (DataStore.Matchs[matchId].FirstTimeHasStreaming == DateTime.MinValue)
                                            {
                                                DataStore.Matchs[matchId].FirstTimeHasStreaming = DateTime.Now;
                                            }
                                        }

                                    }
                                }


                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Thread.Sleep(milisecond);
                }

            }
        }

        public void Execute(int milisecond)
        {
            Thread coreServiceThread;
            coreServiceThread = new Thread(() =>
            {
                ExecuteCore(milisecond);
            });
            coreServiceThread.IsBackground = true;
            coreServiceThread.Start();
        }


        public JToken GetStreamingData(string cookie, string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentLength = 0;
                request.Headers["Cookie"] = cookie;

                string username = GetCookieValue(cookie, "_UserName");
                if (string.IsNullOrEmpty(username))
                {
                    username = GetCookieValue(cookie, "LoginName");
                }
                request.Headers["username"] = username.ToUpper();
                //request.Headers["uid"] = username.ToUpper();

                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";

                //using (var stream = request.GetRequestStream())
                //{
                //    stream.Write(datae, 0, datae.Length);
                //}

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JToken.Parse(responseString.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
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
