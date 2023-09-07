using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.DataModels
{
    public class MatchToWatch
    {
        public MatchToWatch()
        {
            BetChangeHistory = new List<Bet>();
        }
        public string TeamHost { get; set; }
        public string TeamGuest { get; set; }

        public List<Bet> BetChangeHistory { get; set; }
    }
}
