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
    public class BetInUnderOutOverService
    {

        private void ExecuteCore(int milisecond)
        {
            while (true)
            {
                try
                {
                    List<Match> matchs = DataStore.Matchs.Values
                        .Where(item => item.LivePeriod == 1 
                            && !item.IsHT 
                            && item.IsMainMarket
                            && item.TimeSpanFromStart.TotalMinutes <= 5
                            && !item.League.Contains(" - ")
                            && !item.League.ToUpper().Contains("SABA")
                            && !item.Home.ToLower().Contains("(pen)")
                        )
                        .ToList();
                    for (int i = 0; i < matchs.Count; i++)
                    {
                        ProcessMatch(matchs[i]);
                    }
                }
                catch
                {

                }
                finally
                {
                    Thread.Sleep(milisecond);
                    Common.Functions.WriteJsonObjectData(
                        Constants.BackUpPath_InUnderOutOvers,
                        DataStore.InUnderOutOvers
                    );
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

        private void ProcessMatch(Match match)
        {
            if (!DataStore.InUnderOutOvers.ContainsKey(match.MatchId))
            {
                DataStore.InUnderOutOvers.Add(match.MatchId, new InUnderOutOver()
                {
                    MatchId = match.MatchId,
                    UnderHdp = 0,
                    Over050Message = "",
                    Over075Message = "",
                    Over100Message = ""
                });
            }

            InUnderOutOver iuoo = DataStore.InUnderOutOvers[match.MatchId];

            if (iuoo.UnderHdp == 0)
            {
                int hdpHasGoodPrice = FindHdpHasGoodUnderPrice(match);
                if (hdpHasGoodPrice > 0 && hdpHasGoodPrice > 100 && DataStore.BetByHdpPrice.Count == 0)
                {
                    DataStore.BetByHdpPrice.Add(new BetByHdpPrice() {
                        Id = Guid.NewGuid(),
                        CreateDateTime = DateTime.Now,
                        Hdp = hdpHasGoodPrice,
                        IsFulltime = false,
                        IsOver = false,
                        //LivePeriod = 1,
                        MatchId = match.MatchId,
                        MoneyLine = match.OverUnderMoneyLine,
                        Price = -50,
                        Stake = 9,
                        ResultMessage = "decoy_under",
                        TotalRed = 100,
                        TotalScore = 100
                    });

                    DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                    {
                        Id = Guid.NewGuid(),
                        CreateDateTime = DateTime.Now,
                        Hdp = 100,
                        IsFulltime = false,
                        IsOver = true,
                        //LivePeriod = 1,
                        MatchId = match.MatchId,
                        MoneyLine = match.OverUnderMoneyLine,
                        Price = 100,
                        Stake = 3,
                        ResultMessage = "decoy_over_100",
                        TotalRed = 150,
                        TotalScore = 150
                    });

                    DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                    {
                        Id = Guid.NewGuid(),
                        CreateDateTime = DateTime.Now,
                        Hdp = 75,
                        IsFulltime = false,
                        IsOver = true,
                        //LivePeriod = 1,
                        MatchId = match.MatchId,
                        MoneyLine = match.OverUnderMoneyLine,
                        Price = 100,
                        Stake = 3,
                        ResultMessage = "decoy_over_075",
                        TotalRed = 150,
                        TotalScore = 150
                    });

                    DataStore.BetByHdpPrice.Add(new BetByHdpPrice()
                    {
                        Id = Guid.NewGuid(),
                        CreateDateTime = DateTime.Now,
                        Hdp = 50,
                        IsFulltime = false,
                        IsOver = true,
                        //LivePeriod = 1,
                        MatchId = match.MatchId,
                        MoneyLine = match.OverUnderMoneyLine,
                        Price = 100,
                        Stake = 3,
                        ResultMessage = "decoy_over_050",
                        TotalRed = 150,
                        TotalScore = 150
                    });

                    DataStore.InUnderOutOvers[match.MatchId].UnderHdp = hdpHasGoodPrice;
                }
            }

            
        }

        private int FindHdpHasGoodUnderPrice(Match match)
        {
            Product selectedProduct = DataStore.Products.Values.FirstOrDefault(item =>
                item.MatchId == match.MatchId
                && item.Bettype == Enums.BetType.FirstHalfOverUnder
                && item.Odds2a100 < 0
            );

            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;

            if (selectedProduct == null)
            {
                return 0;
            }
            else
            {
                return (int)(selectedProduct.Hdp1 * 100) - totalLiveScore;
            }

        }

    }
}
