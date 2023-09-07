using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class BetDirect
    {
        public Guid Id { get; set; }
        public long MatchId { get; set; }
        public int Order { get; set; }
        public int Stake { get; set; }
        public bool IsFt { get; set; }

        public string ResultMessage { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
