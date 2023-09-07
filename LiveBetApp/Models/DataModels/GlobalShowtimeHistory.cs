using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class GlobalShowtimeHistory
    {
        public DateTime GlobalShowtime { get; set; }
        public string GlobalShowtimeStr { get { return GlobalShowtime.ToString("dd/MM HH:mm"); } }
        public DateTime UpdateTime { get; set; }
        public string UpdateTimeStr { get { return UpdateTime.ToString("dd/MM HH:mm"); } }
    }
}
