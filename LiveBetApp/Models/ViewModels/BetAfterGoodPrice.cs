using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.ViewModels
{
    public class BetAfterGoodPrice : LiveBetApp.Models.DataModels.BetAfterGoodPrice
    {
        public string Home { get; set; }
        public string Away { get; set; }
    }
}
