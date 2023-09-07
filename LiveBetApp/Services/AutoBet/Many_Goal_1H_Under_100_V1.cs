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
    public class Many_Goal_1H_Under_100_V1
    {

        private List<long> _bettedMatchId;

        public Many_Goal_1H_Under_100_V1()
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
            int numberGoalAsMany = 4;
            List<Match> matchs = DataStore.Matchs.Values.ToList();

            for(int i=0; i<matchs.Count; i++)
            {
                int minute = (int)matchs[i].TimeSpanFromStart.TotalMinutes;
                if (!_bettedMatchId.Contains(matchs[i].MatchId)
                    && matchs[i].LivePeriod == 2
                    && matchs[i].IsMainMarket
                    && minute >= 30)
                {
                    if(DataStore.OverUnderScoreTimes.ContainsKey(matchs[i].MatchId)
                        && DataStore.OverUnderScoreTimes[matchs[i].MatchId].Keys.Contains(50))
                    {
                        int maxkey = DataStore.OverUnderScoreTimes[matchs[i].MatchId].Keys.Max();
                        if (maxkey > 200)
                        {
                            if (DataStore.GoalHistories.ContainsKey(matchs[i].MatchId))
                            {
                                int count = DataStore.GoalHistories[matchs[i].MatchId].Count(item => item.LivePeriod == 1);
                                if (count >= numberGoalAsMany)
                                {
                                    _bettedMatchId.Add(matchs[i].MatchId);
                                    DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                                    {
                                        Id = Guid.NewGuid(),
                                        MatchId = matchs[i].MatchId,
                                        LivePeriod = 2,
                                        Hdp = 50,
                                        Price = 90,
                                        Stake = DataStore.AutobetStake,
                                        IsOver = false,
                                        IsFulltime = true,
                                        TotalScore = 1000,
                                        TotalRed = 1000,
                                        CreateDateTime = DateTime.Now,
                                        MoneyLine = matchs[i].OverUnderMoneyLine,
                                        AutoBetMessage = "Many_Goal_1H_Under_100_V1"
                                    });
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
