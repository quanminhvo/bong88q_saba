using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services.AutoBet.Strategy_002
{
    public class Strategy_002
    {
        private Dictionary<long, Strategy_002_Model> _matchBetTracker;

        public Strategy_002()
        {
            _matchBetTracker = new Dictionary<long, Strategy_002_Model>();
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
                while (true)
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
                        Init();
                        Cancel();
                        Process();
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
                if (DataStore.Matchs[matchIds[i]].LivePeriod == -1)
                {
                    _matchBetTracker.Remove(matchIds[i]);
                }
            }
        }

        private void Init()
        {
            List<Match> matchs = DataStore.Matchs.Values
                .Where(item =>
                    item.OverUnderMoneyLine >= 8
                    && item.OverUnderMoneyLine <= 18
                    && item.LivePeriod == 2
                    && (item.LiveAwayScore + item.LiveHomeScore) == 1
                    && DataStore.GoalHistories.ContainsKey(item.MatchId)
                    && DataStore.GoalHistories[item.MatchId].Count == 1
                    && DataStore.GoalHistories[item.MatchId][0].TimeSpanFromStart.TotalMinutes >= 33
                    && DataStore.GoalHistories[item.MatchId][0].TimeSpanFromStart.TotalMinutes <= 42
                    && DataStore.GoalHistories[item.MatchId][0].LivePeriod == 1
                    && !_matchBetTracker.ContainsKey(item.MatchId)
                )
                .ToList();

            for (int i = 0; i < matchs.Count; i++)
            {
                long matchId = matchs[i].MatchId;
                _matchBetTracker.Add(matchs[i].MatchId, new Strategy_002_Model());
                PlaceTempBetStepOne(matchs[i]);
            }
        }

        private void Process()
        {
            List<long> matchIds = _matchBetTracker.Keys.ToList();
            for (int i=0; i<matchIds.Count; i++)
            {
                Match match = DataStore.Matchs[matchIds[i]];
                Strategy_002_Model tracker = _matchBetTracker[matchIds[i]];

                if (match.LivePeriod == 2
                    && match.TimeSpanFromStart.TotalMinutes >= 31)
                {
                    if (!tracker.StepOne_a.Betted && !tracker.StepOne_a.Canceled)
                    {
                        _matchBetTracker[match.MatchId].StepOne_a.Betted = true;
                        Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, true, 2, 100, 50, "Strategy_002: Đánh tài cuối H2 lần 1 ở phút " + (int)match.TimeSpanFromStart.TotalMinutes);
                    }

                    if (!tracker.StepOne_b.Betted && !tracker.StepOne_b.Canceled
                        && match.TimeSpanFromStart.TotalMinutes >= (DataStore.GoalHistories[match.MatchId][0].TimeSpanFromStart.TotalMinutes + 1))
                    {
                        _matchBetTracker[match.MatchId].StepOne_b.Betted = true;
                        Common.Functions.UpdateTempBet(tracker.StepOne_b.BetId, true, 2, 100, 50, "Strategy_002: Đánh tài cuối H2 lần 2 ở phút " + (int)match.TimeSpanFromStart.TotalMinutes);
                    }
                }
            }
        }

        private void Cancel()
        {
            List<long> matchIds = _matchBetTracker.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                Match match = DataStore.Matchs[matchIds[i]];
                Strategy_002_Model tracker = _matchBetTracker[matchIds[i]];

                if (DataStore.OverUnderScoreTimes.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(125)
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(100)
                    && match.TimeSpanFromStart.TotalMinutes >= 31
                    && match.LivePeriod == 2)
                {
                    int remain_125 = DataStore.OverUnderScoreTimes[matchIds[i]][125].Skip(1).Count(item => item.Length > 0);
                    int remain_100 = DataStore.OverUnderScoreTimes[matchIds[i]][100].Skip(1).Count(item => item.Length > 0);
                    if (remain_125 <= 11 || remain_100 <= 10)
                    {
                        if (!tracker.StepOne_a.Betted && !tracker.StepOne_a.Canceled)
                        {
                            _matchBetTracker[match.MatchId].StepOne_a.Canceled = true;
                            Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, true, 2, 0, -1, "Strategy_002: Đánh tài cuối H2 lần 1: Hủy vì remain 125 và remain 100 không hợp lệ");
                        }

                        if (!tracker.StepOne_b.Betted && !tracker.StepOne_b.Canceled)
                        {
                            _matchBetTracker[match.MatchId].StepOne_b.Canceled = true;
                            Common.Functions.UpdateTempBet(tracker.StepOne_b.BetId, true, 2, 0, -1, "Strategy_002: Đánh tài cuối H2 lần 2: Hủy vì remain 125 và remain 100 không hợp lệ");
                        }
                    }
                }


                if (DataStore.OverUnderScoreTimes.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(150)
                    && match.TimeSpanFromStart.TotalMinutes == 25
                    && match.LivePeriod == 2)
                {
                    int price_150 = 0;
                    string price_150_str = DataStore.OverUnderScoreTimes[matchIds[i]][150].Skip(1).Last(item => item.Length > 0);
                    if (price_150_str != null)
                    {
                        price_150 = int.Parse(price_150_str);
                        if (price_150 == 100 || price_150 < 0)
                        {
                            if (!tracker.StepOne_a.Betted && !tracker.StepOne_a.Canceled)
                            {
                                _matchBetTracker[match.MatchId].StepOne_a.Canceled = true;
                                Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, true, 2, 0, -1, "Strategy_002: Đánh tài cuối H2 lần 1: Hủy vì giá 150 ở phút 25 không hợp lệ");
                            }

                            if (!tracker.StepOne_b.Betted && !tracker.StepOne_b.Canceled)
                            {
                                _matchBetTracker[match.MatchId].StepOne_b.Canceled = true;
                                Common.Functions.UpdateTempBet(tracker.StepOne_b.BetId, true, 2, 0, -1, "Strategy_002: Đánh tài cuối H2 lần 2: Hủy vì giá 150 ở phút 25 không hợp lệ");
                            }
                        }
                    }
                }

                if (DataStore.OverUnderScoreTimes.ContainsKey(matchIds[i])
                    && DataStore.OverUnderScoreTimes[matchIds[i]].ContainsKey(175)
                    && match.TimeSpanFromStart.TotalMinutes == 20
                    && match.LivePeriod == 2)
                {
                    int price_175 = 0;
                    string price_175_str = DataStore.OverUnderScoreTimes[matchIds[i]][175].Skip(1).Last(item => item.Length > 0);
                    if (price_175_str != null)
                    {
                        price_175 = int.Parse(price_175_str);
                        if (price_175 == 100 || price_175 < 0)
                        {
                            if (!tracker.StepOne_a.Betted && !tracker.StepOne_a.Canceled)
                            {
                                _matchBetTracker[match.MatchId].StepOne_a.Canceled = true;
                                Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, true, 2, 0, -1, "Strategy_002: Đánh tài cuối H2 lần 1: Hủy vì giá 175 ở phút 25 không hợp lệ");
                            }

                            if (!tracker.StepOne_b.Betted && !tracker.StepOne_b.Canceled)
                            {
                                _matchBetTracker[match.MatchId].StepOne_b.Canceled = true;
                                Common.Functions.UpdateTempBet(tracker.StepOne_b.BetId, true, 2, 0, -1, "Strategy_002: Đánh tài cuối H2 lần 2: Hủy vì giá 175 ở phút 25 không hợp lệ");
                            }
                        }
                    }
                }

                if (match.TimeSpanFromStart.TotalMinutes <= 30
                    && (match.LiveAwayScore + match.LiveHomeScore) > 1
                    && match.LivePeriod == 2)
                {
                    if (!tracker.StepOne_a.Betted && !tracker.StepOne_a.Canceled)
                    {
                        _matchBetTracker[match.MatchId].StepOne_a.Canceled = true;
                        Common.Functions.UpdateTempBet(tracker.StepOne_a.BetId, true, 2, 0, -1, "Strategy_002: Đánh tài cuối H2 lần 1: Hủy vì có thêm banh trước phút 31");
                    }

                    if (!tracker.StepOne_b.Betted && !tracker.StepOne_b.Canceled)
                    {
                        _matchBetTracker[match.MatchId].StepOne_b.Canceled = true;
                        Common.Functions.UpdateTempBet(tracker.StepOne_b.BetId, true, 2, 0, -1, "Strategy_002: Đánh tài cuối H2 lần 2: Hủy vì có thêm banh trước phút 31");
                    }
                }
            }
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
                LivePeriod = 2,
                Hdp = 0,
                Price = -1,
                Stake = DataStore.AutobetStake,
                IsOver = true,
                IsFulltime = true,
                TotalScore = 1,
                TotalRed = 0,
                CreateDateTime = DateTime.Now,
                MoneyLine = match.OverUnderMoneyLine,
                AutoBetMessage = "Strategy_002: Đánh tài cuối H2 lần 1: Lên lệnh trước"
            });

            DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
            {
                Id = _matchBetTracker[matchId].StepOne_b.BetId,
                MatchId = matchId,
                LivePeriod = 2,
                Hdp = 0,
                Price = -1,
                Stake = DataStore.AutobetStake,
                IsOver = true,
                IsFulltime = true,
                TotalScore = 1,
                TotalRed = 0,
                CreateDateTime = DateTime.Now,
                MoneyLine = match.OverUnderMoneyLine,
                AutoBetMessage = "Strategy_002: Đánh tài cuối H2 lần 2: Lên lệnh trước"
            });

        }

    }
}
