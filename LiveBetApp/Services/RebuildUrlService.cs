using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveBetApp.Common;
using System.Web;
using System.Net;

namespace LiveBetApp.Services
{
    public class RebuildUrlService
    {
        private static Random random = new Random();
        private string _gid = "";
        private string _token = "";
        private string _id = "";
        private string _rid = "";


        public string Token { get { return _token; } }
        public string Gid { get { return _gid; } }
        public string Id { get { return _id; } }
        public string Rid { get { return _rid; } }

        public string GetWebSocketUrl()
        {

            string getSidUrl = Common.Config.GetConfigModel().getSidUrl;
            Uri SocketIoUrl = new Uri(getSidUrl);

            this._gid = GetRandomHexNumber(16);
            this._token = HttpUtility.ParseQueryString(SocketIoUrl.Query).Get("token");
            this._id = HttpUtility.ParseQueryString(SocketIoUrl.Query).Get("id");
            this._rid = HttpUtility.ParseQueryString(SocketIoUrl.Query).Get("rid");

            string gid = _gid;
            string token = this._token;
            string id = this._id;
            string rid = HttpUtility.ParseQueryString(SocketIoUrl.Query).Get("rid");
            string EIO = HttpUtility.ParseQueryString(SocketIoUrl.Query).Get("EIO");
            string transport = "websocket";
            //string sid = GetSidFromResponse(getSidUrl);
            string webSocketPrefix = getSidUrl.Substring(2, 1) == "s" ? "wss://" : "ws://";
            if (getSidUrl.Substring(2, 1) == "s")
            {
                DataStore.IsWss = true;
            }
            else
            {
                DataStore.IsWss = false;
            }
            return webSocketPrefix + SocketIoUrl.Host + SocketIoUrl.AbsolutePath // khi get getSidUrl là https => wss; còn là http => ws
                + "?gid=" + gid
                + "&token=" + token
                + "&id=" + id
                + "&rid=" + rid
                + "&EIO=" + EIO
                + "&transport=" + transport
                //+ "&sid=" + sid;
            ;
        }

        private string GetSidFromResponse(string url)
        {
            string result = "";
            using (var wb = new WebClient())
            {
                string response = wb.DownloadString(url);

                int start = response.IndexOf("\"sid\":") + 7;
                int end = response.IndexOf(",\"upgrades\"") - 1;

                result = response.Substring(start, end - start);
            }
            return result;
        }

        public string GetSboDataUrlToday(int v)
        {
            string sboUrl = Common.Config.GetConfigModelSbo().sboUrl;
            return sboUrl.Replace(".com/", ".com")
                +   "/web-root/restricted/odds-display/today-data.aspx?od-param=1,1,1,1,1,2,1,2,0,1&fi=1&dl=0&v="     // today
                + v;
        }

        public string GetSboDataUrlLive(int v)
        {
            string sboUrl = Common.Config.GetConfigModelSbo().sboUrl;
            return sboUrl.Replace(".com/", ".com")
                + "/web-root/restricted/odds-display/live-data.aspx?od-param=1,1,3,1,1,2,1,2,0,1&v="              // live
                + v;
        }


        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result.ToLower();
            return result.ToLower() + random.Next(16).ToString("X").ToLower();
        }
    }
}


//private readonly string _gid;
//private readonly string _token;
//private readonly string _id;
//private readonly string _rid;
//private readonly string _EIO;
//private readonly string _transport;
//private readonly string _t;
//private string _sid;

//https://agnj3.5566688.com/socket.io/?
//gid=61d4a659e88a150f
//&token=f2dd68f8-b206-4ac5-b6c9-502bf3c91a59
//&id=36562670
//&rid=2
//&EIO=3
//&transport=polling
//&t=MiWPwjJ

//https://agnj3.5566688.com/socket.io/?
//gid=61d4a659e88a150f
//&token=f2dd68f8-b206-4ac5-b6c9-502bf3c91a59
//&id=36562670
//&rid=2
//&EIO=3
//&transport=polling
//&t=MiWPwlw
//&sid=d2Kb3o94RBCbaHVbAHBN

//wss://agnj3.5566688.com/socket.io/
//?gid=61d4a659e88a150f
//&token=f2dd68f8-b206-4ac5-b6c9-502bf3c91a59
//&id=36562670
//&rid=2
//&EIO=3
//&transport=websocket
//&sid=d2Kb3o94RBCbaHVbAHBN

//https://agnj3.5566688.com/socket.io/
//?gid=61d4a659e88a150f
//&token=f2dd68f8-b206-4ac5-b6c9-502bf3c91a59
//&id=36562670
//&rid=2
//&EIO=3
//&transport=polling
//&t=MiWPwoQ
//&sid=d2Kb3o94RBCbaHVbAHBN

//https://agnj3.5566688.com/socket.io/
//?gid=61d4a659e88a150f
//&token=f2dd68f8-b206-4ac5-b6c9-502bf3c91a59
//&id=36562670
//&rid=2
//&EIO=3
//&transport=polling
//&t=MiWPwoS
//&sid=d2Kb3o94RBCbaHVbAHBN
