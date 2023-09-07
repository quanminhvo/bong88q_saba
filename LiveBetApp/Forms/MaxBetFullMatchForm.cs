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
    public partial class MaxBetFullMatchForm : Form
    {
        private Thread _executeThread { get; set; }
        private bool _isNonRequestMaxBet { get; set; }
        public MaxBetFullMatchForm(bool isNonRequestMaxBet)
        {
            InitializeComponent();
            _isNonRequestMaxBet = isNonRequestMaxBet;
            this.StartPosition = FormStartPosition.Manual;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height/3*2;
            int startX = Screen.PrimaryScreen.Bounds.Width - this.Width < 0 ?
                0 :
                Screen.PrimaryScreen.Bounds.Width - this.Width;

            int startY = Screen.PrimaryScreen.Bounds.Height - this.Height - 30;
            this.Location = new Point(0, startY);
            if (isNonRequestMaxBet)
            {
                this.Text = "Max Bet Non-Request";
            }
            else
            {
                this.Text = "Max Bet Request";
            }
        }

        private void MaxBetFullMatchForm_Load(object sender, EventArgs e)
        {
            
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


            List <MatchMaxBetFull> matchs = AutoMapper.Mapper.Map<List<MatchMaxBetFull>>(source);
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

            dataGridView.DefaultCellStyle = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Regular), ForeColor = Color.Black };

            dataGridView.Columns["MatchId"].Visible = false;
            dataGridView.Columns["MatchCode"].Visible = false;
            dataGridView.Columns["League"].Visible = false;
            dataGridView.Columns["GlobalShowtimeStr"].Visible = false;
            dataGridView.Columns["HasStreaming"].Visible = false;
            dataGridView.Columns["Order"].Visible = false;
            dataGridView.Columns["GlobalShowtime"].Visible = false;
            dataGridView.Columns["TimeSpanFromStart"].Visible = false;
            dataGridView.Columns["IsNoShowMatch"].Visible = false;
            dataGridView.Columns["IsHT"].Visible = false;
            if (!_isNonRequestMaxBet)
            {
                dataGridView.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = false,
                    Text = "Get Data",
                    Name = "getDataRowBtn",
                    HeaderText = "Get Data"
                });
            }


            //dataGridView.Columns["League"].DefaultCellStyle = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold), ForeColor = Color.Black };
            //public int LivePeriod { get; set; }
            //public int MinuteFromStart { get { return (int)TimeSpanFromStart.TotalMinutes; } }
            dataGridView.Columns["LivePeriod"].Width = 20;
            dataGridView.Columns["MinuteFromStart"].Width = 20;

            dataGridView.Columns["League"].Width = 275;
            dataGridView.Columns["Home"].Width = 100;
            dataGridView.Columns["LiveHomeScore"].Width = 15;
            dataGridView.Columns["LiveAwayScore"].Width = 15;
            dataGridView.Columns["Away"].Width = 100;
            dataGridView.Columns["Home"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["LiveHomeScore"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["LiveAwayScore"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (!_isNonRequestMaxBet)
            {
                dataGridView.Columns["getDataRowBtn"].Width = 70;
            }
                

            dataGridView.Columns["League"].DefaultCellStyle = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold), ForeColor = Color.Black };

            for (int i=1; i<=49; i++)
            {
                dataGridView.Columns["M" + i].HeaderText = i.ToString();
                if (i == 3 || i == 6 || i == 36 || i == 41)
                {
                    dataGridView.Columns["M" + i].HeaderCell.Style.BackColor = Color.Orange;
                }
            }

            int index = 0;
            for (int i = 0; i < dataGridView.Rows.Count; i += 2)
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
                try
                {
                    string matchIdStr = dataGridView.Rows[i].Cells["MatchId"].Value.ToString();
                    long matchId = long.Parse(matchIdStr);
                    var match = DataStore.Matchs[matchId];

                    List<GoalHistory> goalHistories;
                    if (DataStore.GoalHistories.TryGetValue(matchId, out goalHistories))
                    {

                    }
                    else
                    {
                        goalHistories = new List<GoalHistory>();
                    }

                    for (int j = 1; j <= 45; j++)
                    {
                        try
                        {
                            if (match.LivePeriod == 1 || match.LivePeriod == 2)
                            {
                                CheckGoalHistory(goalHistories, j, match.LivePeriod, i);
                                CheckUpDown(j, match.LivePeriod, i);
                            }
                            else
                            {

                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    if (!_isNonRequestMaxBet)
                    {
                        DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)dataGridView.Rows[i].Cells["getDataRowBtn"];
                        if (match.MatchId == DataStore.MatchIdNeedGetMaxBetRequest)
                        {
                            buttonCell.Value = "Getting";
                        }
                        else
                        {
                            buttonCell.Value = "Get Data";
                        }
                    }

                    if (match.LivePeriod == 2)
                    {
                        dataGridView.Rows[i].Cells["LivePeriod"].Style.BackColor = Color.YellowGreen;
                    }
                }
                catch (Exception ex)
                {

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
                dataGridView.Rows[rowIndex].Cells["M" + minute].Style = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
            }
        }

        private void CheckUpDown(int minute, int livePeriod, int rowIndex)
        {
            if (minute <= 1 || minute >= 46) return;
            int currentMaxBet = int.Parse(dataGridView.Rows[rowIndex].Cells["M" + minute].Value.ToString());
            int previousMaxBet = int.Parse(dataGridView.Rows[rowIndex].Cells["M" + (minute - 1)].Value.ToString());

            if (currentMaxBet > 0 && previousMaxBet > 0)
            {
                if (currentMaxBet > previousMaxBet)
                {
                    dataGridView.Rows[rowIndex].Cells["M" + minute].Style.BackColor = Color.LightGreen;
                }
                else if (previousMaxBet > currentMaxBet)
                {
                    dataGridView.Rows[rowIndex].Cells["M" + minute].Style.BackColor = Color.Yellow;
                }
            }


        }

        private List<MatchMaxBetFull> UpdateOrder(List<MatchMaxBetFull> matchs)
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

        private MatchMaxBetFull UpdateMatchMaxBet(MatchMaxBetFull match, int hdpIndex)
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
                if (match.LivePeriod == 1 || match.LivePeriod == 2)
                {
                    List<int> dataHdpH1 = data[match.LivePeriod][hdpIndex];

                    int addMinute = (match.LivePeriod - 1) * 45;

                    match.M1 = dataHdpH1[1 + addMinute];
                    match.M2 = dataHdpH1[2 + addMinute];
                    match.M3 = dataHdpH1[3 + addMinute];
                    match.M4 = dataHdpH1[4 + addMinute];
                    match.M5 = dataHdpH1[5 + addMinute];
                    match.M6 = dataHdpH1[6 + addMinute];
                    match.M7 = dataHdpH1[7 + addMinute];
                    match.M8 = dataHdpH1[8 + addMinute];
                    match.M9 = dataHdpH1[9 + addMinute];
                    match.M10 = dataHdpH1[10 + addMinute];
                    match.M11 = dataHdpH1[11 + addMinute];
                    match.M12 = dataHdpH1[12 + addMinute];
                    match.M13 = dataHdpH1[13 + addMinute];
                    match.M14 = dataHdpH1[14 + addMinute];
                    match.M15 = dataHdpH1[15 + addMinute];
                    match.M16 = dataHdpH1[16 + addMinute];
                    match.M17 = dataHdpH1[17 + addMinute];
                    match.M18 = dataHdpH1[18 + addMinute];
                    match.M19 = dataHdpH1[19 + addMinute];
                    match.M20 = dataHdpH1[20 + addMinute];
                    match.M21 = dataHdpH1[21 + addMinute];
                    match.M22 = dataHdpH1[22 + addMinute];
                    match.M23 = dataHdpH1[23 + addMinute];
                    match.M24 = dataHdpH1[24 + addMinute];
                    match.M25 = dataHdpH1[25 + addMinute];
                    match.M26 = dataHdpH1[26 + addMinute];
                    match.M27 = dataHdpH1[27 + addMinute];
                    match.M28 = dataHdpH1[28 + addMinute];
                    match.M29 = dataHdpH1[29 + addMinute];
                    match.M30 = dataHdpH1[30 + addMinute];
                    match.M31 = dataHdpH1[31 + addMinute];
                    match.M32 = dataHdpH1[32 + addMinute];
                    match.M33 = dataHdpH1[33 + addMinute];
                    match.M34 = dataHdpH1[34 + addMinute];
                    match.M35 = dataHdpH1[35 + addMinute];
                    match.M36 = dataHdpH1[36 + addMinute];
                    match.M37 = dataHdpH1[37 + addMinute];
                    match.M38 = dataHdpH1[38 + addMinute];
                    match.M39 = dataHdpH1[39 + addMinute];
                    match.M40 = dataHdpH1[40 + addMinute];
                    match.M41 = dataHdpH1[41 + addMinute];
                    match.M42 = dataHdpH1[42 + addMinute];
                    match.M43 = dataHdpH1[43 + addMinute];
                    match.M44 = dataHdpH1[44 + addMinute];
                    match.M45 = dataHdpH1[45 + addMinute];
                    match.M46 = dataHdpH1[46 + addMinute];
                    match.M47 = dataHdpH1[47 + addMinute];
                    match.M48 = dataHdpH1[48 + addMinute];
                    match.M49 = dataHdpH1[49 + addMinute];
                }
            }
            return match;
        }

        private List<MatchMaxBetFull> UpdateMatchsMaxBet(List<MatchMaxBetFull> matchs)
        {
            List<MatchMaxBetFull> result = new List<MatchMaxBetFull>();

            for (int i = 0; i < matchs.Count; i++)
            {
                try
                {
                    MatchMaxBetFull matchA = (MatchMaxBetFull)matchs[i].Clone();
                    MatchMaxBetFull matchB = (MatchMaxBetFull)matchs[i].Clone();

                    matchA = UpdateMatchMaxBet(matchA, 0);
                    matchB = UpdateMatchMaxBet(matchB, 1);
                    matchB.League = "";
                    result.Add(matchA);
                    result.Add(matchB);
                }
                catch (Exception ex)
                {

                }


            }

            return result;
        }

        private void MaxBetFullMatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["getDataRowBtn"].Index)
            {
                long id = (long)dataGridView.Rows[e.RowIndex].Cells["MatchId"].Value;
                if (DataStore.MatchIdNeedGetMaxBetRequest != id)
                {
                    DataStore.MatchIdNeedGetMaxBetRequest = id;
                }
                else
                {
                    DataStore.MatchIdNeedGetMaxBetRequest = 0;
                }
                
                UpdateMainGrid();
            }
        }
    }
}
