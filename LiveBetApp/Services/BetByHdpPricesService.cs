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
    public class BetByHdpPricesService
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
                    List<BetByHdpPrice> betByHdpPrices = DataStore.BetByHdpPrice.Where(item => item.Hdp > 0).ToList();
                    for (int i = 0; i < betByHdpPrices.Count; i++)
                    {
                        _betOk = false;
                        
                        ProcessBet(betByHdpPrices[i]);

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

        private void SetUnapproveMessage(Guid id, string message, bool needSaveLog = false)
        {
            BetByHdpPrice selectedItem = DataStore.BetByHdpPrice.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            if (needSaveLog)
            {
                selectedItem.ResultMessages.Add(message);
            }
            DataStore.BetByHdpPrice.RemoveAll(item => item.Id == id);
            DataStore.BetByHdpPrice.Add(selectedItem);
        }
        private void SetResultBetMessage(Guid id, string message)
        {
            BetByHdpPrice selectedItem = DataStore.BetByHdpPrice.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            selectedItem.ResultMessages.Add(message);
            DataStore.BetByHdpPrice.RemoveAll(item => item.Id == id);
            DataStore.FinishedBetByHdpPrice.Add(selectedItem);
        }

        private void ProcessBet(BetByHdpPrice betByHdpPrice)
        {
            Match matchOfBet;
            if (betByHdpPrice == null) return;
            if (!DataStore.Matchs.TryGetValue(betByHdpPrice.MatchId, out matchOfBet)) return;
            if (matchOfBet.LivePeriod == -1) return;

            if (betByHdpPrice.TotalRed < matchOfBet.HomeRed + matchOfBet.AwayRed)
            {
                SetResultBetMessage(betByHdpPrice.Id, "Có thêm thẻ đỏ");
                return;
            }

            Enums.BetType bettype = (betByHdpPrice.IsFulltime ? Enums.BetType.FullTimeOverUnder : Enums.BetType.FirstHalfOverUnder);
            Product selectedProduct = DataStore.Products.Values
                .FirstOrDefault(item =>
                    item.MatchId == matchOfBet.MatchId
                    && item.Bettype == bettype
                    && (((int)((item.Hdp1 + item.Hdp2)*100) - (matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)*100)) == betByHdpPrice.Hdp
                );

            if (selectedProduct == null)
            {
                SetUnapproveMessage(betByHdpPrice.Id, "Không tìm thấy kèo phù hợp... Đang chờ kèo");
                return;
            }


            Product selectedProductHasGoodPrice = null;
            int productPrice = betByHdpPrice.IsOver ? selectedProduct.Odds1a100 : selectedProduct.Odds2a100;

            if (betByHdpPrice.IsOver)
            {
                if (   (betByHdpPrice.Price > 0 && productPrice > 0 && productPrice >= betByHdpPrice.Price)
                    || (betByHdpPrice.Price < 0 && productPrice < 0 && productPrice >= betByHdpPrice.Price)
                    || (betByHdpPrice.Price > 0 && productPrice < 0))
                {
                    selectedProductHasGoodPrice = selectedProduct;
                }
            }
            else // under
            {
                if (   (betByHdpPrice.Price > 0 && productPrice > 0 && productPrice <= betByHdpPrice.Price)
                    || (betByHdpPrice.Price < 0 && productPrice < 0 && productPrice <= betByHdpPrice.Price)
                    || (betByHdpPrice.Price < 0 && productPrice > 0))
                {
                    selectedProductHasGoodPrice = selectedProduct;
                }
            }

            if (selectedProductHasGoodPrice == null)
            {
                SetUnapproveMessage(betByHdpPrice.Id, "Đã tìm thấy kèo " + betByHdpPrice.Hdp.ToString() + ", Đang chờ giá " + betByHdpPrice.Price.ToString());
                return;
            }

            if (betByHdpPrice.CareGoal)
            {
                if (betByHdpPrice.TotalScore < matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)
                {
                    SetResultBetMessage(betByHdpPrice.Id, "Có thêm banh");
                    return;
                }
            }
            else
            {
                if (betByHdpPrice.GoalLimit < matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)
                {
                    SetResultBetMessage(betByHdpPrice.Id, "Vượt quá giới hạn banh");
                    return;
                }
            }

            if (selectedProductHasGoodPrice.OddsStatus != "running")
            {
                SetUnapproveMessage(betByHdpPrice.Id, "Trạng thái kèo chưa phù hợp: " + selectedProductHasGoodPrice.OddsStatus);
                return;
            }

            if (matchOfBet.LivePeriod > 0
                && betByHdpPrice.UpToMinute > 0
                && matchOfBet.TimeSpanFromStart.TotalMinutes + (matchOfBet.LivePeriod - 1) * 45 > betByHdpPrice.UpToMinute)
            {
                SetResultBetMessage(betByHdpPrice.Id, "Tìm thấy kèo/giá nhưng đã lố thời gian: " + betByHdpPrice.UpToMinute);
                return;
            }

            try
            {
                ProcessBetModel model = Common.Functions.ConstructProcessBetOverUnderModelV2(selectedProductHasGoodPrice, matchOfBet, betByHdpPrice.Stake, betByHdpPrice.IsOver, betByHdpPrice.IsFulltime);
                JToken ticket = new ProcessBetService().GetTicket(model, DataStore.Config.cookie, DataStore.Config.getTicketUrl);

                if (ticket != null)
                {
                    if (ticket["ErrorCode"] != null && ticket["ErrorCode"].ToString() != "0")
                    {
                        SetUnapproveMessage(betByHdpPrice.Id, "Lỗi vé cược - Code " + ticket["ErrorCode"].ToString(), true);
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
                    SetResultBetMessage(betByHdpPrice.Id, message);

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

                    SetUnapproveMessage(betByHdpPrice.Id, betResultJToken["ErrorMsg"].ToString() + message, true);
                    return;
                }

            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
                //if (ex.Message.ToLower().Contains("unexpected character encountered while parsing value"))
                //{
                //    BetByHdpPrice oldOne = DataStore.BetByHdpPrice.FirstOrDefault(item => item.Id == betByHdpPrice.Id);
                //    if (oldOne != null)
                //    {
                //        DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                //        {
                //            Id = Guid.NewGuid(),
                //            MatchId = oldOne.MatchId,
                //            LivePeriod = oldOne.LivePeriod,
                //            Hdp = oldOne.Hdp,
                //            Price = oldOne.Price,
                //            Stake = oldOne.Stake,
                //            IsOver = oldOne.IsOver,
                //            IsFulltime = oldOne.IsFulltime,
                //            TotalScore = oldOne.TotalScore,
                //            TotalRed = oldOne.TotalRed,
                //            CreateDateTime = DateTime.Now,
                //            MoneyLine = oldOne.MoneyLine,
                //            AutoBetMessage = oldOne.AutoBetMessage + " Đặt lại"
                //        });
                //    }
                //}
                SetResultBetMessage(betByHdpPrice.Id, "Lỗi: " + ex.Message);
                return;
            }

        }




    }
}
