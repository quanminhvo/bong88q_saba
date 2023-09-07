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
    public class BetByTimmingService
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
                    List<BetByTimming> betByTimmings = DataStore.BetByTimming
                        .Where(item => item.Minute > 0)
                        .OrderBy(item => item.LivePeriod)
                        .ThenBy(item => item.Minute)
                        .ToList();

                    for (int i = 0; i < betByTimmings.Count; i++)
                    {
                        _betOk = false;

                        ProcessBet(betByTimmings[i]);

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
            BetByTimming selectedItem = DataStore.BetByTimming.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.BetByTimming.RemoveAll(item => item.Id == id);
            DataStore.BetByTimming.Add(selectedItem);
        }
        private void SetResultBetMessage(Guid id, string message)
        {
            BetByTimming selectedItem = DataStore.BetByTimming.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.BetByTimming.RemoveAll(item => item.Id == id);
            DataStore.FinishedBetByTimming.Add(selectedItem);
        }

        private Product GetSelectedProduct(BetByTimming betByTimming, Match matchOfBet)
        {
            Product selectedProduct = null;
            int totalGoal = matchOfBet.LiveHomeScore + matchOfBet.LiveAwayScore;
            if (betByTimming.BetOption == Common.Enums.BetByTimmingOption.FIRST_HALF_MAX)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByTimming.MatchId
                        && item.Bettype == Enums.BetType.FirstHalfOverUnder
                    )
                    .OrderBy(item => (item.Hdp1 * 100))
                    .LastOrDefault();

            }
            else if (betByTimming.BetOption == Common.Enums.BetByTimmingOption.FIRST_HALF_MIN)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByTimming.MatchId
                        && item.Bettype == Enums.BetType.FirstHalfOverUnder
                    )
                    .OrderBy(item => (item.Hdp1 * 100))
                    .FirstOrDefault();
            }
            else if (betByTimming.BetOption == Common.Enums.BetByTimmingOption.FULL_TIME_TYPE_X00)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByTimming.MatchId
                        && item.Bettype == Common.Enums.BetType.FullTimeOverUnder
                        && (item.Hdp1 * 100) % 100 == 0
                    )
                    .FirstOrDefault();
            }
            else if (betByTimming.BetOption == Common.Enums.BetByTimmingOption.FULL_TIME_TYPE_X25)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByTimming.MatchId
                        && item.Bettype == Common.Enums.BetType.FullTimeOverUnder
                        && (item.Hdp1 * 100) % 100 == 25
                    )
                    .FirstOrDefault();
            }
            else if (betByTimming.BetOption == Common.Enums.BetByTimmingOption.FULL_TIME_TYPE_X50)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByTimming.MatchId
                        && item.Bettype == Common.Enums.BetType.FullTimeOverUnder
                        && (item.Hdp1 * 100) % 100 == 50
                    )
                    .FirstOrDefault();
            }
            else if (betByTimming.BetOption == Common.Enums.BetByTimmingOption.FULL_TIME_TYPE_X75)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByTimming.MatchId
                        && item.Bettype == Common.Enums.BetType.FullTimeOverUnder
                        && (item.Hdp1 * 100) % 100 == 75
                    )
                    .FirstOrDefault();
            }

            return selectedProduct;
        }

        private void ProcessBet(BetByTimming betByTimming)
        {
            Match matchOfBet;

            if (betByTimming == null) return;
            if (!DataStore.Matchs.TryGetValue(betByTimming.MatchId, out matchOfBet)) return;

            if (betByTimming.TotalRed != matchOfBet.HomeRed + matchOfBet.AwayRed)
            {
                SetResultBetMessage(betByTimming.Id, "Có thêm thẻ đỏ");
                return;
            }

            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(matchOfBet.LiveTimer).TotalMinutes);

            if (matchOfBet.LivePeriod != betByTimming.LivePeriod)
            {
                return;
            }

            if (currentMinute < betByTimming.Minute)
            {
                SetUnapproveMessage(betByTimming.Id, "Đang chờ đến phút " + betByTimming.Minute);
                return;
            }
            else if (currentMinute > betByTimming.Minute + 2)
            {
                SetUnapproveMessage(betByTimming.Id, "Đã vượt qua phút " + betByTimming.Minute);
                return;
            }

            if (DateTime.Now.Second <= 26)
            {
                SetUnapproveMessage(betByTimming.Id, "Đang chờ đến giây 27");
                return;
            }

            Product selectedProduct = GetSelectedProduct(betByTimming, matchOfBet);
            if (selectedProduct == null)
            {
                SetUnapproveMessage(betByTimming.Id, "Không tìm thấy kèo " + betByTimming.BetOption);
                return;
            }

            if (betByTimming.CareGoal)
            {
                if (betByTimming.TotalScore < matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)
                {
                    SetResultBetMessage(betByTimming.Id, "Có thêm banh");
                    return;
                }
            }
            else
            {
                if (betByTimming.GoalLimit < matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)
                {
                    SetResultBetMessage(betByTimming.Id, "Vượt quá giới hạn banh");
                    return;
                }
            }

            try
            {
                ProcessBetModel model = Common.Functions.ConstructProcessBetOverUnderModelV2(
                    selectedProduct,
                    matchOfBet,
                    betByTimming.Stake,
                    true,
                    selectedProduct.Bettype == LiveBetApp.Common.Enums.BetType.FirstHalfOverUnder
                );
                JToken ticket = new ProcessBetService().GetTicket(model, DataStore.Config.cookie, DataStore.Config.getTicketUrl);

                if (ticket != null)
                {
                    if (ticket["ErrorCode"] != null && ticket["ErrorCode"].ToString() != "0")
                    {
                        SetUnapproveMessage(betByTimming.Id, "Lỗi vé cược - Code " + ticket["ErrorCode"].ToString());
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
                    SetResultBetMessage(betByTimming.Id, message);

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

                    SetUnapproveMessage(betByTimming.Id, betResultJToken["ErrorMsg"].ToString() + message);
                    return;
                }

            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
                SetResultBetMessage(betByTimming.Id, "Lỗi: " + ex.Message);
                return;
            }

        }
    }
}
