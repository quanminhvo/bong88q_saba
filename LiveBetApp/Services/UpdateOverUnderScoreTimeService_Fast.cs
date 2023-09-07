using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiveBetApp.Models.DataModels;
using LiveBetApp.Common;

namespace LiveBetApp.Services
{
    public class UpdateOverUnderScoreTimeService_Fast
    {

        private void ExecuteCore(int milisecond)
        {
            if (DataStore.SemiAutoBetOnly) return;

            while (true)
            {
                try
                {
                    List<Match> matchs = DataStore.Matchs.Values.Where(item => 
                        !DataStore.Blacklist.Any(p => p.IsActive && p.League == item.League) &&
                        (item.LivePeriod >= 0 || item.IsHT == true)).ToList();
                    List<Product> productsOfMatch;
                    List<Product> productOuFhsOfMatch;
                    for (int i = 0; i < matchs.Count; i++)
                    {
                        productsOfMatch = DataStore.Products.Values.Where(item => item.MatchId == matchs[i].MatchId && item.Bettype == Enums.BetType.FullTimeOverUnder).ToList();
                        productOuFhsOfMatch = DataStore.Products.Values.Where(item => item.MatchId == matchs[i].MatchId && item.Bettype == Enums.BetType.FirstHalfOverUnder).ToList();
                        //ProcessOverUnderScore(matchs[i], productsOfMatch);
                        ProcessOverUnderScore_v2(matchs[i], productsOfMatch);
                        ProcessOverUnderScore_v3(matchs[i], productsOfMatch);
                    }
                    //UpdateProductIdHistory();
                    Thread.Sleep(milisecond);
                }
                catch (Exception ex)
                {
                    Common.Functions.WriteExceptionLog(ex);
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


        private void ProcessOverUnderScore_v2(Match match, List<Product> products)
        {
            if (match.IsHT) return;
            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
            if (match.LivePeriod == 0 && !match.IsHT)
            {
                currentMinute = 0;
            }

            if (currentMinute > 45)
            {
                return;
            }

            if (match.LivePeriod == 1)
            {
                currentMinute += 0;
            }
            else if (match.LivePeriod == 2)
            {
                currentMinute += 45;
            }

            //working
            if (currentMinute == 0 && match.HasHt)
            {
                currentMinute = 91;
            }

            if (currentMinute == 0 && match.LivePeriod == 2)
            {
                return;
            }

            if (!DataStore.OverUnderScoreTimesV2.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreTimesV2.Add(match.MatchId, new Dictionary<int, List<OverUnderScoreTimesV2Item>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2)) * 100);
                if (!DataStore.OverUnderScoreTimesV2[match.MatchId].ContainsKey(score))
                {
                    DataStore.OverUnderScoreTimesV2[match.MatchId].Add(score, new List<OverUnderScoreTimesV2Item>());
                    for (int j = 0; j <= 91; j++)
                    {
                        DataStore.OverUnderScoreTimesV2[match.MatchId][score].Add(new OverUnderScoreTimesV2Item()
                        {
                            Over = 0,
                            Under = 0
                        });
                    }
                }
                if (currentMinute >= 0 && currentMinute <= 90)
                {
                    DataStore.OverUnderScoreTimesV2[match.MatchId][score][currentMinute] = new OverUnderScoreTimesV2Item()
                    {
                        Over = products[i].Odds1a100,
                        Under = products[i].Odds2a100
                    };
                }
            }
        }

        private void ProcessOverUnderScore_v3(Match match, List<Product> products)
        {
            if (match.IsHT) return;
            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
            if (match.LivePeriod == 0 && !match.IsHT)
            {
                currentMinute = 0;
            }

            if (currentMinute == 0)
            {

            }
            else if (currentMinute > 45)
            {
                return;
            }

            if (match.LivePeriod == 1)
            {
                currentMinute += 0;
            }
            else if (match.LivePeriod == 2)
            {
                currentMinute += 45;
            }
            else if (match.LivePeriod == 0)
            {
                currentMinute = 0;
            }


            if (currentMinute == 0 && match.HasHt)
            {
                currentMinute = 91;
            }


            if (currentMinute == 0 && match.LivePeriod == 2)
            {
                return;
            }

            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreTimesV3.Add(match.MatchId, new Dictionary<int, List<OverUnderScoreTimesV2Item>>());
            }

            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;

            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2) - totalLiveScore) * 100);
                if (!DataStore.OverUnderScoreTimesV3[match.MatchId].ContainsKey(score))
                {
                    DataStore.OverUnderScoreTimesV3[match.MatchId].Add(score, new List<OverUnderScoreTimesV2Item>());
                    for (int j = 0; j <= 91; j++)
                    {
                        DataStore.OverUnderScoreTimesV3[match.MatchId][score].Add(new OverUnderScoreTimesV2Item()
                        {
                            Over = 0,
                            Under = 0
                        });
                    }
                }
                if (currentMinute >= 0 && currentMinute <= 90)
                {
                    DataStore.OverUnderScoreTimesV3[match.MatchId][score][currentMinute] = new OverUnderScoreTimesV2Item()
                    {
                        OddsId = products[i].OddsId,
                        Over = products[i].Odds1a100,
                        Under = products[i].Odds2a100
                    };
                }
            }
        }



        
    }
}
