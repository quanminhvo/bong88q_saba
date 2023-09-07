using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class OverUnderScoreTimesV2Item
    {
        public long OddsId { get; set; }
        public int Over { get; set; }
        public int Under { get; set; }
    }
}
