using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services.AutoBet.Strategy_001_d
{

    public class Strategy_001_d
    {
        private int _minMoneyLine = 8;
        private int _maxMoneyLine = 18;
        private int _minBeginHdp = 225;
        private int _maxBeginHdp = 500;
        private int _stepOneStake = 10;
        private int _stepTwoStake = 30;

        private Dictionary<long, Strategy_001_d_Model> _betTracker;

        private Dictionary<long, bool> _matchBeginWithHdpsStepOneBetted_a; // <matchId, isBetted (step 1)>
        private Dictionary<long, bool> _matchBeginWithHdpsStepOneBetted_b; // <matchId, isBetted (step 1)>

        private Dictionary<long, bool> _matchBeginWithHdpsStepTwoBetted_a; // <matchId, isBetted (step 2)>
        private Dictionary<long, bool> _matchBeginWithHdpsStepTwoBetted_b; // <matchId, isBetted (step 2)>
        private Dictionary<long, bool> _matchBeginWithHdpsStepTwoBetted_c; // <matchId, isBetted (step 2)>
        private Dictionary<long, int> _targetHdpOfMatch; // <matchId, targetHdp>
        private Dictionary<long, int> _beginHdpOfMatch; // <matchId, targetHdp>

        public Strategy_001_d()
        {
            _matchBeginWithHdpsStepOneBetted_a = new Dictionary<long, bool>();
            _matchBeginWithHdpsStepOneBetted_b = new Dictionary<long, bool>();

            _matchBeginWithHdpsStepTwoBetted_a = new Dictionary<long, bool>();
            _matchBeginWithHdpsStepTwoBetted_b = new Dictionary<long, bool>();
            _matchBeginWithHdpsStepTwoBetted_c = new Dictionary<long, bool>();

            _targetHdpOfMatch = new Dictionary<long, int>();
            _beginHdpOfMatch = new Dictionary<long, int>();

            _betTracker = new Dictionary<long, Strategy_001_d_Model>();
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

        private void PlaceTempBetStepOne(Match match)
        {
            long matchId = match.MatchId;

            if (!_betTracker.ContainsKey(matchId))
            {
                _betTracker.Add(matchId, new Strategy_001_d_Model());
            }

            _betTracker[matchId].StepOneId_a = Guid.NewGuid();
            _betTracker[matchId].StepOneId_b = Guid.NewGuid();

            int stake = _stepOneStake;

            if (Common.Functions.IsWeekend(match.KickoffTime))
            {
                if (match.OverUnderMoneyLine == 18)
                {
                    stake = 3;
                }
            }

            DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
            {
                Id = _betTracker[matchId].StepOneId_a,
                MatchId = matchId,
                LivePeriod = 1,
                Stake = stake,
                IsOver = true,
                IsFulltime = false,
                TotalScore = 0,
                TotalRed = 0,
                CreateDateTime = DateTime.Now,
                MoneyLine = match.OverUnderMoneyLine,

                AutoBetMessage = "Đánh tài H1 (lần 1):"
                    + " kèo cao nhất trừ 25: ??"
                    + ", biên phút 01: ??"
                    + ", biên chính thức: ??"
                    + ", kèo mục tiêu: ??"
                    + ", giá kèo mục tiêu: ??",
                Hdp = 0,
                Price = -1
            });

            DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
            {
                Id = Guid.NewGuid(),
                MatchId = matchId,
                LivePeriod = 1,
                Stake = stake,
                IsOver = true,
                IsFulltime = false,
                TotalScore = 0,
                TotalRed = 0,
                CreateDateTime = DateTime.Now,
                MoneyLine = match.OverUnderMoneyLine,

                AutoBetMessage = "Đánh tài H1 (Lần 2):" 
                    + " kèo cao nhất trừ 25: ??"
                    + ", giá mở kèo: ??"
                    + ", kèo mục tiêu: ??"
                    + ", giá kèo mục tiêu: ??",
                Hdp = 0,
                Price = -1
            });

        }

        private void PlaceTempBetStepTwo(Match match)
        {
            long matchId = match.MatchId;

            if (!_betTracker.ContainsKey(matchId))
            {
                _betTracker.Add(matchId, new Strategy_001_d_Model());
            }

            _betTracker[matchId].StepTwoId_a = Guid.NewGuid();
            _betTracker[matchId].StepTwoId_b = Guid.NewGuid();
            _betTracker[matchId].StepTwoId_c = Guid.NewGuid();

            int stake = _stepTwoStake;

            if (Common.Functions.IsWeekend(match.KickoffTime))
            {
                if (match.OverUnderMoneyLine == 18)
                {
                    stake = 15;
                }
            }

            DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
            {
                Id = Guid.NewGuid(),
                MatchId = match.MatchId,
                LivePeriod = 2,
                Stake = stake,
                IsOver = true,
                IsFulltime = true,
                TotalScore = 0,
                TotalRed = 0,
                CreateDateTime = DateTime.Now,
                MoneyLine = match.OverUnderMoneyLine,

                AutoBetMessage = "Gỡ tài H1 (lần 1): "
                    + " Max nghỉ giải lao: ??"
                    + " Min đóng kèo: ??"
                    + " Đánh tài vào kèo : ??"
                    + " Giá: ??",
                Hdp = 0,
                Price = -1
            });

            DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
            {
                Id = Guid.NewGuid(),
                MatchId = match.MatchId,
                LivePeriod = 2,
                Stake = stake / 2,
                IsOver = true,
                IsFulltime = true,
                TotalScore = 0,
                TotalRed = 0,
                CreateDateTime = DateTime.Now,
                MoneyLine = match.OverUnderMoneyLine,

                AutoBetMessage = "Gỡ tài H1 (lần 2):"
                    + " Max nghỉ giải lao: ??"
                    + " Min đóng kèo: ??"
                    + " Đánh tài vào kèo : ??" 
                    + " Giá: ??",
                Hdp = 0,
                Price = -1
            });

        }

        private void UpdateTempBet(Guid betId, int hdp, int price, string autobetMessage)
        {
            BetByHdpPrice bet = DataStore.BetByHdpPrice.FirstOrDefault(item => item.Id == betId);
            if (bet != null)
            {
                bet.Hdp = hdp;
                bet.Price = price;
                bet.AutoBetMessage = autobetMessage;
            } 
        }

        private void CleanUp()
        {
            List<long> matchIds = _matchBeginWithHdpsStepOneBetted_a.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                if (DataStore.Matchs[matchIds[i]].LivePeriod == -1)
                {
                    _matchBeginWithHdpsStepOneBetted_a.Remove(matchIds[i]);
                }
            }

            matchIds = _matchBeginWithHdpsStepOneBetted_b.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                if (DataStore.Matchs[matchIds[i]].LivePeriod == -1)
                {
                    _matchBeginWithHdpsStepOneBetted_b.Remove(matchIds[i]);
                }
            }

            matchIds = _matchBeginWithHdpsStepTwoBetted_a.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                if (DataStore.Matchs[matchIds[i]].LivePeriod == -1)
                {
                    _matchBeginWithHdpsStepTwoBetted_a.Remove(matchIds[i]);
                }
            }

            matchIds = _matchBeginWithHdpsStepTwoBetted_b.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                if (DataStore.Matchs[matchIds[i]].LivePeriod == -1)
                {
                    _matchBeginWithHdpsStepTwoBetted_b.Remove(matchIds[i]);
                }
            }

            matchIds = _matchBeginWithHdpsStepTwoBetted_c.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                if (DataStore.Matchs[matchIds[i]].LivePeriod == -1)
                {
                    _matchBeginWithHdpsStepTwoBetted_c.Remove(matchIds[i]);
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

                if (!DataStore.OverUnderScoreTimes.ContainsKey(matchId) || DataStore.OverUnderScoreTimes[matchId].Count == 0) continue;

                Match match = DataStore.Matchs[matchId];
                int? beginHdp = DataStore.OverUnderScoreTimes[matchId].Keys.Max();

                if (beginHdp.HasValue
                    && _minBeginHdp <= beginHdp && beginHdp <= _maxBeginHdp
                    && _minMoneyLine <= match.OverUnderMoneyLine && match.OverUnderMoneyLine <= _maxMoneyLine
                    && !_betTracker.ContainsKey(matchId)
                    && !_matchBeginWithHdpsStepOneBetted_a.ContainsKey(matchId))
                {

                    _matchBeginWithHdpsStepOneBetted_a.Add(matchId, false);
                    _matchBeginWithHdpsStepOneBetted_b.Add(matchId, false);
                    _targetHdpOfMatch.Add(matchId, beginHdp.Value - 100);
                    _beginHdpOfMatch.Add(matchId, beginHdp.Value - 25);

                    PlaceTempBetStepOne(match);

                    UpdateTempBet(_betTracker[matchId].StepOneId_a, 0, -1, 
                        "Đánh tài H1 (lần 1):"
                        + " kèo cao nhất trừ 25: " + (beginHdp.Value - 25)
                        + ", biên phút 01: ??"
                        + ", biên chính thức: ??"
                        + ", kèo mục tiêu: " + (beginHdp.Value - 100)
                        + ", giá kèo mục tiêu: ??"
                    );

                    UpdateTempBet(_betTracker[matchId].StepOneId_b, 0, -1, 
                        "Đánh tài H1 (Lần 2):" 
                        + " kèo cao nhất trừ 25: " + (beginHdp.Value - 25)
                        + ", giá mở kèo: ??"
                        + ", kèo mục tiêu: " + (beginHdp.Value - 100)
                        + ", giá kèo mục tiêu: ??"
                    );
                }
            }
        }

        private void StepOne()
        {
            List<long> matchIds = _betTracker.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                CheckAndPlaceBetStepOne(matchIds[i]);
            }
        }

        private int CalculateBoundaryA(Match match)
        {
            int boundaryA = 100;
            try
            {
                if (match.OverUnderMoneyLine <= 18
                    && DataStore.OverUnderScoreTimes.ContainsKey(match.MatchId)
                    && DataStore.OverUnderScoreTimes[match.MatchId].Count > 0)
                {
                    int maxHdp = 0;
                    List<int> hdps = DataStore.OverUnderScoreTimes[match.MatchId].Keys.OrderByDescending(item => item).ToList();
                    Dictionary<int, int> priceOpen = new Dictionary<int, int>();
                    for (int i = 0; i < hdps.Count; i++)
                    {
                        string valueOpenStr = DataStore.OverUnderScoreTimes[match.MatchId][hdps[i]][1];
                        int valueOpen;
                        if (valueOpenStr != "" && int.TryParse(valueOpenStr, out valueOpen))
                        {
                            priceOpen.Add(hdps[i], valueOpen);
                            maxHdp = maxHdp < hdps[i] ? hdps[i] : maxHdp;
                        }
                    }

                    if (priceOpen.Count == 4)
                    {
                        if (priceOpen[maxHdp] < 0
                            && priceOpen[maxHdp - 25] > 0
                            && priceOpen[maxHdp - 50] > 0
                            && priceOpen[maxHdp - 75] > 0)
                        {
                            boundaryA = (priceOpen[maxHdp - 25] + priceOpen[maxHdp - 50]) / 2;
                        }
                        else if (priceOpen[maxHdp] < 0
                            && priceOpen[maxHdp - 25] < 0
                            && priceOpen[maxHdp - 50] > 0
                            && priceOpen[maxHdp - 75] > 0)
                        {
                            boundaryA = priceOpen[maxHdp - 50];
                        }
                    }
                    else if (priceOpen.Count == 3)
                    {
                        if (priceOpen[maxHdp] > 0
                            && priceOpen[maxHdp - 25] > 0
                            && priceOpen[maxHdp - 50] > 0)
                        {
                            boundaryA = (priceOpen[maxHdp] + priceOpen[maxHdp - 25]) / 2;
                        }
                        else if (priceOpen[maxHdp] < 0
                            && priceOpen[maxHdp - 25] < 0
                            && priceOpen[maxHdp - 50] > 0)
                        {
                            boundaryA = priceOpen[maxHdp - 50];
                        }
                        else if (priceOpen[maxHdp] > 0
                            && priceOpen[maxHdp - 25] > 0
                            && priceOpen[maxHdp - 50] > 0)
                        {
                            boundaryA = (priceOpen[maxHdp] + priceOpen[maxHdp - 25]) / 2;
                        }
                    }
                    else if (priceOpen.Count == 2)
                    {
                        boundaryA = priceOpen[maxHdp - 25];
                    }
                }

            }
            catch
            {

            }
            

            return boundaryA;
        }

        private void InitStepTwo(long matchId)
        {
            if (!_matchBeginWithHdpsStepTwoBetted_a.ContainsKey(matchId)) _matchBeginWithHdpsStepTwoBetted_a.Add(matchId, false);
            if (!_matchBeginWithHdpsStepTwoBetted_b.ContainsKey(matchId)) _matchBeginWithHdpsStepTwoBetted_b.Add(matchId, false);
            if (!_matchBeginWithHdpsStepTwoBetted_c.ContainsKey(matchId)) _matchBeginWithHdpsStepTwoBetted_c.Add(matchId, false);
        }

        private void CheckAndPlaceBetStepOne(long matchId)
        {
            if (_matchBeginWithHdpsStepOneBetted_a[matchId] == true
                && _matchBeginWithHdpsStepOneBetted_b[matchId] == true) return;
            Match match = DataStore.Matchs[matchId];

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

            int boundaryA = CalculateBoundaryA(match);
            int boundaryB = firstPriceOfTargetHdp + match.OverUnderMoneyLine;
            int boundary = boundaryA < boundaryB ? boundaryA : boundaryB;

            if (boundary > 0 
                && priceOfbeginHdp > 0
                && priceOfbeginHdp > boundary) // Hủy lệnh theo giá mở kèo mục tiêu và biên
            {
                UpdateTempBet(_betTracker[matchId].StepOneId_a, 0, -1,
                    "Hủy lệnh theo giá mở kèo mục tiêu và biên"
                );

                UpdateTempBet(_betTracker[matchId].StepOneId_b, 0, -1,
                    "Hủy lệnh theo giá mở kèo mục tiêu và biên"
                );

                return;
            }

            UpdateTempBet(_betTracker[matchId].StepOneId_a, 0, -1,
                "Đánh tài H1 (lần 1):"
                + " kèo cao nhất trừ 25: " + beginHdp
                + ", biên phút 01: " + boundaryA
                + ", biên chính thức: " + boundary
                + ", kèo mục tiêu: " + targetHdp
                + ", giá kèo mục tiêu: " + priceOfTargetHdp
            );

            UpdateTempBet(_betTracker[matchId].StepOneId_b, 0, -1,
                "Đánh tài H1 (Lần 2):"
                + " kèo cao nhất trừ 25: " + beginHdp
                + ", giá mở kèo: " + priceOfbeginHdp
                + ", kèo mục tiêu: " + targetHdp
                + ", giá kèo mục tiêu: " + priceOfTargetHdp
            );

            int stake = _stepOneStake;

            if (Common.Functions.IsWeekend(match.KickoffTime))
            {
                if (match.OverUnderMoneyLine == 18)
                {
                    stake = 3;
                }
            }

            if (_matchBeginWithHdpsStepOneBetted_b.ContainsKey(matchId)
                && !_matchBeginWithHdpsStepOneBetted_b[matchId]
                && ((priceOfbeginHdp * priceOfTargetHdp > 0 && priceOfTargetHdp >= priceOfbeginHdp) || (priceOfbeginHdp > 0 && priceOfTargetHdp < 0)))
            {
                _matchBeginWithHdpsStepOneBetted_b[matchId] = true;

                UpdateTempBet(_betTracker[matchId].StepOneId_b, 50, -100,
                    "Đánh tài H1 (Lần 2):"
                    + " kèo cao nhất trừ 25: " + beginHdp
                    + ", giá mở kèo: " + priceOfbeginHdp
                    + ", kèo mục tiêu: " + targetHdp
                    + ", giá kèo mục tiêu: " + priceOfTargetHdp
                );

            }


            List<string> pricesStr = new List<string>();
            int minuteCheck = 0;

            if (match.OverUnderMoneyLine <= 10)
            {
                minuteCheck = 4;
            }
            else if (match.OverUnderMoneyLine <= 18)
            {
                minuteCheck = 6;
            }

            pricesStr = DataStore.OverUnderScoreTimes[match.MatchId][targetHdp].Skip(1).Where(item => item != "").Take(minuteCheck).ToList();

            for (int i = 0; i < pricesStr.Count; i++)
            {
                if (int.Parse(pricesStr[i]) >= boundary && !_matchBeginWithHdpsStepOneBetted_a[matchId])
                {
                    _matchBeginWithHdpsStepOneBetted_a[matchId] = true;
                    InitStepTwo(matchId);

                    UpdateTempBet(_betTracker[matchId].StepOneId_a, 50, 50,
                        "Đánh tài H1 (lần 1):"
                        + " kèo cao nhất trừ 25: " + beginHdp
                        + ", biên phút 01: " + boundaryA
                        + ", biên chính thức: " + boundary
                        + ", kèo mục tiêu: " + targetHdp
                        + ", giá kèo mục tiêu: " + pricesStr[i]
                        + ", trước phút thứ " + minuteCheck
                    );

                    return;
                }
            }

            if (pricesStr.Count >= minuteCheck && !_matchBeginWithHdpsStepOneBetted_a[matchId])
            {
                _matchBeginWithHdpsStepOneBetted_a[matchId] = true;
                InitStepTwo(matchId);

                UpdateTempBet(_betTracker[matchId].StepOneId_a, 50, 50,
                    "Đánh tài H1 (lần 1):"
                    + " kèo cao nhất trừ 25: " + beginHdp
                    + ", biên phút 01: " + boundaryA
                    + ", biên chính thức: " + boundary
                    + ", kèo mục tiêu: " + targetHdp
                    + " đánh ở phút " + (minuteCheck + 1)
                );

            }
        }

        private void StepTwo()
        {
            List<long> matchIds = _matchBeginWithHdpsStepTwoBetted_a.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                CheckAndPlaceBetStepTwo(matchIds[i]);
            }
        }

        private void PlaceBet_8_14(Match match, int stake, int maxScore, int priceOfBelowMaxScore)
        {
            if (_matchBeginWithHdpsStepTwoBetted_a.ContainsKey(match.MatchId)
                && !_matchBeginWithHdpsStepTwoBetted_a[match.MatchId])
            {
                _matchBeginWithHdpsStepTwoBetted_a[match.MatchId] = true;

                UpdateTempBet(_betTracker[match.MatchId].StepTwoId_a, maxScore - 50, priceOfBelowMaxScore,
                    "Gỡ tài H1 (lần 1): "
                    + " Max nghỉ giải lao: " + maxScore
                    + " Đánh tài vào kèo : " + (maxScore - 50)
                    + " Giá: " + priceOfBelowMaxScore
                );

            }

            if (_matchBeginWithHdpsStepTwoBetted_b.ContainsKey(match.MatchId)
                && !_matchBeginWithHdpsStepTwoBetted_b[match.MatchId])
            {
                _matchBeginWithHdpsStepTwoBetted_b[match.MatchId] = true;

                UpdateTempBet(_betTracker[match.MatchId].StepTwoId_b, maxScore - 75, priceOfBelowMaxScore,
                    "Gỡ tài H1 (lần 2): "
                    + " Max nghỉ giải lao: " + maxScore
                    + " Đánh tài vào kèo : " + (maxScore - 75)
                    + " Giá: " + priceOfBelowMaxScore
                );

            }

        }

        private void PlaceBet_16_18(Match match, int stake, int maxScore, int priceOfBelowMaxScore)
        {
            if (maxScore == 125
                && _matchBeginWithHdpsStepTwoBetted_b.ContainsKey(match.MatchId)
                && !_matchBeginWithHdpsStepTwoBetted_b[match.MatchId]
                && DataStore.OverUnderScoreTimes.ContainsKey(match.MatchId)
                && DataStore.OverUnderScoreTimes[match.MatchId].ContainsKey(75))
            {
                _matchBeginWithHdpsStepTwoBetted_b[match.MatchId] = true;

                UpdateTempBet(_betTracker[match.MatchId].StepTwoId_b, maxScore - 25, 50,
                    "Gỡ tài H1 (lần 2): "
                    + " Max nghỉ giải lao: " + maxScore
                    + " Đánh tài vào kèo : " + (maxScore - 25)
                    + " Giá: khi 075 xuất hiện"
                );

            }

            if (_matchBeginWithHdpsStepTwoBetted_a.ContainsKey(match.MatchId)
                && !_matchBeginWithHdpsStepTwoBetted_a[match.MatchId])
            {
                _matchBeginWithHdpsStepTwoBetted_a[match.MatchId] = true;

                UpdateTempBet(_betTracker[match.MatchId].StepTwoId_a, maxScore - 50, priceOfBelowMaxScore,
                    "Gỡ tài H1 (lần 1): "
                    + " Max nghỉ giải lao: " + maxScore
                    + " Đánh tài vào kèo : " + (maxScore - 50)
                    + " Giá: " + priceOfBelowMaxScore
                );

            }

            if (_matchBeginWithHdpsStepTwoBetted_b.ContainsKey(match.MatchId)
                && !_matchBeginWithHdpsStepTwoBetted_b[match.MatchId])
            {
                _matchBeginWithHdpsStepTwoBetted_b[match.MatchId] = true;
                UpdateTempBet(_betTracker[match.MatchId].StepTwoId_b, maxScore - 100, priceOfBelowMaxScore,
                    "Gỡ tài H1 (lần 2): "
                    + " Max nghỉ giải lao: " + maxScore
                    + " Đánh tài vào kèo : " + (maxScore - 75)
                    + " Giá: " + priceOfBelowMaxScore
                );
            }
        }

        private void CheckAndPlaceBetStepTwo(long matchId)
        {
            Match match;
            List<int> scores;
            int priceOfMaxScore;
            int priceOfBelowMaxScore;
            int maxScore;
            int minuteCheck = 2;

            if (!_matchBeginWithHdpsStepOneBetted_a.ContainsKey(matchId)
                || !_matchBeginWithHdpsStepOneBetted_a[matchId]) return;

            match = DataStore.Matchs[matchId];
            if (_betTracker.ContainsKey(matchId)
                && _betTracker[matchId].StepTwoId_a == Guid.Empty)
            {
                PlaceTempBetStepTwo(match);
            }

            if (_matchBeginWithHdpsStepTwoBetted_a.ContainsKey(matchId)
                && !_matchBeginWithHdpsStepTwoBetted_a[matchId]
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

                int boundaryOfStepTwo = BoundaryOfStepTwo(matchId, maxScore);
                if (priceOfMaxScore < 0
                    && priceOfMaxScore > boundaryOfStepTwo)
                {
                    if (DataStore.OverUnderScoreHalfTimes[matchId].ContainsKey(maxScore - 25)
                        && DataStore.OverUnderScoreHalfTimes[matchId][maxScore - 25][minuteCheck] != "")
                    {
                        maxScore = maxScore - 25;
                        priceOfMaxScore = int.Parse(DataStore.OverUnderScoreHalfTimes[matchId][maxScore - 25][minuteCheck]);
                    }
                    else return;
                }

                UpdateTempBet(_betTracker[matchId].StepTwoId_a, 0, -1,
                    "Gỡ tài H1 (lần 1): "
                    + " Max nghỉ giải lao: " + maxScore
                    + " Min đóng kèo: " + boundaryOfStepTwo
                    + " Đánh tài vào kèo : ??"
                    + " Giá: ??"
                );

                UpdateTempBet(_betTracker[matchId].StepTwoId_b, 0, -1,
                    "Gỡ tài H1 (lần 2): "
                    + " Max nghỉ giải lao: " + maxScore
                    + " Min đóng kèo: " + boundaryOfStepTwo
                    + " Đánh tài vào kèo : ??"
                    + " Giá: ??"
                );

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

                if (match.OverUnderMoneyLine <= 14)
                {
                    PlaceBet_8_14(match, stake, maxScore, priceOfBelowMaxScore);
                }
                else if (match.OverUnderMoneyLine <= 18)
                {
                    PlaceBet_16_18(match, stake, maxScore, priceOfBelowMaxScore);
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
                List<string> row = DataStore
                    .OverUnderScoreTimes[matchId][i]
                    .Skip(1)
                    .Where(item => item != "")
                    .ToList();

                if (row.Count > 0)
                {
                    string lastItemOfRow = row.Last();

                    if (int.TryParse(lastItemOfRow, out value))
                    {
                        if (value < 0 && value > result)
                        {
                            result = value;
                        }
                    }
                }
            }

            return result;
        }

    }
}
