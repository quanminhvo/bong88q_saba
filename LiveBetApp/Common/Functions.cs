using AutoMapper;
using LiveBetApp.Models.DataModels;
using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LiveBetApp.Common
{
    public static class Functions
    {
        public static void InitDataStoreOneTime()
        {
            DataStore.MatchIdNeedGetMaxBetRequest = 0;
            DataStore.HourToBackUp = 3;
            DataStore.MinuteToBackUp = 20;
            DataStore.Backuptimes = new List<string>();
            DataStore.Config = Common.Config.GetConfigModel();
            DataStore.FilterDate = DateTime.Now.Date;
            //DataStore.MatchFilterDateTimeFrom = DateTime.Now.Date;
            //DataStore.MatchFilterDateTimeTo = DateTime.Now.Date.AddDays(1);
            DataStore.MatchFilterDateTimeChecked = true;
            DataStore.ShowPlaningMatchsChecked = false;
            DataStore.ShowFinishedMatchsChecked = false;

            DataStore.ShowRunningMatchsH1Checked = true;
            DataStore.ShowRunningMatchsH2Checked = true;
            DataStore.ShowRunningMatchsHtChecked = true;

            DataStore.ShowNoShowMatchsChecked = false;
            DataStore.MainformKeepRefreshingChecked = true;
            DataStore.ShowCornersChecked = false;
            DataStore.RunAutobet = false;
            DataStore.LSteamSearch = Enums.LiveSteamSearch.Live;
            DataStore.RFinishedSearch = Enums.RunningFinishedSearch.Running;
            DataStore.AutobetStake = 3;
            DataStore.LeagueFilter = "";
            DataStore.MaxHdp = 10000;
            DataStore.MinHdp = 0;
            DataStore.OuMoneyLine = 0;
            DataStore.QuickBetMinute = 0;
            DataStore.Theme = Enums.VisualThemes.PC;
            DataStore.LastTick = DateTime.MinValue;

            DataStore.AlertSetting = new List<bool>();
            for (int i = 0; i <= 20; i++)
            {
                DataStore.AlertSetting.Add(true);
            }
        }
        public static void InitDataStore()
        {
            DataStore.ProductIdsOuFtOfMatchHistoryLive = new Dictionary<long, List<ProductIdsHistory>>();
            DataStore.ProductIdsOuFtOfMatchHistoryBeforeLive = new Dictionary<long, List<ProductIdsHistory>>();

            DataStore.ProductIdsOuFhOfMatchHistoryBeforeLive = new Dictionary<long, List<ProductIdsHistory>>();
            DataStore.ProductIdsOuFhOfMatchHistoryLive = new Dictionary<long, List<ProductIdsHistory>>();

            DataStore.ProductIdsHdpFhOfMatchHistoryBeforeLive = new Dictionary<long, List<ProductIdsHistory>>();
            DataStore.ProductIdsHdpFhOfMatchHistoryLive = new Dictionary<long, List<ProductIdsHistory>>();

            DataStore.ProductIdsHdpFtOfMatchHistoryBeforeLive = new Dictionary<long, List<ProductIdsHistory>>();
            DataStore.ProductIdsHdpFtOfMatchHistoryLive = new Dictionary<long, List<ProductIdsHistory>>();

            DataStore.ProductIdsOfMatch = new Dictionary<long, List<ProductIdLog>>();
            DataStore.AppStartLog = new List<long>();
            DataStore.Leagues = new Dictionary<long, string>();
            DataStore.Matchs = new Dictionary<long, Models.DataModels.Match>();
            DataStore.Products = new Dictionary<long, Models.DataModels.Product>();
            DataStore.ProductStatus = new Dictionary<long, List<List<ProductStatusLog>>>();
            DataStore.MatchHasProductStatus = new Dictionary<long, DateTime>();
            DataStore.VeryFirstProducts = new Dictionary<long, List<Product>>();

            DataStore.OverUnderScoreTimesBeforeLive = new Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV3Item>>>();
            DataStore.OverUnderScoreTimesFirstHalfBeforeLive = new Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV3Item>>>();
            DataStore.HandicapScoreTimesBeforeLive = new Dictionary<long, Dictionary<int, List<HandicapLifeTimeHistoryV2>>>();
            DataStore.HandicapScoreTimesFirstHalfBeforeLive = new Dictionary<long, Dictionary<int, List<HandicapLifeTimeHistoryV2>>>();

            DataStore.UnderScoreTimes = new Dictionary<long, Dictionary<int, List<string>>>();
            DataStore.UnderScoreTimesFirstHalf = new Dictionary<long, Dictionary<int, List<string>>>();
            DataStore.UnderScoreHalfTimes = new Dictionary<long, Dictionary<int, List<string>>>();

            DataStore.OverUnderScoreTimes = new Dictionary<long, Dictionary<int, List<string>>>();
            DataStore.OverUnderScoreTimesFreq = new Dictionary<long, Dictionary<int, List<int>>>();
            DataStore.OverUnderScoreTimesFreqFh = new Dictionary<long, Dictionary<int, List<int>>>();

            DataStore.OverUnderScoreTimesFreqBeforeLive = new Dictionary<long, Dictionary<int, List<int>>>();
            DataStore.OverUnderScoreTimesFreqFhBeforeLive = new Dictionary<long, Dictionary<int, List<int>>>();
            DataStore.OverUnderScoreTimesFreqHalfTime = new Dictionary<long, Dictionary<int, List<int>>>();

            DataStore.HandicapFreqBeforeLive = new Dictionary<long, Dictionary<int, List<int>>>();
            DataStore.HandicapFreqFhBeforeLive = new Dictionary<long, Dictionary<int, List<int>>>();
            DataStore.HandicapFreqHalfTime = new Dictionary<long, Dictionary<int, List<int>>>();

            DataStore.OverUnderScoreHalfTimes = new Dictionary<long, Dictionary<int, List<string>>>();
            DataStore.OverUnderScoreTimesFirstHalf = new Dictionary<long, Dictionary<int, List<string>>>();
            DataStore.HandicapScoreTimes = new Dictionary<long, Dictionary<long, List<HandicapLifeTimeHistory>>>();
            DataStore.FinishPriceOverUnderScoreTimes = new Dictionary<long, Dictionary<int, OverUnderScoreTimesV2Item>>();
            DataStore.GoalHistories = new Dictionary<long, List<GoalHistory>>();
            DataStore.CardHistories = new Dictionary<long, List<CardHistory>>();
            DataStore.PenHistories = new Dictionary<long, List<PenHistory>>();
            DataStore.GlobalShowtimeHistories = new Dictionary<long, List<GlobalShowtimeHistory>>();
            DataStore.MatchCodeHistories = new Dictionary<long, List<MatchCodeHistory>>();
            DataStore.TimmingBetOverUnder = new List<TimmingBetOverUnder>();
            DataStore.FinishedTimmingBetOverUnder = new List<TimmingBetOverUnder>();
            DataStore.BetByHdpPrice = new List<BetByHdpPrice>();
            DataStore.FinishedBetByHdpPrice = new List<BetByHdpPrice>();
            DataStore.BetByHandicapHdpPrice = new List<BetByHdpPrice>();
            DataStore.FinishedBetByHandicapHdpPrice = new List<BetByHdpPrice>();
            DataStore.BetByTimming = new List<BetByTimming>();
            DataStore.FinishedBetByTimming = new List<BetByTimming>();
            DataStore.BetByHdpPriceIuoo = new List<BetByHdpPrice>();
            DataStore.FinishedBetByHdpPriceIuoo = new List<BetByHdpPrice>();
            DataStore.BetAfterGoodPrice = new List<BetAfterGoodPrice>();
            DataStore.FinishedBetAfterGoodPrice = new List<BetAfterGoodPrice>();
            DataStore.BetByHdpClose = new List<BetByHdpClose>();
            DataStore.FinishedBetByHdpClose = new List<BetByHdpClose>();
            DataStore.BetByQuick = new List<BetByQuick>();
            DataStore.FinishedBetByQuick = new List<BetByQuick>();
            DataStore.BetDirect = new List<BetDirect>();
            DataStore.FinishedBetDirect = new List<BetDirect>();
            DataStore.OverUnderLastBoundariesOfMatch = new Dictionary<long, Dictionary<int, int>>();
            DataStore.OverUnderScoreHalftime = new Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV3Item>>>();
            DataStore.OverUnderScoreTimesV2 = new Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>>();
            DataStore.OverUnderScoreTimesV3 = new Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>>();
            DataStore.OverUnderScoreTimesV4_60 = new Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>>();
            DataStore.OverUnderScoreTimesFirstHalfV2 = new Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>>();
            DataStore.OverUnderScoreTimesFirstHalfV3 = new Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>>();
            DataStore.BetFirstHalfByFulltime = new List<BetFirstHalfByFulltime>();
            DataStore.FinishedBetFirstHalfByFulltime = new List<BetFirstHalfByFulltime>();
            DataStore.MatchIuooValue_1 = new Dictionary<long, Dictionary<int, IuooValue>>();
            DataStore.MatchIuooValue_2 = new Dictionary<long, Dictionary<int, IuooValue>>();
            DataStore.InUnderOutOvers = new Dictionary<long, InUnderOutOver>();

            if (DataStore.Blacklist == null)
            {
                DataStore.Blacklist = new List<BlackList>();
            }

            DataStore.MatchIuooAlert = new Dictionary<long, MatchIuooAlertItem>();
            DataStore.BookmarkedMatchs = new List<long>();
            DataStore.TrackOverUnderBeforeLive = new Dictionary<long, List<TrackOverUnderBeforeLiveItem>>();
            DataStore.FirstShowIndex = new Dictionary<int, List<KeyValuePair<DateTime, int>>>();
            DataStore.MatchMaxBetNonRequest = new Dictionary<long, List<List<List<int>>>>();
            DataStore.Product1x2History = new Dictionary<long, List<List<Product>>>();
            DataStore.MatchMaxBetRequest = new Dictionary<long, List<List<List<int>>>>();
            DataStore.Excel4th = new Dictionary<long, Dictionary<int, OverUnderSummary>>();
            DataStore.MatchOverUnderMoneyLine = new Dictionary<long, List<List<int>>>();
            DataStore.Alert_Wd1 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd2 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd3 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd4 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd5 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd6 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd7 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd8 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd9 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd10 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd11 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd12 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd13 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd14 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd15 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd16 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd17 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd18 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd19 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd20 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd21 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd22 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd23 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd24 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd25 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd26 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd27 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd28 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd29 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd30 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd31 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd32 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd33 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd34 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd35 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd36 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd37 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd38 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd39 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd40 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd41 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd42 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd43 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd44 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd45 = new Dictionary<long, Alert>();
            DataStore.Alert_Wd46 = new Dictionary<long, Alert>();
            DataStore.PropertyMapping = new Dictionary<string, Dictionary<int, string>>();
            DataStore.OpeningDetailMatch = new List<long>();
            DataStore.HandicapFhScoreTimes = new Dictionary<long, Dictionary<long, List<HandicapLifeTimeHistory>>>();
        }

        public static void InitAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.DataModels.Match, Models.ViewModels.Match>()
                    .ForMember(dest => dest.HasStreaming, opt => opt.MapFrom(src => 
                        src.HasStreaming && src.FirstTimeHasStreaming != DateTime.MinValue 
                        ? ((int)src.FirstTimeHasStreaming.Subtract(src.GlobalShowtime).TotalMinutes).ToString()
                        : "")
                    )
                    //.ForMember(dest => dest.MinMaxFt, opt => opt.MapFrom(src =>
                    //    (src.MinHdpFtCurrent == 10000 ? "__" : src.MinHdpFtCurrent.ToString())
                    //    + " " + ((src.MaxHdpFtCurrent == 0 || src.MaxHdpFtCurrent == src.MinHdpFtCurrent) ? "__" : src.MaxHdpFtCurrent.ToString())
                    //    )
                    //)
                    //.ForMember(dest => dest.MinMaxFh, opt => opt.MapFrom(src =>
                    //    (src.MinHdpFhCurrent == 10000 ? "__" : src.MinHdpFhCurrent.ToString())
                    //    + " " + ((src.MaxHdpFhCurrent == 0 || src.MaxHdpFhCurrent == src.MinHdpFhCurrent) ? "__" : src.MaxHdpFhCurrent.ToString())
                    //    )
                    //)
                    //.ForMember(dest => dest.BetTeam, opt => opt.MapFrom(src => src.IsOver ? "Home" : "Away")
                    ;
                //HasInsert1x2FtBeforelive

                cfg.CreateMap<Models.DataModels.Match, Models.ViewModels.MatchMaxBet>()
                    .ForMember(dest => dest.HasStreaming, opt => opt.MapFrom(src =>
                        src.HasStreaming && src.FirstTimeHasStreaming != DateTime.MinValue
                        ? ((int)src.FirstTimeHasStreaming.Subtract(src.GlobalShowtime).TotalMinutes).ToString()
                        : "")
                    );

                cfg.CreateMap<Models.DataModels.Match, Models.ViewModels.MatchMaxBetFull>()
                    .ForMember(dest => dest.HasStreaming, opt => opt.MapFrom(src =>
                        src.HasStreaming && src.FirstTimeHasStreaming != DateTime.MinValue
                        ? ((int)src.FirstTimeHasStreaming.Subtract(src.GlobalShowtime).TotalMinutes).ToString()
                        : "")
                    );

                cfg.CreateMap<Models.DataModels.Match, Models.ViewModels.Match1x2Product>()
                    .ForMember(dest => dest.HasStreaming, opt => opt.MapFrom(src =>
                        src.HasStreaming && src.FirstTimeHasStreaming != DateTime.MinValue
                        ? ((int)src.FirstTimeHasStreaming.Subtract(src.GlobalShowtime).TotalMinutes).ToString()
                        : "")
                    );


                cfg.CreateMap<Models.ViewModels.Match, Models.ViewModels.MatchExport>();

                cfg.CreateMap<Models.DataModels.Match, Models.DataModels.GoalHistory>();
                cfg.CreateMap<Models.DataModels.Match, Models.DataModels.CardHistory>();
                cfg.CreateMap<Models.DataModels.Match, Models.ViewModels.AlertMatch>();
                cfg.CreateMap<Models.DataModels.Match, Models.ViewModels.BookmarkedMatch>();
                cfg.CreateMap<Models.DataModels.ProductStatusLog, Models.ViewModels.ProductStatusLog>()
                    .ForMember(dest => dest.TypeStr, opt => opt.MapFrom(src => 
                        src.Type == Enums.BetType.FullTime1x2 ? "FT 1x2" :
                        src.Type == Enums.BetType.FullTimeHandicap ? "FT Handicap" :
                        src.Type == Enums.BetType.FullTimeOverUnder ? "FT OverUnder" :
                        src.Type.ToString()
                        )
                    );

                cfg.CreateMap<Models.DataModels.BetByHdpPrice, Models.ViewModels.BetByHdpPriceViewModel>()
                    .ForMember(dest => dest.BetTeam, opt => opt.MapFrom(src => src.IsOver ? "Home" : "Away")
                );


            });
        }

        public static DateTime GetDateTime(long milisecond)
        {
            var timeZoneSpan = TimeZoneInfo.Local.GetUtcOffset(new DateTime(2006, 6, 1));

            return new DateTime(1970, 1, 1)
                .AddMilliseconds(milisecond)
                .AddSeconds(timeZoneSpan.TotalSeconds);
        }

        public static string GetDimDatetime(DateTime dt)
        {
            return dt.ToString("yyyyMMddHHmmss");
        }

        public static void WriteExceptionLog(Exception ex)
        {
            return;
            //if (ex.Message != "Thread was being aborted.")
            //{
            //    Common.Functions.WriteJsonObjectData<Exception>(
            //        Common.Constants.JsonErrorLogfolder + Common.Functions.GetDimDatetime(DateTime.Now),
            //        ex
            //    );
            //}
        }

        public static void SetInterval(Action action, int milisecond)
        {
            try
            {
                Thread.Sleep(milisecond);
                action();
                SetInterval(action, milisecond);
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static void CancelGoal(long matchId)
        {
            Match match;
            if (!DataStore.Matchs.TryGetValue(matchId, out match)) return;

            int countGoal = DataStore.GoalHistories[matchId].Count;
            if (countGoal > 0)
            {
                DataStore.GoalHistories[matchId].RemoveAt(countGoal - 1);
            }
        }

        public static void DeleteMatch(long matchId)
        {
            Match match;
            if (!DataStore.Matchs.TryGetValue(matchId, out match)) return;
            DataStore.Matchs[matchId].DeleteTime = DateTime.Now;
            DataStore.Matchs[matchId].LivePeriod = -1;
        }

        public static void DeleteProduct (long productId)
        {
            DataStore.Products.Remove(productId);
        }

        public static void WriteJsonObjectData<T>(string path, T obj)
        {
            try
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, obj);
                }
            }
            catch
            {

            }

        }

        public static T ReadJsonObjectData<T>(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    string jsonString = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
                catch
                {
                    return (T)Activator.CreateInstance(typeof(T));
                }
            }
            else
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
        }

        public static void BackUpDatastore(DateTime dt)
        {
            if (dt.Hour <= DataStore.HourToBackUp
                && dt.Minute <= DataStore.MinuteToBackUp)
            {
                dt = dt.AddDays(-1);
            }
            
            string dtStr = dt.ToString("yyyyMMdd");

            Directory.CreateDirectory("Datastore\\" + dtStr);

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_AppStartLog.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.AppStartLog
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Leagues.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Leagues
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Matchs.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Matchs
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreHalfTimes.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreHalfTimes
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimes.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimes
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Products.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Products
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_GoalHistories.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.GoalHistories
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesFirstHalf.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesFirstHalf
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_HandicapScoreTimes.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.HandicapScoreTimes
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderLastBoundariesOfMatch.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderLastBoundariesOfMatch
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_FinishedBetByHdpPrice.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.FinishedBetByHdpPrice
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_BetByHdpPrice.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.BetByHdpPrice
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_FinishedBetByHandicapHdpPrice.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.FinishedBetByHandicapHdpPrice
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_BetByHandicapHdpPrice.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.BetByHandicapHdpPrice
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_FinishedTimmingBetOverUnder.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.FinishedTimmingBetOverUnder
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesV2.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesV2
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesV3.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesV3
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_BetFirstHalfByFulltime.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.BetFirstHalfByFulltime
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_FinishedBetFirstHalfByFulltime.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.FinishedBetFirstHalfByFulltime
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_UnderScoreTimes.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.UnderScoreTimes
            );

            
            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_UnderScoreHalfTimes.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.UnderScoreHalfTimes
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_FinishedBetByHdpPriceIuoo.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.FinishedBetByHdpPriceIuoo
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_BetByHdpPriceIuoo.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.BetByHdpPriceIuoo
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_MatchIuooValue_1.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.MatchIuooValue_1
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_MatchIuooValue_2.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.MatchIuooValue_2
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_InUnderOutOvers.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.InUnderOutOvers
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_BookmarkedMatchs.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.BookmarkedMatchs
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_FinishPriceOverUnderScoreTimes.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.FinishPriceOverUnderScoreTimes
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_BetByHdpClose.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.BetByHdpClose
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_FinishedBetByHdpClose.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.FinishedBetByHdpClose
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_BetByTimming.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.BetByTimming
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_FinishedBetByTimming.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.FinishedBetByTimming
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_MatchIuooAlert.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.MatchIuooAlert
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesFirstHalfBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesFirstHalfBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_HandicapScoreTimesBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.HandicapScoreTimesBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_HandicapScoreTimesFirstHalfBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.HandicapScoreTimesFirstHalfBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_VeryFirstProducts.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.VeryFirstProducts
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesFirstHalfV2.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesFirstHalfV2
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesFirstHalfV3.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesFirstHalfV3
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreHalftime.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreHalftime
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_BetAfterGoodPrice.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.BetAfterGoodPrice
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_FinishedBetAfterGoodPrice.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.FinishedBetAfterGoodPrice
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Excel4th.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Excel4th
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_ProductIdsOfMatch.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.ProductIdsOfMatch
            );
            
            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_ProductIdsOuFtOfMatchHistoryBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.ProductIdsOuFtOfMatchHistoryBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_ProductIdsOuFtOfMatchHistoryLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.ProductIdsOuFtOfMatchHistoryLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_ProductIdsOuFhOfMatchHistoryBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.ProductIdsOuFhOfMatchHistoryBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_ProductIdsOuFhOfMatchHistoryLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.ProductIdsOuFhOfMatchHistoryLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_ProductIdsHdpFhOfMatchHistoryBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.ProductIdsHdpFhOfMatchHistoryBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_ProductIdsHdpFhOfMatchHistoryLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.ProductIdsHdpFhOfMatchHistoryLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_ProductIdsHdpFtOfMatchHistoryBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.ProductIdsHdpFtOfMatchHistoryBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_ProductIdsHdpFtOfMatchHistoryLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.ProductIdsHdpFtOfMatchHistoryLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_PropertyMapping.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.PropertyMapping
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_TrackOverUnderBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.TrackOverUnderBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Alert_Wd20.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Alert_Wd20
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Alert_Wd32.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Alert_Wd32
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Alert_Wd34.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Alert_Wd34
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesV4_60.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesV4_60
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_ProductStatus.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.ProductStatus
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_UnderScoreTimesFirstHalf.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.UnderScoreTimesFirstHalf
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Alert_Wd26.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Alert_Wd26
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_HandicapFhScoreTimes.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.HandicapFhScoreTimes
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_CardHistories.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.CardHistories
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_GlobalShowtimeHistories.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.GlobalShowtimeHistories
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesFreq.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesFreq
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesFreqFh.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesFreqFh
            );


            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesFreqBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesFreqBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesFreqFhBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesFreqFhBeforeLive
            );


            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_OverUnderScoreTimesFreqHalfTime.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.OverUnderScoreTimesFreqHalfTime
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_HandicapFreqBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.HandicapFreqBeforeLive
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_HandicapFreqFhBeforeLive.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.HandicapFreqFhBeforeLive
            );


            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_HandicapFreqHalfTime.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.HandicapFreqHalfTime
            );


            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_FirstShowIndex.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.FirstShowIndex
            );
            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Alert_Wd35.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Alert_Wd35
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Alert_Wd36.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Alert_Wd36
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Alert_Wd37.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Alert_Wd37
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_Alert_Wd38.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.Alert_Wd38
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_MatchCodeHistories.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.MatchCodeHistories
            );

            //Common.Functions.WriteJsonObjectData(
            //    Constants.BackUpPath_MatchMaxBetNonRequest.Replace("\\", "\\" + dtStr + "\\"),
            //    DataStore.MatchMaxBetNonRequest
            //);

            //Common.Functions.WriteJsonObjectData(
            //    Constants.BackUpPath_MatchMaxBetRequest.Replace("\\", "\\" + dtStr + "\\"),
            //    DataStore.MatchMaxBetRequest
            //);

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_MatchOverUnderMoneyLine.Replace("\\", "\\" + dtStr + "\\"),
                DataStore.MatchOverUnderMoneyLine
            );

            Common.Functions.WriteJsonObjectData(
                Constants.BackUpPath_BlackList,
                DataStore.Blacklist
            );

        }

        public static void RestoreDatastore(DateTime dt)
        {
            string dtStr = dt.ToString("yyyyMMdd");

            DataStore.AppStartLog = Common.Functions.ReadJsonObjectData<
                List<long>>(
                Constants.BackUpPath_AppStartLog.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.Leagues = Common.Functions.ReadJsonObjectData<
                Dictionary<long, string>>(
                Constants.BackUpPath_Leagues.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.Matchs = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Models.DataModels.Match>>(
                Constants.BackUpPath_Matchs.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreHalfTimes = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<string>>>>(
                Constants.BackUpPath_OverUnderScoreHalfTimes
            );

            DataStore.OverUnderScoreTimes = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<string>>>>(
                Constants.BackUpPath_OverUnderScoreTimes.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.GoalHistories = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<GoalHistory>>>(
                Constants.BackUpPath_GoalHistories.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesFirstHalf = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<string>>>>(
                Constants.BackUpPath_OverUnderScoreTimesFirstHalf.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.HandicapScoreTimes = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<long, List<HandicapLifeTimeHistory>>>>(
                Constants.BackUpPath_HandicapScoreTimes.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderLastBoundariesOfMatch = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, int>>>(
                Constants.BackUpPath_OverUnderLastBoundariesOfMatch.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.FinishedBetByHdpPrice = Common.Functions.ReadJsonObjectData<
                List<BetByHdpPrice>>(
                Constants.BackUpPath_FinishedBetByHdpPrice.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.BetByHdpPrice = Common.Functions.ReadJsonObjectData<
                List<BetByHdpPrice>>(
                Constants.BackUpPath_BetByHdpPrice.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.BetByHandicapHdpPrice = Common.Functions.ReadJsonObjectData<
                List<BetByHdpPrice>>(
                Constants.BackUpPath_BetByHandicapHdpPrice.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.FinishedBetByHandicapHdpPrice = Common.Functions.ReadJsonObjectData<
                List<BetByHdpPrice>>(
                Constants.BackUpPath_FinishedBetByHandicapHdpPrice.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.FinishedTimmingBetOverUnder = Common.Functions.ReadJsonObjectData<
                List<TimmingBetOverUnder>>(
                Constants.BackUpPath_FinishedTimmingBetOverUnder.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesV2 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>>>(
                Constants.BackUpPath_OverUnderScoreTimesV2.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesV3 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>>>(
                Constants.BackUpPath_OverUnderScoreTimesV3.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesFirstHalfV2 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>>>(
                Constants.BackUpPath_OverUnderScoreTimesFirstHalfV2.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesFirstHalfV3 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>>>(
                Constants.BackUpPath_OverUnderScoreTimesFirstHalfV3.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.BetFirstHalfByFulltime = Common.Functions.ReadJsonObjectData<
                List<BetFirstHalfByFulltime>>(
                Constants.BackUpPath_BetFirstHalfByFulltime.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.FinishedBetFirstHalfByFulltime = Common.Functions.ReadJsonObjectData<
                List<BetFirstHalfByFulltime>>(
                Constants.BackUpPath_FinishedBetFirstHalfByFulltime.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.UnderScoreTimes = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<string>>>>(
                Constants.BackUpPath_UnderScoreTimes.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.UnderScoreHalfTimes = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<string>>>>(
                Constants.BackUpPath_UnderScoreHalfTimes.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.FinishPriceOverUnderScoreTimes = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, OverUnderScoreTimesV2Item>>>(
                Constants.BackUpPath_FinishPriceOverUnderScoreTimes.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.FinishedBetByHdpPriceIuoo = Common.Functions.ReadJsonObjectData<
                List<BetByHdpPrice>>(
                Constants.BackUpPath_FinishedBetByHdpPriceIuoo.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.BetByHdpPriceIuoo = Common.Functions.ReadJsonObjectData<
                List<BetByHdpPrice>>(
                Constants.BackUpPath_BetByHdpPriceIuoo.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.MatchIuooValue_1 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, IuooValue>>>(
                Constants.BackUpPath_MatchIuooValue_1.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.MatchIuooValue_2 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, IuooValue>>>(
                Constants.BackUpPath_MatchIuooValue_2.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.InUnderOutOvers = Common.Functions.ReadJsonObjectData<
                Dictionary<long, InUnderOutOver>>(
                Constants.BackUpPath_InUnderOutOvers.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.BookmarkedMatchs = Common.Functions.ReadJsonObjectData<
                List<long>>(
                Constants.BackUpPath_BookmarkedMatchs.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.BetByHdpClose = Common.Functions.ReadJsonObjectData<
                List<BetByHdpClose>>(
                Constants.BackUpPath_BetByHdpClose.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.FinishedBetByHdpClose = Common.Functions.ReadJsonObjectData<
                List<BetByHdpClose>>(
                Constants.BackUpPath_FinishedBetByHdpClose.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.BetByTimming = Common.Functions.ReadJsonObjectData<
                List<BetByTimming>>(
                Constants.BackUpPath_BetByTimming.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.FinishedBetByTimming = Common.Functions.ReadJsonObjectData<
                List<BetByTimming>>(
                Constants.BackUpPath_FinishedBetByTimming.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.MatchIuooAlert = Common.Functions.ReadJsonObjectData<
                Dictionary<long, MatchIuooAlertItem>>(
                Constants.BackUpPath_MatchIuooAlert.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV3Item>>>>(
                Constants.BackUpPath_OverUnderScoreTimesBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );
            DataStore.OverUnderScoreTimesFirstHalfBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV3Item>>>>(
                Constants.BackUpPath_OverUnderScoreTimesFirstHalfBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.HandicapScoreTimesBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<HandicapLifeTimeHistoryV2>>>>(
                Constants.BackUpPath_HandicapScoreTimesBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.HandicapScoreTimesFirstHalfBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<HandicapLifeTimeHistoryV2>>>>(
                Constants.BackUpPath_HandicapScoreTimesFirstHalfBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.VeryFirstProducts = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<Product>>>(
                Constants.BackUpPath_VeryFirstProducts.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreHalftime = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV3Item>>>>(
                Constants.BackUpPath_OverUnderScoreHalftime.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.BetAfterGoodPrice = Common.Functions.ReadJsonObjectData<
                List<BetAfterGoodPrice>>(
                Constants.BackUpPath_BetAfterGoodPrice.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.FinishedBetAfterGoodPrice = Common.Functions.ReadJsonObjectData<
                List<BetAfterGoodPrice>>(
                Constants.BackUpPath_FinishedBetAfterGoodPrice.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.Excel4th = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, OverUnderSummary>>>(
                Constants.BackUpPath_Excel4th.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.ProductIdsOfMatch = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<ProductIdLog>>>(
                Constants.BackUpPath_ProductIdsOfMatch.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.ProductIdsOuFtOfMatchHistoryLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<ProductIdsHistory>>>(
                Constants.BackUpPath_ProductIdsOuFtOfMatchHistoryLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.ProductIdsOuFtOfMatchHistoryBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<ProductIdsHistory>>>(
                Constants.BackUpPath_ProductIdsOuFtOfMatchHistoryBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );


            DataStore.ProductIdsOuFhOfMatchHistoryBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<ProductIdsHistory>>>(
                Constants.BackUpPath_ProductIdsOuFhOfMatchHistoryBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.ProductIdsOuFhOfMatchHistoryLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<ProductIdsHistory>>>(
                Constants.BackUpPath_ProductIdsOuFhOfMatchHistoryLive.Replace("\\", "\\" + dtStr + "\\")
            );


            DataStore.ProductIdsHdpFhOfMatchHistoryBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<ProductIdsHistory>>>(
                Constants.BackUpPath_ProductIdsHdpFhOfMatchHistoryBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.ProductIdsHdpFhOfMatchHistoryLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<ProductIdsHistory>>>(
                Constants.BackUpPath_ProductIdsHdpFhOfMatchHistoryLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.ProductIdsHdpFtOfMatchHistoryBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<ProductIdsHistory>>>(
                Constants.BackUpPath_ProductIdsHdpFtOfMatchHistoryBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.ProductIdsHdpFtOfMatchHistoryLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<ProductIdsHistory>>>(
                Constants.BackUpPath_ProductIdsHdpFtOfMatchHistoryLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.TrackOverUnderBeforeLive = Common.Functions.ReadJsonObjectData<
               Dictionary<long, List<TrackOverUnderBeforeLiveItem>>>(
                Constants.BackUpPath_TrackOverUnderBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.Alert_Wd20 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Alert>>(
                Constants.BackUpPath_Alert_Wd20.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.Alert_Wd32 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Alert>>(
                Constants.BackUpPath_Alert_Wd32.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.Alert_Wd34 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Alert>>(
                Constants.BackUpPath_Alert_Wd34.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesV4_60 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>>>(
                Constants.BackUpPath_OverUnderScoreTimesV4_60.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.ProductStatus = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<List<ProductStatusLog>>>>(
                Constants.BackUpPath_ProductStatus.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.UnderScoreTimesFirstHalf = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<string>>>>(
                Constants.BackUpPath_UnderScoreTimesFirstHalf.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.Alert_Wd26 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Alert>>(
                Constants.BackUpPath_Alert_Wd26.Replace("\\", "\\" + dtStr + "\\")
            );
            DataStore.Alert_Wd36 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Alert>>(
                Constants.BackUpPath_Alert_Wd36.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.Alert_Wd37 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Alert>>(
                Constants.BackUpPath_Alert_Wd37.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.Alert_Wd38 = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Alert>>(
                Constants.BackUpPath_Alert_Wd38.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.HandicapFhScoreTimes = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<long, List<HandicapLifeTimeHistory>>>>(
                Constants.BackUpPath_HandicapFhScoreTimes.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.CardHistories = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<CardHistory>>>(
                Constants.BackUpPath_CardHistories.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.GlobalShowtimeHistories = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<GlobalShowtimeHistory>>>(
                Constants.BackUpPath_GlobalShowtimeHistories.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesFreq = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<int>>>>(
                Constants.BackUpPath_OverUnderScoreTimesFreq.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesFreqFh = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<int>>>>(
                Constants.BackUpPath_OverUnderScoreTimesFreqFh.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesFreqBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<int>>>>(
                Constants.BackUpPath_OverUnderScoreTimesFreqBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesFreqFhBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<int>>>>(
                Constants.BackUpPath_OverUnderScoreTimesFreqFhBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.OverUnderScoreTimesFreqHalfTime = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<int>>>>(
                Constants.BackUpPath_OverUnderScoreTimesFreqHalfTime.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.HandicapFreqBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<int>>>>(
                Constants.BackUpPath_HandicapFreqBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.HandicapFreqFhBeforeLive = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<int>>>>(
                Constants.BackUpPath_HandicapFreqFhBeforeLive.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.HandicapFreqHalfTime = Common.Functions.ReadJsonObjectData<
                Dictionary<long, Dictionary<int, List<int>>>>(
                Constants.BackUpPath_HandicapFreqHalfTime.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.FirstShowIndex = Common.Functions.ReadJsonObjectData<
                Dictionary<int, List<KeyValuePair<DateTime, int>>>>(
                Constants.BackUpPath_FirstShowIndex.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.MatchCodeHistories = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<MatchCodeHistory>>>(
                Constants.BackUpPath_MatchCodeHistories.Replace("\\", "\\" + dtStr + "\\")
            );

            //DataStore.MatchMaxBetNonRequest = Common.Functions.ReadJsonObjectData<
            //    Dictionary<long, List<List<List<int>>>>>(
            //    Constants.BackUpPath_MatchMaxBetNonRequest.Replace("\\", "\\" + dtStr + "\\")
            //);

            //DataStore.MatchMaxBetRequest = Common.Functions.ReadJsonObjectData<
            //    Dictionary<long, List<List<List<int>>>>>(
            //    Constants.BackUpPath_MatchMaxBetRequest.Replace("\\", "\\" + dtStr + "\\")
            //);


            DataStore.MatchOverUnderMoneyLine = Common.Functions.ReadJsonObjectData<
                Dictionary<long, List<List<int>>>>(
                Constants.BackUpPath_MatchOverUnderMoneyLine.Replace("\\", "\\" + dtStr + "\\")
            );

            DataStore.Blacklist = Common.Functions.ReadJsonObjectData<
                List<BlackList>>(
                Constants.BackUpPath_BlackList
            );


            List<Match> matchs = DataStore.Matchs.Values.ToList();
            for (int i = 0; i < matchs.Count; i++)
            {
                double minuteSpan = DateTime.Now.Subtract(matchs[i].GlobalShowtime).TotalMinutes;
                if ((matchs[i].TimeSpanFromStart.TotalMinutes > 50 && matchs[i].LivePeriod == 2) || minuteSpan > 150)
                {
                    DeleteMatch(matchs[i].MatchId);
                }
            }

        }

        public static ProcessBetModel ConstructProcessBetOverUnderModel(Product product, Match match, int stake, bool isOver, bool isFulltime)
        {
            //ProcessBetModel result = new ProcessBetModel()
            //{
            //    sportname = "Bóng đá",
            //    gamename = "",
            //    bettypename = isFulltime ? "Tài/Xỉu" : "Hiệp 1 -  Tài/Xỉu",
            //    ChoiceValue = isOver ? "Tài" : "Xỉu",
            //    Line = product.Hdp1.ToString(),
            //    displayHDP = product.Hdp1.ToString(),
            //    odds = isOver ? product.Odds1a.ToString() : product.Odds2a.ToString(),
            //    home = match.Home,
            //    away = match.Away,
            //    league = match.League,
            //    IsLive = "true",
            //    ProgramID = "",
            //    RaceNum = "0",
            //    Runner = "0",
            //    PoolType = "1",
            //    imgurl = "",
            //    BetID = "",
            //    type = "OU",
            //    bettype = isFulltime ? "3" : "8",
            //    oddsid = product.OddsId.ToString(),
            //    Hscore = match.LiveHomeScore.ToString(),
            //    Ascore = match.LiveAwayScore.ToString(),
            //    Matchid = match.MatchId.ToString(),
            //    betteam = isOver ? "h" : "a",
            //    stake = stake < 3 ? "3" : stake.ToString(),
            //    gameid = "1",
            //    MRPercentage = "",
            //    OddsInfo = "",
            //    LiveInfo = "[" + match.LiveHomeScore.ToString() + "-" + match.LiveAwayScore.ToString() + "]",
            //    AcceptBetterOdds = "true",
            //    AutoAcceptSec = "",
            //    ParentTypeId = isFulltime ? "3" : "8",
            //    showLiveScore = "true",
            //    colorHomeTeam = "",
            //    colorAwayTeam = "",
            //    hasParlay = match.McParlay > 0 ? "true" : "false",
            //    matchcode = "",
            //    isQuickBet = "true",
            //    UseBonus = "0",
            //    kickofftime = match.KickoffTimeNumber.ToString(),
            //    ShowTime = match.ShowTime,
            //    oddsStatus = "",
            //    min = "3",
            //    max = product.MaxBet.ToString(),
            //};

            //return result;
            return null;
        }

        public static ProcessBetModel ConstructProcessBetOverUnderModelV2(Product product, Match match, int stake, bool isOver, bool isFulltime)
        {
            ProcessBetModel result = new ProcessBetModel()
            {
                ChoiceValue = isOver ? "Tài" : "Xỉu",
                Line = product.Hdp1.ToString(),
                Odds = isOver ? product.Odds1a.ToString() : product.Odds2a.ToString(),
                Home = match.Home,
                Away = match.Away,
                ProgramID = "",
                RaceNum = "0",
                Runner = "0",
                PoolType = "1",
                Type = "OU",
                Bettype = isFulltime ? "3" : "8",
                Oddsid = product.OddsId.ToString(),
                Hscore = match.LiveHomeScore.ToString(),
                Ascore = match.LiveAwayScore.ToString(),
                Matchid = match.MatchId.ToString(),
                Betteam = isOver ? "h" : "a",
                Stake = stake < 3 ? "3" : stake.ToString(),
                Gameid = "1",
                MRPercentage = "",
                AcceptBetterOdds = "true",
                isQuickBet = "true",
                BonusID = "0",
                BonusType = "0",
                PromoID = "0",
                IsInPlay = "false",
                SrcOddsInfo = "",
                isTablet = "false",
                Hdp1 = product.Hdp1.ToString(),
                Hdp2 = product.Hdp2.ToString(),
                ErrorCode = "0",

            };

            return result;
        }

        public static ProcessBetModel ConstructProcessBetHandicapModelV2(Product product, Match match, int stake, bool isOver, bool isFulltime)
        {
            ProcessBetModel result = new ProcessBetModel()
            {
                ChoiceValue = isOver ? match.Home : match.Away,
                Line = product.Hdp1.ToString(), // ??
                Odds = isOver ? product.Odds1a.ToString() : product.Odds2a.ToString(),
                Home = match.Home,
                Away = match.Away,
                ProgramID = "",
                RaceNum = "0",
                Runner = "0",
                PoolType = "0",
                Type = "OU",
                Bettype = isFulltime ? "1" : "7",
                Oddsid = product.OddsId.ToString(),
                Hscore = match.LiveHomeScore.ToString(),
                Ascore = match.LiveAwayScore.ToString(),
                Matchid = match.MatchId.ToString(),
                Betteam = isOver ? "h" : "a",
                Stake = stake < 3 ? "3" : stake.ToString(),
                Gameid = "1",
                MRPercentage = "",
                AcceptBetterOdds = "true",
                isQuickBet = "true",
                BonusID = "0",
                BonusType = "0",
                PromoID = "0",
                IsInPlay = "false",
                SrcOddsInfo = "",
                isTablet = "false",
                Hdp1 = product.Hdp1.ToString(),
                Hdp2 = product.Hdp2.ToString(),
                ErrorCode = "0",

            };

            return result;
        }

        public static int GetOverUnderMoneyLinePrice(int a, int b)
        {
            int moneyLine;
            if (a * b < 0)
            {
                moneyLine = (int)(Math.Abs(Math.Abs(b) - Math.Abs(a)));
            }
            else
            {
                moneyLine = (int)((100 - a) + (100 - b));
            }

            return Math.Abs(moneyLine);


        }

        public static int GetOverUnderMoneyLine(long matchId, Product product)
        {
            if (product == null)
            {
                product = DataStore.Products.Values.FirstOrDefault(item => item.MatchId == matchId && item.Bettype == Enums.BetType.FullTimeOverUnder);
            }

            if (product == null) return 0;
            int a = product.Odds1a100;
            int b = product.Odds2a100;
            return GetOverUnderMoneyLinePrice(a, b);
        }

        public static bool CanRunAutoBet()
        {
            try
            {
                ConfigModel config = Config.GetConfigModel();
                return config.cookie != null
                    && config.cookie.Length > 0
                    && config.bong88Url != null
                    && config.bong88Url.Length > 0;
            }
            catch
            {
                return false;
            }


        }

        public static bool CanBetting()
        {
            try
            {
                ConfigModel config = Config.GetConfigModel();
                return config.betting.ToLower().Trim() == "1";
            }
            catch
            {
                return false;
            }
        }

        public static bool CanRunAlert()
        {
            try
            {
                ConfigModel config = Config.GetConfigModel();
                return config.run_alert.ToLower().Trim() == "1";
            }
            catch
            {
                return false;
            }
            
        }

        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday
                || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static void UpdateTempBet(Guid betId, bool isFulltime, int livePeriod, int hdp, int price, string autobetMessage)
        {
            BetByHdpPrice bet = DataStore.BetByHdpPrice.FirstOrDefault(item => item.Id == betId);
            if (bet != null)
            {
                bet.IsFulltime = isFulltime;
                bet.LivePeriod = livePeriod;
                bet.Hdp = hdp;
                bet.Price = price;
                bet.AutoBetMessage = autobetMessage;
            }
        }

        public static void HightLightRowHasGoal(DataGridView dgv)
        {
            List<Match> matchs = DataStore.Matchs.Values.Where(item => item.LivePeriod != -1).ToList();
            for (int i = 0; i < matchs.Count; i++)
            {
                if (DataStore.GoalHistories.ContainsKey(matchs[i].MatchId)
                    && DataStore.GoalHistories[matchs[i].MatchId].Count > 0)
                {
                    GoalHistory lastGoal = DataStore.GoalHistories[matchs[i].MatchId].Last();
                    if (lastGoal.LivePeriod == matchs[i].LivePeriod
                        && matchs[i].TimeSpanFromStart.TotalMinutes - lastGoal.TimeSpanFromStart.TotalMinutes > 0
                        && matchs[i].TimeSpanFromStart.TotalMinutes - lastGoal.TimeSpanFromStart.TotalMinutes <= 100)
                    {
                        for (int j = 0; j < DataStore.BetByHdpPrice.Count; j++)
                        {
                            if (DataStore.BetByHdpPrice[j].MatchId == matchs[i].MatchId)
                            {
                                ColorDataGridView(dgv, "MatchId", matchs[i].MatchId.ToString(), Color.Yellow);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < DataStore.BetByHdpPrice.Count; j++)
                        {
                            if (DataStore.BetByHdpPrice[j].MatchId == matchs[i].MatchId)
                            {
                                ColorDataGridView(dgv, "MatchId", matchs[i].MatchId.ToString(), Color.White);
                            }
                        }
                    }
                }
            }
        }

        private static void ColorDataGridView(DataGridView dgv, string colName, string cellValue, Color color)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells[colName].Value.ToString() == cellValue)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = color;
                }
            }
        }

        public static int MaxNumberOfHdp(long matchId)
        {
            if (DataStore.ProductIdsOfMatch.ContainsKey(matchId))
            {
                return DataStore.ProductIdsOfMatch[matchId].Count(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder);
            }
            return 0;
        }

        public static void HighlightNewRowsAlert(DataGridView dgv, Dictionary<long, Alert> alertData)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                long matchId;
                if (long.TryParse(dgv.Rows[i].Cells["MatchId"].Value.ToString(), out matchId)
                    && alertData.ContainsKey(matchId)
                    && alertData[matchId].IsNew)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        public static string GetBookmarkPath(Match match)
        {
            return ("bookmarks\\"
                + match.MatchId.ToString()
                + "__" + match.Home.ToUpper()
                + "__" + match.Away.ToUpper()
                + ".png").Trim();
        }

        public static bool FastOverUnderPriceStep(int from, int to)
        {
            int standard = 7;

            if (from * to > 0)
            {
                return Math.Abs(from - to) > standard;
            }
            else
            {
                //90 95 100 -98 -80 100
                from = from > 0 ? 100 - from : 100 + from;
                to = to > 0 ? 100 - to : 100 + to;
                return (from + to) > standard;
            }
        }

        public static bool Fast2CellMove(int from, int to, int standard)
        {

            if (from * to > 0)
            {
                return Math.Abs(from - to) > standard;
            }
            else
            {
                //90 95 100 -98 -80 100
                from = from > 0 ? 100 - from : 100 + from;
                to = to > 0 ? 100 - to : 100 + to;
                return (from + to) > standard;
            }
        }

        public static bool DownCellMove(int from, int to, int standard)
        {
            int priceFrom = OverStepPrice(from);
            int priceTo = OverStepPrice(to);
            return priceFrom > priceTo && (priceFrom - priceTo) >= standard;
        }

        public static bool SlowOverUnderPriceStep(int from, int to)
        {
            if (from == to)
            {
                if (from > 0)
                {
                    return from > 90;
                }
                if (from < 0)
                {
                    return (-99 <= from && from < -90)
                        || (-70 <= from && from < -1);
                }
            }
            return false;
        }

        public static bool IsUp_UpDownPeekValley(int cell_a, int cell_b)
        {
            if (cell_a * cell_b > 0)
            {
                return cell_b > cell_a;
            }
            else
            {
                // up:      90 -80
                // down:    -90 98
                return cell_b < 0;
            }
        }

        public static List<List<int>> ExtractDataUpDownPeekValley(int minute, long matchId)
        {
            List<List<int>> result = new List<List<int>>();

            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(matchId))
            {
                return result;
            }

            List<int> keys = DataStore.OverUnderScoreTimesV3[matchId].Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                List<int> row = new List<int>();
                row.Add(DataStore.OverUnderScoreTimesV3[matchId][keys[i]][minute - 1].Over);
                row.Add(DataStore.OverUnderScoreTimesV3[matchId][keys[i]][minute].Over);
                row.Add(DataStore.OverUnderScoreTimesV3[matchId][keys[i]][minute + 1].Over);

                if (row.Count(item => item == 0) < 3)
                {
                    result.Add(row);
                }
            }

            return result;
        }

        public static bool CheckLoadUpDownPeekValley(List<List<int>> data)
        {
            List<int> pattern = new List<int>();
            // 1 ~ up       // thuận chiều
            // 2 ~ down     // nghịch chiều
            // 3 ~ peek     // up rồi down
            // 4 ~ valley   // down rồi up
            for (int i = 0; i < data.Count; i++)
            {
                int a = data[i][0];
                int b = data[i][1];
                int c = data[i][2];

                if (a == 0 || b == 0 || c == 0) continue;

                bool isUp_FirstHalf = Common.Functions.IsUp_UpDownPeekValley(a, b);
                bool isUp_SecondHalf = Common.Functions.IsUp_UpDownPeekValley(b, c);

                if (isUp_FirstHalf && isUp_SecondHalf)
                {
                    pattern.Add(2); // up
                }
                else if (isUp_FirstHalf && !isUp_SecondHalf)
                {
                    pattern.Add(4); // up then down
                }
                else if (!isUp_FirstHalf && !isUp_SecondHalf)
                {
                    pattern.Add(1); // down
                }
                else if (!isUp_FirstHalf && isUp_SecondHalf)
                {
                    pattern.Add(3); // down then up
                }
                else
                {

                }
            }

            if (pattern.Count > 0)
            {
                return pattern.Any(item => item != pattern[0]);
            }
            return false;
        }

        public static bool IuooCanEsc(int underPrice, int overPrice)
        {
            if (underPrice < 0 && overPrice < 0) return true;

            if (underPrice * overPrice < 0
                && underPrice + overPrice >= 0) return true;

            return false;
        }

        public static void UpdateGoalHistory(Match match)
        {
            GoalHistory goalHistory = AutoMapper.Mapper.Map<GoalHistory>(match);
            if (!DataStore.GoalHistories.ContainsKey(match.MatchId))
            {
                DataStore.GoalHistories.Add(match.MatchId, new List<GoalHistory>());
            }

            goalHistory.ProductOuId = DataStore.Products.Values
                .Where(item => item.MatchId == match.MatchId && item.Bettype == Enums.BetType.FullTimeOverUnder)
                .Select(item => item.OddsId)
                .ToList();

            var test = DataStore.Products.Values
                .Where(item => item.MatchId == match.MatchId).ToList();

            goalHistory.ProductStatusLogs = DataStore.Products.Values
                .Where(item => item.MatchId == match.MatchId && item.Bettype == Enums.BetType.FullTimeOverUnder)
                .Select(item => new ProductStatusLog()
                {
                    OddsId = item.OddsId,
                    ServerAction = "",
                    Status = item.OddsStatus,
                    Type = Enums.BetType.FullTimeOverUnder,
                    Hdp1 = item.Hdp1,
                    Hdp2 = item.Hdp2,
                    LivePeriod = match.LivePeriod,
                    IsHt = match.IsHT,
                    DtLog = DateTime.Now,
                    //TotalOuLine = DataStore.Matchs[item.MatchId].TotalOuLine,
                    GlobalShowtime = match.GlobalShowtime,
                    Price = item.Odds1a100,
                    MaxBet = item.MaxBet,
                    TotalGoal = match.LiveHomeScore + match.LiveAwayScore
                })
                .ToList();

            goalHistory.MatchDetectFirst = MatchDetectFirst(match.MatchId);

            DataStore.GoalHistories[match.MatchId].Add(goalHistory);
        }

        public static void UpdateCardHistory(Match match)
        {
            CardHistory cardHistory = AutoMapper.Mapper.Map<CardHistory>(match);
            if (!DataStore.CardHistories.ContainsKey(match.MatchId))
            {
                DataStore.CardHistories.Add(match.MatchId, new List<CardHistory>());
            }
            DataStore.CardHistories[match.MatchId].Add(cardHistory);
        }

        public static bool MatchDetectFirst(long matchId)
        {
            return true;
        }

        public static void UpdateGoalHistorySbo(Match match)
        {
            if (match.LivePeriod == 3) match.LivePeriod = 1;
            if (match.LivePeriod == 4) match.LivePeriod = 2;
            if (match.LivePeriod == 5) match.LivePeriod = 0;

            GoalHistory goalHistory = AutoMapper.Mapper.Map<GoalHistory>(match);
            if (!DataStore.GoalHistories.ContainsKey(match.MatchId))
            {
                DataStore.GoalHistories.Add(match.MatchId, new List<GoalHistory>());
            }

            goalHistory.ProductOuId = DataStore.Products.Values
                .Where(item => item.MatchId == match.MatchId && item.Bettype == Enums.BetType.FullTimeOverUnder)
                .Select(item => item.OddsId)
                .ToList();

            goalHistory.ProductStatusLogs = DataStore.Products.Values
                .Where(item => item.MatchId == match.MatchId && item.Bettype == Enums.BetType.FullTimeOverUnder)
                .Select(item => new ProductStatusLog()
                {
                    OddsId = item.OddsId,
                    ServerAction = "",
                    Status = item.OddsStatus,
                    Type = Enums.BetType.FullTimeOverUnder,
                    Hdp1 = item.Hdp1,
                    Hdp2 = item.Hdp2,
                    LivePeriod = match.LivePeriod,
                    IsHt = match.IsHT,
                    DtLog = DateTime.Now,
                    TotalOuLine = DataStore.Matchs[item.MatchId].TotalOuLine,
                    GlobalShowtime = match.GlobalShowtime,
                    Price = item.Odds1a100,
                    MaxBet = item.MaxBet,
                    TotalGoal = match.LiveHomeScore + match.LiveAwayScore
                })
                .ToList();

            DataStore.GoalHistories[match.MatchId].Add(goalHistory);
        }


        public static void UpdateOverUnderScoreTimesFreqFh(long matchId, Product product)
        {
            Match match;
            if (!DataStore.Matchs.TryGetValue(matchId, out match)) return;
            if (match.LivePeriod != 1) return;


            if (!DataStore.OverUnderScoreTimesFreqFh.ContainsKey(matchId))
            {
                DataStore.OverUnderScoreTimesFreqFh.Add(matchId, new Dictionary<int, List<int>>());
            }
            int score = (int)(((product.Hdp1 + product.Hdp2) - match.LiveAwayScore - match.LiveHomeScore) * 100);
            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
            if (currentMinute < 0 || currentMinute > 45)
            {
                return;
            }

            if (!DataStore.OverUnderScoreTimesFreqFh[matchId].ContainsKey(score))
            {
                List<int> empty = new List<int>();
                for (int i = 0; i <= 55; i++)
                {
                    empty.Add(0);
                }
                DataStore.OverUnderScoreTimesFreqFh[matchId].Add(score, empty);
            }

            DataStore.OverUnderScoreTimesFreqFh[matchId][score][currentMinute]++;

        }

        public static void UpdateOverUnderScoreTimesFreq(long matchId, Product product)
        {
            Match match;
            if (!DataStore.Matchs.TryGetValue(matchId, out match)) return;
            if (match.LivePeriod <= 0) return;


            if (!DataStore.OverUnderScoreTimesFreq.ContainsKey(matchId))
            {
                DataStore.OverUnderScoreTimesFreq.Add(matchId, new Dictionary<int, List<int>>());
            }
            int score = (int)(((product.Hdp1 + product.Hdp2) - match.LiveAwayScore - match.LiveHomeScore) * 100);
            int currentMinute = GetCurrentMinutePlusTime(match);
            if (currentMinute < 0 || currentMinute > 100)
            {
                return;
            }
            
            if (!DataStore.OverUnderScoreTimesFreq[matchId].ContainsKey(score))
            {
                List<int> empty = new List<int>();
                for (int i = 0; i <= 100; i++)
                {
                    empty.Add(0);
                }
                DataStore.OverUnderScoreTimesFreq[matchId].Add(score, empty);
            }

            DataStore.OverUnderScoreTimesFreq[matchId][score][currentMinute]++;

        }

        public static void UpdateOverUnderScoreTimesFreqHalfTime(long matchId, Product product)
        {
            Match match;
            if (!DataStore.Matchs.TryGetValue(matchId, out match)) return;
            if (!match.IsHT) return;

            int restedMinutes = (int)DateTime.Now.Subtract(match.BeginHT).TotalMinutes;
            if (restedMinutes > 30 || restedMinutes <= 0) return;

            if (!DataStore.OverUnderScoreTimesFreqHalfTime.ContainsKey(matchId))
            {
                DataStore.OverUnderScoreTimesFreqHalfTime.Add(matchId, new Dictionary<int, List<int>>());
            }
            int score = (int)(((product.Hdp1 + product.Hdp2) - match.LiveAwayScore - match.LiveHomeScore) * 100);


            if (!DataStore.OverUnderScoreTimesFreqHalfTime[matchId].ContainsKey(score))
            {
                List<int> empty = new List<int>();
                for (int i = 0; i <= 30; i++)
                {
                    empty.Add(0);
                }
                DataStore.OverUnderScoreTimesFreqHalfTime[matchId].Add(score, empty);
            }

            DataStore.OverUnderScoreTimesFreqHalfTime[matchId][score][restedMinutes]++;

        }

        public static void UpdateOverUnderScoreTimesFreqBeforeLive(long matchId, Product product)
        {
            Match match;
            if (!DataStore.Matchs.TryGetValue(matchId, out match)) return;
            if (match.LivePeriod != 0 || match.IsHT) return;

            int minuteDiff = (int)DateTime.Now.Subtract(match.GlobalShowtime).TotalMinutes;
            if (minuteDiff >= 0) return;


            minuteDiff = Math.Abs(minuteDiff);


            if (!DataStore.OverUnderScoreTimesFreqBeforeLive.ContainsKey(matchId))
            {
                DataStore.OverUnderScoreTimesFreqBeforeLive.Add(matchId, new Dictionary<int, List<int>>());
            }
            int score = (int)(((product.Hdp1 + product.Hdp2) - match.LiveAwayScore - match.LiveHomeScore) * 100);

            if (minuteDiff <= 0 || minuteDiff > 90)
            {
                return;
            }

            if (!DataStore.OverUnderScoreTimesFreqBeforeLive[matchId].ContainsKey(score))
            {
                List<int> empty = new List<int>();
                for (int i = 0; i <= 90; i++)
                {
                    empty.Add(0);
                }
                DataStore.OverUnderScoreTimesFreqBeforeLive[matchId].Add(score, empty);
            }

            DataStore.OverUnderScoreTimesFreqBeforeLive[matchId][score][minuteDiff]++;

        }

        public static void HandicapFreqBeforeLive(long matchId, Product product)
        {
            Match match;
            if (!DataStore.Matchs.TryGetValue(matchId, out match)) return;
            if (match.LivePeriod != 0 || match.IsHT) return;

            int minuteDiff = (int)DateTime.Now.Subtract(match.GlobalShowtime).TotalMinutes;
            if (minuteDiff >= 0) return;


            minuteDiff = Math.Abs(minuteDiff);


            if (!DataStore.HandicapFreqBeforeLive.ContainsKey(matchId))
            {
                DataStore.HandicapFreqBeforeLive.Add(matchId, new Dictionary<int, List<int>>());
            }
            int score = 0;
            if ((int)((product.Hdp1) * 100) > 0)
            {
                score = (int)((product.Hdp1) * 100);
            }
            else
            {
                score = (int)((product.Hdp2) * 100);
            }

            if (minuteDiff <= 0 || minuteDiff > 90)
            {
                return;
            }

            if (!DataStore.HandicapFreqBeforeLive[matchId].ContainsKey(score))
            {
                List<int> empty = new List<int>();
                for (int i = 0; i <= 90; i++)
                {
                    empty.Add(0);
                }
                DataStore.HandicapFreqBeforeLive[matchId].Add(score, empty);
            }

            DataStore.HandicapFreqBeforeLive[matchId][score][minuteDiff]++;

        }

        public static void HandicapFreqHalfTime(long matchId, Product product)
        {
            Match match;
            if (!DataStore.Matchs.TryGetValue(matchId, out match)) return;
            if (!match.IsHT) return;

            int restedMinutes = (int)DateTime.Now.Subtract(match.BeginHT).TotalMinutes;
            if (restedMinutes > 30 || restedMinutes <= 0) return;


            if (!DataStore.HandicapFreqHalfTime.ContainsKey(matchId))
            {
                DataStore.HandicapFreqHalfTime.Add(matchId, new Dictionary<int, List<int>>());
            }
            int score = 0;
            if ((int)((product.Hdp1) * 100) > 0)
            {
                score = (int)((product.Hdp1) * 100);
            }
            else
            {
                score = (int)((product.Hdp2) * 100);
            }

            if (!DataStore.HandicapFreqHalfTime[matchId].ContainsKey(score))
            {
                List<int> empty = new List<int>();
                for (int i = 0; i <= 30; i++)
                {
                    empty.Add(0);
                }
                DataStore.HandicapFreqHalfTime[matchId].Add(score, empty);
            }

            DataStore.HandicapFreqHalfTime[matchId][score][restedMinutes]++;

        }

        public static void HandicapFreqFhBeforeLive(long matchId, Product product)
        {
            Match match;
            if (!DataStore.Matchs.TryGetValue(matchId, out match)) return;
            if (match.LivePeriod != 0 || match.IsHT) return;

            int minuteDiff = (int)DateTime.Now.Subtract(match.GlobalShowtime).TotalMinutes;
            if (minuteDiff >= 0) return;


            minuteDiff = Math.Abs(minuteDiff);


            if (!DataStore.HandicapFreqFhBeforeLive.ContainsKey(matchId))
            {
                DataStore.HandicapFreqFhBeforeLive.Add(matchId, new Dictionary<int, List<int>>());
            }
            int score = 0;
            if ((int)((product.Hdp1) * 100) > 0)
            {
                score = (int)((product.Hdp1) * 100);
            }
            else
            {
                score = (int)((product.Hdp2) * 100);
            }

            if (minuteDiff <= 0 || minuteDiff > 90)
            {
                return;
            }

            if (!DataStore.HandicapFreqFhBeforeLive[matchId].ContainsKey(score))
            {
                List<int> empty = new List<int>();
                for (int i = 0; i <= 90; i++)
                {
                    empty.Add(0);
                }
                DataStore.HandicapFreqFhBeforeLive[matchId].Add(score, empty);
            }

            DataStore.HandicapFreqFhBeforeLive[matchId][score][minuteDiff]++;

        }

        public static void UpdateOverUnderScoreTimesFreqFhBeforeLive(long matchId, Product product)
        {
            Match match;
            if (!DataStore.Matchs.TryGetValue(matchId, out match)) return;
            if (match.LivePeriod != 0 || match.IsHT) return;

            int minuteDiff = (int)DateTime.Now.Subtract(match.GlobalShowtime).TotalMinutes;
            if (minuteDiff >= 0) return;


            minuteDiff = Math.Abs(minuteDiff);


            if (!DataStore.OverUnderScoreTimesFreqFhBeforeLive.ContainsKey(matchId))
            {
                DataStore.OverUnderScoreTimesFreqFhBeforeLive.Add(matchId, new Dictionary<int, List<int>>());
            }
            int score = (int)(((product.Hdp1 + product.Hdp2) - match.LiveAwayScore - match.LiveHomeScore) * 100);

            if (minuteDiff <= 0 || minuteDiff > 90)
            {
                return;
            }

            if (!DataStore.OverUnderScoreTimesFreqFhBeforeLive[matchId].ContainsKey(score))
            {
                List<int> empty = new List<int>();
                for (int i = 0; i <= 90; i++)
                {
                    empty.Add(0);
                }
                DataStore.OverUnderScoreTimesFreqFhBeforeLive[matchId].Add(score, empty);
            }

            DataStore.OverUnderScoreTimesFreqFhBeforeLive[matchId][score][minuteDiff]++;

        }

        public static void UpdateFinishPriceOverUnderScoreTimes(long matchId, Product product)
        {
            if (!DataStore.Matchs.ContainsKey(matchId)) return;

            Match match = DataStore.Matchs[matchId];
            if (!DataStore.FinishPriceOverUnderScoreTimes.ContainsKey(matchId))
            {
                DataStore.FinishPriceOverUnderScoreTimes.Add(matchId, new Dictionary<int, OverUnderScoreTimesV2Item>());
            }

            int score = (int)(((product.Hdp1 + product.Hdp2) - match.LiveAwayScore - match.LiveHomeScore) * 100);

            if (!DataStore.FinishPriceOverUnderScoreTimes[matchId].ContainsKey(score))
            {
                DataStore.FinishPriceOverUnderScoreTimes[matchId].Add(score, new OverUnderScoreTimesV2Item());
            }
            DataStore.FinishPriceOverUnderScoreTimes[matchId][score] = new OverUnderScoreTimesV2Item() { 
                Over = product.Odds1a100,
                Under = product.Odds2a100,
            }; 

        }

        public static void UpdateMaxMinHdpOfMatch(long matchId, Product product)
        {
            if (product.Bettype == Enums.BetType.FirstHalfOverUnder
                || product.Bettype == Enums.BetType.FullTimeOverUnder)
            {
                if (!DataStore.Matchs.ContainsKey(matchId)) return;
                Match match = DataStore.Matchs[matchId];
                int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;
                
                int hdp_100 = (int)(((product.Hdp1 + product.Hdp2) - totalLiveScore) * 100);


                if (product.Bettype == Enums.BetType.FullTimeOverUnder)
                {
                    if (hdp_100 > match.MaxHdpFt)
                    {
                        DataStore.Matchs[matchId].MaxHdpFt = hdp_100;
                    }

                    if (hdp_100 > 0 && hdp_100 < match.MinHdpFt)
                    {
                        DataStore.Matchs[matchId].MinHdpFt = hdp_100;
                    }
                }
                else if (product.Bettype == Enums.BetType.FirstHalfOverUnder)
                {
                    if (hdp_100 > match.MaxHdpFh)
                    {
                        DataStore.Matchs[matchId].MaxHdpFh = hdp_100;
                    }

                    if (hdp_100 > 0 && hdp_100 < match.MinHdpFh)
                    {
                        DataStore.Matchs[matchId].MinHdpFh = hdp_100;
                    }
                }
            }
        }

        public static class EnumUtil
        {
            public static IEnumerable<T> GetValues<T>()
            {
                return Enum.GetValues(typeof(T)).Cast<T>();
            }
        }

        public static bool CheckBetResult(JToken betResult)
        {
            if (betResult != null && betResult["ErrorCode"] != null && betResult["ErrorCode"].ToString() == "0")
            {
                JToken data = betResult["Data"];
                if (data != null && data["ItemList"] != null && data["ItemList"].Count() > 0)
                {
                    JToken errorCode = data["ItemList"][0]["ErrorCode"];
                    JToken code = data["ItemList"][0]["Code"];
                    if (errorCode != null && (errorCode.ToString() == "0" || errorCode.ToString() == "8")
                        && code != null && (code.ToString() == "0" || code.ToString() == "8" || code.ToString() == "1"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static List<IuooValue_3> GetDataIuoo_ByGoal_3(long matchId, int goalTh, bool hasFourHdpCount, int maxNumberOfHdp)
        {
            List<int> Hdps = DataStore.OverUnderScoreTimes[matchId]
                .Keys
                .OrderByDescending(item => item)
                .ToList();

            Dictionary<int, IuooValue_3> data = new Dictionary<int, IuooValue_3>();

            for (int i = 0; i < Hdps.Count; i++)
            {
                data.Add(Hdps[i], CalculateIuooValue_ByMinute_3(matchId, Hdps[i], goalTh));
            }

            if (!DataStore.OverUnderScoreTimes.ContainsKey(matchId)
                || DataStore.OverUnderScoreTimes[matchId].Keys.Count == 0)
            {
                return new List<IuooValue_3>();
            }

            int goalLimit = goalTh - 1;
            int limitMinute = 0;
            int fromMinute = 1;

            if ((DataStore.GoalHistories.ContainsKey(matchId)
                && DataStore.GoalHistories[matchId].Count >= goalLimit))
            {
                if (DataStore.GoalHistories[matchId].Count == goalLimit)
                {
                    limitMinute = 90;
                }
                else
                {
                    limitMinute = (int)DataStore.GoalHistories[matchId][goalLimit].TimeSpanFromStart.TotalMinutes + (45 * (DataStore.GoalHistories[matchId][goalLimit].LivePeriod - 1));
                    limitMinute = ((limitMinute >= 90) ? 90 : (limitMinute + 1));
                }

                if (goalLimit == 0)
                {
                    fromMinute = 1;
                }
                else
                {
                    GoalHistory lastGoal = DataStore.GoalHistories[matchId][goalLimit - 1];
                    fromMinute = (int)lastGoal.TimeSpanFromStart.TotalMinutes;
                    if (lastGoal.LivePeriod == 2)
                    {
                        fromMinute += 45;
                    }

                    if (goalTh > 1)
                    {
                        fromMinute += 2;
                    }
                }
            }
            else if (goalLimit == 0)
            {
                limitMinute = 90;
            }

            int maxHdp = FindMaxHdpOfIuoo(matchId, fromMinute, limitMinute, Hdps);
            maxHdp = (hasFourHdpCount && (maxHdp - 25 > 0)) ? maxHdp - 25 : maxHdp;

            if (!data.ContainsKey(maxHdp) || !data.ContainsKey(maxHdp - 25))
            {
                return new List<IuooValue_3>();
            }

            int selectedHdp = data[maxHdp].TGT < data[maxHdp - 25].TGT ? maxHdp : maxHdp - 25;

            Hdps = Hdps.Where(item => item > 0 && item <= maxHdp)
                .OrderByDescending(item => item)
                .ToList();

            for (int i = 0; i < Hdps.Count; i++)
            {
                if (Hdps[i] < maxHdp)
                {
                    data[Hdps[i]].TW_a = data[maxHdp].TGT * (i + 1);
                }

                if (Hdps[i] < maxHdp - 25)
                {
                    data[Hdps[i]].TW_b = data[maxHdp - 25].TGT * i;
                }

                if (data.ContainsKey(maxHdp - 50))
                {
                    if (Hdps[i] < maxHdp - 50)
                    {
                        data[Hdps[i]].TW_c = data[maxHdp - 50].TGT * i;
                    }
                }


                if (maxNumberOfHdp <= 2)
                {
                    if ((Hdps[i] == maxHdp || Hdps[i] == maxHdp - 25) && data[Hdps[i]].TX > 0)
                    {
                        data[Hdps[i]].TX = data[Hdps[i]].TGT;
                    }
                }
                else if (maxNumberOfHdp > 2)
                {
                    if ((Hdps[i] == maxHdp || Hdps[i] == maxHdp - 25 || Hdps[i] == maxHdp - 50) && data[Hdps[i]].TX > 0)
                    {
                        data[Hdps[i]].TX = data[Hdps[i]].TGT;
                    }
                }



                data[Hdps[i]] = UpdateTwl_3(matchId, data[maxHdp - 0].GX, data[Hdps[i]], 1, fromMinute, limitMinute);
                data[Hdps[i]] = UpdateTwl_3(matchId, data[maxHdp - 25].GX, data[Hdps[i]], 2, fromMinute, limitMinute);
                if (data.ContainsKey(maxHdp - 50))
                {
                    data[Hdps[i]] = UpdateTwl_3(matchId, data[maxHdp - 50].GX, data[Hdps[i]], 3, fromMinute, limitMinute);
                }
            }

            return data.Values.Where(item => item.Hdp > 0 && item.Hdp <= maxHdp && item.GX != 0).ToList();
        }

        private static IuooValue_3 CalculateIuooValue_ByMinute_3(long matchId, int hdp, int goalTh)
        {
            IuooValue_3 result = new IuooValue_3();

            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(matchId)
                || !DataStore.OverUnderScoreTimesV3[matchId].ContainsKey(hdp))
            {
                return result;
            }

            List<OverUnderScoreTimesV2Item> row = DataStore.OverUnderScoreTimesV3[matchId][hdp];


            int goalLimit = goalTh - 1;
            int limitMinute = 0;
            int fromMinute = 1;
            if (DataStore.GoalHistories.ContainsKey(matchId)
                && DataStore.GoalHistories[matchId].Count >= goalLimit)
            {
                if (goalLimit == DataStore.GoalHistories[matchId].Count)
                {
                    limitMinute = 90;
                }
                else
                {
                    limitMinute = (int)DataStore.GoalHistories[matchId][goalLimit].TimeSpanFromStart.TotalMinutes + (45 * (DataStore.GoalHistories[matchId][goalLimit].LivePeriod - 1));
                    limitMinute = ((limitMinute >= 90) ? 90 : (limitMinute + 1));
                }

                if (goalLimit == 0)
                {
                    fromMinute = 1;
                }
                else
                {
                    GoalHistory lastGoal = DataStore.GoalHistories[matchId][goalLimit - 1];
                    fromMinute = (int)lastGoal.TimeSpanFromStart.TotalMinutes;
                    if (lastGoal.LivePeriod == 2)
                    {
                        fromMinute += 45;
                    }

                    if (goalTh > 1)
                    {
                        fromMinute += 2;
                    }
                }

            }
            else if (goalLimit == 0)
            {
                limitMinute = 90;
            }



            result.Hdp = hdp;
            result.K = 0;
            for (int i = fromMinute; i <= limitMinute; i++)
            {
                if (row[i].Over == 0)
                {

                }
                else
                {
                    result.K = i - 1;
                    break;
                }

            }

            for (int i = fromMinute; i <= limitMinute; i++)
            {
                if (row[i].Under != 0)
                {
                    result.GX = row[i].Under;
                    result.TGX = i;
                    break;
                }
            }

            if (row[0].Over != 0 &&
                (row[1].Over != 0 || row[2].Over != 0 || row[3].Over != 0))
            {
                result.K = 0;
                result.GX = row[0].Under;
                result.TGX = 0;
            }

            result.R = 0;
            for (int i = result.K + 1; i < limitMinute; i++)
            {
                if (row[i].Over != 0)
                {
                    result.R++;
                    if (result.TGT == 0 &&
                        ((row[i].Over + result.GX >= 0 && row[i].Over * result.GX < 0) || (row[i].Over < 0 && result.GX < 0)))
                    {
                        result.TGT = i;
                    }
                }
                if (row[i].Over == 0 && i >= 3)
                {
                    if (i + 1 < limitMinute)
                    {
                        if (row[i + 1].Over == 0)
                        {
                            result.TCLOSE = i - 1;
                            break;
                        }
                    }
                    else
                    {
                        result.TCLOSE = i - 1;
                        break;
                    }

                }

            }

            if (result.TGT > 0)
            {
                result.TX = result.TGT - result.TGX + 1;
            }

            return result;
        }

        private static int FindMaxHdpOfIuoo(long matchId, int fromMinute, int limitMinute, List<int> Hdps)
        {
            int maxHdp = 0;

            for (int i = fromMinute; i <= limitMinute; i++)
            {
                for (int j = 0; j < Hdps.Count; j++)
                {
                    if (DataStore.OverUnderScoreTimesV3.ContainsKey(matchId)
                        && DataStore.OverUnderScoreTimesV3[matchId].ContainsKey(Hdps[j])
                        && DataStore.OverUnderScoreTimesV3[matchId][Hdps[j]][i].Over != 0
                        && DataStore.OverUnderScoreTimesV3[matchId][Hdps[j]][i].Under != 0)
                    {
                        return Hdps[j];
                    }
                }
            }

            return maxHdp;
        }

        //private static int FindMaxHdpOfIuoo(long matchId, int limitMinute, List<int> Hdps)
        //{
        //    int maxHdp = 0;

        //    for (int i = 1; i <= limitMinute; i++)
        //    {
        //        for (int j = 0; j < Hdps.Count; j++)
        //        {
        //            if (DataStore.OverUnderScoreTimesV3.ContainsKey(matchId)
        //                && DataStore.OverUnderScoreTimesV3[matchId].ContainsKey(Hdps[j])
        //                && DataStore.OverUnderScoreTimesV3[matchId][Hdps[j]][i].Over != 0
        //                && DataStore.OverUnderScoreTimesV3[matchId][Hdps[j]][i].Under != 0)
        //            {
        //                return Hdps[j];
        //            }
        //        }
        //    }

        //    return maxHdp;
        //}

        private static IuooValue_3 UpdateTwl_3(long matchId, int priceUnder, IuooValue_3 iuoo, int update_character, int fromMinute, int limitMinute)
        {
            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(matchId)
                || !DataStore.OverUnderScoreTimesV3[matchId].ContainsKey(iuoo.Hdp))
            {
                return iuoo;
            }

            List<OverUnderScoreTimesV2Item> row = DataStore.OverUnderScoreTimesV3[matchId][iuoo.Hdp];

            for (int i = fromMinute; i <= limitMinute; i++)
            {
                if ((row[i].Over + priceUnder >= 0 && row[i].Over * priceUnder < 0)
                    || (row[i].Over < 0 && priceUnder < 0))
                {
                    if (update_character == 1)
                    {
                        iuoo.TWL_a = i;
                    }
                    else if (update_character == 2)
                    {
                        iuoo.TWL_b = i;
                    }
                    else
                    {
                        iuoo.TWL_c = i;
                    }
                    return iuoo;
                }
            }

            return iuoo;
        }

        public static int OverStepPrice(int price)
        {
            if (price == 0) return 0;
            else if (price > 0) return price;
            else return 100 + 100 + price;
        }

        public static List<int> MaxHdpFtEveryMinute(long matchId)
        {
            List<int> result = new List<int>();

            for (int i = 0; i <= 92; i++ )
            {
                result.Add(0);
            }

            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(matchId)) return result;
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[matchId];
            List<int> keys = data.Keys.OrderByDescending(item => item).ToList();


            for (int minute = 1; minute <= 90; minute ++ )
            {
                for (int i=0; i<keys.Count; i++)
                {
                    if (data[keys[i]][minute].Over != 0)
                    {
                        result[minute] = keys[i];
                        break;
                    }
                }
            }

            if (DataStore.OverUnderScoreHalfTimes.ContainsKey(matchId))
            {
                Dictionary<int, List<string>> dataHt = DataStore.OverUnderScoreHalfTimes[matchId];
                List<int> scores = dataHt.Keys.OrderByDescending(item => item).ToList();
                for (int i = 0; i < scores.Count - 1; i++)
                {
                    if (dataHt[scores[i]][2].Length > 0)
                    {
                        result[92] = scores[i];
                        break;
                    }
                }

            }

            return result;
        }

        public static List<int> MaxHdpFhEveryMinute(long matchId)
        {
            List<int> result = new List<int>();

            for (int i = 0; i <= 45; i++)
            {
                result.Add(0);
            }

            if (!DataStore.OverUnderScoreTimesFirstHalf.ContainsKey(matchId)) return result;
            Dictionary<int, List<string>> data = DataStore.OverUnderScoreTimesFirstHalf[matchId];
            List<int> keys = data.Keys.OrderByDescending(item => item).ToList();


            for (int minute = 1; minute <= 45; minute++)
            {
                for (int i = 0; i < keys.Count; i++)
                {
                    if (data[keys[i]][minute].Length != 0)
                    {
                        result[minute] = keys[i];
                        break;
                    }
                }
            }

            return result;
        }

        public static string ListToString<T>(List<T> list, string seperator)
        {
            string result = "";
            for (int i = 0; i < list.Count; i++)
            {
                result += list[i].ToString();
                if (i != list.Count - 1)
                {
                    result += seperator;
                }
            }
            return result;
        }

        public static bool HasPriceAtBegin(long matchId)
        {
            if (!DataStore.Matchs.ContainsKey(matchId)) return true;
            if (!DataStore.OverUnderScoreTimesV2.ContainsKey(matchId)) return true;
            Match match = DataStore.Matchs[matchId];
            if (match.LivePeriod != 1) return true;
            if (match.TimeSpanFromStart.TotalMinutes < 3
                || match.TimeSpanFromStart.TotalMinutes > 6) return true;

            Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV2[matchId];

            List<int> hdps = data.Keys.ToList();

            for (int j = 0; j < hdps.Count; j++)
            {
                if (data[hdps[j]][1].Over != 0 ||
                    data[hdps[j]][2].Over != 0)
                {
                    return true;
                }
            } 

            return false;
        }

        public static List<OverUnderScoreTimesV3Item> GetEmptyRowBeforeLiveOuv3()
        {
            List<OverUnderScoreTimesV3Item> emptyRow = new List<OverUnderScoreTimesV3Item>();
            for (int j = 1; j <= 100; j++)
            {
                emptyRow.Add(
                    new OverUnderScoreTimesV3Item()
                    {
                        Over = 0,
                        Under = 0,
                        RecordedDatetime = DateTime.MinValue
                    }
                );
            }
            return emptyRow;
        }

        public static List<HandicapLifeTimeHistoryV2> GetEmptyRowHandicapLifeTime()
        {
            List<HandicapLifeTimeHistoryV2> emptyRow = new List<HandicapLifeTimeHistoryV2>();
            for (int j = 1; j <= 100; j++)
            {
                emptyRow.Add(
                    new HandicapLifeTimeHistoryV2()
                    {
                        OddsId = 0,
                        Hdp1 = 0,
                        Hdp2 = 0,
                        Odds1a = 0,
                        Odds2a = 0,
                        RecordedDatetime = DateTime.MinValue
                    }
                );
            }
            return emptyRow;
        }

        public static int HdpToNumberConvert(int hdp)
        {
            if (hdp % 25 == 0)
            {
                return hdp / 25;
            }
            else
            {
                throw new InvalidOperationException("hdp is wrong");
            }
        }

        public static List<int> GetOpenHdps(long matchId)
        {
            List<int> result = new List<int>();
            if (DataStore.OverUnderScoreTimesV3.ContainsKey(matchId))
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[matchId];
                List<int> keys = data.Keys.ToList();
                for (int minute = 1; minute <= 90; minute++)
                {
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (data[keys[i]][minute].Over != 0)
                        {
                            result.Add(keys[i]);
                        }
                    }
                    if (result.Count > 0) return result;
                }
            }

            return result;
        }

        public static List<int> GetCurrentHdps(long matchId)
        {
            List<Product> products = DataStore.Products.Values.Where(item => item.MatchId == matchId && item.Bettype == Enums.BetType.FullTimeOverUnder).ToList();
            Match match = DataStore.Matchs[matchId];
            List<int> result = new List<int>();
            for (int i = 0; i < products.Count; i++)
            {
                result.Add(
                    (int)(products[i].Hdp1 * 100) - (match.LiveAwayScore + match.LiveHomeScore)
                );
            }
            return result;
        }

        public static int GetTotalProductOuFtAtMinute(long matchId, int minute)
        {

            List<long> result = new List<long>();
            if (DataStore.OverUnderScoreTimesV4_60.ContainsKey(matchId)
                && minute >= 0 && minute <= 90)
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV4_60[matchId];
                List<int> keys = data.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    if (data[keys[i]][minute].OddsId != 0)
                    {
                        result.Add(data[keys[i]][minute].OddsId);
                    }
                }
            }


            return result.Count;
        }

        public static int GetTotalProductOuFhAtMinute(long matchId, int minute)
        {
            List<long> result = new List<long>();
            if (DataStore.OverUnderScoreTimesFirstHalfV3.ContainsKey(matchId)
                && minute >= 0 && minute <= 45)
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesFirstHalfV3[matchId];
                List<int> keys = data.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    if (data[keys[i]][minute].OddsId != 0)
                    {
                        result.Add(data[keys[i]][minute].OddsId);
                    }
                }
            }


            return result.Count;
        }

        public static int GetHdpsOpenMax(long matchId)
        {

            if (DataStore.OverUnderScoreTimesV3.ContainsKey(matchId))
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[matchId];
                List<int> keys = DataStore.OverUnderScoreTimesV3[matchId].Keys.OrderByDescending(item => item).ToList();
                for (int minute = 1; minute <= 90; minute++)
                {
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (data[keys[i]][minute].Over != 0)
                        {
                            if (IsOpenMax(keys[i], data, matchId))
                            {
                                return keys[i];
                            }
                            else if (keys.Count > i + 1)
                            {
                                return keys[i + 1];
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public static int GetMaxHdpAtMinuteV2(long matchId, int minute)
        {
            if (DataStore.OverUnderScoreTimesV3.ContainsKey(matchId)
                && minute >= 1)
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[matchId];
                List<int> keys = DataStore.OverUnderScoreTimesV3[matchId].Keys.OrderByDescending(item => item).ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    if (data[keys[i]][minute].Over != 0)
                    {
                        if (IsOpenMax(keys[i], data, matchId))
                        {
                            return keys[i];
                        }
                        else if (keys.Count > i + 1)
                        {
                            return keys[i + 1];
                        }
                    }
                }
                
            }
            return 0;
        }

        public static int GetMaxHdpAtMinute(long matchId, int minute)
        {
            if (DataStore.OverUnderScoreTimesV3.ContainsKey(matchId)
                && minute >= 1)
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[matchId];
                List<int> keys = DataStore.OverUnderScoreTimesV3[matchId].Keys.OrderByDescending(item => item).ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    if (data[keys[i]][minute].Over != 0)
                    {
                        return keys[i];
                    }
                }

            }
            return 0;
        }

        public static int GetMinHdpAtMinute(long matchId, int minute)
        {
            if (DataStore.OverUnderScoreTimesV3.ContainsKey(matchId)
                && minute >= 1)
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[matchId];
                List<int> keys = DataStore.OverUnderScoreTimesV3[matchId].Keys.OrderBy(item => item).ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    if (data[keys[i]][minute].Over != 0)
                    {
                        return keys[i];
                    }
                }

            }
            return 0;
        }

        public static int CountHdpAtMinute(long matchId, int minute)
        {
            int count = 0;
            if (DataStore.OverUnderScoreTimesV3.ContainsKey(matchId)
                && minute >= 1
                && minute <= 90)
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[matchId];
                List<int> keys = DataStore.OverUnderScoreTimesV3[matchId].Keys.OrderByDescending(item => item).ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    if (data[keys[i]][minute].Over != 0)
                    {
                        count++;
                    }
                }

            }
            return count;
        }

        public static int GetRealTimeMaxHdp(Match match)
        {
            int minute = (int)match.TimeSpanFromStart.TotalMinutes + (match.LivePeriod - 1) * 45;
            return GetMaxHdpAtMinuteV2(match.MatchId, minute);
        }

        private static bool IsOpenMax(int hdp, Dictionary<int, List<OverUnderScoreTimesV2Item>> data, long matchId)
        {
            int remain = data[hdp].Count(item => item.Over != 0);
            int firstPrice = 0;
            int lastPrice = 0;
            OverUnderScoreTimesV2Item firstItem = data[hdp].FirstOrDefault(item => item.Over != 0);
            OverUnderScoreTimesV2Item lastItem = data[hdp].LastOrDefault(item => item.Over != 0);
            if (firstItem != null && lastItem != null)
            {
                firstPrice = Common.Functions.OverStepPrice(firstItem.Over);
                lastPrice = Common.Functions.OverStepPrice(lastItem.Over);
            }
            Match match = null;
            if (DataStore.Matchs.ContainsKey(matchId))
            {
                match = DataStore.Matchs[matchId];
            }

            return remain > 3
                && match != null
                && Math.Abs(firstPrice - lastPrice) > match.OverUnderMoneyLine;


        }

        public static int GetHdpOpenMaxOfNgl(long matchId)
        {
            Dictionary<int, List<OverUnderScoreTimesV3Item>> data;

            if (DataStore.OverUnderScoreHalftime.TryGetValue(matchId, out data))
            {
                List<int> keys = data.Keys.OrderByDescending(item => item).ToList();
                for (int i = 0; i <= 30; i++)
                {
                    for (int j = 0; j < keys.Count; j++)
                    {
                        if (data[keys[j]][i].Over != 0)
                        {
                            return keys[j];
                        }
                    }
                }
            }

            return 0;
        }

        public static int GetHdpOpenMinOfNgl(long matchId)
        {
            Dictionary<int, List<OverUnderScoreTimesV3Item>> data;

            if (DataStore.OverUnderScoreHalftime.TryGetValue(matchId, out data))
            {
                List<int> keys = data.Keys.OrderBy(item => item).ToList();
                for (int i = 0; i <= 30; i++)
                {
                    for (int j = 0; j < keys.Count; j++)
                    {
                        if (data[keys[j]][i].Over != 0)
                        {
                            return keys[j];
                        }
                    }
                }
            }

            return 0;
        }

        public static int GetNglByOpenMax(int openMax)
        {
            if (openMax % 100 == 0)
            {
                return HdpToNumberConvert(openMax + 50) / 2;
            }
            else if (openMax % 100 == 25)
            {
                return HdpToNumberConvert(openMax + 25) / 2;
            }
            else if (openMax % 100 == 50)
            {
                return HdpToNumberConvert(openMax + 50) / 2;
            }
            else if (openMax % 100 == 75)
            {
                return HdpToNumberConvert(openMax + 25) / 2;
            }
            return 0;
        }

        public static int GetLevelByOpenMax(int openMax)
        {
            if (openMax % 100 == 0)
            {
                return 4;
            }
            else if (openMax % 100 == 25)
            {
                return 5;
            }
            else if (openMax % 100 == 50)
            {
                return 4;
            }
            else if (openMax % 100 == 75)
            {
                return 5;
            }
            return 0;
        }

        public static int CalculateSubtractValue(int a, int b, int line)
        {
            return Common.Functions.OverStepPrice(a) - Common.Functions.OverStepPrice(b) - line;
        }

        public static List<List<string>> GetSubtractHdpFt(long matchId)
        {

            List<List<string>> result = new List<List<string>>();
            result.Add(new List<string>());
            result.Add(new List<string>());
            result.Add(new List<string>());
            result.Add(new List<string>());

            for (int i = 0; i <= 93; i++)
            {
                result[0].Add("");
                result[1].Add("");
                result[2].Add("");
                result[3].Add("");
            }

            if (!DataStore.OverUnderScoreTimesV2.ContainsKey(matchId)) return result;

            Match match = DataStore.Matchs[matchId];

            Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV2[matchId];

            List<int> keys = data.Keys.OrderByDescending(item => item).ToList();


            List<ProductIdLog> productIdLogs = DataStore.ProductIdsOfMatch[matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder && item.Minute <= -3)
                .ToList();

            for (int i = 0; i < productIdLogs.Count; i++)
            {
                if (!keys.Contains(productIdLogs[i].Hdp))
                {
                    keys.Add(productIdLogs[i].Hdp);
                }
            }
            keys = keys.OrderByDescending(item => item).ToList();


            for (int minute = 0; minute <= 90; minute++)
            {
                int keyIndex = 0;
                for (int key = 0; key < keys.Count - 1; key++)
                {
                    if (data.ContainsKey(keys[key])
                        && data.ContainsKey(keys[key+1]))
                    {
                        if (data[keys[key]][minute].Over != 0
                            && data[keys[key + 1]][minute].Over != 0)
                        {
                            result[keyIndex][minute + 2] = Common.Functions.CalculateSubtractValue(data[keys[key]][minute].Over, data[keys[key + 1]][minute].Over, GetOverUnderMoneyLine(match.MatchId, minute)).ToString();
                            keyIndex++;
                            if (keyIndex == 3) break;
                        }
                    }
                }
            }

            for (int minute = 0; minute <= 1; minute++)
            {
                int keyIndex = 0;
                for (int key = 0; key < keys.Count - 1; key++)
                {
                    ProductIdLog currentProductIdLog = productIdLogs.FirstOrDefault(item => item.Hdp == keys[key]);
                    ProductIdLog belowProductIdLog = productIdLogs.FirstOrDefault(item => item.Hdp == keys[key + 1]);
                    if (currentProductIdLog != null && belowProductIdLog != null)
                    {
                        result[keyIndex][1] = Common.Functions.CalculateSubtractValue(currentProductIdLog.Odds1a100, belowProductIdLog.Odds1a100, GetOverUnderMoneyLine(match.MatchId, minute)).ToString();
                        keyIndex++;
                        if (keyIndex == 3) break;
                    }
                }
            }


            if (DataStore.OverUnderScoreHalfTimes.ContainsKey(matchId))
            {
                Dictionary<int, List<string>> dataHt = DataStore.OverUnderScoreHalfTimes[matchId];
                List<int> scores = dataHt.Keys.OrderByDescending(item => item).ToList();
                int keyIndex = 0;
                for (int i = 0; i < scores.Count - 1; i++)
                {
                    if (dataHt[scores[i]][2].Length > 0
                        && dataHt[scores[i + 1]][2].Length > 0
                        )
                    {
                        result[keyIndex][93] = Common.Functions.CalculateSubtractValue(
                            int.Parse(dataHt[scores[i + 0]][2]),
                            int.Parse(dataHt[scores[i + 1]][2]), 
                            match.OverUnderMoneyLine
                        ).ToString();
                        keyIndex++;
                        if (keyIndex == 3) break;
                    }
                }

            }

            for (int i = 0; i <= 93; i++)
            {
                int a = 0;
                int b = 0;
                int c = 0;
                bool canConvertA = int.TryParse(result[0][i], out a);
                bool canConvertB = int.TryParse(result[1][i], out b);
                bool canConvertC = int.TryParse(result[2][i], out c);
                int countConvert = 0;
                if (canConvertA) countConvert++;
                if (canConvertB) countConvert++;
                if (canConvertC) countConvert++;

                if (canConvertA || canConvertB || canConvertC)
                {
                    result[3][i] = (a + b + c + GetOverUnderMoneyLine(matchId, i) * countConvert).ToString();
                }
                    
            }

            return result;
        }

        private static int GetOverUnderMoneyLine45(long matchId, int livePeriod, int minute45)
        {
            try
            {
                if (!DataStore.MatchOverUnderMoneyLine.ContainsKey(matchId) || minute45 > 45)
                {
                    return 0;
                }
                return DataStore.MatchOverUnderMoneyLine[matchId][livePeriod][minute45];
            }
            catch
            {
                return 0;
            }
        }

        private static int GetOverUnderMoneyLine(long matchId, int minute90)
        {
            try
            {
                if (!DataStore.MatchOverUnderMoneyLine.ContainsKey(matchId) || minute90 >= 90)
                {
                    return 0;
                }
                int realMinute;
                int liverPeriod;

                if (minute90 <= 45)
                {
                    realMinute = minute90;
                    liverPeriod = 1;
                }
                else
                {
                    realMinute = minute90 - 45;
                    liverPeriod = 2;
                }

                return DataStore.MatchOverUnderMoneyLine[matchId][liverPeriod][realMinute];
            }
            catch
            {
                return 0;
            }

        }

        public static string GetProductOuFtCountHistoryBeforeLive(long matchId)
        {
            //return "";
            return GetGetProductCountHistory(matchId, 
                DataStore.ProductIdsOfMatch[matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder && item.Minute < 0)
                    .ToList(),
                0, true
            );
        }

        public static string GetMaxFreqLeague(long matchId)
        {
            string result = "";
            int value;
            //Dictionary<int, List<int>> dataOverUnderScoreTimesFreqBeforeLive;
            //if (DataStore.OverUnderScoreTimesFreqBeforeLive.TryGetValue(matchId, out dataOverUnderScoreTimesFreqBeforeLive))
            //{
            //    value = GetMaxSumFreq(dataOverUnderScoreTimesFreqBeforeLive);
            //    if (value < 10)
            //    {
            //        result += ("0" + value + " ");
            //    }
            //    else
            //    {
            //        result += (value + " ");
            //    }
                
            //}
            //else
            //{
            //    result += "00 ";
            //}

            //Dictionary<int, List<int>> dataOverUnderScoreTimesFreqFhBeforeLive;
            //if (DataStore.OverUnderScoreTimesFreqFhBeforeLive.TryGetValue(matchId, out dataOverUnderScoreTimesFreqFhBeforeLive))
            //{
            //    value = GetMaxSumFreq(dataOverUnderScoreTimesFreqFhBeforeLive);
            //    if (value < 10)
            //    {
            //        result += ("0" + value + " ");
            //    }
            //    else
            //    {
            //        result += (value + " ");
            //    }
            //}
            //else
            //{
            //    result += "00 ";
            //}

            //Dictionary<int, List<int>> dataHandicapFreqBeforeLive;
            //if (DataStore.HandicapFreqBeforeLive.TryGetValue(matchId, out dataHandicapFreqBeforeLive))
            //{
            //    value = GetMaxSumFreq(dataHandicapFreqBeforeLive);
            //    if (value < 10)
            //    {
            //        result += ("0" + value + " ");
            //    }
            //    else
            //    {
            //        result += (value + " ");
            //    }
            //}
            //else
            //{
            //    result += "00 ";
            //}

            //Dictionary<int, List<int>> dataHandicapFreqFhBeforeLive;
            //if (DataStore.HandicapFreqFhBeforeLive.TryGetValue(matchId, out dataHandicapFreqFhBeforeLive))
            //{
            //    value = GetMaxSumFreq(dataHandicapFreqFhBeforeLive);
            //    if (value < 10)
            //    {
            //        result += ("0" + value + " ");
            //    }
            //    else
            //    {
            //        result += (value + " ");
            //    }
            //}
            //else
            //{
            //    result += "00 ";
            //}

            Dictionary<int, List<int>> dataOverUnderScoreTimesFreqHalfTime;
            if (DataStore.OverUnderScoreTimesFreqHalfTime.TryGetValue(matchId, out dataOverUnderScoreTimesFreqHalfTime))
            {
                value = GetMaxSumFreq(dataOverUnderScoreTimesFreqHalfTime);
                if (value < 10)
                {
                    result += ("0" + value + " ");
                }
                else
                {
                    result += (value + " ");
                }
            }
            else
            {
                result += "00 ";
            }

            Dictionary<int, List<int>> dataHandicapFreqHalfTime;
            if (DataStore.HandicapFreqHalfTime.TryGetValue(matchId, out dataHandicapFreqHalfTime))
            {
                value = GetMaxSumFreq(dataHandicapFreqHalfTime);
                if (value < 10)
                {
                    result += ("0" + value + " ");
                }
                else
                {
                    result += (value + " ");
                }
            }
            else
            {
                result += "00 ";
            }

            return result;
        }

        private static int GetMaxSumFreq(Dictionary<int, List<int>> data)
        {
            int max = 0;
            List<int> keys = data.Keys.ToList();
            int colCount = 0;
            if (keys.Count > 0)
            {
                colCount = data[keys[0]].Count;
            }
            for (int j = 0; j < colCount; j++)
            {
                int sumCol = 0;
                for (int i = 0; i < keys.Count; i++)
                {
                    sumCol += data[keys[i]][j];
                }
                if (sumCol > max)
                {
                    max = sumCol;
                }
            }
            return max;
        }

        public static string GetProductOuFtCountHistoryLive(long matchId)
        {
            //return "";
            string bonusChar = "";

            if (DataStore.OverUnderScoreTimesV3.ContainsKey(matchId)
                && DataStore.ProductIdsOuFtOfMatchHistoryLive.ContainsKey(matchId)
                && DataStore.ProductIdsOuFtOfMatchHistoryLive[matchId].Count > 0)
            {
                int countRealStepOuTable = DataStore.OverUnderScoreTimesV3[matchId].Count(item => item.Value.Skip(1).Count(p => p.Over != 0) > 0) - GetOpenHdps(matchId).Count;
                int stepInProduct = (int)DataStore.ProductIdsOuFtOfMatchHistoryLive[matchId].Max(item => item.Count) - (int)DataStore.ProductIdsOuFtOfMatchHistoryLive[matchId].Min(item => item.Count);

                if (countRealStepOuTable > stepInProduct)
                {
                    if (DataStore.Matchs[matchId].MinuteSpade == 0)
                    {
                        DataStore.Matchs[matchId].MinuteSpade = (int)DataStore.Matchs[matchId].TimeSpanFromStart.TotalMinutes;
                    }

                    bonusChar = " ♠ " + DataStore.Matchs[matchId].MinuteSpade;
                }
            }

            return GetGetProductCountHistory(matchId, 
                DataStore.ProductIdsOfMatch[matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder && item.Minute >= 0)
                    .ToList(),
                DataStore.ProductIdsOfMatch[matchId].Count(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder && item.Minute < 0),
                true
            ) + bonusChar;

        }

        public static string GetGetProductCountHistory(long matchId, List<ProductIdLog> history, int beginIndex, bool needSortIndex)
        {

            string result = "";
            if (!needSortIndex)
            {
                history = history.OrderBy(item => item.Minute).ToList();
                for (int i = 0; i < history.Count; i++)
                {
                    result += ((i + beginIndex + 1) + " ");
                }
                return result;
            }

            List<ProductIdLog> historySorted = history.Where(item => item.Minute <= 0).OrderBy(item => item.Minute).ToList();
            List<ProductIdLog> historyNotSorted = history.Where(item => item.Minute > 0).OrderBy(item => item.Minute).ToList();
            List<KeyValuePair<int, int>> indexSorted = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> indexNotSorted = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < historySorted.Count; i++)
            {
                indexSorted.Add(new KeyValuePair<int, int>(i + 1 + beginIndex, historySorted[i].Hdp));
            }

            for (int i = 0; i < historyNotSorted.Count; i++)
            {
                indexNotSorted.Add(new KeyValuePair<int, int>(i + 1 + historySorted.Count + beginIndex, historyNotSorted[i].Hdp));
            }

            indexSorted = indexSorted.OrderByDescending(item => item.Value).ToList();

            for (int i = 0; i < indexSorted.Count; i++)
            {
                result += (indexSorted[i].Key + " ");
            }

            for (int i = 0; i < indexNotSorted.Count; i++)
            {
                result += (indexNotSorted[i].Key + " ");
            }



            return result;
        }

        public static string GetGetProductCountHistoryMinute(long matchId, Dictionary<long, List<ProductIdsHistory>> dataHistory)
        {
            string result = "";

            if (dataHistory.ContainsKey(matchId))
            {
                List<ProductIdsHistory> history = dataHistory[matchId];
                for (int i = 0; i < history.Count; i++)
                {
                    result += (history[i].Minute + " ");
                }
            }

            return result;
        }

        private static int MaxMinuteHalfTime(long matchid)
        {
            int max = 0;
            if (DataStore.OverUnderScoreHalfTimes.ContainsKey(matchid))
            {
                Dictionary<int, List<string>> data = DataStore.OverUnderScoreHalfTimes[matchid];
                List<int> keys = data.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    for (int j = data[keys[i]].Count - 1; j >= 0; j--)
                    {
                        if (data[keys[i]][j].Length > 0)
                        {
                            if (j > max) max = j;
                            break;
                        }
                    }
                }
            }

            return max;
        }

        public static string GetProductCountHistoryMinuteSpecial (long matchId, Dictionary<long, List<ProductIdsHistory>> dataHistory, Enums.BetType betType)
        {
            string result = "";
            int minuteHt = 0;
            Match match = DataStore.Matchs[matchId];
            minuteHt = MaxMinuteHalfTime(matchId);


            List<ProductIdLog> data = DataStore.ProductIdsOfMatch[matchId].Where(item => item.ProductBetType == betType).ToList();
            
            if (dataHistory.ContainsKey(matchId))
            {
                int diff = data.Count - dataHistory[matchId].Count;
                if (diff >= 0)
                {
                    List<ProductIdsHistory> history = dataHistory[matchId];
                    for (int i = 0; i < history.Count; i++)
                    {
                        int minute = data[i + diff].Minute;
                        if (minute > 45)
                        {
                            minute = minute - 45;
                            if (minuteHt > 0)
                            {
                                minute = minute - minuteHt;
                            }
                        }
                        if (data[i + diff].LivePeriod == 2)
                        {
                            result += ("[" + minute + "] ");
                        }
                        else
                        {
                            result += (minute + " ");
                        }
                        
                    }
                }

            }

            return result;
        }

        public static void PlaySound(string audioPath)
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    var reader = new WaveFileReader(audioPath);
                    var waveOut = new WaveOut(); // or WaveOutEvent()
                    waveOut.Init(reader);
                    waveOut.Play();
                    Thread.Sleep(1000);
                }
                catch
                {

                }

            });

            t.IsBackground = true;
            t.Start();
        }

        public static int CountStepPriceWD23(long matchId, Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>> ds)
        {
            if (!ds.ContainsKey(matchId)) return 0;
            int count = 0;
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data = ds[matchId];
            List<int> keys = data.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                for (int j = 1; j < 91; j++)
                {
                    int current = data[keys[i]][j].Over;
                    int previous = data[keys[i]][j - 1].Over;
                    if (current != 0
                        && previous != 0
                        && j != 46
                        && Common.Functions.DownCellMove(previous, current, DataStore.Matchs[matchId].PriceStepDown))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public static int ColWidthStyle()
        {
            if (DataStore.Theme == Enums.VisualThemes.TV_FULL_HD)
            {
                return 31;
            }
            else if (DataStore.Theme == Enums.VisualThemes.TV_HD)
            {
                return 26;
            }
            return 22;
        }

        public static void SetMiniStyle(DataGridView dgv)
        {
            if (DataStore.Theme == Enums.VisualThemes.TV_FULL_HD)
            {
                dgv.DefaultCellStyle.Font = new Font(dgv.DefaultCellStyle.Font.FontFamily, 12, GraphicsUnit.Point);
                dgv.DefaultCellStyle.Padding = new Padding(-1);
                dgv.RowTemplate.Height = 20;
            }
            else if (DataStore.Theme == Enums.VisualThemes.TV_HD)
            {
                dgv.DefaultCellStyle.Font = new Font(dgv.DefaultCellStyle.Font.FontFamily, (float)9.7, GraphicsUnit.Point);
                dgv.DefaultCellStyle.Padding = new Padding(-1);
                dgv.RowTemplate.Height = 18;
            }

        }
        
        public static bool NeedShowColorGetProductStatusLog1x2HasPrice(Match match, Enums.BetType bettype)
        {
            if (!DataStore.ProductStatus.ContainsKey(match.MatchId)
                || (match.LivePeriod != 1 && match.LivePeriod != 2))
            {
                return false;
            }
            
            List<List<ProductStatusLog>> data = DataStore.ProductStatus[match.MatchId];
            int realMinute = (int)match.TimeSpanFromStart.TotalMinutes;
            ProductStatusLog firstItem = data[realMinute].FirstOrDefault(item =>
                item.LivePeriod == match.LivePeriod
                && item.Type == bettype
                && item.Price != 0
            );
            if (firstItem != null)
            {
                return true;
            }

            if (realMinute - 1 >= 1)
            {
                firstItem = data[realMinute - 1].FirstOrDefault(item =>
                    item.LivePeriod == match.LivePeriod
                    && item.Type == bettype
                    && item.Price != 0
                );

                if (firstItem != null)
                {
                    return true;
                }
            }

            if (realMinute - 2 >= 1)
            {
                firstItem = data[realMinute - 2].FirstOrDefault(item =>
                    item.LivePeriod == match.LivePeriod
                    && item.Type == bettype
                    && item.Price != 0
                );

                if (firstItem != null)
                {
                    return true;
                }
            }

            if (realMinute - 3 >= 1)
            {
                firstItem = data[realMinute - 3].FirstOrDefault(item =>
                    item.LivePeriod == match.LivePeriod
                    && item.Type == bettype
                    && item.Price != 0
                );

                if (firstItem != null)
                {
                    return true;
                }
            }

            if (realMinute - 4 >= 1)
            {
                firstItem = data[realMinute - 4].FirstOrDefault(item =>
                    item.LivePeriod == match.LivePeriod
                    && item.Type == bettype
                    && item.Price != 0
                );

                if (firstItem != null)
                {
                    return true;
                }
            }

            if (realMinute - 5 >= 1)
            {
                firstItem = data[realMinute - 5].FirstOrDefault(item =>
                    item.LivePeriod == match.LivePeriod
                    && item.Type == bettype
                    && item.Price != 0
                );

                if (firstItem != null)
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetProductStatusLog1x2HasPrice(long matchId, Enums.BetType bettype)
        {
            if (!DataStore.ProductStatus.ContainsKey(matchId)) return "";
            List<List<ProductStatusLog>> data = DataStore.ProductStatus[matchId];

            List<ProductStatusLog> firstItems = new List<ProductStatusLog>();
            string result = "";


            if (Enums.BetType.FullTime1x2 == bettype)
            {
                if (DataStore.Matchs[matchId].HasInsert1x2FtBeforelive)
                {
                    result += "♠ ";
                }
            }
            else if (Enums.BetType.FirstHalf1x2 == bettype)
            {
                if (DataStore.Matchs[matchId].HasInsert1x2FhBeforelive)
                {
                    result += "♠ ";
                }
            }

            for (int i = 46; i >= 5; i--)
            {
                int realMinute = i - 1;
                ProductStatusLog firstItem = data[realMinute].FirstOrDefault(item =>
                    item.LivePeriod == 2
                    && item.Type == bettype
                    && item.Price != 0
                );
                if (firstItem != null)
                {
                    result += (realMinute);
                    if (firstItem.LivePeriod == 2) result += "\"";
                    result += " ";
                    firstItems.Add(firstItem);
                }
                
                if (firstItems.Count >= 3) break;
            }

            for (int i = 46; i >= 5; i--)
            {
                int realMinute = i - 1;
                ProductStatusLog firstItem = data[realMinute].FirstOrDefault(item =>
                    item.LivePeriod == 1
                    && item.Type == bettype
                    && item.Price != 0
                );
                if (firstItem != null)
                {
                    result += (realMinute);
                    if (firstItem.LivePeriod == 2) result += "\"";
                    result += " ";
                    firstItems.Add(firstItem);
                }

                if (firstItems.Count >= 3) break;
            }


            List<string> temp = result.Split(' ').Reverse().ToList();
            result = "";
            for (int i=0; i< temp.Count; i++)
            {
                result += temp[i];
                result += " ";
            }

            

            return result;
        }

        public static string GetProductStatusLog1x2HasPriceBeforeLive(long matchId, Enums.BetType bettype, string suffix)
        {
            if (!DataStore.ProductStatus.ContainsKey(matchId)) return "";
            List<List<ProductStatusLog>> data = DataStore.ProductStatus[matchId];

            //int totalClosePrice = 0;
            //totalClosePrice = data.Sum(item => item.Count(p => p.Type == bettype && p.Status == "closePrice"));

            int realMinute = 0;
            if (data[realMinute].Any(item =>
                item.LivePeriod == 0
                && !item.IsHt
                && item.Type == bettype
                && item.Price != 0
            ))
            {
                return "+ " + (suffix ?? "");
            }
            return suffix;

        }

        public static string GetOverUnderLine(List<int> lines)
        {
            if (lines.Count == 0) return "";
            else if (lines.Count == 1) return lines.FirstOrDefault().ToString();
            else return lines[lines.Count - 1] + "(" + (lines[lines.Count - 1] - lines[lines.Count - 2]) + ")";
        }
        public static string GetHdpAlert(long matchId)
        {
            if (DataStore.Alert_Wd44.ContainsKey(matchId))
            {
                Alert alert = DataStore.Alert_Wd44[matchId];
                Match match = DataStore.Matchs[matchId];
                return alert.CustomValue;
            }
            return "";
        }

        public static void FreezeBand(DataGridViewBand band)
        {
            band.Frozen = true;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.WhiteSmoke;
            band.DefaultCellStyle = style;
        }

        public static Dictionary<int, List<int>> BuildOuScoreRemainingTime(long matchId)
        {
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data;
            Dictionary<int, List<int>> dataGrid = new Dictionary<int, List<int>>();

            List<GoalHistory> goalHistories = new List<GoalHistory>();
            DataStore.GoalHistories.TryGetValue(matchId, out goalHistories);
            if (goalHistories == null) goalHistories = new List<GoalHistory>();
            List<int> goalMinute = new List<int>();
            for (int i = 0; i < goalHistories.Count(); i++)
            {
                int minute = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes + (goalHistories[i].LivePeriod - 1) * 45;
                goalMinute.Add(minute);
            }

            goalMinute.Add(90);


            if (!DataStore.OverUnderScoreTimesV2.TryGetValue(matchId, out data)) return dataGrid;
            List<int> keys = data.Keys.OrderByDescending(item => item).ToList();

            for (int i = 0; i < goalMinute.Count; i++)
            {
                int toMinute = goalMinute[i];
                int fromMinute = 0;
                if (i > 0) fromMinute = (int)goalHistories[i - 1].TimeSpanFromStart.TotalMinutes + (goalHistories[i - 1].LivePeriod - 1) * 45;
                for (int j = 0; j < keys.Count; j++)
                {
                    List<int> row = data[keys[j]]
                        .Skip(fromMinute + 1)
                        .Take(toMinute - fromMinute)
                        .Select(item => item.Under)
                        .ToList();

                    if (!dataGrid.ContainsKey(keys[j]))
                    {
                        dataGrid.Add(keys[j], new List<int>());
                    }
                    dataGrid[keys[j]].Add(row.Count(item => item != 0));
                }
            }


            return dataGrid;
        }

        public static int GetCurrentMinutePlusTime(Match match)
        {
            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
            if (match.LivePeriod == 0 && !match.IsHT)
            {
                currentMinute = 0;
            }

            if (match.LivePeriod == 1)
            {
                currentMinute += 0;
            }
            else if (match.LivePeriod == 2)
            {
                currentMinute += 45;
            }

            if (currentMinute == 0 && match.HasHt)
            {
                return 0;
            }

            if (currentMinute == 0 && match.LivePeriod == 2)
            {
                return 0;
            }

            if (currentMinute < 0) return 0;

            return currentMinute;
        }

        public static int GetCurrentMinute(Match match)
        {
            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
            if (match.LivePeriod == 0 && !match.IsHT)
            {
                currentMinute = 0;
            }


            if (currentMinute > 45)
            {
                return 0;
            }

            if (match.LivePeriod == 1)
            {
                currentMinute += 0;
            }
            else if (match.LivePeriod == 2)
            {
                currentMinute += 45;
            }

            if (currentMinute == 0 && match.HasHt)
            {
                return 0;
            }

            if (currentMinute == 0 && match.LivePeriod == 2)
            {
                return 0;
            }

            if (currentMinute > 90) return 0;
            if (currentMinute < 1) return 0;

            return currentMinute;
        }

        //public static int GetCurrentMinute100ChangeLand(Match match)
        //{
        //    int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
        //    if (match.IsHT)
        //    {
        //        int restedMinutes = (int)DateTime.Now.Subtract(match.BeginHT).TotalMinutes;
        //        currentMinute = 45 + 10 + restedMinutes;
        //        if (currentMinute > 100)
        //        {
        //            currentMinute = 100;
        //        }
        //        return currentMinute;
        //    }


        //    if (match.LivePeriod == 1)
        //    {
        //        currentMinute += 0;
        //    }
        //    else if (match.LivePeriod == 2)
        //    {
        //        currentMinute += 45;
        //    }

        //    if (currentMinute == 0 && match.HasHt)
        //    {
        //        return 46;
        //    }

        //    if (currentMinute == 0 && match.LivePeriod == 2)
        //    {
        //        return 46;
        //    }

        //    if (currentMinute > 100) return 100;
        //    if (currentMinute < 1) return 0;

        //    return currentMinute;
        //}

        public static int GetCurrentMinute100(Match match)
        {
            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
            if (match.LivePeriod == 0 && !match.IsHT)
            {
                currentMinute = 0;
            }


            if (match.LivePeriod == 1)
            {
                currentMinute += 0;
            }
            else if (match.LivePeriod == 2)
            {
                currentMinute += 45;
            }

            if (currentMinute == 0 && match.HasHt)
            {
                return 46;
            }

            if (currentMinute == 0 && match.LivePeriod == 2)
            {
                return 46;
            }

            if (currentMinute > 100) return 100;
            if (currentMinute < 1) return 0;

            return currentMinute;
        }

        public static void UpdateProductStatus(Product product, string action)
        {
            if (!DataStore.Matchs.ContainsKey(product.MatchId)) return;
            Match match = DataStore.Matchs[product.MatchId];
            if (product.Bettype == Enums.BetType.FullTimeOverUnder)
            {
                if (action == "delete")
                {
                    DataStore.Matchs[product.MatchId].TotalOuLine--;
                }
                else if (action == "insert")
                {
                    DataStore.Matchs[product.MatchId].TotalOuLine++;
                }
            }

            if (product.Bettype == Enums.BetType.FullTime1x2
                && action == "insert")
            {
                int minuteDiff = (int)match.GlobalShowtime.Subtract(DateTime.Now).TotalMinutes;
                if (minuteDiff <= 5 && minuteDiff >= 2)
                {
                    DataStore.Matchs[product.MatchId].HasInsert1x2FtBeforelive = true;
                }
            }

            if (product.Bettype == Enums.BetType.FirstHalf1x2
                && action == "insert")
            {
                int minuteDiff = (int)match.GlobalShowtime.Subtract(DateTime.Now).TotalMinutes;
                if (minuteDiff <= 5 && minuteDiff >= 2)
                {
                    DataStore.Matchs[product.MatchId].HasInsert1x2FhBeforelive = true;
                }
            }


            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);

            


            if (match.LivePeriod == 0 && match.HasHt)
            {
                int restedMinute = (int)DateTime.Now.Subtract(match.BeginHT).TotalMinutes;
                currentMinute = restedMinute + 56;
            }
            if (match.LivePeriod == 0 && !match.IsHT)
            {
                currentMinute = 0;
            }

            if (currentMinute > 101) currentMinute = 100;
            if (currentMinute < 0) currentMinute = 0;


            if (!DataStore.ProductStatus.ContainsKey(match.MatchId))
            {
                DataStore.ProductStatus.Add(match.MatchId, new List<List<ProductStatusLog>>());
                for (int i=0; i<=101; i++)
                {
                    DataStore.ProductStatus[match.MatchId].Add(new List<ProductStatusLog>());
                }
            }

            Enums.BetType betType = product.Bettype;

            DataStore.ProductStatus[match.MatchId][currentMinute].Add(
                new ProductStatusLog()
                {
                    OddsId = product.OddsId,
                    ServerAction = action,
                    Status = product.OddsStatus,
                    Type = betType,
                    Hdp1 = product.Hdp1,
                    Hdp2 = product.Hdp2,
                    LivePeriod = match.LivePeriod,
                    IsHt = match.IsHT,
                    DtLog = DateTime.Now,
                    TotalOuLine = DataStore.Matchs[product.MatchId].TotalOuLine,
                    GlobalShowtime = match.GlobalShowtime,
                    Price = product.Odds1a100,
                    MaxBet = product.MaxBet,
                    TotalGoal = match.LiveHomeScore + match.LiveAwayScore
                }
            );

            if (!DataStore.MatchHasProductStatus.ContainsKey(product.MatchId))
            {
                DataStore.MatchHasProductStatus.Add(product.MatchId, DateTime.Now);
            }

            DataStore.MatchHasProductStatus[product.MatchId] = DateTime.Now;
            DataStore.Matchs[product.MatchId].LastimeHasNewProductAction = DateTime.Now;
        }

        public static void SaveToCsv<T>(List<T> reportData, string path)
        {
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.ToList().Select(x => x.Name));
            lines.Add(header);
            var valueLines = reportData.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            File.WriteAllLines(path, lines.ToArray());
        }

        public static bool CheckSpecialFilter(long matchId)
        {
            if (!DataStore.ProductStatus.ContainsKey(matchId)) return false;

            List<List<ProductStatusLog>> data = DataStore.ProductStatus[matchId];

            for (int i = 2; i <= 99; i++)
            {
                int realMinute = i;

                int countRunning = data[realMinute].Count(item => item.Status == "running");
                int countSuspend = data[realMinute].Count(item => item.Status == "suspend");
                int countClosePrice = data[realMinute].Count(item => item.Status == "closePrice");
                int countPrevious = data[realMinute - 1].Count(item => item.Status.Length > 0);
                int countNext = data[realMinute + 1].Count(item => item.Status.Length > 0);

                if (countPrevious == 0 
                    && countNext == 0 
                    && (
                        (countRunning == 1 && countSuspend == 1 && countClosePrice == 0) 
                        || (countRunning == 2 && countSuspend == 2 && countClosePrice == 0))
                    )
                {
                    return true;
                }

            }

            return false;
        }

        public static List<string> SumOverUnderScoreTimesFreqFt45(long matchId)
        {
            List<string> result = new List<string>() { "." };
            if (!DataStore.Matchs.ContainsKey(matchId)) return result;

            Dictionary<int, List<int>> data;
            if (!DataStore.OverUnderScoreTimesFreq.TryGetValue(matchId, out data))
            {
                return result;
            }
            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            scores = scores.OrderBy(item => item).ToList();

            if (scores.Count == 0) return result;
            List<int> sumRow = new List<int>();
            for (int i = 0; i <= 46; i++)
            {
                sumRow.Add(0);
            }

            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = new List<string>();
                row.Add(scores[i].ToString());
                if (data.ContainsKey(scores[i]))
                {
                    for (int j = 0; j <= 45; j++)
                    {
                        sumRow[j + 1] += data[scores[i]][j];
                    }
                }
            }
            result.AddRange(sumRow.ConvertAll<string>(x => x > 0 ? x.ToString() : "."));
            return result;

        }

        public static List<string> SumOverUnderScoreTimesFreqFt90(long matchId)
        {
            List<string> result = new List<string>() { "." };
            if (!DataStore.Matchs.ContainsKey(matchId)) return result;

            Dictionary<int, List<int>> data;
            if (!DataStore.OverUnderScoreTimesFreq.TryGetValue(matchId, out data))
            {
                return result;
            }
            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            scores = scores.OrderBy(item => item).ToList();

            if (scores.Count == 0) return result;
            List<int> sumRow = new List<int>();
            for (int i = 0; i <= 46; i++)
            {
                sumRow.Add(0);
            }

            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = new List<string>();
                row.Add(scores[i].ToString());
                if (data.ContainsKey(scores[i]))
                {
                    for (int j = 0; j <= 45; j++)
                    {
                        sumRow[j + 1] += data[scores[i]][j + 45];
                    }
                }
            }
            result.AddRange(sumRow.ConvertAll<string>(x => x > 0 ? x.ToString() : "."));
            return result;

        }

        public static List<string> SumOverUnderScoreTimesFreqFh45(long matchId)
        {
            List<string> result = new List<string>() { "." };
            if (!DataStore.Matchs.ContainsKey(matchId)) return result;

            Dictionary<int, List<int>> data;
            if (!DataStore.OverUnderScoreTimesFreqFh.TryGetValue(matchId, out data))
            {
                return result;
            }
            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            scores = scores.OrderBy(item => item).ToList();

            if (scores.Count == 0) return result;
            List<int> sumRow = new List<int>();
            for (int i = 0; i <= 46; i++)
            {
                sumRow.Add(0);
            }

            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = new List<string>();
                row.Add(scores[i].ToString());
                if (data.ContainsKey(scores[i]))
                {
                    for (int j = 0; j <= 45; j++)
                    {
                        sumRow[j + 1] += data[scores[i]][j];
                    }
                }
            }
            result.AddRange(sumRow.ConvertAll<string>(x => x > 0 ? x.ToString() : "."));
            return result;

        }

        public static List<string> GetProductIndex(long matchId)
        {
            List<string> result = new List<string>();

            List<ProductIdLog> products = DataStore.ProductIdsOfMatch[matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                .OrderBy(item => item.Minute)
                .ToList();

            List<Product> currentProducts = DataStore.Products.Values
                .Where(item => item.MatchId == matchId && item.Bettype == Enums.BetType.FullTimeOverUnder)
                .OrderByDescending(item => (item.Hdp1 + item.Hdp2))
                .ToList();

            for (int i = 0; i < currentProducts.Count; i++)
            {
                for (int j = 0; j < products.Count; j++)
                {
                    if (currentProducts[i].OddsId == products[j].OddsId)
                    {
                        if (products[j].Minute >= 2)
                        {
                            result.Add((j + 1).ToString() + "");
                        }
                        else
                        {
                            result.Add((j + 1).ToString());
                        }
                        
                        break;
                    }
                }
            }

            return result;
        }

        public static Color GetMainFormPcolor(string cellValue)
        {
            if (cellValue == null || cellValue.Length == 0 || !cellValue.Contains(""))
            {
                return Color.White;
            }
            List<Color> colors = new List<Color>() {
                Color.Gold,
                Color.GreenYellow,
                Color.SkyBlue,
                Color.LightGreen,
                Color.LightYellow,
                Color.LightPink,
                Color.DarkGray,
                Color.ForestGreen,
                Color.SteelBlue,
                Color.Orange,
                Color.Lime,
                Color.SeaGreen
            };
            int cellValueNumber = int.Parse(cellValue.Replace("", ""));
            int colorIndex = (cellValueNumber % colors.Count);
            return colors[colorIndex];
        }

        public static int DayOfFirstShow(DateTime firstShow)
        {
            DateTime newDt = firstShow.AddHours(-8);
            int result = newDt.Year * 10000 + newDt.Month * 100 + newDt.Day;
            return result;
        }

        public static int OverUnderLineAtMinute(int key, int minute, Dictionary<int, List<string>> dataOver, Dictionary<int, List<string>> dataUnder)
        {
            int overPrice = 0;
            int underPrice = 0;

            if (dataOver[key][minute].Length > 0
                && dataUnder[key][minute].Length > 0)
            {
                if (int.TryParse(dataOver[key][minute], out overPrice)
                    && int.TryParse(dataUnder[key][minute], out underPrice))
                {
                    return Common.Functions.GetOverUnderMoneyLinePrice(overPrice, underPrice);
                }
            }
            return 0;
        }

        public static void BackUpExcel()
        {
            List<Models.DataModels.Match> sampleSource = DataStore.Matchs.Values.Where(item =>
                item.Home != null
                && item.Away != null
                && item.League != null
                && item.League.Trim().Length > 0
                && !item.League.ToLower().Contains("saba soccer pingoal")
                && !item.League.ToLower().Contains("e-football")
                && !item.League.ToUpper().Contains(" - CORNERS")
                && !item.League.ToUpper().Contains(" - BOOKING")
                && !item.League.ToUpper().Contains("- WINNER")
                && !item.League.ToUpper().Contains("- TOTAL CORNER & TOTAL GOAL")
                && !item.League.ToUpper().Contains("- SUBSTITUTION")
                && !item.League.ToUpper().Contains("- GOAL KICK")
                && !item.League.ToUpper().Contains("- OFFSIDE")
                && !item.League.ToUpper().Contains("- THROW IN")
                && !item.League.ToUpper().Contains("- FREE KICK")
                && !item.League.ToUpper().Contains("- 1ST HALF VS 2ND HALF")
                && !item.League.ToUpper().Contains("- RED CARD")
                && !item.League.ToUpper().Contains("- OWN GOAL")
                && !item.League.ToUpper().Contains("- PENALTY")
                && !item.League.ToUpper().Contains("- TOTAL GOALS MINUTES")
                && !item.League.ToUpper().Contains("- WHICH TEAM TO KICK OFF")
                && !item.League.ToUpper().Contains("SABA")
                && !item.Home.ToLower().Contains("(pen)")
                //&& item.LivePeriod == -1
            )
            .OrderBy(item => item.GlobalShowtime)
            .ToList();

            List<Models.ViewModels.Match> matchs_view = AutoMapper.Mapper.Map<List<Models.ViewModels.Match>>(sampleSource);
            List<Models.ViewModels.MatchExport> matchs_export = AutoMapper.Mapper.Map<List<Models.ViewModels.MatchExport>>(matchs_view);
            string dtStr = DateTime.Now.ToString("yyyyMMdd");
            string folder = "DataExcel";
            string path = folder + "/" + dtStr + ".csv";
            System.IO.Directory.CreateDirectory(folder);
            SaveToCsv(matchs_export, path);

        }

        public static int CountClosePriceBeforeLive(long matchId, int fromMinute, int toMinute)
        {
            if (!DataStore.ProductStatus.ContainsKey(matchId)) return 0;

            List<List<ProductStatusLog>> data = DataStore.ProductStatus[matchId];

            return data[0].Count(item => 
                item.Status == "closePrice" 
                && item.GlobalShowtime.Subtract(item.DtLog).TotalMinutes >= fromMinute
                && item.GlobalShowtime.Subtract(item.DtLog).TotalMinutes <= toMinute
            );
        }

        public static bool HasActionInHalfTime(long matchId)
        {
            if (!DataStore.ProductStatus.ContainsKey(matchId)) return false;

            List<List<ProductStatusLog>> data = DataStore.ProductStatus[matchId];

            for (int i=0; i<data.Count; i++)
            {
                if (data[i].Any(item => item.IsHt))
                {
                    return true;
                }
            }

            return false;
        }

        public static int GetTeamStronger(long matchId)
        {
            List<Product> productsHandicaps =  DataStore.Products.Values.Where(item => item.Bettype == Enums.BetType.FullTimeHandicap && item.MatchId == matchId).ToList();
            

            long max = 0;
            Product productMax = null;
            for (int i=0; i<productsHandicaps.Count; i++)
            {
                long value = (long)((productsHandicaps[i].Odds1a100 > 0 ? productsHandicaps[i].Odds1a100 : -productsHandicaps[i].Odds1a100) * 100)
                    + (long)((productsHandicaps[i].Odds2a100 > 0 ? productsHandicaps[i].Odds2a100 : -productsHandicaps[i].Odds2a100) * 100);

                if (value > max)
                {
                    max = value;
                    productMax = productsHandicaps[i];
                }
            }

            if (productMax == null)
            {
                return 0;
            }

            if (productMax.Hdp1 > 0)
            {
                return 1;
            }
            else if (productMax.Hdp2 > 0)
            {
                return -1;
            }
            return 0;
        }

        public static string IndexOfProduct(List<ProductIdLog> products, string id)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].OddsId.ToString() == id)
                {
                    return (i + 1).ToString();
                }
            }
            return "";
        }

        public static string GetOverUnderMoneyLinesString(long matchid)
        {
            List<int> oneDimensionLines = new List<int>();
            if (DataStore.MatchOverUnderMoneyLine.ContainsKey(matchid))
            {
                List<List<int>> lines = DataStore.MatchOverUnderMoneyLine[matchid];

                for (int period = 1; period <= 2; period++)
                {
                    for (int minute = 1; minute <= 45; minute++)
                    {
                        if (lines[period][minute] != 0)
                        {
                            if (oneDimensionLines.Count == 0)
                            {
                                oneDimensionLines.Add(lines[period][minute]);
                            }
                            else
                            {
                                if (oneDimensionLines.Last() != lines[period][minute])
                                {
                                    oneDimensionLines.Add(lines[period][minute]);
                                }
                            }
                        }
                    }
                }
            }
            return string.Join(" ~ ", oneDimensionLines); ;
        }

        public static void InitBlackList()
        {
            List<string> blackList = new List<string>()
            {
                "ALBANIA 1ST DIVISION",
                "ALGERIA 1ST DIVISION",
                "ALGERIA 2ND DIVISION",
                "ALGERIA RESERVE LEAGUE 1",
                "ALGERIA RESERVE LEAGUE 2",
                "ARGENTINA PRIMERA B METROPOLITANA",
                "ARGENTINA PRIMERA B NACIONAL",
                "AUSTRALIA FOOTBALL QUEENSLAND PREMIER LEAGUE 2 U23",
                "AUSTRALIA VICTORIA PREMIER LEAGUE 3",
                "AUSTRIA 2ND LEAGUE",
                "AUSTRIA LANDESLIGA",
                "BELARUS RESERVE LEAGUE",
                "BELARUS WOMEN PREMIER LEAGUE",
                "BELGIUM CHALLENGER PRO LEAGUE",
                "BHUTAN PREMIER LEAGUE",
                "BULGARIA CUP",
                "BULGARIA SECOND PROFESSIONAL LEAGUE",
                "CAMBODIAN PREMIER LEAGUE (PLAY OFF)",
                "CHILE PRIMERA B",
                "CLUB FRIENDLY",
                "COLOMBIA PRIMERA B",
                "COSTA RICA CUP",
                "CROATIA HNL LEAGUE U19",
                "CYPRUS 1ST DIVISION",
                "CYPRUS 2ND DIVISION",
                "CYPRUS 3RD DIVISION",
                "CZECH REPUBLIC 1ST DIVISION",
                "CZECH REPUBLIC 3RD DIVISION CFL",
                "CZECH REPUBLIC 3RD DIVISION MSFL",
                "CZECH REPUBLIC U19 LEAGUE",
                "DENMARK 1ST DIVISION",
                "DENMARK SERIES",
                "DENMARK U19 LEAGUE",
                "ECUADOR SERIE B",
                "EGYPT PREMIER LEAGUE",
                "EGYPT SECOND LEAGUE",
                "ENGLISH CENTRAL RESERVE LEAGUE",
                "ENGLISH ISTHMIAN LEAGUE PREMIER DIVISION",
                "ENGLISH LEAGUE ONE",
                "ENGLISH LEAGUE TWO",
                "ENGLISH PREMIER LEAGUE 2 DIVISION 1",
                "ENGLISH PROFESSIONAL DEVELOPMENT LEAGUE U21",
                "ETHIOPIA PREMIER LEAGUE",
                "ESTONIA ESILIIGA",
                "ESTONIA ESILIIGA B",
                "FANTASY MATCH",
                "FINLAND KAKKONEN",
                "FINLAND WOMEN NATIONAL LEAGUE (PLAY OFF)",
                "FRANCE U19 LEAGUE",
                "GEORGIA EROVNULI LIGA 2",
                "GEORGIA EROVNULI LIGA",
                "GERMANY OBERLIGA",
                "GERMANY REGIONAL LEAGUE NORTH",
                "GERMANY REGIONAL LEAGUE NORTHEAST",
                "GERMANY REGIONAL LEAGUE SOUTHWEST",
                "GERMANY WOMEN BUNDESLIGA 2",
                "GREECE CUP",
                "GREECE SUPER LEAGUE 2",
                "GREECE SUPER LEAGUE U19",
                "GUATEMALA LIGA NATIONAL (PLAY OFF)",
                "HONG KONG 1ST DIVISION",
                "HONG KONG 2ND DIVISION",
                "HONG KONG RESERVE LEAGUE",
                "HONG KONG SAPLING CUP",
                "ICELAND 4TH DIVISION",
                "ICELAND WOMEN PREMIER LEAGUE",
                "INDIA MIZORAM PREMIER LEAGUE",
                "IRAN 2ND DIVISION",
                "IRAN AZADEGAN LEAGUEGHANA PREMIER LEAGUE",
                "ISRAEL LEUMIT LEAGUE (PLAY OFF)",
                "ISRAEL LIGA ALEF",
                "ISRAEL LIGA BET",
                "ISRAEL NATIONAL LEAGUE SOUTH U19",
                "ISRAEL PREMIER LEAGUE U19",
                "ISRAEL STATE CUP",
                "ITALY SERIE B",
                "ITALY SERIE C",
                "ITALY SERIE D",
                "JAMAICA PREMIER LEAGUE",
                "KENYA NATIONAL SUPER LEAGUE",
                "KENYA PREMIER LEAGUE",
                "KOREA K3 LEAGUE",
                "MOLDOVA LIGA 1 (PLAY OFF)",
                "MONTENEGRO SECOND LEAGUE",
                "MOROCCO BOTOLA 2",
                "NETHERLANDS WOMEN EREDIVISIE",
                "NIGERIA PROFESSIONAL FOOTBALL LEAGUENORWAY NATIONAL CHAMPIONS LEAGUE U19",
                "NORTHERN IRELAND PREMIERSHIP",
                "NORWAY 3RD DIVISION",
                "NORWAY NATIONAL CHAMPIONS LEAGUE U19",
                "OMAN PROFESSIONAL LEAGUE",
                "POLAND 2ND DIVISION",
                "POLAND 3RD DIVISION",
                "PORTUGAL LEAGUE CUP",
                "PORTUGAL LIGA 3",
                "ROMANIA 2ND DIVISION",
                "ROMANIA 2ND DIVISION (PLAY OFF)",
                "ROMANIA 3RD DIVISION (PLAY OFF)",
                "ROMANIA 3RD DIVISION",
                "RUSSIA 2ND DIVISION",
                "RUSSIA CUP",
                "RUSSIA YOUTH FOOTBALL LEAGUE U19 (PLAY OFF)",
                "SAUDI PREMIER LEAGUE U19",
                "SCOTLAND CHALLENGE CUP",
                "SCOTLAND RESERVE CUP",
                "SERBIA LEAGUE U19",
                "SLOVAKIA 2ND DIVISION",
                "SLOVAKIA LIGA 3",
                "SLOVENIA LEAGUE U19",
                "SOUTH AFRICA 1ST DIVISION",
                "SOUTH AUSTRALIA WOMEN PREMIER LEAGUE RESERVE",
                "SPAIN DIVISION DE HONOR U19",
                "SPAIN PRIMERA FEDERACION",
                "SPAIN SEGUNDA DIVISION",
                "SPAIN TERCERA FEDERACION",
                "SPAIN WOMEN PRIMERA DIVISION",
                "SPAIN WOMEN SEGUNDA FEDERACION",
                "SWEDEN ALLSVENSKAN U21",
                "TANZANIA PREMIER LEAGUE",
                "TURKEY ELITE LEAGUE A U19",
                "TURKEY SECOND LEAGUE",
                "UGANDA CUP",
                "UGANDA PREMIER LEAGUE",
                "URUGUAY RESERVE LEAGUE",
                "VIETNAM V LEAGUE 2",
                "WALES PREMIER LEAGUE",
                "WESTERN AUSTRALIA NATIONAL PREMIER LEAGUE U20",
                "WESTERN AUSTRALIA STATE LEAGUE DIVISION 1 RESERVE",
                "NORWAY 1ST DIVISION",
                "POLAND EKSTRAKLASA",
                "CZECH REPUBLIC 1ST DIVISION (PLAY OFF)",
                "FINLAND VEIKKAUSLIIGA",
                "SCOTLAND PREMIERSHIP (PLAY OFF)",
                "ENGLISH LEAGUE ONE (PLAY OFF)",
                "SOUTH AUSTRALIA STATE LEAGUE 1 RESERVE",
                "SOUTH AUSTRALIA PREMIER LEAGUE RESERVE",
                "AUSTRALIA VICTORIA STATE LEAGUE DIVISION 2 RESERVE",
                "AUSTRALIA VICTORIA STATE LEAGUE DIVISION 1 RESERVE",
                "AUSTRALIA FOOTBALL NEW SOUTH WALES LEAGUE 1 U20",
                "AUSTRALIA NEW SOUTH WALES PREMIER LEAGUE U20",
                "AUSTRALIA FOOTBALL NEW SOUTH WALES LEAGUE 1",
                "AUSTRALIA FOOTBALL QUEENSLAND PREMIER LEAGUE 1 U23",
                "BRAZIL CAMPEONATO PAULISTA U23 SEGUNDA DIVISION",
                "BRAZIL CAMPEONATO CATARINENSE U20",
                "BRAZIL CAMPEONATO PERNAMBUCANO U20",
                "BRAZIL SERIE D",
                "AUSTRALIA NORTHERN NEW SOUTH WALES PREMIER LEAGUE RESERVE",
                "AUSTRALIA CAPITAL NATIONAL PREMIER LEAGUE U23",
                "AUSTRALIA QUEENSLAND NATIONAL PREMIER LEAGUE U23",
                "AUSTRALIA CAPITAL NATIONAL PREMIER LEAGUE U23",
                "AUSTRALIA FOOTBALL QUEENSLAND PREMIER LEAGUE 3 - METRO",
                "ENGLISH LEAGUE CHAMPIONSHIP"
            };

            blackList = blackList.OrderBy(x => x).ToList();

            List<string> blackListNotActive = new List<string>()
            {
                "BHUTAN PREMIER LEAGUE",
                "CHILE PRIMERA B",
                "COLOMBIA PRIMERA B",
                "EGYPT PREMIER LEAGUE",
                "GREECE CUP",
                "ITALY SERIE B",
                "RUSSIA CUP",
                "BULGARIA CUP",
                "CLUB FRIENDLY",
                "DENMARK SERIES",
                "GEORGIA EROVNULI LIGA",
                "HONG KONG SAPLING CUP",
                "ISRAEL LIGA ALEF",
                "KENYA NATIONAL SUPER LEAGUE",
                "POLAND 2ND DIVISION",
                "ROMANIA 2ND DIVISION",
                "THAILAND LEAGUE 2",
                "ARMENIA FIRST LEAGUE",
                "KENYA NATIONAL SUPER LEAGUE",
                "POLAND WOMEN EKSTRALIGA",
                "AUSTRIA 2ND LEAGUE",
                "DENMARK 1ST DIVISION",
                "ECUADOR SERIE B",
                "ENGLISH LEAGUE ONE",
                "GERMANY WOMEN BUNDESLIGA 2",
                "GREECE SUPER LEAGUE 2",
                "NORWAY 3RD DIVISION",
                "RUSSIA 2ND DIVISION",
                "WALES PREMIER LEAGUE",
                "FINLAND VEIKKAUSLIIGA",
                "POLAND EKSTRAKLASA",
                "SPAIN SEGUNDA DIVISION",
                "ENGLISH LEAGUE CHAMPIONSHIP"
            };

            for (int i = 0; i < blackList.Count; i++)
            {
                if (!DataStore.Blacklist.Any(item => item.League.ToString().ToUpper() == blackList[i].ToUpper()))
                {

                    DataStore.Blacklist.Add(new BlackList()
                    {
                        Id = Guid.NewGuid(),
                        League = blackList[i].ToUpper(),
                        IsActive = !blackListNotActive.Contains(blackList[i].ToUpper())
                    }); ;
                }

            }
        }

    }
}
