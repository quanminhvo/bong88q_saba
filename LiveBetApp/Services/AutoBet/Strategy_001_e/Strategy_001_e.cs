using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services.AutoBet.Strategy_001_e
{
    public class Strategy_001_e
    {
        private int _minBeginHdp = 225;
        private int _maxBeginHdp = 375;

        private Dictionary<long, Strategy_001_e_Model> _matchBetTracker;

        public Strategy_001_e()
        {
            _matchBetTracker = new Dictionary<long, Strategy_001_e_Model>();
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

            Thread threadCleanUp;
            threadCleanUp = new Thread(() =>
            {
                while(true)
                {
                    try
                    {
                        CleanUp();
                    }
                    catch
                    {

                    }
                    Thread.Sleep(60000 * 45);
                }
            });
            threadCleanUp.IsBackground = true;
            threadCleanUp.Start();
            

        }

        private void ExecuteCore(int milisecond)
        {
            while (true)
            {
                try
                {
                    if (DataStore.RunAutobet)
                    {
                        InitBetTracker();
                        UpdateBetTracker();
                        CheckCancel();
                        ProcessBets();
                    }
                }
                catch (Exception ex)
                {
                    Common.Functions.WriteExceptionLog(ex);
                }
                finally
                {
                    Thread.Sleep(milisecond);
                }
            }
        }

        private void CleanUp()
        {
            List<long> matchIds = _matchBetTracker.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match = null;
                if (DataStore.Matchs.TryGetValue(matchIds[i], out match)
                    && match.LivePeriod == -1)
                {
                    _matchBetTracker.Remove(matchIds[i]);
                }
            }
        }

        private void ProcessBets()
        {
            List<long> matchIds = _matchBetTracker.Keys.ToList();
            for(int i=0; i< matchIds.Count; i++)
            {
                Strategy_001_e_Model tracker = _matchBetTracker[matchIds[i]];
                Match match = DataStore.Matchs[matchIds[i]];

                if ((!tracker.StepOne_a.Betted && !tracker.StepOne_a.Canceled)
                    || (!tracker.StepOne_b.Betted && !tracker.StepOne_b.Canceled))
                {
                    ProcessBetStepOne(match, tracker);
                }
                
                if (!tracker.StepTwo_a.Betted && !tracker.StepTwo_a.Canceled)
                {
                    ProcessBetStepTwo(match, tracker);
                }
            }
        }

        private void ProcessBetStepOne(Match match, Strategy_001_e_Model tracker)
        {
            bool placeBetBaforeFine = false;

            if (tracker.TargetPriceStepOne == 0) return;
            int tagetHdp = tracker.BeginHdp - 75;
            List<string> openPrices = DataStore.OverUnderScoreTimes[match.MatchId][tagetHdp].Skip(1).Where(item => item.Length > 0).Take(4).ToList();
            if (openPrices.Count == 0) return;

            for (int j = 0; j < (openPrices.Count > 4 ? 4 : openPrices.Count); j++)
            {
                if (int.Parse(openPrices[j]) >= tracker.TargetPriceStepOne)
                {
                    placeBetBaforeFine = true;
                    PlaceBetBeforeFineFirstHalf(match.MatchId, tracker);
                }
            }

            if (openPrices.Count > 4 && !placeBetBaforeFine)
            {
                PlaceBetAtFineFirstHalf(match.MatchId, tracker);
            }
        }

        private void ProcessBetStepTwo(Match match, Strategy_001_e_Model tracker)
        {
            int tagetHdp = tracker.BeginHdp - 100;
            bool placeBetBaforeFine = false;
            if (! DataStore.OverUnderScoreTimes.ContainsKey(match.MatchId)
                || !DataStore.OverUnderScoreTimes[match.MatchId].ContainsKey(tagetHdp))
            {
                return;
            }

            List<string> rowOfTaget = DataStore.OverUnderScoreTimes[match.MatchId][tagetHdp].Skip(1).Where(item => item.Length > 0).ToList();
            List<string> openPrices = rowOfTaget.Take(6).ToList();
            if (rowOfTaget.Count == 0) return;


            if (DataStore.OverUnderScoreTimes[match.MatchId].ContainsKey(tracker.BeginHdp)
                && DataStore.OverUnderScoreTimes[match.MatchId].ContainsKey(tracker.BeginHdp - 25)
                && DataStore.OverUnderScoreTimes[match.MatchId].ContainsKey(tracker.BeginHdp - 100))
            {
                string firstOpenStr = DataStore.OverUnderScoreTimes[match.MatchId][tracker.BeginHdp].Skip(1).FirstOrDefault(item => item.Length > 0);
                string secondOpenStr = DataStore.OverUnderScoreTimes[match.MatchId][tracker.BeginHdp - 25].Skip(1).FirstOrDefault(item => item.Length > 0);
                string openMax_100_Str = DataStore.OverUnderScoreTimes[match.MatchId][tracker.BeginHdp - 100].Skip(1).FirstOrDefault(item => item.Length > 0);
                if (firstOpenStr != null && secondOpenStr != null && openMax_100_Str != null)
                {
                    int firstOpen = int.Parse(firstOpenStr);
                    int secondOpen = int.Parse(secondOpenStr);
                    int openMax_100 = int.Parse(openMax_100_Str);
                    int med = (int)Math.Floor((double)((firstOpen + secondOpen) / 2));

                    if (firstOpen > 0 && secondOpen > 0
                        && med < 71
                        && openMax_100 < med)
                    {
                        if (!tracker.StepTwo_a.Betted && !tracker.StepTwo_a.Canceled)
                        {
                            _matchBetTracker[match.MatchId].StepTwo_a.Betted = true;
                            UpdateStakeTempBet(tracker.StepTwo_a.BetId, DataStore.AutobetStake * 2);
                            Common.Functions.UpdateTempBet(tracker.StepTwo_a.BetId, false, 1, 50, 50, "Đánh tài H1 (lần 2) - Double Bet: "
                                + " giá trung bình: " + med
                                + " ở phút " + (int)match.TimeSpanFromStart.TotalMinutes
                                + " Giá biên(nếu có): " + tracker.TargetPriceStepTwo
                            );
                        }
                    }
                }
            }

            for (int j = 0; j < (openPrices.Count > 5 ? 5 : openPrices.Count); j++)
            {
                if (int.Parse(openPrices[j]) >= tracker.TargetPriceStepTwo)
                {
                    placeBetBaforeFine = true;
                    PlaceBetBeforeSixFirstHalf(match.MatchId, tracker);
                }
            }

            if (openPrices.Count > 5 && !placeBetBaforeFine)
            {
                PlaceBetAtSixFirstHalf(match.MatchId, tracker);
            }
        }

        private void PlaceBetBeforeSixFirstHalf(long matchId, Strategy_001_e_Model tracker)
        {
            List<Product> ouProducts = DataStore.Products.Values
                .Where(item => item.MatchId == matchId && item.Bettype == Common.Enums.BetType.FirstHalfOverUnder)
                .OrderByDescending(item => item.Hdp1)
                .ToList();

            if (tracker.StepTwo_a.Betted || tracker.StepTwo_a.Canceled)
            {
                return;
            }

            string autoBetMessage = "Đánh tài H1 (lần 2): Đặt trước phút 06 của kèo " + (tracker.BeginHdp - 100);
            if (ouProducts.Count == 1)
            {
                _matchBetTracker[matchId].StepTwo_a.Betted = true;
                Common.Functions.UpdateTempBet(tracker.StepTwo_a.BetId, false, 1, (int)(ouProducts[0].Hdp1 * 100), tracker.TargetPriceStepTwo, autoBetMessage + " Biên: " + tracker.TargetPriceStepTwo);
            }
            else if (ouProducts.Count == 2)
            {
                int maxHdp = (int)(ouProducts[0].Hdp1 * 100);
                if (maxHdp == 125)
                {
                    _matchBetTracker[matchId].StepTwo_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepTwo_a.BetId, false, 1, 100, 93, autoBetMessage);
                }
                else if (maxHdp == 100)
                {
                    _matchBetTracker[matchId].StepTwo_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepTwo_a.BetId, false, 1, 75, 93, autoBetMessage);
                }
                else if (maxHdp == 75)
                {
                    _matchBetTracker[matchId].StepTwo_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepTwo_a.BetId, false, 1, 50, 93, autoBetMessage);
                }
            }
            else
            {
                _matchBetTracker[matchId].StepOne_a.Canceled = true;
                Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy " + " Giá biên(nếu có): " + tracker.TargetPriceStepOne);
            }
        }

        private void PlaceBetAtSixFirstHalf(long matchId, Strategy_001_e_Model tracker)
        {
            List<Product> ouProducts = DataStore.Products.Values
                .Where(item => item.MatchId == matchId && item.Bettype == Common.Enums.BetType.FirstHalfOverUnder)
                .OrderByDescending(item => item.Hdp1)
                .ToList();

            if (tracker.StepTwo_a.Betted || tracker.StepTwo_a.Canceled)
            {
                return;
            }

            string autoBetMessage = "Đánh tài H1 (lần 2): Đặt ở phút 06 của kèo " + (tracker.BeginHdp - 100);
            if (ouProducts.Count == 1)
            {
                _matchBetTracker[matchId].StepTwo_a.Betted = true;
                Common.Functions.UpdateTempBet(tracker.StepTwo_a.BetId, false, 1, (int)(ouProducts[0].Hdp1 * 100), tracker.TargetPriceStepTwo, autoBetMessage + " Biên: " + tracker.TargetPriceStepTwo);
            }
            else if (ouProducts.Count == 2)
            {
                int maxHdp = (int)(ouProducts[0].Hdp1 * 100);
                if (maxHdp == 125)
                {
                    _matchBetTracker[matchId].StepTwo_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepTwo_a.BetId, false, 1, 100, 50, autoBetMessage);
                }
                else if (maxHdp == 100)
                {
                    _matchBetTracker[matchId].StepTwo_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepTwo_a.BetId, false, 1, 75, 50, autoBetMessage);
                }
                else if (maxHdp == 75)
                {
                    _matchBetTracker[matchId].StepTwo_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepTwo_a.BetId, false, 1, 50, 50, autoBetMessage);
                }
            }
            else
            {
                _matchBetTracker[matchId].StepOne_a.Canceled = true;
                Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy " + " Giá biên(nếu có): " + tracker.TargetPriceStepOne);
            }
        }

        private void PlaceBetBeforeFineFirstHalf(long matchId, Strategy_001_e_Model tracker)
        {
            if (!tracker.StepOne_b.Betted && !tracker.StepOne_b.Canceled)
            {
                _matchBetTracker[matchId].StepOne_b.Betted = true;
                Common.Functions.UpdateTempBet(tracker.StepOne_b.BetId, true, 1, tracker.BeginHdp - 75, 100, "Đánh tài cả trận (lần 1):"
                    + " Đặt trước phút 05 của kèo " + (tracker.BeginHdp - 75)
                );
            }

            Match match = DataStore.Matchs[matchId];

            List<Product> ouProducts = DataStore.Products.Values
                .Where(item => item.MatchId == matchId && item.Bettype == Common.Enums.BetType.FirstHalfOverUnder)
                .OrderByDescending(item => item.Hdp1)
                .ToList();

            if (tracker.StepOne_a.Betted || tracker.StepOne_a.Canceled)
            {
                return;
            }

            string autoBetMessage = "Đánh tài H1 (lần 1): Đặt trước phút 05 của kèo " + (tracker.BeginHdp - 75);
            if (ouProducts.Count == 1)
            {
                _matchBetTracker[matchId].StepOne_a.Betted = true;
                Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, (int)(ouProducts[0].Hdp1 * 100), 50, autoBetMessage);
            }
            else if (ouProducts.Count == 2)
            {
                if (DataStore.OverUnderScoreTimesFirstHalf.ContainsKey(matchId)
                    && (DataStore.OverUnderScoreTimesFirstHalf[matchId].ContainsKey(75)
                    || DataStore.OverUnderScoreTimesFirstHalf[matchId].ContainsKey(50)))
                {
                    string over_075_str = DataStore.OverUnderScoreTimesFirstHalf[matchId][75].Skip(1).Last(item => item.Length > 0);
                    if (over_075_str != null)
                    {
                        int over_075 = int.Parse(over_075_str);
                        if (!tracker.StepOne_a.Betted && !tracker.StepOne_a.Canceled)
                        {
                            if (over_075 > 0)
                            {
                                _matchBetTracker[matchId].StepOne_a.Betted = true;
                                Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 75, 92, autoBetMessage);
                            }
                            else if (over_075 < 0 && over_075 <= -92)
                            {
                                _matchBetTracker[matchId].StepOne_a.Betted = true;
                                Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 75, -92, autoBetMessage);
                            }
                            else if (over_075 < 0 && over_075 > -92)
                            {
                                _matchBetTracker[matchId].StepOne_a.Betted = true;
                                Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 50, 92, autoBetMessage);
                            }
                        }

                        string over_050_str = DataStore.OverUnderScoreTimesFirstHalf[matchId][50].Skip(1).Last(item => item.Length > 0);
                        if (over_050_str != null)
                        {
                            int over_050 = int.Parse(over_050_str);
                            if (!tracker.StepOne_a.Betted && !tracker.StepOne_a.Canceled)
                            {
                                if (over_050 < 0)
                                {
                                    _matchBetTracker[matchId].StepOne_a.Betted = true;
                                    Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 50, 50, autoBetMessage);
                                }
                            }
                        }

                    }
                    else
                    {
                        if (!tracker.StepOne_a.Betted && !tracker.StepOne_a.Canceled)
                        {
                            _matchBetTracker[matchId].StepOne_a.Canceled = true;
                            Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy vì không tìm thấy kèo 075 ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + tracker.TargetPriceStepOne);
                        }
                    }
                }
            }
        }

        private bool CheckCancelBeforePlaceBetAtFirstHalf(long matchId, Strategy_001_e_Model tracker)
        {
            Match match = DataStore.Matchs[matchId];
            if (DataStore.OverUnderScoreTimesFirstHalf.ContainsKey(matchId)
                && DataStore.OverUnderScoreTimesFirstHalf[matchId].ContainsKey(75)
                && DataStore.OverUnderScoreTimesFirstHalf[matchId].ContainsKey(50))
            {
                string price_50_str = DataStore.OverUnderScoreTimesFirstHalf[matchId][50].Skip(1).Last(item => item.Length > 0);
                int price_50 = int.Parse(price_50_str);
                if (price_50 < 0 
                    && price_50 > -92
                    && tracker.BeginHdp == 225)
                {
                    if (!tracker.StepOne_a.Betted && !tracker.StepOne_a.Canceled)
                    {
                        _matchBetTracker[matchId].StepOne_a.Canceled = true;
                        Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy vì giá kèo 050 H1 tốt hơn -92 ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + tracker.TargetPriceStepOne);
                    }

                    if (!tracker.StepOne_b.Betted && !tracker.StepOne_b.Canceled)
                    {
                        _matchBetTracker[matchId].StepOne_b.Canceled = true;
                        Common.Functions.UpdateTempBet(tracker.StepOne_b.BetId, true, 1, 0, -1, "Đánh tài cả trận (lần 1): Hủy vì giá kèo 050 H1 tốt hơn -92 ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + tracker.TargetPriceStepOne);
                    }

                    if (!tracker.StepTwo_a.Betted && !tracker.StepTwo_a.Canceled)
                    {
                        _matchBetTracker[matchId].StepTwo_a.Canceled = true;
                        Common.Functions.UpdateTempBet(tracker.StepTwo_a.BetId, false, 1, 0, -1, "Đánh tài H1 (lần 2): Hủy vì giá kèo 050 H1 tốt hơn -92 ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + tracker.TargetPriceStepTwo);
                    }

                }
            }
            return false;
        }

        private void PlaceBetAtFineFirstHalf(long matchId, Strategy_001_e_Model tracker)
        {
            CheckCancelBeforePlaceBetAtFirstHalf(matchId, tracker);

            if (!tracker.StepOne_b.Betted && !tracker.StepOne_b.Canceled)
            {
                _matchBetTracker[matchId].StepOne_b.Betted = true;
                Common.Functions.UpdateTempBet(tracker.StepOne_b.BetId, true, 1, tracker.BeginHdp - 75, 100, "Đánh tài cả trận (lần 1):"
                    + " Đặt ở phút 05 của kèo " + (tracker.BeginHdp - 75)
                );
            }

            List<Product> ouProducts = DataStore.Products.Values
                .Where(item => item.MatchId == matchId && item.Bettype == Common.Enums.BetType.FirstHalfOverUnder)
                .OrderByDescending(item => item.Hdp1)
                .ToList();

            if (tracker.StepOne_a.Betted || tracker.StepOne_a.Canceled)
            {
                return;
            }

            string autoBetMessage = "Đánh tài H1 (lần 1): Đặt ở phút 05 của kèo " + (tracker.BeginHdp - 75);
            if (ouProducts.Count == 1)
            {
                _matchBetTracker[matchId].StepOne_a.Betted = true;
                Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, (int)(ouProducts[0].Hdp1 * 100), 50, autoBetMessage);
            }
            else if (ouProducts.Count == 2)
            {
                int maxHdp = (int)(ouProducts[0].Hdp1 * 100);
                if (maxHdp == 150)
                {
                    _matchBetTracker[matchId].StepOne_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 125, 50, autoBetMessage);
                }
                else if (maxHdp == 125)
                {
                    _matchBetTracker[matchId].StepOne_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 100, 50, autoBetMessage);
                }
                else if (maxHdp == 100)
                {
                    _matchBetTracker[matchId].StepOne_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 100, 50, autoBetMessage);
                }
                else if (maxHdp == 75)
                {
                    _matchBetTracker[matchId].StepOne_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 75, 50, autoBetMessage);
                }
                else if (maxHdp == 50)
                {
                    _matchBetTracker[matchId].StepOne_a.Betted = true;
                    Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 50, 50, autoBetMessage);
                }
                else
                {
                    _matchBetTracker[matchId].StepOne_a.Canceled = true;
                    Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy " + " Giá biên(nếu có): " + tracker.TargetPriceStepOne);
                }
            }

        }

        private void InitBetTracker()
        {
            List<Match> matchs = DataStore.Matchs.Values
                .Where(item => item.LivePeriod == 1
                    && item.OverUnderMoneyLine == 18
                    && item.TimeSpanFromStart.TotalMinutes >= 5
                    && !item.Home.Contains("(R)")
                    && !item.Away.Contains("(R)")
                ).ToList();

            for (int i = 0; i < matchs.Count; i++)
            {
                long matchId = matchs[i].MatchId;
                Match match = DataStore.Matchs[matchId];

                if (!DataStore.OverUnderScoreTimes.ContainsKey(matchId) || DataStore.OverUnderScoreTimes[matchId].Count == 0) continue;
                int beginHdp = DataStore.OverUnderScoreTimes[matchId].Keys.Max();

                if (_minBeginHdp <= beginHdp 
                    && beginHdp <= _maxBeginHdp 
                    && !_matchBetTracker.ContainsKey(matchId))
                {
                    _matchBetTracker.Add(matchId, new Strategy_001_e_Model() { BeginHdp = beginHdp });
                    PlaceTempBetStepOne(match);
                    PlaceTempBetStepTwo(match);
                }
            }
        }

        private void UpdateBetTracker()
        {
            List<long> matchIds = _matchBetTracker.Keys.ToList();
            for (int i=0; i< matchIds.Count; i++)
            {
                Match match = DataStore.Matchs[matchIds[i]];
                Strategy_001_e_Model tracker = _matchBetTracker[matchIds[i]];
                if (DataStore.OverUnderScoreTimes.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(tracker.BeginHdp - 75)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(tracker.BeginHdp - 50)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(tracker.BeginHdp - 25)
                    && tracker.TargetPriceStepOne == 0
                    && match.LivePeriod == 1
                    && tracker.StepOne_a.Canceled == false
                    && tracker.StepOne_b.Canceled == false)
                {
                    int targetPrice = CalculateTargetPrice(matchIds[i]);

                    int openTargetHdpPrice = FindOpenPrice(matchIds[i], tracker.BeginHdp - 75);
                    if(targetPrice != 0 
                        && openTargetHdpPrice != 0)
                    {
                        targetPrice = targetPrice < (openTargetHdpPrice + 18) ? targetPrice : (openTargetHdpPrice + 18);
                    }


                    Guid StepOneId_a = _matchBetTracker[matchIds[i]].StepOne_a.BetId;
                    Guid StepOneId_b = _matchBetTracker[matchIds[i]].StepOne_b.BetId;

                    _matchBetTracker[matchIds[i]].TargetPriceStepOne = targetPrice;
                    if (targetPrice == 0)
                    {
                        _matchBetTracker[matchIds[i]].StepOne_a.Canceled = true;
                        Common.Functions.UpdateTempBet(StepOneId_a, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy lệnh do việc tìm biên có vấn đề ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + tracker.TargetPriceStepOne);

                        _matchBetTracker[matchIds[i]].StepOne_b.Canceled = true;
                        Common.Functions.UpdateTempBet(StepOneId_b, true, 1, 0, -1, "Đánh tài cả trận (lần 1): Hủy lệnh do việc tìm biên có vấn đề ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + tracker.TargetPriceStepOne);
                    }
                    else
                    {
                        Common.Functions.UpdateTempBet(StepOneId_a, false, 1, 0, -1, "Đánh tài H1 (lần 1):"
                            + " Giá mục tiêu: " + targetPrice
                        );
                        Common.Functions.UpdateTempBet(StepOneId_b, true, 1, 0, -1, "Đánh tài cả trận (lần 1):"
                            + " Giá mục tiêu: " + targetPrice
                        );
                    }
                }

                if (DataStore.OverUnderScoreTimes.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(tracker.BeginHdp - 25)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(tracker.BeginHdp - 50)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(tracker.BeginHdp - 75)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(tracker.BeginHdp - 100)
                    && tracker.TargetPriceStepTwo == 0
                    && match.LivePeriod == 1
                    && tracker.StepTwo_a.Canceled == false)
                {
                    int targetPrice = CalculateTargetPrice(matchIds[i]);

                    int openTargetHdpPrice = FindOpenPrice(matchIds[i], tracker.BeginHdp - 100);
                    if (targetPrice != 0
                        && openTargetHdpPrice != 0)
                    {
                        targetPrice = targetPrice < (openTargetHdpPrice + 18) ? targetPrice : (openTargetHdpPrice + 18);
                    }

                    Guid StepTwoId_a = _matchBetTracker[matchIds[i]].StepTwo_a.BetId;

                    _matchBetTracker[matchIds[i]].TargetPriceStepTwo = targetPrice;

                    if (targetPrice == 0)
                    {
                        _matchBetTracker[matchIds[i]].StepTwo_a.Canceled = true;
                        Common.Functions.UpdateTempBet(StepTwoId_a, false, 1, 0, -1, "Đánh tài H1 (lần 2): Hủy lệnh do việc tìm biên có vấn đề ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + tracker.TargetPriceStepTwo);
                    }
                    else
                    {
                        Common.Functions.UpdateTempBet(StepTwoId_a, false, 1, 0, -1, "Đánh tài H1 (lần 2):"
                            + " Giá mục tiêu: " + targetPrice
                        );
                    }

                }

            }
        }

        private int CountOuProduct(long matchId)
        {
            List<Product> productsOfMatch = DataStore.Products.Values.Where(item => item.MatchId == matchId && item.Bettype == Enums.BetType.FullTimeOverUnder).ToList();
            return productsOfMatch.Count();
        }

        private void CheckCancel()
        {
            List<long> matchIds = _matchBetTracker.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {

                Strategy_001_e_Model itemTracker = _matchBetTracker[matchIds[i]];
                Match match = DataStore.Matchs[matchIds[i]];
                Guid StepOneId_a = itemTracker.StepOne_a.BetId;
                Guid StepOneId_b = itemTracker.StepOne_b.BetId;
                Guid StepTwoId_a = itemTracker.StepTwo_a.BetId;
                int tagetPrice = itemTracker.TargetPriceStepOne;
                int targetHdp = itemTracker.BeginHdp - 75;

                int openTargetHdpPrice = FindOpenPrice(matchIds[i], targetHdp);

                if (openTargetHdpPrice != 0) 
                {
                    if (openTargetHdpPrice >= tagetPrice 
                        && tagetPrice != 0)
                    {
                        if (!itemTracker.StepOne_a.Canceled)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_a, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy lệnh vì giá mở cửa " + targetHdp + " lớn hơn biên ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepOne_b.Canceled)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_b.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_b, true, 1, 0, -1, "Đánh tài cả trận (lần 1): Hủy lệnh vì giá mở cửa " + targetHdp + " lớn hơn biên ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepTwo_a.Canceled && !itemTracker.StepTwo_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepTwo_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepTwoId_a, false, 1, 0, -1, "Đánh tài H1 (lần 2): Hủy lệnh vì giá mở cửa " + targetHdp + " lớn hơn biên ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepTwo);
                        }

                    }
                }

                
                if (DataStore.OverUnderScoreTimesFirstHalf.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimesFirstHalf[matchIds[i]].Keys.Count > 0)
                {
                    int openMaxFirstHalf = DataStore.OverUnderScoreTimesFirstHalf[matchIds[i]].Keys.Max();
                    if (openMaxFirstHalf > 150)
                    {
                        if (!itemTracker.StepOne_a.Canceled && !itemTracker.StepOne_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_a, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy lệnh vì max kèo H1 lớn hơn 150 ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepOne_b.Canceled && !itemTracker.StepOne_b.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_b.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_b, true, 1, 0, -1, "Đánh tài cả trận (lần 1): Hủy lệnh vì max kèo H1 lớn hơn 150 ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }
                    }

                    if (openMaxFirstHalf > 125)
                    {
                        if (!itemTracker.StepTwo_a.Canceled && !itemTracker.StepTwo_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepTwo_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepTwoId_a, false, 1, 0, -1, "Đánh tài H1 (lần 2): Hủy lệnh vì max kèo H1 lớn hơn 125 ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepTwo);
                        }
                    }
                }

                if (itemTracker.BeginHdp % 50 == 0
                    && DataStore.OverUnderScoreTimes.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(itemTracker.BeginHdp - 100)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(itemTracker.BeginHdp - 50))
                {
                    int remain = DataStore.OverUnderScoreTimes[matchIds[i]][itemTracker.BeginHdp - 50].Skip(1).Count(item => item.Length > 0);
                    if(remain <=10)
                    {
                        if (!itemTracker.StepOne_a.Canceled && !itemTracker.StepOne_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_a, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy lệnh vì kèo " + (itemTracker.BeginHdp - 50) + " tồn tại ít hơn 11 phút ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepOne_b.Canceled && !itemTracker.StepOne_b.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_b.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_b, true, 1, 0, -1, "Đánh tài cả trận (lần 1): Hủy lệnh vì kèo " + (itemTracker.BeginHdp - 50) + " tồn tại ít hơn 11 phút ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepTwo_a.Canceled && !itemTracker.StepTwo_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepTwo_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepTwoId_a, false, 1, 0, -1, "Đánh tài H1 (lần 2): Hủy lệnh vì kèo " + (itemTracker.BeginHdp - 50) + " tồn tại ít hơn 11 phút ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepTwo);
                        }
                    }
                }

                if (DataStore.OverUnderScoreTimes.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(itemTracker.BeginHdp - 75)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(itemTracker.BeginHdp - 25))
                {
                    int countBeginHdp_25 = DataStore.OverUnderScoreTimes[matchIds[i]][itemTracker.BeginHdp - 25].Skip(1).Count(item => item.Length > 0);
                    if (countBeginHdp_25 <= 8 && countBeginHdp_25 > 0)
                    {
                        if (!itemTracker.StepOne_a.Canceled && !itemTracker.StepOne_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_a, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy lệnh vì kèo " + (itemTracker.BeginHdp - 25) + " tồn tại ít hơn 9 phút ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepOne_b.Canceled && !itemTracker.StepOne_b.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_b.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_b, true, 1, 0, -1, "Đánh tài cả trận (lần 1): Hủy lệnh vì kèo " + (itemTracker.BeginHdp - 25) + " tồn tại ít hơn 9 phút ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepTwo_a.Canceled && !itemTracker.StepTwo_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepTwo_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepTwoId_a, false, 1, 0, -1, "Đánh tài H1 (lần 2): Hủy lệnh vì kèo " + (itemTracker.BeginHdp - 25) + " tồn tại ít hơn 9 phút ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepTwo);
                        }
                    }
                }

                if (DataStore.OverUnderScoreTimes.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(itemTracker.BeginHdp)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(itemTracker.BeginHdp - 25)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(itemTracker.BeginHdp - 75))
                {

                    if (DiffMaxAndBelow(match, itemTracker) <= 3)
                    {
                        if (!itemTracker.StepOne_a.Canceled && !itemTracker.StepOne_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_a, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy lệnh vì [remain max] - [remain max - 25] <= 3 (không kiểm tra liên tục) ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepOne_b.Canceled && !itemTracker.StepOne_b.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_b.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_b, true, 1, 0, -1, "Đánh tài cả trận (lần 1): Hủy lệnh vì [remain max] - [remain max - 25] <= 3 (không kiểm tra liên tục) ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepTwo_a.Canceled && !itemTracker.StepTwo_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepTwo_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepTwoId_a, false, 1, 0, -1, "Đánh tài H1 (lần 2): Hủy lệnh vì [remain max] - [remain max - 25] <= 3 (không kiểm tra liên tục) ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepTwo);
                        }
                    }
                }

                if (DataStore.OverUnderScoreTimes.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(itemTracker.BeginHdp - 75)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(itemTracker.BeginHdp - 25))
                {
                    int openPrice = 0;
                    int closePrice = 0;
                    List<string> row = DataStore.OverUnderScoreTimes[matchIds[i]][itemTracker.BeginHdp - 25].Skip(1).Where(item => item.Length > 0).ToList();
                    if (row.Count > 0)
                    {
                        openPrice = int.Parse(row.FirstOrDefault());
                        closePrice = int.Parse(row.LastOrDefault());

                        int stepOpen = openPrice < 0 ? (100 + openPrice) : (100 - openPrice);
                        int stepClose = closePrice < 0 ? (100 + closePrice) : (100 - closePrice);

                        if (stepOpen + stepClose > 36)
                        {
                            int diffMaxAndBelow = DiffMaxAndBelow(match, itemTracker);
                            if (itemTracker.BeginHdp == 225 || (diffMaxAndBelow >= 11 && itemTracker.BeginHdp > 225))
                            {
                                if (!itemTracker.StepOne_a.Canceled && !itemTracker.StepOne_a.Betted)
                                {
                                    _matchBetTracker[matchIds[i]].StepOne_a.Canceled = true;
                                    Common.Functions.UpdateTempBet(StepOneId_a, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy lệnh vì [remain max - 25] chạy hơn 36 giá,"
                                        + " kèo max: " + itemTracker.BeginHdp
                                        + " [remain max] - [remain max - 25]: " + diffMaxAndBelow
                                        + " ở phút " + (int)match.TimeSpanFromStart.TotalMinutes 
                                        + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                                }

                                if (!itemTracker.StepOne_b.Canceled && !itemTracker.StepOne_b.Betted)
                                {
                                    _matchBetTracker[matchIds[i]].StepOne_b.Canceled = true;
                                    Common.Functions.UpdateTempBet(StepOneId_b, true, 1, 0, -1, "Đánh tài cả trận (lần 1): Hủy lệnh vì [remain max - 25] chạy hơn 36 giá,"
                                        + " kèo max: " + itemTracker.BeginHdp
                                        + " [remain max] - [remain max - 25]:: " + diffMaxAndBelow
                                        + " ở phút " + (int)match.TimeSpanFromStart.TotalMinutes
                                        + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                                }

                                if (!itemTracker.StepTwo_a.Canceled && !itemTracker.StepTwo_a.Betted)
                                {
                                    _matchBetTracker[matchIds[i]].StepTwo_a.Canceled = true;
                                    Common.Functions.UpdateTempBet(StepTwoId_a, false, 1, 0, -1, "Đánh tài H1 (lần 2): Hủy lệnh vì [remain max - 25] chạy hơn 36 giá,"
                                        + " kèo max: " + itemTracker.BeginHdp
                                        + " [remain max] - [remain max - 25]: " + diffMaxAndBelow
                                        + " ở phút " + (int)match.TimeSpanFromStart.TotalMinutes 
                                        + " Giá biên(nếu có): " + itemTracker.TargetPriceStepTwo);
                                }

                            }
                        }
                        

                    }


                }

                if (CountOuProduct(matchIds[i]) < 2 
                    && match.TimeSpanFromStart.TotalMinutes >= 4
                    && match.TimeSpanFromStart.TotalMinutes <= 40)
                {
                    if (!itemTracker.StepOne_a.Canceled && !itemTracker.StepOne_a.Betted)
                    {
                        _matchBetTracker[matchIds[i]].StepOne_a.Canceled = true;
                        Common.Functions.UpdateTempBet(StepOneId_a, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy lệnh vì số lượng kèo O/U có vấn đề ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                    }

                    if (!itemTracker.StepOne_b.Canceled && !itemTracker.StepOne_b.Betted)
                    {
                        _matchBetTracker[matchIds[i]].StepOne_b.Canceled = true;
                        Common.Functions.UpdateTempBet(StepOneId_b, true, 1, 0, -1, "Đánh tài cả trận (lần 1): Hủy lệnh vì số lượng kèo O/U có vấn đề ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                    }

                    if (!itemTracker.StepTwo_a.Canceled && !itemTracker.StepTwo_a.Betted)
                    {
                        _matchBetTracker[matchIds[i]].StepTwo_a.Canceled = true;
                        Common.Functions.UpdateTempBet(StepTwoId_a, false, 1, 0, -1, "Đánh tài H1 (lần 2): Hủy lệnh vì số lượng kèo O/U có vấn đề ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepTwo);
                    }
                }

                if (itemTracker.BeginHdp == 250
                    && DataStore.OverUnderScoreTimes.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(225))
                {
                    //Open max = 250 và remain 225 >= 18 phút
                    int countLowwerRow = CountRemainSpecial(match.MatchId, 225);

                    if (countLowwerRow >= 18)
                    {
                        if (!itemTracker.StepOne_a.Canceled && !itemTracker.StepOne_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_a, false, 1, 0, -1, "Đánh tài H1 (lần 1): Hủy lệnh vì Open max = 250 và remain 225 >= 18 phút. ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepOne_b.Canceled && !itemTracker.StepOne_b.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepOne_b.Canceled = true;
                            Common.Functions.UpdateTempBet(StepOneId_b, true, 1, 0, -1, "Đánh tài cả trận (lần 1): Hủy lệnh vì Open max = 250 và remain 225 >= 18 phút. ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepOne);
                        }

                        if (!itemTracker.StepTwo_a.Canceled && !itemTracker.StepTwo_a.Betted)
                        {
                            _matchBetTracker[matchIds[i]].StepTwo_a.Canceled = true;
                            Common.Functions.UpdateTempBet(StepTwoId_a, false, 1, 0, -1, "Đánh tài H1 (lần 2): Hủy lệnh vì Open max = 250 và remain 225 >= 18 phút. ở phút " + (int)match.TimeSpanFromStart.TotalMinutes + " Giá biên(nếu có): " + itemTracker.TargetPriceStepTwo);
                        }
                    }
                    
                }

            }
        }

        private int CountRemainSpecial(long matchId, int hdp)
        {
            if (DataStore.OverUnderScoreTimes.ContainsKey(matchId)
                && DataStore.OverUnderScoreTimes[matchId].ContainsKey(hdp))
            {
                List<string> row = DataStore.OverUnderScoreTimes[matchId][hdp].ToList();

                for (int i = 1; i < row.Count - 1; i++)
                {
                    if (row[i].Length > 0 && row[i + 1].Length == 0)
                    {
                        return i;
                    }
                }
            }

            return 0;
        }

        private int DiffMaxAndBelow(Match match, Strategy_001_e_Model tracker)
        {
            if (DataStore.OverUnderScoreTimes.ContainsKey(match.MatchId)
                && DataStore.OverUnderScoreTimes[match.MatchId].ContainsKey(tracker.BeginHdp)
                && DataStore.OverUnderScoreTimes[match.MatchId].ContainsKey(tracker.BeginHdp - 25))
            {
                int lastIndexOfRowMax = CountRemainSpecial(match.MatchId, tracker.BeginHdp);
                int lastIndexOfRowBelowMax = CountRemainSpecial(match.MatchId, tracker.BeginHdp - 25);
                return lastIndexOfRowBelowMax - lastIndexOfRowMax;
            }

            return 0;
            
        }

        private int FindOpenPrice(long matchId, int hdp)
        {
            if (!DataStore.OverUnderScoreTimes.ContainsKey(matchId)
                || !DataStore.OverUnderScoreTimes[matchId].ContainsKey(hdp))
            {
                return 0;
            }

            string openTargetHdpPriceStr = DataStore.OverUnderScoreTimes[matchId][hdp].Skip(1).FirstOrDefault(item => item.Length > 0);
            if (openTargetHdpPriceStr != null)
            {
                return int.Parse(openTargetHdpPriceStr);
            }
            return 0;
        }

        private int CalculateTargetPrice(long matchId)
        {
            string r1c1, r1c2, r1c3, r1c4;
            string r2c1, r2c2, r2c3, r2c4;
            string r3c1;
            int firstPrice = 0;
            int secondPrice = 0;
            string thirdPriceStr;
            bool findHiddenPrice = false;

            int beginHdp = _matchBetTracker[matchId].BeginHdp;
            
            r1c1 = DataStore.OverUnderScoreTimes[matchId][beginHdp][1];
            r1c2 = DataStore.OverUnderScoreTimes[matchId][beginHdp][2];
            r1c3 = DataStore.OverUnderScoreTimes[matchId][beginHdp][3];
            r1c4 = DataStore.OverUnderScoreTimes[matchId][beginHdp][4];

            r2c1 = DataStore.OverUnderScoreTimes[matchId][beginHdp - 25][1];
            r2c2 = DataStore.OverUnderScoreTimes[matchId][beginHdp - 25][2];
            r2c3 = DataStore.OverUnderScoreTimes[matchId][beginHdp - 25][3];
            r2c4 = DataStore.OverUnderScoreTimes[matchId][beginHdp - 25][4];

            r3c1 = DataStore.OverUnderScoreTimes[matchId][beginHdp - 50][1];

            thirdPriceStr = DataStore.OverUnderScoreTimes[matchId][beginHdp - 50].Skip(1).FirstOrDefault(item => item.Length > 0);
            
            if (r1c1.Length > 0 && r1c2.Length > 0 && r1c3.Length > 0 
                  && r2c1.Length > 0 && r2c2.Length > 0 && r2c3.Length > 0)
            {
                firstPrice = int.Parse(r1c1);
                secondPrice = int.Parse(r2c1);
            }
            else if (r1c1.Length == 0 && r1c2.Length > 0 && r1c3.Length > 0
                  && r2c1.Length == 0 && r2c2.Length > 0 && r2c3.Length > 0)
            {
                firstPrice = int.Parse(r1c2);
                secondPrice = int.Parse(r2c2);
            }
            else if (r1c1.Length == 0 && r1c2.Length == 0 && r1c3.Length > 0
                  && r2c1.Length == 0 && r2c2.Length == 0 && r2c3.Length > 0)
            {
                firstPrice = int.Parse(r1c3);
                secondPrice = int.Parse(r2c3);
            }
            else if (r1c1.Length == 0 && r1c2.Length == 0 && r1c3.Length > 0
                  && r2c1.Length == 0 && r2c2.Length == 0 && r2c3.Length > 0)
            {
                firstPrice = int.Parse(r1c3);
                secondPrice = int.Parse(r2c3);
            }
            else if (r1c1.Length > 0 && (r1c2.Length + r1c3.Length > 0)
                  && r2c1.Length > 0 && (r2c2.Length + r2c3.Length > 0))
            {
                firstPrice = int.Parse(r1c1);
                secondPrice = int.Parse(r2c1);
            }
            else if (r1c1.Length >  0 && r1c2.Length > 0 && r1c3.Length > 0
                  && r2c1.Length == 0 && r2c2.Length > 0 && r2c3.Length > 0)
            {
                firstPrice = int.Parse(r1c1);
                findHiddenPrice = true;
            }
            else if (r1c1.Length == 0 && r1c2.Length >  0 && r1c3.Length > 0
                  && r2c1.Length == 0 && r2c2.Length == 0 && r2c3.Length > 0)
            {
                firstPrice = int.Parse(r1c2);
                findHiddenPrice = true;
            }
            else if (r1c1.Length == 0 && r1c2.Length == 0 && r1c3.Length >  0 && r1c4.Length > 0
                  && r2c1.Length == 0 && r2c2.Length == 0 && r2c3.Length == 0 && r2c4.Length > 0)
            {
                firstPrice = int.Parse(r1c3);
                findHiddenPrice = true;
            }
            else if (r1c1.Length == 0 && r1c2.Length > 0 && r1c3.Length > 0
                  && r2c1.Length >  0 && r2c2.Length > 0 && r2c3.Length > 0
                  && r3c1.Length >  0)
            {
                return int.Parse(r2c1);
            }
            else
            {
                return 0;
            }

            if (findHiddenPrice)
            {
                if (firstPrice > 0)
                {
                    secondPrice = 73;
                }
                else
                {
                    return FindHiddenTargetPrice(matchId, beginHdp, firstPrice);
                }
            }

            if (firstPrice > 0 && secondPrice > 0)
            {
                return secondPrice >= 71 ? secondPrice : (firstPrice + secondPrice) / 2;
            }
            else if (firstPrice < 0 && secondPrice < 0)
            {
                return int.Parse(thirdPriceStr);
            }
            else if(firstPrice < 0 && secondPrice > 0)
            {
                return secondPrice;
            }

            return 0;
           
        }

        private int FindHiddenTargetPrice(long matchId, int beginHdp, int firstPrice)
        {
            int acceptableDiff = 2;

            List<string> row = DataStore.OverUnderScoreTimes[matchId][beginHdp - 25].Skip(1).ToList();

            for (int i=0; i < row.Count; i++)
            {
                if (row[i].Length > 0)
                {
                    int value = int.Parse(row[i]);
                    if ((firstPrice * value > 0 && (value + acceptableDiff) >= firstPrice)
                        || (firstPrice > 0 && value < 0))
                    {
                        string hiddenPriceStr = DataStore.OverUnderScoreTimes[matchId][beginHdp - 50][i];
                        if (hiddenPriceStr.Length > 0)
                        {
                            return int.Parse(hiddenPriceStr);
                        }
                    }
                }
            }

            return 0;
        }

        private void PlaceTempBetStepOne(Match match)
        {
            long matchId = match.MatchId;

            _matchBetTracker[matchId].StepOne_a.BetId = Guid.NewGuid();
            _matchBetTracker[matchId].StepOne_b.BetId = Guid.NewGuid();

            int stake = DataStore.AutobetStake;

            DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
            {
                Id = _matchBetTracker[matchId].StepOne_a.BetId,
                MatchId = matchId,
                LivePeriod = 1,
                Stake = stake,
                IsOver = true,
                IsFulltime = false,
                TotalScore = 0,
                TotalRed = 0,
                CreateDateTime = DateTime.Now,
                MoneyLine = match.OverUnderMoneyLine,

                AutoBetMessage = "Đánh tài H1 (lần 1): lên lệnh trước",
                Hdp = 0,
                Price = -1
            });

            DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
            {
                Id = _matchBetTracker[matchId].StepOne_b.BetId,
                MatchId = matchId,
                LivePeriod = 1,
                Stake = stake,
                IsOver = true,
                IsFulltime = true,
                TotalScore = 0,
                TotalRed = 0,
                CreateDateTime = DateTime.Now,
                MoneyLine = match.OverUnderMoneyLine,

                AutoBetMessage = "Đánh tài cả trận (Lần 1): lên lệnh trước",
                Hdp = 0,
                Price = -1
            });

        }

        private void PlaceTempBetStepTwo(Match match)
        {
            long matchId = match.MatchId;

            _matchBetTracker[matchId].StepTwo_a.BetId = Guid.NewGuid();

            int stake = DataStore.AutobetStake;

            DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
            {
                Id = _matchBetTracker[matchId].StepTwo_a.BetId,
                MatchId = matchId,
                LivePeriod = 1,
                Stake = stake,
                IsOver = true,
                IsFulltime = false,
                TotalScore = 0,
                TotalRed = 0,
                CreateDateTime = DateTime.Now,
                MoneyLine = match.OverUnderMoneyLine,

                AutoBetMessage = "Đánh tài H1 (lần 2): lên lệnh trước",
                Hdp = 0,
                Price = -1
            });


        }

        private void UpdateStakeTempBet(Guid betId, int stake)
        {
            BetByHdpPrice bet = DataStore.BetByHdpPrice.FirstOrDefault(item => item.Id == betId);
            if (bet != null)
            {
                bet.Stake = stake;
            }
        }
    }
}
