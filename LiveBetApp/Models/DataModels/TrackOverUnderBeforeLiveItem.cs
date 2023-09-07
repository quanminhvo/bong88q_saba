using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class TrackOverUnderBeforeLiveItem
    {
        public TrackOverUnderBeforeLiveItem()
        {
            this.Hdps_100 = new List<KeyValuePair<int, long>>();
        }
        public long MinuteBeforeLive { get; set; }
        public List <KeyValuePair<int, long>> Hdps_100 { get; set; }
    }
}
