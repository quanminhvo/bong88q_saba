using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class MatchIuooAlertItem
    {
        public int OUFT_Hdp { get; set; }
        public int OUFT_Price { get; set; }

        public int OUFH_Hdp { get; set; }
        public int OUFH_Price { get; set; }

        public int Handicap_Hdp { get; set; }
        public int Handicap_Price { get; set; }
    }
}
