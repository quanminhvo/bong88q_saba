using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class MatchCodeHistory
    {
        public string MathCode { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateTimeStr { get { return UpdateTime.ToString("dd/MM HH:mm"); } }
    }
}
