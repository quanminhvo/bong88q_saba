using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class BetByTimming
    {
        public BetByTimming()
        {
            this.CareGoal = true;
            this.GoalLimit = 100;
        }

        public Guid Id { get; set; }
        public long MatchId { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public int Minute { get; set; }
        public int LivePeriod { get; set; }
        public Common.Enums.BetByTimmingOption BetOption { get; set; }
        public int Stake { get; set; }
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
