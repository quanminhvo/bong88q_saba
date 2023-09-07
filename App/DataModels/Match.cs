using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModels
{
    public class Match
    {
        public Match()
        {
            this.Bets = new List<Bet>();
            this.TimmingBetButton = new BetButtonOfMatch();
            this.TimmingBetButtons = new SixTimingBetButtons();
        }

        public string TeamHost { get; set; }
        public string TeamGuest { get; set; }
        public string TeamStronger { get; set; }
        public BetButtonOfMatch TimmingBetButton { get; set; }
        public SixTimingBetButtons TimmingBetButtons { get; set; }

        public List<Bet> Bets { get; set; }
    }
}
