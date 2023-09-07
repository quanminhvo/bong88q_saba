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
    public class BetDirectService
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
                    List<BetDirect> betDirects = DataStore.BetDirect.ToList();
                    for (int i = 0; i < betDirects.Count; i++)
                    {
                        _betOk = false;

                        ProcessBet(betDirects[i]);

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
            BetDirect selectedItem = DataStore.BetDirect.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.BetDirect.RemoveAll(item => item.Id == id);
            DataStore.BetDirect.Add(selectedItem);
        }
        private void SetResultBetMessage(Guid id, string message)
        {
            BetDirect selectedItem = DataStore.BetDirect.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.BetDirect.RemoveAll(item => item.Id == id);
            DataStore.FinishedBetDirect.Add(selectedItem);
        }

        private void ProcessBet(BetDirect betDirect)
        {
            Match matchOfBet;
            if (betDirect == null) return;
            if (!DataStore.Matchs.TryGetValue(betDirect.MatchId, out matchOfBet)) return;

            Enums.BetType bettype = (betDirect.IsFt ? Enums.BetType.FullTimeOverUnder : Enums.BetType.FirstHalfOverUnder);
            List<Product> availableProducts = DataStore.Products.Values
                .Where(item => item.MatchId == matchOfBet.MatchId && item.Bettype == bettype)
                .OrderBy(item => item.Hdp1)
                .ToList();

            if (availableProducts.Count < betDirect.Order)
            {
                SetResultBetMessage(betDirect.Id, "Chỉ tìm thấy " + availableProducts.Count + " kèo");
                return;
            }

            Product selectedProduct = availableProducts[betDirect.Order - 1];

            if (selectedProduct.OddsStatus != "running")
            {
                SetUnapproveMessage(betDirect.Id, "Trạng thái kèo chưa phù hợp: " + selectedProduct.OddsStatus);
                return;
            }

            try
            {
                ProcessBetModel model = Common.Functions.ConstructProcessBetOverUnderModelV2(selectedProduct, matchOfBet, betDirect.Stake, false, betDirect.IsFt);
                JToken ticket = new ProcessBetService().GetTicket(model, DataStore.Config.cookie, DataStore.Config.getTicketUrl);

                if (ticket != null)
                {
                    if (ticket["ErrorCode"] != null && ticket["ErrorCode"].ToString() != "0")
                    {
                        SetUnapproveMessage(betDirect.Id, "Lỗi vé cược - Code " + ticket["ErrorCode"].ToString());
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
                    SetResultBetMessage(betDirect.Id, message);

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

                    SetUnapproveMessage(betDirect.Id, betResultJToken["ErrorMsg"].ToString() + message);
                    return;
                }

            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
                SetResultBetMessage(betDirect.Id, "Lỗi: " + ex.Message);
                return;
            }

        }

    }
}
