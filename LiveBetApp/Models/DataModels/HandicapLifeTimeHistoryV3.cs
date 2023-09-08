using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class HandicapLifeTimeHistoryV3
    {
        public long OddsId { get; set; }

        public float Odds1a { get; set; }
        public float Odds2a { get; set; }

        public float Hdp1 { get; set; }
        public float Hdp2 { get; set; }

        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public int Hdp_100
        {
            get
            {
                if (Hdp1 > 0)
                    return (int)(Hdp1 * 100);
                else
                    return (int)(Hdp2 * 100);
            }
        }

        public TimeSpan TimeSpanFromStart { get; set; }
        public string TimeSpanFromStartStr
        {
            get
            {
                return TimeSpanFromStart.TotalMinutes.ToString("N0");
            }
        }
    }

}
