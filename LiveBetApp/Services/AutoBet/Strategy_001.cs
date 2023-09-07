using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services.AutoBet
{

    public class Strategy_001
    {
        private int _firstBeginHdp;
        private int _secondBeginHdp;
        private int _targetHdp;
        private int _minuteFirstBeginHdpRemain;
        private int _minMoneyLine = 12;
        private int _maxMoneyLine = 18;

        private List<int> _boundaryIndexes;
        private Dictionary<long, bool> _matchBeginWithHdpsStepOneBetted; // <matchId, isBetted (step 1)>
        private Dictionary<long, bool> _matchBeginWithHdpsStepTwoBetted; // <matchId, isBetted (step 2)>

        public Strategy_001(int firstBeginHdp, int tagetHdp, int minuteFirstBeginHdpRemain)
        {
            _firstBeginHdp = firstBeginHdp;
            _secondBeginHdp = firstBeginHdp - 25;
            _targetHdp = tagetHdp;
            _minuteFirstBeginHdpRemain = minuteFirstBeginHdpRemain;
            _boundaryIndexes = new List<int>();
            _matchBeginWithHdpsStepOneBetted = new Dictionary<long, bool>();
            _matchBeginWithHdpsStepTwoBetted = new Dictionary<long, bool>();

            if (_targetHdp >= _secondBeginHdp)
            {
                throw new InvalidOperationException();
            }
            int delta = _firstBeginHdp - _targetHdp;
            for (int i = 25; i <= delta; i = i + 25)
            {
                _boundaryIndexes.Add(_targetHdp + i);
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

        private void ExecuteCore(int milisecond)
        {
            while (true)
            {
                try
                {
                    UpdateMatchsStartWith_BeginHdps();
                    StepOne();
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
            List<long> stepOneMatchIds = _matchBeginWithHdpsStepOneBetted.Keys.ToList();
            for (int i = 0; i < stepOneMatchIds.Count; i++)
            {
                if (DataStore.Matchs[stepOneMatchIds[i]].LivePeriod != 1)
                {
                    _matchBeginWithHdpsStepOneBetted.Remove(stepOneMatchIds[i]);
                }
            }

            List<long> stepTwoMatchIds = _matchBeginWithHdpsStepTwoBetted.Keys.ToList();
            for (int i = 0; i < stepTwoMatchIds.Count; i++)
            {
                if (DataStore.Matchs[stepTwoMatchIds[i]].LivePeriod == -1)
                {
                    _matchBeginWithHdpsStepTwoBetted.Remove(stepTwoMatchIds[i]);
                }
            }
        }

        private void UpdateMatchsStartWith_BeginHdps()
        {
            List<Match> matchs = DataStore.Matchs.Values.Where(item => item.LivePeriod == 1 && item.TimeSpanFromStart.TotalMinutes < 5).ToList();

            for (int i = 0; i < matchs.Count; i++ )
            {
                long matchId = matchs[i].MatchId;
                int moneyLine = matchs[i].OverUnderMoneyLine;

                if (moneyLine >= _minMoneyLine
                    && moneyLine <= _maxMoneyLine
                    && DataStore.OverUnderScoreTimes.ContainsKey(matchId)
                    && DataStore.OverUnderScoreTimes[matchId].ContainsKey(_firstBeginHdp)
                    && DataStore.OverUnderScoreTimes[matchId].ContainsKey(_secondBeginHdp)
                    && DataStore.OverUnderScoreTimes[matchId].Count == 2
                    && !_matchBeginWithHdpsStepOneBetted.ContainsKey(matchId))
                {
                    _matchBeginWithHdpsStepOneBetted.Add(matchs[i].MatchId, false);
                }
            }
        }

        private void StepOne()
        {
            List<long> matchIds = _matchBeginWithHdpsStepOneBetted.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                CheckAndPlaceBetByHdpPrice(matchIds[i]);
            }
        }

        private void CheckAndPlaceBetByHdpPrice(long matchId)
        {
            if (_matchBeginWithHdpsStepOneBetted[matchId] == true) return;
            if (!DataStore.OverUnderScoreTimes[matchId].ContainsKey(_targetHdp)) return;

            int? minPriceOfLastBoundaries = DataStore.OverUnderLastBoundariesOfMatch[matchId]
                .Where(item => item.Key <= _firstBeginHdp && item.Key > _targetHdp && item.Value < 0)
                .Max(item => item.Value);

            if (!minPriceOfLastBoundaries.HasValue) return;

            int remainingTimeOfFirstBeginHdp = DataStore.OverUnderScoreTimes[matchId][_firstBeginHdp]
                .Where(item => item != "")
                .Count();

            Match match = DataStore.Matchs[matchId];
            string priceOfTargetHdpStr = DataStore.OverUnderScoreTimes[matchId][_targetHdp].Where(item => item != "").LastOrDefault();
            if (priceOfTargetHdpStr == null) return;

            int priceOfTargetHdp = int.Parse(priceOfTargetHdpStr);
            int moneyLine = match.OverUnderMoneyLine;

            if (remainingTimeOfFirstBeginHdp >= _minuteFirstBeginHdpRemain 
                && minPriceOfLastBoundaries.Value < 0
                && priceOfTargetHdp < 0
                && minPriceOfLastBoundaries < 0
                && priceOfTargetHdp + moneyLine > minPriceOfLastBoundaries)
            {
                _matchBeginWithHdpsStepOneBetted[matchId] = true;
                
                DataStore.BetByHdpPrice.Add(new BetByHdpPrice() {
                    Id = Guid.NewGuid(),
                    MatchId = matchId,
                    LivePeriod = 1,
                    Hdp = 50,
                    Price = -100,
                    Stake = 3,
                    IsOver = true,
                    IsFulltime = false,
                    TotalScore = 0,
                    TotalRed = 0,
                    CreateDateTime = DateTime.Now,
                    MoneyLine = match.OverUnderMoneyLine,
                    AutoBetMessage = "Strategy_001: (" + _firstBeginHdp + " " + _secondBeginHdp + "), kèo mục tiêu " + _targetHdp + ", giá min biên" + minPriceOfLastBoundaries + ", giá thoát xỉu: " + (priceOfTargetHdp + moneyLine) + " -> đánh tài H1 "
                });

                if (!_matchBeginWithHdpsStepTwoBetted.ContainsKey(matchId))
                {
                    _matchBeginWithHdpsStepTwoBetted.Add(matchId, false);
                }

            }
        }



    }
}
