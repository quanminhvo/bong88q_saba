using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetApp.Models.ReadFromWeb
{
    public class Match
    {
        public string Home { get; set; }
        public string Away { get; set; }
        public string TeamStronger { get; set; }
        public Bet BetArea { get; set; }

        public Match()
        {
            this.BetArea = new Bet();
        }

    }

    public class Bet
    {
        public string Score { get; set; }
        public string TimePlaying { get; set; }
        public BetRow FirstBetRow { get; set; }
        public BetRow SecondBetRow { get; set; }
        public BetRow ThirdBetRow { get; set; }

    }

    public class BetRow
    {
        public BetCell FthdcBetCell { get; set; } // Fthdc -> full time handicap
        public BetCell FtouBetCell { get; set; } // Ftou -> full time over under
        public BetCell HthdcBetCell { get; set; } // Hthdc -> half time handicap
        public BetCell HtouBetCell { get; set; } // Htou -> half time over under

    }

    public class BetCell
    {
        public string EstimatedGoal { get; set; }

        public float HomeRate { get; set; }
        public IWebElement HomeButton { get; set; }
        public bool IsHomeButtonStable { get; set; }

        public float AwayRate { get; set; }
        public IWebElement AwayButton { get; set; }
        public bool IsAwayButtonStable { get; set; }

        public string Score { get; set; }
        public string TimePlaying { get; set; }
        public string TeamStronger { get; set; }

    }
}
