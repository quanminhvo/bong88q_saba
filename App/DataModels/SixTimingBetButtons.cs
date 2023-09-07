using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModels
{
    public class SixTimingBetButtons
    {
        public SixTimingBetButtons()
        {
            FirstTimingBetButton = new BetButtonOfMatch();
            SecondTimingBetButton = new BetButtonOfMatch();
            ThirdTimingBetButton = new BetButtonOfMatch();
            FourthTimingBetButton = new BetButtonOfMatch();
            FifthTimingBetButton = new BetButtonOfMatch();
            SixthTimingBetButton = new BetButtonOfMatch();
        }

        public BetButtonOfMatch FirstTimingBetButton { get; set; }
        public BetButtonOfMatch SecondTimingBetButton { get; set; }
        public BetButtonOfMatch ThirdTimingBetButton { get; set; }
        public BetButtonOfMatch FourthTimingBetButton { get; set; }
        public BetButtonOfMatch FifthTimingBetButton { get; set; }
        public BetButtonOfMatch SixthTimingBetButton { get; set; }

    }
}
