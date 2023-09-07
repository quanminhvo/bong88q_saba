using App.DataModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.LogicServices
{
    public static class SeleniumService
    {

        public static ReadOnlyCollection<IWebElement> GetLeagueGroups(IWebElement liveTable)
        {
            return liveTable.FindElements(By.ClassName("leagueGroup"));
        }

        public static string GetLeagueName(IWebElement leagueGroup)
        {
            IWebElement leagueElement = leagueGroup.FindElement(By.ClassName("leagueName"));
            string innerHTML = leagueElement.GetAttribute("innerHTML");
            return Common.RemoveHtmlComments(innerHTML);
        }

        public static IWebElement GetLiveTable(ReadOnlyCollection<IWebElement> tables)
        {
            if (tables.Count < 2) return null;
            return tables[0];
        }

        public static ReadOnlyCollection<IWebElement> GetMatchAreasOfLiveTable(IWebElement liveTable)
        {
            return liveTable.FindElements(By.ClassName("matchArea"));
        }

        public static string GetTimePlayingOfMatch(IWebElement match)
        {
            IWebElement leagueElement = match.FindElement(By.ClassName("timePlaying"));
            string innerHTML = leagueElement.GetAttribute("innerHTML");
            return Common.RemoveHtmlComments(innerHTML);
        }

        public static string GetScoreOfMatch(IWebElement match)
        {
            IWebElement leagueElement = match.FindElement(By.ClassName("score"));
            string innerHTML = leagueElement.GetAttribute("innerHTML");

            //<span>2</span><span>-</span><span>3</span>
            innerHTML = Regex.Replace(innerHTML, "<span>", String.Empty, RegexOptions.Singleline);
            innerHTML = Regex.Replace(innerHTML, "</span>", String.Empty, RegexOptions.Singleline);

            return Common.RemoveHtmlComments(innerHTML);
        }

        public static ReadOnlyCollection<IWebElement> GetTeamsOfMath(IWebElement match)
        {
            return match.FindElements(By.ClassName("team"));
        }

        public static ReadOnlyCollection<IWebElement> GetBetsOfMath(IWebElement match)
        {
            return match.FindElements(By.ClassName("multiOdds"));
        }

        public static ReadOnlyCollection<IWebElement> GetBetAreasOfBet(IWebElement bet)
        {
            return bet.FindElements(By.ClassName("betArea"));
        }

        public static ReadOnlyCollection<IWebElement> GetBetButtonOfBet(IWebElement bet)
        {
            return bet.FindElements(By.ClassName("oddsBet"));
        }


        public static Bet DeserializeBetAreasOfOneBet(ReadOnlyCollection<IWebElement> betAreas)
        {
            Bet bet = new Bet();

            ReadOnlyCollection<IWebElement> spans = betAreas[0].FindElements(By.TagName("span"));
            if (spans.Count > 1)
            {
                bet.FullTimeHandicap = Common.RemoveHtmlComments(spans[0].GetAttribute("innerHTML"));
                bet.FullTimeHandicap_HostRate = float.Parse(spans[1].GetAttribute("innerHTML"));
            }

            spans = betAreas[1].FindElements(By.TagName("span"));
            if (spans.Count > 1)
            {
                if (bet.FullTimeHandicap.Length == 0)
                {
                    bet.FullTimeHandicap = Common.RemoveHtmlComments(spans[0].GetAttribute("innerHTML"));
                }
                bet.FullTimeHandicap_GuestRate = float.Parse(spans[1].GetAttribute("innerHTML"));
            }

            spans = betAreas[2].FindElements(By.TagName("span"));
            if (spans.Count > 1)
            {
                bet.FullTimeOverUnder = Common.RemoveHtmlComments(spans[0].GetAttribute("innerHTML"));
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

        public static Team GetTeamName(IWebElement team)
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

        public static void Login(ChromeDriver driver)
        {
            Config config = new Config();
            driver.Navigate().GoToUrl(config.Url);
            driver.FindElementById("txtID").SendKeys(config.Username);
            driver.FindElementById("txtPW").SendKeys(config.Password);
            driver.FindElementByCssSelector("a.login_btn > span").Click();
        }

    }
}
