using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class ProcessBetModel : ProcessBetModelV2
    {
        public ProcessBetModel()
        {
            this.sinfo = "";
        }
        //public string sportname { get; set; }
        //public string gamename { get; set; }
        //public string bettypename { get; set; }
        //public string ChoiceValue { get; set; }
        //public string Line { get; set; }
        //public string displayHDP { get; set; }
        //public string odds { get; set; }
        //public string home { get; set; }
        //public string away { get; set; }
        //public string league { get; set; }
        //public string IsLive { get; set; }
        //public string ProgramID { get; set; }
        //public string RaceNum { get; set; }
        //public string Runner { get; set; }
        //public string PoolType { get; set; }
        //public string imgurl { get; set; }
        //public string BetID { get; set; }
        //public string type { get; set; }
        //public string bettype { get; set; }
        //public string oddsid { get; set; }
        //public string Hscore { get; set; }
        //public string Ascore { get; set; }
        //public string Matchid { get; set; }
        //public string betteam { get; set; }
        //public string stake { get; set; }
        //public string gameid { get; set; }
        //public string MRPercentage { get; set; }
        //public string OddsInfo { get; set; }
        //public string LiveInfo { get; set; } // "" or [1-0]
        //public string AcceptBetterOdds { get; set; }
        //public string AutoAcceptSec { get; set; }
        //public string ParentTypeId { get; set; }
        //public string showLiveScore { get; set; }
        //public string colorHomeTeam { get; set; }
        //public string colorAwayTeam { get; set; }
        //public string hasParlay { get; set; }
        //public string matchcode { get; set; }
        //public string isQuickBet { get; set; }
        //public string UseBonus { get; set; }
        //public string kickofftime { get; set; }
        //public string ShowTime { get; set; }
        //public string oddsStatus { get; set; }
        //public string min { get; set; }
        //public string max { get; set; }
    }

    public class ProcessBetModelV2
    {
        public string Type { get; set; }
        public string Bettype { get; set; }
        public string Oddsid { get; set; }
        public string Odds { get; set; }
        public string Line { get; set; }
        public string Hdp1 { get; set; }
        public string Hdp2 { get; set; }
        public string Hscore { get; set; }
        public string Ascore { get; set; }
        public string Betteam { get; set; }
        public string Stake { get; set; }
        public string Matchid { get; set; }
        public string ChoiceValue { get; set; }
        public string SrcOddsInfo { get; set; }
        public string ErrorCode { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public string Gameid { get; set; }
        public string ProgramID { get; set; }
        public string RaceNum { get; set; }
        public string Runner { get; set; }
        public string PoolType { get; set; }
        public string MRPercentage { get; set; }
        public string AcceptBetterOdds { get; set; }
        public string isQuickBet { get; set; }
        public string isTablet { get; set; }


        public string IsInPlay { get; set; }
        public string BonusID { get; set; }
        public string BonusType { get; set; }
        public string PromoID { get; set; }
        public string sinfo { get; set; }
    }

}

//public string PostData
//{
//get
//{
//string result = "";

//result += "ItemList[0][sportname]=" + sportname;
//result += "&ItemList[0][gamename]=" + gamename;
//result += "&ItemList[0][bettypename]=" + bettypename;
//result += "&ItemList[0][ChoiceValue]=" + ChoiceValue;
//result += "&ItemList[0][Line]=" + Line;
//result += "&ItemList[0][displayHDP]=" + displayHDP;
//result += "&ItemList[0][odds]=" + odds;
//result += "&ItemList[0][home]=" + home;
//result += "&ItemList[0][away]=" + away;
//result += "&ItemList[0][league]=" + league;
//result += "&ItemList[0][IsLive]=" + IsLive;
//result += "&ItemList[0][ProgramID]=" + ProgramID;
//result += "&ItemList[0][RaceNum]=" + RaceNum;
//result += "&ItemList[0][Runner]=" + Runner;
//result += "&ItemList[0][PoolType]=" + PoolType;
//result += "&ItemList[0][imgurl]=" + imgurl;
//result += "&ItemList[0][BetID]=" + BetID;
//result += "&ItemList[0][type]=" + type;
//result += "&ItemList[0][bettype]=" + bettype;
//result += "&ItemList[0][oddsid]=" + oddsid;
//result += "&ItemList[0][Hscore]=" + Hscore;
//result += "&ItemList[0][Ascore]=" + Ascore;
//result += "&ItemList[0][Matchid]=" + Matchid;
//result += "&ItemList[0][betteam]=" + betteam;
//result += "&ItemList[0][stake]=" + stake;
//result += "&ItemList[0][gameid]=" + gameid;
//result += "&ItemList[0][MRPercentage]=" + MRPercentage;
//result += "&ItemList[0][OddsInfo]=" + OddsInfo;
//result += "&ItemList[0][LiveInfo]=" + LiveInfo;
//result += "&ItemList[0][AcceptBetterOdds]=" + AcceptBetterOdds;
//result += "&ItemList[0][AutoAcceptSec]=" + AutoAcceptSec;
//result += "&ItemList[0][ParentTypeId]=" + ParentTypeId;
//result += "&ItemList[0][showLiveScore]=" + showLiveScore;
//result += "&ItemList[0][colorHomeTeam]=" + colorHomeTeam;
//result += "&ItemList[0][colorAwayTeam]=" + colorAwayTeam;
//result += "&ItemList[0][hasParlay]=" + hasParlay;
//result += "&ItemList[0][matchcode]=" + matchcode;
//result += "&ItemList[0][isQuickBet]=" + isQuickBet;
//result += "&ItemList[0][UseBonus]=" + UseBonus;
//result += "&ItemList[0][kickofftime]=" + kickofftime;
//result += "&ItemList[0][ShowTime]=" + ShowTime;
//result += "&ItemList[0][oddsStatus]=" + oddsStatus;
//result += "&ItemList[0][min]=" + min;
//result += "&ItemList[0][max]=" + max;

//return result;
//}
//}
