using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LiveBetApp.Common.Enums;

namespace LiveBetApp.Services
{
    public class AlertService
    {
        private Dictionary<long, List<int>> WD13_CheckedHdp;
        private Dictionary<long, List<long>> WD20_A_Ids;
        private Dictionary<long, List<long>> WD34_MarkedHdps;
        private List<long> WD20_B_Ids;
        private List<long> WD20_C_Ids;
        private List<long> WD20_D_Ids;
        private List<long> WD20_E_Ids;
        private List<long> WD20_F_Ids;
        private List<long> WD20_G_Ids;
        private List<long> WD20_H_Ids;
        private List<long> WD20_I_Ids;

        public AlertService()
        {
            WD13_CheckedHdp = new Dictionary<long, List<int>>();
            WD20_A_Ids = new Dictionary<long, List<long>>();
            WD34_MarkedHdps = new Dictionary<long, List<long>>();
            WD20_B_Ids = new List<long>();
            WD20_C_Ids = new List<long>();
            WD20_D_Ids = new List<long>();
            WD20_E_Ids = new List<long>();
            WD20_F_Ids = new List<long>();
            WD20_G_Ids = new List<long>();
            WD20_H_Ids = new List<long>();
            WD20_I_Ids = new List<long>();

        }

        private void ExecuteCore(int milisecond)
        {
            if (DataStore.SemiAutoBetOnly) return;
            while (true)
            {
                try
                {
                    List<Match> matchs = DataStore.Matchs.Values.Where(item =>
                        item.Home != null
                        && item.League != null
                        && item.Away != null
                        && !item.League.Contains(" - ")
                        && !item.Home.ToLower().Contains("(pen)")
                        && !item.Home.ToLower().Contains("(r)")
                        && !(item.Home.ToLower().Contains("(w)") && item.League.ToLower().Contains("friendly"))
                        && (item.LivePeriod != 0 || item.IsHT)
                        && (
                            DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.All
                            || (DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.Live && item.HasStreaming)
                            || (DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.NoneLive && !item.HasStreaming)
                        )
                    )
                    .ToList();

                    List<Match> minimizedMatchs = new List<Match>();
                    for (int i = 0; i < matchs.Count; i++)
                    {
                        //if (CheckMinimize(matchs[i], matchs))
                        {
                            minimizedMatchs.Add(matchs[i]);
                        }
                    }

                    try { if (DataStore.AlertSetting[1]) UpdateAlert_WD1(minimizedMatchs); }
                    catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[2]) UpdateAlert_WD2(minimizedMatchs); }
                    catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[3]) UpdateAlert_WD3(minimizedMatchs); }
                    catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[4]) UpdateAlert_WD4(minimizedMatchs); }
                    catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[5]) UpdateAlert_WD5(minimizedMatchs); }
                    catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[6]) UpdateAlert_WD6(minimizedMatchs); }
                    catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[7]) UpdateAlert_WD7(minimizedMatchs); }
                    catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[8]) UpdateAlert_WD8(minimizedMatchs); }
                    catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[9]) UpdateAlert_WD9(minimizedMatchs); }
                    catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[10]) UpdateAlert_WD10(minimizedMatchs); }
                    catch (Exception ex) { }
                    //if (DataStore.AlertSetting[11]) UpdateAlert_WD11(minimizedMatchs);
                    try { if (DataStore.AlertSetting[12]) UpdateAlert_WD12(minimizedMatchs); }
                    catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[13]) UpdateAlert_WD13(minimizedMatchs); }
                    catch (Exception ex) { }
                    //try { if (DataStore.AlertSetting[14]) UpdateAlert_WD14(minimizedMatchs); }
                    //catch (Exception ex) { }
                    try { if (DataStore.AlertSetting[15]) UpdateAlert_WD15(minimizedMatchs); }
                    catch (Exception ex) { }
                    //UpdateAlert_WD16(minimizedMatchs);
                    //if (DataStore.AlertSetting[17]) UpdateAlert_WD17(minimizedMatchs);
                    try { if (DataStore.AlertSetting[18]) UpdateAlert_WD18(); }
                    catch (Exception ex) { }

                    //try { if (DataStore.AlertSetting[19]) UpdateAlert_WD19(minimizedMatchs); }
                    //catch (Exception ex) { }

                    //try { if (DataStore.AlertSetting[20]) UpdateAlert_WD20(minimizedMatchs); }
                    //catch (Exception ex) { }

                    try { UpdateAlert_WD22(minimizedMatchs); }
                    catch (Exception ex) { }

                    try { UpdateAlert_WD23(minimizedMatchs); }
                    catch (Exception ex) { }

                    try { UpdateAlert_Wd24(minimizedMatchs); }
                    catch (Exception ex) { }

                    try { UpdateAlert_Wd25(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd26(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd27(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd28(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd29(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd30(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd31(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd33(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd34(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd35(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd36(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd37(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd38(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd39(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd40(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd41(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd42(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd43(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd44(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd45(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                    try { UpdateAlert_Wd46(minimizedMatchs); }
                    catch (Exception ex)
                    { }
                    //


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Thread.Sleep(milisecond);
                }

            }
        }

        private void UpdateCountBeforeCalculate()
        {

        }

        private void UpdateCountAfterCalculate()
        {

        }

        public void Execute(int milisecond)
        {
            Thread coreServiceThread;
            coreServiceThread = new Thread(() =>
            {
                ExecuteCore(milisecond);
            });
            coreServiceThread.IsBackground = true;
            coreServiceThread.Start();
        }

        private bool CheckMinimize(Match match, List<Match> matchs)
        {
            return matchs.Count(item => item.League == match.League && Math.Abs(match.KickoffTime.Subtract(item.KickoffTime).TotalMinutes) > 5) <= 5;
        }

        private string CheckUpdateAlert_WD1(Match match)
        {
            string result = "";
            if (match.IsHT && (match.LiveAwayScore + match.LiveHomeScore) == 0)
            {
                int openMax = Common.Functions.GetHdpsOpenMax(match.MatchId);
                int calculatedOpenMaxNgl = Common.Functions.GetNglByOpenMax(openMax);
                int realOpenMaxNgl = Common.Functions.GetHdpOpenMaxOfNgl(match.MatchId);

                if (realOpenMaxNgl > calculatedOpenMaxNgl)
                {
                    result = "red_normal_";
                }
                else if (realOpenMaxNgl < calculatedOpenMaxNgl)
                {
                    result = "red_bold_";
                }
                else
                {
                    result = "?_?_";
                }

                if (openMax - realOpenMaxNgl <= 75)
                {
                    result += "blue";
                }
                else if (openMax - realOpenMaxNgl >= 100)
                {
                    result += "yellow";
                }
                else
                {
                    result += "?";
                }

            }
            return result;
        }
        private void UpdateAlert_WD1(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);

            for (int i = 0; i < localMatchs.Count; i++)
            {
                string result = CheckUpdateAlert_WD1(localMatchs[i]);

                if (result.Length == 0
                    && DataStore.Alert_Wd1.ContainsKey(localMatchs[i].MatchId))
                {
                    DataStore.Alert_Wd1.Remove(localMatchs[i].MatchId);
                    continue;
                }

                if (result.Length > 0 && !DataStore.Alert_Wd1.ContainsKey(localMatchs[i].MatchId))
                {
                    DataStore.Alert_Wd1.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = result });
                }
            }

            List<long> matchIds = DataStore.Alert_Wd1.Keys.ToList();

            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                DataStore.Matchs.TryGetValue(matchIds[i], out match);
                if (match == null
                    || match.LivePeriod == 2)
                {
                    DataStore.Alert_Wd1.Remove(matchIds[i]);
                }
            }

        }

        private bool CheckUpdateAlert_WD2(Match match)
        {
            return match.LivePeriod == 2 && (match.LiveAwayScore + match.LiveHomeScore) == 0;
        }

        private void UpdateAlert_WD2(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (CheckUpdateAlert_WD2(localMatchs[i]))
                {
                    if (!DataStore.Alert_Wd2.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd2.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                }
            }

            List<long> matchIds = DataStore.Alert_Wd2.Keys.ToList();

            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                DataStore.Matchs.TryGetValue(matchIds[i], out match);
                if (match == null
                    || match.LivePeriod == -1
                    || !CheckUpdateAlert_WD2(match))
                {
                    DataStore.Alert_Wd2.Remove(matchIds[i]);
                }
            }

        }

        private int CheckUpdateAlert_WD3(Match match)
        {
            int currentMaxHdp = Common.Functions.GetRealTimeMaxHdp(match);
            int openMax = Common.Functions.GetHdpsOpenMax(match.MatchId);
            int totalGoal = match.LiveAwayScore + match.LiveHomeScore;

            if (match.FirstShow <= match.GlobalShowtime)
            {
                if (match.LivePeriod == 2
                    && totalGoal == 0)
                {
                    if (openMax - currentMaxHdp == 75) return 1;
                    else if (openMax - currentMaxHdp == 100) return 2;
                    else if (openMax - currentMaxHdp == 125) return 3;
                }
                else if (match.LivePeriod == 2 && totalGoal <= openMax) return 4;
            }

            return 0;
        }

        private void UpdateAlert_WD3(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);

            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    int checkResult = CheckUpdateAlert_WD3(localMatchs[i]);
                    if (checkResult > 0)
                    {
                        if (!DataStore.Alert_Wd3.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd3.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = checkResult.ToString() });
                        }
                        else
                        {
                            DataStore.Alert_Wd3[localMatchs[i].MatchId].CustomValue = checkResult.ToString();
                        }
                    }
                }
                catch
                {

                }

            }

            List<long> matchIds = DataStore.Alert_Wd3.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if ((DataStore.Matchs.TryGetValue(matchIds[i], out match) && match.LivePeriod == -1))
                {
                    DataStore.Alert_Wd3.Remove(matchIds[i]);
                }
            }

        }

        private bool CheckUpdateAlert_WD4_WD5(Match match, int hdp)
        {
            int openMax = Common.Functions.GetHdpsOpenMax(match.MatchId);
            if (match.LivePeriod == 2
                && ((match.LiveAwayScore + match.LiveHomeScore) * 100) <= openMax
                && DataStore.Products.Values.Any(item =>
                    item.MatchId == match.MatchId
                    && item.Bettype == Enums.BetType.FullTimeOverUnder
                    && ((item.Hdp1 * 100) - match.LiveHomeScore - match.LiveAwayScore) == hdp)
                )
            {
                return true;
            }

            return false;

        }

        private void UpdateAlert_WD4(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    if (CheckUpdateAlert_WD4_WD5(localMatchs[i], 100) &&
                        !DataStore.Alert_Wd4.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd4.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                }
                catch
                {

                }
            }

            List<long> matchIds = DataStore.Alert_Wd4.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if (DataStore.Matchs.TryGetValue(matchIds[i], out match)
                    && !CheckUpdateAlert_WD4_WD5(match, 100))
                {
                    DataStore.Alert_Wd4.Remove(matchIds[i]);
                }
            }

        }

        private void UpdateAlert_WD5(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    if (CheckUpdateAlert_WD4_WD5(localMatchs[i], 50)
                        && !DataStore.Alert_Wd5.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd5.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                }
                catch
                {

                }

            }

            List<long> matchIds = DataStore.Alert_Wd5.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if (DataStore.Matchs.TryGetValue(matchIds[i], out match))
                {
                    DataStore.Alert_Wd5.Remove(matchIds[i]);
                }
            }
        }

        private bool CheckUpdateAlert_WD6(Match match)
        {
            List<Product> productsOfMatch = DataStore.Products.Values.Where(item => item.MatchId == match.MatchId && item.Bettype == Enums.BetType.FullTimeOverUnder).ToList();
            return productsOfMatch.Count == 1 && (match.LivePeriod >= 1 || match.IsHT);
        }
        private void UpdateAlert_WD6(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    if (CheckUpdateAlert_WD6(localMatchs[i]) &&
                        !DataStore.Alert_Wd6.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd6.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                }
                catch
                {

                }

            }

            //List<long> matchIds = DataStore.Alert_Wd6.Keys.ToList();
            //for (int i = 0; i < matchIds.Count; i++)
            //{
            //    Match match;
            //    if ((DataStore.Matchs.TryGetValue(matchIds[i], out match) && match.LivePeriod == -1))
            //    {
            //        DataStore.Alert_Wd6.Remove(matchIds[i]);
            //    }
            //}
        }

        private bool Alert_WD7_CheckAtMinute(Dictionary<int, List<OverUnderScoreTimesV2Item>> data, int minute)
        {
            List<int> scores = data.Keys.ToList();
            for (int j = 0; j < scores.Count; j++)
            {
                if (data[scores[j]][minute].Over != 0)
                {
                    return false;
                }
            }
            return true;
        }
        private int CheckUpdateAlert_WD7(Match match)
        {
            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(match.MatchId)
                || (match.LivePeriod == -1)
                || match.FirstShow > match.GlobalShowtime) return 0;

            Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[match.MatchId];

            int count = 0;
            for (int i = 3; i <= 90; i++)
            {
                if (!Alert_WD7_CheckAtMinute(data, i)
                    && Alert_WD7_CheckAtMinute(data, i - 1)
                    && Alert_WD7_CheckAtMinute(data, i - 2))
                {
                    count++;
                }
            }

            return count;
        }
        private void UpdateAlert_WD7(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            for (int i = 0; i < localMatchs.Count; i++)
            {
                int count = CheckUpdateAlert_WD7(localMatchs[i]);
                if (count > 0
                    && !DataStore.Alert_Wd7.ContainsKey(localMatchs[i].MatchId))
                {
                    DataStore.Alert_Wd7.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = count.ToString() });
                }
            }

            List<long> matchIds = DataStore.Alert_Wd7.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if ((DataStore.Matchs.TryGetValue(matchIds[i], out match) && match.LivePeriod == -1))
                {
                    DataStore.Alert_Wd7.Remove(matchIds[i]);
                }
            }

        }

        private int CheckUpdateAlert_WD8(Match match)
        {
            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(match.MatchId)) return 0;
            List<int> keys = DataStore.OverUnderScoreTimesV3[match.MatchId].Keys.ToList();
            int count = 0;
            for (int i = 0; i < keys.Count; i++)
            {
                List<OverUnderScoreTimesV2Item> data = DataStore.OverUnderScoreTimesV3[match.MatchId][keys[i]];
                for (int j = 1; j <= 89; j++)
                {
                    if (data[j].Under != 0)
                    {
                        if (data[j].Under < 0 && data[j + 1].Under > 0)
                        {
                            count++;
                        }
                        break;
                    }
                }
            }

            return count;
        }
        private void UpdateAlert_WD8(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);

            for (int i = 0; i < localMatchs.Count; i++)
            {
                int count = CheckUpdateAlert_WD8(localMatchs[i]);
                if (count > 0
                    && !DataStore.Alert_Wd8.ContainsKey(localMatchs[i].MatchId))
                {
                    DataStore.Alert_Wd8.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = count.ToString() });
                }

            }

            List<long> matchIds = DataStore.Alert_Wd8.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if ((DataStore.Matchs.TryGetValue(matchIds[i], out match) && match.LivePeriod == -1))
                {
                    DataStore.Alert_Wd8.Remove(matchIds[i]);
                }
            }
        }


        private bool CheckUpdateAlert_WD9(Match match)
        {
            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(match.MatchId)) return false;
            List<int> keys = DataStore.OverUnderScoreTimesV3[match.MatchId].Keys.ToList();

            int curentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes) + (match.LivePeriod - 1) * 45;

            if (curentMinute <= 0
                || curentMinute == 1
                || curentMinute == 46
                || curentMinute > 90) return false;

            for (int i = 0; i < keys.Count; i++)
            {
                List<OverUnderScoreTimesV2Item> data = DataStore.OverUnderScoreTimesV3[match.MatchId][keys[i]];
                OverUnderScoreTimesV2Item current = data[curentMinute];
                OverUnderScoreTimesV2Item previous = data[curentMinute - 1];

                if (previous.Over != 0
                    && current.Over != 0
                    && Common.Functions.FastOverUnderPriceStep(previous.Over, current.Over))
                {
                    return true;
                }
            }

            return false;
        }

        private int CountAlert_WD9(Match match)
        {
            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(match.MatchId)) return 0;
            List<int> keys = DataStore.OverUnderScoreTimesV3[match.MatchId].Keys.ToList();

            int curentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes) + (match.LivePeriod - 1) * 45;

            int count = 0;

            if (curentMinute <= 0
                || curentMinute == 1
                || curentMinute == 46
                || curentMinute > 90) return count;



            for (int i = 0; i < keys.Count; i++)
            {
                List<OverUnderScoreTimesV2Item> data = DataStore.OverUnderScoreTimesV3[match.MatchId][keys[i]];
                OverUnderScoreTimesV2Item current = data[curentMinute];
                OverUnderScoreTimesV2Item previous = data[curentMinute - 1];

                if (previous.Over != 0
                    && current.Over != 0
                    && Common.Functions.FastOverUnderPriceStep(previous.Over, current.Over))
                {
                    count++;
                }
            }

            return count;
        }

        private void UpdateAlert_WD9(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    if (CheckUpdateAlert_WD9(localMatchs[i]))
                    {
                        Alert oldAlert;
                        if (!DataStore.Alert_Wd9.TryGetValue(localMatchs[i].MatchId, out oldAlert))
                        {
                            DataStore.Alert_Wd9.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = "1" });
                        }
                        else
                        {
                            int count = CountAlert_WD9(localMatchs[i]);
                            if (count > int.Parse(oldAlert.CustomValue))
                            {
                                DataStore.Alert_Wd9[localMatchs[i].MatchId].CustomValue = count.ToString();
                                DataStore.Alert_Wd9[localMatchs[i].MatchId].IsNew = true;
                            }
                        }
                    }
                }
                catch
                {

                }
            }

            List<long> matchIds = DataStore.Alert_Wd9.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if ((DataStore.Matchs.TryGetValue(matchIds[i], out match) && match.LivePeriod == -1))
                {
                    DataStore.Alert_Wd9.Remove(matchIds[i]);
                }
            }

        }

        private bool CheckUpdateAlert_WD10(Match match)
        {
            List<Product> productFullTimeHandicapOfThisMatch = DataStore.Products.Values
                .Where(item =>
                    item.MatchId == match.MatchId
                    && item.Bettype == Enums.BetType.FullTimeHandicap
                )
                .ToList();
            return productFullTimeHandicapOfThisMatch.Any(item => (item.Hdp1 * 100) >= 300 || (item.Hdp2 * 100) >= 300)
                && match.LivePeriod == 1
                && match.TimeSpanFromStart.TotalMinutes <= 5;
        }

        private void UpdateAlert_WD10(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    if (CheckUpdateAlert_WD10(localMatchs[i]) &&
                        !DataStore.Alert_Wd10.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd10.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                }
                catch
                {

                }

            }

            List<long> matchIds = DataStore.Alert_Wd10.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if ((DataStore.Matchs.TryGetValue(matchIds[i], out match) && match.LivePeriod == -1))
                {
                    DataStore.Alert_Wd10.Remove(matchIds[i]);
                }
            }

        }

        private bool CheckUpdateAlert_WD11(Match match)
        {
            return match.HomeRed + match.AwayRed > 0
                && DataStore.PenHistories.ContainsKey(match.MatchId)
                && DataStore.PenHistories[match.MatchId].Count > 0;
        }
        private void UpdateAlert_WD11(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (CheckUpdateAlert_WD11(localMatchs[i]))
                {
                    int currentMinute = (int)localMatchs[i].TimeSpanFromStart.TotalMinutes;
                    if (!DataStore.Alert_Wd11.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd11.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = currentMinute.ToString() });
                    }
                    else
                    {

                    }
                }

            }

            List<long> matchIds = DataStore.Alert_Wd11.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if ((DataStore.Matchs.TryGetValue(matchIds[i], out match) && match.LivePeriod == -1))
                {
                    DataStore.Alert_Wd11.Remove(matchIds[i]);
                }
            }

        }

        private bool CheckRed_Wd12(List<int> currentHdps, Dictionary<int, List<OverUnderScoreTimesV2Item>> data)
        {
            List<int> keys = data.Keys.Where(item => currentHdps.Count(p => p == item) == 0).ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                if (data[keys[i]].Count(item => item.Over < 0) == 0
                    && data[keys[i]].Count(item => item.Over > 0) > 0)
                {
                    return true;
                }
            }
            return false;
        }


        private int CheckUpdateAlert_WD12(Match match)
        {
            if (DataStore.OverUnderScoreTimesV3.ContainsKey(match.MatchId))
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[match.MatchId];
                List<int> openHdps = Common.Functions.GetOpenHdps(match.MatchId);
                List<int> currentHdps = Common.Functions.GetCurrentHdps(match.MatchId);

                if (CheckRed_Wd12(currentHdps, data)) return 2;

                List<int> keys = data.Keys.Where(item =>
                    openHdps.Count(p => p == item) == 0
                    && currentHdps.Count(p => p == item) == 0
                ).ToList();

                for (int i = 0; i < keys.Count; i++)
                {
                    if (data[keys[i]].Count(item => item.Over != 0) <= 11)
                    {
                        return 1;
                    }
                }
            }

            return 0;
        }
        private void UpdateAlert_WD12(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs.ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                int color = CheckUpdateAlert_WD12(localMatchs[i]);
                if (color > 0
                    && !DataStore.Alert_Wd12.ContainsKey(localMatchs[i].MatchId))
                {
                    DataStore.Alert_Wd12.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = color.ToString() });
                }
            }

            List<long> matchIds = DataStore.Alert_Wd12.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if ((DataStore.Matchs.TryGetValue(matchIds[i], out match) && match.LivePeriod == -1))
                {
                    DataStore.Alert_Wd12.Remove(matchIds[i]);
                }
            }

        }

        private int CheckUpdateAlert_WD14_Red(Match match)
        {
            int result = 0;

            if (DataStore.OverUnderScoreTimesV3.ContainsKey(match.MatchId))
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[match.MatchId];
                List<int> keys = data.Keys.OrderByDescending(item => item).ToList();

                for (int minute = 2; minute < 90; minute++)
                {
                    for (int i = 0; i < keys.Count - 1; i++)
                    {
                        if (data[keys[i]][minute + 1].Over == 0
                            && data[keys[i]][minute].Over != 0
                            && data[keys[i + 1]][minute + 1].Over != 0)
                        {
                            for (int j = 0; j < keys.Count - 2; i++)
                            {
                                if (data[keys[j]][minute].Over != 0
                                    && data[keys[j + 1]][minute].Over != 0

                                    && data[keys[j + 1]][minute + 1].Over != 0
                                    && data[keys[j + 2]][minute + 1].Over != 0)
                                {
                                    int currentSubtract = Common.Functions.CalculateSubtractValue(data[keys[j]][minute].Over, data[keys[j + 1]][minute].Over, match.OverUnderMoneyLine);
                                    int previousSubtract = Common.Functions.CalculateSubtractValue(data[keys[j + 1]][minute + 1].Over, data[keys[j + 2]][minute + 1].Over, match.OverUnderMoneyLine);
                                    if (Math.Abs(currentSubtract - previousSubtract) >= 10)
                                    {
                                        result++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        private int CheckUpdateAlert_WD14_Blue(Match match)
        {
            int result = 0;

            if (DataStore.OverUnderScoreTimesV3.ContainsKey(match.MatchId))
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[match.MatchId];
                List<int> keys = data.Keys.OrderByDescending(item => item).ToList();
                for (int i = 0; i < keys.Count - 1; i++)
                {
                    for (int minute = 2; minute <= 90; minute++)
                    {
                        if (data[keys[i]][minute].Over != 0
                            && data[keys[i]][minute - 1].Over != 0
                            && data[keys[i + 1]][minute].Over != 0
                            && data[keys[i + 1]][minute - 1].Over != 0)
                        {
                            int currentSubtract = Common.Functions.CalculateSubtractValue(data[keys[i]][minute].Over, data[keys[i + 1]][minute].Over, match.OverUnderMoneyLine);
                            int previousSubtract = Common.Functions.CalculateSubtractValue(data[keys[i]][minute - 1].Over, data[keys[i + 1]][minute - 1].Over, match.OverUnderMoneyLine);
                            if (Math.Abs(currentSubtract - previousSubtract) >= 7)
                            {
                                result++;
                            }
                        }

                    }
                }
            }

            return result;
        }

        private void UpdateAlert_WD14(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                int countRed = CheckUpdateAlert_WD14_Red(localMatchs[i]);
                int countblue = CheckUpdateAlert_WD14_Blue(localMatchs[i]);
                if (countRed + countblue > 0)
                {
                    if (!DataStore.Alert_Wd14.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd14.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = (countRed + countblue).ToString() });
                    }
                    else DataStore.Alert_Wd14[localMatchs[i].MatchId].CustomValue = (countRed + countblue).ToString();
                }

            }

            List<long> matchIds = DataStore.Alert_Wd14.Keys.ToList();

            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if (DataStore.Matchs.TryGetValue(matchIds[i], out match)
                    && match.LivePeriod == -1
                    && DataStore.Alert_Wd14.ContainsKey(matchIds[i]))
                {
                    DataStore.Alert_Wd14.Remove(matchIds[i]);
                }
            }

        }

        private LiveBetApp.Common.Enums.AlertWd15 CheckUpdateAlert_WD15(Match match)
        {
            if (DataStore.Excel4th.ContainsKey(match.MatchId))
            {
                Dictionary<int, OverUnderSummary> data = DataStore.Excel4th[match.MatchId];

                List<int> keys = data.Keys.OrderByDescending(item => item).ToList();
                int maxHdpCol1 = 0;
                int maxHdpCol2 = 0;
                int maxHdpCol3 = 0;
                int maxHdpCol4 = 0;

                if (data.Values.Count(item => item.VeryFirstPrice.Over != 0) > 0)
                {
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (data[keys[i]].VeryFirstPrice.Over != 0
                            && maxHdpCol1 == 0)
                        {
                            maxHdpCol1 = keys[i];
                        }

                        if (data[keys[i]].At90BeforeLive.Over != 0
                            && maxHdpCol2 == 0)
                        {
                            maxHdpCol2 = keys[i];
                        }

                        if (data[keys[i]].BeforeLivePrice.Over != 0
                            && maxHdpCol3 == 0)
                        {
                            maxHdpCol3 = keys[i];
                        }

                        if (data[keys[i]].FirstPriceInLive.Over != 0
                            && maxHdpCol4 == 0)
                        {
                            maxHdpCol4 = keys[i];
                        }

                        if (maxHdpCol2 != 0
                            && maxHdpCol3 != 0
                            && maxHdpCol4 != 0) break;
                    }

                    if (maxHdpCol1 != 0
                        && maxHdpCol2 != 0
                        && maxHdpCol3 != 0
                        && maxHdpCol4 != 0)
                    {
                        if (maxHdpCol4 > maxHdpCol1
                            && maxHdpCol4 > maxHdpCol2
                            && maxHdpCol4 > maxHdpCol3) return Enums.AlertWd15.LightYellow;
                        else if (maxHdpCol4 < maxHdpCol3
                            && maxHdpCol4 > maxHdpCol1
                            && maxHdpCol4 > maxHdpCol2) return Enums.AlertWd15.LightGreen;
                        else if (maxHdpCol4 < maxHdpCol1
                            || maxHdpCol4 < maxHdpCol2
                            || maxHdpCol4 < maxHdpCol3) return Enums.AlertWd15.LightPink;

                        return Enums.AlertWd15.None;
                    }
                }
                else
                {
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (data[keys[i]].At90BeforeLive.Over != 0
                            && maxHdpCol2 == 0)
                        {
                            maxHdpCol2 = keys[i];
                        }

                        if (data[keys[i]].BeforeLivePrice.Over != 0
                            && maxHdpCol3 == 0)
                        {
                            maxHdpCol3 = keys[i];
                        }

                        if (data[keys[i]].FirstPriceInLive.Over != 0
                            && maxHdpCol4 == 0)
                        {
                            maxHdpCol4 = keys[i];
                        }

                        if (maxHdpCol2 != 0
                            && maxHdpCol3 != 0
                            && maxHdpCol4 != 0) break;
                    }

                    if (maxHdpCol2 != 0
                            && maxHdpCol3 != 0
                            && maxHdpCol4 != 0)
                    {
                        if (maxHdpCol2 == maxHdpCol3
                            && maxHdpCol3 != maxHdpCol4) return Enums.AlertWd15.SkyBlue;

                        if (maxHdpCol3 == maxHdpCol4
                            && maxHdpCol3 != maxHdpCol2) return Enums.AlertWd15.SkyBlue;
                    }

                }
            }

            return Enums.AlertWd15.None;
        }
        private void UpdateAlert_WD15(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);

            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    Enums.AlertWd15 color = CheckUpdateAlert_WD15(localMatchs[i]);
                    if (color == Enums.AlertWd15.None)
                    {
                        if (DataStore.Alert_Wd15.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd15.Remove(localMatchs[i].MatchId);
                        }
                        continue;
                    }

                    if (!DataStore.Alert_Wd15.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd15.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = color.ToString() });
                    }
                    else
                    {
                        DataStore.Alert_Wd15[localMatchs[i].MatchId].CustomValue = color.ToString();
                    }
                }
                catch
                {

                }

            }

            List<long> matchIds = DataStore.Alert_Wd15.Keys.ToList();

            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if (DataStore.Matchs.TryGetValue(matchIds[i], out match)
                    && match.LivePeriod == -1
                    && DataStore.Alert_Wd15.ContainsKey(matchIds[i]))
                {
                    DataStore.Alert_Wd15.Remove(matchIds[i]);
                }
            }

        }

        private bool CheckUpdateAlert_WD16(Match match)
        {
            int hdp = 100;

            if (match.LivePeriod == 2
                && DataStore.OverUnderScoreTimes.ContainsKey(match.MatchId)
                && DataStore.OverUnderScoreTimes[match.MatchId].ContainsKey(hdp)
                && DataStore.OverUnderScoreTimes[match.MatchId][hdp].Count(item => item.Length > 0) == 3
                && DataStore.OverUnderScoreHalfTimes.ContainsKey(match.MatchId)
                && !DataStore.OverUnderScoreHalfTimes[match.MatchId].ContainsKey(hdp))
            {
                return true;
            }
            return false;

        }
        private void UpdateAlert_WD16(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .Where(item => CheckUpdateAlert_WD16(item))
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (!DataStore.Alert_Wd16.ContainsKey(localMatchs[i].MatchId))
                {
                    DataStore.Alert_Wd16.Add(localMatchs[i].MatchId, new Alert(false, true));
                }
                else
                {

                }
            }

            List<long> matchIds = DataStore.Alert_Wd16.Keys.ToList();

            for (int i = 0; i < matchIds.Count; i++)
            {
                if (DataStore.Alert_Wd16[matchIds[i]].TimeSpanFromCreate.TotalMinutes >= 15)
                {
                    DataStore.Alert_Wd16[matchIds[i]].Deleted = true;
                }
            }

        }

        private int CheckUpdateAlert_WD17(Match match)
        {
            int openMaxHalftime = Common.Functions.GetHdpOpenMaxOfNgl(match.MatchId);
            int openMinHalftime = Common.Functions.GetHdpOpenMinOfNgl(match.MatchId);

            Dictionary<int, List<OverUnderScoreTimesV3Item>> data;

            if (DataStore.OverUnderScoreHalftime.TryGetValue(match.MatchId, out data))
            {
                int maxKey = data.Keys.Max();
                int minKey = data.Keys.Min();

                if (maxKey > openMaxHalftime) return 1;
                if (openMinHalftime > minKey) return 2;
            }
            return 0;
        }
        private void UpdateAlert_WD17(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);

            for (int i = 0; i < localMatchs.Count; i++)
            {
                int checkResult = CheckUpdateAlert_WD17(localMatchs[i]);
                if (checkResult > 0 &&
                    !DataStore.Alert_Wd17.ContainsKey(localMatchs[i].MatchId))
                {
                    DataStore.Alert_Wd17.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = checkResult.ToString() });
                }
            }

            List<long> matchIds = DataStore.Alert_Wd17.Keys.ToList();

            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if (DataStore.Matchs.TryGetValue(matchIds[i], out match)
                    && match.LivePeriod == -1
                    && DataStore.Alert_Wd17.ContainsKey(matchIds[i]))
                {
                    DataStore.Alert_Wd17.Remove(matchIds[i]);
                }
            }

        }

        private int CheckUpdateAlert_WD13(Match match)
        {
            //tx là số phút từ khi vào xỉu đến lúc thoát được tài
            //khi tx của 2 kèo kế bên cách nhau >= 3 

            //DataStore.OverUnderScoreTimesV3[_matchId][hdp];
            int count = 0;
            if (DataStore.MatchIuooValue_1.ContainsKey(match.MatchId))
            {
                Dictionary<int, IuooValue> data = DataStore.MatchIuooValue_1[match.MatchId];
                List<int> keys = data.Keys.OrderByDescending(item => item).ToList();
                for (int i = 1; i < keys.Count; i++)
                {
                    if (Math.Abs(data[keys[i]].TX - data[keys[i - 1]].TX) >= 3)
                    {
                        count++;
                    }
                }
            }

            if (DataStore.MatchIuooValue_2.ContainsKey(match.MatchId))
            {
                Dictionary<int, IuooValue> data = DataStore.MatchIuooValue_2[match.MatchId];
                List<int> keys = data.Keys.OrderByDescending(item => item).ToList();
                for (int i = 1; i < keys.Count; i++)
                {
                    if (Math.Abs(data[keys[i]].TX - data[keys[i - 1]].TX) >= 3)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private int FindMaxHdpOfIuoo(long matchId, int fromMinute, int limitMinute, List<int> Hdps)
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

        private void UpdateAlert_WD13(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                int count = CheckUpdateAlert_WD13(localMatchs[i]);
                if (count > 0
                    && !DataStore.Alert_Wd13.ContainsKey(localMatchs[i].MatchId))
                {
                    DataStore.Alert_Wd13.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = count.ToString() });
                }
            }

            List<long> matchIds = DataStore.Alert_Wd13.Keys.ToList();

            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if (DataStore.Matchs.TryGetValue(matchIds[i], out match)
                    && match.LivePeriod == -1
                    && DataStore.Alert_Wd13.ContainsKey(matchIds[i]))
                {
                    DataStore.Alert_Wd13.Remove(matchIds[i]);
                }
            }

        }

        //private void UpdateAlert_WD18()
        //{
        //    List<Match> matchs = DataStore.Matchs.Values.Where(item => item.LivePeriod == 1
        //        && !item.IsHT
        //        && item.IsMainMarket
        //        && item.TimeSpanFromStart.TotalMinutes <= 5
        //        && !item.League.Contains(" - ")
        //        && !item.League.ToUpper().Contains("SABA")
        //        && !item.Home.ToLower().Contains("(pen)")
        //    ).ToList();

        //    for (int i = 0; i < matchs.Count; i++)
        //    {
        //        if (DataStore.FinishedBetByHdpPrice.Any(item => item.MatchId == matchs[i].MatchId && item.ResultMessage.ToUpper().Contains("ERROR"))
        //            && DataStore.Alert_Wd18.ContainsKey(matchs[i].MatchId))
        //        {
        //            DataStore.Alert_Wd18.Add(matchs[i].MatchId, new Alert(false, true));
        //        }
        //    }
        //}


        private bool CheckListMinuteWd18(List<int> minutes)
        {
            minutes = minutes.Where(item => item < 0).ToList();
            int max = 0;
            int min = 0;
            if (minutes.Count > 0)
            {
                max = minutes.Where(item => item < 0).Max();
                min = minutes.Where(item => item < 0).Min();
            }

            return Math.Abs(min - max) >= 13;
        }

        private void UpdateAlert_WD18()
        {
            List<Match> matchs = DataStore.Matchs.Values.Where(item =>
                !item.League.Contains(" - ")
                && item.Home != null
                && item.League != null
                && !item.Home.ToLower().Contains("(pen)")
                && !item.Home.ToLower().Contains("(r)")
                && !(item.Home.ToLower().Contains("(w)") && item.League.ToLower().Contains("friendly"))
                && (item.LivePeriod >= 0)
                && (
                    DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.All
                    || (DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.Live && item.HasStreaming)
                    || (DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.NoneLive && !item.HasStreaming)
                )
            ).ToList();

            for (int i = 0; i < matchs.Count; i++)
            {
                if (DataStore.ProductIdsOfMatch.ContainsKey(matchs[i].MatchId))
                {
                    List<int> minuteFullTimeOverUnder = DataStore.ProductIdsOfMatch[matchs[i].MatchId]
                        .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                        .Select(item => item.Minute)
                        .ToList();

                    List<int> minuteFullTimeHandicap = DataStore.ProductIdsOfMatch[matchs[i].MatchId]
                        .Where(item => item.ProductBetType == Enums.BetType.FullTimeHandicap)
                        .Select(item => item.Minute)
                        .ToList();

                    List<int> minuteFirstHalfOverUnder = DataStore.ProductIdsOfMatch[matchs[i].MatchId]
                        .Where(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder)
                        .Select(item => item.Minute)
                        .ToList();

                    List<int> minuteFirstHalfHandicap = DataStore.ProductIdsOfMatch[matchs[i].MatchId]
                        .Where(item => item.ProductBetType == Enums.BetType.FirstHalfHandicap)
                        .Select(item => item.Minute)
                        .ToList();

                    if ((CheckListMinuteWd18(minuteFullTimeOverUnder)
                        || CheckListMinuteWd18(minuteFullTimeHandicap)
                        || CheckListMinuteWd18(minuteFirstHalfOverUnder)
                        || CheckListMinuteWd18(minuteFirstHalfHandicap))
                        && !DataStore.Alert_Wd18.ContainsKey(matchs[i].MatchId))
                    {
                        DataStore.Alert_Wd18.Add(matchs[i].MatchId, new Alert(false, true) { CustomValue = matchs[i].GlobalShowtime.ToString("dd/MM hh:mm tt") });
                    }
                }

            }

            List<long> matchIds = DataStore.Alert_Wd18.Keys.ToList();

            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if (DataStore.Matchs.TryGetValue(matchIds[i], out match)
                    && match.LivePeriod == -1
                    && DataStore.Alert_Wd18.ContainsKey(matchIds[i]))
                {
                    DataStore.Alert_Wd18.Remove(matchIds[i]);
                }
            }
        }

        private List<int> CheckAlert_WD19(Match match)
        {
            List<int> minutes = new List<int>();
            if (DataStore.GoalHistories.ContainsKey(match.MatchId))
            {
                List<GoalHistory> goals = DataStore.GoalHistories[match.MatchId];
                for (int i = 0; i < goals.Count; i++)
                {
                    GoalHistory nextGoal = null;
                    if (i < goals.Count - 1)
                    {
                        nextGoal = goals[i + 1];
                    }
                    if (CheckAlert_WD19_Minute(goals[i], nextGoal, match))
                    {
                        minutes.Add(goals[i].RealMinute);
                    }
                }
            }
            return minutes;
        }

        private bool CheckAlert_WD19_Minute(GoalHistory goal, GoalHistory nextGoal, Match match)
        {
            int minuteBeforeGoal = goal.RealMinute - 1;
            int minuteAfterGoal = goal.RealMinute + 1;
            int countHdpBeforeGoal = Common.Functions.CountHdpAtMinute(match.MatchId, minuteBeforeGoal);
            int maxHdpBeforeGoal = Common.Functions.GetMaxHdpAtMinuteV2(match.MatchId, minuteBeforeGoal);
            if (countHdpBeforeGoal == 3)
            {
                maxHdpBeforeGoal = maxHdpBeforeGoal - 25;
            }
            int maxHdpAfterGoal = maxHdpBeforeGoal - 25;
            int priceBeforeGoal = 0;
            int priceAfterGoal_1 = 0;
            int priceAfterGoal_2 = 0;
            int priceAfterGoal_3 = 0;

            if (maxHdpBeforeGoal > 0
                && maxHdpAfterGoal >= 50
                && countHdpBeforeGoal > 0)
            {
                priceBeforeGoal = Math.Abs(DataStore.OverUnderScoreTimesV3[match.MatchId][maxHdpBeforeGoal][minuteBeforeGoal].Over);
                if (minuteAfterGoal <= 90) priceAfterGoal_1 = DataStore.OverUnderScoreTimesV3[match.MatchId][maxHdpBeforeGoal][minuteAfterGoal + 0].Under;
                if (minuteAfterGoal <= 89 && (nextGoal == null || nextGoal.RealMinute - goal.RealMinute >= 2)) priceAfterGoal_2 = DataStore.OverUnderScoreTimesV3[match.MatchId][maxHdpBeforeGoal][minuteAfterGoal + 1].Under;
                if (minuteAfterGoal <= 88 && (nextGoal == null || nextGoal.RealMinute - goal.RealMinute >= 3)) priceAfterGoal_3 = DataStore.OverUnderScoreTimesV3[match.MatchId][maxHdpBeforeGoal][minuteAfterGoal + 2].Under;

                return (priceAfterGoal_1 > priceBeforeGoal
                    || priceAfterGoal_2 > priceBeforeGoal
                    || priceAfterGoal_3 > priceBeforeGoal);
            }

            return false;
        }

        private void UpdateAlert_WD19(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    List<int> minutes = CheckAlert_WD19(localMatchs[i]);

                    if (minutes.Count > 0)
                    {
                        string customString = Common.Functions.ListToString<int>(minutes, " ");
                        if (!DataStore.Alert_Wd19.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd19.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = customString });
                        }
                        else
                        {
                            int countSpace = customString.Count(item => item == ' ');
                            int countOldSpace = DataStore.Alert_Wd19[localMatchs[i].MatchId].CustomValue.Count(item => item == ' ');
                            if (countSpace > countOldSpace)
                            {
                                DataStore.Alert_Wd19[localMatchs[i].MatchId].CustomValue = customString;
                                DataStore.Alert_Wd19[localMatchs[i].MatchId].IsNew = true;
                            }
                        }

                    }
                }
                catch
                {

                }

            }
        }

        private void UpdateAlert_WD20(List<Match> matchs)
        {

            for (int i = 0; i < matchs.Count; i++)
            {
                if (DataStore.ProductIdsOfMatch.ContainsKey(matchs[i].MatchId))
                {
                    List<ProductIdLog> ids = DataStore.ProductIdsOfMatch[matchs[i].MatchId];
                    string alertResult = "";
                    if (CheckAlert_WD20_A(matchs[i], ids)) alertResult += "a ";
                    if (CheckAlert_WD20_B(matchs[i], ids)) alertResult += "b ";
                    if (CheckAlert_WD20_C(matchs[i], ids)) alertResult += "c ";
                    if (CheckAlert_WD20_D(matchs[i], ids)) alertResult += "d ";
                    if (CheckAlert_WD20_E(matchs[i], ids)) alertResult += "e ";
                    if (CheckAlert_WD20_F(matchs[i], ids)) alertResult += "f ";
                    if (CheckAlert_WD20_G(matchs[i], ids)) alertResult += "g ";
                    if (CheckAlert_WD20_H(matchs[i], ids)) alertResult += "h ";
                    if (CheckAlert_WD20_I(matchs[i], ids)) alertResult += "i ";

                    if (alertResult.Length > 0)
                    {
                        if (!DataStore.Alert_Wd20.ContainsKey(matchs[i].MatchId))
                        {
                            DataStore.Alert_Wd20.Add(matchs[i].MatchId, new Alert(false, true) { CustomValue = "" });
                        }
                        DataStore.Alert_Wd20[matchs[i].MatchId].CustomValue += alertResult;
                    }
                }


            }

            //List<long> matchIds = DataStore.Alert_Wd20.Keys.ToList();

            //for (int i = 0; i < matchIds.Count; i++)
            //{
            //    Match match;
            //    if (DataStore.Matchs.TryGetValue(matchIds[i], out match)
            //        && match.LivePeriod == -1
            //        && DataStore.Alert_Wd20.ContainsKey(matchIds[i]))
            //    {
            //        DataStore.Alert_Wd20.Remove(matchIds[i]);
            //        if (WD20_A_Ids.ContainsKey(matchIds[i])) WD20_A_Ids.Remove(matchIds[i]);
            //        if (WD20_B_Ids.Contains(matchIds[i])) WD20_B_Ids.Remove(matchIds[i]);
            //        if (WD20_C_Ids.Contains(matchIds[i])) WD20_C_Ids.Remove(matchIds[i]);
            //        if (WD20_D_Ids.Contains(matchIds[i])) WD20_D_Ids.Remove(matchIds[i]);
            //        if (WD20_E_Ids.Contains(matchIds[i])) WD20_E_Ids.Remove(matchIds[i]);
            //        if (WD20_F_Ids.Contains(matchIds[i])) WD20_F_Ids.Remove(matchIds[i]);
            //        if (WD20_G_Ids.Contains(matchIds[i])) WD20_G_Ids.Remove(matchIds[i]);
            //        if (WD20_H_Ids.Contains(matchIds[i])) WD20_H_Ids.Remove(matchIds[i]);
            //        if (WD20_I_Ids.Contains(matchIds[i])) WD20_I_Ids.Remove(matchIds[i]);
            //    }
            //}
        }

        private bool CheckAlert_WD20_A(Match match, List<ProductIdLog> ids)
        {
            if (!this.WD20_A_Ids.ContainsKey(match.MatchId))
            {
                this.WD20_A_Ids.Add(match.MatchId, new List<long>());
            }
            bool result = false;

            for (int i = 0; i < ids.Count; i++)
            {
                if (ids[i].Minute > 45
                    && !WD20_A_Ids[match.MatchId].Contains(ids[i].OddsId))
                {
                    result = true;
                    WD20_A_Ids[match.MatchId].Add(ids[i].OddsId);
                }
            }
            return result;
        }

        private bool CheckAlert_WD20_B(Match match, List<ProductIdLog> ids)
        {
            if (!WD20_B_Ids.Contains(match.MatchId))
            {
                int countMinus = ids.Count(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder && item.Minute < 0);
                int countZero = ids.Count(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder && item.Minute == 0);
                WD20_B_Ids.Add(match.MatchId);
                return countZero > countMinus && countMinus > 0;
            }
            return false;
        }

        private bool CheckAlert_WD20_C(Match match, List<ProductIdLog> ids)
        {
            if (!WD20_C_Ids.Contains(match.MatchId))
            {
                int countMinus = ids.Count(item => item.ProductBetType == Enums.BetType.FullTimeHandicap && item.Minute < 0);
                int countZero = ids.Count(item => item.ProductBetType == Enums.BetType.FullTimeHandicap && item.Minute == 0);
                WD20_C_Ids.Add(match.MatchId);
                return countZero > countMinus && countMinus > 0;
            }
            return false;
        }

        private bool CheckAlert_WD20_D(Match match, List<ProductIdLog> ids)
        {
            if (!WD20_D_Ids.Contains(match.MatchId))
            {
                if (CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FullTimeOverUnder, -1, 0)
                    || CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FullTimeHandicap, -1, 0)
                    || CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FirstHalfOverUnder, -1, 0)
                    || CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FirstHalfHandicap, -1, 0))
                {
                    WD20_D_Ids.Add(match.MatchId);
                    return true;
                }
            }
            return false;
        }

        private bool CheckAlert_WD20_E(Match match, List<ProductIdLog> ids)
        {
            if (!WD20_E_Ids.Contains(match.MatchId))
            {
                if (CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FullTimeOverUnder, 0, 1)
                    || CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FullTimeHandicap, 0, 1)
                    || CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FirstHalfOverUnder, 0, 1)
                    || CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FirstHalfHandicap, 0, 1))
                {
                    WD20_E_Ids.Add(match.MatchId);
                    return true;
                }
            }
            return false;
        }

        private bool CheckAlert_WD20_F(Match match, List<ProductIdLog> ids)
        {
            if (!WD20_F_Ids.Contains(match.MatchId))
            {
                if (CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FullTimeOverUnder, 1, 2)
                    || CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FullTimeHandicap, 1, 2)
                    || CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FirstHalfOverUnder, 1, 2)
                    || CheckAlert_WD20_DEF_BetType(match, ids, Enums.BetType.FirstHalfHandicap, 1, 2))
                {
                    WD20_F_Ids.Add(match.MatchId);
                    return true;
                }
            }
            return false;
        }

        private bool CheckAlert_WD20_DEF_BetType(Match match, List<ProductIdLog> ids, Common.Enums.BetType betType, int a, int b)
        {

            List<ProductIdLog> data = ids
                .Where(item => item.ProductBetType == betType)
                .OrderBy(item => item.Minute)
                .ToList();

            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i].Minute == a
                    && data[i + 1].Minute == b)
                {
                    return true;
                }
            }
            return false;

        }

        private bool CheckAlert_WD20_G(Match match, List<ProductIdLog> ids)
        {
            if (!WD20_G_Ids.Contains(match.MatchId))
            {
                if (Common.Functions.GetProductCountHistoryMinuteSpecial(match.MatchId, DataStore.ProductIdsOuFtOfMatchHistoryLive, Enums.BetType.FullTimeOverUnder).Contains("-")
                    || Common.Functions.GetProductCountHistoryMinuteSpecial(match.MatchId, DataStore.ProductIdsOuFhOfMatchHistoryLive, Enums.BetType.FirstHalfOverUnder).Contains("-")
                    || Common.Functions.GetProductCountHistoryMinuteSpecial(match.MatchId, DataStore.ProductIdsHdpFtOfMatchHistoryLive, Enums.BetType.FullTimeHandicap).Contains("-")
                    || Common.Functions.GetProductCountHistoryMinuteSpecial(match.MatchId, DataStore.ProductIdsHdpFhOfMatchHistoryLive, Enums.BetType.FirstHalfHandicap).Contains("-"))
                {
                    WD20_G_Ids.Add(match.MatchId);
                    return true;
                }
            }
            return false;
        }
        //GetOpenHdps

        private bool CheckAlert_WD20_H(Match match, List<ProductIdLog> ids)
        {
            if (!WD20_H_Ids.Contains(match.MatchId))
            {
                int countOpenHdps = Common.Functions.GetOpenHdps(match.MatchId).Count;
                int countIdsAtZero = ids.Count(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder && item.Minute == 0);
                if (countOpenHdps > 0 && countIdsAtZero > 0
                    && countIdsAtZero > countOpenHdps)
                {
                    WD20_H_Ids.Add(match.MatchId);
                    return true;
                }
            }
            return false;
        }

        private bool CheckAlert_WD20_I(Match match, List<ProductIdLog> ids)
        {
            List<ProductIdLog> temp = ids.Where(item =>
                    item.ProductBetType == Enums.BetType.FirstHalfHandicap
                    || item.ProductBetType == Enums.BetType.FirstHalfOverUnder
                    || item.ProductBetType == Enums.BetType.FullTimeHandicap
                    || item.ProductBetType == Enums.BetType.FullTimeOverUnder
                )
                .OrderBy(item => item.LivePeriod)
                .ThenBy(item => item.Minute)
                .ToList();

            if (!WD20_I_Ids.Contains(match.MatchId))
            {
                for (int i = 0; i < temp.Count - 1; i++)
                {

                    if (temp[i].Minute > 0 && temp[i + 1].Minute > 0
                        && temp[i + 1].Minute - temp[i].Minute == 1)
                    {
                        WD20_I_Ids.Add(match.MatchId);
                        return true;
                    }
                }
            }
            return false;
        }

        private int CountChar_WD22(string values)
        {
            int count = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == 'b'
                    || values[i] == 'c'
                    || values[i] == 'd'
                    || values[i] == 'h')
                {
                    count++;
                }
            }
            return count;
        }

        private void UpdateAlert_WD22(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (DataStore.Alert_Wd20.ContainsKey(localMatchs[i].MatchId))
                {
                    if (!DataStore.Alert_Wd22.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd22.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = "0", CreateTime = DateTime.MinValue });
                    }

                    Alert alert22 = DataStore.Alert_Wd22[localMatchs[i].MatchId];
                    Alert alert20 = DataStore.Alert_Wd20[localMatchs[i].MatchId];

                    int newCount = CountChar_WD22(alert20.CustomValue);
                    int oldCount = int.Parse(alert22.CustomValue);

                    if (newCount > oldCount)
                    {
                        DataStore.Alert_Wd22[localMatchs[i].MatchId].CustomValue = newCount.ToString();
                        DataStore.Alert_Wd22[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    }

                }

            }

            List<long> matchIds = DataStore.Alert_Wd22.Keys.ToList();

            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if (DataStore.Matchs.TryGetValue(matchIds[i], out match)
                    && match.LivePeriod == -1
                    && DataStore.Alert_Wd22.ContainsKey(matchIds[i]))
                {
                    DataStore.Alert_Wd22.Remove(matchIds[i]);
                }
            }

        }

        private int CountStepPriceWD22(long matchId, Dictionary<long, Dictionary<int, List<OverUnderScoreTimesV2Item>>> ds)
        {
            int count = 0;
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data = ds[matchId];
            List<int> keys = data.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                for (int j = 2; j < data[keys[i]].Count; j++)
                {
                    int current = data[keys[i]][j].Over;
                    int previous = data[keys[i]][j - 2].Over;
                    if (current != 0
                        && previous != 0
                        && j != 46
                        && j != 47
                        && Common.Functions.Fast2CellMove(current, previous, DataStore.Matchs[matchId].PriceStep))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void UpdateAlert_Wd24(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (localMatchs[i].TimeHasStreaming.Subtract(localMatchs[i].FirstShow).TotalMinutes >= 2)
                {
                    if (!DataStore.Alert_Wd24.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd24.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                }
            }

            //List<long> matchIds = DataStore.Alert_Wd24.Keys.ToList();

            //for (int i = 0; i < matchIds.Count; i++)
            //{
            //    Match match;
            //    if (DataStore.Alert_Wd24.ContainsKey(matchIds[i])
            //        && DataStore.Matchs.TryGetValue(matchIds[i], out match)
            //        && (match.LivePeriod == -1)
            //        && DataStore.Alert_Wd24.ContainsKey(matchIds[i]))
            //    {
            //        DataStore.Alert_Wd24.Remove(matchIds[i]);
            //    }

            //}

        }

        private void UpdateAlert_WD23(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                int total = 0;
                total = Common.Functions.CountStepPriceWD23(localMatchs[i].MatchId, DataStore.OverUnderScoreTimesV3);
                total += Common.Functions.CountStepPriceWD23(localMatchs[i].MatchId, DataStore.OverUnderScoreTimesFirstHalfV3);


                int oldTotal = 0;
                if (DataStore.Alert_Wd23.ContainsKey(localMatchs[i].MatchId))
                {
                    int.TryParse(DataStore.Alert_Wd23[localMatchs[i].MatchId].CustomValue, out oldTotal);
                }
                else
                {
                    DataStore.Alert_Wd23.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = total.ToString(), CreateTime = DateTime.MinValue });
                }

                if (total > oldTotal)
                {
                    DataStore.Alert_Wd23[localMatchs[i].MatchId] = new Alert(false, true) { CustomValue = total.ToString() };
                }


            }

            List<long> matchIds = DataStore.Alert_Wd23.Keys.ToList();

            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match;
                if (DataStore.Matchs.TryGetValue(matchIds[i], out match)
                    && match.LivePeriod == -1
                    && DataStore.Alert_Wd23.ContainsKey(matchIds[i]))
                {
                    DataStore.Alert_Wd23.Remove(matchIds[i]);
                }
            }

        }

        private void UpdateAlert_Wd25(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                int minute = (int)localMatchs[i].TimeSpanFromStart.TotalMinutes + (45 * (localMatchs[i].LivePeriod - 1));
                if (minute > 90) minute = 90;
                if (minute <= 1) continue;
                int currentMaxHdp = Common.Functions.GetMaxHdpAtMinute(localMatchs[i].MatchId, minute);
                int currentMinHdp = Common.Functions.GetMinHdpAtMinute(localMatchs[i].MatchId, minute);
                int previousMaxHdp = Common.Functions.GetMaxHdpAtMinute(localMatchs[i].MatchId, minute - 1);
                int previousMinHdp = Common.Functions.GetMinHdpAtMinute(localMatchs[i].MatchId, minute - 1);
                if (currentMaxHdp * previousMaxHdp == 0) continue;

                if (currentMaxHdp > previousMaxHdp)
                {
                    if (DataStore.Alert_Wd25.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd25[localMatchs[i].MatchId] = new Alert(false, true)
                        {
                            CustomValue = "up "
                                + previousMinHdp + " "
                                + currentMaxHdp
                        };
                    }
                    else
                    {
                        DataStore.Alert_Wd25.Add(localMatchs[i].MatchId, new Alert(false, true)
                        {
                            CustomValue = "up "
                                + previousMinHdp + " "
                                + currentMaxHdp
                        });
                    }

                }
                else if (currentMaxHdp < previousMaxHdp)
                {
                    if (DataStore.Alert_Wd25.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd25[localMatchs[i].MatchId] = new Alert(false, true) {
                            CustomValue = "down "
                                + previousMaxHdp + " "
                                + currentMinHdp
                        };
                    }
                    else
                    {
                        DataStore.Alert_Wd25.Add(localMatchs[i].MatchId, new Alert(false, true)
                        {
                            CustomValue = "down "
                                + previousMaxHdp + " "
                                + currentMinHdp
                        });
                    }
                }
                else
                {
                    if (DataStore.Alert_Wd25.ContainsKey(localMatchs[i].MatchId))
                    {
                        Alert alert = DataStore.Alert_Wd25[localMatchs[i].MatchId];
                        if (alert.TimeSpanFromCreate.TotalMinutes >= 2)
                        {
                            DataStore.Alert_Wd25.Remove(localMatchs[i].MatchId);
                        }

                    }
                }
            }

            //List<long> matchIds = DataStore.Alert_Wd25.Keys.ToList();

            //for (int i = 0; i < matchIds.Count; i++)
            //{
            //    Match match;
            //    if (DataStore.Alert_Wd25.ContainsKey(matchIds[i])
            //        && DataStore.Matchs.TryGetValue(matchIds[i], out match)
            //        && (match.LivePeriod == -1 || match.LivePeriod == 0)
            //        && DataStore.Alert_Wd25.ContainsKey(matchIds[i]))
            //    {
            //        DataStore.Alert_Wd25.Remove(matchIds[i]);
            //    }

            //}

        }

        private bool CheckMatch_Wd26(long matchId)
        {
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data;

            if (!DataStore.OverUnderScoreTimesV3.TryGetValue(matchId, out data)) return false;

            List<ProductIdLog> products = DataStore.ProductIdsOfMatch[matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                .OrderBy(item => item.Minute)
                .ToList();

            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            if (scores.Count == 0) return false;

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].LivePeriod >= 1
                    && DateTime.Now.Subtract(products[i].CreateDatetime).TotalMinutes >= 3
                    && !FindProductId_Wd26(data, products[i].OddsId))
                {
                    return true;
                }
            }

            return false;
        }

        private bool FindProductId_Wd26(Dictionary<int, List<OverUnderScoreTimesV2Item>> data, long oddsId)
        {
            List<int> keys = data.Keys.OrderBy(item => item).ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                if (data[keys[i]].Any(item => item.OddsId == oddsId))
                {
                    return true;
                }
            }

            return false;
        }

        private void UpdateAlert_Wd26(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (!DataStore.Alert_Wd26.ContainsKey(localMatchs[i].MatchId)
                    && CheckMatch_Wd26(localMatchs[i].MatchId))
                {
                    DataStore.Alert_Wd26.Add(localMatchs[i].MatchId, new Alert(false, true));
                }
            }

            List<long> matchIds = DataStore.Alert_Wd26.Keys.ToList();

            //for (int i = 0; i < matchIds.Count; i++)
            //{
            //    Match match;
            //    if (DataStore.Alert_Wd26.ContainsKey(matchIds[i])
            //        && DataStore.Matchs.TryGetValue(matchIds[i], out match)
            //        && match.LivePeriod == -1
            //        && DataStore.Alert_Wd26.ContainsKey(matchIds[i]))
            //    {
            //        DataStore.Alert_Wd26.Remove(matchIds[i]);
            //    }
            //}

        }

        private bool CheckMatch_Wd27(long matchId)
        {
            List<ProductIdLog> data = DataStore.ProductIdsOfMatch[matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                .OrderBy(item => item.Minute)
                .ToList();

            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i].CreateDatetime > data[i + 1].CreateDatetime)
                {
                    return true;
                }
            }

            return false;
        }

        private void UpdateAlert_Wd27(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (!DataStore.Alert_Wd27.ContainsKey(localMatchs[i].MatchId)
                    && CheckMatch_Wd27(localMatchs[i].MatchId))
                {
                    Alert alert = new Alert(false, true);
                    int currentMinute = (int)localMatchs[i].TimeSpanFromStart.TotalMinutes;
                    alert.CustomValue = currentMinute.ToString();
                    DataStore.Alert_Wd27.Add(localMatchs[i].MatchId, alert);
                }
            }

            List<long> matchIds = DataStore.Alert_Wd27.Keys.ToList();

            //for (int i = 0; i < matchIds.Count; i++)
            //{
            //    Match match;
            //    if (DataStore.Alert_Wd27.ContainsKey(matchIds[i])
            //        && DataStore.Matchs.TryGetValue(matchIds[i], out match)
            //        && match.LivePeriod == -1
            //        && DataStore.Alert_Wd27.ContainsKey(matchIds[i]))
            //    {
            //        DataStore.Alert_Wd27.Remove(matchIds[i]);
            //    }

            //}

        }

        private void UpdateAlert_Wd28(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (!DataStore.Alert_Wd28.ContainsKey(localMatchs[i].MatchId)
                    && CheckMatch_Wd28(localMatchs[i].MatchId))
                {
                    DataStore.Alert_Wd28.Add(localMatchs[i].MatchId, new Alert(false, true));
                }
            }

            List<long> matchIds = DataStore.Alert_Wd28.Keys.ToList();
        }

        private bool CheckMatch_Wd28(long matchId)
        {
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data;

            if (!DataStore.OverUnderScoreTimesV3.TryGetValue(matchId, out data)) return false;

            Match match = DataStore.Matchs[matchId];
            int currentMinute = (int)match.TimeSpanFromStart.TotalMinutes;

            List<int> keys = data.Keys.OrderByDescending(item => item).ToList();

            for (int i = 0; i < keys.Count - 1; i++)
            {
                if (data[keys[i]][currentMinute].OddsId == data[keys[i + 1]][currentMinute - 1].OddsId
                    && data[keys[i + 1]][currentMinute].OddsId == data[keys[i]][currentMinute - 1].OddsId
                    && data[keys[i]][currentMinute].OddsId != 0
                    && data[keys[i + 1]][currentMinute].OddsId != 0)
                {
                    return true;
                }
            }

            return false;
        }


        private void UpdateAlert_Wd29(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {

                if (!DataStore.Alert_Wd29.ContainsKey(localMatchs[i].MatchId))
                {
                    int checkResult = CheckMatch_Wd29(localMatchs[i].MatchId);
                    if (checkResult != 0)
                    {
                        DataStore.Alert_Wd29.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = checkResult.ToString() });
                    }

                }
            }

        }

        private int CheckMatch_Wd29(long matchId)
        {
            Match match = DataStore.Matchs[matchId];
            if (match.OverUnderMoneyLines.Count > 1)
            {
                int newOuLine = match.OverUnderMoneyLines[match.OverUnderMoneyLines.Count - 1];
                int oldOuLine = match.OverUnderMoneyLines[match.OverUnderMoneyLines.Count - 2];
                if (newOuLine > oldOuLine)
                {
                    return 1;
                }
                else if (oldOuLine > newOuLine)
                {
                    return -1;
                }
            }
            return 0;
        }


        private void UpdateAlert_Wd30(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (CheckMatch_Wd30(localMatchs[i].MatchId))
                {
                    if (!DataStore.Alert_Wd30.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd30.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                    DataStore.Alert_Wd30[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                }
            }
            //List<long> matchIds = DataStore.Alert_Wd30.Keys.ToList();

            //for (int i = 0; i < matchIds.Count; i++)
            //{
            //    Match match;
            //    if (DataStore.Alert_Wd30.ContainsKey(matchIds[i])
            //        && DataStore.Matchs.TryGetValue(matchIds[i], out match)
            //        && match.LivePeriod == -1
            //        && DataStore.Alert_Wd30.ContainsKey(matchIds[i]))
            //    {
            //        DataStore.Alert_Wd30.Remove(matchIds[i]);
            //    }

            //}

        }

        private bool CheckMatch_Wd30(long matchId)
        {
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data = null;
            Match match = DataStore.Matchs[matchId];
            if (match.LivePeriod != 1) return false;
            int minute = (int)match.TimeSpanFromStart.TotalMinutes;
            if (minute <= 1) return false;
            if (DataStore.OverUnderScoreTimesFirstHalfV3.TryGetValue(matchId, out data))
            {
                List<int> keys = data.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    int current = data[keys[i]][minute].Over;
                    int previous = data[keys[i]][minute - 1].Over;
                    if (current != 0 && previous != 0 && Common.Functions.Fast2CellMove(current, previous, match.PriceStep))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void UpdateAlert_Wd31(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (CheckMatch_Wd31(localMatchs[i].MatchId))
                {
                    if (!DataStore.Alert_Wd31.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd31.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                    DataStore.Alert_Wd31[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                }
            }
            List<long> matchIds = DataStore.Alert_Wd31.Keys.ToList();

            //for (int i = 0; i < matchIds.Count; i++)
            //{
            //    Match match;
            //    if (DataStore.Alert_Wd31.ContainsKey(matchIds[i])
            //        && DataStore.Matchs.TryGetValue(matchIds[i], out match)
            //        && match.LivePeriod == -1
            //        && DataStore.Alert_Wd31.ContainsKey(matchIds[i]))
            //    {
            //        DataStore.Alert_Wd31.Remove(matchIds[i]);
            //    }
            //}

        }

        private int CountAtMinute_Wd31(Dictionary<int, List<OverUnderScoreTimesV3Item>> data, List<int> scores, int minute)
        {
            int count = 0;

            for (int i = 0; i < scores.Count; i++)
            {
                if (data[scores[i]][minute].OddsId != 0)
                {
                    count++;
                }
            }

            return count;
        }

        private bool CheckMatch_Wd31(long matchId)
        {
            Dictionary<int, List<OverUnderScoreTimesV3Item>> data;

            if (!DataStore.OverUnderScoreHalftime.TryGetValue(matchId, out data)) return false;
            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            List<long> oddIds = new List<long>();

            for (int minute = 29; minute >= 0; minute--)
            {
                for (int i = 0; i < scores.Count; i++)
                {
                    long id = data[scores[i]][minute].OddsId;
                    if (id != 0 && !oddIds.Contains(id))
                    {
                        oddIds.Add(id);
                    }
                }
            }

            for (int minute = 29; minute >= 0; minute--)
            {
                int count = CountAtMinute_Wd31(data, scores, minute);
                if (count > 0 && count != scores.Count)
                {
                    return true;
                }
            }

            return false;
        }

        private void UpdateAlert_Wd33(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (Common.Functions.CheckSpecialFilter(localMatchs[i].MatchId))
                {
                    if (!DataStore.Alert_Wd33.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd33.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                    DataStore.Alert_Wd33[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                }
            }
            List<long> matchIds = DataStore.Alert_Wd33.Keys.ToList();

            //for (int i = 0; i < matchIds.Count; i++)
            //{
            //    Match match;
            //    if (DataStore.Alert_Wd33.ContainsKey(matchIds[i])
            //        && DataStore.Matchs.TryGetValue(matchIds[i], out match)
            //        && match.LivePeriod == -1
            //        && DataStore.Alert_Wd33.ContainsKey(matchIds[i]))
            //    {
            //        DataStore.Alert_Wd33.Remove(matchIds[i]);
            //    }

            //}
        }

        private void UpdateAlert_Wd34(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .Where(item => item.LiveAwayScore + item.LiveHomeScore == 0)
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (CheckMatch_Wd34(localMatchs[i].MatchId))
                {
                    if (!DataStore.Alert_Wd34.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd34.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                    DataStore.Alert_Wd34[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                }
            }
            //List<long> matchIds = DataStore.Alert_Wd34.Keys.ToList();

            //for (int i = 0; i < matchIds.Count; i++)
            //{
            //    Match match;
            //    if (DataStore.Alert_Wd34.ContainsKey(matchIds[i])
            //        && DataStore.Matchs.TryGetValue(matchIds[i], out match)
            //        && match.LivePeriod == -1
            //        && DataStore.Alert_Wd34.ContainsKey(matchIds[i]))
            //    {
            //        DataStore.Alert_Wd34.Remove(matchIds[i]);
            //        if (WD34_MarkedHdps.ContainsKey(matchIds[i]))
            //        {
            //            WD34_MarkedHdps.Remove(matchIds[i]);
            //        }
            //    }

            //}
        }

        private bool CheckMatch_Wd34(long matchId)
        {
            //WD34_MarkedHdps
            if (!WD34_MarkedHdps.ContainsKey(matchId))
            {
                WD34_MarkedHdps.Add(matchId, new List<long>());
            }
            List<ProductIdLog> data = DataStore.ProductIdsOfMatch[matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                .ToList();

            if (data.Count <= 2) return false;

            int minMinute = data.Min(item => item.Minute);
            int maxHdp = data.Max(item => item.Hdp);
            if (WD34_MarkedHdps[matchId].Contains(maxHdp)) return false;
            ProductIdLog selectedProduct = data.FirstOrDefault(item => item.Hdp == maxHdp && item.Minute >= 1 && item.Minute > minMinute);
            if (selectedProduct != null)
            {
                WD34_MarkedHdps[matchId].Add(maxHdp);
                return true;
            }
            return false;
        }

        private void UpdateAlert_Wd35(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (!DataStore.Alert_Wd35.ContainsKey(localMatchs[i].MatchId))
                {
                    string checkResult = CheckMatch_Wd35(localMatchs[i].MatchId);
                    if (checkResult != string.Empty)
                    {
                        if (!DataStore.Alert_Wd35.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd35.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = checkResult });
                        }
                        DataStore.Alert_Wd35[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    }
                }

            }

        }

        private string CheckMatch_Wd35(long matchId)
        {
            Match match = DataStore.Matchs[matchId];
            if (match.LivePeriod == 2) return string.Empty;
            if (!DataStore.OverUnderScoreTimes.ContainsKey(matchId)) return string.Empty;
            if (!DataStore.UnderScoreTimes.ContainsKey(matchId)) return string.Empty;
            Dictionary<int, List<string>> dataOver = DataStore.OverUnderScoreTimes[matchId];
            Dictionary<int, List<string>> dataUnder = DataStore.UnderScoreTimes[matchId];
            List<int> ouKeys = dataOver.Keys.ToList();

            for (int i = 1; i <= 15; i++)
            {
                for (int j = 0; j < ouKeys.Count; j++)
                {
                    int currentMinute = Common.Functions.OverUnderLineAtMinute(ouKeys[j], i, dataOver, dataUnder);
                    int previousMinute = Common.Functions.OverUnderLineAtMinute(ouKeys[j], i - 1, dataOver, dataUnder);
                    if (currentMinute != 0 && previousMinute != 0)
                    {
                        if (currentMinute > previousMinute)
                        {
                            return "red";
                        }
                        else if (currentMinute < previousMinute)
                        {
                            return "yellow";
                        }
                    }
                }
            }
            return string.Empty;
        }

        private void UpdateAlert_Wd36(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (CheckMatch_Wd36_Wd37(localMatchs[i].MatchId, true))
                {
                    if (!DataStore.Alert_Wd36.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd36.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }

                    int currentMinute = (int)localMatchs[i].TimeSpanFromStart.TotalMinutes;
                    currentMinute += localMatchs[i].LivePeriod == 2 ? 45 : 0;

                    DataStore.Alert_Wd36[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    DataStore.Alert_Wd36[localMatchs[i].MatchId].CustomValue += (currentMinute + ".");
                }
            }
        }

        private void UpdateAlert_Wd37(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (CheckMatch_Wd36_Wd37(localMatchs[i].MatchId, false))
                {
                    if (!DataStore.Alert_Wd37.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd37.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }

                    int currentMinute = (int)localMatchs[i].TimeSpanFromStart.TotalMinutes;
                    currentMinute += localMatchs[i].LivePeriod == 2 ? 45 : 0;

                    DataStore.Alert_Wd37[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    DataStore.Alert_Wd37[localMatchs[i].MatchId].CustomValue += (currentMinute + ".");
                }
            }
        }

        private bool CheckMatch_Wd36_Wd37(long matchId, bool isFt)
        {
            Match match = DataStore.Matchs[matchId];
            int currentMinute = (int)match.TimeSpanFromStart.TotalMinutes;
            if (currentMinute <= 100 && currentMinute >= 2 && DataStore.ProductStatus.ContainsKey(matchId))
            {
                List<List<ProductStatusLog>> data = DataStore.ProductStatus[matchId];
                return CheckOneMinute(matchId, data, currentMinute, isFt)
                    || CheckTwoMinute(matchId, data, currentMinute, isFt);
            }

            return false;
        }

        private bool CheckOneMinute(long matchId, List<List<ProductStatusLog>> data, int currentMinute, bool isFt)
        {
            if (isFt)
            {
                List<ProductStatusLog> rowDataFt = data[currentMinute].Where(item => item.Type == Enums.BetType.FullTimeOverUnder).ToList();
                bool checkFt = CheckDataWd36Wd37(rowDataFt, matchId, currentMinute, true);
                return checkFt;
            }
            else
            {
                List<ProductStatusLog> rowDataFh = data[currentMinute].Where(item => item.Type == Enums.BetType.FirstHalfOverUnder).ToList();
                bool checkFh = CheckDataWd36Wd37(rowDataFh, matchId, currentMinute, false);
                return checkFh;
            }
        }

        private bool CheckTwoMinute(long matchId, List<List<ProductStatusLog>> data, int currentMinute, bool isFt)
        {
            if (isFt)
            {
                List<ProductStatusLog> rowDataFtCurrent = data[currentMinute].Where(item => item.Type == Enums.BetType.FullTimeOverUnder).ToList();
                List<ProductStatusLog> rowDataFtPrevious = data[currentMinute - 1].Where(item => item.Type == Enums.BetType.FullTimeOverUnder).ToList();
                List<ProductStatusLog> rowDataFt = new List<ProductStatusLog>();
                rowDataFt.AddRange(rowDataFtCurrent);
                rowDataFt.AddRange(rowDataFtPrevious);
                return CheckDataWd36Wd37(rowDataFt, matchId, currentMinute, isFt);
            }
            else
            {
                List<ProductStatusLog> rowDataFhCurrent = data[currentMinute].Where(item => item.Type == Enums.BetType.FirstHalfOverUnder).ToList();
                List<ProductStatusLog> rowDataFhPrevious = data[currentMinute - 1].Where(item => item.Type == Enums.BetType.FirstHalfOverUnder).ToList();
                List<ProductStatusLog> rowDataFh = new List<ProductStatusLog>();
                rowDataFh.AddRange(rowDataFhCurrent);
                rowDataFh.AddRange(rowDataFhPrevious);
                return CheckDataWd36Wd37(rowDataFh, matchId, currentMinute, isFt);
            }

        }

        private bool CheckDataWd36Wd37(List<ProductStatusLog> rowData, long matchId, int currentMinute, bool isFt)
        {
            int countTotal = rowData.Count;
            if (countTotal != 6) return false;

            int countInsert = rowData.Count(item => item.ServerAction == "insert");
            if (countInsert != 3) return false;

            int countDelete = rowData.Count(item => item.ServerAction == "delete");
            if (countDelete != 3) return false;


            List<int> hdps = ExtractHdpWd36(rowData);
            if (hdps.Count != 2) return false;

            int hdpMax = hdps.Max();
            int hdpMin = hdps.Min();

            int countInsertMin = rowData.Count(item => item.ServerAction == "insert" && item.Hdp100 == hdpMin);
            if (countInsertMin != 2) return false;

            int countInsertMax = rowData.Count(item => item.ServerAction == "insert" && item.Hdp100 == hdpMax);
            if (countInsertMax != 1) return false;

            int countDeleteMin = rowData.Count(item => item.ServerAction == "delete" && item.Hdp100 == hdpMin);
            if (countDeleteMin != 1) return false;

            int countDeleteMax = rowData.Count(item => item.ServerAction == "delete" && item.Hdp100 == hdpMax);
            if (countDeleteMax != 2) return false;

            int expectedTotalLine = (hdpMax - hdpMin) / 25;
            int totalLine = 0;
            if (isFt)
            {
                totalLine = Common.Functions.GetTotalProductOuFtAtMinute(matchId, currentMinute);
            }
            else
            {
                totalLine = Common.Functions.GetTotalProductOuFhAtMinute(matchId, currentMinute);
            }

            if (expectedTotalLine != totalLine) return false;

            return true;
        }

        private List<int> ExtractHdpWd36(List<ProductStatusLog> rowData)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < rowData.Count; i++)
            {
                int hdp = rowData[i].Hdp100;
                if (!result.Contains(hdp))
                {
                    result.Add(hdp);
                }
            }

            return result;
        }

        private void UpdateAlert_Wd38(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            string checkResult = "";
            for (int i = 0; i < localMatchs.Count; i++)
            {
                checkResult = CheckMatch_Wd38(localMatchs[i]);
                if (checkResult.Length > 0)
                {
                    if (!DataStore.Alert_Wd38.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd38.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = "" });
                    }
                    DataStore.Alert_Wd38[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    DataStore.Alert_Wd38[localMatchs[i].MatchId].CustomValue += checkResult;
                }
            }
        }

        private string CheckMatch_Wd38(Match match)
        {
            string result = "";
            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes) - 1;
            if (currentMinute < 0) currentMinute = 0;
            if (currentMinute > 60) currentMinute = 60;
            List<ProductStatusLog> data = DataStore.ProductStatus[match.MatchId][currentMinute];

            for (int i = 0; i < data.Count - 2; i++)
            {
                if (CheckItemWd38(data[i], Enums.BetType.FirstHalf1x2, "running", "delete")
                    && CheckItemWd38(data[i + 1], Enums.BetType.FirstHalf1x2, "suspend", "insert")
                    && CheckItemWd38(data[i + 2], Enums.BetType.FirstHalf1x2, "running", "update"))
                {
                    result += "a";
                }

                if (CheckItemWd38(data[i], Enums.BetType.FullTime1x2, "running", "delete")
                    && CheckItemWd38(data[i + 1], Enums.BetType.FullTime1x2, "suspend", "insert")
                    && CheckItemWd38(data[i + 2], Enums.BetType.FullTime1x2, "running", "update"))
                {
                    result += "b";
                }
            }

            for (int i = 0; i < data.Count - 1; i++)
            {
                if (CheckItemWd38(data[i], Enums.BetType.FirstHalf1x2, "closePrice", "update")
                    && CheckItemWd38(data[i + 1], Enums.BetType.FirstHalf1x2, "running", "update"))
                {
                    result += "c";
                }

                if (CheckItemWd38(data[i], Enums.BetType.FullTime1x2, "closePrice", "update")
                    && CheckItemWd38(data[i + 1], Enums.BetType.FullTime1x2, "running", "update"))
                {
                    result += "d";
                }
            }

            return result;
        }

        private bool CheckItemWd38(ProductStatusLog item, Enums.BetType type, string status, string serverAction)
        {
            return item.Type == type
                && item.Status == status
                && item.ServerAction == serverAction;
        }

        private void UpdateAlert_Wd39(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            bool checkResult = false;
            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    checkResult = CheckMatch_Wd39(localMatchs[i]);
                    if (checkResult)
                    {
                        if (!DataStore.Alert_Wd39.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd39.Add(localMatchs[i].MatchId, new Alert(false, true));
                        }
                        DataStore.Alert_Wd39[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    }
                }
                catch
                {

                }
            }
        }

        private bool CheckMatch_Wd39(Match match)
        {
            if (match.LivePeriod != 1) return false;
            Dictionary<int, List<string>> dataFt;

            if (match.LivePeriod == 2
                && DataStore.OverUnderScoreTimes.TryGetValue(match.MatchId, out dataFt))
            {
                if (CheckDownData_Wd39(dataFt, match.MatchId))
                {
                    return true;
                }
            }

            Dictionary<int, List<string>> dataFh;
            if (match.LivePeriod == 2
                && DataStore.OverUnderScoreTimesFirstHalf.TryGetValue(match.MatchId, out dataFh))
            {
                if (CheckDownData_Wd39(dataFh, match.MatchId))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckDownData_Wd39(Dictionary<int, List<string>> data, long matchId)
        {
            Match match = DataStore.Matchs[matchId];
            int currentMinute = (int)match.TimeSpanFromStart.TotalMinutes;
            if (currentMinute < 2 || currentMinute > 45) return false;
            if (data == null) return false;
            List<int> keys = data.Keys.ToList();
            List<string> row;

            for (int i = 0; i < keys.Count; i++)
            {
                row = data[keys[i]];
                int current = 0;
                int previous = 0;
                if (int.TryParse(row[currentMinute], out current)
                    && int.TryParse(row[currentMinute - 1], out previous)
                    && current != 0
                    && previous != 0
                    && Common.Functions.DownCellMove(previous, current, match.PriceStepDown))
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateAlert_Wd40(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            bool checkResult = false;
            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    checkResult = CheckMatch_Wd40(localMatchs[i]);
                    if (checkResult)
                    {
                        if (!DataStore.Alert_Wd40.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd40.Add(localMatchs[i].MatchId, new Alert(false, true));
                        }
                        DataStore.Alert_Wd40[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    }
                }
                catch
                {

                }
            }
        }

        private bool CheckMatch_Wd40(Match match)
        {
            List<string> sums = new List<string>();
            if (match.LivePeriod == 1)
            {
                sums = Common.Functions.SumOverUnderScoreTimesFreqFt45(match.MatchId);
            }
            else if (match.LivePeriod == 2)
            {
                sums = Common.Functions.SumOverUnderScoreTimesFreqFt90(match.MatchId);
            }
            else
            {
                return false;
            }

            int currentMinute = (int)match.TimeSpanFromStart.TotalMinutes;
            if (currentMinute >= 46) return false;
            if (currentMinute <= 0) return false;
            int fromMinute = currentMinute - 10 <= 0 ? 1 : currentMinute - 10;
            int maxValue = 0;
            for (int j = fromMinute + 1; j <= currentMinute; j++)
            {
                int value = 0;
                if (int.TryParse(sums[j], out value))
                {
                    if (value > maxValue)
                    {
                        maxValue = value;
                    }
                }
            }

            int currentValue = 0;
            if (int.TryParse(sums[currentMinute], out currentValue)
                && currentValue == maxValue)
            {
                return true;
            }

            return false;
        }

        private void UpdateAlert_Wd41(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            bool checkResult = false;
            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    checkResult = CheckMatch_Wd41(localMatchs[i]);
                    if (checkResult)
                    {
                        if (!DataStore.Alert_Wd41.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd41.Add(localMatchs[i].MatchId, new Alert(false, true));
                        }
                        DataStore.Alert_Wd41[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    }
                }
                catch
                {

                }
            }
        }

        private void UpdateAlert_Wd42(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            bool checkResult = false;
            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    checkResult = CheckMatch_Wd42(localMatchs[i]);
                    if (checkResult)
                    {
                        if (!DataStore.Alert_Wd42.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd42.Add(localMatchs[i].MatchId, new Alert(false, true));
                        }
                        DataStore.Alert_Wd42[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    }
                }
                catch
                {

                }
            }
        }

        private void UpdateAlert_Wd43(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            bool checkResult = false;
            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    checkResult = CheckMatch_Wd43(localMatchs[i]);
                    if (checkResult)
                    {
                        if (!DataStore.Alert_Wd43.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd43.Add(localMatchs[i].MatchId, new Alert(false, true));
                        }
                        DataStore.Alert_Wd43[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    }
                }
                catch
                {

                }
            }
        }

        private bool CheckMatch_Wd41(Match match)
        {
            List<string> sums = new List<string>();
            if (match.LivePeriod == 1)
            {
                sums = Common.Functions.SumOverUnderScoreTimesFreqFh45(match.MatchId);
            }
            else
            {
                return false;
            }

            int currentMinute = (int)match.TimeSpanFromStart.TotalMinutes + 2;
            if (currentMinute >= 46) return false;
            if (currentMinute <= 0) return false;
            int fromMinute = currentMinute - 10 <= 0 ? 1 : currentMinute - 10;
            int maxValue = 0;
            for (int j = fromMinute + 1; j <= currentMinute; j++)
            {
                int value = 0;
                if (int.TryParse(sums[j], out value))
                {
                    if (value > maxValue)
                    {
                        maxValue = value;
                    }
                }
            }

            int currentValue = 0;
            if (int.TryParse(sums[currentMinute], out currentValue)
                && currentValue == maxValue)
            {
                return true;
            }

            return false;
        }

        private bool CheckMatch_Wd42(Match match)
        {
            int currentMinute = (int)match.TimeSpanFromStart.TotalMinutes - 1;
            if (currentMinute <= 0 || currentMinute > 90)
            {
                return false;
            }
            if (DataStore.ProductStatus.ContainsKey(match.MatchId))
            {
                List<List<ProductStatusLog>> data = DataStore.ProductStatus[match.MatchId];
                List<ProductStatusLog> rowData = data[currentMinute]
                    .Where(item => item.LivePeriod == match.LivePeriod)
                    .ToList();

                int countClosePriceFh1x2 = rowData.Count(item => item.Status == "closePrice" && item.Type == Enums.BetType.FirstHalf1x2);
                if (countClosePriceFh1x2 == 0)
                {
                    return false;
                }
                int countClosePriceFhOu = rowData.Count(item => item.Status == "closePrice" && item.Type == Enums.BetType.FirstHalfOverUnder);
                if (countClosePriceFhOu <= 2)
                {
                    return false;
                }

                int countFtOverUnderProduct = DataStore.Products.Values.Count(item => item.MatchId == match.MatchId && item.Bettype == Enums.BetType.FullTimeOverUnder);

                return countClosePriceFh1x2 > 1
                    && countClosePriceFhOu == countFtOverUnderProduct + 2;

            }

            return false;
        }

        private bool CheckMatch_Wd43(Match match)
        {
            int currentMinute = (int)match.TimeSpanFromStart.TotalMinutes - 1;
            if (currentMinute <= 0 || currentMinute > 90)
            {
                return false;
            }
            if (DataStore.ProductStatus.ContainsKey(match.MatchId))
            {
                List<List<ProductStatusLog>> data = DataStore.ProductStatus[match.MatchId];

                List<ProductStatusLog> rowData = data[currentMinute]
                    .Where(item => item.LivePeriod == match.LivePeriod)
                    .ToList();

                int countClosePriceFt1x2 = rowData.Count(item => item.Status == "closePrice" && item.Type == Enums.BetType.FullTime1x2);
                if (countClosePriceFt1x2 == 0)
                {
                    return false;
                }

                int countClosePriceFtOu = rowData.Count(item => item.Status == "closePrice" && item.Type == Enums.BetType.FullTimeOverUnder);
                if (countClosePriceFtOu <= 2)
                {
                    return false;
                }

                int countFtOverUnderProduct = DataStore.Products.Values.Count(item => item.MatchId == match.MatchId && item.Bettype == Enums.BetType.FullTimeOverUnder);

                return countClosePriceFt1x2 > 1
                    && countClosePriceFtOu == countFtOverUnderProduct + 2;

            }

            return false;
        }

        private void UpdateAlert_Wd44(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            string checkResult = "";
            for (int i = 0; i < localMatchs.Count; i++)
            {
                checkResult = CheckMatch_Wd44(localMatchs[i]);
                if (checkResult.Length > 0)
                {
                    if (!DataStore.Alert_Wd44.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd44.Add(localMatchs[i].MatchId, new Alert(false, true) { CustomValue = "" });
                    }
                    DataStore.Alert_Wd44[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    DataStore.Alert_Wd44[localMatchs[i].MatchId].CustomValue = checkResult;
                }
            }
        }

        private string CheckMatch_Wd44(Match match)
        {
            return CheckMatch_Wd44_A(match)
                + CheckMatch_Wd44_B(match)
                + CheckMatch_Wd44_C(match)
                + CheckMatch_Wd44_D(match);
        }

        private string CheckMatch_Wd44_A(Match match)
        {
            Dictionary<long, List<HandicapLifeTimeHistory>> data;

            if (!DataStore.HandicapFhScoreTimes.TryGetValue(match.MatchId, out data)) return "";

            List<int> scores = new List<int>();
            Dictionary<int, List<long>> viewData = new Dictionary<int, List<long>>();
            List<long> keys = data.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                List<HandicapLifeTimeHistory> row = data[keys[i]];
                for (int j = 0; j < row.Count; j++)
                {
                    if (row[j] != null &&
                        !scores.Contains(row[j].Hdp_100))
                    {
                        scores.Add(row[j].Hdp_100);
                    }
                }
            }

            if (scores.Count == 0) return "";

            scores = scores.OrderBy(item => item).ToList();


            for (int i = 0; i < scores.Count; i++)
            {
                List<long> row = new List<long>();
                for (int j = 0; j <= 45; j++)
                {
                    row.Add(0);
                }
                viewData.Add(scores[i], row);
            }

            for (int i = 0; i < keys.Count; i++)
            {
                List<HandicapLifeTimeHistory> row = data[keys[i]];
                for (int j = 0; j < row.Count; j++)
                {
                    if (row[j] != null)
                    {
                        viewData[row[j].Hdp_100][j] = row[j].OddsId;
                    }
                }
            }

            List<ProductIdLog> products = DataStore.ProductIdsOfMatch[match.MatchId]
                .Where(item => item.ProductBetType == Enums.BetType.FirstHalfHandicap)
                .OrderBy(item => item.Minute)
                .ToList();

            List<List<string>> rows = new List<List<string>>();
            int firstIndex = 47;
            for (int i = 0; i < scores.Count; i++)
            {
                List<string> row = new List<string>();
                List<string> rowData = viewData[scores[i]].Select(item => item.ToString()).ToList();
                row.Add(scores[i].ToString());
                row.AddRange(rowData);
                rows.Add(row);

                for (int j = 2; j <= 46; j++)
                {
                    if (row[j] != "0" && firstIndex > j)
                    {
                        firstIndex = j;
                        break;
                    }
                }

            }

            List<string> idAtZero = rows.Select(row => row[1]).Where(item => item != "0").ToList();
            List<string> idAtFirst = rows.Select(row => row[firstIndex]).Where(item => item != "0").ToList();

            for (int i = 0; i < idAtZero.Count; i++)
            {
                if (idAtFirst.Any(item => item == idAtZero[i]))
                {
                    return "";
                }
            }

            if (idAtZero.Count > 0 && idAtFirst.Count > 0)
            {
                return "A ";
            }
            return "";

        }

        private string CheckMatch_Wd44_B(Match match)
        {
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data;

            if (!DataStore.OverUnderScoreTimesFirstHalfV3.TryGetValue(match.MatchId, out data)) return "";
            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            if (scores.Count == 0) return "";

            List<ProductIdLog> products = DataStore.ProductIdsOfMatch[match.MatchId]
                .Where(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder)
                .OrderBy(item => item.Minute)
                .ToList();

            List<List<string>> rows = new List<List<string>>();
            int firstIndex = 47;

            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = new List<string>();
                List<string> rowData = data[scores[i]].Select(item => item.OddsId.ToString()).ToList();


                row.Add(scores[i].ToString());
                row.AddRange(rowData);
                rows.Add(row);

                for (int j = 2; j <= 46; j++)
                {
                    if (row[j] != "0" && firstIndex > j)
                    {
                        firstIndex = j;
                        break;
                    }
                }
            }

            List<string> idAtZero = rows.Select(row => row[1]).Where(item => item != "0").ToList();
            List<string> idAtFirst = rows.Select(row => row[firstIndex]).Where(item => item != "0").ToList();

            for (int i = 0; i < idAtZero.Count; i++)
            {
                if (idAtFirst.Any(item => item == idAtZero[i]))
                {
                    return "";
                }
            }

            if (idAtZero.Count > 0 && idAtFirst.Count > 0)
            {
                return "B ";
            }
            return "";

        }

        private string CheckMatch_Wd44_C(Match match)
        {
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data;

            if (!DataStore.OverUnderScoreTimesV3.TryGetValue(match.MatchId, out data)) return "";
            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            if (scores.Count == 0) return "";

            List<ProductIdLog> products = DataStore.ProductIdsOfMatch[match.MatchId]
                .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                .OrderBy(item => item.Minute)
                .ToList();

            List<List<string>> rows = new List<List<string>>();
            int firstIndex = 47;

            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = new List<string>();
                List<string> rowData = data[scores[i]].Select(item => item.OddsId.ToString()).ToList();


                row.Add(scores[i].ToString());
                row.AddRange(rowData);
                rows.Add(row);

                for (int j = 2; j <= 46; j++)
                {
                    if (row[j] != "0" && firstIndex > j)
                    {
                        firstIndex = j;
                        break;
                    }
                }

            }


            List<string> idAtZero = rows.Select(row => row[1]).Where(item => item != "0").ToList();
            List<string> idAtFirst = rows.Select(row => row[firstIndex]).Where(item => item != "0").ToList();

            for (int i = 0; i < idAtZero.Count; i++)
            {
                if (idAtFirst.Any(item => item == idAtZero[i]))
                {
                    return "";
                }
            }

            if (idAtZero.Count > 0 && idAtFirst.Count > 0)
            {
                return "C ";
            }
            return "";

        }

        private string CheckMatch_Wd44_D(Match match)
        {
            Dictionary<long, List<HandicapLifeTimeHistory>> data;

            if (!DataStore.HandicapScoreTimes.TryGetValue(match.MatchId, out data)) return "";

            List<int> scores = new List<int>();
            Dictionary<int, List<long>> viewData = new Dictionary<int, List<long>>();
            List<long> keys = data.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                List<HandicapLifeTimeHistory> row = data[keys[i]];
                for (int j = 0; j < row.Count; j++)
                {
                    if (row[j] != null &&
                        !scores.Contains(row[j].Hdp_100))
                    {
                        scores.Add(row[j].Hdp_100);
                    }
                }
            }



            if (scores.Count == 0) return "";

            scores = scores.OrderBy(item => item).ToList();


            for (int i = 0; i < scores.Count; i++)
            {
                List<long> row = new List<long>();
                for (int j = 0; j <= 100; j++)
                {
                    row.Add(0);
                }
                viewData.Add(scores[i], row);
            }

            for (int i = 0; i < keys.Count; i++)
            {
                List<HandicapLifeTimeHistory> row = data[keys[i]];
                for (int j = 0; j < row.Count; j++)
                {
                    if (row[j] != null)
                    {
                        viewData[row[j].Hdp_100][j] = row[j].OddsId;
                    }
                }
            }

            List<ProductIdLog> products = DataStore.ProductIdsOfMatch[match.MatchId]
                .Where(item => item.ProductBetType == Enums.BetType.FullTimeHandicap)
                .OrderBy(item => item.Minute)
                .ToList();

            List<List<string>> rows = new List<List<string>>();
            int firstIndex = 47;

            for (int i = 0; i < scores.Count; i++)
            {
                List<string> row = new List<string>();
                List<string> rowData = viewData[scores[i]].Select(item => item.ToString()).ToList();

                row.Add(scores[i].ToString());
                row.AddRange(rowData);
                rows.Add(row);
                for (int j = 2; j <= 46; j++)
                {
                    if (row[j] != "0" && firstIndex > j)
                    {
                        firstIndex = j;
                        break;
                    }
                }

            }


            List<string> idAtZero = rows.Select(row => row[1]).Where(item => item != "0").ToList();
            List<string> idAtFirst = rows.Select(row => row[firstIndex]).Where(item => item != "0").ToList();

            for (int i = 0; i < idAtZero.Count; i++)
            {
                if (idAtFirst.Any(item => item == idAtZero[i]))
                {
                    return "";
                }
            }

            if (idAtZero.Count > 0 && idAtFirst.Count > 0)
            {
                return "D ";
            }
            return "";

        }

        private void UpdateAlert_Wd45(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            bool checkResult = false;
            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    checkResult = CheckMatch_Wd45(localMatchs[i]);
                    if (checkResult)
                    {
                        if (!DataStore.Alert_Wd45.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd45.Add(localMatchs[i].MatchId, new Alert(false, true));
                        }
                        DataStore.Alert_Wd45[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    }
                }
                catch
                {

                }
            }
        }

        private bool CheckMatch_Wd45(Match match)
        {
            int currentMinute = (int)match.TimeSpanFromStart.TotalMinutes - 1;
            if (currentMinute < 30)
            {
                return false;
            }
            if (match.LivePeriod != 2)
            {
                return false;
            }

            int fromMinute = 30;
            int toMinute = 55;
            if (fromMinute <= toMinute
                && toMinute <= 100
                && fromMinute >= 0
                && DataStore.ProductStatus.ContainsKey(match.MatchId))
            {
                List<List<ProductStatusLog>> data = DataStore.ProductStatus[match.MatchId];
                List<ProductStatusLog> rowData = new List<ProductStatusLog>();
                for (int i = fromMinute; i <= toMinute; i++)
                {
                    var row = data[i]
                        .Where(item => item.LivePeriod == 2)
                        .ToList();

                    for (int j = 0; j < row.Count - 1; j++)
                    {
                        if (row[j].Type == Enums.BetType.FullTime1x2
                            && row[j].ServerAction == "update"
                            && row[j].Status.ToLower() == "suspend"

                            && row[j + 1].Type == Enums.BetType.FullTime1x2
                            && row[j + 1].ServerAction == "update"
                            && row[j + 1].Status.ToLower() == "running")
                        {
                            return true;
                        }
                    }


                }

            }

            return false;
        }

        private void UpdateAlert_Wd46(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            bool checkResult = false;
            for (int i = 0; i < localMatchs.Count; i++)
            {
                try
                {
                    checkResult = CheckMatch_Wd46(localMatchs[i]);
                    if (checkResult)
                    {
                        if (!DataStore.Alert_Wd46.ContainsKey(localMatchs[i].MatchId))
                        {
                            DataStore.Alert_Wd46.Add(localMatchs[i].MatchId, new Alert(false, true));
                        }
                        DataStore.Alert_Wd46[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                    }
                }
                catch
                {

                }
            }
        }

        private bool CheckMatch_Wd46(Match match)
        {
            List<GoalHistory> goals = new List<GoalHistory>();
            if (!DataStore.GoalHistories.TryGetValue(match.MatchId, out goals))
            {
                return false;
            }

            if (goals.Count != 1)
            {
                return false;
            }

            int minuteFirstGoal = (int)goals.FirstOrDefault().TimeSpanFromStart.TotalMinutes;
            if (minuteFirstGoal <= 1 || minuteFirstGoal >= 45)
            {
                return false;
            }
            Common.Enums.BetType betType = BetType.FirstHalf1x2;
            List<List<ProductStatusLog>> data = DataStore.ProductStatus[match.MatchId];
            List<string> headerRow = new List<string>() { betType.ToString() };
            List<string> dataRowRunning = new List<string>() { "running" };
            List<string> dataRowSuspend = new List<string>() { "suspend" };
            List<string> dataRowClosePrice = new List<string>() { "closePrice" };

            int livePeriod = 1;

            for (int i = 1; i <= 102; i++)
            {
                int realMinute = 0;
                realMinute = i - 1;
                headerRow.Add((i - 1).ToString());

                List<ProductStatusLog> dataAtMinute = data[realMinute].Where(item =>
                    item.LivePeriod == livePeriod
                    || (livePeriod == 1 && !item.IsHt && item.LivePeriod == 0
                && (
                                betType == Common.Enums.BetType.FirstHalf1x2
                                || betType == Common.Enums.BetType.FirstHalfHandicap
                                || betType == Common.Enums.BetType.FirstHalfOverUnder
                            )
                        )
                    || (livePeriod == 1
                        && (item.IsHt || item.LivePeriod == 0)
                && (
                            betType == Common.Enums.BetType.FullTime1x2
                            || betType == Common.Enums.BetType.FullTimeHandicap
                            || betType == Common.Enums.BetType.FullTimeOverUnder
                        )
                    )
                ).ToList();

                int countRunning = dataAtMinute.Count(item => item.Status == "running" && item.Type == betType);
                int countSuspend = dataAtMinute.Count(item => item.Status == "suspend" && item.Type == betType);
                int countClosePrice = dataAtMinute.Count(item => item.Status == "closePrice" && item.Type == betType);

                bool hasRunning1x2Price = dataAtMinute.Any(item =>
                    item.Status == "running"
                    && item.Type == betType
                && (betType == Common.Enums.BetType.FullTime1x2 || betType == Common.Enums.BetType.FirstHalf1x2)
                && item.Price != 0
                );

                bool hasSuspend1x2Price = dataAtMinute.Any(item =>
                    item.Status == "suspend"
                    && item.Type == betType
                && (betType == Common.Enums.BetType.FullTime1x2 || betType == Common.Enums.BetType.FirstHalf1x2)
                && item.Price != 0
                );

                bool hasClosePrice1x2Price = dataAtMinute.Any(item =>
                    item.Status == "closePrice"
                    && item.Type == betType
                && (betType == Common.Enums.BetType.FullTime1x2 || betType == Common.Enums.BetType.FirstHalf1x2)
                && item.Price != 0
                );

                bool hasDeleteFirstRunning = HasDeleteFirst_Wd46(dataAtMinute.Where(item => item.Status == "running" && item.Type == betType).ToList());
                bool hasDeleteFirstSuspend = HasDeleteFirst_Wd46(dataAtMinute.Where(item => item.Status == "suspend" && item.Type == betType).ToList());
                bool hasDeleteFirstClosePrice = HasDeleteFirst_Wd46(dataAtMinute.Where(item => item.Status == "closePrice" && item.Type == betType).ToList());


                int countRunning_Insert = dataAtMinute.Count(item =>
                    item.Status == "running"
                    && item.ServerAction == "insert"
                    && item.Type == betType
                );

                int countRunning_Delete = dataAtMinute.Count(item =>
                    item.Status == "running"
                    && item.ServerAction == "delete"
                    && item.Type == betType
                );

                int countRunning_Update = dataAtMinute.Count(item =>
                    item.Status == "running"
                    && item.ServerAction == "update"
                    && item.Type == betType
                );

                int countSuspend_Delete = dataAtMinute.Count(item =>
                    item.Status == "suspend"
                    && item.ServerAction == "delete"
                    && item.Type == betType
                );

                int countSuspend_Insert = dataAtMinute.Count(item =>
                    item.Status == "suspend"
                    && item.ServerAction == "insert"
                    && item.Type == betType
                );

                int countSuspend_Update = dataAtMinute.Count(item =>
                    item.Status == "suspend"
                    && (item.ServerAction == "update")
                    && item.Type == betType
                );

                int countClosePrice_Delete = dataAtMinute.Count(item =>
                    item.Status == "closePrice"
                    && item.ServerAction == "delete"
                    && item.Type == betType
                );

                int countClosePrice_Insert = dataAtMinute.Count(item =>
                    item.Status == "closePrice"
                    && item.ServerAction == "insert"
                    && item.Type == betType
                );

                int countClosePrice_Update = dataAtMinute.Count(item =>
                    item.Status == "closePrice"
                    && (item.ServerAction == "update")
                    && item.Type == betType
                );

                string colorRunning = GetColor_Wd46(countRunning_Update, countRunning_Insert, countRunning_Delete, hasDeleteFirstRunning);
                string colorSuspend = GetColor_Wd46(countSuspend_Update, countSuspend_Insert, countSuspend_Delete, hasDeleteFirstSuspend);
                string colorClosePrice = GetColorClosePrice_Wd46(countClosePrice_Update, countClosePrice_Insert, countClosePrice_Delete, hasDeleteFirstClosePrice, dataAtMinute, countClosePrice);

                dataRowRunning.Add((countRunning > 0 ? countRunning.ToString() + colorRunning : "") + (hasRunning1x2Price ? "+" : ""));
                dataRowSuspend.Add((countSuspend > 0 ? countSuspend.ToString() + colorSuspend : "") + (hasSuspend1x2Price ? "+" : ""));
                dataRowClosePrice.Add((countClosePrice > 0 ? countClosePrice.ToString() + colorClosePrice : "") + (hasClosePrice1x2Price ? "+" : ""));

            }
            //dgv.Rows.Add(headerRow.ToArray());
            //dgv.Rows.Add(dataRowRunning.ToArray());
            //dgv.Rows.Add(dataRowSuspend.ToArray());
            //dgv.Rows.Add(dataRowClosePrice.ToArray());



            return (dataRowClosePrice[minuteFirstGoal].Contains("1") && dataRowClosePrice[minuteFirstGoal].Contains("+"))
                || (dataRowClosePrice[minuteFirstGoal+1].Contains("1") && dataRowClosePrice[minuteFirstGoal+1].Contains("+"))
                || (dataRowClosePrice[minuteFirstGoal-1].Contains("1") && dataRowClosePrice[minuteFirstGoal-1].Contains("+"));

            return false;
        }


        private bool HasDeleteFirst_Wd46(List<ProductStatusLog> dataAtMinute)
        {
            for (int i = 0; i < dataAtMinute.Count; i++)
            {
                if (dataAtMinute[i].ServerAction == "delete")
                {
                    return true;
                }
                else if (dataAtMinute[i].ServerAction == "insert")
                {
                    return false;
                }
            }
            return false;
        }

        private string GetColor_Wd46(int countUpdate, int countInsert, int countDelete, bool hasDeleteFirst)
        {
            string colorRunning = "";
            if (countUpdate > 0 && countInsert == 0 && countDelete == 0)
            {
                colorRunning = "r";
            }
            else if (countUpdate > 0 && countInsert > 0 && countDelete == 0)
            {
                colorRunning = "b";
            }
            else if (countUpdate > 0 && countInsert > 0 && countDelete > 0)
            {
                colorRunning = "aa";
            }
            else if (countUpdate == 0 && countInsert == 0 && countDelete == 0)
            {
                colorRunning = "";
            }
            else if (countUpdate == 0 && countInsert > 0 && countDelete == 0)
            {
                colorRunning = "g";
            }
            else if (countUpdate == 0 && countInsert > 0 && countDelete > 0)
            {
                if (hasDeleteFirst)
                {
                    colorRunning = "dd";
                }
                else
                {
                    colorRunning = "cc";
                }
            }
            else if (countUpdate == 0 && countInsert == 0 && countDelete > 0)
            {
                colorRunning = "y";
            }
            else if (countUpdate > 0 && countInsert == 0 && countDelete > 0)
            {
                colorRunning = "o";
            }
            return colorRunning;
        }

        private string GetColorClosePrice_Wd46(int countUpdate, int countInsert, int countDelete, bool hasDeleteFirst, List<ProductStatusLog> dataAtMinute, int countClosePrice)
        {
            string colorRunning = "";
            ProductStatusLog productOu = dataAtMinute.FirstOrDefault(item => item.Type == Common.Enums.BetType.FullTimeOverUnder);
            if (productOu != null
                && productOu.TotalOuLine > 0
                && countClosePrice > 0)
            {
                if (countClosePrice - productOu.TotalOuLine >= 2)
                {
                    return "aa";
                }
            }

            if (countUpdate > 0 && countInsert == 0 && countDelete == 0)
            {
                colorRunning = "p";
            }
            else if (countUpdate > 0 && countInsert > 0 && countDelete == 0)
            {
                colorRunning = "b";
            }
            //else if (countUpdate > 0 && countInsert > 0 && countDelete > 0)
            //{
            //    colorRunning = "aa";
            //}
            else if (countUpdate == 0 && countInsert == 0 && countDelete == 0)
            {
                colorRunning = "";
            }
            else if (countUpdate == 0 && countInsert > 0 && countDelete == 0)
            {
                colorRunning = "g";
            }
            else if (countUpdate == 0 && countInsert > 0 && countDelete > 0)
            {
                if (hasDeleteFirst)
                {
                    colorRunning = "dd";
                }
                else
                {
                    colorRunning = "cc";
                }
            }
            else if (countUpdate == 0 && countInsert == 0 && countDelete > 0)
            {
                colorRunning = "y";
            }
            else if (countUpdate > 0 && countInsert == 0 && countDelete > 0)
            {
                colorRunning = "o";
            }
            return colorRunning;
        }

    }
}
