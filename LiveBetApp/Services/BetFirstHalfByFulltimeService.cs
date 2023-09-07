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
    public class BetFirstHalfByFulltimeService
    {
        private bool _betOk;
        private void ExecuteCore(int milisecond)
        {
            if (DataStore.Config == null) return;
            if (DataStore.Config.cookie == null || DataStore.Config.cookie.Length == 0) return;
            if (DataStore.Config.bong88Url == null || DataStore.Config.bong88Url.Length == 0) return;

            while (true)
            {
                try
                {
                    List<BetFirstHalfByFulltime> betFirstHalfByFulltimes = DataStore.BetFirstHalfByFulltime.Where(item => item.Hdp > 0).ToList();
                    for (int i = 0; i < betFirstHalfByFulltimes.Count; i++)
                    {
                        _betOk = false;

                        ProcessBet(betFirstHalfByFulltimes[i]);

                        if (_betOk)
                        {
                            Thread.Sleep(milisecond);
                        }

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
            BetFirstHalfByFulltime selectedItem = DataStore.BetFirstHalfByFulltime.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.BetFirstHalfByFulltime.RemoveAll(item => item.Id == id);
            DataStore.BetFirstHalfByFulltime.Add(selectedItem);
        }
        private void SetResultBetMessage(Guid id, string message)
        {
            BetFirstHalfByFulltime selectedItem = DataStore.BetFirstHalfByFulltime.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.BetFirstHalfByFulltime.RemoveAll(item => item.Id == id);
            DataStore.FinishedBetFirstHalfByFulltime.Add(selectedItem);
        }

        private void ProcessBet(BetFirstHalfByFulltime betFirstHalfByFulltime)
        {
            Match matchOfBet;
            if (betFirstHalfByFulltime == null) return;
            if (!DataStore.Matchs.TryGetValue(betFirstHalfByFulltime.MatchId, out matchOfBet)) return;

            if (matchOfBet.LivePeriod == -1
                || matchOfBet.LivePeriod == 2
                || matchOfBet.IsHT)
            {
                SetResultBetMessage(betFirstHalfByFulltime.Id, "Hiệp 1 đã qua");
                return;
            }

            if (betFirstHalfByFulltime.TotalRed != matchOfBet.HomeRed + matchOfBet.AwayRed)
            {
                SetResultBetMessage(betFirstHalfByFulltime.Id, "Có thêm thể đỏ");
                return;
            }

            Enums.BetType bettype = Enums.BetType.FullTimeOverUnder;
            Product selectedProduct = DataStore.Products.Values
                .FirstOrDefault(item =>
                    item.MatchId == matchOfBet.MatchId
                    && item.Bettype == bettype
                    && ((item.Hdp1 + item.Hdp2 - matchOfBet.LiveAwayScore - matchOfBet.LiveHomeScore) * 100) == betFirstHalfByFulltime.Hdp
                );

            var test = DataStore.Products.Values
                .Where(item =>
                    item.MatchId == matchOfBet.MatchId
                    && item.Bettype == bettype
                ).ToList();

            if (selectedProduct == null)
            {
                SetUnapproveMessage(betFirstHalfByFulltime.Id, "Không tìm thấy kèo phù hợp... Đang chờ kèo");
                return;
            }

            Product selectedProductHasGoodPrice = null;
            int productPrice = betFirstHalfByFulltime.IsOver ? selectedProduct.Odds1a100 : selectedProduct.Odds2a100;

            if ((betFirstHalfByFulltime.Price > 0 && productPrice > 0 && productPrice >= betFirstHalfByFulltime.Price)
                || (betFirstHalfByFulltime.Price > 0 && productPrice < 0)
                || (betFirstHalfByFulltime.Price < 0 && productPrice < 0 && productPrice >= betFirstHalfByFulltime.Price))
            {
                if (betFirstHalfByFulltime.IsOver)
                {
                    int selectedHdp = 0;

                    if (betFirstHalfByFulltime.BetMax)
                    {
                        selectedHdp = DataStore.Products.Values
                            .Where(item =>
                                item.MatchId == matchOfBet.MatchId
                                && item.Bettype == Enums.BetType.FirstHalfOverUnder
                            )
                            .Max(item => (int)((item.Hdp1 + item.Hdp2 - matchOfBet.LiveAwayScore - matchOfBet.LiveHomeScore) * 100));
                    }
                    else
                    {
                        selectedHdp = DataStore.Products.Values
                            .Where(item =>
                                item.MatchId == matchOfBet.MatchId
                                && item.Bettype == Enums.BetType.FirstHalfOverUnder
                            )
                            .Min(item => (int)((item.Hdp1 + item.Hdp2 - matchOfBet.LiveAwayScore - matchOfBet.LiveHomeScore) * 100));
                    }

                    selectedProductHasGoodPrice = DataStore.Products.Values
                        .FirstOrDefault(item => item.MatchId == matchOfBet.MatchId
                            && ((item.Hdp1 + item.Hdp2 - matchOfBet.LiveAwayScore - matchOfBet.LiveHomeScore) * 100) == selectedHdp
                        );

                }
                else
                {
                    int selectedHdp = 0;
                    if (betFirstHalfByFulltime.BetMax)
                    {
                        selectedHdp = DataStore.Products.Values
                            .Where(item =>
                                item.MatchId == matchOfBet.MatchId
                                && item.Bettype == Enums.BetType.FirstHalfOverUnder
                            )
                            .Max(item => (int)((item.Hdp1 + item.Hdp2 - matchOfBet.LiveAwayScore - matchOfBet.LiveHomeScore) * 100));
                    }
                    else
                    {
                        selectedHdp = DataStore.Products.Values
                            .Where(item =>
                                item.MatchId == matchOfBet.MatchId
                                && item.Bettype == Enums.BetType.FirstHalfOverUnder
                            )
                            .Min(item => (int)((item.Hdp1 + item.Hdp2 - matchOfBet.LiveAwayScore - matchOfBet.LiveHomeScore) * 100));
                    }

                    selectedProductHasGoodPrice = DataStore.Products.Values
                        .FirstOrDefault(item => item.MatchId == matchOfBet.MatchId
                            && ((item.Hdp1 + item.Hdp2 - matchOfBet.LiveAwayScore - matchOfBet.LiveHomeScore) * 100) == selectedHdp
                        );
                }


            }

            if (selectedProductHasGoodPrice == null)
            {
                SetUnapproveMessage(betFirstHalfByFulltime.Id, "Đã tìm thấy kèo " + betFirstHalfByFulltime.Hdp.ToString() + ", Đang chờ giá " + betFirstHalfByFulltime.Price.ToString());
                return;
            }

            if (selectedProductHasGoodPrice.OddsStatus != "running")
            {
                SetUnapproveMessage(betFirstHalfByFulltime.Id, "Trạng thái kèo chưa phù hợp: " + selectedProductHasGoodPrice.OddsStatus);
                return;
            }

            if (betFirstHalfByFulltime.CareGoal)
            {
                if(betFirstHalfByFulltime.TotalScore < matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)
                {
                    SetResultBetMessage(betFirstHalfByFulltime.Id, "Có thêm banh");
                    return;
                }
            }
            else
            {
                if (betFirstHalfByFulltime.GoalLimit < matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)
                {
                    SetResultBetMessage(betFirstHalfByFulltime.Id, "Banh vượt quá giới hạn");
                    return;
                }
            }
                

            try
            {
                ProcessBetModel model = Common.Functions.ConstructProcessBetOverUnderModelV2(selectedProductHasGoodPrice, matchOfBet, betFirstHalfByFulltime.Stake, betFirstHalfByFulltime.IsOver, false);
                JToken ticket = new ProcessBetService().GetTicket(model, DataStore.Config.cookie, DataStore.Config.getTicketUrl);

                if (ticket != null)
                {
                    if (ticket["ErrorCode"] != null && ticket["ErrorCode"].ToString() != "0")
                    {
                        SetUnapproveMessage(betFirstHalfByFulltime.Id, "Lỗi vé cược - Code " + ticket["ErrorCode"].ToString());
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
                        if (ticket["Data"][0]["sinfo"] != null)
                        {
                            model.sinfo = ticket["Data"][0]["sinfo"].ToString();
                        }
                        //model.oddsStatus = ticket["Data"][0]["isOddsChange"].ToString() == "true" ? "statusChanged" : "";
                    }
                }

                string betResultString = new ProcessBetService().DoBet(model, DataStore.Config.cookie, DataStore.Config.processBetUrl);
                JToken betResultJToken = JToken.Parse(betResultString);

                if (Common.Functions.CheckBetResult(betResultJToken))
                {
                    _betOk = true;
                    string message =
                        (betResultJToken["Data"]["ItemList"][0]["Message"] != null ? betResultJToken["Data"]["ItemList"][0]["Message"].ToString() : "")
                        + " - Code "
                        + (betResultJToken["Data"]["ItemList"][0]["Code"] != null ? betResultJToken["Data"]["ItemList"][0]["Code"].ToString() : "");
                    SetResultBetMessage(betFirstHalfByFulltime.Id, message);

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

                    SetUnapproveMessage(betFirstHalfByFulltime.Id, betResultJToken["ErrorMsg"].ToString() + message);
                    return;
                }

            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
                SetResultBetMessage(betFirstHalfByFulltime.Id, "Lỗi: " + ex.Message);
                return;
            }

        }

    }
}
