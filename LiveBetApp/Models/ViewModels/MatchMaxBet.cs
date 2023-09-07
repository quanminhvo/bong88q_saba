using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.ViewModels
{
    public class MatchMaxBet : ICloneable
    {
        public long MatchId { get; set; }
        public string MatchCode { get; set; }
        public bool IsNoShowMatch { get; set; }
        public int Order { get; set; }
        public string League { get; set; }
        public DateTime GlobalShowtime { get; set; }
        public string GlobalShowtimeStr
        {
            get
            {
                return GlobalShowtime.ToString("dd/MM HH:mm");
            }
        }
        public TimeSpan TimeSpanFromStart { get; set; }
        public int LivePeriod { get; set; }
        public int MinuteFromStart { get { return (int)TimeSpanFromStart.TotalMinutes; } }

        public string Home { get; set; }
        public int LiveHomeScore { get; set; }
        public int LiveAwayScore { get; set; }
        public string Away { get; set; }
        public string HasStreaming { get; set; }
        public bool IsHT { get; set; }

        public int M35H1 { get; set; }
        public int M36H1 { get; set; }
        public int M37H1 { get; set; }
        public int M38H1 { get; set; }
        public int M39H1 { get; set; }
        public int M40H1 { get; set; }
        public int M41H1 { get; set; }
        public int M42H1 { get; set; }
        public int M43H1 { get; set; }
        public int M44H1 { get; set; }
        public int M45H1 { get; set; }
        public int M46H1 { get; set; }
        public int M47H1 { get; set; }

        public int M35H2 { get; set; }
        public int M36H2 { get; set; }
        public int M37H2 { get; set; }
        public int M38H2 { get; set; }
        public int M39H2 { get; set; }
        public int M40H2 { get; set; }
        public int M41H2 { get; set; }
        public int M42H2 { get; set; }
        public int M43H2 { get; set; }
        public int M44H2 { get; set; }
        public int M45H2 { get; set; }

        public int M46H2 { get; set; }
        public int M47H2 { get; set; }
        public int M48H2 { get; set; }
        public int M49H2 { get; set; }

        public object Clone()
        {
            return new MatchMaxBet()
            {
                MatchId = MatchId,
                MatchCode = MatchCode,
                IsNoShowMatch = IsNoShowMatch,
                Order = Order,
                League = League,
                GlobalShowtime = GlobalShowtime,
                TimeSpanFromStart = TimeSpanFromStart,
                LivePeriod = LivePeriod,
                Home = Home,
                LiveHomeScore = LiveHomeScore,
                LiveAwayScore = LiveAwayScore,
                Away = Away,
                HasStreaming = HasStreaming,
                IsHT = IsHT,
                M35H1 = M35H1,
                M36H1 = M36H1,
                M37H1 = M37H1,
                M38H1 = M38H1,
                M39H1 = M39H1,
                M40H1 = M40H1,
                M41H1 = M41H1,
                M42H1 = M42H1,
                M43H1 = M43H1,
                M44H1 = M44H1,
                M45H1 = M45H1,
                M35H2 = M35H2,
                M36H2 = M36H2,
                M37H2 = M37H2,
                M38H2 = M38H2,
                M39H2 = M39H2,
                M40H2 = M40H2,
                M41H2 = M41H2,
                M42H2 = M42H2,
                M43H2 = M43H2,
                M44H2 = M44H2,
                M45H2 = M45H2,
            };
        }
    }
}
