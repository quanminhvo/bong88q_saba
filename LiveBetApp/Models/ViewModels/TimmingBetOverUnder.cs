using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.ViewModels
{
    public class TimmingBetOverUnder
    {
        public Guid Id { get; set; }
        public long MatchId { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public bool IsFulltime { get; set; }
        public int LivePeriod { get; set; }
        public int Minute { get; set; }
        public int Stake { get; set; }
        public bool IsOver { get; set; }
        public string ResultMessage { get; set; }
        public string AutoBetMessage { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
