//using LiveBetApp.Common;
//using LiveBetApp.Models.DataModels;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace LiveBetApp.Services
//{
//    public class BetByHdpPricesIuooService
//    {
//        private bool _betOk;

//        private void ExecuteCore(int milisecond)
//        {
//            if (DataStore.Config == null) return;
//            if (DataStore.Config.cookie == null || DataStore.Config.cookie.Length == 0) return;
//            if (DataStore.Config.bong88Url == null || DataStore.Config.bong88Url.Length == 0) return;

//            while (true)
//            {
//                try
//                {
//                    List<BetByHdpPrice> betByHdpPrices = DataStore.BetByHdpPriceIuoo.Where(item => item.Hdp > 0).ToList();
//                    for (int i = 0; i < betByHdpPrices.Count; i++)
//                    {
//                        _betOk = false;
                        
//                        ProcessBet(betByHdpPrices[i]);

//                        if (_betOk)
//                        {
//                            Thread.Sleep(milisecond);
//                        }

//                    }
//                }
//                catch
//                {

//                }
//                finally
//                {
//                    Thread.Sleep(milisecond);
//                }

//            }
//        }

//        public void Execute(int milisecond)
//        {
//            Thread coreServiceThread;
//            coreServiceThread = new Thread(() =>
//            {
//                ExecuteCore(milisecond);
//            });
//            coreServiceThread.IsBackground = true;
//            coreServiceThread.Start();
//        }

//        private void SetUnapproveMessage(Guid id, string message)
//        {
//            BetByHdpPrice selectedItem = DataStore.BetByHdpPriceIuoo.FirstOrDefault(item => item.Id == id);
//            selectedItem.ResultMessage = message;
//            DataStore.BetByHdpPriceIuoo.RemoveAll(item => item.Id == id);
//            DataStore.BetByHdpPriceIuoo.Add(selectedItem);
//        }
//        private void SetResultBetMessage(Guid id, string message)
//        {
//            BetByHdpPrice selectedItem = DataStore.BetByHdpPriceIuoo.FirstOrDefault(item => item.Id == id);
//            selectedItem.ResultMessage = message;
//            DataStore.BetByHdpPriceIuoo.RemoveAll(item => item.Id == id);
//            DataStore.FinishedBetByHdpPriceIuoo.Add(selectedItem);
//        }


//        private void ProcessBet(BetByHdpPrice betByHdpPrice)
//        {
//            Match matchOfBet;
//            if (betByHdpPrice == null) return;
//            if (!DataStore.Matchs.TryGetValue(betByHdpPrice.MatchId, out matchOfBet)) return;
//            //if (betByHdpPrice.LivePeriod != matchOfBet.LivePeriod) return;

//            Enums.BetType bettype = (betByHdpPrice.IsFulltime ? Enums.BetType.FullTimeOverUnder : Enums.BetType.FirstHalfOverUnder);
//            Product selectedProduct = DataStore.Products.Values
//                .FirstOrDefault(item =>
//                    item.MatchId == matchOfBet.MatchId
//                    && item.Bettype == bettype
//                    && ((item.Hdp1 + item.Hdp2 - matchOfBet.LiveAwayScore - matchOfBet.LiveHomeScore) * 100) == betByHdpPrice.Hdp
//                );

//            if (selectedProduct == null)
//            {
//                SetUnapproveMessage(betByHdpPrice.Id, "Không tìm thấy kèo phù hợp... Đang chờ kèo");
//                return;
//            }


//            Product selectedProductHasGoodPrice = null;
//            int productPrice = betByHdpPrice.IsOver ? selectedProduct.Odds1a100 : selectedProduct.Odds2a100;

//            if (betByHdpPrice.IsOver)
//            {
//                if (   (betByHdpPrice.Price > 0 && productPrice > 0 && productPrice >= betByHdpPrice.Price)
//                    || (betByHdpPrice.Price < 0 && productPrice < 0 && productPrice >= betByHdpPrice.Price)
//                    || (betByHdpPrice.Price > 0 && productPrice < 0))
//                {
//                    selectedProductHasGoodPrice = selectedProduct;
//                }
//            }
//            else // under
//            {
//                if (   (betByHdpPrice.Price > 0 && productPrice > 0 && productPrice <= betByHdpPrice.Price)
//                    || (betByHdpPrice.Price < 0 && productPrice < 0 && productPrice <= betByHdpPrice.Price)
//                    || (betByHdpPrice.Price < 0 && productPrice > 0))
//                {
//                    selectedProductHasGoodPrice = selectedProduct;
//                }
//            }


