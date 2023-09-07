using LiveBetApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms.Setting
{
    public partial class MatchFilter : Form
    {
        MainForm _mainform;
        public MatchFilter(MainForm mainform)
        {
            InitializeComponent();
            this._mainform = mainform;
        }

        private void MatchFilter_Load(object sender, EventArgs e)
        {
            DataStore.FilterDate = DateTime.Now.Date;
            this.dtFilterFrom.Value = DataStore.FilterDate.Date;

            this.chbPlaningMatchs.Checked = DataStore.ShowPlaningMatchsChecked;
            this.chbRunningMatchsH1.Checked = DataStore.ShowRunningMatchsH1Checked;
            this.chbRunningMatchsH2.Checked = DataStore.ShowRunningMatchsH2Checked;
            this.chbRunningMatchsHt.Checked = DataStore.ShowRunningMatchsHtChecked;

            this.chbFinishedMatchs.Checked = DataStore.ShowFinishedMatchsChecked;

            this.chbKeepRefreshing.Checked = DataStore.MainformKeepRefreshingChecked;
            //this.numMaxHdp.Value = DataStore.MaxHdp;
            //this.numMinHdp.Value = DataStore.MinHdp;
            //this.numMoneyLine.Value = DataStore.OuMoneyLine;
            this.rtbLeagues.Text = DataStore.LeagueFilter;
            this.chbShowCorners.Checked = DataStore.ShowCornersChecked;
            this.rdAll.Checked = (DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.All);
            this.rdLive.Checked = (DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.Live);
            this.rdNoneLive.Checked = (DataStore.LSteamSearch == Common.Enums.LiveSteamSearch.NoneLive);
            this.chbShowSaba.Checked = DataStore.ShowSabaOnly;
            this.chbNoShowMatchs.Checked = DataStore.ShowNoShowMatchsChecked;
            
            this.chbSpecialFilter.Checked = DataStore.SpecialFilter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DataStore.MatchFilterDateTimeTo = this.dtFilterTo.Value.Date;
            DataStore.ShowPlaningMatchsChecked = this.chbPlaningMatchs.Checked;
            DataStore.ShowFinishedMatchsChecked = this.chbFinishedMatchs.Checked;


            DataStore.ShowRunningMatchsH1Checked = this.chbRunningMatchsH1.Checked;
            DataStore.ShowRunningMatchsH2Checked = this.chbRunningMatchsH2.Checked;
            DataStore.ShowRunningMatchsHtChecked = this.chbRunningMatchsHt.Checked;



            DataStore.ShowNoShowMatchsChecked = this.chbNoShowMatchs.Checked;


            DataStore.MainformKeepRefreshingChecked = this.chbKeepRefreshing.Checked;
            DataStore.ShowCornersChecked = this.chbShowCorners.Checked;
            //DataStore.MaxHdp = (int)this.numMaxHdp.Value;
            //DataStore.MinHdp = (int)this.numMinHdp.Value;
            //DataStore.OuMoneyLine = (int)this.numMoneyLine.Value;
            DataStore.LeagueFilter = this.rtbLeagues.Text;
            DataStore.LSteamSearch = rdAll.Checked ? Common.Enums.LiveSteamSearch.All :
                rdLive.Checked ? Common.Enums.LiveSteamSearch.Live : 
                Common.Enums.LiveSteamSearch.NoneLive;

            DataStore.ShowSabaOnly = this.chbShowSaba.Checked;
            DataStore.SpecialFilter = this.chbSpecialFilter.Checked;
            try
            {
                if (this.dtFilterFrom.Value.Date != DataStore.FilterDate.Date)
                {
                    DataStore.FilterDate = this.dtFilterFrom.Value.Date;
                    Common.Functions.RestoreDatastore(DataStore.FilterDate);

                    //if (DataStore.Blacklist.Count == 0)
                    //{
                    //    Common.Functions.InitBlackList();
                    //}
                }
            }
            catch (Exception ex)
            {
                var confirmResult = MessageBox.Show(
                    "Lỗi không đọc được data ngày " + DataStore.FilterDate.ToString("dd/MM/yyyy"),
                    "Lỗi",
                    MessageBoxButtons.OK
                );
            }
            
            this._mainform.UpdateMainGrid();

            this.Close();
        }

        private void dtFilter_CloseUp(object sender, EventArgs e)
        {
            //if (Math.Abs(this.dtFilterFrom.Value.Subtract(DateTime.Now).TotalDays) >= 1)
            //{
            //    this.chbKeepRefreshing.Checked = false;
            //    this.chbKeepRefreshing.Enabled = false;
            //}
            //else
            //{
            //    this.chbKeepRefreshing.Enabled = true;
            //}
        }


        private string GetPenHistories(long matchId)
        {
            string result = "";
            List<LiveBetApp.Models.DataModels.PenHistory> listPen;

            if (DataStore.PenHistories.TryGetValue(matchId, out listPen))
            {
                for (int i = 0; i < listPen.Count; i++)
                {
                    int minute = listPen[i].Minute + (listPen[i].LivePeriod - 1) * 45;
                    if (listPen[i].LivePeriod == 1 && minute > 45)
                    {
                        minute = 45;
                    }
                    result += (minute + " ");
                }
            }

            return result;
        }

        private string GetAlertWd20(long matchId)
        {
            if (DataStore.Alert_Wd20.ContainsKey(matchId))
            {
                return DataStore.Alert_Wd20[matchId].CustomValue;
            }
            return "";
        }

        private string GetAlertWd21(long matchId)
        {
            Models.DataModels.Match match = DataStore.Matchs[matchId];
            LiveBetApp.Models.DataModels.Alert alert;
            if (DataStore.Alert_Wd21.ContainsKey(matchId))
            {
                alert = DataStore.Alert_Wd21[matchId];
                if (alert.TimeSpanFromCreate.TotalSeconds <= 46)
                {
                    if (DateTime.Now.Subtract(match.LastUpdatePhGreaterHdpPlaySound).TotalSeconds >= 35)
                    {
                        DataStore.Matchs[match.MatchId].LastUpdatePhGreaterHdpPlaySound = DateTime.Now;
                    }
                    return Common.Constants.SpeakerChar;
                }
            }
            return "";
        }

        private string GetOpenPrices(long matchId)
        {
            List<string> prices = new List<string>();

            if (DataStore.OverUnderScoreTimes.ContainsKey(matchId))
            {
                List<int> keys = DataStore.OverUnderScoreTimes[matchId].Keys.OrderByDescending(item => item).ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    int key = keys[i];
                    List<string> row = DataStore.OverUnderScoreTimes[matchId][key];
                    if (row.Count > 0 && row[0].Length > 0)
                    {
                        prices.Add(row[0]);
                    }
                }
            }

            return Common.Functions.ListToString<string>(prices, " ");
        }

        private string GetOpenHdps(long matchId)
        {
            List<int> hdps = new List<int>();

            if (DataStore.OverUnderScoreTimes.ContainsKey(matchId))
            {
                List<int> keys = DataStore.OverUnderScoreTimes[matchId].Keys.OrderByDescending(item => item).ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    int key = keys[i];
                    List<string> row = DataStore.OverUnderScoreTimes[matchId][key];
                    if (row.Count > 0 && row[0].Length > 0)
                    {
                        hdps.Add(keys[i]);
                    }
                }
            }

            return Common.Functions.ListToString<int>(hdps, " ");
        }

        private string GetGoalHistory(long matchId)
        {
            string result = "";
            if (DataStore.GoalHistories.ContainsKey(matchId))
            {
                List<Models.DataModels.GoalHistory> goalHistory = DataStore.GoalHistories[matchId];
                for (int i = 0; i < goalHistory.Count; i++)
                {
                    if (goalHistory[i].LivePeriod == 1)
                    {
                        result += (goalHistory[i].GoalTeam + goalHistory[i].RealMinute + " ");
                    }
                    else
                    {
                        result += (goalHistory[i].GoalTeam + ".");
                    }
                }
            }
            return result;
        }

    }
}
