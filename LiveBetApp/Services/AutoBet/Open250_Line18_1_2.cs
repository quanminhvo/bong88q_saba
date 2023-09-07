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
    public class Open250_Line18_1_2
    {

        private List<long> _bettedMatchId;

        public Open250_Line18_1_2()
        {
            _bettedMatchId = new List<long>();
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
                    && item.LivePeriod == 1
                    && (item.LiveHomeScore + item.LiveAwayScore) == 1
                    && !_bettedMatchId.Contains(item.MatchId)
                    && DataStore.GoalHistories.ContainsKey(item.MatchId)
                    && DataStore.GoalHistories[item.MatchId].Count > 0
                    && DataStore.OverUnderScoreTimes.ContainsKey(item.MatchId)
                    && DataStore.OverUnderScoreTimes[item.MatchId].ContainsKey(150)
                    && DataStore.OverUnderScoreTimes[item.MatchId].ContainsKey(125)
                    && DataStore.OverUnderScoreTimes[item.MatchId].Keys.Max() == 250
                ).ToList();

            for(int i=0; i<matchs.Count; i++)
            {
                int firstGoalMinute = (int)DataStore.GoalHistories[matchs[i].MatchId][0].TimeSpanFromStart.TotalMinutes;
                if ( DataStore.OverUnderScoreTimes[matchs[i].MatchId][125][firstGoalMinute] == "0"
                    || DataStore.OverUnderScoreTimes[matchs[i].MatchId][150][firstGoalMinute] == "0")
                {
                    continue;
                }


                int priceOver125AtGoal = PriceAtMinute(matchs[i].MatchId, 125, firstGoalMinute);
                if (priceOver125AtGoal < 0 || priceOver125AtGoal > 68)
                {
                    continue;
                }

                _bettedMatchId.Add(matchs[i].MatchId);

                if (firstGoalMinute < 27)
                {
                    DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                    {
                        Id = Guid.NewGuid(),
                        MatchId = matchs[i].MatchId,
                        LivePeriod = 1,
                        Hdp = 150,
                        Price = 50,
                        Stake = DataStore.AutobetStake,
                        IsOver = true,
                        IsFulltime = true,
                        TotalScore = 1,
                        TotalRed = 10,
                        CreateDateTime = DateTime.Now,
                        MoneyLine = matchs[i].OverUnderMoneyLine,
                        AutoBetMessage = "Open250_Line18_1"
                    });
                }
                else if (firstGoalMinute > 27 && firstGoalMinute < 50)
                {
                    DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                    {
                        Id = Guid.NewGuid(),
                        MatchId = matchs[i].MatchId,
                        LivePeriod = 1,
                        Hdp = 125,
                        Price = 50,
                        Stake = DataStore.AutobetStake,
                        IsOver = false,
                        IsFulltime = true,
                        TotalScore = 1,
                        TotalRed = 10,
                        CreateDateTime = DateTime.Now,
                        MoneyLine = matchs[i].OverUnderMoneyLine,
                        AutoBetMessage = "Open250_Line18_2"
                    });
                }
            }
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

        private int PriceAtMinute(long matchId, int key, int minute)
        {
            string itemStr = DataStore.OverUnderScoreTimes[matchId][key][minute];
            return int.Parse(itemStr);
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
