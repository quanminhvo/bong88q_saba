using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetApp.Models.ViewModels
{
    public class Match
    {
        public string Home { get; set; }
        public string Away { get; set; }
        public string Score { get; set; }
        public string TimePlaying { get; set; }
    }
}
