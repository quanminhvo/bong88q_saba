using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.ViewModels
{
    public class Match1x2Product : ICloneable
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

        public string M1 { get; set; }
        public string M2 { get; set; }
        public string M3 { get; set; }
        public string M4 { get; set; }
        public string M5 { get; set; }
        public string M6 { get; set; }
        public string M7 { get; set; }
        public string M8 { get; set; }
        public string M9 { get; set; }
        public string M10 { get; set; }
        public string M11 { get; set; }
        public string M12 { get; set; }
        public string M13 { get; set; }
        public string M14 { get; set; }
        public string M15 { get; set; }
        public string M16 { get; set; }
        public string M17 { get; set; }
        public string M18 { get; set; }
        public string M19 { get; set; }
        public string M20 { get; set; }
        public string M21 { get; set; }
        public string M22 { get; set; }
        public string M23 { get; set; }
        public string M24 { get; set; }
        public string M25 { get; set; }
        public string M26 { get; set; }
        public string M27 { get; set; }
        public string M28 { get; set; }
        public string M29 { get; set; }
        public string M30 { get; set; }
        public string M31 { get; set; }
        public string M32 { get; set; }
        public string M33 { get; set; }
        public string M34 { get; set; }
        public string M35 { get; set; }
        public string M36 { get; set; }
        public string M37 { get; set; }
        public string M38 { get; set; }
        public string M39 { get; set; }
        public string M40 { get; set; }
        public string M41 { get; set; }
        public string M42 { get; set; }
        public string M43 { get; set; }
        public string M44 { get; set; }
        public string M45 { get; set; }

        public object Clone()
        {
            return new Match1x2Product()
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
                M1 = M1,
                M2 = M2,
                M3 = M3,
                M4 = M4,
                M5 = M5,
                M6 = M6,
                M7 = M7,
                M8 = M8,
                M9 = M9,
                M10 = M10,
                M11 = M11,
                M12 = M12,
                M13 = M13,
                M14 = M14,
                M15 = M15,
                M16 = M16,
                M17 = M17,
                M18 = M18,
                M19 = M19,
                M20 = M20,
                M21 = M21,
                M22 = M22,
                M23 = M23,
                M24 = M24,
                M25 = M25,
                M26 = M26,
                M27 = M27,
                M28 = M28,
                M29 = M29,
                M30 = M30,
                M31 = M31,
                M32 = M32,
                M33 = M33,
                M34 = M34,
                M35 = M35,
                M36 = M36,
                M37 = M37,
                M38 = M38,
                M39 = M39,
                M40 = M40,
                M41 = M41,
                M42 = M42,
                M43 = M43,
                M44 = M44,
                M45 = M45
            };
        }

    }
}
