using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class BetFirstHalfByFulltime
    {
        public BetFirstHalfByFulltime()
        {
            CareGoal = true;
        }
        public Guid Id { get; set; }
        public long MatchId { get; set; }

        public int Hdp { get; set; }
        public int Price { get; set; }

        public int Stake { get; set; }
        public bool IsOver { get; set; }
        public bool BetMax { get; set; }
        public int TotalScore { get; set; }
        public int TotalRed { get; set; }
        public string ResultMessage { get; set; }
        public string AutoBetMessage { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int MoneyLine { get; set; }
        public bool CareGoal { get; set; }
        public int GoalLimit { get; set; }
    }
}
