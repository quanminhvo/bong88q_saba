using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;

namespace LiveBetApp
{
    public class DataStore
    {
        public static int HourToBackUp { get; set; }
        public static int MinuteToBackUp { get; set; }
        public static bool IsWss = false;
        public static bool IsSourceV1 = false;
        public static bool IsMiniFont = false;
        public static bool SemiAutoBetOnly = false;
        public static bool ShowSabaOnly = false;
        public static DateTime GlobalDateTime { get; set; }
        public static DateTime FilterDate { get; set; }
        //public static DateTime MatchFilterDateTimeFrom { get; set; }
        //public static DateTime MatchFilterDateTimeTo { get; set; }
        public static bool MatchFilterDateTimeChecked { get; set; }
        public static bool ShowPlaningMatchsChecked { get; set; }
        public static bool ShowFinishedMatchsChecked { get; set; }

        public static bool ShowRunningMatchsH1Checked { get; set; }
        public static bool ShowRunningMatchsH2Checked { get; set; }
        public static bool ShowRunningMatchsHtChecked { get; set; }

        public static bool ShowNoShowMatchsChecked { get; set; }

        public static DateTime LastTick { get; set; }

        public static bool SpecialFilter = false;

        public static bool KeepPlaySoundAl32 = true;
        public static bool KeepPlaySoundAl31 = false;
        public static bool KeepPlaySoundAl29 = false;
        public static bool KeepPlaySoundAl30 = false;
        public static bool KeepPlaySoundAl27 = true;
        public static bool KeepPlaySoundAl26 = false;
        public static bool KeepPlaySoundAl25 = false;
        public static bool KeepPlaySoundAl24 = false;
        public static bool KeepPlaySoundAl22 = false;
        public static bool KeepPlaySoundAl34 = true;
        public static bool KeepPlaySoundAl36_37 = true;
        public static bool KeepPlaySoundAl38 = true;
        public static bool KeepPlaySoundAl39 = true;
        public static bool KeepPlaySoundAl40_41 = false;
        public static bool KeepPlaySoundAlUpdatePhOuLive = true;
        public static bool KeepPlaySoundHasStatusClosePrice = true;
        public static bool MainformKeepRefreshingChecked;
        public static bool ShowCornersChecked;
        public static int MaxHdp;
        public static int MinHdp;
        public static int OuMoneyLine;
        public static int QuickBetMinute { get; set; }
        public static Enums.LiveSteamSearch LSteamSearch;
        public static Enums.RunningFinishedSearch RFinishedSearch;
        public static List<string> Backuptimes { get; set; }

        public static LiveBetApp.Common.Enums.VisualThemes Theme;
        public static List<bool> AlertSetting { get; set; }
        public static List<int> AlertMatchCountBeforeUpdate { get; set; }
        public static List<int> AlertMatchCountAfterUpdate { get; set; }

        public static string LeagueFilter;


        public static int AutobetStake { get; set; }
        public static bool RunAutobet { get; set; }
        public static ConfigModel Config { get; set; }

        public static List<long> AppStartLog { get; set; }

        public static List<TimmingBetOverUnder> TimmingBetOverUnder { get; set; }
        public static List<TimmingBetOverUnder> FinishedTimmingBetOverUnder { get; set; }

        public static List<BetByTimming> BetByTimming { get; set; }
        public static List<BetByTimming> FinishedBetByTimming { get; set; }

        public static List<BetByHdpPrice> BetByHdpPrice { get; set; }
        public static List<BetByHdpPrice> FinishedBetByHdpPrice { get; set; }

        public static List<BetByHdpPrice> BetByHandicapHdpPrice { get; set; }
        public static List<BetByHdpPrice> FinishedBetByHandicapHdpPrice { get; set; }

        public static List<BetByHdpPrice> BetByHdpPriceIuoo { get; set; }
        public static List<BetByHdpPrice> FinishedBetByHdpPriceIuoo { get; set; }

        public static List<BetByHdpClose> BetByHdpClose { get; set; }
        public static List<BetByHdpClose> FinishedBetByHdpClose { get; set; }

        public static List<BetDirect> BetDirect { get; set; }
        public static List<BetDirect> FinishedBetDirect { get; set; }



        public static List<BetAfterGoodPrice> BetAfterGoodPrice { get; set; }
        public static List<BetAfterGoodPrice> FinishedBetAfterGoodPrice { get; set; }

