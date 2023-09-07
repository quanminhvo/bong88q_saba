using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SboBetSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeBet();
        }


        static string GetData()
        {
            try
            {

                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=0
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=1&v=0&dl=0
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=1&v=3737&dl=0
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=1&v=4011&dl=0
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=4050
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=4115
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=1&v=4059&dl=0
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=4430
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=4886
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=4998
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=1&v=4565&dl=0
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=5077
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=5097
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=5136
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=1&v=5085&dl=0
                
                // get live match
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=0
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=5521
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=5602

                // get today
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=1&v=5476&dl=0
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=1&v=5602&dl=0

                // unknown
                // http://i0697tq67284.asia.camquit.com/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=0&v=5536

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://i0697tq67284.asia.com3456.com/web-root/restricted/odds-display/live-data.aspx?od-param=1,1,3,1,1,2,1,2,0,1&v=0");

                //http://i0697tq67284.asia.com3456.com/web-root/restricted/odds-display/live-data.aspx?od-param=1,1,3,1,1,2,1,2,0,1&v=0
                //http://i0697tq67284.asia.com3456.com/web-root/restricted/odds-display/live-data.aspx?od-param=1,1,3,1,1,2,1,2,0,1&v=11910

                request.Method = "GET";
                request.Headers["Cookie"] = "_ga=GA1.2.482631289.1578557879; _hjid=57ce3977-9793-40a5-acf4-f672ded43553; PSID=hnIWFozhsC8UQ9VMP1W5Mw==; AWSUSER_ID=awsuser_id1578558085294r9339; _ga=GA1.4.482631289.1578557879; D_IID=04B1F20A-6B14-3411-82D5-D76E275EBA40; D_UID=E61E2EF7-23F1-366B-A41A-05394F9394BF; D_ZID=FE411A9D-BFA1-38FB-9B96-6176A676CE59; D_ZUID=7D58AE5C-BC5B-3793-AD7A-40D931223054; D_HID=B9E80274-AD6D-36D6-9F59-1AF884578025; D_SID=115.78.100.116:G4Wo31ssxQQZegWjQU6u/UZz/e7gzJNv23XSOle/FFU; G_DN=1vy0356q1smw; _gid=GA1.2.1336058796.1578904233; nkei83=!FQArcJP+K7aM+QaVsshbG50NQwwCyXavtXzu9aq/FmkW28BOnV1OpFxxL83SUyETGIGOHClxEWHS2Q==; lang=vi-vn; dtil99=!1soWj38TmSkZWwmVsshbG50NQwwCyefNHUbNlxL2pLge+DjsAwqD6qCpO77P2rz7lMVZ0J7D7HevWA==; _gid=GA1.4.1336058796.1578904233; ASP.NET_SessionId=22iuyfm2lhuhmre2lajqm2sp; AWSSESSION_ID=awssession_id1578965934889r7514; _za=Z/zYMbYGK+83L9nxyhIgjA==; states=:1:1:::::::::12:1:1:2898331:1578965955933:1578965955942:1578965955945:1578965957037:1578965955943; _gat=1";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36";

                var response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();

                responseString = responseString.Substring(30);
                responseString = responseString.Substring(0,responseString.Length - 2);
                responseString = responseString.Replace("\\u200C", "");

                JToken jToken = JToken.Parse(responseString);

                List<float> test = jToken[2][5][0][2].ToObject<List<float>>();

                return responseString;
            }
            catch
            {
                return "";
            }

        }

        static void MakeBet()
        {
            // loginname actualy is session id
            string url = "http://i0697tq67284.asia.com3456.com/web-root/restricted/ticket/confirm.aspx?loginname=e214d771e18c939baaf31050ce0471a8&sameticket=0&betcount=1&stake=1&ostyle=1&stakeInAuto=1&betpage=12&acceptIfAny=0&autoProcess=0&autoRefresh=1&voucherId=0&oid=46419503&js=1&timeDiff=6988";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers["Cookie"] = "_ga=GA1.2.482631289.1578557879; _hjid=57ce3977-9793-40a5-acf4-f672ded43553; PSID=hnIWFozhsC8UQ9VMP1W5Mw==; AWSUSER_ID=awsuser_id1578558085294r9339; _ga=GA1.4.482631289.1578557879; D_IID=04B1F20A-6B14-3411-82D5-D76E275EBA40; D_UID=E61E2EF7-23F1-366B-A41A-05394F9394BF; D_ZID=FE411A9D-BFA1-38FB-9B96-6176A676CE59; D_ZUID=7D58AE5C-BC5B-3793-AD7A-40D931223054; D_HID=B9E80274-AD6D-36D6-9F59-1AF884578025; D_SID=115.78.100.116:G4Wo31ssxQQZegWjQU6u/UZz/e7gzJNv23XSOle/FFU; G_DN=1vy0356q1smw; _gid=GA1.2.784336723.1579485805; _gid=GA1.4.784336723.1579485805; nkei83=!HWViL2Q3fiaHC6yVsshbG50NQwwCyWKBNKecrndFMDry9u5lPvY9I/7z9qXwOy/d/7NFqQbWcZ2yfg==; dtil99=!vYBlJo6PJW1Uod+VsshbG50NQwwCydQNgrUu1PYoGicEMNA95GxaeYsqHq6rRQKCy9MahXZeq78DgA==; AWSSESSION_ID=awssession_id1579590014741r3018; lv_prod=sports-euro; lang=en; pip=2; _gat=1; _dc_gtm_UA-71527796-13=1; _dc_gtm_UA-71527796-14=1; _gat_UA-117575056-5UA-142496905-1=1; _gat_UA-142497672-3=1; ASP.NET_SessionId=ifn0cjpckscrv45wjygwqndx; _za=Z/zYMbYGK+83L9nxyhIgjA==; _gat_UA-126137788-7=1; _gat_UA-143326782-2=1; _dc_gtm_UA-71527796-8=1; _gat_UA-139424303-7=1; _gat_UA-117575056-5=1; _gat_UA-136824553-2=1; _gat_UA-126137788-6=1; states=:1:1:::::::::::::1579597413096:1579597413099:1579597413100:1579590036868:1579597413100:";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36";

            var response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();
        
        }
    }
}
