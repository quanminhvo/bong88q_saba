using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.DataModels
{
    public class Match
    {
        public Match()
        {
            this.Bets = new List<Bet>();
        }

        public string TeamHost { get; set; }
        public string TeamGuest { get; set; }
        public string TeamStronger { get; set; }

        public List<Bet> Bets { get; set; }
    }
}
