using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class InUnderOutOver
    {
        public long MatchId { get; set; }
        public int UnderHdp { get; set; }
        public string Over100Message { get; set; }
        public string Over075Message { get; set; }
        public string Over050Message { get; set; }
    }
}
