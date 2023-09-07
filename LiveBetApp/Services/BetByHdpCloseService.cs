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
    public class BetByHdpCloseService
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
                    List<BetByHdpClose> betByHdpCloses = DataStore.BetByHdpClose.Where(item => item.FullTimeHdp > 0).ToList();
                    for (int i = 0; i < betByHdpCloses.Count; i++)
                    {
                        _betOk = false;

                        ProcessBet(betByHdpCloses[i]);

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
            BetByHdpClose selectedItem = DataStore.BetByHdpClose.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.BetByHdpClose.RemoveAll(item => item.Id == id);
            DataStore.BetByHdpClose.Add(selectedItem);
        }
        private void SetResultBetMessage(Guid id, string message)
        {
            BetByHdpClose selectedItem = DataStore.BetByHdpClose.FirstOrDefault(item => item.Id == id);
            selectedItem.ResultMessage = message;
            DataStore.BetByHdpClose.RemoveAll(item => item.Id == id);
            DataStore.FinishedBetByHdpClose.Add(selectedItem);
        }

        private int MinuteClose(List<OverUnderScoreTimesV2Item> rows)
        {
            for (int i = 90; i >=2; i--)
            {
                if (rows[i].Over == 0
                    && rows[i - 1].Over != 0)
                {
                    return i - 1;
                }
            }
            return 0;
        }

        private Product GetSelectedProduct(BetByHdpClose betByHdpClose, Match matchOfBet)
        {
            Product selectedProduct = null;
            if (betByHdpClose.BetOption == Enums.BetByHdpCloseOption.FIRST_HALF_MAX)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByHdpClose.MatchId
                        && item.Bettype == Enums.BetType.FirstHalfOverUnder
                    )
                    .OrderBy(item => (item.Hdp1 * 100))
                    .LastOrDefault();
            }
            else if (betByHdpClose.BetOption == Enums.BetByHdpCloseOption.FIRST_HALF_MIN)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByHdpClose.MatchId
                        && item.Bettype == Enums.BetType.FirstHalfOverUnder
                    )
                    .OrderBy(item => (item.Hdp1 * 100))
                    .FirstOrDefault();
            }
            else if (betByHdpClose.BetOption == Enums.BetByHdpCloseOption.FULL_TIME_HDP_025)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByHdpClose.MatchId
                        && item.Bettype == Enums.BetType.FullTimeOverUnder
                        && ((item.Hdp1 + item.Hdp2) * 100 - (matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore) * 100) == (betByHdpClose.FullTimeHdp - 25)
                    )
                    .FirstOrDefault();
            }
            else if (betByHdpClose.BetOption == Enums.BetByHdpCloseOption.FULL_TIME_HDP_050)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByHdpClose.MatchId
                        && item.Bettype == Enums.BetType.FullTimeOverUnder
                        && ((item.Hdp1 + item.Hdp2) * 100 - (matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore) * 100) == (betByHdpClose.FullTimeHdp - 50)
                    )
                    .FirstOrDefault();
            }
            else if (betByHdpClose.BetOption == Enums.BetByHdpCloseOption.FULL_TIME_HDP_075)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByHdpClose.MatchId
                        && item.Bettype == Enums.BetType.FullTimeOverUnder
                        && ((item.Hdp1 + item.Hdp2) * 100 - (matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore) * 100) == (betByHdpClose.FullTimeHdp - 75)
                    )
                    .FirstOrDefault();
            }
            else if (betByHdpClose.BetOption == Enums.BetByHdpCloseOption.FULL_TIME_HDP_100)
            {
                selectedProduct = DataStore.Products.Values
                    .Where(item =>
                        item.MatchId == betByHdpClose.MatchId
                        && item.Bettype == Enums.BetType.FullTimeOverUnder
                        && ((item.Hdp1 + item.Hdp2) * 100 - (matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore) * 100) == (betByHdpClose.FullTimeHdp - 100)
                    )
                    .FirstOrDefault();
            }
            return selectedProduct;
        }

        private void ProcessBet(BetByHdpClose betByHdpClose)
        {
            Match matchOfBet;
            
            if (betByHdpClose == null) return;
            if (!DataStore.Matchs.TryGetValue(betByHdpClose.MatchId, out matchOfBet)) return;

            if (betByHdpClose.TotalRed != matchOfBet.HomeRed + matchOfBet.AwayRed)
            {
                SetResultBetMessage(betByHdpClose.Id, "Có thêm thẻ đỏ");
                return;
            }

            if(!DataStore.OverUnderScoreTimesV3[matchOfBet.MatchId].ContainsKey(betByHdpClose.FullTimeHdp))
            {
                SetUnapproveMessage(betByHdpClose.Id, "Đang chờ kèo mục tiêu");
                return;
            }

            List<OverUnderScoreTimesV2Item> rows = DataStore.OverUnderScoreTimesV3[matchOfBet.MatchId][betByHdpClose.FullTimeHdp];

            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(matchOfBet.LiveTimer).TotalMinutes)
                + ((matchOfBet.LivePeriod - 1) * 45);

            int minuteClose = MinuteClose(rows);


            // current minute = 30
            // minute close = 31
            if (currentMinute <= minuteClose)
            {
                SetUnapproveMessage(betByHdpClose.Id, "Đang chờ đến lúc đóng kèo ");
                return;
            }
            else if (currentMinute < minuteClose + betByHdpClose.MinuteAfterClose)
            {
                SetUnapproveMessage(betByHdpClose.Id, "Đang chờ đến lúc đóng kèo + " +  betByHdpClose.MinuteAfterClose);
                return;
            }
            else if (currentMinute > minuteClose + betByHdpClose.MinuteAfterClose + 2)
            {
                SetResultBetMessage(betByHdpClose.Id, "Đã vượt qua lúc đóng kèo + " + betByHdpClose.MinuteAfterClose);
                return;
            }
            else if (betByHdpClose.UpToMinute > 0 
                && minuteClose > betByHdpClose.UpToMinute)
            {
                SetResultBetMessage(betByHdpClose.Id, "Kèo đã đóng, nhưng lố thời gian: " + betByHdpClose.UpToMinute);
                return;
            }


            Product selectedProduct = GetSelectedProduct(betByHdpClose, matchOfBet);
            if (selectedProduct == null)
            {
                SetResultBetMessage(betByHdpClose.Id, "Không tìm thấy kèo " + betByHdpClose.BetOption);
                return;
            }

            if (betByHdpClose.CareGoal)
            {
                if(betByHdpClose.TotalScore < matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)
                {
                    SetResultBetMessage(betByHdpClose.Id, "Có thêm banh");
                    return;
                }
            }
            else
            {
                if (betByHdpClose.GoalLimit < matchOfBet.LiveAwayScore + matchOfBet.LiveHomeScore)
                {
                    SetResultBetMessage(betByHdpClose.Id, "Vượt quá giới hạn banh");
                    return;
                }
            }




            try
            {
                ProcessBetModel model = Common.Functions.ConstructProcessBetOverUnderModelV2(
                    selectedProduct,
                    matchOfBet,
                    betByHdpClose.Stake,
                    true,
                    selectedProduct.Bettype == Enums.BetType.FirstHalfOverUnder
                );
                JToken ticket = new ProcessBetService().GetTicket(model, DataStore.Config.cookie, DataStore.Config.getTicketUrl);
                
                if (ticket != null)
                {
                    if (ticket["ErrorCode"] != null && ticket["ErrorCode"].ToString() != "0")
                    {
                        SetUnapproveMessage(betByHdpClose.Id, "Lỗi vé cược - Code " + ticket["ErrorCode"].ToString());
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
                    SetResultBetMessage(betByHdpClose.Id, message);

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

                    SetUnapproveMessage(betByHdpClose.Id, betResultJToken["ErrorMsg"].ToString() + message);
                    return;
                }

            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
                SetResultBetMessage(betByHdpClose.Id, "Lỗi: " + ex.Message);
                return;
            }


        }
    }
}