        public static List<BetByQuick> BetByQuick { get; set; }
        public static List<BetByQuick> FinishedBetByQuick { get; set; }

        public static List<BetFirstHalfByFulltime> BetFirstHalfByFulltime { get; set; }
        public static List<BetFirstHalfByFulltime> FinishedBetFirstHalfByFulltime { get; set; }

        public static Dictionary<long, string> Leagues { get; set; }

        public static Dictionary<long, List<ProductIdLog>> ProductIdsOfMatch { get; set; }
        public static Dictionary<long, List<ProductIdsHistory>> ProductIdsOuFtOfMatchHistoryLive { get; set; }
        public static Dictionary<long, List<ProductIdsHistory>> ProductIdsOuFtOfMatchHistoryBeforeLive { get; set; }

        public static Dictionary<long, List<ProductIdsHistory>> ProductIdsOuFhOfMatchHistoryLive { get; set; }
        public static Dictionary<long, List<ProductIdsHistory>> ProductIdsOuFhOfMatchHistoryBeforeLive { get; set; }

        public static Dictionary<long, List<ProductIdsHistory>> ProductIdsHdpFtOfMatchHistoryLive { get; set; }
        public static Dictionary<long, List<ProductIdsHistory>> ProductIdsHdpFtOfMatchHistoryBeforeLive { get; set; }

        public static Dictionary<long, List<ProductIdsHistory>> ProductIdsHdpFhOfMatchHistoryLive { get; set; }
        public static Dictionary<long, List<ProductIdsHistory>> ProductIdsHdpFhOfMatchHistoryBeforeLive { get; set; }

        public static Dictionary<long, Models.DataModels.Match> Matchs { get; set; } // <MatchId, Match>
        public static Dictionary<long, Product> Products { get; set; } // <OddsId, Product>
        public static Dictionary<long, List<List<ProductStatusLog>>> ProductStatus { get; set; } // <OddsId, Product>
        public static Dictionary<long, DateTime> MatchHasProductStatus { get; set; } // <OddsId, Product>
        public static Dictionary<long, List<Product>> VeryFirstProducts { get; set; } // <OddsId, Match>

