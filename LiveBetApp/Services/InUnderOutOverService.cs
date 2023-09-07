//using LiveBetApp.Models.DataModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace LiveBetApp.Services
//{
//    public class InUnderOutOverService
//    {
//        public InUnderOutOverService()
//        {

//        }

//        private void ExecuteCore(int milisecond)
//        {

//            while (true)
//            {
//                try
//                {
//                    PlanInUnder();
//                    PlaceBet();
//                }
//                catch
//                {

//                }
//                finally
//                {
//                    Thread.Sleep(milisecond);
//                }

//            }
//        }

//        public void Execute(int milisecond)
//        {
//            Thread coreServiceThread;
//            coreServiceThread = new Thread(() =>
//            {
//                ExecuteCore(milisecond);
//            });
//            coreServiceThread.IsBackground = true;
//            coreServiceThread.Start();
//        }

//        private void PlanInUnder()
//        {
//            List<Match> matchs = DataStore.Matchs.Values
//                .Where(item => 
//                    Math.Abs(item.GlobalShowtime.Subtract(DateTime.Now).TotalMinutes) <= 5
//                    //&& item.MaxHdp == 325
//                    )
//                .ToList();

//            if (DataStore.InUnderOutOvers.Count > 0) return;

//            for (int i = 0; i < matchs.Count; i++)
//            {
//                long matchId = matchs[i].MatchId;

//                if (!DataStore.InUnderOutOvers.ContainsKey(matchId))
//                {
//                    DataStore.InUnderOutOvers.Add(matchId, new Dictionary<int, InUnderOutOverPrice>());
//                }

//                List<Product> products = DataStore.Products.Values
//                    .Where(item => 
//                        item.MatchId == matchId 
//                        && item.Bettype == Common.Enums.BetType.FullTimeOverUnder)
//                    .ToList();

//                for (int j = 0; j < products.Count; j++)
//                {
//                    int hdp = (int)Math.Round(products[j].Hdp1 * 100);
//                    if (!DataStore.InUnderOutOvers[matchId].ContainsKey(hdp)
//                        && hdp % 50 == 0
//                        && DataStore.InUnderOutOvers[matchId].Count == 0
//                        )
//                    {
//                        DataStore.InUnderOutOvers[matchId].Add(hdp, new InUnderOutOverPrice()
//                        {
//                            ActualBetPriceOver = 0,
//                            ActualBetPriceUnder = 0
//                        });
//                    }
//                }
//                break;
//            }
//        }

//        private int CalculateOverPrice(int underPrice)
//        {
//            if (underPrice < 0)
//            {
//                return 100;
//            }
//            else
//            {
//                return -underPrice;
//            }
//        }

//        private void PlaceBet()
//        {
//            List<long> matchIds = DataStore.InUnderOutOvers.Keys.ToList();
//            for (int i = 0; i < matchIds.Count; i++)
//            {
//                Match match = DataStore.Matchs[matchIds[i]];
//                List<int> hdps = DataStore.InUnderOutOvers[matchIds[i]].Keys.ToList();
//                for (int j = 0; j < hdps.Count; j++)
//                {
//                    int actualBetPriceUnder = DataStore.InUnderOutOvers[matchIds[i]][hdps[j]].ActualBetPriceUnder;
//                    int actualBetPriceOver = DataStore.InUnderOutOvers[matchIds[i]][hdps[j]].ActualBetPriceOver;

//                    if (actualBetPriceUnder == 0)
//                    {
//                        DataStore.BetByHdpPriceIuoo.Add(new Models.DataModels.BetByHdpPrice()
//                        {
//                            Id = Guid.NewGuid(),
//                            MatchId = matchIds[i],
//                            LivePeriod = 1,
//                            Hdp = hdps[j],
//                            Price = -50,
//                            Stake = DataStore.AutobetStake,
//                            IsOver = false,
//                            IsFulltime = true,
//                            TotalScore = 0,
//                            TotalRed = 0,
//                            MoneyLine = match.OverUnderMoneyLine,
//                            CreateDateTime = DateTime.Now
//                        });

//                        DataStore.InUnderOutOvers[matchIds[i]][hdps[j]].ActualBetPriceUnder = -1;
//                    }

//                    // under is betted
//                    // over is not
//                    if (actualBetPriceUnder != -1 && actualBetPriceUnder != 0 && actualBetPriceOver == 0) 
//                    {
//                        DataStore.BetByHdpPriceIuoo.Add(new Models.DataModels.BetByHdpPrice()
//                        {
//                            Id = Guid.NewGuid(),
//                            MatchId = matchIds[i],
//                            LivePeriod = 1,
//                            Hdp = hdps[j],
//                            Price = CalculateOverPrice(actualBetPriceUnder),
//                            Stake = DataStore.AutobetStake,
//                            IsOver = true,
//                            IsFulltime = true,
//                            TotalScore = 0,
//                            TotalRed = 0,
//                            MoneyLine = match.OverUnderMoneyLine,
//                            CreateDateTime = DateTime.Now
//                        });

//                        DataStore.InUnderOutOvers[matchIds[i]][hdps[j]].ActualBetPriceOver = -1;
//                    }

//                }
//            }
//        }

//    }
//}
