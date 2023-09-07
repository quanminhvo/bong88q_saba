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
    public class UpdateMaxBetService
    {
        private int _milisecondPerRequest;
        public UpdateMaxBetService(int milisecondPerRequest)
        {
            _milisecondPerRequest = milisecondPerRequest;
        }

        private void ExecuteCore()
        {
            if (DataStore.Config == null) return;
            if (DataStore.Config.cookie == null || DataStore.Config.cookie.Length == 0) return;
            if (DataStore.Config.bong88Url == null || DataStore.Config.bong88Url.Length == 0) return;

            while (true)
            {
                try
                {

                    List<string> selectedLeagues = DataStore.LeagueFilter.Split('\n').ToList();
                    for (int i = 0; i < selectedLeagues.Count; i++)
                    {
                        selectedLeagues[i] = selectedLeagues[i].ToLower();
                    }

                    List < Match > matchs = DataStore.Matchs.Values.Where(item => true
                        //(!DataStore.ShowLiveMatchsOnlyChecked || ((DataStore.ShowLiveMatchsOnlyChecked && item.LivePeriod > 0) || item.IsHT))
                        && (
                            (DataStore.ShowFinishedMatchsChecked && item.LivePeriod == -1)
                            || (DataStore.ShowRunningMatchsH1Checked && item.LivePeriod == 1)
                            || (DataStore.ShowRunningMatchsH2Checked && item.LivePeriod == 2)
                            || (DataStore.ShowRunningMatchsHtChecked && item.IsHT)
                            || (DataStore.ShowNoShowMatchsChecked && item.IsNoShowMatch)
                        )
                        //&& item.GlobalShowtime.Date >= DataStore.MatchFilterDateTimeFrom
                        //&& item.GlobalShowtime.Date <= DataStore.MatchFilterDateTimeTo
                        && item.Home != null
                        && item.Away != null
                        && item.League != null
                        && item.League.Trim().Length > 0
                        && !item.League.ToLower().Contains("e-football")
                        && !item.League.ToLower().Contains("saba soccer pingoal")
                        && item.MinHdpFt >= DataStore.MinHdp && item.MaxHdpFt <= DataStore.MaxHdp
                        && !DataStore.Blacklist.Any(p => p.IsActive && p.League == item.League)
                        && (item.OverUnderMoneyLine == DataStore.OuMoneyLine || DataStore.OuMoneyLine == 0)
                        && (selectedLeagues.Contains(item.League.ToLower()) || DataStore.LeagueFilter.Length == 0)
                        && ((!DataStore.ShowSabaOnly && !item.League.ToUpper().Contains("SABA")) || (DataStore.ShowSabaOnly && item.League.ToUpper().Contains("SABA")))
                        && (
                            !item.League.Contains(" - ")
                            || (!item.League.ToUpper().Contains(" - CORNERS")
                                && !item.League.ToUpper().Contains(" - BOOKING")
                                && !item.League.ToUpper().Contains("- WINNER")
                                && !item.League.ToUpper().Contains("- TOTAL CORNER & TOTAL GOAL")
                                && !item.League.ToUpper().Contains("- SUBSTITUTION")
                                && !item.League.ToUpper().Contains("- GOAL KICK")
                                && !item.League.ToUpper().Contains("- OFFSIDE")
                                && !item.League.ToUpper().Contains("- THROW IN")
                                && !item.League.ToUpper().Contains("- FREE KICK")
                                && !item.League.ToUpper().Contains("- 1ST HALF VS 2ND HALF")
                                && !item.League.ToUpper().Contains("- RED CARD")
                                && !item.League.ToUpper().Contains("- OWN GOAL")
                                && !item.League.ToUpper().Contains("- PENALTY")
                                && !item.League.ToUpper().Contains("- TOTAL GOALS MINUTES")
                                && !item.League.ToUpper().Contains("- WHICH TEAM TO KICK OFF")
                                && !item.League.ToUpper().Contains("SABA")
                            )
                            || (DataStore.ShowCornersChecked && item.League.ToUpper().Contains(" - CORNERS"))
                            || (DataStore.ShowSabaOnly && item.League.ToUpper().Contains("SABA"))
                        )
                        && (
                            DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.All
                            || (DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.Live && item.HasStreaming)
                            || (DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.NoneLive && !item.HasStreaming)
                        )

                        && !item.Home.ToLower().Contains("(pen)")
                    ).ToList();
                    UpdateMatchMaxBet(matchs);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Thread.Sleep(_milisecondPerRequest);
                }

            }
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

        private void UpdateMatchMaxBet(List<Match> matchs)
        {
            matchs = matchs.Where(item => item.MatchId == DataStore.MatchIdNeedGetMaxBetRequest).ToList();

            for (int i = 0; i < matchs.Count; i++)
            {
                if (!DataStore.MatchMaxBetRequest.ContainsKey(matchs[i].MatchId))
                {
                    DataStore.MatchMaxBetRequest.Add(matchs[i].MatchId, new List<List<List<int>>>());
                    List<List<int>> dataH1 = new List<List<int>>();
                    List<List<int>> dataH2 = new List<List<int>>();
                    List<List<int>> dataH0 = new List<List<int>>();

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

                    DataStore.MatchMaxBetRequest[matchs[i].MatchId].Add(dataH0);
                    DataStore.MatchMaxBetRequest[matchs[i].MatchId].Add(dataH1);
                    DataStore.MatchMaxBetRequest[matchs[i].MatchId].Add(dataH2);
                }

                List<Product> products = GetFirstOverUnderProduct(matchs[i].MatchId, matchs[i].LivePeriod);
                int currentMinute = (int)matchs[i].TimeSpanFromStart.TotalMinutes;
                if (currentMinute <= 0) currentMinute = 0;
                if (currentMinute >= 55) currentMinute = 55;
                if (matchs[i].LivePeriod == 2) currentMinute += 45;

                try
                {
                    if (products.Count == 2)
                    {
                        DataStore.MatchMaxBetRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute] = GetMaxBet(products[0], matchs[i]);
                        Thread.Sleep(_milisecondPerRequest);
                        DataStore.MatchMaxBetRequest[matchs[i].MatchId][matchs[i].LivePeriod][1][currentMinute] = GetMaxBet(products[1], matchs[i]);
                        Thread.Sleep(_milisecondPerRequest);
                    }
                    else if (products.Count == 1)
                    {
                        DataStore.MatchMaxBetRequest[matchs[i].MatchId][matchs[i].LivePeriod][0][currentMinute] = GetMaxBet(products[0], matchs[i]);
                        Thread.Sleep(_milisecondPerRequest);
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }

        private int GetMaxBet(Product ouProduct, Match match)
        {
            ProcessBetModel model = Common.Functions.ConstructProcessBetOverUnderModelV2(
                ouProduct,
                match,
                3,
                true,
                true
            );
            JToken ticket = new ProcessBetService().GetTicket(model, DataStore.Config.cookie, DataStore.Config.getTicketUrl);
            int maxbet = 0;
            if (ticket != null
                && ticket["Data"] != null
                && ticket["Data"][0] != null
                && int.TryParse(ticket["Data"][0]["Maxbet"].ToString().Replace(",", "").Replace(".", ""), out maxbet))
            {
                return maxbet;
            }
            return 0;
        }

        private List<Product> GetFirstOverUnderProduct(long matchId, int livePeriod)
        {
            List<Product> result = new List<Product>();
            List<Product> productsOfMatch;
            List<Product> productOuFhsOfMatch;

            productsOfMatch = DataStore.Products.Values
                .Where(item => item.MatchId == matchId && item.Bettype == Enums.BetType.FullTimeOverUnder)
                .OrderBy(item => item.Hdp1)
                .ToList();

            productOuFhsOfMatch = DataStore.Products.Values
                .Where(item => item.MatchId == matchId && item.Bettype == Enums.BetType.FirstHalfOverUnder)
                .OrderBy(item => item.Hdp1)
                .ToList();

            if (livePeriod == 1)
            {
                if (productOuFhsOfMatch.Count >= 2)
                {
                    result.Add(productOuFhsOfMatch[0]);
                    result.Add(productOuFhsOfMatch[1]);
                }
                else if (productOuFhsOfMatch.Count == 1)
                {
                    result.Add(productOuFhsOfMatch[0]);
                }
            }

            if (livePeriod == 2)
            {
                if (productsOfMatch.Count >= 2)
                {
                    result.Add(productsOfMatch[0]);
                    result.Add(productsOfMatch[1]);
                }
                else if (productsOfMatch.Count == 1)
                {
                    result.Add(productsOfMatch[0]);
                }
            }


                


            return result;

        }

    }
}
