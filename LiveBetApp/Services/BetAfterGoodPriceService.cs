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
    public class BetAfterGoodPriceService
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
                    List<BetAfterGoodPrice> betAfterGoodPrices = DataStore.BetAfterGoodPrice.Where(item => item.Hdp > 0).ToList();
                    for (int i = 0; i < betAfterGoodPrices.Count; i++)
                    {
                        _betOk = false;

                        ProcessBet(betAfterGoodPrices[i]);

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
            BetAfterGoodPrice selectedItem = DataStore.BetAfterGoodPrice.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.BetAfterGoodPrice.RemoveAll(item => item.Id == id);
            DataStore.BetAfterGoodPrice.Add(selectedItem);
        }
        private void SetResultBetMessage(Guid id, string message)
        {
            BetAfterGoodPrice selectedItem = DataStore.BetAfterGoodPrice.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.BetAfterGoodPrice.RemoveAll(item => item.Id == id);
            DataStore.FinishedBetAfterGoodPrice.Add(selectedItem);
        }

        private void ProcessBet(BetAfterGoodPrice betAfterGoodPrice)
        {
            Match matchOfBet;
            if (betAfterGoodPrice == null) return;
            if (!DataStore.Matchs.TryGetValue(betAfterGoodPrice.MatchId, out matchOfBet)) return;
            if (matchOfBet.LivePeriod == -1) return;

            if (betAfterGoodPrice.TotalRed < matchOfBet.HomeRed + matchOfBet.AwayRed)
            {
                SetResultBetMessage(betAfterGoodPrice.Id, "Có thêm thẻ đỏ");
                return;
            }

            Enums.BetType bettype = (betAfterGoodPrice.IsFulltime ? Enums.BetType.FullTimeOverUnder : Enums.BetType.FirstHalfOverUnder);
            Product selectedProduct = DataStore.Products.Values
                .FirstOrDefault(item =>
                    item.MatchId == matchOfBet.MatchId
                    && item.Bettype == bettype
                    && (((item.Hdp1 + item.Hdp2) * 100 - (matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore) * 100)) == betAfterGoodPrice.Hdp
                );

            if (selectedProduct == null)
            {
                SetUnapproveMessage(betAfterGoodPrice.Id, "Không tìm thấy kèo phù hợp... Đang chờ kèo");
                return;
            }


            Product selectedProductHasGoodPrice = null;
            int productPrice = betAfterGoodPrice.IsOver ? selectedProduct.Odds1a100 : selectedProduct.Odds2a100;

            if (betAfterGoodPrice.IsOver)
            {
                if ((betAfterGoodPrice.Price > 0 && productPrice > 0 && productPrice >= betAfterGoodPrice.Price)
                    || (betAfterGoodPrice.Price < 0 && productPrice < 0 && productPrice >= betAfterGoodPrice.Price)
                    || (betAfterGoodPrice.Price > 0 && productPrice < 0))
                {
                    selectedProductHasGoodPrice = selectedProduct;
                }
            }
            else // under
            {
                if ((betAfterGoodPrice.Price > 0 && productPrice > 0 && productPrice <= betAfterGoodPrice.Price)
                    || (betAfterGoodPrice.Price < 0 && productPrice < 0 && productPrice <= betAfterGoodPrice.Price)
                    || (betAfterGoodPrice.Price < 0 && productPrice > 0))
                {
                    selectedProductHasGoodPrice = selectedProduct;
                }
            }

            if (selectedProductHasGoodPrice == null)
            {
                SetUnapproveMessage(betAfterGoodPrice.Id, "Đã tìm thấy kèo " + betAfterGoodPrice.Hdp.ToString() + ", Đang chờ giá " + betAfterGoodPrice.Price.ToString());
                return;
            }

            if (betAfterGoodPrice.CareGoal
                && betAfterGoodPrice.TotalScore < matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)
            {
                SetResultBetMessage(betAfterGoodPrice.Id, "Có thêm banh");
                return;
            }

            if (selectedProductHasGoodPrice.OddsStatus != "running")
            {
                SetUnapproveMessage(betAfterGoodPrice.Id, "Trạng thái kèo chưa phù hợp: " + selectedProductHasGoodPrice.OddsStatus);
                return;
            }

            if (matchOfBet.LivePeriod > 0
                && betAfterGoodPrice.UpToMinute > 0
                && matchOfBet.TimeSpanFromStart.TotalMinutes + (matchOfBet.LivePeriod - 1) * 45 > betAfterGoodPrice.UpToMinute)
            {
                SetResultBetMessage(betAfterGoodPrice.Id, "Tìm thấy kèo/giá nhưng đã lố thời gian: " + betAfterGoodPrice.UpToMinute);
                return;
            }

            if (betAfterGoodPrice.MinuteAfterGoodPrice > 0
                && betAfterGoodPrice.FirstMinuteHasGoodPrice == 0
                && matchOfBet.LivePeriod > 0)
            {
                betAfterGoodPrice.FirstMinuteHasGoodPrice = (int)matchOfBet.TimeSpanFromStart.TotalMinutes + (matchOfBet.LivePeriod - 1) * 45;
                SetUnapproveMessage(betAfterGoodPrice.Id, "Đã tìm thấy kèo/giá đang chờ thêm : " + betAfterGoodPrice.MinuteAfterGoodPrice + " phút");
                return;
            }

            int minuteOfMatch = (int)matchOfBet.TimeSpanFromStart.TotalMinutes + (matchOfBet.LivePeriod - 1) * 45;
            if (betAfterGoodPrice.FirstMinuteHasGoodPrice + betAfterGoodPrice.MinuteAfterGoodPrice > minuteOfMatch
                || matchOfBet.LivePeriod == 0)
            {
                SetUnapproveMessage(betAfterGoodPrice.Id, "Đã tìm thấy kèo/giá đang chờ thêm : " + betAfterGoodPrice.MinuteAfterGoodPrice + " phút");
                return;
            }

            try
            {
                ProcessBetModel model = Common.Functions.ConstructProcessBetOverUnderModelV2(selectedProductHasGoodPrice, matchOfBet, betAfterGoodPrice.Stake, betAfterGoodPrice.IsOver, betAfterGoodPrice.IsFulltime);
                JToken ticket = new ProcessBetService().GetTicket(model, DataStore.Config.cookie, DataStore.Config.getTicketUrl);

                if (ticket != null)
                {
                    if (ticket["ErrorCode"] != null && ticket["ErrorCode"].ToString() != "0")
                    {
                        SetUnapproveMessage(betAfterGoodPrice.Id, "Lỗi vé cược - Code " + ticket["ErrorCode"].ToString());
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
                    SetResultBetMessage(betAfterGoodPrice.Id, message);

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

                    SetUnapproveMessage(betAfterGoodPrice.Id, betResultJToken["ErrorMsg"].ToString() + message);
                    return;
                }

            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
                SetResultBetMessage(betAfterGoodPrice.Id, "Lỗi: " + ex.Message);
                return;
            }

        }




    }
}
