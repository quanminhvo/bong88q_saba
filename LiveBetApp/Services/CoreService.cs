using LiveBetApp.Common;
using LiveBetApp.Forms;
using LiveBetApp.Models.DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace LiveBetApp.Services
{
    public class CoreService
    {
        private RebuildUrlService _rebuildUrlService;
        private int _reconnectCount;
        private const int _reconnectCountLimit = 1000;

        public CoreService()
        {
            _rebuildUrlService = new RebuildUrlService();
            _reconnectCount = 0;
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

        public void ExecuteCore()
        {
            Thread.Sleep(2000);
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _reconnectCount++;
            string webSocketUrl = "";
            try
            {
                webSocketUrl = _rebuildUrlService.GetWebSocketUrl();
            }
            catch (Exception ex)
            {
                if (_reconnectCount < _reconnectCountLimit)
                {
                    _reconnectCount++;
                    ExecuteCore();
                }
                else
                {
                    Common.Functions.WriteJsonObjectData<Exception>(
                         "ErrorLog\\ReconnectCountExceed_" + Common.Functions.GetDimDatetime(DateTime.Now),
                         ex
                    );
                }
            }
            
            using (var ws = new WebSocket(webSocketUrl))
            {
                if (DataStore.IsWss)
                {
                    ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                }

                ws.OnMessage += (sender, e) =>
                {
                    if (e.Data == "40") {
                        ws.Send("42[\"init\",{\"gid\":\"" + _rebuildUrlService.Gid + "\",\"token\":\"" + _rebuildUrlService.Token + "\",\"id\":\"" + _rebuildUrlService.Id + "\",\"rid\":\"" + _rebuildUrlService.Rid + "\",\"v\":1}]");
                        //ws.Send("42[\"subscribe\",\"odds\",[{\"id\":\"c2\",\"rev\":0,\"condition\":{\"sporttype\":1,\"marketid\":\"L\",\"bettype\":[1,3,5,7,8,15,301,302,303,304],\"sorting\":\"n\"}}]]");
                        ws.Send("42[\"subscribe\",\"odds\",[{\"id\":\"c2\",\"rev\":0,\"condition\":{\"sporttype\":1,\"marketid\":\"L\",\"bettype\":[1,3,5,7,8,15,301,302,303,304],\"sorting\":\"n\"}},{\"id\":\"c3\",\"rev\":0,\"condition\":{\"sporttype\":1,\"marketid\":\"T\",\"bettype\":[1,3,5,7,8,15,301,302,303,304],\"sorting\":\"n\"}}]]");
                    }
                    else if (e.Data.Length > 6)
                    {
                        try
                        {
                            ProcessWebSocketData(JToken.Parse(e.Data.Substring(2, e.Data.Length - 2)));
                        }
                        catch (Exception ex)
                        {
                            Common.Functions.WriteExceptionLog(ex);
                        }
                    }
                };

                ws.OnError += (sender, e) => {
                    Common.Functions.WriteJsonObjectData<ErrorEventArgs>(
                        "ErrorLog\\OnError_" + Common.Functions.GetDimDatetime(DateTime.Now),
                        e
                    );
                };

                ws.OnClose += (sender, e) => {
                    Common.Functions.WriteJsonObjectData<CloseEventArgs>(
                         "ErrorLog\\OnClose_" + Common.Functions.GetDimDatetime(DateTime.Now),
                         e
                    );
                    if (_reconnectCount < _reconnectCountLimit)
                    {
                        _reconnectCount++;
                        ExecuteCore();
                    }
                    else
                    {
                        Common.Functions.WriteJsonObjectData<CloseEventArgs>(
                             "ErrorLog\\ReconnectCountExceed_" + Common.Functions.GetDimDatetime(DateTime.Now),
                             e
                        );
                    }
                };

                try
                {
                    ws.Connect();
                    ws.Send("2probe");

                    while(true)
                    {
                        Thread.Sleep(7000);
                        ws.Send("2");
                    }
                }
                catch(Exception ex)
                {
                    Common.Functions.WriteExceptionLog(ex);
                }
           
            }
        }

        private void ProcessWebSocketData(JToken jToken)
        {
            if (jToken.Count() == 0) return;
            string actionCode = jToken[0].ToString();

            switch (actionCode)
            {
                case "init":
                    Init(jToken[1]);
                    break;
                case "r":
                    ResetMatchs(jToken[1]);
                    break;
                case "p":
                    ProcessMatchData(jToken[1]);
                    break;
                case "t":
                    UpdateGlobalTime(jToken[1]);
                    break;
                default:
                    WriteLogWebSocket(jToken);
                    break;
            }
        }

        private void WriteLogWebSocket(JToken jToken)
        {
            Common.Functions.WriteJsonObjectData<JToken>(
                "ErrorLog\\unknown_" + Common.Functions.GetDimDatetime(DateTime.Now),
                jToken
            );
        }

        private void UpdateGlobalTime(JToken jToken)
        {
            try
            {
                //DataStore.GlobalDateTime = Common.Functions.GetDateTime(long.Parse(jToken.ToString()));
            }
            catch
            {

            }
        }

        private void ProcessMatchData(JToken jToken)
        {
            if (jToken[0].Type == JTokenType.Array)
            {
                jToken = jToken[0];
            }
            else if (jToken[0].Type == JTokenType.String)
            {

            }

            string actionCode = jToken[0].ToString().Substring(0, 1);

            switch (actionCode)
            {
                case "b":
                    UpdateMatchs(jToken[1]);
                    break;
                default:
                    break;
            }
        }

        private void UpdateMatchs(JToken jToken)
        {
            for (int i = 0; i < jToken.Count(); i++)
            {
                ProcessCommandData(jToken[i]);
            }
        }

        private void ProcessCommandData(JToken jToken)
        {
            if (jToken["type"] != null)
            {
                string actionCode = jToken["type"].ToString();
                switch (actionCode)
                {
                    case "m":
                        UpdateMatch(jToken);
                        break;
                    case "o":
                        UpdateProduct(jToken);
                        break;
                    case "dm":
                    case "-m":
                        DeleteMatch(jToken);
                        break;
                    case "ls":
                        UpdateLiveScore(jToken);
                        break;
                    case "do":
                        DeleteProduct(jToken);
                        break;
                    case "-ls":
                        DeleteLiveScore(jToken);
                        break;
                    case "l":
                        UpdateLeague(jToken);
                        break;
                    case "-l":
                        DeleteLeague(jToken);
                        break;
                    case "reset":
                        Reset();
                        break;
                    case "done":
                        Done();
                        break;
                    case "empty":
                        Empty();
                        break;
                    default:
                        break;
                }
            }
            else
            {

            }
        }

        private void DeleteProduct(JToken jToken)
        {
            Product product;
            long oddsid = (long)jToken["oddsid"];
            if (DataStore.Products.TryGetValue(oddsid, out product))
            {
                Common.Functions.UpdateProductStatus(product, "delete");
            }
            Common.Functions.DeleteProduct(oddsid);
        }

        private void UpdateMatch(JToken jToken)
        {
            long matchId = (long)jToken["matchid"];
            bool isAddNew;
            bool isCancelGoal = false;
            bool hasGoal = false;
            bool hasCard = false;
            Match match;

            if (DataStore.Matchs.TryGetValue(matchId, out match))
            {
                isAddNew = false;
            }
            else
            {
                isAddNew = true;
                match = new Match() { MatchId = matchId, FirstShow = DateTime.Now, BeginLive = null, LastTimeHasTGT225 = DateTime.MinValue, LastTime_Ou_050_87 = DateTime.MinValue };
            }

            if (jToken["homeid"] != null) match.HomeId = (long)jToken["homeid"];
            if (jToken["hteamnameen"] != null) match.Home = jToken["hteamnameen"].ToString();
            if (jToken["livehomescore"] != null) 
            {
                if ((int)jToken["livehomescore"] < match.LiveHomeScore)
                {
                    isCancelGoal = true;
                }
                else if ((int)jToken["livehomescore"] > match.LiveHomeScore)
                {
                    hasGoal = true;
                }
                match.LiveHomeScore = (int)jToken["livehomescore"];
            }
            if (jToken["homered"] != null)
            {
                if ((int)jToken["homered"] > match.HomeRed)
                {
                    hasCard = true;
                }
                match.HomeRed = (int)jToken["homered"];
            }

            if (jToken["awayid"] != null) match.AwayId = (long)jToken["awayid"];
            if (jToken["ateamnameen"] != null) match.Away = jToken["ateamnameen"].ToString();
            if (jToken["liveawayscore"] != null)
            {
                if ((int)jToken["liveawayscore"] < match.LiveAwayScore)
                {
                    isCancelGoal = true;
                }
                else if ((int)jToken["liveawayscore"] > match.LiveAwayScore)
                {
                    hasGoal = true;
                }
                match.LiveAwayScore = (int)jToken["liveawayscore"];
            }

            if (jToken["awayred"] != null)
            {
                if ((int)jToken["awayred"] > match.AwayRed)
                {
                    hasCard = true;
                }
                match.AwayRed = (int)jToken["awayred"];
            }

            if (jToken["leagueid"] != null) match.LeagueId = (long)jToken["leagueid"];
            if (jToken["leaguenameen"] != null) match.League = jToken["leaguenameen"].ToString();
            if (DataStore.Leagues.ContainsKey(match.LeagueId)) match.League = DataStore.Leagues[match.LeagueId];
            if (jToken["kickofftime"] != null) { match.KickoffTime = Common.Functions.GetDateTime((long)jToken["kickofftime"] * 1000); match.KickoffTimeNumber = (long)jToken["kickofftime"]; }
            if (jToken["showtimedt"] != null) match.Showtimedt = Common.Functions.GetDateTime((long)jToken["showtimedt"] * 1000);
            if (jToken["globalshowtime"] != null)
            {
                DateTime globalShowtime = Common.Functions.GetDateTime((long)jToken["globalshowtime"] * 1000);
                if (globalShowtime != match.GlobalShowtime)
                {
                    match.CountGlobalShowtimeChange++;
                    if (!DataStore.GlobalShowtimeHistories.ContainsKey(match.MatchId))
                    {
                        DataStore.GlobalShowtimeHistories.Add(match.MatchId, new List<GlobalShowtimeHistory>());
                    }
                    DataStore.GlobalShowtimeHistories[match.MatchId].Add(new GlobalShowtimeHistory()
                    {
                        GlobalShowtime = globalShowtime,
                        UpdateTime = DateTime.Now
                    });
                }
                match.GlobalShowtime = globalShowtime;
            }
            if (jToken["livetimer"] != null) match.LiveTimer = Common.Functions.GetDateTime((long)jToken["livetimer"] * 1000).AddMinutes(-1);

            if (jToken["csstatus"] != null) match.Csstatus = (int)jToken["csstatus"];
            if (jToken["isht"] != null) match.IsHT = (bool)jToken["isht"];
            if (jToken["liveperiod"] != null) match.LivePeriod = (int)jToken["liveperiod"];
            if (jToken["ismainmarket"] != null) match.IsMainMarket = (bool)jToken["ismainmarket"];
            if (jToken["injurytime"] != null) match.InjuryTime = (int)jToken["injurytime"];
            if (jToken["goalteam"] != null) match.GoalTeam = jToken["goalteam"].ToString();
            if (jToken["delaylive"] != null) match.DelayLive = (int)jToken["delaylive"];
            if (jToken["showtime"] != null) match.ShowTime = jToken["showtime"].ToString();

            if (jToken["mc_parlay"] != null) match.McParlay = (int)jToken["mc_parlay"];
            if (jToken["mc_special"] != null) match.McSpecial = (int)jToken["mc_special"];
            if (jToken["mc_specialParlay"] != null) match.McSpecialParlay = (int)jToken["mc_specialParlay"];

            if (jToken["istest"] != null) match.IsTest = (bool)jToken["istest"];
            if (jToken["isneutral"] != null) match.IsNeutral = (int)jToken["isneutral"];
            if (jToken["movebo3down"] != null) match.MoveBo3Down = (bool)jToken["movebo3down"];
            if (jToken["isstartingsoon"] != null) match.IsStartingSoon = (bool)jToken["isstartingsoon"];
            if (jToken["bestofmap"] != null) match.BestOfMap = (int)jToken["bestofmap"];
            if (jToken["isfulltime"] != null) match.IsFullTime = (bool)jToken["isfulltime"];
            if (jToken["ispen"] != null) match.IsPen = (bool)jToken["ispen"];
            if (jToken["haslive"] != null) match.HasStreaming = jToken["haslive"].ToString() != "0";
            if (jToken["matchcode"] != null)
            {
                string matchCode = jToken["matchcode"].ToString();
                if (matchCode != match.MatchCode)
                {
                    match.CountMatchCodeChange++;
                    if (!DataStore.MatchCodeHistories.ContainsKey(match.MatchId))
                    {
                        DataStore.MatchCodeHistories.Add(match.MatchId, new List<MatchCodeHistory>());
                    }
                    DataStore.MatchCodeHistories[match.MatchId].Add(new MatchCodeHistory()
                    {
                        MathCode = matchCode,
                        UpdateTime = DateTime.Now
                    });
                }
            }

            if (match.HasStreaming)
            {
                if (match.TimeHasStreaming == DateTime.MinValue)
                {
                    match.TimeHasStreaming = DateTime.Now;
                }

                if (match.FirstTimeHasStreaming == DateTime.MinValue)
                {
                    match.FirstTimeHasStreaming = DateTime.Now;
                }
            }

            if (match.IsHT) match.HasHt = true;


            //if (!match.IsMainMarket)
            //{
            //    return;
            //}

            if (isCancelGoal)
            {
                Common.Functions.CancelGoal(match.MatchId);
                Common.Functions.WriteJsonObjectData<JToken>(
                    "ErrorLog\\cancleGoal_" + Common.Functions.GetDimDatetime(DateTime.Now),
                    jToken
                );
            }

            if (hasGoal)
            {
                Common.Functions.UpdateGoalHistory(match);
            }

            if (hasCard)
            {
                Common.Functions.UpdateCardHistory(match);
            }

            if (match.Csstatus == 2 || match.IsPen)
            {
                UpdatePenHistory(match);
            }

            if (match.IsHT) match.BeginHT = DateTime.Now;
            if (!match.IsHT && match.LivePeriod == 2) match.EndHT = DateTime.Now;

            if ((match.LivePeriod != 0 || match.IsHT) && match.BeginLive == null)
            {
                match.BeginLive = DateTime.Now;
            }

            if(isAddNew)
            {
                match.FirstShowIndex = GetFirstShowIndex(match);
                DataStore.Matchs.Add(matchId, match);
            }
            else
            {
                DataStore.Matchs[matchId] = match;
            }
        }

        private int GetFirstShowIndex(Match match)
        {
            if (match.League.Trim().Length == 0
               || match.League.ToLower().Contains("saba soccer pingoal")
               || match.League.ToLower().Contains("e-football")
               || match.League.ToUpper().Contains("SABA")
               || match.League.ToUpper().Contains(" - CORNERS")
               || match.League.ToUpper().Contains("SABA")
               || match.Home.ToLower().Contains("(pen)"))
            {
                return 0;
            }

            int dayOfFirstShow = Common.Functions.DayOfFirstShow(match.FirstShow);
            if (!DataStore.FirstShowIndex.ContainsKey(dayOfFirstShow))
            {
                DataStore.FirstShowIndex.Add(dayOfFirstShow, new List<KeyValuePair<DateTime, int>>());
                DateTime initFirstShow = match.FirstShowBlock;
                DataStore.FirstShowIndex[dayOfFirstShow].Add(new KeyValuePair<DateTime, int>(initFirstShow, 0));
            }

            int newIndex = 0;
            if (DataStore.FirstShowIndex[dayOfFirstShow].Count == 1)
            {
                newIndex = 1;
            }
            else
            {
                KeyValuePair<DateTime, int> lastItem = DataStore.FirstShowIndex[dayOfFirstShow].Last();
                if (match.FirstShowBlock <= lastItem.Key.AddMinutes(14))
                {
                    newIndex = lastItem.Value;
                }
                else
                {
                    newIndex = lastItem.Value + 1;
                }
            }

            DataStore.FirstShowIndex[dayOfFirstShow].Add(new KeyValuePair<DateTime, int>(
                match.FirstShowBlock,
                newIndex
            ));

            return newIndex;
        }

        private void UpdatePenHistory(Match match)
        {
            int minute = (int)match.TimeSpanFromStart.TotalMinutes;
            int liverPeriod = match.LivePeriod;

            if (!DataStore.PenHistories.ContainsKey(match.MatchId))
            {
                DataStore.PenHistories.Add(match.MatchId, new List<PenHistory>());
            }

            if (!DataStore.PenHistories[match.MatchId].Any(item => item.LivePeriod == liverPeriod && item.Minute == minute))
            {
                DataStore.PenHistories[match.MatchId].Add(new PenHistory() {
                    Minute = minute,
                    LivePeriod = liverPeriod
                });
            }

        }

        private void UpdateProduct(JToken jToken)
        {
            long oddsid = (long)jToken["oddsid"];
            bool isAddNew;
            Product product;
            if(DataStore.Products.TryGetValue(oddsid, out product))
            {
                isAddNew = false;
            }
            else
            {
                isAddNew = true;
                product = new Product() { OddsId = oddsid };
            }
            
            if (jToken["matchid"] != null) product.MatchId = (long)jToken["matchid"];
            if (jToken["bettype"] != null)
            {
                try
                {
                    product.Bettype = (Common.Enums.BetType)(int)jToken["bettype"];
                }
                catch
                {
                    product.Bettype = Enums.BetType.Unknown;
                }
            }
            if (jToken["oddsstatus"] != null)
            {
                product.OddsStatus = jToken["oddsstatus"].ToString();
            }
            
            if (jToken["hdp1"] != null) product.Hdp1 = (float)jToken["hdp1"];
            if (jToken["hdp2"] != null) product.Hdp2 = (float)jToken["hdp2"];
            if (jToken["maxbet"] != null) product.MaxBet = (int)jToken["maxbet"];
            if (jToken["odds1a"] != null) product.Odds1a = (float)jToken["odds1a"];
            if (jToken["odds2a"] != null) product.Odds2a = (float)jToken["odds2a"];
            if (jToken["com1"] != null) product.Com1 = (float)jToken["com1"];
            if (jToken["comx"] != null) product.Comx = (float)jToken["comx"];
            if (jToken["com2"] != null) product.Com2 = (float)jToken["com2"];

            //if (! (product.Bettype == Common.Enums.BetType.FullTimeOverUnder ||
            //    product.Bettype == Common.Enums.BetType.FirstHalfOverUnder ||
            //    product.Bettype == Common.Enums.BetType.FullTimeHandicap ||
            //    product.Bettype == Enums.BetType.FirstHalfHandicap) ) return;

            if (jToken["oddsstatus"] != null)
            {
                Common.Functions.UpdateProductStatus(product, isAddNew ? "insert" : "update");
            }

            if (isAddNew)
            {
                DataStore.Products.Add(oddsid, product);
                Match match;


                if (DataStore.Matchs.TryGetValue(product.MatchId, out match) && match.LivePeriod >= 0)
                {
                    if (!DataStore.ProductIdsOfMatch.ContainsKey(product.MatchId))
                    {
                        DataStore.ProductIdsOfMatch.Add(product.MatchId, new List<ProductIdLog>());
                    }

                    if (!DataStore.ProductIdsOfMatch[product.MatchId].Any(item => item.OddsId == product.OddsId))
                    {
                        UpdateAlert21(product);
                        DataStore.ProductIdsOfMatch[product.MatchId].Add(new ProductIdLog() { 
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
            }
            else
            {
                DataStore.Products[oddsid] = product;
            }

            if (product.Bettype == Enums.BetType.FullTimeOverUnder)
            {
                int newOuLine = Common.Functions.GetOverUnderMoneyLine(product.MatchId, product);
                if (newOuLine != 0)
                {
                    int oldOuLine = DataStore.Matchs[product.MatchId].OverUnderMoneyLine;
                    if (newOuLine != oldOuLine)
                    {
                        DataStore.Matchs[product.MatchId].OverUnderMoneyLines.Add(newOuLine);
                    }
                    DataStore.Matchs[product.MatchId].OverUnderMoneyLine = newOuLine;
                }

            }

            Common.Functions.UpdateFinishPriceOverUnderScoreTimes(product.MatchId, product);
            
            //UpdateOuLifeTimeHistories(product, isAddNew);
            //UpdateOuLifeTimeFirstHalfHistories(product, isAddNew);
            //UpdateHandicapLifeTimeHistoies(product, isAddNew);
        }

        private void UpdateAlert21(Product product)
        {
            try
            {
                if (product.Bettype != Enums.BetType.FirstHalfOverUnder
                    && product.Bettype != Enums.BetType.FullTimeOverUnder)
                    return;

                int hdp = (int)(product.Hdp1 * 100);
                int maxHdp = DataStore.ProductIdsOfMatch[product.MatchId]
                    .Where(item => item.ProductBetType == product.Bettype)
                    .Max(item => item.Hdp);
                if (hdp > maxHdp)
                {
                    if (!DataStore.Alert_Wd21.ContainsKey(product.MatchId))
                    {
                        DataStore.Alert_Wd21.Add(product.MatchId, new Alert(false, true));
                    }
                    DataStore.Alert_Wd21[product.MatchId] = new Alert(false, true);
                }
            }
            catch
            {

            }
        }

        private string CalculateProductIdHdp(Product product)
        {
            if(product.Bettype == Enums.BetType.FirstHalfHandicap || product.Bettype == Enums.BetType.FullTimeHandicap)
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


        private void DeleteMatch(JToken jToken)
        {
            Common.Functions.DeleteMatch((long)jToken["matchid"]);
        }

        private void UpdateLiveScore(JToken jToken)
        {

        }

        private void DeleteLiveScore(JToken jToken)
        {

        }

        private void UpdateLeague(JToken jToken)
        {
            long leagueId = 0;
            string league = "";
            if (jToken["leagueid"] != null) leagueId = (long)jToken["leagueid"];
            if (jToken["leaguenameen"] != null) league = jToken["leaguenameen"].ToString();

            if (!DataStore.Leagues.ContainsKey(leagueId))
            {
                DataStore.Leagues.Add(leagueId, league);
            }
        }

        private void DeleteLeague(JToken jToken)
        {

        }

        private void Reset()
        {

        }

        private void Done()
        {

        }

        private void Empty()
        {

        }
        
        private void ResetMatchs(JToken jToken)
        {
            string actionCode_0 = jToken[0].ToString();
            string actionCode_1 = jToken[1].ToString();

            if (actionCode_0 == "c2")
            {
                UpdateMatchs(jToken[2]);
            }
            else if (actionCode_0 == "c3")
            {
                UpdateMatchs(jToken[2]);
            }
        }

        private void Init(JToken jToken)
        {
            try
            {
                //DataStore.GlobalDateTime = Common.Functions.GetDateTime(long.Parse(jToken["t"].ToString()));
            }
            catch
            {

            }
            
        }

        #region Archive
        //private void UpdateHandicapLifeTimeHistoies(Product product, bool isAddNew)
        //{
        //    if (product.Bettype != Common.Enums.BetType.FullTimeHandicap) return;
        //    if (!DataStore.Matchs.ContainsKey(product.MatchId)) return;
        //    if (!DataStore.HandicapLifeTimeHistories.ContainsKey(product.MatchId))
        //    {
        //        DataStore.HandicapLifeTimeHistories.Add(product.MatchId, new Dictionary<long, List<HandicapLifeTimeHistory>>());
        //    }

        //    Match match = DataStore.Matchs[product.MatchId];

        //    HandicapLifeTimeHistory history = new HandicapLifeTimeHistory()
        //    {
        //        Odds1a = product.Odds1a * 100,
        //        Odds2a = product.Odds2a * 100,
        //        Hdp1 = product.Hdp1,
        //        Hdp2 = product.Hdp2,
        //        TimeSpanFromStart = match.TimeSpanFromStart
        //    };

        //    if (isAddNew)
        //    {
        //        DataStore.HandicapLifeTimeHistories[product.MatchId].Add(product.OddsId, new List<HandicapLifeTimeHistory>());
        //    }

        //    DataStore.HandicapLifeTimeHistories[product.MatchId][product.OddsId].Add(history);
        //}

        //private void UpdateOuLifeTimeFirstHalfHistories(Product product, bool isAddNew)
        //{
        //    if (product.Bettype != Common.Enums.BetType.FirstHalfOverUnder) return;
        //    if (!DataStore.Matchs.ContainsKey(product.MatchId)) return;
        //    if (!DataStore.OuLifeTimeFirstHalfHistories.ContainsKey(product.MatchId))
        //    {
        //        DataStore.OuLifeTimeFirstHalfHistories.Add(product.MatchId, new Dictionary<int, List<OuLifeTimeHistory>>());
        //    }

        //    Match match = DataStore.Matchs[product.MatchId];
        //    int score = (int)(((product.Hdp1 + product.Hdp2) - (match.LiveAwayScore + match.LiveHomeScore)) * 100);

        //    OuLifeTimeHistory history = new OuLifeTimeHistory()
        //    {
        //        Odds1a = product.Odds1a * 100,
        //        TimeSpanFromStart = match.TimeSpanFromStart
        //    };

        //    if (!DataStore.OuLifeTimeFirstHalfHistories[product.MatchId].ContainsKey(score))
        //    {
        //        DataStore.OuLifeTimeFirstHalfHistories[product.MatchId].Add(score, new List<OuLifeTimeHistory>());
        //    }

        //    DataStore.OuLifeTimeFirstHalfHistories[product.MatchId][score].Add(history);
        //}

        //private void UpdateOuLifeTimeHistories(Product product, bool isAddNew)
        //{

        //    if (product.Bettype != Common.Enums.BetType.FullTimeOverUnder) return;
        //    if (!DataStore.Matchs.ContainsKey(product.MatchId)) return;
        //    if (!DataStore.OuLifeTimeHistories.ContainsKey(product.MatchId))
        //    {
        //        DataStore.OuLifeTimeHistories.Add(product.MatchId, new Dictionary<int, List<OuLifeTimeHistory>>());
        //    }

        //    Match match = DataStore.Matchs[product.MatchId];
        //    int score = (int)(((product.Hdp1 + product.Hdp2) - (match.LiveAwayScore + match.LiveHomeScore)) * 100);

        //    OuLifeTimeHistory history = new OuLifeTimeHistory()
        //    {
        //        Odds1a = product.Odds1a * 100,
        //        TimeSpanFromStart = match.TimeSpanFromStart
        //    };

        //    if (!DataStore.OuLifeTimeHistories[product.MatchId].ContainsKey(score))
        //    {
        //        DataStore.OuLifeTimeHistories[product.MatchId].Add(score, new List<OuLifeTimeHistory>());
        //    }

        //    DataStore.OuLifeTimeHistories[product.MatchId][score].Add(history);

        //}

        #endregion

    }
}
