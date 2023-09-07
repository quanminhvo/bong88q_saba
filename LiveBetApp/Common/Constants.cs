using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Common
{
    public class Constants
    {
        public static int OverUnderScoreTime_ColumnPerMinute = 1;
        public static int DefaultMinutePerSet = 45;
        public static string NumDtFormat = "yyyyMMddHHmm";
        public static string SpeakerChar = "🔊";

        public static string LivescoreUrl = "https://www.livescore.com/soccer/";
        public static string JsonErrorLogfolder = "ErrorLog\\";

        public static string BackUpPath_AppStartLog = "Datastore\\AppStartLog";
        public static string BackUpPath_LiveScoreMatchs = "Datastore\\LiveScoreMatchs";
        public static string BackUpPath_Leagues = "Datastore\\Leagues";
        public static string BackUpPath_Matchs = "Datastore\\Matchs";
        public static string BackUpPath_OuLifeTimeHistories = "Datastore\\OuLifeTimeHistories";
        public static string BackUpPath_OverUnderScoreHalfTimes = "Datastore\\OverUnderScoreHalfTimes";
        public static string BackUpPath_OverUnderScoreTimes = "Datastore\\OverUnderScoreTimes";
        public static string BackUpPath_Products = "Datastore\\Products";
        public static string BackUpPath_GoalHistories = "Datastore\\GoalHistories";
        public static string BackUpPath_OverUnderScoreTimesFirstHalf = "Datastore\\OverUnderScoreTimesFirstHalf";
        public static string BackUpPath_OuLifeTimeFirstHalfHistories = "Datastore\\OuLifeTimeFirstHalfHistories";
        public static string BackUpPath_HandicapLifeTimeHistories = "Datastore\\HandicapLifeTimeHistories";
        public static string BackUpPath_HandicapScoreTimes = "Datastore\\HandicapScoreTimes";
        public static string BackUpPath_OverUnderLastBoundariesOfMatch = "Datastore\\OverUnderLastBoundariesOfMatch";
        public static string BackUpPath_FinishedBetByHdpPrice = "Datastore\\FinishedBetByHdpPrice";
        public static string BackUpPath_BetByHdpPrice = "Datastore\\BetByHdpPrice";
        public static string BackUpPath_BetByHandicapHdpPrice = "Datastore\\BetByHandicapHdpPrice";
        public static string BackUpPath_FinishedBetByHandicapHdpPrice = "Datastore\\FinishedBetByHandicapHdpPrice";
        public static string BackUpPath_FinishedTimmingBetOverUnder = "Datastore\\FinishedTimmingBetOverUnder";
        public static string BackUpPath_OverUnderScoreTimesV2 = "Datastore\\OverUnderScoreTimesV2";
        public static string BackUpPath_OverUnderScoreTimesV3 = "Datastore\\OverUnderScoreTimesV3";
        public static string BackUpPath_OverUnderScoreTimesV4 = "Datastore\\OverUnderScoreTimesV4";
        public static string BackUpPath_OverUnderScoreTimesFirstHalfV2 = "Datastore\\OverUnderScoreTimesFirstHalfV2";
        public static string BackUpPath_OverUnderScoreTimesFirstHalfV3 = "Datastore\\OverUnderScoreTimesFirstHalfV3";
        public static string BackUpPath_BetFirstHalfByFulltime = "Datastore\\BetFirstHalfByFulltime";
        public static string BackUpPath_FinishedBetFirstHalfByFulltime = "Datastore\\FinishedBetFirstHalfByFulltime";
        public static string BackUpPath_UnderScoreTimes = "Datastore\\UnderScoreTimes";
        public static string BackUpPath_UnderScoreHalfTimes = "Datastore\\UnderScoreHalfTimes";
        public static string BackUpPath_FinishPriceOverUnderScoreTimes = "Datastore\\FinishPriceOverUnderScoreTimes";
        public static string BackUpPath_FinishedBetByHdpPriceIuoo = "Datastore\\FinishedBetByHdpPriceIuoo";
        public static string BackUpPath_BetByHdpPriceIuoo = "Datastore\\BetByHdpPriceIuoo";
        public static string BackUpPath_MatchIuooValue_1 = "Datastore\\MatchIuooValue_1";
        public static string BackUpPath_MatchIuooValue_2 = "Datastore\\MatchIuooValue_2";
        public static string BackUpPath_ProductHistories = "Datastore\\ProductHistories";
        public static string BackUpPath_InUnderOutOvers = "Datastore\\InUnderOutOvers";
        public static string BackUpPath_MatchMaxNumberOfProductFulltimeOU = "Datastore\\MatchMaxNumberOfProductFulltimeOU";
        public static string BackUpPath_MatchNumberOfProductFulltimeOU = "Datastore\\MatchNumberOfProductFulltimeOU";
        public static string BackUpPath_BookmarkedMatchs = "Datastore\\BookmarkedMatchs";
        public static string BackUpPath_BetByHdpClose = "Datastore\\BetByHdpClose";
        public static string BackUpPath_FinishedBetByHdpClose = "Datastore\\FinishedBetByHdpClose";
        public static string BackUpPath_BetByTimming = "Datastore\\BetByTimming";
        public static string BackUpPath_FinishedBetByTimming = "Datastore\\FinishedBetByTimming";
        public static string BackUpPath_MatchIuooAlert = "Datastore\\MatchIuooAlert";

        public static string BackUpPath_OverUnderScoreTimesBeforeLive = "Datastore\\OverUnderScoreTimesBeforeLive";
        public static string BackUpPath_OverUnderScoreTimesFirstHalfBeforeLive = "Datastore\\OverUnderScoreTimesFirstHalfBeforeLive";
        public static string BackUpPath_HandicapScoreTimesBeforeLive = "Datastore\\HandicapScoreTimesBeforeLive";
        public static string BackUpPath_HandicapScoreTimesFirstHalfBeforeLive = "Datastore\\HandicapScoreTimesFirstHalfBeforeLive";
        public static string BackUpPath_VeryFirstProducts = "Datastore\\VeryFirstProducts";
        public static string BackUpPath_OverUnderScoreHalftime = "Datastore\\OverUnderScoreHalftime";
        public static string BackUpPath_BetAfterGoodPrice = "Datastore\\BetAfterGoodPrice";
        public static string BackUpPath_FinishedBetAfterGoodPrice = "Datastore\\FinishedBetAfterGoodPrice";
        public static string BackUpPath_Excel4th = "Datastore\\Excel4th";
        public static string BackUpPath_ProductIdsOfMatch = "Datastore\\ProductIdsOfMatch";

        public static string BackUpPath_ProductIdsOuFtOfMatchHistoryLive = "Datastore\\ProductIdsOuFtOfMatchHistoryLive";
        public static string BackUpPath_ProductIdsOuFtOfMatchHistoryBeforeLive = "Datastore\\ProductIdsOuFtOfMatchHistoryBeforeLive";

        public static string BackUpPath_ProductIdsOuFhOfMatchHistoryBeforeLive = "Datastore\\ProductIdsOuFhOfMatchHistoryBeforeLive";
        public static string BackUpPath_ProductIdsOuFhOfMatchHistoryLive = "Datastore\\ProductIdsOuFhOfMatchHistoryLive";

        public static string BackUpPath_ProductIdsHdpFhOfMatchHistoryBeforeLive = "Datastore\\ProductIdsHdpFhOfMatchHistoryBeforeLive";
        public static string BackUpPath_ProductIdsHdpFhOfMatchHistoryLive = "Datastore\\ProductIdsHdpFhOfMatchHistoryLive";

        public static string BackUpPath_ProductIdsHdpFtOfMatchHistoryBeforeLive = "Datastore\\ProductIdsHdpFtOfMatchHistoryBeforeLive";
        public static string BackUpPath_ProductIdsHdpFtOfMatchHistoryLive = "Datastore\\ProductIdsHdpFtOfMatchHistoryLive";
        public static string BackUpPath_PropertyMapping = "Datastore\\PropertyMapping";
        public static string BackUpPath_TrackOverUnderBeforeLive = "Datastore\\TrackOverUnderBeforeLive";

        public static string BackUpPath_Alert_Wd20 = "Datastore\\Alert_Wd20";
        public static string BackUpPath_Alert_Wd32 = "Datastore\\Alert_Wd32";
        public static string BackUpPath_Alert_Wd34 = "Datastore\\Alert_Wd34";

        public static string BackUpPath_OverUnderScoreTimesV4_60 = "Datastore\\OverUnderScoreTimesV4_60";
        public static string BackUpPath_ProductStatus = "Datastore\\ProductStatus";
        public static string BackUpPath_UnderScoreTimesFirstHalf = "Datastore\\UnderScoreTimesFirstHalf";
        public static string BackUpPath_Alert_Wd26 = "Datastore\\Alert_Wd26";
        public static string BackUpPath_Alert_Wd35 = "Datastore\\Alert_Wd35";
        public static string BackUpPath_Alert_Wd36 = "Datastore\\Alert_Wd36";
        public static string BackUpPath_Alert_Wd37 = "Datastore\\Alert_Wd37";
        public static string BackUpPath_Alert_Wd38 = "Datastore\\Alert_Wd38";
        public static string BackUpPath_HandicapFhScoreTimes = "Datastore\\HandicapFhScoreTimes";
        public static string BackUpPath_CardHistories = "Datastore\\CardHistories";
        public static string BackUpPath_GlobalShowtimeHistories = "Datastore\\GlobalShowtimeHistories";
        public static string BackUpPath_OverUnderScoreTimesFreq = "Datastore\\OverUnderScoreTimesFreq";
        public static string BackUpPath_OverUnderScoreTimesFreqFh = "Datastore\\OverUnderScoreTimesFreqFh";


        public static string BackUpPath_OverUnderScoreTimesFreqBeforeLive = "Datastore\\OverUnderScoreTimesFreqBeforeLive";
        public static string BackUpPath_OverUnderScoreTimesFreqFhBeforeLive = "Datastore\\OverUnderScoreTimesFreqFhBeforeLive";
        public static string BackUpPath_OverUnderScoreTimesFreqHalfTime = "Datastore\\OverUnderScoreTimesFreqHalfTime";

        public static string BackUpPath_HandicapFreqBeforeLive = "Datastore\\HandicapFreqBeforeLive";
        public static string BackUpPath_HandicapFreqFhBeforeLive = "Datastore\\HandicapFreqFhBeforeLive";
        public static string BackUpPath_HandicapFreqHalfTime = "Datastore\\HandicapFreqHalfTime";

        public static string BackUpPath_FirstShowIndex = "Datastore\\FirstShowIndex";
        public static string BackUpPath_MatchCodeHistories = "Datastore\\MatchCodeHistories";
        public static string BackUpPath_MatchMaxBetNonRequest = "Datastore\\MatchMaxBetNonRequest";
        public static string BackUpPath_MatchMaxBetRequest = "Datastore\\MatchMaxBetRequest";
        public static string BackUpPath_MatchOverUnderMoneyLine = "Datastore\\MatchOverUnderMoneyLine";
        public static string BackUpPath_BlackList = "Datastore\\BlackList";
        //
        //
        //
        //
        public static int[] MinutesToAlert = { 7, 16, 25, 34 };
        public static int[] MinutesToCaptureHandicapPrice = {0, 1, 9, 18, 27, 36, 45, 45 + 1, 45 + 9, 45 + 18, 45 + 27, 45 + 36, 45 + 45 };
    }
}
