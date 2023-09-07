using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class LiveScoreMatch
    {
        public string Home { get; set; }
        public string Away { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
