using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModels
{
    public class MatchWithLastestBet
    {
        public string TeamHost { get; set; }
        public string TeamGuest { get; set; }
        public string TeamStronger { get; set; }

        public string Score { get; set; }
        public string TimePlaying { get; set; }
        public DateTime RecordedDateTime { get; set; }
        public TimeSpan TimeSpanFromLastChanges { get; set; }

        public string FullTimeHandicap { get; set; }
        public float FullTimeHandicap_HostRate { get; set; }
        public float FullTimeHandicap_GuestRate { get; set; }

        public string FullTimeOverUnder { get; set; }
        public float FullTimeOverUnder_HostRate { get; set; }
        public float FullTimeOverUnder_GuestRate { get; set; }
    }
}
