using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services
{
    public class TimmingBetOverUnderService
    {
        public TimmingBetOverUnderService()
        {

        }

        private void ExecuteCore(int milisecond)
        {
            if (DataStore.Config == null) return;
            if (DataStore.Config.cookie == null || DataStore.Config.cookie.Length == 0) return;
            if (DataStore.Config.bong88Url == null || DataStore.Config.bong88Url.Length == 0) return;

            while (true)
            {
                try
                {
                    List<TimmingBetOverUnder> timmingBetOvers = DataStore.TimmingBetOverUnder;
                    for (int i = 0; i < timmingBetOvers.Count; i++)
                    {
                        ProcessBet(timmingBetOvers[i]);
                    }
                }
                catch
                {

                }
                finally
                {
                    Thread.Sleep(milisecond);
                }

            }
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

        private void SetUnapproveMessage(Guid id, string message)
        {
            TimmingBetOverUnder selectedItem = DataStore.TimmingBetOverUnder.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.TimmingBetOverUnder.RemoveAll(item => item.Id == id);
            DataStore.TimmingBetOverUnder.Add(selectedItem);
        }
        private void SetResultBetMessage(Guid id, string message)
        {
            TimmingBetOverUnder selectedItem = DataStore.TimmingBetOverUnder.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.TimmingBetOverUnder.RemoveAll(item => item.Id == id);
            DataStore.FinishedTimmingBetOverUnder.Add(selectedItem);
        }


        private void ProcessBet(TimmingBetOverUnder timmingBetOver)
        {
            Match matchOfBet;
            if (timmingBetOver == null) return;
            if (!DataStore.Matchs.TryGetValue(timmingBetOver.MatchId, out matchOfBet)) return;

            if (timmingBetOver.LivePeriod == matchOfBet.LivePeriod && (timmingBetOver.Minute <= (int)matchOfBet.TimeSpanFromStart.TotalMinutes))
            {
                if (timmingBetOver.CareGoal
                    && timmingBetOver.TotalScore < matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)
                {
                    SetResultBetMessage(timmingBetOver.Id, "Có thêm banh");
                    return;
                }

                if (matchOfBet.HomeRed + matchOfBet.AwayRed != timmingBetOver.TotalRed)
                {
                    SetResultBetMessage(timmingBetOver.Id, "Có thêm thể đỏ");
                    return;
                }

                Enums.BetType bettype = (timmingBetOver.IsFulltime ? Enums.BetType.FullTimeOverUnder : Enums.BetType.FirstHalfOverUnder);

                List<Product> productsOfMatch = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == matchOfBet.MatchId
                        && item.Bettype == bettype
                    ).ToList();

                if (productsOfMatch == null || productsOfMatch.Count == 0)
                {
                    SetUnapproveMessage(timmingBetOver.Id, "Không tìm thấy kèo phù hợp... Đang chờ kèo");
                    return;
                }

                int selectedScore_100 = 0;
                Product selectedProduct = null;

                //if (timmingBetOver.IsOver)
                //{
                //    selectedScore = productsOfMatch.Min(item => item.Hdp1 + item.Hdp2);
                //}
                //else
                //{
                //    selectedScore = productsOfMatch.Max(item => item.Hdp1 + item.Hdp2);
                //}

                if (productsOfMatch.Any(item => (item.Hdp1 + item.Hdp2 - matchOfBet.LiveAwayScore - matchOfBet.LiveHomeScore) * 100 == 50))
                {
                    selectedScore_100 = ((matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore) * 100 + 50);
                }
                else
                {
                    selectedScore_100 = (int)(productsOfMatch.Max(item => item.Hdp1 + item.Hdp2) * 100);
                }

                selectedProduct = productsOfMatch.FirstOrDefault(item => ((item.Hdp1 + item.Hdp2) * 100) == selectedScore_100);

                
                if (selectedProduct.OddsStatus != "running")
                {
                    SetUnapproveMessage(timmingBetOver.Id, "Trạng thái kèo chưa phù hợp: " + selectedProduct.OddsStatus);
                    return;
                }
                else
                {
                    try
                    {
                        ProcessBetModel model = Common.Functions.ConstructProcessBetOverUnderModel(selectedProduct, matchOfBet, timmingBetOver.Stake, timmingBetOver.IsOver, timmingBetOver.IsFulltime);
                        JToken ticket = new ProcessBetService().GetTicket(model, DataStore.Config.cookie, DataStore.Config.getTicketUrl);

                        if (ticket != null)
                        {
                            if (ticket["ErrorCode"] != null && ticket["ErrorCode"].ToString() != "0")
                            {
                                SetUnapproveMessage(timmingBetOver.Id, "Lỗi vé cược - Code " + ticket["ErrorCode"].ToString());
                                return;
                            }
                            else if (ticket["Data"] != null && ticket["Data"][0] != null)
                            {
                                int maxbet = 10;
                                int currentBet = 10;
                                if (int.TryParse(ticket["Data"][0]["Maxbet"].ToString().Replace(",", ""), out maxbet)
                                    && int.TryParse(model.Stake, out currentBet))
                                {
                                    if (currentBet > maxbet)
                                    {
                                        model.Stake = maxbet.ToString();
                                    }
                                }
                                //model.oddsStatus = ticket["Data"][0]["isOddsChange"].ToString() == "true" ? "statusChanged" : "";
                            }
                        }

                        string betResultString = new ProcessBetService().DoBet(model, DataStore.Config.cookie, DataStore.Config.processBetUrl);
                        JToken betResultJToken = JToken.Parse(betResultString);

                        if (Common.Functions.CheckBetResult(betResultJToken))
                        {
                            string message = 
                                (betResultJToken["Data"]["ItemList"][0]["Message"] != null ? betResultJToken["Data"]["ItemList"][0]["Message"].ToString() : "")
                                + " - Code "
                                + (betResultJToken["Data"]["ItemList"][0]["Code"] != null ? betResultJToken["Data"]["ItemList"][0]["Code"].ToString() : "");
                            SetResultBetMessage(timmingBetOver.Id, message);

                            Common.Functions.WriteJsonObjectData<JToken>(
                                "BetLog\\betResultJToken_ok_" + Common.Functions.GetDimDatetime(DateTime.Now),
                                betResultJToken
                            );
                        }
                        else
                        {
                            Common.Functions.WriteJsonObjectData<JToken>(
                                "BetLog\\betResultJToken_fail_" + Common.Functions.GetDimDatetime(DateTime.Now),
                                betResultJToken
                            );

                            string message = "";
                            try
                            {
                                message = (betResultJToken["Data"]["ItemList"][0]["Message"] != null ? betResultJToken["Data"]["ItemList"][0]["Message"].ToString() : "");
                            }
                            catch { }

                            SetUnapproveMessage(timmingBetOver.Id, betResultJToken["ErrorMsg"].ToString() + message);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.Functions.WriteExceptionLog(ex);
                        SetResultBetMessage(timmingBetOver.Id, "Lỗi: " + ex.Message);
                        return;                        
                    }
                }
            }
        }

        private ProcessBetModel ConstructProcessBetOverUnderModel(Product product, Match match, int stake, bool isOver, bool isFulltime)
        {
        //    ProcessBetModel result = new ProcessBetModel()
        //    {
        //        sportname = "Bóng đá",
        //        gamename = "",
        //        bettypename = isFulltime ? "Tài/Xỉu" : "Hiệp 1 -  Tài/Xỉu",
        //        ChoiceValue = isOver ? "Tài" : "Xỉu",
        //        Line = product.Hdp1.ToString(),
        //        displayHDP = product.Hdp1.ToString(),
        //        odds = isOver ? product.Odds1a.ToString() : product.Odds2a.ToString(),
        //        home = match.Home,
        //        away = match.Away,
        //        league = match.League,
        //        IsLive = "true",
        //        ProgramID = "",
        //        RaceNum = "0",
        //        Runner = "0",
        //        PoolType = "1",
        //        imgurl = "",
        //        BetID = "",
        //        type = "OU",
        //        bettype = isFulltime ? "3" : "8",
        //        oddsid = product.OddsId.ToString(),
        //        Hscore = match.LiveHomeScore.ToString(),
        //        Ascore = match.LiveAwayScore.ToString(),
        //        Matchid = match.MatchId.ToString(),
        //        betteam = isOver ? "h" : "a",
        //        stake = stake < 3 ? "3" : stake.ToString(),
        //        gameid = "1",
        //        MRPercentage = "",
        //        OddsInfo = "",
        //        LiveInfo = "[" + match.LiveHomeScore.ToString() + "-" + match.LiveAwayScore.ToString() + "]",
        //        AcceptBetterOdds = "true",
        //        AutoAcceptSec = "",
        //        ParentTypeId = isFulltime ? "3" : "8",
        //        showLiveScore = "true",
        //        colorHomeTeam = "",
        //        colorAwayTeam = "",
        //        hasParlay = match.McParlay > 0 ? "true" : "false",
        //        matchcode = "",
        //        isQuickBet = "true",
        //        UseBonus = "0",
        //        kickofftime = match.KickoffTimeNumber.ToString(),
        //        ShowTime = match.ShowTime,
        //        oddsStatus = "",
        //        min = "3",
        //        max = product.MaxBet.ToString(),
        //    };

        //    return result;
            return null;
        }
    }
}
