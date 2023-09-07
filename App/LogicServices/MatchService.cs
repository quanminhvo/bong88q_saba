using App.DataModels;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.LogicServices
{
    public static class MatchService
    {
        public static bool IsWatchingMatch(DataModels.Match match, List<MatchToWatch> matchsToWatch)
        {
            for (int i = 0; i < matchsToWatch.Count; i++)
            {
                if (match.TeamHost.Trim().ToUpper() == matchsToWatch[i].TeamHost.Trim().ToUpper()
                    && match.TeamGuest.Trim().ToUpper() == matchsToWatch[i].TeamGuest.Trim().ToUpper())
                    return true;
            }
            return false;
        }

        public static MatchToWatch FindMatchToWatchByTeamName(List<MatchToWatch> matchsToWatch, string teamHost, string teamGuest)
        {
            for (int i = 0; i < matchsToWatch.Count; i++)
            {
                if (matchsToWatch[i].TeamHost == teamHost
                    && matchsToWatch[i].TeamGuest == teamGuest)
                {
                    return matchsToWatch[i];
                }
            }
            return new MatchToWatch();
        }

        private static TimmingBet CheckAndDoBet(IWebElement betBtn, TimmingBet timmingBet, string timePlaying)
        {
            IWebElement betTxt;
            IWebElement betSubmit;
            IWebElement btnConfirm;

            if (timePlaying == timmingBet.TimeToBet
                && timmingBet.BetAmount > 0
                && !timmingBet.IsClicked)
            {
                timmingBet.IsClicked = true;
                betBtn.Click();
                Thread.Sleep(500);
                betTxt = betBtn.FindElement(By.Id("quickBetStake"));
                betTxt.SendKeys(timmingBet.BetAmount.ToString());
                betSubmit = betBtn.FindElement(By.ClassName("btnArea")).FindElement(By.ClassName("largeBtn"));
                betSubmit.Click();
                Thread.Sleep(400);
                btnConfirm = betBtn.FindElement(By.ClassName("largeBtn"));
                btnConfirm.Click();
            }

            return timmingBet;
        }

        public static BetButtonOfMatch CheckAndDoBetOneOfSix(BetButtonOfMatch betButtonOfMatch, string timePlaying, BetButtonOfMatch recoredBetButtonOfMatch)
        {
            if (betButtonOfMatch.FullTimeHandicap_TeamGuest.IsGoodToGo)
            {
                betButtonOfMatch.FullTimeHandicap_TeamGuest = CheckAndDoBet(
                    recoredBetButtonOfMatch.FullTimeHandicap_TeamGuest.Button,
                    betButtonOfMatch.FullTimeHandicap_TeamGuest,
                    timePlaying
                );
            }
            else if (betButtonOfMatch.FullTimeHandicap_TeamHost.IsGoodToGo)
            {
                betButtonOfMatch.FullTimeHandicap_TeamHost = CheckAndDoBet(
                    recoredBetButtonOfMatch.FullTimeHandicap_TeamHost.Button,
                    betButtonOfMatch.FullTimeHandicap_TeamHost,
                    timePlaying
                );
            }
            else if (betButtonOfMatch.FullTimeOverUnder_TeamGuest.IsGoodToGo)
            {
                betButtonOfMatch.FullTimeHandicap_TeamHost = CheckAndDoBet(
                    recoredBetButtonOfMatch.FullTimeOverUnder_TeamGuest.Button,
                    betButtonOfMatch.FullTimeOverUnder_TeamGuest,
                    timePlaying
                );
            }
            else if (betButtonOfMatch.FullTimeOverUnder_TeamHost.IsGoodToGo)
            {
                betButtonOfMatch.FullTimeHandicap_TeamHost = CheckAndDoBet(
                    recoredBetButtonOfMatch.FullTimeOverUnder_TeamHost.Button,
                    betButtonOfMatch.FullTimeOverUnder_TeamHost,
                    timePlaying
                );
            }


            return betButtonOfMatch;
        }

        public static MatchToWatch ProcessSixTimingBets(DataModels.Match recoredMatch, MatchToWatch matchToWatch)
        {
            string timePlaying = recoredMatch.Bets.Last().TimePlaying;

            matchToWatch.TimmingBetButtons.FirstTimingBetButton = CheckAndDoBetOneOfSix(matchToWatch.TimmingBetButtons.FirstTimingBetButton, timePlaying, recoredMatch.TimmingBetButton);
            matchToWatch.TimmingBetButtons.SecondTimingBetButton = CheckAndDoBetOneOfSix(matchToWatch.TimmingBetButtons.SecondTimingBetButton, timePlaying, recoredMatch.TimmingBetButton);
            matchToWatch.TimmingBetButtons.ThirdTimingBetButton = CheckAndDoBetOneOfSix(matchToWatch.TimmingBetButtons.ThirdTimingBetButton, timePlaying, recoredMatch.TimmingBetButton);
            matchToWatch.TimmingBetButtons.FourthTimingBetButton = CheckAndDoBetOneOfSix(matchToWatch.TimmingBetButtons.FourthTimingBetButton, timePlaying, recoredMatch.TimmingBetButton);
            matchToWatch.TimmingBetButtons.FifthTimingBetButton = CheckAndDoBetOneOfSix(matchToWatch.TimmingBetButtons.FifthTimingBetButton, timePlaying, recoredMatch.TimmingBetButton);
            matchToWatch.TimmingBetButtons.SixthTimingBetButton = CheckAndDoBetOneOfSix(matchToWatch.TimmingBetButtons.SixthTimingBetButton, timePlaying, recoredMatch.TimmingBetButton);

            return matchToWatch;
        }

        public static MatchToWatch ProcessTimingBet(DataModels.Match recoredMatch, MatchToWatch matchToWatch)
        {
            string timePlaying = recoredMatch.Bets.Last().TimePlaying;

            try
            {
                matchToWatch.TimmingBetButton.FullTimeHandicap_TeamHost = CheckAndDoBet(
                    recoredMatch.TimmingBetButton.FullTimeHandicap_TeamHost.Button,
                    matchToWatch.TimmingBetButton.FullTimeHandicap_TeamHost,
                    timePlaying
                );

                matchToWatch.TimmingBetButton.FullTimeHandicap_TeamGuest = CheckAndDoBet(
                    recoredMatch.TimmingBetButton.FullTimeHandicap_TeamGuest.Button,
                    matchToWatch.TimmingBetButton.FullTimeHandicap_TeamGuest,
                    timePlaying
                );

                matchToWatch.TimmingBetButton.FullTimeOverUnder_TeamHost = CheckAndDoBet(
                    recoredMatch.TimmingBetButton.FullTimeOverUnder_TeamHost.Button,
                    matchToWatch.TimmingBetButton.FullTimeOverUnder_TeamHost,
                    timePlaying
                );

                matchToWatch.TimmingBetButton.FullTimeOverUnder_TeamGuest = CheckAndDoBet(
                    recoredMatch.TimmingBetButton.FullTimeOverUnder_TeamGuest.Button,
                    matchToWatch.TimmingBetButton.FullTimeOverUnder_TeamGuest,
                    timePlaying
                );
            }
            catch
            {

            }

            return matchToWatch;
        }

        public static List<MatchToWatch> ProcessRecoredMatch(List<DataModels.Match> recoredMatchs, List<MatchToWatch> matchsToWatch)
        {
            for (int i = 0; i < recoredMatchs.Count; i++)
            {
                for (int j = 0; j < matchsToWatch.Count; j++)
                {
                    if (recoredMatchs[i].TeamHost == matchsToWatch[j].TeamHost
                        && recoredMatchs[i].TeamGuest == matchsToWatch[j].TeamGuest)
                    {

                        Bet firstRecordedBet = recoredMatchs[i].Bets[0];
                        matchsToWatch[j].TeamStronger = recoredMatchs[i].TeamStronger;

                        if (matchsToWatch[j].BetChangeHistory.Count == 0)
                        {
                            matchsToWatch[j].BetChangeHistory.Add(firstRecordedBet);
                        }
                        else
                        {
                            Bet lastestBetHistory = matchsToWatch[j].BetChangeHistory[matchsToWatch[j].BetChangeHistory.Count - 1];
                            if (BetService.IsBetHistoryChange(firstRecordedBet, lastestBetHistory))
                            {
                                firstRecordedBet.TimeSpanFromLastChanges = firstRecordedBet.RecordedDateTime.Subtract(lastestBetHistory.RecordedDateTime);
                                matchsToWatch[j].BetChangeHistory.Add(firstRecordedBet);
                            }
                        }
                        //matchsToWatch[j] = ProcessTimingBet(recoredMatchs[i], matchsToWatch[j]);
                        matchsToWatch[j] = ProcessSixTimingBets(recoredMatchs[i], matchsToWatch[j]);
                        
                    }
                }
            }
            return matchsToWatch;
        }

        public static List<DataModels.Match> GetSelectedMatchFromLiveTable(List<MatchToWatch> matchsToWatch, ReadOnlyCollection<IWebElement> matchAreas)
        {
            List<DataModels.Match> recoredMatchs = new List<DataModels.Match>();

            foreach (IWebElement matchArea in matchAreas)
            {
                ReadOnlyCollection<IWebElement> teamsOfMath = SeleniumService.GetTeamsOfMath(matchArea);
                DataModels.Match match = new DataModels.Match();
                Team teamHost = SeleniumService.GetTeamName(teamsOfMath[0]);
                Team teamGuest = SeleniumService.GetTeamName(teamsOfMath[1]);
                match.TeamHost = teamHost.TeamName.Trim().ToUpper();
                match.TeamGuest = teamGuest.TeamName.Trim().ToUpper();

                if (!MatchService.IsWatchingMatch(match, matchsToWatch)) continue;

                string timePlaying = SeleniumService.GetTimePlayingOfMatch(matchArea);
                string score = SeleniumService.GetScoreOfMatch(matchArea);

                if (teamHost.IsStrongerTeam)
                {
                    match.TeamStronger = teamHost.TeamName;
                }
                else if (teamGuest.IsStrongerTeam)
                {
                    match.TeamStronger = teamGuest.TeamName;
                }

                ReadOnlyCollection<IWebElement> betElements = SeleniumService.GetBetsOfMath(matchArea);

                ReadOnlyCollection<IWebElement> betAreas = SeleniumService.GetBetAreasOfBet(betElements[0]);
                match = UpdateTimmingBetButton(match, betAreas);
                Bet bet = SeleniumService.DeserializeBetAreasOfOneBet(betAreas);
                bet.TimePlaying = timePlaying;
                bet.Score = score;
                bet.RecordedDateTime = DateTime.Now;

                match.Bets.Add(bet);

                //foreach (IWebElement betElement in betElements)
                //{
                //    ReadOnlyCollection<IWebElement> betAreas = SeleniumService.GetBetAreasOfBet(betElement);
                //    Bet bet = SeleniumService.DeserializeBetAreasOfOneBet(betAreas);
                //    bet.TimePlaying = timePlaying;
                //    bet.Score = score;
                //    bet.RecordedDateTime = DateTime.Now;

                //    match.Bets.Add(bet);
                //}

                recoredMatchs.Add(match);
            }

            return recoredMatchs;
        }

        public static Match UpdateTimmingBetButton(Match match, ReadOnlyCollection<IWebElement> betAreas)
        {
            ReadOnlyCollection<IWebElement> oddsBets = betAreas[0].FindElements(By.ClassName("oddsBet"));
            if (oddsBets.Count > 0)
            {
                match.TimmingBetButton.FullTimeHandicap_TeamHost.Button = oddsBets[0];
            }

            oddsBets = betAreas[1].FindElements(By.ClassName("oddsBet"));
            if (oddsBets.Count > 0)
            {
                match.TimmingBetButton.FullTimeHandicap_TeamGuest.Button = oddsBets[0];
            }

            oddsBets = betAreas[2].FindElements(By.ClassName("oddsBet"));
            if (oddsBets.Count > 0)
            {
                match.TimmingBetButton.FullTimeOverUnder_TeamHost.Button = oddsBets[0];
            }

            oddsBets = betAreas[3].FindElements(By.ClassName("oddsBet"));
            if (oddsBets.Count > 0)
            {
                match.TimmingBetButton.FullTimeOverUnder_TeamGuest.Button = oddsBets[0];
            }

            return match;
        }
    }
}
