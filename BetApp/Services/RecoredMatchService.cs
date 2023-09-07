using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetApp.Services
{
    public class HistoryMatchService
    {
        private List<BetApp.Models.SavedAsHistory.Match> _historyMatchs;


        public HistoryMatchService()
        {
            this._historyMatchs = new List<Models.SavedAsHistory.Match>();
        }

        public List<BetApp.Models.SavedAsHistory.Match> HistoryMatchs
        {
            get
            {
                return this._historyMatchs;
            }
        }

        private int FindIndexOfHistoryMatch(Models.ReadFromWeb.Match matchReadFromWeb)
        {
            for (int i = 0; i < _historyMatchs.Count; i++)
            {
                if (_historyMatchs[i].Home == matchReadFromWeb.Home && _historyMatchs[i].Away == matchReadFromWeb.Away) 
                    return i;
            }
            return -1;
        }

        private BetApp.Models.SavedAsHistory.BetCellHistory ConstructNewBetCellHistory(BetApp.Models.ReadFromWeb.BetCell betCellFromWeb)
        {
            BetApp.Models.SavedAsHistory.BetCellHistory result = new Models.SavedAsHistory.BetCellHistory();

            if (betCellFromWeb != null)
            {
                
                result.HomeButton = betCellFromWeb.HomeButton;
                result.AwayButton = betCellFromWeb.AwayButton;

                result.History.Add(new Models.SavedAsHistory.BetCell()
                {
                    HomeRate = betCellFromWeb.HomeRate,
                    AwayRate = betCellFromWeb.AwayRate,
                    EstimatedGoal = betCellFromWeb.EstimatedGoal,
                    Score = betCellFromWeb.Score,
                    TeamStronger = betCellFromWeb.TeamStronger,
                    TimePlaying = betCellFromWeb.TimePlaying,
                    DateTimeRecored = DateTime.UtcNow,
                    TimeSpanFromStart = 0
                });
            }

            return result;
        }

        private BetApp.Models.SavedAsHistory.BetRow ConstructNewBetRow(BetApp.Models.ReadFromWeb.BetRow betRowFromWeb)
        {
            BetApp.Models.SavedAsHistory.BetRow result = new Models.SavedAsHistory.BetRow();

            if (betRowFromWeb != null)
            {
                result.FthdcBetCell = ConstructNewBetCellHistory(betRowFromWeb.FthdcBetCell);
                result.FtouBetCell = ConstructNewBetCellHistory(betRowFromWeb.FtouBetCell);

                result.HthdcBetCell = ConstructNewBetCellHistory(betRowFromWeb.HthdcBetCell);
                result.HtouBetCell = ConstructNewBetCellHistory(betRowFromWeb.HtouBetCell);
            }

            return result;
        }

        private BetApp.Models.SavedAsHistory.Bet ConstructNewBetArea(BetApp.Models.ReadFromWeb.Bet betFromWeb)
        {
            BetApp.Models.SavedAsHistory.Bet result = new Models.SavedAsHistory.Bet();

            result.FirstBetRow = ConstructNewBetRow(betFromWeb.FirstBetRow);
            result.SecondBetRow = ConstructNewBetRow(betFromWeb.SecondBetRow);
            result.ThirdBetRow = ConstructNewBetRow(betFromWeb.ThirdBetRow);

            return result;
        }

        private void AddNewMatchToHistory(Models.ReadFromWeb.Match matchFromWeb)
        {
            BetApp.Models.SavedAsHistory.Bet newBet = ConstructNewBetArea(matchFromWeb.BetArea);

            _historyMatchs.Add(new Models.SavedAsHistory.Match {
                Away = matchFromWeb.Away,
                Home = matchFromWeb.Home,
                BetArea = newBet,
                TimePlaying = matchFromWeb.BetArea.TimePlaying,
                Score = matchFromWeb.BetArea.Score
            });
        }

        private bool BetCellHistoryChanged(BetApp.Models.ReadFromWeb.BetCell betCellFromWeb, List<BetApp.Models.SavedAsHistory.BetCell> History)
        {
            if (History == null || History.Count == 0) return true;

            BetApp.Models.SavedAsHistory.BetCell lastHistory = History[History.Count-1];

            if (lastHistory.AwayRate != betCellFromWeb.AwayRate) return true;
            if (lastHistory.HomeRate != betCellFromWeb.HomeRate) return true;
            if (lastHistory.Score != betCellFromWeb.Score) return true;
            if (lastHistory.EstimatedGoal != betCellFromWeb.EstimatedGoal) return true;

            //if (lastHistory.TimePlaying != newTime) return true;
            //if (lastHistory.TeamStronger != betCellFromWeb.TeamStronger) return true;

            return false;
        }

        private BetApp.Models.SavedAsHistory.BetCellHistory UpdateBetCellHistory(BetApp.Models.ReadFromWeb.BetCell betCellFromWeb, BetApp.Models.SavedAsHistory.BetCellHistory betCellHisotry)
        {
            if (betCellFromWeb == null) return betCellHisotry;

            betCellHisotry.HomeButton = betCellFromWeb.HomeButton;
            betCellHisotry.AwayButton = betCellFromWeb.AwayButton;

            if (BetCellHistoryChanged(betCellFromWeb, betCellHisotry.History))
            {
                Models.SavedAsHistory.BetCell newHistory = new Models.SavedAsHistory.BetCell()
                {
                    HomeRate = betCellFromWeb.HomeRate,
                    AwayRate = betCellFromWeb.AwayRate,
                    EstimatedGoal = betCellFromWeb.EstimatedGoal,
                    Score = betCellFromWeb.Score,
                    TimePlaying = betCellFromWeb.TimePlaying,
                    TeamStronger = betCellFromWeb.TeamStronger,
                    DateTimeRecored = DateTime.UtcNow
                };

                if(betCellHisotry.History.Count >= 1)
                {
                    Models.SavedAsHistory.BetCell last = betCellHisotry.History[betCellHisotry.History.Count - 1];
                    newHistory.TimeSpanFromStart = last.TimeSpanFromStart + DateTime.UtcNow.Subtract(last.DateTimeRecored).Seconds;
                }
                else
                {
                    newHistory.TimeSpanFromStart = 0;
                }

                betCellHisotry.History.Add(newHistory);
            }

            return betCellHisotry;
        }

        private BetApp.Models.SavedAsHistory.BetRow UpdateBetRowHistory(BetApp.Models.ReadFromWeb.BetRow betRowFromWeb, BetApp.Models.SavedAsHistory.BetRow historyBetRow)
        {
            if (betRowFromWeb == null)
            {
                return historyBetRow;
            }
                

            historyBetRow.FthdcBetCell = UpdateBetCellHistory(betRowFromWeb.FthdcBetCell, historyBetRow.FthdcBetCell);
            historyBetRow.FtouBetCell = UpdateBetCellHistory(betRowFromWeb.FtouBetCell, historyBetRow.FtouBetCell);

            historyBetRow.HthdcBetCell = UpdateBetCellHistory(betRowFromWeb.HthdcBetCell, historyBetRow.HthdcBetCell);
            historyBetRow.HtouBetCell = UpdateBetCellHistory(betRowFromWeb.HtouBetCell, historyBetRow.HtouBetCell);

            return historyBetRow;
        }

        private void UpdateHistory(Models.ReadFromWeb.Match matchFromWeb, int coresponseMatchIndex)
        {
            _historyMatchs[coresponseMatchIndex].TimePlaying = matchFromWeb.BetArea.TimePlaying;
            _historyMatchs[coresponseMatchIndex].Score = matchFromWeb.BetArea.Score;

            _historyMatchs[coresponseMatchIndex].BetArea.FirstBetRow = UpdateBetRowHistory(
                matchFromWeb.BetArea.FirstBetRow, 
                _historyMatchs[coresponseMatchIndex].BetArea.FirstBetRow
            );

            _historyMatchs[coresponseMatchIndex].BetArea.SecondBetRow = UpdateBetRowHistory(
                matchFromWeb.BetArea.SecondBetRow,
                _historyMatchs[coresponseMatchIndex].BetArea.SecondBetRow
            );

            _historyMatchs[coresponseMatchIndex].BetArea.ThirdBetRow = UpdateBetRowHistory(
                matchFromWeb.BetArea.ThirdBetRow,
                _historyMatchs[coresponseMatchIndex].BetArea.ThirdBetRow
            );
        }

        public List<BetApp.Models.SavedAsHistory.Match> ProcessRecoredMatch(List<Models.ReadFromWeb.Match> matchsReadFromWeb)
        {
            Models.ReadFromWeb.Match matchFromWeb;
            for (int i = 0; i < matchsReadFromWeb.Count; i++)
            {
                matchFromWeb = matchsReadFromWeb[i];
                int indexOfHistoryMatch = FindIndexOfHistoryMatch(matchFromWeb);
                if (indexOfHistoryMatch == -1)
                {
                    AddNewMatchToHistory(matchFromWeb);
                }
                else
                {
                    UpdateHistory(matchFromWeb, indexOfHistoryMatch);
                }
            }


            return this._historyMatchs;
        }
    }
}
