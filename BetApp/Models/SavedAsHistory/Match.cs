using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetApp.Models.SavedAsHistory
{
    public class Match
    {
        public string Home { get; set; }
        public string Away { get; set; }
        public Bet BetArea { get; set; }

        public string Score { get; set; }
        public string TimePlaying { get; set; }


        public Match()
        {
            BetArea = new Bet();
        }
    }

    public class Bet
    {
        public BetRow FirstBetRow { get; set; }
        public BetRow SecondBetRow { get; set; }
        public BetRow ThirdBetRow { get; set; }

    }

    public class BetRow
    {
        public BetRow()
        {
            FthdcBetCell = new BetCellHistory();
            FtouBetCell= new BetCellHistory();
            HthdcBetCell = new BetCellHistory();
            HtouBetCell = new BetCellHistory();
        }

        public BetCellHistory FthdcBetCell { get; set; } // Fthdc -> full time handicap
        public BetCellHistory FtouBetCell { get; set; } // Ftou -> full time over under
        public BetCellHistory HthdcBetCell { get; set; } // Hthdc -> half time handicap
        public BetCellHistory HtouBetCell { get; set; } // Htou -> half time over under

    }

    public class BetCellHistory
    {

        public BetCellHistory()
        {
            History = new List<BetCell>();
        }

        public List<BetCell> History { get; set; }
        
        public IWebElement HomeButton { get; set; }
        public bool IsHomeButtonStable { get; set; }

        public IWebElement AwayButton { get; set; }
        public bool IsAwayButtonStable { get; set; }

    }

    public class BetCell
    {
        public long TimeSpanFromStart { get; set; }
        public DateTime DateTimeRecored { get; set; }
        public string Score { get; set; }
        public string TimePlaying { get; set; }

        public string TeamStronger { get; set; }
        public string EstimatedGoal { get; set; }
        public float HomeRate { get; set; }
        public float AwayRate { get; set; }

    }
}
