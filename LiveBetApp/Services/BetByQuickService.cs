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
    public class BetByQuickService
    {

        private void ExecuteCore(int milisecond)
        {
            if (DataStore.Config == null) return;
            if (DataStore.Config.cookie == null || DataStore.Config.cookie.Length == 0) return;
            if (DataStore.Config.bong88Url == null || DataStore.Config.bong88Url.Length == 0) return;

            while (true)
            {
                try
                {
                    List<BetByQuick> betByQuicks = DataStore.BetByQuick.Where(item => item.Hdp > 0 && item.Planted == false).ToList();
                    for (int i = 0; i < betByQuicks.Count; i++)
                    {
                        ProcessBet(betByQuicks[i]);
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

        private void ProcessBet(BetByQuick betByQuick)
        {
            Dictionary<int, List<string>> data;
            if (!DataStore.OverUnderScoreTimesFirstHalf.TryGetValue(betByQuick.MatchId, out data)) return;
            if (!data.ContainsKey(betByQuick.Hdp)) return;

            if (data[betByQuick.Hdp].Count(item => item.Contains("-")) >= betByQuick.Minute
                && DataStore.Matchs.ContainsKey(betByQuick.MatchId))
            {
                Match match = DataStore.Matchs[betByQuick.MatchId];
                DataStore.BetByHdpPrice.Add(new BetByHdpPrice() { 
                    Id = Guid.NewGuid(),
                    CreateDateTime = DateTime.Now,
                    Hdp = betByQuick.Hdp,
                    IsFulltime = false,
                    IsOver = true,
                    //LivePeriod = 1,
                    MatchId = betByQuick.MatchId,
                    MoneyLine = match.OverUnderMoneyLine,
                    Price = 100,
                    Stake = betByQuick.Stake,
                    ResultMessage = "QuickBet",
                    TotalRed = 100,
                    TotalScore = betByQuick.TotalGoal,
                    CareGoal = betByQuick.CareGoal
                });
                for (int i = 0; i < DataStore.BetByQuick.Count; i++)
                {
                    if (DataStore.BetByQuick[i].Id == betByQuick.Id)
                    {
                        DataStore.BetByQuick[i].Planted = true;
                        break;
                    }
                }
            }

        }
    }
}