//            if (selectedProductHasGoodPrice == null)
//            {
//                SetUnapproveMessage(betByHdpPrice.Id, "Đã tìm thấy kèo " + betByHdpPrice.Hdp.ToString() + ", Đang chờ giá " + betByHdpPrice.Price.ToString());
//                return;
//            }

//            if (selectedProductHasGoodPrice.OddsStatus != "running")
//            {
//                SetUnapproveMessage(betByHdpPrice.Id, "Trạng thái kèo chưa phù hợp: " + selectedProductHasGoodPrice.OddsStatus);
//                return;
//            }

//            try
//            {
//                ProcessBetModel model = Common.Functions.ConstructProcessBetOverUnderModel(selectedProductHasGoodPrice, matchOfBet, betByHdpPrice.Stake, betByHdpPrice.IsOver, betByHdpPrice.IsFulltime);
//                JToken ticket = new ProcessBetService().GetTicket(model, DataStore.Config.cookie, DataStore.Config.getTicketUrl);

//                if (ticket != null)
//                {
//                    if (ticket["ErrorCode"] != null && ticket["ErrorCode"].ToString() != "0")
//                    {
//                        SetUnapproveMessage(betByHdpPrice.Id, "Lỗi vé cược - Code " + ticket["ErrorCode"].ToString());
//                        return;
//                    }
//                    else if (ticket["Data"] != null && ticket["Data"][0] != null)
//                    {
//                        model.oddsStatus = ticket["Data"][0]["isOddsChange"].ToString() == "true" ? "statusChanged" : "";
//                    }
//                }

//                string betResultString = new ProcessBetService().DoBet(model, DataStore.Config.cookie, DataStore.Config.processBetUrl);
//                JToken betResultJToken = JToken.Parse(betResultString);

//                if (Common.Functions.CheckBetResult(betResultJToken))
//                {
//                    _betOk = true;
//                    string message =
//                        (betResultJToken["Data"]["ItemList"][0]["Message"] != null ? betResultJToken["Data"]["ItemList"][0]["Message"].ToString() : "")
//                        + " - Code "
//                        + (betResultJToken["Data"]["ItemList"][0]["Code"] != null ? betResultJToken["Data"]["ItemList"][0]["Code"].ToString() : "");
//                    SetResultBetMessage(betByHdpPrice.Id, message);

//                    Common.Functions.WriteJsonObjectData<JToken>(
//                        "BetLog\\betResultJToken_ok_" + Common.Functions.GetDimDatetime(DateTime.Now),
//                        betResultJToken
//                    );

//                    float price = 0;
//                    float hdp = 0;
//                    if (float.TryParse(betResultJToken["Data"]["ItemList"][0]["DisplayHDP"].ToString(), out hdp)
//                        && float.TryParse(betResultJToken["Data"]["ItemList"][0]["DisplayOdds"].ToString(), out price)
//                        && DataStore.InUnderOutOvers[betByHdpPrice.MatchId].ContainsKey((int)(hdp * 100.0)))
//                    {
//                        int hdp_100 = (int)(hdp * 100.0);
//                        price = price * 100;
//                        if (!betByHdpPrice.IsOver)
//                        {
//                            DataStore.InUnderOutOvers[betByHdpPrice.MatchId][hdp_100].ActualBetPriceUnder = (int)price;
//                        }
//                        else
//                        {
//                            DataStore.InUnderOutOvers[betByHdpPrice.MatchId][hdp_100].ActualBetPriceOver = (int)price;
//                        }

//                        for (int i=0; i<DataStore.FinishedBetByHdpPriceIuoo.Count; i++)
//                        {
//                            if (DataStore.FinishedBetByHdpPriceIuoo[i].Id == betByHdpPrice.Id)
//                            {
//                                DataStore.FinishedBetByHdpPriceIuoo[i].Price = (int)price;
//                            }
//                        }
//                    }

//                }
//                else
//                {
//                    Common.Functions.WriteJsonObjectData<JToken>(
//                        "BetLog\\betResultJToken_fail_" + Common.Functions.GetDimDatetime(DateTime.Now),
//                        betResultJToken
//                    );

//                    string message = "";
//                    try
//                    {
//                        message = (betResultJToken["Data"]["ItemList"][0]["Message"] != null ? betResultJToken["Data"]["ItemList"][0]["Message"].ToString() : "");
//                    }
//                    catch { }

//                    SetUnapproveMessage(betByHdpPrice.Id, betResultJToken["ErrorMsg"].ToString() + message);
//                    return;
//                }

//            }
//            catch (Exception ex)
//            {
//                Common.Functions.WriteExceptionLog(ex);
//                SetResultBetMessage(betByHdpPrice.Id, "Lỗi: " + ex.Message);
//                return;
//            }

//        }
//    }
//}
