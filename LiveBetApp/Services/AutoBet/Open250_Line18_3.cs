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
    public class Open250_Line18_3
    {

        private List<long> _bettedMatchId_3;
        private List<long> _bettedMatchId_45;

        public Open250_Line18_3()
        {
            _bettedMatchId_3 = new List<long>();
            _bettedMatchId_45 = new List<long>();
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
                    if (DataStore.RunAutobet)
                    {
                        UpdateShouldBetMatchIds();
                    }
                    //CleanUp();
                    Thread.Sleep(milisecond);
                }
                catch (Exception ex)
                {
                    Common.Functions.WriteExceptionLog(ex);
                }
            }
        }

        public void UpdateShouldBetMatchIds()
        {
            List<Match> matchs = DataStore.Matchs.Values
                .Where(item => 
                    item.OverUnderMoneyLine == 18
                    && item.League.Trim().ToUpper() != "SPAIN TERCERA DIVISION"
                    && item.LivePeriod == 1
                    && (item.LiveHomeScore + item.LiveAwayScore) == 1
                    && !_bettedMatchId_3.Contains(item.MatchId)
                    && DataStore.GoalHistories.ContainsKey(item.MatchId)
                    && DataStore.GoalHistories[item.MatchId].Count > 0
                    && DataStore.OverUnderScoreTimes.ContainsKey(item.MatchId)
                    && DataStore.OverUnderScoreTimes[item.MatchId].ContainsKey(225)
                    && DataStore.OverUnderScoreTimes[item.MatchId].ContainsKey(250)
                    && DataStore.OverUnderScoreTimes[item.MatchId].Keys.Max() == 250
                    )
                .ToList();

            try{ UpdateBet3(matchs); }
            catch{ }

            try { UpdateBet45(matchs); }
            catch { }
            
        }

        private void UpdateBet3(List<Match> matchs)
        {

            for (int i = 0; i < matchs.Count; i++)
            {
                int firstGoalMinute = (int)DataStore.GoalHistories[matchs[i].MatchId][0].TimeSpanFromStart.TotalMinutes;
                if (_bettedMatchId_3.Contains(matchs[i].MatchId)
                    || firstGoalMinute > 10
                    || DataStore.OverUnderScoreTimes[matchs[i].MatchId][225][firstGoalMinute] == "0"
                    || DataStore.OverUnderScoreTimes[matchs[i].MatchId][250][firstGoalMinute] == "0")
                {
                    continue;
                }

                int openPriceOver225 = OpenPriceOver(matchs[i].MatchId, 225);
                if (openPriceOver225 < 68 || openPriceOver225 > 75)
                {
                    continue;
                }

                _bettedMatchId_3.Add(matchs[i].MatchId);

                DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                {
                    Id = Guid.NewGuid(),
                    MatchId = matchs[i].MatchId,
                    LivePeriod = 1,
                    Hdp = 225,
                    Price = 95,
                    Stake = DataStore.AutobetStake,
                    IsOver = true,
                    IsFulltime = true,
                    TotalScore = 1,
                    TotalRed = 10,
                    CreateDateTime = DateTime.Now,
                    MoneyLine = matchs[i].OverUnderMoneyLine,
                    AutoBetMessage = "Open250_Line18_3"
                });

                DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                {
                    Id = Guid.NewGuid(),
                    MatchId = matchs[i].MatchId,
                    LivePeriod = 1,
                    Hdp = 200,
                    Price = 95,
                    Stake = DataStore.AutobetStake,
                    IsOver = true,
                    IsFulltime = true,
                    TotalScore = 1,
                    TotalRed = 10,
                    CreateDateTime = DateTime.Now,
                    MoneyLine = matchs[i].OverUnderMoneyLine,
                    AutoBetMessage = "Open250_Line18_3"
                });

                //DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                //{
                //    Id = Guid.NewGuid(),
                //    MatchId = matchs[i].MatchId,
                //    LivePeriod = 1,
                //    Hdp = 175,
                //    Price = 95,
                //    Stake = DataStore.AutobetStake,
                //    IsOver = true,
                //    IsFulltime = true,
                //    TotalScore = 1,
                //    TotalRed = 10,
                //    CreateDateTime = DateTime.Now,
                //    MoneyLine = matchs[i].OverUnderMoneyLine,
                //    AutoBetMessage = "Open250_Line18_3"
                //});

                DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                {
                    Id = Guid.NewGuid(),
                    MatchId = matchs[i].MatchId,
                    LivePeriod = 1,
                    Hdp = 150,
                    Price = 95,
                    Stake = DataStore.AutobetStake,
                    IsOver = true,
                    IsFulltime = true,
                    TotalScore = 1,
                    TotalRed = 10,
                    CreateDateTime = DateTime.Now,
                    MoneyLine = matchs[i].OverUnderMoneyLine,
                    AutoBetMessage = "Open250_Line18_3"
                });

                DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                {
                    Id = Guid.NewGuid(),
                    MatchId = matchs[i].MatchId,
                    LivePeriod = 1,
                    Hdp = 125,
                    Price = 95,
                    Stake = DataStore.AutobetStake,
                    IsOver = true,
                    IsFulltime = true,
                    TotalScore = 1,
                    TotalRed = 10,
                    CreateDateTime = DateTime.Now,
                    MoneyLine = matchs[i].OverUnderMoneyLine,
                    AutoBetMessage = "Open250_Line18_3"
                });

                DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                {
                    Id = Guid.NewGuid(),
                    MatchId = matchs[i].MatchId,
                    LivePeriod = 1,
                    Hdp = 50,
                    Price = 95,
                    Stake = DataStore.AutobetStake,
                    IsOver = true,
                    IsFulltime = false,
                    TotalScore = 1,
                    TotalRed = 10,
                    CreateDateTime = DateTime.Now,
                    MoneyLine = matchs[i].OverUnderMoneyLine,
                    AutoBetMessage = "Open250_Line18_3"
                });
            }
        }

        private void UpdateBet45(List<Match> matchs)
        {

            for (int i = 0; i < matchs.Count; i++)
            {
                int firstGoalMinute = (int)DataStore.GoalHistories[matchs[i].MatchId][0].TimeSpanFromStart.TotalMinutes;
                List<string> priceOverFirstHalf = OverPriceFirstHalfAtMinute(matchs[i].MatchId, firstGoalMinute);
                if (firstGoalMinute > 7
                    || DataStore.OverUnderScoreTimes[matchs[i].MatchId][225][firstGoalMinute] == "0"
                    //|| DataStore.OverUnderScoreTimes[matchs[i].MatchId][225][firstGoalMinute].Contains("-")
                    || DataStore.OverUnderScoreTimes[matchs[i].MatchId][250][firstGoalMinute] == "0"
                    //|| DataStore.OverUnderScoreTimes[matchs[i].MatchId][250][firstGoalMinute].Contains("-")
                    //|| priceOverFirstHalf.Any(item => item.Contains("-")
                    || _bettedMatchId_45.Contains(matchs[i].MatchId)
                )    
                {
                    continue;
                }

                int openPriceOver225 = OpenPriceOver(matchs[i].MatchId, 225);
                if (openPriceOver225 < 61 || openPriceOver225 > 75)
                {
                    continue;
                }

                _bettedMatchId_45.Add(matchs[i].MatchId);

                DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                {
                    Id = Guid.NewGuid(),
                    MatchId = matchs[i].MatchId,
                    LivePeriod = 1,
                    Hdp = 75,
                    Price = 50,
                    Stake = DataStore.AutobetStake,
                    IsOver = true,
                    IsFulltime = false,
                    TotalScore = 1,
                    TotalRed = 10,
                    CreateDateTime = DateTime.Now,
                    MoneyLine = matchs[i].OverUnderMoneyLine,
                    AutoBetMessage = "Open250_Line18_45"
                });

            }
        }

        private List<string> OverPriceFirstHalfAtMinute(long matchId, int minute)
        {
            List<int> hdps = DataStore.OverUnderScoreTimesFirstHalf[matchId].Keys.ToList();
            List<string> result = new List<string>();
            if (minute <= 45)
            {
                for (int i = 0; i < hdps.Count; i++)
                {
                    if (DataStore.OverUnderScoreTimesFirstHalf[matchId][hdps[i]][minute] != "0")
                    {
                        result.Add(DataStore.OverUnderScoreTimesFirstHalf[matchId][hdps[i]][minute]);
                    }
                }
            }
            return result;
        }

        private int OpenPriceOver(long matchId, int key)
        {
            string itemStr = DataStore.OverUnderScoreTimes[matchId][key].FirstOrDefault(item => item != "0");
            if (itemStr != null)
            {
                return int.Parse(itemStr);
            }
            return 0;
        }

        private int OpenMinuteOver(long matchId, int key)
        {
            List<string> row = DataStore.OverUnderScoreTimes[matchId][key];
            string itemStr = row.FirstOrDefault(item => item != "0");
            if (itemStr != null)
            {
                for (int i = 1; i <= 45; i++ )
                {
                    if (row[i] == itemStr)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

    }
}
