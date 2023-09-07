using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.ViewModels
{
    public class Match
    {
        //public string streamingsrc { get; set; }
        //public string streamingid { get; set; }
        //public string streamingfixing { get; set; }
        //public string streamingofferto { get; set; }
        //public string _f { get; set; }
        public long MatchId { get; set; }
        public string MatchCode { get; set; }
        public int CountMatchCodeChange { get; set; }
        public string MatchCodeStr
        {
            get
            {
                string matchCode = "";
                if (MatchCode != null && MatchCode.Length > 4)
                {
                    matchCode = MatchCode.Substring(0, 4) + " " + MatchCode.Substring(4);
                }


                if (CountMatchCodeChange <= 1)
                {
                    return matchCode;
                }
                return matchCode + " (" + (CountMatchCodeChange - 1) + ")";
            }
        }

        public string FreqBeforeLive { get; set; }
        public string League { get; set; }


        public DateTime FirstShow { get; set; }
        public DateTime GlobalShowtime { get; set; }
        public int CountGlobalShowtimeChange { get; set; }

        public string GlobalShowtimeStr
        {
            get
            {
                if (CountGlobalShowtimeChange <= 1)
                {
                    return GlobalShowtime.ToString("HH:mm");
                }
                return "(" + (CountGlobalShowtimeChange - 1) + ") " + GlobalShowtime.ToString("HH:mm");
            }
        }
        public string FirstShowStr
        {
            get
            {
                return FirstShow.ToString("HH:mm");
            }
        }
        public int FirstShowIndex { get; set; }

        public string DiffMinute
        {
            get
            {
                return (GlobalShowtime.Subtract(FirstShow).TotalMinutes / 60).ToString("N1");
            }
        }
        public bool IsNoShowMatch { get; set; }
        
        public TimeSpan TimeSpanFromStart { get; set; }

        public string OverUnderMoneyLine { get; set; }
        public List<int> OverUnderMoneyLines { get; set; }

        public string MPH { get; set; }
        public string SPT { get; set; }

        public int Order { get; set; }
        public int LivePeriod { get; set; }
        public int MinuteFromStart { get { return (int)TimeSpanFromStart.TotalMinutes; } }
        public string Home { get; set; }
        public int LiveHomeScore { get; set; }
        public int LiveAwayScore { get; set; }
        public string Away { get; set; }
        public string HasStreaming { get; set; }
        public string HdpAlert { get; set; }
        public int MaxBetFirstShow { get; set; }
        public int MaxBetBeginLive { get; set; }
        public string IUOO_ft { get; set; }
        public string IUOO_fh { get; set; }
        public string IUOO_hdp { get; set; }
        public string PriceStep { get; set; }
        public string PriceStepDown { get; set; }

        public string PHBeforeLive { get; set; }
        public string PHLive { get; set; }
        public string GoalHistory { get; set; }
        public string PenHistory { get; set; }
        public string OpenHdps { get; set; }
        public string OpenPrices { get; set; }

        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
        public string P4 { get; set; }
        public string MinMaxFt { get; set; }
        public string MinMaxFh { get; set; }



        public bool IsHT { get; set; }

        public int HomeRed { get; set; }
        public int AwayRed { get; set; }
        //public bool HasPlan { get; set; }



    }

    public class MatchExport
    {
        public string MatchCode { get; set; }
        public string MatchCodeStr { get; set; }
        public string League { get; set; }
        public string GlobalShowtimeStr { get; set; }
        public string FirstShowStr { get; set; }
        public int FirstShowIndex { get; set; }
        public int DiffMinute { get; set; }
        public int LivePeriod { get; set; }
        public string OverUnderMoneyLine { get; set; }
        public string MPH { get; set; }
        public string SPT { get; set; }
        public int Order { get; set; }
        public int MinuteFromStart { get; set; }
        public string Home { get; set; }
        public int LiveHomeScore { get; set; }
        public int LiveAwayScore { get; set; }
        public string Away { get; set; }
        public string HasStreaming { get; set; }
        public string HdpAlert { get; set; }
        public string IUOO_fh { get; set; }
        public string IUOO_hdp { get; set; }
        public int PriceStep { get; set; }
        public int PriceStepDown { get; set; }
        public string GoalHistory { get; set; }
        public string PenHistory { get; set; }
        public string OpenHdps { get; set; }
        public string MinMaxFt { get; set; }
        public string MinMaxFh { get; set; }
    }

    public class AlertMatch
    {
        public long MatchId { get; set; }
        public string League { get; set; }
        public string ScoreHome { get; set; }
        public string ScoreAway { get; set; }
        public string CustomValue { get; set; }
    }

    public class BookmarkedMatch
    {
        public long MatchId { get; set; }
        public string League { get; set; }
        public string ScoreHome { get; set; }
        public string ScoreAway { get; set; }
    }
}
