using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.ViewModels
{
    public class BetByHdpPrice
    {
        public Guid Id { get; set; }
        public long MatchId { get; set; }
        
        public string Home { get; set; }
        public string Away { get; set; }
        public bool IsFulltime { get; set; }
        public int LivePeriod { get; set; }
        public bool CareGoal { get; set; }
        public int Hdp { get; set; }
        public int Price { get; set; }
        public int UpToMinute { get; set; }
        public string BetTeam { get; set; }
        public int Stake { get; set; }
        public bool IsOver { get; set; }
        public int TotalScore { get; set; }
        public int TotalRed { get; set; }
        public string ResultMessage { get; set; }
        public string AutoBetMessage { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int MoneyLine { get; set; }
    }

    public class BetByHdpPriceViewModel
    {
        public BetByHdpPriceViewModel()
        {
            this.CareGoal = true;
            this.GoalLimit = 100;
            this.ResultMessages = new List<string>();
        }
        public Guid Id { get; set; }
        public long MatchId { get; set; }
        public bool IsFulltime { get; set; }
        public int LivePeriod { get; set; }
        public int Hdp { get; set; }
        public int Price { get; set; }
        public int UpToMinute { get; set; }
        public bool CareGoal { get; set; }
        public int GoalLimit { get; set; }
        public string BetTeam { get; set; }
        public int Stake { get; set; }
        
        public int TotalScore { get; set; }
        public int TotalRed { get; set; }
        public string ResultMessage { get; set; }
        public string AutoBetMessage { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int MoneyLine { get; set; }
        public List<string> ResultMessages { get; set; }
    }
}
