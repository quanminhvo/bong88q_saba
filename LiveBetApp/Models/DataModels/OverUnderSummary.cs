using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class OverUnderSummary
    {
        public int Hdp { get; set; }
        public OverUnderScoreTimesV2Item VeryFirstPrice { get; set; }
        public OverUnderScoreTimesV2Item At90BeforeLive { get; set; }
        public OverUnderScoreTimesV2Item BeforeLivePrice { get; set; }
        public OverUnderScoreTimesV2Item FirstPriceInLive { get; set; }
    }
}
