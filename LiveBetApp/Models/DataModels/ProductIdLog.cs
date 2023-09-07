using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class ProductIdLog
    {
        public long OddsId { get; set; }
        public string HdpS { get; set; }
        public int Hdp { get; set; }
        public int Odds1a100 { get; set; }
        public int Odds2a100 { get; set; }
        public LiveBetApp.Common.Enums.BetType ProductBetType { get;set; }
        public int Minute { get; set; }
        public int LivePeriod { get; set; }
        public string STT { get; set; }
        public DateTime CreateDatetime { get; set; }

        public string CD
        { 
            get 
            {
                return CreateDatetime.ToString("HH:mm");
            } 
        }
    }
}