        public static Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV3Item>>> OverUnderScoreTimesBeforeLive { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV3Item>>> OverUnderScoreTimesFirstHalfBeforeLive { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<HandicapLifeTimeHistoryV2>>> HandicapScoreTimesBeforeLive { get; set; } // <MatchId, <ProductId, [history1, history2, history3,...]>>
        public static Dictionary<long, Dictionary<int, List<HandicapLifeTimeHistoryV2>>> HandicapScoreTimesFirstHalfBeforeLive { get; set; } // <MatchId, <ProductId, [history1, history2, history3,...]>>

        public static Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV3Item>>> OverUnderScoreHalftime { get; set; }

        public static Dictionary<long, Dictionary<int, List<string>>> UnderScoreTimes { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<string>>> UnderScoreHalfTimes { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<string>>> UnderScoreTimesFirstHalf { get; set; } // <MatchId, <Score, [over1, over2,...]>>

        public static Dictionary<long, Dictionary<int, List<string>>> OverUnderScoreTimes { get; set; } // <MatchId, <Score, [over1, over2,...]>>

        public static Dictionary<long, Dictionary<int, List<int>>> OverUnderScoreTimesFreqBeforeLive { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<int>>> OverUnderScoreTimesFreqFhBeforeLive { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<int>>> OverUnderScoreTimesFreqHalfTime { get; set; } // <MatchId, <Score, [over1, over2,...]>>


        public static Dictionary<long, Dictionary<int, List<int>>> HandicapFreqBeforeLive { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<int>>> HandicapFreqFhBeforeLive { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<int>>> HandicapFreqHalfTime { get; set; } // <MatchId, <Score, [over1, over2,...]>>


        public static Dictionary<long, Dictionary<int, List<int>>> OverUnderScoreTimesFreq { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<int>>> OverUnderScoreTimesFreqFh { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<string>>> OverUnderScoreHalfTimes { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<string>>> OverUnderScoreTimesFirstHalf { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<long, List<HandicapLifeTimeHistory>>> HandicapScoreTimes { get; set; } // <MatchId, <ProductId, [history1, history2, history3,...]>>
        public static Dictionary<long, Dictionary<long, List<HandicapLifeTimeHistory>>> HandicapFhScoreTimes { get; set; } // <MatchId, <ProductId, [history1, history2, history3,...]>>

        public static Dictionary<long, List<GoalHistory>> GoalHistories { get; set; } // <MatchId, [history1, history2, history3,..]>
        public static Dictionary<long, List<CardHistory>> CardHistories { get; set; } // <MatchId, [history1, history2, history3,..]>
        public static Dictionary<long, List<PenHistory>> PenHistories { get; set; }
        public static Dictionary<long, List<GlobalShowtimeHistory>> GlobalShowtimeHistories { get; set; }
        public static Dictionary<long, List<MatchCodeHistory>> MatchCodeHistories { get; set; }

        public static Dictionary<long, Dictionary<int, int>> OverUnderLastBoundariesOfMatch { get; set; }
        public static Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>> OverUnderScoreTimesV2 { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>> OverUnderScoreTimesV3 { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>> OverUnderScoreTimesV4_60 { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>> OverUnderScoreTimesFirstHalfV2 { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>> OverUnderScoreTimesFirstHalfV3 { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, Dictionary<int, OverUnderScoreTimesV2Item>> FinishPriceOverUnderScoreTimes { get; set; } // <MatchId, <Score, [over1, over2,...]>>
        public static Dictionary<long, InUnderOutOver> InUnderOutOvers { get; set; } // <MatchId, InUnderOutOver>
        public static Dictionary<long, Dictionary<int, OverUnderSummary>> Excel4th { get; set; }
        public static Dictionary<long, Dictionary<int, IuooValue>> MatchIuooValue_1 { get; set; } // <MatchId, <hdp, [iuoo1, iuoo2,...]>>
        public static Dictionary<long, Dictionary<int, IuooValue>> MatchIuooValue_2 { get; set; } // <MatchId, <hdp, [iuoo1, iuoo2,...]>>
        public static List<BlackList> Blacklist { get; set; }
        public static Dictionary<long, MatchIuooAlertItem> MatchIuooAlert { get; set; }
        public static List<long> OpeningDetailMatch { get; set; }
        public static List<long> BookmarkedMatchs { get; set; }
        public static Dictionary<long, List<TrackOverUnderBeforeLiveItem>> TrackOverUnderBeforeLive { get; set; }
        public static Dictionary<int, List<KeyValuePair<DateTime, int>>> FirstShowIndex { get; set; } // <Day, List<firstShow, order>>

        public static Dictionary<long, List<List<List<int>>>> MatchMaxBetNonRequest { get; set; }
        public static Dictionary<long, List<List<int>>> MatchOverUnderMoneyLine { get; set; }

        public static Dictionary<long, List<List<List<int>>>> MatchMaxBetRequest { get; set; } // matchid, livePeriod, hdpIndex, minute
        public static Dictionary<long, List<List<Product>>> Product1x2History { get; set; } // matchid, minute

        public static Dictionary<long, List<HandicapLifeTimeHistoryV3>> ProductHandicapFulltimeHistory { get; set; } // <MatchId, <ProductId, [history1, history2, history3,...]>>
        public static Dictionary<long, List<HandicapLifeTimeHistoryV3>> ProductHandicapFirstHalfHistory { get; set; } // <MatchId, <ProductId, [history1, history2, history3,...]>>

        public static Dictionary<long, Alert> Alert_Wd1 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd2 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd3 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd4 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd5 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd6 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd7 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd8 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd9 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd10 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd11 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd12 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd13 { get; set; }

        public static Dictionary<long, Alert> Alert_Wd14 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd15 { get; set; }

        public static Dictionary<long, Alert> Alert_Wd16 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd17 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd18 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd19 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd20 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd21 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd22 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd23 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd24 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd25 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd26 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd27 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd28 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd29 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd30 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd31 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd32 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd33 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd34 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd35 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd36 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd37 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd38 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd39 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd40 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd41 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd42 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd43 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd44 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd45 { get; set; }
        public static Dictionary<long, Alert> Alert_Wd46 { get; set; }

        public static Dictionary<string, Dictionary<int, string>> PropertyMapping { get; set; } // <name , Dictionary_Of_Properties>

        public static long MatchIdNeedGetMaxBetRequest { get; set; }

        public static List<Authorization> Authorizations
        {
            get
            {
                return new List<Authorization>() { 

                };
            }
        }
    }
}
