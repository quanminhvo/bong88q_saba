using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.ViewModels
{
    public class DecoyProduct
    {
        public string Leauge { get; set; }
        public string Home { get; set; }
        public int LiveHomeScore { get; set; }
        public int LiveAwayScore { get; set; }
        public string Away { get; set; }
        public int LivePeriod { get; set; }
        public bool IsHt { get; set; }
        public DateTime GlobalShowtime { get; set; }
        public DateTime FirstShow { get; set; }

        public LiveBetApp.Common.Enums.BetType ProductBetType { get; set; }
        public int Minute { get; set; }
        public DateTime TimeAddProduct { get; set; }

        public string HdpS { get; set; }
        public int Hdp { get; set; }
        public int Odds1a100 { get; set; }
        public int Odds2a100 { get; set; } // OP

    }
}
