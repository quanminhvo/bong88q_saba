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
    public class UpdateOverUnderScoreTimeService
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
                    List<Product> product1x2OfMatch;
                    for (int i = 0; i < matchs.Count; i++)
                    {
                        productsOfMatch = DataStore.Products.Values.Where(item => item.MatchId == matchs[i].MatchId && item.Bettype == Enums.BetType.FullTimeOverUnder).ToList();
                        product1x2OfMatch = DataStore.Products.Values.Where(item => item.MatchId == matchs[i].MatchId && (item.Bettype == Enums.BetType.FirstHalf1x2 || item.Bettype == Enums.BetType.FullTime1x2)).ToList();
                        productOuFhsOfMatch = DataStore.Products.Values.Where(item => item.MatchId == matchs[i].MatchId && item.Bettype == Enums.BetType.FirstHalfOverUnder).ToList();
                        Process1x2Product(matchs[i], product1x2OfMatch);
                        UpdateMinMaxOuFulltimeHdp(matchs[i], productsOfMatch, productOuFhsOfMatch);
                        ProcessOverUnderScore(matchs[i], productsOfMatch);
                        ProcessOverUnderScore_v3(matchs[i], productsOfMatch);
                        ProcessUnderScore(matchs[i], productsOfMatch);
                        ProcessUnderScoreHalfTime(matchs[i], productsOfMatch);
                        ProcessHalfTime(matchs[i], productsOfMatch);
                        ProcessHalfTimeV2(matchs[i], productsOfMatch);
                        UpdateOverUnderFtBeforeLive(matchs[i], productsOfMatch);
                        UpdateTrackOverUnderBeforeLive(matchs[i], productsOfMatch);
                        UpdateOverUnderFhBeforeLive(matchs[i], productOuFhsOfMatch);
                        UpdateVeryFirstProducts(matchs[i]);
                        
                        try
                        {
                            UpdateOverUnderMoneyLine(matchs[i]);
                        }
                        catch (Exception ex)
                        {

                        }
                        


                        try { UpdateIuoo_1(matchs[i], productsOfMatch); }
                        catch { }
                        try { UpdateIuoo_2(matchs[i], productsOfMatch); }
                        catch { }
                        try { UpdateSpecialAlert(matchs[i]); }
                        catch { }
                        try { UpdateAllExcel4th(matchs[i]); } 
                        catch { }

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

        private void UpdateMinMaxOuFulltimeHdp(Match match, List<Product> productOuFts, List<Product> productOuFhs)
        {
            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;
            if (productOuFts != null && productOuFts.Count > 0)
            {
                int minOuFt = productOuFts.Min(item => (int)(((item.Hdp1 + item.Hdp2) - totalLiveScore) * 100));
                int maxOuFt = productOuFts.Max(item => (int)(((item.Hdp1 + item.Hdp2) - totalLiveScore) * 100));
                DataStore.Matchs[match.MatchId].MinHdpFtCurrent = minOuFt;
                DataStore.Matchs[match.MatchId].MaxHdpFtCurrent = maxOuFt;
            }
            else
            {
                DataStore.Matchs[match.MatchId].MaxHdpFtCurrent = 0;
                DataStore.Matchs[match.MatchId].MinHdpFtCurrent = 10000;
            }

            if (productOuFhs != null && productOuFhs.Count > 0)
            {
                int minOuFh = productOuFhs.Min(item => (int)(((item.Hdp1 + item.Hdp2) - totalLiveScore) * 100));
                int maxOuFh = productOuFhs.Max(item => (int)(((item.Hdp1 + item.Hdp2) - totalLiveScore) * 100));
                DataStore.Matchs[match.MatchId].MinHdpFhCurrent = minOuFh;
                DataStore.Matchs[match.MatchId].MaxHdpFhCurrent = maxOuFh;
            }
            else
            {
                DataStore.Matchs[match.MatchId].MaxHdpFhCurrent = 0;
                DataStore.Matchs[match.MatchId].MinHdpFhCurrent = 10000;
            }
        }

        private void UpdateProductIdHistory()
        {
            List<Match> matchs = DataStore.Matchs.Values.Where(item =>
                !DataStore.Blacklist.Any(p => p.IsActive && p.League == item.League) &&
                (item.TimeSpanFromStartGb.TotalMinutes >= 0)).ToList();
            for (int i = 0; i < matchs.Count; i++)
            {
                try { UpdateProductIdsOuFtOfMatchHistoryBeforeLive(matchs[i]); }
                catch { }
                try { UpdateProductIdsOuFtOfMatchHistoryLive(matchs[i]); }
                catch { }

                try { UpdateProductIdsHdpFtOfMatchHistoryBeforeLive(matchs[i]); }
                catch { }
                try { UpdateProductIdsHdpFtOfMatchHistoryLive(matchs[i]); }
                catch { }

                try { UpdateProductIdsOuFhOfMatchHistoryBeforeLive(matchs[i]); }
                catch { }
                try { UpdateProductIdsOuFhOfMatchHistoryLive(matchs[i]); }
                catch { }

                try { UpdateProductIdsHdpFhOfMatchHistoryBeforeLive(matchs[i]); }
                catch { }
                try { UpdateProductIdsHdpFhOfMatchHistoryLive(matchs[i]); }
                catch { }
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

        private void UpdateLastBoundary(long matchId, int newScoreAppear)
        {
            int scoreHasLastBoundary = newScoreAppear + 50;
            if (DataStore.OverUnderScoreTimes[matchId].ContainsKey(scoreHasLastBoundary))
            {
                string boundary = DataStore.OverUnderScoreTimes[matchId][scoreHasLastBoundary].Where(item => item != "").LastOrDefault();
                if (boundary != null)
                {
                    DataStore.OverUnderLastBoundariesOfMatch[matchId][scoreHasLastBoundary] = int.Parse(boundary);
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

            if (!DataStore.OverUnderScoreTimesV4_60.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreTimesV4_60.Add(match.MatchId, new Dictionary<int, List<OverUnderScoreTimesV2Item>>());
            }

            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;

            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2)) * 100);
                if (!DataStore.OverUnderScoreTimesV4_60[match.MatchId].ContainsKey(score))
                {
                    DataStore.OverUnderScoreTimesV4_60[match.MatchId].Add(score, new List<OverUnderScoreTimesV2Item>());
                    for (int j = 0; j <= 91; j++)
                    {
                        DataStore.OverUnderScoreTimesV4_60[match.MatchId][score].Add(new OverUnderScoreTimesV2Item()
                        {
                            Over = 0,
                            Under = 0
                        });
                    }
                }
                if (currentMinute >= 0 && currentMinute <= 90)
                {
                    DataStore.OverUnderScoreTimesV4_60[match.MatchId][score][currentMinute] = new OverUnderScoreTimesV2Item()
                    {
                        OddsId = products[i].OddsId,
                        Over = products[i].Odds1a100,
                        Under = products[i].Odds2a100
                    };
                }
            }
        }

        private void Process1x2Product(Match match, List<Product> products)
        {
            try
            {
                int currentMinute = Common.Functions.GetCurrentMinute(match);

                if (match.LivePeriod == 2)
                {
                    currentMinute -= 45;
                }

                if (!DataStore.Product1x2History.ContainsKey(match.MatchId))
                {
                    List<List<Product>> productTemp = new List<List<Product>>();
                    for (int i = 0; i <= 90; i++)
                    {
                        productTemp.Add(new List<Product>());
                    }
                    DataStore.Product1x2History.Add(match.MatchId, productTemp);
                }

                if (currentMinute < 0 || currentMinute > 90)
                {
                    return;
                }
                DataStore.Product1x2History[match.MatchId][currentMinute] = new List<Product>();
                for (int i = 0; i < products.Count; i++)
                {
                    DataStore.Product1x2History[match.MatchId][currentMinute].Add(new Product()
                    {
                        Bettype = products[i].Bettype,
                        Com1 = products[i].Com1,
                        Com2 = products[i].Com2,
                        Comx = products[i].Comx,
                        Hdp1 = products[i].Hdp1,
                        Hdp2 = products[i].Hdp2,
                        MatchId = match.MatchId,
                        MaxBet = products[i].MaxBet,
                        Odds1a = products[i].Odds1a,
                        Odds2a = products[i].Odds2a,
                        OddsId = products[i].OddsId,
                        OddsStatus = products[i].OddsStatus
                    });
                }
            }
            catch(Exception ex)
            {

            }


        }

        private void ProcessOverUnderScore(Match match, List<Product> products)
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

            if (match.LivePeriod == 2 && currentMinute == 0)
            {
                currentMinute = 1;
            }

            if (match.IsHT) return;

            if (currentMinute == 0 && match.HasHt)
            {
                currentMinute = 91;
            }

            if (currentMinute == 0 && match.LivePeriod == 2)
            {
                return;
            }

            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;

            if (!DataStore.OverUnderScoreTimes.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreTimes.Add(match.MatchId, new Dictionary<int, List<string>>());
                DataStore.OverUnderLastBoundariesOfMatch.Add(match.MatchId, new Dictionary<int, int>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2) - totalLiveScore) * 100);
                int scoreV4 = (int)(((products[i].Hdp1 + products[i].Hdp2)) * 100);
                if (!DataStore.OverUnderScoreTimes[match.MatchId].ContainsKey(score))
                {
                    DataStore.OverUnderScoreTimes[match.MatchId].Add(score, new List<string>());

                    try { UpdateLastBoundary(match.MatchId, score); }
                    catch (Exception ex) { Common.Functions.WriteExceptionLog(ex); }

                    for (int j = 0; j <= 45; j++)
                    {
                        DataStore.OverUnderScoreTimes[match.MatchId][score].Add("");
                    }
                }

                if (currentMinute <= 45 && currentMinute >= 0)
                {
                    DataStore.OverUnderScoreTimes[match.MatchId][score][currentMinute] = products[i].Odds1a100.ToString();
                }
            }
        }

        public void UpdateProductIdsOuFtOfMatchHistoryBeforeLive(Match match)
        {
            int minute = (int)match.TimeSpanFromStartGb.TotalMinutes;
            if (minute < 0)
            {
                if (!DataStore.ProductIdsOuFtOfMatchHistoryBeforeLive.ContainsKey(match.MatchId))
                {
                    DataStore.ProductIdsOuFtOfMatchHistoryBeforeLive.Add(match.MatchId, new List<ProductIdsHistory>());
                }
                if (DataStore.ProductIdsOfMatch.ContainsKey(match.MatchId))
                {
                    int count = DataStore.ProductIdsOfMatch[match.MatchId].Count(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder);

                    if (DataStore.ProductIdsOuFtOfMatchHistoryBeforeLive[match.MatchId].Count == 0)
                    {
                        DataStore.ProductIdsOuFtOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
                    }

                    if (DataStore.ProductIdsOuFtOfMatchHistoryBeforeLive[match.MatchId].LastOrDefault().Count != count)
                    {
                        DataStore.ProductIdsOuFtOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
                    }
                }
            }
        }
        public void UpdateProductIdsOuFtOfMatchHistoryLive(Match match)
        {
            int minute = (int)match.TimeSpanFromStartGb.TotalMinutes;
            if (minute >= 0)
            {
                if (!DataStore.ProductIdsOuFtOfMatchHistoryLive.ContainsKey(match.MatchId))
                {
                    DataStore.ProductIdsOuFtOfMatchHistoryLive.Add(match.MatchId, new List<ProductIdsHistory>());
                }
                if (DataStore.ProductIdsOfMatch.ContainsKey(match.MatchId))
                {
                    int count = DataStore.ProductIdsOfMatch[match.MatchId].Count(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder);

                    if (DataStore.ProductIdsOuFtOfMatchHistoryLive[match.MatchId].Count == 0)
                    {
                        DataStore.ProductIdsOuFtOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                        DataStore.Matchs[match.MatchId].LastUpdatePhOuFtLive = DateTime.Now;
                    }

                    if (DataStore.ProductIdsOuFtOfMatchHistoryLive[match.MatchId].LastOrDefault().Count != count)
                    {
                        DataStore.ProductIdsOuFtOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                        DataStore.Matchs[match.MatchId].LastUpdatePhOuFtLive = DateTime.Now;
                    }
                }
            }
        }


        public void UpdateProductIdsHdpFtOfMatchHistoryBeforeLive(Match match)
        {
            int minute = (int)match.TimeSpanFromStartGb.TotalMinutes;
            if (minute < 0)
            {
                if (!DataStore.ProductIdsHdpFtOfMatchHistoryBeforeLive.ContainsKey(match.MatchId))
                {
                    DataStore.ProductIdsHdpFtOfMatchHistoryBeforeLive.Add(match.MatchId, new List<ProductIdsHistory>());
                }
                if (DataStore.ProductIdsOfMatch.ContainsKey(match.MatchId))
                {
                    int count = DataStore.ProductIdsOfMatch[match.MatchId].Count(item => item.ProductBetType == Enums.BetType.FullTimeHandicap);
                    if (DataStore.ProductIdsHdpFtOfMatchHistoryBeforeLive[match.MatchId].Count == 0)
                    {
                        DataStore.ProductIdsHdpFtOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
                    }

                    if (DataStore.ProductIdsHdpFtOfMatchHistoryBeforeLive[match.MatchId].LastOrDefault().Count != count)
                    {
                        DataStore.ProductIdsHdpFtOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
                    }
                }
            }
        }
        public void UpdateProductIdsHdpFtOfMatchHistoryLive(Match match)
        {
            int minute = (int)match.TimeSpanFromStartGb.TotalMinutes;
            if (minute >= 0)
            {
                if (!DataStore.ProductIdsHdpFtOfMatchHistoryLive.ContainsKey(match.MatchId))
                {
                    DataStore.ProductIdsHdpFtOfMatchHistoryLive.Add(match.MatchId, new List<ProductIdsHistory>());
                }
                if (DataStore.ProductIdsOfMatch.ContainsKey(match.MatchId))
                {
                    int count = DataStore.ProductIdsOfMatch[match.MatchId].Count(item => item.ProductBetType == Enums.BetType.FullTimeHandicap);

                    if (DataStore.ProductIdsHdpFtOfMatchHistoryLive[match.MatchId].Count == 0)
                    {
                        DataStore.ProductIdsHdpFtOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                        DataStore.Matchs[match.MatchId].LastUpdatePhHdpFtLive = DateTime.Now;
                    }

                    if (DataStore.ProductIdsHdpFtOfMatchHistoryLive[match.MatchId].LastOrDefault().Count != count)
                    {
                        DataStore.ProductIdsHdpFtOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                        DataStore.Matchs[match.MatchId].LastUpdatePhHdpFtLive = DateTime.Now;
                    }
                }
            }
        }


        public void UpdateProductIdsOuFhOfMatchHistoryBeforeLive(Match match)
        {
            int minute = (int)match.TimeSpanFromStartGb.TotalMinutes;
            if (minute < 0)
            {
                if (!DataStore.ProductIdsOuFhOfMatchHistoryBeforeLive.ContainsKey(match.MatchId))
                {
                    DataStore.ProductIdsOuFhOfMatchHistoryBeforeLive.Add(match.MatchId, new List<ProductIdsHistory>());
                }
                if (DataStore.ProductIdsOfMatch.ContainsKey(match.MatchId))
                {
                    int count = DataStore.ProductIdsOfMatch[match.MatchId].Count(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder);

                    if (DataStore.ProductIdsOuFhOfMatchHistoryBeforeLive[match.MatchId].Count == 0)
                    {
                        DataStore.ProductIdsOuFhOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
                    }

                    if (DataStore.ProductIdsOuFhOfMatchHistoryBeforeLive[match.MatchId].LastOrDefault().Count != count)
                    {
                        DataStore.ProductIdsOuFhOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
                    }
                }
            }
        }
        public void UpdateProductIdsOuFhOfMatchHistoryLive(Match match)
        {
            int minute = (int)match.TimeSpanFromStartGb.TotalMinutes;
            if (minute >= 0)
            {
                if (!DataStore.ProductIdsOuFhOfMatchHistoryLive.ContainsKey(match.MatchId))
                {
                    DataStore.ProductIdsOuFhOfMatchHistoryLive.Add(match.MatchId, new List<ProductIdsHistory>());
                }
                if (DataStore.ProductIdsOfMatch.ContainsKey(match.MatchId))
                {
                    int count = DataStore.ProductIdsOfMatch[match.MatchId].Count(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder);

                    if (DataStore.ProductIdsOuFhOfMatchHistoryLive[match.MatchId].Count == 0)
                    {
                        DataStore.ProductIdsOuFhOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                        DataStore.Matchs[match.MatchId].LastUpdatePhOuFhLive = DateTime.Now;
                    }

                    if (DataStore.ProductIdsOuFhOfMatchHistoryLive[match.MatchId].LastOrDefault().Count != count)
                    {
                        DataStore.ProductIdsOuFhOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                        DataStore.Matchs[match.MatchId].LastUpdatePhOuFhLive = DateTime.Now;
                    }
                }
            }
        }

        public void UpdateProductIdsHdpFhOfMatchHistoryBeforeLive(Match match)
        {
            int minute = (int)match.TimeSpanFromStartGb.TotalMinutes;
            if (minute < 0)
            {
                if (!DataStore.ProductIdsHdpFhOfMatchHistoryBeforeLive.ContainsKey(match.MatchId))
                {
                    DataStore.ProductIdsHdpFhOfMatchHistoryBeforeLive.Add(match.MatchId, new List<ProductIdsHistory>());
                }
                if (DataStore.ProductIdsOfMatch.ContainsKey(match.MatchId))
                {
                    int count = DataStore.ProductIdsOfMatch[match.MatchId].Count(item => item.ProductBetType == Enums.BetType.FirstHalfHandicap);

                    if (DataStore.ProductIdsHdpFhOfMatchHistoryBeforeLive[match.MatchId].Count == 0)
                    {
                        DataStore.ProductIdsHdpFhOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
                    }

                    if (DataStore.ProductIdsHdpFhOfMatchHistoryBeforeLive[match.MatchId].LastOrDefault().Count != count)
                    {
                        DataStore.ProductIdsHdpFhOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
                    }
                }
            }
        }
        public void UpdateProductIdsHdpFhOfMatchHistoryLive(Match match)
        {
            int minute = (int)match.TimeSpanFromStartGb.TotalMinutes;
            if (minute >= 0)
            {
                if (!DataStore.ProductIdsHdpFhOfMatchHistoryLive.ContainsKey(match.MatchId))
                {
                    DataStore.ProductIdsHdpFhOfMatchHistoryLive.Add(match.MatchId, new List<ProductIdsHistory>());
                }
                if (DataStore.ProductIdsOfMatch.ContainsKey(match.MatchId))
                {
                    int count = DataStore.ProductIdsOfMatch[match.MatchId].Count(item => item.ProductBetType == Enums.BetType.FirstHalfHandicap);

                    if (DataStore.ProductIdsHdpFhOfMatchHistoryLive[match.MatchId].Count == 0)
                    {
                        DataStore.ProductIdsHdpFhOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                        DataStore.Matchs[match.MatchId].LastUpdatePhHdpFhLive = DateTime.Now;
                    }

                    if (DataStore.ProductIdsHdpFhOfMatchHistoryLive[match.MatchId].LastOrDefault().Count != count)
                    {
                        DataStore.ProductIdsHdpFhOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                        DataStore.Matchs[match.MatchId].LastUpdatePhHdpFhLive = DateTime.Now;
                    }
                }
            }
        }



        private void UpdateAllExcel4th(Match match)
        {
            if (DataStore.OverUnderScoreTimesV3.ContainsKey(match.MatchId))
            {
                int minuteDiff = (int)match.GlobalShowtime.Subtract(match.FirstShow).TotalMinutes;
                if (minuteDiff < 0) return;

                Dictionary<int, List<OverUnderScoreTimesV2Item>> data = DataStore.OverUnderScoreTimesV3[match.MatchId];
                Dictionary<int, List<OverUnderScoreTimesV3Item>> dataBeforeLive = new Dictionary<int, List<OverUnderScoreTimesV3Item>>();
                List<Product> beforeLiveProduct = new List<Product>();
                if (DataStore.VeryFirstProducts.ContainsKey(match.MatchId))
                {
                    beforeLiveProduct = DataStore.VeryFirstProducts[match.MatchId].Where(item => item.Bettype == Enums.BetType.FullTimeOverUnder).ToList();
                }

                if (DataStore.OverUnderScoreTimesBeforeLive.ContainsKey(match.MatchId))
                {
                    dataBeforeLive = DataStore.OverUnderScoreTimesBeforeLive[match.MatchId];
                }

                List<int> hdps = data.Keys.OrderByDescending(item => item).ToList();
                Dictionary<int, OverUnderSummary> summaryData = new Dictionary<int, OverUnderSummary>();

                Dictionary<int, List<int>> dataExcel = new Dictionary<int,List<int>>();

                for (int i = 0; i < hdps.Count; i++)
                {
                    if (data[hdps[i]][0].Over == 0
                        && data[hdps[i]][1].Over == 0
                        && data[hdps[i]][2].Over == 0)
                    {
                        continue;
                    }

                    OverUnderSummary item = new OverUnderSummary()
                    {
                        Hdp = hdps[i],
                        VeryFirstPrice = minuteDiff < 90 ? new OverUnderScoreTimesV2Item() : FindVeryFirstPrice(hdps[i], beforeLiveProduct),
                        At90BeforeLive = FindPriceAt90BeforeLive(match, hdps[i], dataBeforeLive),
                        BeforeLivePrice = FindBeforeLivePrice(hdps[i], data),
                        FirstPriceInLive = FindFirstPriceInLive(hdps[i], data)
                    };

                    summaryData.Add(hdps[i], item);
                }
                if (!DataStore.Excel4th.ContainsKey(match.MatchId))
                {
                    DataStore.Excel4th.Add(match.MatchId, new Dictionary<int, OverUnderSummary>());
                }
                DataStore.Excel4th[match.MatchId] = summaryData;
                
            }
        }

        private OverUnderScoreTimesV2Item FindVeryFirstPrice(int hdp, List<Product> beforeLiveProducts)
        {
            OverUnderScoreTimesV2Item veryFirstPrice = new OverUnderScoreTimesV2Item();

            for (int i = 0; i < beforeLiveProducts.Count; i++)
            {
                if (beforeLiveProducts[i].Hdp1 * 100 == hdp)
                {
                    veryFirstPrice.Over = beforeLiveProducts[i].Odds1a100;
                    veryFirstPrice.Under = beforeLiveProducts[i].Odds2a100;
                    break;
                }
            }

            return veryFirstPrice;
        }

        private OverUnderScoreTimesV2Item FindPriceAt90BeforeLive(Match match, int hdp, Dictionary<int, List<OverUnderScoreTimesV3Item>> dataBeforeLive)
        {
            OverUnderScoreTimesV2Item priceAt90BeforeLive = new OverUnderScoreTimesV2Item();

            if (dataBeforeLive.ContainsKey(hdp))
            {
                OverUnderScoreTimesV3Item veryFirstPriceV3 = dataBeforeLive[hdp].Where(item =>
                    item.RecordedDatetime != DateTime.MinValue
                    && item.RecordedDatetime != DateTime.MaxValue.Subtract(TimeSpan.FromDays(1))
                    && item.RecordedDatetime != DateTime.MaxValue
                )
                .FirstOrDefault(item => (int)match.GlobalShowtime.Subtract(item.RecordedDatetime).TotalMinutes == 90);

                if (veryFirstPriceV3 != null)
                {
                    priceAt90BeforeLive.Over = veryFirstPriceV3.Over;
                    priceAt90BeforeLive.Under = veryFirstPriceV3.Under;
                }

            }

            return priceAt90BeforeLive;
        }

        private OverUnderScoreTimesV2Item FindBeforeLivePrice(int hdp, Dictionary<int, List<OverUnderScoreTimesV2Item>> data)
        {
            OverUnderScoreTimesV2Item beforeLivePrice = new OverUnderScoreTimesV2Item();

            if (data.ContainsKey(hdp))
            {
                beforeLivePrice = data[hdp][0];
            }

            return beforeLivePrice;
        }

        private OverUnderScoreTimesV2Item FindFirstPriceInLive(int hdp, Dictionary<int, List<OverUnderScoreTimesV2Item>> data)
        {
            OverUnderScoreTimesV2Item beforeLivePrice = new OverUnderScoreTimesV2Item();

            if (data.ContainsKey(hdp))
            {
                beforeLivePrice = data[hdp].Skip(1).FirstOrDefault(item => item.Over != 0);
                if (beforeLivePrice == null) beforeLivePrice = new OverUnderScoreTimesV2Item();
            }

            return beforeLivePrice;
        }



        private void UpdateSpecialAlert(Match match)
        {
            try
            {
                if (match.LastTime_Ou_050_87 == DateTime.MinValue && DataStore.Matchs.ContainsKey(match.MatchId))
                {
                    if (DataStore.OverUnderScoreTimes.ContainsKey(match.MatchId))
                    {
                        Dictionary<int, List<string>> data = DataStore.OverUnderScoreTimes[match.MatchId];
                        if (data.ContainsKey(50))
                        {
                            List<string> row = data[50];
                            if (row.Where(item => item.Length > 0).Any(item => int.Parse(item) > 87 || int.Parse(item) < 0))
                            {
                                DataStore.Matchs[match.MatchId].LastTime_Ou_050_87 = DateTime.Now;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }

            try
            {
                if (match.LastTimeHasTGT225 == DateTime.MinValue && DataStore.Matchs.ContainsKey(match.MatchId))
                {
                    int maxNumberOfHdp = Common.Functions.MaxNumberOfHdp(match.MatchId);
                    List<IuooValue_3> data = Common.Functions.GetDataIuoo_ByGoal_3(match.MatchId, 1, false, maxNumberOfHdp);
                    if (data.Any(item => item.Hdp == 250 && item.TGT > 0))
                    {
                        DataStore.Matchs[match.MatchId].LastTimeHasTGT225 = DateTime.Now;
                    }
                }
            }
            catch(Exception ex)
            {

            }


        }

        private void ProcessHalfTime(Match match, List<Product> products)
        {
            if (!match.IsHT) return;

            int restedMinutes = (int)DateTime.Now.Subtract(match.BeginHT).TotalMinutes;
            if (restedMinutes > 30) return;

            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;

            if (!DataStore.OverUnderScoreHalfTimes.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreHalfTimes.Add(match.MatchId, new Dictionary<int, List<string>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2) - totalLiveScore) * 100);
                if (!DataStore.OverUnderScoreHalfTimes[match.MatchId].ContainsKey(score))
                {
                    List<string> row = new List<string>();
                    for (int j = 0; j <= 30; j++)
                    {
                        row.Add("");
                    }
                    DataStore.OverUnderScoreHalfTimes[match.MatchId].Add(score, row);
                }
                DataStore.OverUnderScoreHalfTimes[match.MatchId][score][restedMinutes] = products[i].Odds1a100.ToString();
            }
        }

        private void ProcessHalfTimeV2(Match match, List<Product> products)
        {
            if (!match.IsHT) return;

            int restedMinutes = (int)DateTime.Now.Subtract(match.BeginHT).TotalMinutes;
            if (restedMinutes > 30) return;

            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;

            if (!DataStore.OverUnderScoreHalftime.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreHalftime.Add(match.MatchId, new Dictionary<int, List<OverUnderScoreTimesV3Item>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2) - totalLiveScore) * 100);
                if (!DataStore.OverUnderScoreHalftime[match.MatchId].ContainsKey(score))
                {
                    List<OverUnderScoreTimesV3Item> row = new List<OverUnderScoreTimesV3Item>();
                    for (int j = 0; j <= 30; j++)
                    {
                        row.Add(new OverUnderScoreTimesV3Item() { 
                            Over = 0,
                            Under = 0,
                            RecordedDatetime = DateTime.MinValue
                        });
                    }
                    DataStore.OverUnderScoreHalftime[match.MatchId].Add(score, row);
                }
                DataStore.OverUnderScoreHalftime[match.MatchId][score][restedMinutes] = new OverUnderScoreTimesV3Item() 
                { 
                    Over = products[i].Odds1a100,
                    Under = products[i].Odds2a100,
                    RecordedDatetime = DateTime.Now,
                    OddsId = products[i].OddsId
                };
            }
        }

        private void ProcessUnderScoreHalfTime(Match match, List<Product> products)
        {
            if (!match.IsHT) return;

            int restedMinutes = (int)DateTime.Now.Subtract(match.BeginHT).TotalMinutes;
            if (restedMinutes > 30) return;

            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;

            if (!DataStore.UnderScoreHalfTimes.ContainsKey(match.MatchId))
            {
                DataStore.UnderScoreHalfTimes.Add(match.MatchId, new Dictionary<int, List<string>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2) - totalLiveScore) * 100);
                if (!DataStore.UnderScoreHalfTimes[match.MatchId].ContainsKey(score))
                {
                    List<string> row = new List<string>();
                    for (int j = 0; j <= 30; j++)
                    {
                        row.Add("");
                    }
                    DataStore.UnderScoreHalfTimes[match.MatchId].Add(score, row);
                }
                DataStore.UnderScoreHalfTimes[match.MatchId][score][restedMinutes] = products[i].Odds2a100.ToString();
            }
        }

        private void ProcessUnderScore(Match match, List<Product> products)
        {
            if (match.IsHT) return;
            int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
            if (match.LivePeriod == 0 && !match.IsHT)
            {
                currentMinute = 0;
            }

            int totalLiveScore = match.LiveAwayScore + match.LiveHomeScore;

            if (!DataStore.UnderScoreTimes.ContainsKey(match.MatchId))
            {
                DataStore.UnderScoreTimes.Add(match.MatchId, new Dictionary<int, List<string>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int score = (int)(((products[i].Hdp1 + products[i].Hdp2) - totalLiveScore) * 100);
                if (!DataStore.UnderScoreTimes[match.MatchId].ContainsKey(score))
                {
                    DataStore.UnderScoreTimes[match.MatchId].Add(score, new List<string>());
                    for (int j = 0; j <= 45; j++)
                    {
                        DataStore.UnderScoreTimes[match.MatchId][score].Add("");
                    }
                }
                if (currentMinute <= 45 && currentMinute >= 0)
                {
                    DataStore.UnderScoreTimes[match.MatchId][score][currentMinute] = products[i].Odds2a100.ToString();
                }
            }
        }

        private void UpdateIuoo_1(Match match, List<Product> products)
        {
            if (!DataStore.OverUnderScoreTimesV2.ContainsKey(match.MatchId)) return;

            //(1, false, maxNumberOfHdp);
            int maxNumberOfHdp = Common.Functions.MaxNumberOfHdp(match.MatchId);
            List<IuooValue_3> data = Common.Functions.GetDataIuoo_ByGoal_3(match.MatchId, 1, false, maxNumberOfHdp);

            if (!DataStore.MatchIuooValue_1.ContainsKey(match.MatchId))
            {
                DataStore.MatchIuooValue_1.Add(match.MatchId, new Dictionary<int, IuooValue>());
            }

            for (int i=0; i<data.Count; i++)
            {
                if (!DataStore.MatchIuooValue_1[match.MatchId].ContainsKey(data[i].Hdp))
                {
                    DataStore.MatchIuooValue_1[match.MatchId].Add(data[i].Hdp, new IuooValue() { TX = data[i].TX });
                }
                DataStore.MatchIuooValue_1[match.MatchId][data[i].Hdp].TX = data[i].TX;
            }
        }

        private void UpdateIuoo_2(Match match, List<Product> products)
        {
            if (!DataStore.OverUnderScoreTimesV2.ContainsKey(match.MatchId)) return;

            //(1, false, maxNumberOfHdp);
            int maxNumberOfHdp = Common.Functions.MaxNumberOfHdp(match.MatchId);
            List<IuooValue_3> data = Common.Functions.GetDataIuoo_ByGoal_3(match.MatchId, 2, false, maxNumberOfHdp);
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Hdp += 100;
                if (data[i].TGT > 0)
                {
                    data[i].TX = data[i].TGT - data[i].TGX + 1;
                }

            }


            if (!DataStore.MatchIuooValue_2.ContainsKey(match.MatchId))
            {
                DataStore.MatchIuooValue_2.Add(match.MatchId, new Dictionary<int, IuooValue>());
            }

            for (int i = 0; i < data.Count; i++)
            {
                if (!DataStore.MatchIuooValue_2[match.MatchId].ContainsKey(data[i].Hdp))
                {
                    DataStore.MatchIuooValue_2[match.MatchId].Add(data[i].Hdp, new IuooValue() { TX = data[i].TX });
                }
                DataStore.MatchIuooValue_2[match.MatchId][data[i].Hdp].TX = data[i].TX;
            }
        }

        private void UpdateTrackOverUnderBeforeLive(Match match, List<Product> products)
        {
            if (match.LivePeriod != 0) return;
            if (match.IsHT) return;
            int minuteDiff = (int)DateTime.Now.Subtract(match.GlobalShowtime).TotalMinutes;
            if (minuteDiff >= 0) return;

            if (!DataStore.TrackOverUnderBeforeLive.ContainsKey(match.MatchId))
            {
                DataStore.TrackOverUnderBeforeLive.Add(match.MatchId, new List<TrackOverUnderBeforeLiveItem>());
                DataStore.TrackOverUnderBeforeLive[match.MatchId].Add(new TrackOverUnderBeforeLiveItem());
            }

            List<int> hdps_100 = products.Select(item => (int)((item.Hdp1 + item.Hdp2) * 100)).ToList();

            if (hdps_100.Count == 0) return;

            TrackOverUnderBeforeLiveItem lastItem = DataStore.TrackOverUnderBeforeLive[match.MatchId].LastOrDefault();
            List<int> last_hdps_100 = lastItem.Hdps_100.Select(item => item.Key).ToList();



            if (hdps_100.Except(last_hdps_100).Count() > 0
                || last_hdps_100.Except(hdps_100).Count() > 0)
            {
                List<KeyValuePair<int, long>> newData = new List<KeyValuePair<int, long>>();
                for (int i=0; i<products.Count; i++)
                {
                    int hdp = (int)((products[i].Hdp1 + products[i].Hdp2) * 100);
                    newData.Add(new KeyValuePair<int, long>(
                        hdp,
                        products[i].OddsId
                    ));
                }


                DataStore.TrackOverUnderBeforeLive[match.MatchId].Add(new TrackOverUnderBeforeLiveItem() { 
                    MinuteBeforeLive = minuteDiff,
                    Hdps_100 = newData
                });
            }
        }

        private void UpdateOverUnderFtBeforeLive(Match match, List<Product> products)
        {
            if (match.LivePeriod != 0) return;
            if (match.IsHT) return;
            int minuteDiff = (int)DateTime.Now.Subtract(match.GlobalShowtime).TotalMinutes;
            if (minuteDiff >= 0) return;

            if (!DataStore.OverUnderScoreTimesBeforeLive.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreTimesBeforeLive.Add(match.MatchId, new Dictionary<int, List<OverUnderScoreTimesV3Item>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int hdp = (int)((products[i].Hdp1 + products[i].Hdp2) * 100);
                if (!DataStore.OverUnderScoreTimesBeforeLive[match.MatchId].ContainsKey(hdp))
                {
                    List<OverUnderScoreTimesV3Item> emptyRow = Common.Functions.GetEmptyRowBeforeLiveOuv3();

                    DataStore.OverUnderScoreTimesBeforeLive[match.MatchId].Add(hdp, emptyRow);
                }

                DateTime lastUpdateDt = DataStore.OverUnderScoreTimesBeforeLive[match.MatchId][hdp].Min(item => item.RecordedDatetime);
                bool isFirstUpdate = !DataStore.OverUnderScoreTimesBeforeLive[match.MatchId][hdp].Any(item => item.RecordedDatetime != DateTime.MinValue);

                for (int j=0; j < DataStore.OverUnderScoreTimesBeforeLive[match.MatchId][hdp].Count; j++)
                {
                    if (DataStore.OverUnderScoreTimesBeforeLive[match.MatchId][hdp][j].RecordedDatetime == lastUpdateDt)
                    {
                        if (minuteDiff >= -100)
                        {
                            DataStore.OverUnderScoreTimesBeforeLive[match.MatchId][hdp][j] = new OverUnderScoreTimesV3Item()
                            {
                                Over = products[i].Odds1a100,
                                Under = products[i].Odds2a100,
                                RecordedDatetime = DateTime.Now
                            };
                        }

                        if (isFirstUpdate)
                        {
                            DataStore.OverUnderScoreTimesBeforeLive[match.MatchId][hdp][j] = new OverUnderScoreTimesV3Item()
                            {
                                Over = products[i].Odds1a100,
                                Under = products[i].Odds2a100,
                                RecordedDatetime = DateTime.MaxValue
                            };
                        }

                        break;
                    }
                }
            }

        }

        private void UpdateOverUnderFhBeforeLive(Match match, List<Product> products)
        {
            if (match.LivePeriod != 0) return;
            if (match.IsHT) return;
            int minuteDiff = (int)DateTime.Now.Subtract(match.GlobalShowtime).TotalMinutes;
            if (minuteDiff >= 0) return;

            if (!DataStore.OverUnderScoreTimesFirstHalfBeforeLive.ContainsKey(match.MatchId))
            {
                DataStore.OverUnderScoreTimesFirstHalfBeforeLive.Add(match.MatchId, new Dictionary<int, List<OverUnderScoreTimesV3Item>>());
            }

            for (int i = 0; i < products.Count; i++)
            {
                int hdp = (int)((products[i].Hdp1 + products[i].Hdp2) * 100);
                if (!DataStore.OverUnderScoreTimesFirstHalfBeforeLive[match.MatchId].ContainsKey(hdp))
                {
                    List<OverUnderScoreTimesV3Item> emptyRow = Common.Functions.GetEmptyRowBeforeLiveOuv3();

                    DataStore.OverUnderScoreTimesFirstHalfBeforeLive[match.MatchId].Add(hdp, emptyRow);
                }

                DateTime lastUpdateDt = DataStore.OverUnderScoreTimesFirstHalfBeforeLive[match.MatchId][hdp].Min(item => item.RecordedDatetime);
                bool isFirstUpdate = !DataStore.OverUnderScoreTimesFirstHalfBeforeLive[match.MatchId][hdp].Any(item => item.RecordedDatetime != DateTime.MinValue);

                for (int j = 0; j < DataStore.OverUnderScoreTimesFirstHalfBeforeLive[match.MatchId][hdp].Count; j++)
                {
                    if (DataStore.OverUnderScoreTimesFirstHalfBeforeLive[match.MatchId][hdp][j].RecordedDatetime == lastUpdateDt)
                    {
                        if (minuteDiff >= -100)
                        {
                            DataStore.OverUnderScoreTimesFirstHalfBeforeLive[match.MatchId][hdp][j] = new OverUnderScoreTimesV3Item()
                            {
                                Over = products[i].Odds1a100,
                                Under = products[i].Odds2a100,
                                RecordedDatetime = DateTime.Now
                            };
                        }

                        if (isFirstUpdate)
                        {
                            DataStore.OverUnderScoreTimesFirstHalfBeforeLive[match.MatchId][hdp][j] = new OverUnderScoreTimesV3Item()
                            {
                                Over = products[i].Odds1a100,
                                Under = products[i].Odds2a100,
                                RecordedDatetime = DateTime.MaxValue
                            };
                        }

                        break;
                    }
                }
            }


        }

        private void UpdateVeryFirstProducts(Match match)
        {
            if(!DataStore.VeryFirstProducts.ContainsKey(match.MatchId))
            {
                DataStore.VeryFirstProducts.Add(match.MatchId, DataStore.Products.Values.Where(item => item.MatchId == match.MatchId).ToList());
            }
        }

        private void UpdateOverUnderMoneyLine(Match match)
        {
            
            if (match.LivePeriod != 1 && match.LivePeriod != 2)
            {
                return;
            }

            if (match.TimeSpanFromStart.TotalMinutes <= 0 || match.TimeSpanFromStart.TotalMinutes > 45)
            {
                return;
            }

            if (!DataStore.MatchOverUnderMoneyLine.ContainsKey(match.MatchId))
            {
                List<List<int>> data = new List<List<int>>();
                List<int> row1 = new List<int>();
                List<int> row2 = new List<int>();

                for (int i = 0; i <= 45; i++)
                {
                    row1.Add(0);
                    row2.Add(0);
                }

                data.Add(new List<int>());
                data.Add(row1);
                data.Add(row2);
                DataStore.MatchOverUnderMoneyLine.Add(match.MatchId , data);
            }
            DataStore.MatchOverUnderMoneyLine[match.MatchId][match.LivePeriod][(int)match.TimeSpanFromStart.TotalMinutes] = match.OverUnderMoneyLine;

        }

    }
}
