using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace LiveBetApp.Services
{
    public class UpdateMaxBetNoneRequestService
    {
        private void ExecuteCore(int milisecond)
        {

            while (true)
            {
                try
                {
                    List<Match> matchs = DataStore.Matchs.Values.Where(item =>
                        !DataStore.Blacklist.Any(p => p.IsActive && p.League == item.League) &&
                        (item.LivePeriod == 1 || item.LivePeriod == 2)
                    ).ToList();
                    List<Product> productsOfMatch;
                    //List<Product> productOuFhsOfMatch;
                    for (int i = 0; i < matchs.Count; i++)
                    {
                        productsOfMatch = DataStore.Products.Values
                            .Where(item => item.MatchId == matchs[i].MatchId && item.Bettype == Enums.BetType.FullTimeOverUnder)
                            .OrderByDescending(item => item.Hdp1)
                            .ToList();

                        //productOuFhsOfMatch = DataStore.Products.Values
                        //    .Where(item => item.MatchId == matchs[i].MatchId && item.Bettype == Enums.BetType.FirstHalfOverUnder)
                        //    .OrderByDescending(item => item.Hdp1)
                        //    .ToList();

                        if (!DataStore.MatchMaxBetNonRequest.ContainsKey(matchs[i].MatchId))
                        {
                            List<List<int>> dataH0 = new List<List<int>>();
                            List<List<int>> dataH1 = new List<List<int>>();
                            List<List<int>> dataH2 = new List<List<int>>();

                            List<int> dataH1Row1 = new List<int>();
                            List<int> dataH1Row2 = new List<int>();
                            List<int> dataH2Row1 = new List<int>();
                            List<int> dataH2Row2 = new List<int>();

                            for (int j = 0; j <= 100; j++)
                            {
                                dataH1Row1.Add(0);
                                dataH1Row2.Add(0);
                                dataH2Row1.Add(0);
                                dataH2Row2.Add(0);
                            }

                            dataH1.Add(dataH1Row1);
                            dataH1.Add(dataH1Row2);
                            dataH2.Add(dataH2Row1);
                            dataH2.Add(dataH2Row2);

                            DataStore.MatchMaxBetNonRequest.Add(matchs[i].MatchId, new List<List<List<int>>>());
                            DataStore.MatchMaxBetNonRequest[matchs[i].MatchId].Add(dataH0);
                            DataStore.MatchMaxBetNonRequest[matchs[i].MatchId].Add(dataH1);
                            DataStore.MatchMaxBetNonRequest[matchs[i].MatchId].Add(dataH2);
                        }

                        int currentMinute = (int)matchs[i].TimeSpanFromStart.TotalMinutes;
                        if (currentMinute <= 0) currentMinute = 0;
                        if (currentMinute >= 55) currentMinute = 55;
                        if (matchs[i].LivePeriod == 2) currentMinute += 45;

                        if (currentMinute == 0) continue;

                        int currentMaxBet = 0;
                        int previousMaxBet = 0;

                        if (matchs[i].LivePeriod == 1)
                        {
                            if (productsOfMatch.Count >= 2)
                            {
                                currentMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute];
                                previousMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute - 1];
                                if (currentMaxBet == previousMaxBet || currentMaxBet == 0)
                                {
                                    DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute] = productsOfMatch[0].MaxBet;
                                }

                                currentMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][1][currentMinute];
                                previousMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][1][currentMinute - 1];
                                if (currentMaxBet == previousMaxBet || currentMaxBet == 0)
                                {
                                    DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][1][currentMinute] = productsOfMatch[1].MaxBet;
                                }

                                continue;
                            }
                            else if (productsOfMatch.Count == 1)
                            {
                                currentMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute];
                                previousMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute - 1];
                                if (currentMaxBet == previousMaxBet || currentMaxBet == 0)
                                {
                                    DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute] = productsOfMatch[0].MaxBet;
                                }

                                continue;
                            }
                        }

                        if (matchs[i].LivePeriod == 2)
                        {
                            if (productsOfMatch.Count >= 2)
                            {
                                currentMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute];
                                previousMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute - 1];
                                if (currentMaxBet == previousMaxBet || currentMaxBet == 0)
                                {
                                    DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute] = productsOfMatch[0].MaxBet;
                                }

                                currentMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][1][currentMinute];
                                previousMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][1][currentMinute - 1];
                                if (currentMaxBet == previousMaxBet || currentMaxBet == 0)
                                {
                                    DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][1][currentMinute] = productsOfMatch[1].MaxBet;
                                }

                                
                                continue;
                            }
                            else if (productsOfMatch.Count == 1)
                            {
                                currentMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute];
                                previousMaxBet = DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute - 1];
                                if (currentMaxBet == previousMaxBet || currentMaxBet == 0)
                                {
                                    DataStore.MatchMaxBetNonRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute] = productsOfMatch[0].MaxBet;
                                }
                                continue;
                            }
                        }

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


    }
}
