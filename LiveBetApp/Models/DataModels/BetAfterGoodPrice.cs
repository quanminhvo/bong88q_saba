using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class BetAfterGoodPrice : BetByHdpPrice
    {
        public BetAfterGoodPrice()
        {

        }
        public int MinuteAfterGoodPrice { get; set; }
        public int FirstMinuteHasGoodPrice { get; set; }
    }
}
