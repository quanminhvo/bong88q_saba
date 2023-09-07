using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class OverUnderScoreTimesV3Item : OverUnderScoreTimesV2Item
    {
        public DateTime RecordedDatetime { get; set; }
    }
}
