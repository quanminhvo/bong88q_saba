﻿using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services.AutoBet
{

    public class Strategy_001_a
    {
        private int _minMoneyLine = 8;
        private int _maxMoneyLine = 18;
        private int _minBeginHdp = 225;
        private int _maxBeginHdp = 500;
        private int _stepOneStake = 10;
        private int _stepTwoStake = 30;

        private Dictionary<long, bool> _matchBeginWithHdpsStepOneBetted; // <matchId, isBetted (step 1)>
        private Dictionary<long, bool> _matchBeginWithHdpsStepTwoBetted; // <matchId, isBetted (step 2)>
        private Dictionary<long, int> _targetHdpOfMatch; // <matchId, targetHdp>
        private Dictionary<long, int> _beginHdpOfMatch; // <matchId, targetHdp>

        public Strategy_001_a()
        {
            _matchBeginWithHdpsStepOneBetted = new Dictionary<long, bool>();
            _matchBeginWithHdpsStepTwoBetted = new Dictionary<long, bool>();
            _targetHdpOfMatch = new Dictionary<long, int>();
            _beginHdpOfMatch = new Dictionary<long, int>();
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

        private void ExecuteCore(int milisecond)
        {
            while (true)
            {
                try
                {
                    UpdateMatchsStartWith_Hdp();
                    StepOne();
                    StepTwo();
                    CleanUp();
                    Thread.Sleep(milisecond);
                }
                catch (Exception ex)
                {
                    Common.Functions.WriteExceptionLog(ex);
                }
            }
        }

        private void CleanUp()
        {
            List<long> matchIds = _matchBeginWithHdpsStepOneBetted.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                if (DataStore.Matchs[matchIds[i]].LivePeriod == -1)
                {
                    _matchBeginWithHdpsStepOneBetted.Remove(matchIds[i]);
                }
            }

            matchIds = _matchBeginWithHdpsStepTwoBetted.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                if (DataStore.Matchs[matchIds[i]].LivePeriod == -1)
                {
                    _matchBeginWithHdpsStepTwoBetted.Remove(matchIds[i]);
                }
            }

            matchIds = _targetHdpOfMatch.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                if (DataStore.Matchs[matchIds[i]].LivePeriod == -1)
                {
                    _targetHdpOfMatch.Remove(matchIds[i]);
                }
            }

            matchIds = _beginHdpOfMatch.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                if (DataStore.Matchs[matchIds[i]].LivePeriod == -1)
                {
                    _beginHdpOfMatch.Remove(matchIds[i]);
                }
            }
        }

        private void UpdateMatchsStartWith_Hdp()
        {
            List<Match> matchs = DataStore.Matchs.Values
                .Where(item => item.LivePeriod == 1 
                    && item.TimeSpanFromStart.TotalMinutes < 5
                ).ToList();

            for (int i = 0; i < matchs.Count; i++ )
            {
                long matchId = matchs[i].MatchId;
                

                if (!DataStore.OverUnderScoreTimes.ContainsKey(matchId)
                    || DataStore.OverUnderScoreTimes[matchId].Count == 0) continue;

                int? beginHdp = DataStore.OverUnderScoreTimes[matchId].Keys.Max();

                Match match = DataStore.Matchs[matchId];
                int moneyLine = match.OverUnderMoneyLine;

                if (beginHdp.HasValue
                    && _minMoneyLine <= moneyLine && moneyLine <= _maxMoneyLine
                    && _minBeginHdp <= beginHdp && beginHdp <= _maxBeginHdp
                    && !_matchBeginWithHdpsStepOneBetted.ContainsKey(matchId))
                {
                    _matchBeginWithHdpsStepOneBetted.Add(matchId, false);
                    _targetHdpOfMatch.Add(matchId, beginHdp.Value - 100);
                    _beginHdpOfMatch.Add(matchId, beginHdp.Value - 25);
                }
            }
        }

        private void StepOne()
        {
            List<long> matchIds = _matchBeginWithHdpsStepOneBetted.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                CheckAndPlaceBetStepOne(matchIds[i]);
            }
        }

        private void CheckAndPlaceBetStepOne(long matchId)
        {
            if (_matchBeginWithHdpsStepOneBetted[matchId] == true) return;

            int targetHdp = _targetHdpOfMatch[matchId];
            int beginHdp = _beginHdpOfMatch[matchId];
            if (!DataStore.OverUnderScoreTimes[matchId].ContainsKey(beginHdp)) return;
            if (!DataStore.OverUnderScoreTimes[matchId].ContainsKey(targetHdp)) return;

            string priceOfbeginHdpStr = DataStore.OverUnderScoreTimes[matchId][beginHdp].Skip(1).FirstOrDefault(item => item != "");
            if (priceOfbeginHdpStr == null) return;
            int priceOfbeginHdp = int.Parse(priceOfbeginHdpStr);

            string priceOfTargetHdpStr = DataStore.OverUnderScoreTimes[matchId][targetHdp].LastOrDefault(item => item != "");
            if (priceOfTargetHdpStr == null) return;
            int priceOfTargetHdp = int.Parse(priceOfTargetHdpStr);

            string firstPriceOfTargetHdpStr = DataStore.OverUnderScoreTimes[matchId][targetHdp].Skip(1).FirstOrDefault(item => item != "");
            if (firstPriceOfTargetHdpStr == null) return;
            int firstPriceOfTargetHdp = int.Parse(firstPriceOfTargetHdpStr);

            if(firstPriceOfTargetHdp > 0 && priceOfbeginHdp > 0 
                && priceOfbeginHdp < firstPriceOfTargetHdp) // Hủy lệnh theo giá mở kèo mục tiêu và giá mở của kèo đầu tiên
            {
                return;
            }

            if ((priceOfbeginHdp * priceOfTargetHdp > 0 && priceOfTargetHdp >= priceOfbeginHdp)
                || (priceOfbeginHdp > 0 && priceOfTargetHdp < 0))
            {
                _matchBeginWithHdpsStepOneBetted[matchId] = true;
                _matchBeginWithHdpsStepTwoBetted.Add(matchId, false);

                int stake = _stepOneStake;
                Match match = DataStore.Matchs[matchId];

                if (Common.Functions.IsWeekend(match.KickoffTime))
                {
                    if (match.OverUnderMoneyLine == 18)
                    {
                        stake = 3;
                    }
                }

                DataStore.BetByHdpPrice.Add(new BetByHdpPrice() {
                    Id = Guid.NewGuid(),
                    MatchId = matchId,
                    LivePeriod = 1,
                    Hdp = 50,
                    Price = -100,
                    Stake = stake,
                    IsOver = true,
                    IsFulltime = false,
                    TotalScore = 0,
                    TotalRed = 0,
                    CreateDateTime = DateTime.Now,
                    MoneyLine = DataStore.Matchs[matchId].OverUnderMoneyLine,
                    AutoBetMessage = "Strategy_001_a: kèo đầu tiên: " + beginHdp 
                        + ", giá mở kèo: " + priceOfbeginHdp 
                        + ", kèo mục tiêu: " + targetHdp 
                        + ", giá kèo mục tiêu: " + priceOfTargetHdp 
                        + " -> đánh tài H1 "
                });


            }
        }

        private void StepTwo()
        {
            List<long> matchIds = _matchBeginWithHdpsStepTwoBetted.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                CheckAndPlaceBetStepTwo(matchIds[i]);
            }
        }

        private void CheckAndPlaceBetStepTwo(long matchId)
        {
            Match match;
            List<int> scores;
            int priceOfMaxScore;
            int priceOfBelowMaxScore;
            int maxScore;
            int minuteCheck = 7;

            if (_matchBeginWithHdpsStepOneBetted.ContainsKey(matchId)
                && _matchBeginWithHdpsStepOneBetted[matchId]
                && _matchBeginWithHdpsStepTwoBetted.ContainsKey(matchId)
                && !_matchBeginWithHdpsStepTwoBetted[matchId]
                && DataStore.Matchs.TryGetValue(matchId, out match)
                && match.LivePeriod == 2
                && match.LiveHomeScore + match.LiveAwayScore == 0
                && DataStore.OverUnderScoreHalfTimes.ContainsKey(matchId))
            {
                scores = DataStore.OverUnderScoreHalfTimes[matchId].Keys.ToList();
                maxScore = 0;
                priceOfMaxScore = -1;
                for (int i=0; i<scores.Count; i++)
                {
                    string value = DataStore.OverUnderScoreHalfTimes[matchId][scores[i]][minuteCheck];
                    if (value != "" && scores[i] > maxScore)
                    {
                        maxScore = scores[i];
                        priceOfMaxScore = int.Parse(value);
                    }
                }

                if (priceOfMaxScore < 0 && priceOfMaxScore > BoundaryOfStepTwo(matchId, maxScore))
                {
                    if (DataStore.OverUnderScoreHalfTimes[matchId].ContainsKey(maxScore - 25)
                        && DataStore.OverUnderScoreHalfTimes[matchId][maxScore - 25][minuteCheck] != "")
                    {
                        maxScore = maxScore - 25;
                        priceOfMaxScore = int.Parse(DataStore.OverUnderScoreHalfTimes[matchId][maxScore - 25][minuteCheck]);
                    }
                    else return;
                }

                if (!DataStore.OverUnderScoreHalfTimes[matchId].ContainsKey(maxScore - 25) 
                    || !int.TryParse(DataStore.OverUnderScoreHalfTimes[matchId][maxScore - 25][minuteCheck], out priceOfBelowMaxScore))
                {
                    return;
                }

                int stake = _stepTwoStake;

                if (Common.Functions.IsWeekend(match.KickoffTime))
                {
                    if (match.OverUnderMoneyLine == 18)
                    {
                        stake = 15;
                    }
                }

                if (maxScore == 200 || maxScore == 175)
                {
                    _matchBeginWithHdpsStepTwoBetted[matchId] = true;
                    DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                    {
                        Id = Guid.NewGuid(),
                        MatchId = matchId,
                        LivePeriod = 2,
                        Hdp = maxScore - 50,
                        Price = priceOfBelowMaxScore,
                        Stake = stake,
                        IsOver = true,
                        IsFulltime = true,
                        TotalScore = 0,
                        TotalRed = 0,
                        CreateDateTime = DateTime.Now,
                        MoneyLine = DataStore.Matchs[matchId].OverUnderMoneyLine,
                        AutoBetMessage = "Strategy_001_a: tài H1 bị thua thì đánh tiếp tài H2."
                            + " Max nghỉ giải lao: " + maxScore
                            + " Đánh tài vào kèo : " + (maxScore - 50)
                            + " Giá: " + priceOfBelowMaxScore
                    });
                }
                else if (maxScore == 150 || maxScore == 125)
                {
                    if (DataStore.OverUnderScoreTimes[matchId].ContainsKey(maxScore - 50))
                    {
                        _matchBeginWithHdpsStepTwoBetted[matchId] = true;
                        DataStore.BetByHdpPrice.Add(new Models.DataModels.BetByHdpPrice()
                        {
                            Id = Guid.NewGuid(),
                            MatchId = matchId,
                            LivePeriod = 2,
                            Hdp = maxScore - 50,
                            Price = priceOfBelowMaxScore,
                            Stake = stake,
                            IsOver = true,
                            IsFulltime = true,
                            TotalScore = 0,
                            TotalRed = match.HomeRed + match.AwayRed,
                            MoneyLine = match.OverUnderMoneyLine,
                            CreateDateTime = DateTime.Now,
                            AutoBetMessage = "Strategy_001_a: tài H1 bị thua thì đánh tiếp tài H2."
                                + " Max nghỉ giải lao: " + maxScore
                                + " Đánh tài vào kèo : " + (maxScore - 50)
                                + " Giá: " + priceOfBelowMaxScore
                        });
                    }
                }

            }
        }

        private int BoundaryOfStepTwo(long matchId, int minHdp)
        {
            int maxHdp = DataStore.OverUnderScoreTimes[matchId].Keys.Max();

            int result = -100;
            int value;

            for (int i = minHdp + 25; i <= maxHdp; i = i + 25)
            {
                string lastItemOfRow = DataStore
                    .OverUnderScoreTimes[matchId][i]
                    .Skip(1)
                    .Where(item => item != "")
                    .Last();

                if (int.TryParse(lastItemOfRow, out value))
                {
                    if (value < 0 && value > result)
                    {
                        result = value;
                    }
                }
            }

            return result;
        }

    }
}
