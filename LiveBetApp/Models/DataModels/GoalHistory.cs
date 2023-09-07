using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class GoalHistory
    {
        public GoalHistory()
        {
            ProductOuId = new List<long>();
            ProductStatusLogs = new List<ProductStatusLog>();
        }

        public string GoalTeam { get; set; }
        public int LivePeriod { get; set; }
        public TimeSpan TimeSpanFromStart { get; set; }
        public int RealMinute { get { return (LivePeriod - 1) * 45 + (int)TimeSpanFromStart.TotalMinutes; } }

        public List<long> ProductOuId { get; set; }
        public List<ProductStatusLog> ProductStatusLogs { get; set; }
        public bool MatchDetectFirst { get; set; }
    }

    public class CardHistory
    {
        public int LivePeriod { get; set; }
        public TimeSpan TimeSpanFromStart { get; set; }
        public int RealMinute { get { return (LivePeriod - 1) * 45 + (int)TimeSpanFromStart.TotalMinutes; } }
    }

    public class GoalHistoryByProduct
    {
        public GoalHistoryByProduct()
        {

        }
        public DateTime DetectAt { get; set; }
    }
}
