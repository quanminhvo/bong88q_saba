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
    public partial class Product1x2HistoryForm : Form
    {
        private Thread _executeThread { get; set; }
        public Product1x2HistoryForm()
        {
            InitializeComponent();
            this.Text = "Product 1x2";
        }

        private void Product1x2HistoryForm_Load(object sender, EventArgs e)
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


            List<Match1x2Product> matchs = AutoMapper.Mapper.Map<List<Match1x2Product>>(source);
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


            dataGridView.Columns["League"].DefaultCellStyle = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold), ForeColor = Color.Black };

            for (int i = 1; i <= 45; i++)
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

        private List<Match1x2Product> UpdateMatchsMaxBet(List<Match1x2Product> matchs)
        {
            List<Match1x2Product> result = new List<Match1x2Product>();

            for (int i = 0; i < matchs.Count; i++)
            {
                try
                {
                    Match1x2Product matchA = (Match1x2Product)matchs[i].Clone();
                    Match1x2Product matchB = (Match1x2Product)matchs[i].Clone();

                    matchA = UpdateMatchMaxBet(matchA, Common.Enums.BetType.FirstHalf1x2);
                    matchB = UpdateMatchMaxBet(matchB, Common.Enums.BetType.FullTime1x2);
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

        private string CheckPlus(Product product)
        {
            if (product != null && product.Odds1a100 != 0)
            {
                return "+";
            }
            return "";
        }

        private Match1x2Product UpdateMatchMaxBet(Match1x2Product match, LiveBetApp.Common.Enums.BetType type)
        {
            List<List<Product>> data = new List<List<Product>>();
            if (DataStore.Product1x2History.ContainsKey(match.MatchId))
            {
                data = DataStore.Product1x2History[match.MatchId];
            }

            List<Product> row = new List<Product>();
            for (int i = 0; i <= 45; i++)
            {
                row.Add(new Product());
                row[i] = data[i].FirstOrDefault(item => item.Bettype == type);
            }

            if (match.LivePeriod == 1 || match.LivePeriod == 2)
            {
                int addMinute = 0;
                match.M1 = CheckPlus(row[1 + addMinute]);
                match.M2 = CheckPlus(row[2 + addMinute]);
                match.M3 = CheckPlus(row[3 + addMinute]);
                match.M4 = CheckPlus(row[4 + addMinute]);
                match.M5 = CheckPlus(row[5 + addMinute]);
                match.M6 = CheckPlus(row[6 + addMinute]);
                match.M7 = CheckPlus(row[7 + addMinute]);
                match.M8 = CheckPlus(row[8 + addMinute]);
                match.M9 = CheckPlus(row[9 + addMinute]);
                match.M10 = CheckPlus(row[10 + addMinute]);
                match.M11 = CheckPlus(row[11 + addMinute]);
                match.M12 = CheckPlus(row[12 + addMinute]);
                match.M13 = CheckPlus(row[13 + addMinute]);
                match.M14 = CheckPlus(row[14 + addMinute]);
                match.M15 = CheckPlus(row[15 + addMinute]);
                match.M16 = CheckPlus(row[16 + addMinute]);
                match.M17 = CheckPlus(row[17 + addMinute]);
                match.M18 = CheckPlus(row[18 + addMinute]);
                match.M19 = CheckPlus(row[19 + addMinute]);
                match.M20 = CheckPlus(row[20 + addMinute]);
                match.M21 = CheckPlus(row[21 + addMinute]);
                match.M22 = CheckPlus(row[22 + addMinute]);
                match.M23 = CheckPlus(row[23 + addMinute]);
                match.M24 = CheckPlus(row[24 + addMinute]);
                match.M25 = CheckPlus(row[25 + addMinute]);
                match.M26 = CheckPlus(row[26 + addMinute]);
                match.M27 = CheckPlus(row[27 + addMinute]);
                match.M28 = CheckPlus(row[28 + addMinute]);
                match.M29 = CheckPlus(row[29 + addMinute]);
                match.M30 = CheckPlus(row[30 + addMinute]);
                match.M31 = CheckPlus(row[31 + addMinute]);
                match.M32 = CheckPlus(row[32 + addMinute]);
                match.M33 = CheckPlus(row[33 + addMinute]);
                match.M34 = CheckPlus(row[34 + addMinute]);
                match.M35 = CheckPlus(row[35 + addMinute]);
                match.M36 = CheckPlus(row[36 + addMinute]);
                match.M37 = CheckPlus(row[37 + addMinute]);
                match.M38 = CheckPlus(row[38 + addMinute]);
                match.M39 = CheckPlus(row[39 + addMinute]);
                match.M40 = CheckPlus(row[40 + addMinute]);
                match.M41 = CheckPlus(row[41 + addMinute]);
                match.M42 = CheckPlus(row[42 + addMinute]);
                match.M43 = CheckPlus(row[43 + addMinute]);
                match.M44 = CheckPlus(row[44 + addMinute]);
                match.M45 = CheckPlus(row[45 + addMinute]);
            }
            return match;
        }

    }
}
