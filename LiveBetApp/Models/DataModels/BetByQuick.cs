using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class BetByQuick
    {
        public Guid Id { get; set; }
        public long MatchId { get; set; }
        public int Minute { get; set; }
        public int Hdp { get; set; }
        public int Stake { get; set; }
        public bool Planted { get; set; }
        public bool CareGoal { get; set; }
        public int TotalGoal { get; set; }
    }
}
