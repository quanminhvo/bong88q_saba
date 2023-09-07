using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModels
{
    public class TimmingBet
    {

        public TimmingBet()
        {
            IsClicked = false;
        }

        public bool IsGoodToGo { get { return (TimeToBet != null && TimeToBet.Length > 1); } }

        public IWebElement Button { get; set; }
        public int BetAmount { get; set; }
        public string TimeToBet { get; set; }
        public bool IsClicked { get; set; }


    }
}
