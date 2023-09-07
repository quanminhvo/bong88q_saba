using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModels
{
    public class BetButtonOfMatch
    {
        public BetButtonOfMatch()
        {
            FullTimeHandicap_TeamHost = new TimmingBet();
            FullTimeHandicap_TeamGuest = new TimmingBet();
            FullTimeOverUnder_TeamHost = new TimmingBet();
            FullTimeOverUnder_TeamGuest = new TimmingBet();
        }
        public TimmingBet FullTimeHandicap_TeamHost { get; set; }
        public TimmingBet FullTimeHandicap_TeamGuest { get; set; }

        public TimmingBet FullTimeOverUnder_TeamHost { get; set; }
        public TimmingBet FullTimeOverUnder_TeamGuest { get; set; }

    }
}
