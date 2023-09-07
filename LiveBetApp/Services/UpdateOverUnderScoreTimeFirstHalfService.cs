using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services
{
    public class UpdateOverUnderScoreTimeFirstHalfService
    {
        private void ExecuteCore(int milisecond)
        {
            while (true)
            {
                try
                {
                    List<Match> matchs = DataStore.Matchs.Values.Where(item =>
                        !DataStore.Blacklist.Any(p => p.IsActive && p.League == item.League) &&
                        (item.LivePeriod >= 0 || item.IsHT == true)).ToList();
                    List<Product> productsOfMatch;
                    for (int i = 0; i < matchs.Count; i++)
                    {
                        productsOfMatch = DataStore.Products.Values.Where(item => item.MatchId == matchs[i].MatchId && item.Bettype == Enums.BetType.FirstHalfOverUnder).ToList();
                        ProcessOverUnderScoreFirstHalf(matchs[i], productsOfMatch);
                        ProcessOverUnderScoreFirstHalfV2(matchs[i], productsOfMatch);
                        ProcessOverUnderScoreFirstHalfV3(matchs[i], productsOfMatch);
                    }
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

        private void ProcessOverUnderScoreFirstHalf(Match match, List<Product> products)
        {
            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
            if (match.LivePeriod == 0 && !match.IsHT)
            {
                currentMinute = 0;
                //if (DataStore.OverUnderScoreTimes.ContainsKey(match.MatchId))
                //{
                //    DataStore.OverUnderScoreTimes.Remove(match.MatchId);
                //    DataStore.OverUnderLastBoundariesOfMatch.Remove(match.MatchId);
                //}
            }

            if (match.IsHT) return;


            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;

            if (!DataStore.OverUnderScoreTimesFirstHalf.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreTimesFirstHalf.Add(match.MatchId, new Dictionary<int, List<string>>());
                DataStore.UnderScoreTimesFirstHalf.Add(match.MatchId, new Dictionary<int, List<string>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2) - totalLiveScore) * 100);

                if (!DataStore.OverUnderScoreTimesFirstHalf[match.MatchId].ContainsKey(score))
                {
                    DataStore.OverUnderScoreTimesFirstHalf[match.MatchId].Add(score, new List<string>());
                    DataStore.UnderScoreTimesFirstHalf[match.MatchId].Add(score, new List<string>());
                    for (int j = 0; j <= 45; j++)
                    {
                        DataStore.OverUnderScoreTimesFirstHalf[match.MatchId][score].Add("");
                        DataStore.UnderScoreTimesFirstHalf[match.MatchId][score].Add("");
                    }
                }
                if (currentMinute <= 45 && currentMinute >= 0)
                {
                    DataStore.OverUnderScoreTimesFirstHalf[match.MatchId][score][currentMinute] = products[i].Odds1a100.ToString();
                    DataStore.UnderScoreTimesFirstHalf[match.MatchId][score][currentMinute] = products[i].Odds2a100.ToString();
                }
            }
        }

        private void ProcessOverUnderScoreFirstHalfV2(Match match, List<Product> products)
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

            if (!DataStore.OverUnderScoreTimesFirstHalfV2.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreTimesFirstHalfV2.Add(match.MatchId, new Dictionary<int, List<OverUnderScoreTimesV2Item>>());
            }


            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2)) * 100);
                if (!DataStore.OverUnderScoreTimesFirstHalfV2[match.MatchId].ContainsKey(score))
                {
                    DataStore.OverUnderScoreTimesFirstHalfV2[match.MatchId].Add(score, new List<OverUnderScoreTimesV2Item>());
                    for (int j = 0; j <= 91; j++)
                    {
                        DataStore.OverUnderScoreTimesFirstHalfV2[match.MatchId][score].Add(new OverUnderScoreTimesV2Item()
                        {
                            Over = 0,
                            Under = 0
                        });
                    }
                }
                if (currentMinute >= 0 && currentMinute <= 90)
                {
                    DataStore.OverUnderScoreTimesFirstHalfV2[match.MatchId][score][currentMinute] = new OverUnderScoreTimesV2Item()
                    {
                        OddsId = products[i].OddsId,
                        Over = products[i].Odds1a100,
                        Under = products[i].Odds2a100
                    };
                }
            }
        }

        private void ProcessOverUnderScoreFirstHalfV3(Match match, List<Product> products)
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

            if (!DataStore.OverUnderScoreTimesFirstHalfV3.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreTimesFirstHalfV3.Add(match.MatchId, new Dictionary<int, List<OverUnderScoreTimesV2Item>>());
            }

            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;

            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2) - totalLiveScore) * 100);
                if (!DataStore.OverUnderScoreTimesFirstHalfV3[match.MatchId].ContainsKey(score))
                {
                    DataStore.OverUnderScoreTimesFirstHalfV3[match.MatchId].Add(score, new List<OverUnderScoreTimesV2Item>());
                    for (int j = 0; j <= 91; j++)
                    {
                        DataStore.OverUnderScoreTimesFirstHalfV3[match.MatchId][score].Add(new OverUnderScoreTimesV2Item()
                        {
                            Over = 0,
                            Under = 0
                        });
                    }
                }
                if (currentMinute >= 0 && currentMinute <= 90)
                {
                    DataStore.OverUnderScoreTimesFirstHalfV3[match.MatchId][score][currentMinute] = new OverUnderScoreTimesV2Item()
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
