using App.DataModels;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.LogicServices
{
    public static class BetService
    {
        public static bool IsBetHistoryChange(Bet betA, Bet betB)
        {
            if (betA.FullTimeHandicap_GuestRate != betB.FullTimeHandicap_GuestRate) return true;
            if (betA.FullTimeHandicap_HostRate != betB.FullTimeHandicap_HostRate) return true;
            if (betA.FullTimeHandicap != betB.FullTimeHandicap) return true;

            if (betA.FullTimeOverUnder_GuestRate != betB.FullTimeOverUnder_GuestRate) return true;
            if (betA.FullTimeOverUnder_HostRate != betB.FullTimeOverUnder_HostRate) return true;
            if (betA.FullTimeOverUnder != betB.FullTimeOverUnder) return true;

            if (betA.Score != betB.Score) return true;

            return false;
        }
        
        public static void PlaceBetAndSubmit(IWebElement betButton, int amount)
        {

        }

    }
}
