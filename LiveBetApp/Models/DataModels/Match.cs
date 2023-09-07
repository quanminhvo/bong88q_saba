using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class Match
    {
        public Match()
        {
            MaxHdpFh = 0;
            MinHdpFh = 10000;
            MaxHdpFt = 0;
            MinHdpFt = 10000;

            MaxHdpFtCurrent = 0;
            MinHdpFtCurrent = 10000;
            MaxHdpFhCurrent = 0;
            MinHdpFhCurrent = 10000;

            HasStreaming = false;
            FirstTimeHasStreaming = DateTime.MinValue;
            TimeHasStreaming = DateTime.MinValue;
            LastUpdatePhOuFtLive = DateTime.MinValue;
            LastUpdatePhOuFhLive = DateTime.MinValue;
            LastUpdatePhHdpFtLive = DateTime.MinValue;
            LastUpdatePhHdpFhLive = DateTime.MinValue;
            LastAlert24PlaySound = DateTime.MinValue;
            LastAlert25PlaySound = DateTime.MinValue;
            LastAlert26PlaySound = DateTime.MinValue;
            LastAlert27PlaySound = DateTime.MinValue;
            LastAlert29PlaySound = DateTime.MinValue;
            LastAlert30PlaySound = DateTime.MinValue;
            LastAlert31PlaySound = DateTime.MinValue;
            LastAlert32PlaySound = DateTime.MinValue;
            LastAlert33PlaySound = DateTime.MinValue;
            LastAlert34PlaySound = DateTime.MinValue;
            LastAlert36PlaySound = DateTime.MinValue;
            LastAlert37PlaySound = DateTime.MinValue;
            LastAlert40PlaySound = DateTime.MinValue;
            LastAlert41PlaySound = DateTime.MinValue;
            LastAlert42_43PlaySound = DateTime.MinValue;
            LastAlert45PlaySound = DateTime.MinValue;

            LastAlertClosePricePlaySound = DateTime.MinValue;
            LastimeHasNewProductAction = DateTime.MinValue;
            AlertNewProductAction = false;
            MinuteSpade = 0;
            this.League = "";
            HasHt = false;
            PriceStep = 9;
            PriceStepDown = 5;
            TimeHasStreaming = DateTime.MinValue;
            OverUnderMoneyLine = 0;
            CountGlobalShowtimeChange = 0;
            CountMatchCodeChange = 0;
            TotalOuLine = 0;
            OverUnderMoneyLines = new List<int>();
        }

        
        public long MatchId { get; set; }
        public string MatchCode { get; set; }
        public long HomeId { get; set; }
        public string Home { get; set; }
        public string ScoreHome { get { return "[ " + this.LiveHomeScore + " ] " + Home; } }
        public int LiveHomeScore { get; set; }
        public int HomeRed { get; set; }

        public long AwayId { get; set; }
        public string Away { get; set; }
        public string ScoreAway { get { return "[ " + this.LiveAwayScore + " ] " + Away; } }
        public int LiveAwayScore { get; set; }
        public int AwayRed { get; set; }

        public long LeagueId { get; set; }
        public string League { get; set; }

        public long KickoffTimeNumber { get; set; }
        public DateTime KickoffTime { get; set; }
        public DateTime Showtimedt { get; set; }
        public DateTime GlobalShowtime { get; set; }
        public int CountGlobalShowtimeChange { get; set; }
        public int CountMatchCodeChange { get; set; }
        public DateTime LiveTimer { get; set; }
        public TimeSpan TimeSpanFromStart
        {
            get
            {
                if (LivePeriod == 1) return DateTime.Now.Subtract(LiveTimer);
                else if (LivePeriod == 2) return DateTime.Now.Subtract(LiveTimer);
                else return new TimeSpan();
            }
        }

        public TimeSpan TimeSpanFromStart90
        {
            get
            {
                if (LivePeriod == 1) return DateTime.Now.Subtract(LiveTimer);
                else if (LivePeriod == 2) return DateTime.Now.Subtract(LiveTimer).Add(new TimeSpan(0,45,0));
                else return new TimeSpan();
            }
        }

        public TimeSpan TimeSpanFromStartGb
        {
            get
            {
                return DateTime.Now.Subtract(GlobalShowtime);
            }
        }

        public bool IsNoShowMatch
        {
            get
            {
                return LivePeriod == -1 && LastUpdatePhOuFtLive == DateTime.MinValue;
            }
        }

        public DateTime BeginHT { get; set; }
        public DateTime EndHT { get; set; }
        public DateTime FirstShow { get; set; }
        public int FirstShowIndex { get; set; }
        public DateTime FirstShowBlock
        {
            get
            {
                int initMinute = FirstShow.Minute < 15 ? 0 :
                    FirstShow.Minute < 30 ? 15 :
                    FirstShow.Minute < 45 ? 30 :
                    45;

                return new DateTime(
                    FirstShow.Year,
                    FirstShow.Month,
                    FirstShow.Day,
                    FirstShow.Hour,
                    initMinute,
                    0
                );

            }
        }
        public DateTime? BeginLive { get; set; }

        public int Csstatus { get; set; }
        public bool IsHT { get; set; }
        public int LivePeriod { get; set; }
        public bool IsMainMarket { get; set; }
        public int InjuryTime { get; set; }
        public string GoalTeam { get; set; }
        public int DelayLive { get; set; }
        public string ShowTime { get; set; }
        public int McParlay { get; set; }
        public int McSpecial { get; set; }
        public int McSpecialParlay { get; set; }
        public int OverUnderMoneyLine { get; set; }
        public int TotalOuLine { get; set; }
        public List<int> OverUnderMoneyLines { get; set; }

        public bool IsFullTime { get; set; }
        public int BestOfMap { get; set; }
        public bool IsStartingSoon { get; set; }
        public bool MoveBo3Down { get; set; }
        public int IsNeutral { get; set; }
        public bool IsTest { get; set; }

        public int MinHdpFt { get; set; }
        public int MaxHdpFt { get; set; }

        public int MinHdpFh { get; set; }
        public int MaxHdpFh { get; set; }

        public int MinHdpFtCurrent { get; set; }
        public int MaxHdpFtCurrent { get; set; }
        public int MinHdpFhCurrent { get; set; }
        public int MaxHdpFhCurrent { get; set; }

        public bool HasStreaming { get; set; }

        //public string streamingsrc { get; set; }
        //public string streamingid { get; set; }
        //public string streamingfixing { get; set; }
        //public string streamingofferto { get; set; }
        //public string _f { get; set; }

        public bool IsPen { get; set; }
        public DateTime LastTimeHasTGT225 { get; set; }
        public DateTime LastTime_Ou_050_87 { get; set; }
        public int MaxBetFirstShow { get; set; }
        public int MaxBetBeginLive { get; set; }
        public DateTime LastUpdatePhOuFtLive { get; set; }
        public DateTime LastUpdatePhOuFhLive { get; set; }
        public DateTime LastUpdatePhHdpFtLive { get; set; }
        public DateTime LastUpdatePhHdpFhLive { get; set; }

        public DateTime LastHasSpeaker { get; set; }
        public DateTime LastHasId3 { get; set; }
        public DateTime FirstTimeHasStreaming { get; set; }
        public DateTime TimeHasStreaming { get; set; }
        public DateTime LastUpdatePhOuFtLivePlaySound { get; set; }
        public DateTime LastUpdatePhLivePlaySound { get; set; }
        public DateTime LastUpdatePhGreaterHdpPlaySound { get; set; }

        public DateTime LastUpdateStepPricePlaySound { get; set; }
        public DateTime LastUpdateStepPriceDownPlaySound { get; set; }
        public DateTime LastAlert24PlaySound { get; set; }
        public DateTime LastAlert25PlaySound { get; set; }
        public DateTime LastAlert26PlaySound { get; set; }
        public DateTime LastAlert27PlaySound { get; set; }
        public DateTime LastAlert28PlaySound { get; set; }
        public DateTime LastAlert29PlaySound { get; set; }
        public DateTime LastAlert30PlaySound { get; set; }

        public DateTime LastAlert31PlaySound { get; set; }
        public DateTime LastAlert32PlaySound { get; set; }
        public DateTime LastAlert33PlaySound { get; set; }
        public DateTime LastAlert34PlaySound { get; set; }
        public DateTime LastAlert36PlaySound { get; set; }
        public DateTime LastAlert37PlaySound { get; set; }
        public DateTime LastAlert38PlaySound { get; set; }

        public DateTime LastAlert40PlaySound { get; set; }
        public DateTime LastAlert41PlaySound { get; set; }

        public DateTime LastAlert42_43PlaySound { get; set; }
        public DateTime LastAlert45PlaySound { get; set; }

        public DateTime LastAlertClosePricePlaySound { get; set; }
        public bool AlertNewProductAction { get; set; }
        public DateTime LastimeHasNewProductAction { get; set; }

        public int MinuteSpade { get; set; }

        public bool HasHt { get; set; }

        public int PriceStep { get; set; }
        public int PriceStepDown { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool HasInsert1x2FtBeforelive { get; set; }
        public bool HasInsert1x2FhBeforelive { get; set; }

        public bool NeedGetMaxBetRequest { get; set; }

    }
}
