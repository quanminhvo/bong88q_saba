using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class ProductHistory
    {
        public float Odds1a_100 { get; set; }
        public float Odds2a_100 { get; set; }
        public float Hdp1 { get; set; }
        public float Hdp2 { get; set; }

        public int LiveHomeScore { get; set; }
        public int LiveAwayScore { get; set; }
        public TimeSpan TimeSpanFromStart { get; set; }
    }
}
