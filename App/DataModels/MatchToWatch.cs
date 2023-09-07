using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModels
{
    public class MatchToWatch
    {
        public MatchToWatch()
        {
            BetChangeHistory = new List<Bet>();
            TimmingBetButton = new BetButtonOfMatch();
            TimmingBetButtons = new SixTimingBetButtons();
        }
        public string MatchID { get { return TeamHost + " " + TeamGuest; } }
        public string TeamHost { get; set; }
        public string TeamGuest { get; set; }
        public string TeamStronger { get; set; }
        public BetButtonOfMatch TimmingBetButton { get; set; }
        public SixTimingBetButtons TimmingBetButtons { get; set; }

        public List<Bet> BetChangeHistory { get; set; }
    }
}
