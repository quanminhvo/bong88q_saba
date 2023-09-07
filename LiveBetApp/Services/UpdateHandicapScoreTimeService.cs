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
    public class UpdateHandicapScoreTimeService
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
                    List<Product> productsFhOfMatch;
                    for (int i = 0; i < matchs.Count; i++)
                    {
                        productsOfMatch = DataStore.Products.Values.Where(item => item.MatchId == matchs[i].MatchId && item.Bettype == Enums.BetType.FullTimeHandicap).ToList();
                        productsFhOfMatch = DataStore.Products.Values.Where(item => item.MatchId == matchs[i].MatchId && item.Bettype == Enums.BetType.FirstHalfHandicap).ToList();

                        ProcessHandicapScoreTime(matchs[i], productsOfMatch);
                        ProcessHandicapFhScoreTime(matchs[i], productsFhOfMatch);
                        ProcessHandicapScoreTimeBeforeLive(matchs[i], productsOfMatch);
                        ProcessHandicapScoreTimeFhBeforeLive(matchs[i], productsFhOfMatch);
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

        private void ProcessHandicapFhScoreTime(Match match, List<Product> products)
        {
            int currentMinute = Convert.ToInt32(match.TimeSpanFromStart.TotalMinutes);



            if (match.LivePeriod != 1 || match.IsHT)
            {
                return;
            }


            if (match.LivePeriod == 0 && !match.IsHT)
            {
                currentMinute = 0;
            }

            if (!DataStore.HandicapFhScoreTimes.ContainsKey(match.MatchId))
            {
                DataStore.HandicapFhScoreTimes.Add(match.MatchId, new Dictionary<long, List<HandicapLifeTimeHistory>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                HandicapLifeTimeHistory history = new HandicapLifeTimeHistory()
                {
                    OddsId = products[i].OddsId,
                    Odds1a = products[i].Odds1a100,
                    Odds2a = products[i].Odds2a100,
                    Hdp1 = products[i].Hdp1,
                    Hdp2 = products[i].Hdp2,
                    TimeSpanFromStart = match.TimeSpanFromStart
                };

                if (!DataStore.HandicapFhScoreTimes[match.MatchId].ContainsKey(products[i].OddsId))
                {
                    DataStore.HandicapFhScoreTimes[match.MatchId].Add(products[i].OddsId, new List<HandicapLifeTimeHistory>(new HandicapLifeTimeHistory[46]));
                }
                if (currentMinute >= 0 && currentMinute <= 45)
                {
                    DataStore.HandicapFhScoreTimes[match.MatchId][products[i].OddsId][currentMinute] = history;
                }
            }
        }


        private void ProcessHandicapScoreTime(Match match, List<Product> products)
        {
            int currentMinute = Convert.ToInt32(match.TimeSpanFromStart.TotalMinutes);

            if (match.LivePeriod == 2)
            {
                if (currentMinute == 0)
                    currentMinute++;
                currentMinute += 45;
            }

            if (match.LivePeriod == 0 && !match.IsHT)
            {
                currentMinute = 0;
            }

            if ((currentMinute == 0 && match.LivePeriod == 2)
                || (currentMinute == 0 && match.HasHt)
                || match.IsHT)
            {
                return;
            }

            if (!DataStore.HandicapScoreTimes.ContainsKey(match.MatchId))
            {
                DataStore.HandicapScoreTimes.Add(match.MatchId, new Dictionary<long, List<HandicapLifeTimeHistory>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                HandicapLifeTimeHistory history = new HandicapLifeTimeHistory()
                {
                    OddsId = products[i].OddsId,
                    Odds1a = products[i].Odds1a100,
                    Odds2a = products[i].Odds2a100,
                    Hdp1 = products[i].Hdp1,
                    Hdp2 = products[i].Hdp2,
                    TimeSpanFromStart = match.TimeSpanFromStart
                };

                if (!DataStore.HandicapScoreTimes[match.MatchId].ContainsKey(products[i].OddsId))
                {
                    DataStore.HandicapScoreTimes[match.MatchId].Add(products[i].OddsId, new List<HandicapLifeTimeHistory>( new HandicapLifeTimeHistory[101]));
                }
                if (currentMinute >= 0 && currentMinute <= 100)
                {
                    DataStore.HandicapScoreTimes[match.MatchId][products[i].OddsId][currentMinute] = history;
                }
            }
        }

        private void ProcessHandicapScoreTimeBeforeLive(Match match, List<Product> products)
        {

            if (match.LivePeriod != 0) return;
            if (match.IsHT) return;
            int minuteDiff = (int)DateTime.Now.Subtract(match.GlobalShowtime).TotalMinutes;
            if (minuteDiff >= 0) return;

            if (!DataStore.HandicapScoreTimesBeforeLive.ContainsKey(match.MatchId))
            {
                DataStore.HandicapScoreTimesBeforeLive.Add(match.MatchId, new Dictionary<int, List<HandicapLifeTimeHistoryV2>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int hdp = (int)((products[i].Hdp1 + products[i].Hdp2) * 100);
                if (!DataStore.HandicapScoreTimesBeforeLive[match.MatchId].ContainsKey(hdp))
                {
                    List<HandicapLifeTimeHistoryV2> emptyRow = Common.Functions.GetEmptyRowHandicapLifeTime();

                    DataStore.HandicapScoreTimesBeforeLive[match.MatchId].Add(hdp, emptyRow);
                }

                DateTime lastUpdateDt = DataStore.HandicapScoreTimesBeforeLive[match.MatchId][hdp].Min(item => item.RecordedDatetime);
                bool isFirstUpdate = !DataStore.HandicapScoreTimesBeforeLive[match.MatchId][hdp].Any(item => item.RecordedDatetime != DateTime.MinValue);

                for (int j = 0; j < DataStore.HandicapScoreTimesBeforeLive[match.MatchId][hdp].Count; j++)
                {
                    if (DataStore.HandicapScoreTimesBeforeLive[match.MatchId][hdp][j].RecordedDatetime == lastUpdateDt)
                    {
                        if (minuteDiff >= -90)
                        {
                            DataStore.HandicapScoreTimesBeforeLive[match.MatchId][hdp][j] = new HandicapLifeTimeHistoryV2()
                            {
                                OddsId = products[i].OddsId,
                                Hdp1 = products[i].Hdp1,
                                Hdp2 = products[i].Hdp2,
                                Odds1a = products[i].Odds1a * 100,
                                Odds2a = products[i].Odds2a * 100,
                                RecordedDatetime = DateTime.Now
                            };
                        }

                        if (isFirstUpdate)
                        {
                            DataStore.HandicapScoreTimesBeforeLive[match.MatchId][hdp][j] = new HandicapLifeTimeHistoryV2()
                            {
                                OddsId = products[i].OddsId,
                                Hdp1 = products[i].Hdp1,
                                Hdp2 = products[i].Hdp2,
                                Odds1a = products[i].Odds1a * 100,
                                Odds2a = products[i].Odds2a * 100,
                                RecordedDatetime = DateTime.MaxValue
                            };
                        }

                        break;
                    }
                }
            }

        }

        private void ProcessHandicapScoreTimeFhBeforeLive(Match match, List<Product> products)
        {

            if (match.LivePeriod != 0) return;
            if (match.IsHT) return;
            int minuteDiff = (int)DateTime.Now.Subtract(match.GlobalShowtime).TotalMinutes;
            if (minuteDiff >= 0) return;

            if (!DataStore.HandicapScoreTimesFirstHalfBeforeLive.ContainsKey(match.MatchId))
            {
                DataStore.HandicapScoreTimesFirstHalfBeforeLive.Add(match.MatchId, new Dictionary<int, List<HandicapLifeTimeHistoryV2>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int hdp = (int)((products[i].Hdp1 + products[i].Hdp2) * 100);
                if (!DataStore.HandicapScoreTimesFirstHalfBeforeLive[match.MatchId].ContainsKey(hdp))
                {
                    List<HandicapLifeTimeHistoryV2> emptyRow = Common.Functions.GetEmptyRowHandicapLifeTime();

                    DataStore.HandicapScoreTimesFirstHalfBeforeLive[match.MatchId].Add(hdp, emptyRow);
                }

                DateTime lastUpdateDt = DataStore.HandicapScoreTimesFirstHalfBeforeLive[match.MatchId][hdp].Min(item => item.RecordedDatetime);
                bool isFirstUpdate = !DataStore.HandicapScoreTimesFirstHalfBeforeLive[match.MatchId][hdp].Any(item => item.RecordedDatetime != DateTime.MinValue);

                for (int j = 0; j < DataStore.HandicapScoreTimesFirstHalfBeforeLive[match.MatchId][hdp].Count; j++)
                {
                    if (DataStore.HandicapScoreTimesFirstHalfBeforeLive[match.MatchId][hdp][j].RecordedDatetime == lastUpdateDt)
                    {
                        if (minuteDiff >= -90)
                        {
                            DataStore.HandicapScoreTimesFirstHalfBeforeLive[match.MatchId][hdp][j] = new HandicapLifeTimeHistoryV2()
                            {
                                OddsId = products[i].OddsId,
                                Hdp1 = products[i].Hdp1,
                                Hdp2 = products[i].Hdp2,
                                Odds1a = products[i].Odds1a * 100,
                                Odds2a = products[i].Odds2a * 100,
                                RecordedDatetime = DateTime.Now
                            };
                        }

                        if (isFirstUpdate)
                        {
                            DataStore.HandicapScoreTimesFirstHalfBeforeLive[match.MatchId][hdp][j] = new HandicapLifeTimeHistoryV2()
                            {
                                OddsId = products[i].OddsId,
                                Hdp1 = products[i].Hdp1,
                                Hdp2 = products[i].Hdp2,
                                Odds1a = products[i].Odds1a * 100,
                                Odds2a = products[i].Odds2a * 100,
                                RecordedDatetime = DateTime.MaxValue
                            };
                        }

                        break;
                    }
                }
            }
            
        }

    }
}
