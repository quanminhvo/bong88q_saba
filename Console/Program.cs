using Console.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Console
{
    class Program
    {

        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string getSidUrl = "https://agnj3.5566688.com/socket.io/?gid=85607b91f4bda30a&token=5c2c821f-2691-4798-9b42-c9dc7279c53e&id=33181355&rid=2&EIO=3&transport=polling&t=Mi6nZH9";
            string sid = GetSidFromResponse(getSidUrl);

            string webSocketUrl = "wss://agnj3.5566688.com/socket.io/?gid=85607b91f4bda30a&token=5c2c821f-2691-4798-9b42-c9dc7279c53e&id=33181355&rid=2&EIO=3&transport=websocket&sid=" + sid;

            using (var ws = new WebSocket(webSocketUrl))
            {
                ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

                ws.OnMessage += (sender, e) =>
                {
                    System.Console.Clear();
                    System.Console.WriteLine(e.Data);
                    if (e.Data == "3probe")
                    {
                        ws.Send("5");
                        ws.Send("42[\"subscribe\",\"menu\",[{\"id\":\"c0\",\"rev\":\"\",\"condition\":{}}]]");
                        //ws.Send("42[\"subscribe\",\"odds\",{\"id\":\"c1\",\"rev\":0,\"condition\":{\"mini\":1,\"bettype\":[1,20,501]}}]");
                        ws.Send("42[\"subscribe\",\"odds\",{\"id\":\"c2\",\"rev\":0,\"condition\":{\"sporttype\":1,\"marketid\":\"L\",\"bettype\":[1,3,5,7,8,15,301,302,303,304],\"sorting\":\"n\"}}]");
                        //ws.Send("42[\"subscribe\",\"odds\",{\"id\":\"c3\",\"rev\":0,\"condition\":{\"sporttype\":1,\"marketid\":\"T\",\"bettype\":[1,3,5,7,8,15,301,302,303,304],\"sorting\":\"n\"}}]");
                    }
                    else
                    {
                        try
                        {
                            if (e.Data.Length > 6)
                            {
                                ProcessWebSocketData(JToken.Parse(e.Data.Substring(2, e.Data.Length - 2)));
                            }

                        }
                        catch { }
                    }

                };
                ws.Connect();
                ws.Send("2probe");
                SetInterval(() => ws.Send("2"), 25000);
                System.Console.ReadKey(true);
            }

        }

        static DateTime GetDateTime(long milisecond)
        {
            var timeZoneSpan = TimeZoneInfo.Local.GetUtcOffset(new DateTime(2006, 6, 1));

            return new DateTime(1970, 1, 1)
                .AddMilliseconds(1559286481283)
                .AddSeconds(timeZoneSpan.TotalSeconds);
        }

        static void ProcessWebSocketData(JToken jToken)
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
                    break;
            }
        }

        static void UpdateGlobalTime(JToken jToken)
        {

        }

        static void ProcessMatchData(JToken jToken)
        {
            string actionCode = jToken[0].ToString();
            switch (actionCode)
            {
                case "r2":
                    UpdateLiveMatchs(jToken[1]);
                    break;
                case "r3":
                    UpdateGoingToLiveMatchs(jToken[1]);
                    break;
                default:
                    break;
            }
        }

        static void UpdateLiveMatchs(JToken jToken)
        {

        }

        static void UpdateGoingToLiveMatchs(JToken jToken)
        {

        }

        static void ResetMatchs(JToken jToken)
        {
            string actionCode_0 = jToken[0].ToString();
            string actionCode_1 = jToken[1].ToString();

            if(actionCode_0 == "c2" && actionCode_1 == "r2")
            {
                for(int i=0; i<jToken[2].Count(); i++)
                {

                }
            }
            else if (actionCode_0 == "c3" && actionCode_1 == "r3")
            {

            }

        }

        static void ResetLiveMatchs(JToken jToken)
        {

        }

        static void Init(JToken jToken)
        {

        }
        

        static string GetSidFromResponse(string url)
        {
            string result = "";
            using (var wb = new WebClient())
            {

                string response = wb.DownloadString(url);

                int start = response.IndexOf("\"sid\":") + 7;
                int end = response.IndexOf(",\"upgrades\"") - 1;

                result =  response.Substring(start, end - start);
            }
            return result;
        }

        static void SetInterval(Action action, int milisecond)
        {
            //Thread executeThread;
            //executeThread = new Thread(() =>
            //{
            Thread.Sleep(milisecond);
            action();
            SetInterval(action, milisecond);
            //});
            //executeThread.IsBackground = true;
            //executeThread.Start();
        }




























        static private void Test()
        {
            // Initialize the Chrome Driver
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://www.bong88.com/login888.aspx?IsSSL=1");

                driver.FindElementById("txtID").SendKeys("qz3707a3013");
                driver.FindElementById("txtPW").SendKeys("Tttt@2222");
                driver.FindElementByCssSelector("a.login_btn > span").Click();

                List<MatchToWatch> matchsToWatch = new List<MatchToWatch>();
                matchsToWatch.Add(new MatchToWatch()
                {
                    TeamHost = "Wellington Phoenix FC",
                    TeamGuest = "Central Coast Mariners"

                });

                int errorCount = 0;

                while (true)
                {
                    try
                    {
                        //Thread.Sleep(1000);
                        ReadOnlyCollection<IWebElement> tables = driver.FindElementsByCssSelector("div.oddsTable.hdpou-a.sport1");
                        if (tables.Count < 2) continue;

                        IWebElement liveTable = GetLiveTable(tables);
                        ReadOnlyCollection<IWebElement> MatchAreas = GetMatchAreasOfLiveTable(liveTable);
                        List<DataModels.Match> recoredMatchs = new List<DataModels.Match>();

                        foreach (IWebElement matchArea in MatchAreas)
                        {
                            ReadOnlyCollection<IWebElement> teamsOfMath = GetTeamsOfMath(matchArea);
                            DataModels.Match match = new DataModels.Match();
                            Team teamHost = GetTeamName(teamsOfMath[0]);
                            Team teamGuest = GetTeamName(teamsOfMath[1]);
                            match.TeamHost = teamHost.TeamName;
                            match.TeamGuest = teamGuest.TeamName;

                            if (!IsWatchingMatch(match, matchsToWatch)) continue;

                            string timePlaying = GetTimePlayingOfMatch(matchArea);
                            string score = GetScoreOfMatch(matchArea);



                            if (teamHost.IsStrongerTeam)
                            {
                                match.TeamStronger = teamHost.TeamName;
                            }
                            else if (teamGuest.IsStrongerTeam)
                            {
                                match.TeamStronger = teamGuest.TeamName;
                            }

                            //if (match.TeamHost != "Malaysia") continue;
                            //if (match.TeamGuest != "Vietnam") continue;

                            ReadOnlyCollection<IWebElement> betElements = GetBetsOfMath(matchArea);

                            foreach (IWebElement betElement in betElements)
                            {
                                ReadOnlyCollection<IWebElement> betAreas = GetBetAreasOfBet(betElement);
                                Bet bet = DeserializeBetAreasOfOneBet(betAreas);
                                bet.TimePlaying = timePlaying;
                                bet.Score = score;
                                bet.RecordedDateTime = DateTime.Now;

                                match.Bets.Add(bet);
                            }

                            recoredMatchs.Add(match);
                        }

                        matchsToWatch = ProcessRecoredMatch(recoredMatchs, matchsToWatch);
                    }
                    catch
                    {
                        errorCount++;
                    }
                }
            }
        }

        static bool IsBetHistoryChange(Bet betA, Bet betB)
        {
            if (betA.FullTimeHandicap_GuestRate != betB.FullTimeHandicap_GuestRate) return true;
            if (betA.FullTimeHandicap_HostRate != betB.FullTimeHandicap_HostRate) return true;
            if (betA.FullTimeHandicap != betB.FullTimeHandicap) return true;

            if (betA.FullTimeOverUnder_GuestRate != betB.FullTimeOverUnder_GuestRate) return true;
            if (betA.FullTimeOverUnder_HostRate != betB.FullTimeOverUnder_HostRate) return true;
            if (betA.FullTimeOverUnder != betB.FullTimeOverUnder) return true;

            if (betA.Score != betB.Score) return true;

            return false;
        }

        static List<MatchToWatch> ProcessRecoredMatch(List<DataModels.Match> recoredMatchs, List<MatchToWatch> matchsToWatch)
        {
            for (int i = 0; i < recoredMatchs.Count; i++)
            {
                for (int j = 0; j < matchsToWatch.Count; j++)
                {
                    if (recoredMatchs[i].TeamHost == matchsToWatch[j].TeamHost
                        && recoredMatchs[i].TeamGuest == matchsToWatch[j].TeamGuest)
                    {

                        Bet firstRecordedBet = recoredMatchs[i].Bets[0];

                        if(matchsToWatch[j].BetChangeHistory.Count == 0)
                        {
                            matchsToWatch[j].BetChangeHistory.Add(firstRecordedBet);
                        }
                        else
                        {
                            Bet lastestBetHistory = matchsToWatch[j].BetChangeHistory[matchsToWatch[j].BetChangeHistory.Count - 1];
                            if (IsBetHistoryChange(firstRecordedBet, lastestBetHistory))
                            {
                                firstRecordedBet.TimeSpanFromLastChanges = firstRecordedBet.RecordedDateTime.Subtract(lastestBetHistory.RecordedDateTime);
                                matchsToWatch[j].BetChangeHistory.Add(firstRecordedBet);
                            }
                        }

                    }
                }
            }
            return matchsToWatch;
        }

        static private bool IsWatchingMatch(DataModels.Match match, List<MatchToWatch> matchsToWatch)
        {
            for(int i=0; i<matchsToWatch.Count; i++)
            {
                if (match.TeamHost == matchsToWatch[i].TeamHost
                    && match.TeamGuest == matchsToWatch[i].TeamGuest)
                    return true;
            }
            return false;
        }

        static private void Execute()
        {
            // Initialize the Chrome Driver
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://www.bong88.com/login888.aspx?IsSSL=1");

                driver.FindElementById("txtID").SendKeys("qz3707a3013");
                driver.FindElementById("txtPW").SendKeys("Tttt@2222");
                driver.FindElementByCssSelector("a.login_btn > span").Click();

                while (true)
                {
                    Thread.Sleep(1000);
                    //var element = driver.FindElementById("mainArea");     
                    //.GetAttribute("innerHTML");
                    ReadOnlyCollection<IWebElement> tables = driver.FindElementsByCssSelector("div.oddsTable.hdpou-a.sport1");
                    if (tables.Count < 2) continue;

                    foreach (IWebElement table in tables)
                    {
                        string innerHtml = table.GetAttribute("innerHTML");

                        System.IO.File.WriteAllText(
                            DateTime.Now.ToString("hh.mm.ss.ffffff")
                            + ".html",
                            innerHtml
                        );

                    }
                }
            }
        }

        static private string RemoveHtmlComments(string html)
        {
            return Regex.Replace(html, "<!--.*?-->", String.Empty, RegexOptions.Singleline);
        }

        static private IWebElement GetLiveTable(ReadOnlyCollection<IWebElement> tables)
        {
            if (tables.Count < 2) return null;
            return tables[0];
        }

        static private ReadOnlyCollection<IWebElement> GetLeagueGroups(IWebElement liveTable)
        {
            return liveTable.FindElements(By.ClassName("leagueGroup"));
        }

        static private string GetLeagueName(IWebElement leagueGroup)
        {
            IWebElement leagueElement = leagueGroup.FindElement(By.ClassName("leagueName"));
            string innerHTML = leagueElement.GetAttribute("innerHTML");
            return RemoveHtmlComments(innerHTML);
        }

        static private ReadOnlyCollection<IWebElement> GetMatchAreasOfLiveTable(IWebElement liveTable)
        {
            return liveTable.FindElements(By.ClassName("matchArea"));
        }

        static private string GetTimePlayingOfMatch (IWebElement match)
        {
            IWebElement leagueElement = match.FindElement(By.ClassName("timePlaying"));
            string innerHTML = leagueElement.GetAttribute("innerHTML");
            return RemoveHtmlComments(innerHTML);
        }

        static private string GetScoreOfMatch(IWebElement match)
        {
            IWebElement leagueElement = match.FindElement(By.ClassName("score"));
            string innerHTML = leagueElement.GetAttribute("innerHTML");

            //<span>2</span><span>-</span><span>3</span>
            innerHTML = Regex.Replace(innerHTML, "<span>", String.Empty, RegexOptions.Singleline);
            innerHTML = Regex.Replace(innerHTML, "</span>", String.Empty, RegexOptions.Singleline);

            return RemoveHtmlComments(innerHTML);
        }

        static private ReadOnlyCollection<IWebElement> GetTeamsOfMath(IWebElement match)
        {
            return match.FindElements(By.ClassName("team"));
        }

        static private Team GetTeamName(IWebElement team)
        {
            IWebElement teamNameElement = team.FindElement(By.ClassName("name-pointer"));
            Team result = new Team();

            string cssClass = teamNameElement.GetAttribute("class");
            result.TeamName = teamNameElement.GetAttribute("title");

            if (cssClass.Contains("accent"))
            {
                result.IsStrongerTeam = true;
            }
            else
            {
                result.IsStrongerTeam = false;
            }

            return result;
        }

        static private ReadOnlyCollection<IWebElement> GetBetsOfMath(IWebElement match)
        {
            return match.FindElements(By.ClassName("multiOdds"));
        }

        static private ReadOnlyCollection<IWebElement> GetBetAreasOfBet(IWebElement Bet)
        {
            return Bet.FindElements(By.ClassName("betArea"));
        }

        static Bet DeserializeBetAreasOfOneBet(ReadOnlyCollection<IWebElement> betAreas)
        {
            Bet bet = new Bet();

            ReadOnlyCollection<IWebElement> spans = betAreas[0].FindElements(By.TagName("span"));
            if (spans.Count > 1)
            {
                bet.FullTimeHandicap = RemoveHtmlComments(spans[0].GetAttribute("innerHTML"));
                bet.FullTimeHandicap_HostRate = float.Parse(spans[1].GetAttribute("innerHTML"));
            }

            spans = betAreas[1].FindElements(By.TagName("span"));
            if (spans.Count > 1)
            {
                if(bet.FullTimeHandicap.Length == 0)
                {
                    bet.FullTimeHandicap = RemoveHtmlComments(spans[0].GetAttribute("innerHTML"));
                }
                bet.FullTimeHandicap_GuestRate = float.Parse(spans[1].GetAttribute("innerHTML"));
            }

            spans = betAreas[2].FindElements(By.TagName("span"));
            if (spans.Count > 1)
            {
                bet.FullTimeOverUnder = RemoveHtmlComments(spans[0].GetAttribute("innerHTML"));
                bet.FullTimeOverUnder_HostRate = float.Parse(spans[1].GetAttribute("innerHTML"));
            }

            spans = betAreas[3].FindElements(By.TagName("span"));
            if (spans.Count > 1)
            {
                bet.FullTimeOverUnder_GuestRate = float.Parse(spans[1].GetAttribute("innerHTML"));
            }
            

            //bet.CapturedDateTime = DateTime.Now;

            return bet;
        }
    }
}
