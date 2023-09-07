using LiveBetApp.Models.DataModels;
using LiveBetApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms
{
    public partial class MatchMaxBetNonRequestForm : Form
    {
        private Thread _executeThread { get; set; }
        private bool _isNonRequestMaxBet { get; set; }
        public MatchMaxBetNonRequestForm(bool isNonRequestMaxBet)
        {
            InitializeComponent();
            _isNonRequestMaxBet = isNonRequestMaxBet;
            this.StartPosition = FormStartPosition.Manual;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height / 3;
            int startX = Screen.PrimaryScreen.Bounds.Width - this.Width < 0 ?
                0 :
                Screen.PrimaryScreen.Bounds.Width - this.Width;

            int startY = Screen.PrimaryScreen.Bounds.Height - this.Height - 30;
            this.Location = new Point(0, startY);
        }

        private void MatchMaxBetNonRequestForm_Load(object sender, EventArgs e)
        {
            this.Text = "Max Bet Request";
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    
                    try
                    {
                        if (DataStore.MainformKeepRefreshingChecked)
                        {
                            UpdateMainGrid();
                        }
                    }
                    catch
                    {

                    }
                    Thread.Sleep(5000);
                }
            });
            _executeThread.IsBackground = true;
            _executeThread.Start();
        }

        private delegate void dlgUpdateMainGrid();

        public void UpdateMainGrid()
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgUpdateMainGrid(UpdateMainGrid));
                return;
            }

            BindingSource bindingSource = new BindingSource();
            List<Models.DataModels.Match> source = DataStore.Matchs.Values.ToList();

            List<string> selectedLeagues = DataStore.LeagueFilter.Split('\n').ToList();
            for (int i = 0; i < selectedLeagues.Count; i++)
            {
                selectedLeagues[i] = selectedLeagues[i].ToLower();
            }

            List<Models.DataModels.Match> sampleSource = source.Where(item =>
                item.Home != null
                && item.Away != null
                && item.League != null
                && item.League.Trim().Length > 0
                && !item.League.ToLower().Contains("e-football")
                && !item.League.ToLower().Contains("saba soccer pingoal")
                && (!DataStore.ShowSabaOnly || (DataStore.ShowSabaOnly && item.League.ToUpper().Contains("SABA")))
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
                        //&& !item.League.ToUpper().Contains("- PENALTY")
                        && !item.League.ToUpper().Contains("- TOTAL GOALS MINUTES")
                        && !item.League.ToUpper().Contains("- WHICH TEAM TO KICK OFF")
                        && !item.League.ToUpper().Contains("SABA")
                    )
                    || (DataStore.ShowCornersChecked && item.League.ToUpper().Contains(" - CORNERS"))
                    || (DataStore.ShowSabaOnly && item.League.ToUpper().Contains("SABA"))
                )
                && !item.Home.ToLower().Contains("(pen)")
                && item.LivePeriod == 0
                && !item.IsHT
                && item.GlobalShowtime > DateTime.Now
            )
            .OrderBy(item => item.GlobalShowtime)
            .ToList();

            List<Models.DataModels.Match> goingToLiveMatchs = sampleSource
                .Where(item => item.GlobalShowtime >= DateTime.Now && item.GlobalShowtime.Subtract(DateTime.Now).TotalMinutes <= 60)
                .ToList();

            source = source.Where(item => true
                    //(!DataStore.ShowLiveMatchsOnlyChecked || ((DataStore.ShowLiveMatchsOnlyChecked && item.LivePeriod > 0) || item.IsHT))
                    && (
                        (DataStore.ShowFinishedMatchsChecked && item.LivePeriod == -1)
                        || (DataStore.ShowRunningMatchsH1Checked && item.LivePeriod == 1)
                        || (DataStore.ShowRunningMatchsH2Checked && item.LivePeriod == 2)
                        || (DataStore.ShowRunningMatchsHtChecked && item.IsHT)
                        || (DataStore.ShowPlaningMatchsChecked && item.LivePeriod == 0 && !item.IsHT)
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
                    && !DataStore.Blacklist.Any(p => p.IsActive && p.League.ToLower() == item.League.ToLower())
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
                            //&& !item.League.ToUpper().Contains("- PENALTY")
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

            if (DataStore.SpecialFilter)
            {
                source = source
                    .Where(item => Common.Functions.CheckSpecialFilter(item.MatchId))
                    .ToList();
            }


            List<MatchMaxBet> matchs = AutoMapper.Mapper.Map<List<MatchMaxBet>>(source);
            matchs = matchs.OrderBy(item => item.IsNoShowMatch)
                .ThenByDescending(item => item.LivePeriod)
                .ThenBy(item => item.GlobalShowtime)
                .ToList();

            for (int i = 0; i < matchs.Count; i++)
            {
                if (matchs[i].LivePeriod == 0 && matchs[i].IsHT)
                {
                    matchs[i].LivePeriod = 3;
                }
            }

            matchs = UpdateMatchsMaxBet(matchs);

            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);
            dataGridView.Columns.Clear();
            dataGridView.DataSource = bindingSource;

            dataGridView.Columns["MatchId"].Visible = false;
            dataGridView.Columns["MatchCode"].Visible = false;
            dataGridView.Columns["League"].Visible = false;
            dataGridView.Columns["GlobalShowtimeStr"].Visible = false;
            dataGridView.Columns["HasStreaming"].Visible = false;
            dataGridView.Columns["Order"].Visible = false;
            dataGridView.Columns["GlobalShowtime"].Visible = false;
            dataGridView.Columns["TimeSpanFromStart"].Visible = false;
            dataGridView.Columns["IsNoShowMatch"].Visible = false;

            dataGridView.Columns["League"].DefaultCellStyle = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold), ForeColor = Color.Black };
            dataGridView.Columns["League"].Width = 275;
            dataGridView.Columns["Home"].Width = 100;
            dataGridView.Columns["LiveHomeScore"].Width = 15;
            dataGridView.Columns["LiveAwayScore"].Width = 15;
            dataGridView.Columns["Away"].Width = 100;
            dataGridView.Columns["Home"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["LiveHomeScore"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["LiveAwayScore"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView.Columns["League"].DefaultCellStyle = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold), ForeColor = Color.Black };

            int index = 0;
            for (int i = 0; i < dataGridView.Rows.Count; i+=2)
            {
                index++;
                if (index % 2 == 0)
                {
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView.Rows[i + 1].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }



            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                string matchIdStr = dataGridView.Rows[i].Cells["MatchId"].Value.ToString();
                long matchId = long.Parse(matchIdStr);

                List<GoalHistory> goalHistories;
                if (DataStore.GoalHistories.TryGetValue(matchId, out goalHistories))
                {

                    if (goalHistories.All(item => item.TimeSpanFromStart.TotalMinutes >= 35))
                    {

                    }

                    for (int j=35; j<=45; j++)
                    {
                        CheckGoalHistory(goalHistories, j, 1, i);
                        CheckGoalHistory(goalHistories, j, 2, i);
                        CheckUpDown(j, 1, i);
                        CheckUpDown(j, 2, i);
                    }
                }
            }
        }

        private void CheckGoalHistory(List<GoalHistory> goalHistories, int minute, int livePeriod, int rowIndex)
        {
            if (goalHistories.Any(item =>
                item.LivePeriod == livePeriod
                && (item.TimeSpanFromStart.TotalMinutes > 45 ? 45 : (int)(item.TimeSpanFromStart.TotalMinutes)) == minute)
            )
            {
                dataGridView.Rows[rowIndex].Cells["M" + minute + "H" + livePeriod].Style = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
            }
        }

        private void CheckUpDown(int minute, int livePeriod, int rowIndex)
        {
            if (minute <= 35 || minute >= 46) return;
            int currentMaxBet = int.Parse(dataGridView.Rows[rowIndex].Cells["M" + minute + "H" + livePeriod].Value.ToString());
            int previousMaxBet = int.Parse(dataGridView.Rows[rowIndex].Cells["M" + (minute - 1) + "H" + livePeriod].Value.ToString());

            if (currentMaxBet > 0 && previousMaxBet > 0)
            {
                if (currentMaxBet > previousMaxBet)
                {
                    dataGridView.Rows[rowIndex].Cells["M" + minute + "H" + livePeriod].Style.BackColor = Color.LightGreen;
                }
                else if (previousMaxBet > currentMaxBet)
                {
                    dataGridView.Rows[rowIndex].Cells["M" + minute + "H" + livePeriod].Style.BackColor = Color.Yellow;
                }
            }


        }

        private List<MatchMaxBet> UpdateOrder(List<MatchMaxBet> matchs)
        {
            matchs = matchs
                .OrderBy(item => item.MatchCode)
                .ToList();

            int orderId = 1;
            for (int i = 0; i < matchs.Count; i++)
            {
                if (matchs[i].LivePeriod > 0 || matchs[i].IsHT)
                {
                    matchs[i].Order = orderId;
                    orderId++;
                }
            }

            orderId = 1;
            for (int i = 0; i < matchs.Count; i++)
            {
                if (matchs[i].LivePeriod == 0 && !matchs[i].IsHT)
                {
                    matchs[i].Order = orderId;
                    orderId++;
                }
            }

            return matchs;
        }

        private void MatchMaxBetNonRequestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }

        private MatchMaxBet UpdateMatchMaxBet(MatchMaxBet match, int hdpIndex)
        {
            List<List<List<int>>> data = new List<List<List<int>>>();
            if (_isNonRequestMaxBet)
            {
                if (DataStore.MatchMaxBetNonRequest.ContainsKey(match.MatchId))
                {
                    data = DataStore.MatchMaxBetNonRequest[match.MatchId];
                }
            }
            else
            {
                if (DataStore.MatchMaxBetRequest.ContainsKey(match.MatchId))
                {
                    data = DataStore.MatchMaxBetRequest[match.MatchId];
                }
            }

            if (data.Count - 1 >= hdpIndex)
            {
                List<int> dataHdpH1 = data[1][hdpIndex];
                match.M35H1 = dataHdpH1[35];
                match.M36H1 = dataHdpH1[36];
                match.M37H1 = dataHdpH1[37];
                match.M38H1 = dataHdpH1[38];
                match.M39H1 = dataHdpH1[39];
                match.M40H1 = dataHdpH1[40];
                match.M41H1 = dataHdpH1[41];
                match.M42H1 = dataHdpH1[42];
                match.M43H1 = dataHdpH1[43];
                match.M44H1 = dataHdpH1[44];
                match.M45H1 = dataHdpH1[45];
                match.M46H1 = dataHdpH1[46];
                match.M47H1 = dataHdpH1[47];

                List<int> dataHdpH2 = data[2][hdpIndex];
                match.M35H2 = dataHdpH2[35 + 45];
                match.M36H2 = dataHdpH2[36 + 45];
                match.M37H2 = dataHdpH2[37 + 45];
                match.M38H2 = dataHdpH2[38 + 45];
                match.M39H2 = dataHdpH2[39 + 45];
                match.M40H2 = dataHdpH2[40 + 45];
                match.M41H2 = dataHdpH2[41 + 45];
                match.M42H2 = dataHdpH2[42 + 45];
                match.M43H2 = dataHdpH2[43 + 45];
                match.M44H2 = dataHdpH2[44 + 45];
                match.M45H2 = dataHdpH2[45 + 45];
                match.M46H2 = dataHdpH2[46 + 45];
                match.M47H2 = dataHdpH2[47 + 45];
                match.M48H2 = dataHdpH2[48 + 45];
                match.M49H2 = dataHdpH2[49 + 45];
            }
            return match;
        }

        private List<MatchMaxBet> UpdateMatchsMaxBet(List<MatchMaxBet> matchs)
        {
            List<MatchMaxBet> result = new List<MatchMaxBet>();

            for (int i=0; i< matchs.Count; i++)
            {
                try
                {
                    MatchMaxBet matchA = (MatchMaxBet)matchs[i].Clone();
                    MatchMaxBet matchB = (MatchMaxBet)matchs[i].Clone();

                    matchA = UpdateMatchMaxBet(matchA, 0);
                    matchB = UpdateMatchMaxBet(matchB, 1);
                    matchB.League = "";
                    result.Add(matchA);
                    result.Add(matchB);
                }
                catch(Exception ex)
                {

                }
                
                
            }

            return result;
        }
    }
}

