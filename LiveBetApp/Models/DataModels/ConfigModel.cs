using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class ConfigModel
    {
        public string getSidUrl { get; set; }

        public string bong88Url { get; set; }
        public string cookie { get; set; }
        public string betting { get; set; }
        public string run_alert { get; set; }

        public string refererUrl 
        { 
            get 
            {
                return bong88Url + "/Sports/";
            } 
        }

        public string processBetUrl
        {
            get
            {
                return bong88Url + "/Betting/ProcessBet";
            }
        }

        public string getTicketUrl
        {
            get
            {
                return bong88Url + "/Betting/GetTickets";
            }
        }

        public string getHasStreaming
        {
            get
            {
                return bong88Url + "/Streaming/LiveList?GMT=7";
            }
        }
    }
}
