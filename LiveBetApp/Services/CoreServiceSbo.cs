using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services
{
    public class CoreServiceSbo
    {
        private RebuildUrlService _rebuildUrlService;
        private int _previousVersionToday = 0;
        private int _previousVersionLive = 0;
        private Dictionary<long, long> _matchMapping; //<map_id, match_id>
        private Dictionary<long, long> _nonMatchMapping; //<map_id, match_id>
        public CoreServiceSbo()
        {
            _rebuildUrlService = new RebuildUrlService();
            _matchMapping = new Dictionary<long, long>();
            _nonMatchMapping = new Dictionary<long, long>();
        }

        public void Execute()
        {
            Thread coreServiceThread;
            coreServiceThread = new Thread(() =>
            {
                ExecuteCore();
            });
            coreServiceThread.IsBackground = true;
            coreServiceThread.Start();
        }

        private JToken GetSboResponseDataToday(string cookie)
        {
            
            string url = _rebuildUrlService.GetSboDataUrlToday(_previousVersionToday);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers["Cookie"] = cookie;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36";

            var response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();

            responseString = responseString.Substring(147);
            responseString = responseString.Substring(0, responseString.Length - 2);
            responseString = responseString.Replace("\\u200C", "");
            responseString = responseString.Replace(",,", ",null,");

            JToken jToken = JToken.Parse(responseString);

            _previousVersionToday = int.Parse(jToken[0].ToString());

            return jToken;
        }

        private JToken GetSboResponseDataLive(string cookie)
        {

            string url = _rebuildUrlService.GetSboDataUrlLive(_previousVersionLive);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers["Cookie"] = cookie;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36";

            var response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();

            responseString = responseString.Substring(117);
            responseString = responseString.Substring(0, responseString.Length - 2);
            responseString = responseString.Replace("\\u200C", "");
            responseString = responseString.Replace(",,", ",null,");

            JToken jToken = JToken.Parse(responseString);

            _previousVersionLive = int.Parse(jToken[0].ToString());

            return jToken;
        }

        private void ExecuteCore()
        {
            string cookie = Common.Config.GetConfigModelSbo().cookie;

            while(true)
            {

                try
                {
                    JToken response = GetSboResponseDataLive(cookie);
                    JToken mainToken = GetMainToken(response);
                    JToken hasLiveToken = GetHasLiveToken(response);
                    JToken leaguesToken = mainToken[0];
                    JToken matchsToken = mainToken[1];
                    JToken matchMappingToken = mainToken[2];
                    JToken matchInfoToken = mainToken[3];
                    JToken deleteMatchsToken = mainToken[4];
                    JToken productsToken = mainToken[5];
                    JToken deleteProductsToken = mainToken[6];

                    try { UpdateLeagues(leaguesToken); }
                    catch (Exception ex)
                    { }

                    try { UpdateMatchs(matchsToken); }
                    catch (Exception ex)
                    { }

                    try { UpdateMatchInfo(matchInfoToken); }
                    catch (Exception ex)
                    { }

                    try { UpdateMatchMappingToken(matchMappingToken); }
                    catch (Exception ex)
                    { }



                    try { DeleteMatchs(deleteMatchsToken); }
                    catch (Exception ex)
                    { }

                    try { UpdateProducts(productsToken); }
                    catch (Exception ex)
                    { }

                    try { DeleteProduct(deleteProductsToken); }
                    catch (Exception ex)
                    { }

                    try { UpdateHasLive(hasLiveToken); }
                    catch (Exception ex)
                    { }

                    response = GetSboResponseDataToday(cookie);
                    mainToken = GetMainToken(response);
                    hasLiveToken = GetHasLiveToken(response);
                    leaguesToken = mainToken[0];
                    matchsToken = mainToken[1];
                    matchMappingToken = mainToken[2];
                    matchInfoToken = mainToken[3];
                    deleteMatchsToken = mainToken[4];
                    productsToken = mainToken[5];
                    deleteProductsToken = mainToken[6];

                    try { UpdateLeagues(leaguesToken); }
                    catch (Exception ex)
                    { }

                    try { UpdateMatchs(matchsToken); }
                    catch (Exception ex)
                    { }

                    try { UpdateMatchInfo(matchInfoToken); }
                    catch (Exception ex)
                    { }

                    try { UpdateMatchMappingToken(matchMappingToken); }
                    catch (Exception ex)
                    { }



                    try { DeleteMatchs(deleteMatchsToken); }
                    catch (Exception ex)
                    { }

                    try { UpdateProducts(productsToken); }
                    catch (Exception ex)
                    { }

                    try { DeleteProduct(deleteProductsToken); }
                    catch (Exception ex)
                    { }

                    try { UpdateHasLive(hasLiveToken); }
                    catch (Exception ex)
                    { }


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Thread.Sleep(20000);
                }
            }
        }

        private void UpdateLeagues(JToken leaguesToken)
        {
            int leagueId;
            string leagueName;
            int length = leaguesToken.Count();

            for (int i = 0; i < length; i++)
            {
                leagueId = int.Parse(leaguesToken[i][0].ToString());
                leagueName = leaguesToken[i][1].ToString();
                if (!DataStore.Leagues.ContainsKey(leagueId))
                {
                    DataStore.Leagues.Add(leagueId, leagueName);
                }
            }

        }

        private DateTime ConvertSboDateTime(string sboDateTime)
        {
            //03/09/2020 21:30
            int month = int.Parse(sboDateTime.Substring(0, 2));
            int day = int.Parse(sboDateTime.Substring(3, 2));
            int year = int.Parse(sboDateTime.Substring(6, 4));
            int hour = int.Parse(sboDateTime.Substring(11, 2));
            int minute = int.Parse(sboDateTime.Substring(14, 2));
            try
            {
                return new DateTime(year, month, day, hour, minute, 0);
            }
            catch
            {
                return DateTime.MinValue;
            } 

            
        }

        private void UpdateMatchs(JToken matchsToken)
        {
            Match match;
            long matchId;

            string LeagueName;
            int length = matchsToken.Count();

            for (int i = 0; i < length; i++)
            {
                matchId = long.Parse(matchsToken[i][0].ToString());
                if (DataStore.Matchs.TryGetValue(matchId, out match))
                {

                }
                else
                {
                    match = new Match() { MatchId = matchId, FirstShow = DateTime.Now };
                    DataStore.Matchs.Add(match.MatchId, match);
                }

                match.LeagueId = long.Parse(matchsToken[i][2].ToString());
                if (DataStore.Leagues.TryGetValue(match.LeagueId, out LeagueName)) match.League = LeagueName;
                match.Home = matchsToken[i][3].ToString();
                match.Away = matchsToken[i][4].ToString();
                match.GlobalShowtime = ConvertSboDateTime(matchsToken[i][7].ToString()).AddHours(-1);
                match.LiveTimer = match.GlobalShowtime;

                DataStore.Matchs[match.MatchId] = match;
            }
        }

        private void UpdateMatchMappingToken(JToken matchMappingToken)
        {
            long mapId;
            long matchId;
            int nonMatchId;
            int liveHomeScore;
            int liveAwayScore;
            Match match;
            int length = matchMappingToken.Count();
            bool hasGoal;
            bool isCancelGoal;

            for (int i = 0; i < length; i++)
            {
                mapId = long.Parse(matchMappingToken[i][0].ToString());
                matchId = long.Parse(matchMappingToken[i][1].ToString());
                nonMatchId = int.Parse(matchMappingToken[i][2].ToString());
                liveHomeScore = int.Parse(matchMappingToken[i][3].ToString());
                liveAwayScore = int.Parse(matchMappingToken[i][4].ToString());

                if (nonMatchId == 0 && !this._matchMapping.ContainsKey(mapId))
                {
                    this._matchMapping.Add(mapId, matchId);
                }

                if (nonMatchId != 0)
                {
                    if (!this._nonMatchMapping.ContainsKey(mapId))
                    {
                        this._nonMatchMapping.Add(mapId, matchId);
                    }
                    continue;
                } 


                if (DataStore.Matchs.TryGetValue(matchId, out match))
                {
                    hasGoal = (match.LiveHomeScore < liveHomeScore
                        || match.LiveAwayScore < liveAwayScore);
                    if (match.LiveHomeScore < liveHomeScore)
                    {
                        match.GoalTeam = "h";
                    }
                    else if(match.LiveAwayScore < liveAwayScore)
                    {
                        match.GoalTeam = "a";
                    }

                    isCancelGoal = (match.LiveHomeScore > liveHomeScore
                        || match.LiveAwayScore > liveAwayScore);

                    match.LiveHomeScore = liveHomeScore;
                    match.LiveAwayScore = liveAwayScore;
                    DataStore.Matchs[matchId] = match;

                    if (hasGoal) Common.Functions.UpdateGoalHistorySbo(match);
                    if (isCancelGoal) Common.Functions.CancelGoal(match.MatchId);
                }
                else
                {
                    // add match. But i dont think this case is posible
                }

            }
        }

        private void UpdateMatchInfo(JToken matchInfoToken)
        {
            long mapId;
            long matchId;
            int currentMinute;
            DateTime liveTimer;
            Match match;

            int length = matchInfoToken.Count();
            for (int i = 0; i < length; i++)
            {
                mapId = long.Parse(matchInfoToken[i][0].ToString());
                currentMinute = int.Parse(matchInfoToken[i][3].ToString());
                liveTimer = DateTime.Now.Subtract(new TimeSpan(0, currentMinute, 0));

                if (_matchMapping.TryGetValue(mapId, out matchId)
                    && DataStore.Matchs.TryGetValue(matchId, out match))
                {
                    match.LivePeriod = int.Parse(matchInfoToken[i][2].ToString());
                    match.HomeRed = int.Parse(matchInfoToken[i][6].ToString());
                    match.AwayRed = int.Parse(matchInfoToken[i][7].ToString());
                    match.InjuryTime = int.Parse(matchInfoToken[i][7].ToString());
                    match.LiveTimer = liveTimer;

                    if (match.LivePeriod == 3) match.LivePeriod = 1;
                    if (match.LivePeriod == 4) match.LivePeriod = 2;

                    if (match.LivePeriod == 5)
                    {
                        if (!match.IsHT) match.BeginHT = DateTime.Now;
                        match.IsHT = true;
                    }
                    else
                    {
                        if (match.IsHT) match.EndHT = DateTime.Now;
                        match.IsHT = false;
                    }

                    DataStore.Matchs[matchId] = match;
                }
            }
        }

        private void DeleteMatchs(JToken deleteMatchsToken)
        {
            long mapId;
            long matchId;
            int length = deleteMatchsToken.Count();
            for (int i = 0; i < length; i++)
            {
                mapId = long.Parse(deleteMatchsToken[i].ToString());
                if (_nonMatchMapping.ContainsKey(mapId)) continue;
                matchId = this._matchMapping[mapId];
                Common.Functions.DeleteMatch(matchId);
            }
        }

        private void UpdateHasLive(JToken hasLiveToken)
        {
            long matchId;
            int length = hasLiveToken.Count();
            for (int i = 0; i < length; i++)
            {
                try
                {
                    matchId = long.Parse(hasLiveToken[i].ToString());
                    Match match = DataStore.Matchs[matchId];
                    match.HasStreaming = true;

                    if (match.TimeHasStreaming == DateTime.MinValue)
                    {
                        match.TimeHasStreaming = DateTime.Now;
                    }

                    if (match.FirstTimeHasStreaming == DateTime.MinValue)
                    {
                        match.FirstTimeHasStreaming = DateTime.Now;
                    }

                    DataStore.Matchs[matchId] = match;
                }
                catch
                {

                }

            }
        }

        private void UpdateProducts(JToken productsToken)
        {
            long mapId;
            long productId;
            long matchId = 0;
            float hdp;
            JToken productInfo;
            JToken productPrice;
            Product product;
            bool isAddNew;

            int length = productsToken.Count();
            for (int i = 0; i < length; i++)
            {
                try
                {
                    productId = long.Parse(productsToken[i][0].ToString());
                    productInfo = productsToken[i][1];

                    if (DataStore.Products.TryGetValue(productId, out product))
                    {
                        isAddNew = false;
                    }
                    else
                    {
                        isAddNew = true;
                        product = new Product()
                        {
                            OddsId = productId
                        };
                        DataStore.Products.Add(productId, product);
                    }


                    if (productInfo.Type != JTokenType.Undefined && productInfo.Type != JTokenType.Null)
                    {
                        if (productInfo[0].Type != JTokenType.Undefined && productInfo[0].Type != JTokenType.Null)
                        {
                            mapId = long.Parse(productInfo[0].ToString());
                            if (_nonMatchMapping.ContainsKey(mapId)) continue;
                            matchId = _matchMapping[mapId];
                            product.MatchId = matchId;
                        }

                        if (productInfo[1].Type != JTokenType.Undefined && productInfo[1].Type != JTokenType.Null)
                        {
                            product.Bettype = ConvertSboProductTypeToBong88ProductType(int.Parse(productInfo[1].ToString()));
                        }

                        if (productInfo.Count() == 5 && productInfo[4].Type != JTokenType.Undefined && productInfo[4].Type != JTokenType.Null)
                        {
                            hdp = float.Parse(productInfo[4].ToString());
                            product.Hdp1 = hdp > 0 ? hdp : 0;
                            product.Hdp2 = hdp < 0 ? -hdp : 0;
                        }

                    }

                    if (productsToken[i].Count() >= 3)
                    {
                        productPrice = productsToken[i][2];
                        if (product.Bettype == Common.Enums.BetType.FirstHalf1x2
                            || product.Bettype == Common.Enums.BetType.FullTime1x2)
                        {
                            if (productPrice[0].Type != JTokenType.Undefined 
                                && productPrice[0].Type != JTokenType.Null) product.Com1 = float.Parse(productPrice[0].ToString());
                            if (productPrice.Count() >= 2
                                && productPrice[1].Type != JTokenType.Undefined
                                && productPrice[1].Type != JTokenType.Null) product.Comx = float.Parse(productPrice[1].ToString());
                            if (productPrice.Count() >= 3 
                                && productPrice[2].Type != JTokenType.Undefined
                                && productPrice[2].Type != JTokenType.Null) product.Com2 = float.Parse(productPrice[2].ToString());
                        }
                        else
                        {
                            if (productPrice[0].Type != JTokenType.Undefined 
                                && productPrice[0].Type != JTokenType.Null) product.Odds1a = float.Parse(productPrice[0].ToString());
                            if (productPrice.Count() >= 2
                                && productPrice[1].Type != JTokenType.Undefined
                                && productPrice[1].Type != JTokenType.Null) product.Odds2a = float.Parse(productPrice[1].ToString());
                        }
                    }

                    DataStore.Products[productId] = product;
                    Common.Functions.UpdateFinishPriceOverUnderScoreTimes(product.MatchId, product);
                    if (isAddNew)
                    {
                        Match match;
                        if (DataStore.Matchs.TryGetValue(product.MatchId, out match) && match.LivePeriod >= 0)
                        {
                            if (!DataStore.ProductIdsOfMatch.ContainsKey(product.MatchId))
                            {
                                DataStore.ProductIdsOfMatch.Add(product.MatchId, new List<ProductIdLog>());
                            }

                            if (!DataStore.ProductIdsOfMatch[product.MatchId].Any(item => item.OddsId == product.OddsId))
                            {
                                //UpdateAlert21(product);
                                DataStore.ProductIdsOfMatch[product.MatchId].Add(new ProductIdLog()
                                {
                                    OddsId = product.OddsId,
                                    HdpS = CalculateProductIdHdp(product),
                                    Hdp = (int)(product.Hdp1 * 100),
                                    Odds1a100 = product.Odds1a100,
                                    Odds2a100 = product.Odds2a100,
                                    ProductBetType = product.Bettype,
                                    Minute = (int)match.TimeSpanFromStartGb.TotalMinutes,
                                    LivePeriod = match.LivePeriod,
                                    CreateDatetime = DateTime.Now
                                });
                                UpdateProductIdsOfMatchHistoryLive(match, product);
                            }
                        }

                        Common.Functions.UpdateMaxMinHdpOfMatch(product.MatchId, product);
                        if (product.Bettype == Enums.BetType.FullTimeOverUnder)
                        {

                        }

                    }

                }
                catch(Exception ex)
                {
                    Common.Functions.WriteJsonObjectData<JToken>(
                        Common.Constants.JsonErrorLogfolder + Common.Functions.GetDimDatetime(DateTime.Now),
                        productsToken[i]
                    );
                    throw ex;
                }

            }
        }

        private void UpdateProductIdsOfMatchHistoryLive(Match match, Product product)
        {
            if (product.Bettype == Enums.BetType.FullTimeOverUnder)
            {
                DataStore.Matchs[match.MatchId].LastHasSpeaker = DateTime.Now;
                UpdateProductIdsOuFtOfMatchHistoryBeforeLive(match);
                UpdateProductIdsOuFtOfMatchHistoryLive(match);
            }
            else if (product.Bettype == Enums.BetType.FirstHalfOverUnder)
            {
                DataStore.Matchs[match.MatchId].LastHasId3 = DateTime.Now;
                UpdateProductIdsOuFhOfMatchHistoryBeforeLive(match);
                UpdateProductIdsOuFhOfMatchHistoryLive(match);
            }
            else if (product.Bettype == Enums.BetType.FullTimeHandicap)
            {
                DataStore.Matchs[match.MatchId].LastHasId3 = DateTime.Now;
                UpdateProductIdsHdpFtOfMatchHistoryBeforeLive(match);
                UpdateProductIdsHdpFtOfMatchHistoryLive(match);
            }
            else if (product.Bettype == Enums.BetType.FirstHalfHandicap)
            {
                DataStore.Matchs[match.MatchId].LastHasId3 = DateTime.Now;
                UpdateProductIdsHdpFhOfMatchHistoryBeforeLive(match);
                UpdateProductIdsHdpFhOfMatchHistoryLive(match);
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
                    DataStore.ProductIdsOuFtOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
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
                    DataStore.ProductIdsOuFtOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                    DataStore.Matchs[match.MatchId].LastUpdatePhOuFtLive = DateTime.Now;
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
                    DataStore.ProductIdsHdpFtOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
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
                    DataStore.ProductIdsHdpFtOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                    DataStore.Matchs[match.MatchId].LastUpdatePhHdpFtLive = DateTime.Now;
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
                    DataStore.ProductIdsOuFhOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
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
                    DataStore.ProductIdsOuFhOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                    DataStore.Matchs[match.MatchId].LastUpdatePhOuFhLive = DateTime.Now;
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
                    DataStore.ProductIdsHdpFhOfMatchHistoryBeforeLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute });
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
                    DataStore.ProductIdsHdpFhOfMatchHistoryLive[match.MatchId].Add(new ProductIdsHistory() { Count = count, Minute = minute, LivePeriod = match.LivePeriod });
                    DataStore.Matchs[match.MatchId].LastUpdatePhHdpFhLive = DateTime.Now;
                }
            }
        }
 

        private string CalculateProductIdHdp(Product product)
        {
            if (product.Bettype == Enums.BetType.FirstHalfHandicap || product.Bettype == Enums.BetType.FullTimeHandicap)
            {
                if (product.Hdp1 != 0)
                {
                    return ((int)(product.Hdp1 * 100)).ToString();
                }
                else
                {
                    return (((int)(product.Hdp1 * 100)).ToString() + " | " + ((int)(product.Hdp2 * 100)).ToString());
                }
            }
            else
            {
                return ((int)(product.Hdp1 * 100)).ToString();
            }
        }

        private LiveBetApp.Common.Enums.BetType ConvertSboProductTypeToBong88ProductType(int sboProductType)
        {
            //Product_Type:
            //1	fulltime handicap
            //2	fulltime odd/even
            //3	fulltime over/under
            //4
            //5	fulltime 1x2
            //6
            //7	first half handicap
            //8	first half 1x2
            //9	first half over/under

            if (sboProductType == 1) return LiveBetApp.Common.Enums.BetType.FullTimeHandicap;
            if (sboProductType == 2) return LiveBetApp.Common.Enums.BetType.Unknown;
            if (sboProductType == 3) return LiveBetApp.Common.Enums.BetType.FullTimeOverUnder;
            if (sboProductType == 5) return LiveBetApp.Common.Enums.BetType.FullTime1x2;
            if (sboProductType == 7) return LiveBetApp.Common.Enums.BetType.FirstHalfHandicap;
            if (sboProductType == 8) return LiveBetApp.Common.Enums.BetType.FirstHalf1x2;
            if (sboProductType == 9) return LiveBetApp.Common.Enums.BetType.FirstHalfOverUnder;
            return LiveBetApp.Common.Enums.BetType.Unknown;
        }

        private void DeleteProduct(JToken deleteProductsToken)
        {
            long productId;
            int length = deleteProductsToken.Count();
            for (int i = 0; i < length; i++)
            {
                productId = int.Parse(deleteProductsToken[i].ToString());
                Common.Functions.DeleteProduct(productId);
            }
        }

        private JToken GetMainToken(JToken response)
        {
            if (response.Count() == 6)
            {
                return response[2];
            }
            else if (response.Count() == 8)
            {
                return response[3];
            }
            return null;
        }

        private JToken GetHasLiveToken(JToken response)
        {
            if (response.Count() == 6)
            {
                return response[3][0];
            }
            else if (response.Count() == 8)
            {
                return response[4][0];
            }
            return null;
        }
    }
}
