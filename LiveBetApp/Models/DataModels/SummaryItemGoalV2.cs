using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class SummaryItemGoalV2
    {
        public int GoalHdp { get; set; }
        public int HdpWithoutGoal { get; set; }
        public int MaxHdpBeforeNextGoal { get; set; }
        public int NewMaxHdp { get; set; }
        public int NewMaxRealHdp { get; set; }
        public int NewMinRealHdp { get; set; }
    }
}
