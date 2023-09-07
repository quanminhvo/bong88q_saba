using BetApp.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BetApp.Services
{
    public  class SeleniumService
    {
        public  IWebElement GetLiveTable(ReadOnlyCollection<IWebElement> tables)
        {
            if (tables.Count < 2) return null;
            return tables[0];
        }
        public  ReadOnlyCollection<IWebElement> GetLeagueGroups(IWebElement liveTable)
        {
            return liveTable.FindElements(By.ClassName("leagueGroup"));
        }
        public  string GetLeagueName(IWebElement leagueGroup)
        {
            IWebElement leagueElement = leagueGroup.FindElement(By.ClassName("leagueName"));
            string innerHTML = leagueElement.GetAttribute("innerHTML");
            return Common.Functions.RemoveHtmlComments(innerHTML);
        }

        public  ReadOnlyCollection<IWebElement> GetMatchAreasOfLiveTable(IWebElement liveTable)
        {
            return liveTable.FindElements(By.ClassName("matchArea"));
        }

        public  string GetTimePlayingOfMatch(IWebElement match)
        {
            IWebElement leagueElement = match.FindElement(By.ClassName("timePlaying"));
            string innerHTML = leagueElement.GetAttribute("innerHTML");
            return Common.Functions.RemoveHtmlComments(innerHTML);
        }

        public  string GetScoreOfMatch(IWebElement match)
        {
            IWebElement leagueElement = match.FindElement(By.ClassName("score"));
            string innerHTML = leagueElement.GetAttribute("innerHTML");

            innerHTML = Regex.Replace(innerHTML, "<span>", String.Empty, RegexOptions.Singleline);
            innerHTML = Regex.Replace(innerHTML, "</span>", String.Empty, RegexOptions.Singleline);

            return Common.Functions.RemoveHtmlComments(innerHTML);
        }

        public  TeamOfMatch GetTeamsOfMath(IWebElement match)
        {
            TeamOfMatch result = new TeamOfMatch();
            ReadOnlyCollection<IWebElement> team = match.FindElements(By.ClassName("team"));

            result.Home = GetTeamName(team[0]);
            result.Away = GetTeamName(team[1]);

            return result;
        }

        private  bool IsStableButton(IWebElement betArea)
        {
            ReadOnlyCollection<IWebElement> indicatorUps = betArea.FindElements(By.ClassName("indicatorUp"));
            ReadOnlyCollection<IWebElement> indicatorDowns = betArea.FindElements(By.ClassName("indicatorDown"));

            if (indicatorUps != null && indicatorUps.Count > 0) return false;
            if (indicatorDowns != null && indicatorDowns.Count > 0) return false;
            return true;
        }

        private  Models.ReadFromWeb.BetCell DeserializeBetCell(IWebElement topBetAreas, IWebElement bottomBetAreas)
        {
            string topBetAreasInnerHtml = topBetAreas.GetAttribute("innerHTML");
            string bottomBetAreasInnerHtml = bottomBetAreas.GetAttribute("innerHTML");
            if (topBetAreasInnerHtml == null || topBetAreasInnerHtml.Length == 0 || bottomBetAreasInnerHtml == null || bottomBetAreasInnerHtml.Length == 0) return null;

            Models.ReadFromWeb.BetCell result = new Models.ReadFromWeb.BetCell();
            result.HomeButton = topBetAreas.FindElement(By.ClassName("oddsBet"));
            result.IsHomeButtonStable = IsStableButton(topBetAreas);

            result.AwayButton = bottomBetAreas.FindElement(By.ClassName("oddsBet"));
            result.IsAwayButtonStable = IsStableButton(bottomBetAreas);

            ReadOnlyCollection<IWebElement> spans = topBetAreas.FindElements(By.TagName("span"));
            if (spans.Count > 1)
            {
                result.EstimatedGoal = Common.Functions.RemoveHtmlComments(spans[0].GetAttribute("innerHTML"));
                result.HomeRate = float.Parse(spans[1].GetAttribute("innerHTML"));
            }

            spans = bottomBetAreas.FindElements(By.TagName("span"));
            if (spans.Count > 1)
            {
                if (result.EstimatedGoal.Length == 0)
                {
                    result.EstimatedGoal = Common.Functions.RemoveHtmlComments(spans[0].GetAttribute("innerHTML"));
                }
                result.AwayRate = float.Parse(spans[1].GetAttribute("innerHTML"));
            }

            return result;
        }

        public  Models.ReadFromWeb.BetRow DeserializeBetRow(IWebElement multiOdds)
        {
            Models.ReadFromWeb.BetRow result = new Models.ReadFromWeb.BetRow();
            ReadOnlyCollection<IWebElement> betAreas = multiOdds.FindElements(By.ClassName("betArea"));

            result.FthdcBetCell = DeserializeBetCell(betAreas[0], betAreas[1]);
            result.FtouBetCell = DeserializeBetCell(betAreas[2], betAreas[3]);

            result.HthdcBetCell = DeserializeBetCell(betAreas[7], betAreas[8]);
            result.HtouBetCell = DeserializeBetCell(betAreas[9], betAreas[10]);

            return result;
        }

        private Models.ReadFromWeb.BetCell FullFillBetCell(Models.ReadFromWeb.BetCell betCell, Models.ReadFromWeb.Match match)
        {
            betCell.Score = match.BetArea.Score;
            betCell.TimePlaying = match.BetArea.TimePlaying;
            betCell.TeamStronger = match.TeamStronger;

            return betCell;
        }

        private  Models.ReadFromWeb.BetRow FullFillBetCellsOfBetRow(Models.ReadFromWeb.BetRow betRow, Models.ReadFromWeb.Match match)
        {
            if (betRow.FthdcBetCell != null) betRow.FthdcBetCell = FullFillBetCell(betRow.FthdcBetCell, match);
            if (betRow.FtouBetCell != null) betRow.FtouBetCell = FullFillBetCell(betRow.FtouBetCell, match);
            if (betRow.HthdcBetCell != null) betRow.HthdcBetCell = FullFillBetCell(betRow.HthdcBetCell, match);
            if (betRow.HtouBetCell != null) betRow.HtouBetCell = FullFillBetCell(betRow.HtouBetCell, match);

            return betRow;
        }

        private  Models.ReadFromWeb.Match FullFillBetCellsOfMatch(Models.ReadFromWeb.Match match)
        {
            if (match.BetArea.FirstBetRow != null) match.BetArea.FirstBetRow = FullFillBetCellsOfBetRow(match.BetArea.FirstBetRow, match);
            if (match.BetArea.SecondBetRow != null) match.BetArea.SecondBetRow = FullFillBetCellsOfBetRow(match.BetArea.SecondBetRow, match);
            if (match.BetArea.ThirdBetRow != null) match.BetArea.ThirdBetRow = FullFillBetCellsOfBetRow(match.BetArea.ThirdBetRow, match);

            return match;
        }

        public  Models.ReadFromWeb.Match DeserializeMatchArea(IWebElement matchArea)
        {
            Models.ReadFromWeb.Match match = new Models.ReadFromWeb.Match();

            TeamOfMatch teamsOfMath = GetTeamsOfMath(matchArea);

            match.Home = teamsOfMath.Home.TeamName;
            match.Away = teamsOfMath.Away.TeamName;

            if (teamsOfMath.Home.IsStrongerTeam) match.TeamStronger = teamsOfMath.Home.TeamName;
            if (teamsOfMath.Away.IsStrongerTeam) match.TeamStronger = teamsOfMath.Away.TeamName;

            match.BetArea.TimePlaying = GetTimePlayingOfMatch(matchArea);
            match.BetArea.Score = GetScoreOfMatch(matchArea);

            ReadOnlyCollection<IWebElement> multiOdds = matchArea.FindElements(By.ClassName("multiOdds"));
            if(multiOdds.Count == 1)
            {
                match.BetArea.FirstBetRow = DeserializeBetRow(multiOdds[0]);
            }
            else if (multiOdds.Count == 2)
            {
                match.BetArea.FirstBetRow = DeserializeBetRow(multiOdds[0]);
                match.BetArea.SecondBetRow = DeserializeBetRow(multiOdds[1]);
            }
            else if (multiOdds.Count == 3)
            {
                match.BetArea.FirstBetRow = DeserializeBetRow(multiOdds[0]);
                match.BetArea.SecondBetRow = DeserializeBetRow(multiOdds[1]);
                match.BetArea.ThirdBetRow = DeserializeBetRow(multiOdds[2]);
            }

            match = FullFillBetCellsOfMatch(match);

            return match;
        }

        public  Team GetTeamName(IWebElement team)
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

        public  void Login(ChromeDriver driver)
        {
            App.Common.Config config = new App.Common.Config();
            driver.Navigate().GoToUrl(config.Url);
            driver.FindElementById("txtID").SendKeys(config.Username);
            driver.FindElementById("txtPW").SendKeys(config.Password);
            driver.FindElementByCssSelector("a.login_btn > span").Click();
        }

    }
}

