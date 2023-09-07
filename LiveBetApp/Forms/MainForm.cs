using LiveBetApp.Common;
using LiveBetApp.Forms.Alerts;
using LiveBetApp.Forms.AutoBet;
using LiveBetApp.Forms.BetAfterGoodPrice;
using LiveBetApp.Forms.BetByHdpClose;
using LiveBetApp.Forms.BetByHdpPrice;
using LiveBetApp.Forms.BetByTimming;
using LiveBetApp.Forms.BetFirstHalfByFulltime;
using LiveBetApp.Forms.BetHandicapByPrice;
using LiveBetApp.Forms.DirectBet;
using LiveBetApp.Forms.Setting;
using LiveBetApp.Forms.TimmingBet;
using LiveBetApp.Models.ViewModels;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace LiveBetApp.Forms
{
    public partial class MainForm : Form
    {
        private Thread _executeThread { get; set; }
        //private Thread _backupThread { get; set; }
        private Thread _liveAlertThread { get; set; }

        public MainForm()
        {
            InitializeComponent();
            //Common.Functions.SetMiniStyle(this.dataGridView);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Subtract(DataStore.LastTick).TotalMinutes >= 2)
            {
                statusToolStripMenuItem.BackColor = Color.Red;
            }
            else
            {
                statusToolStripMenuItem.BackColor = Color.GreenYellow;
            }
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

                        if (DateTime.Now.Subtract(DataStore.LastTick).TotalMinutes >= 2)
                        {
                            statusToolStripMenuItem.BackColor = Color.Red;
                        }
                        else
                        {
                            statusToolStripMenuItem.BackColor = Color.GreenYellow;
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.Functions.WriteExceptionLog(ex);
                    }
                    Thread.Sleep(15000);
                }
            });

            _executeThread.IsBackground = true;
            _executeThread.Start();

            //_backupThread = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        try
            //        {
            //            Thread.Sleep(60 * 1000);
            //            DateTime now = DateTime.Now;
            //            if (now.Hour == DataStore.HourToBackUp && now.Minute == DataStore.MinuteToBackUp)
            //            {
            //                try
            //                {
            //                    Common.Functions.BackUpExcel();
            //                }
            //                catch { }
            //            }
            //        }
            //        catch { }
            //    }
            //});

            //_backupThread.IsBackground = true;
            //_backupThread.Start();

            _liveAlertThread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        UpdateAlertMainGrid();
                    }
                    catch (Exception ex)
                    {
                        Common.Functions.WriteExceptionLog(ex);
                    }
                }
            });

            _liveAlertThread.IsBackground = true;
            _liveAlertThread.Start();


        }

        private delegate void dlgUpdateAlertMainGrid();

        public void UpdateAlertMainGrid()
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgUpdateAlertMainGrid(UpdateAlertMainGrid));
                return;
            }

            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                string matchIdStr = dataGridView.Rows[i].Cells["MatchId"].Value.ToString();
                long matchId = long.Parse(matchIdStr);

                DateTime dt = DateTime.MinValue;


                if (DataStore.MatchHasProductStatus.TryGetValue(matchId, out dt))
                {
                    Models.DataModels.Match match = DataStore.Matchs[matchId];
                    int currentMinute = Common.Functions.GetCurrentMinute100(match);


                    if (DateTime.Now > dt && DateTime.Now.Subtract(dt).TotalSeconds <= 10)
                    {
                        if (currentMinute > 0 && currentMinute <= 100)
                        {
                            dataGridView.Rows[i].Cells["PenHistory"].Value = DataStore.ProductStatus[matchId][currentMinute].Count;
                        }
                    }

                    if (match.LastAlertClosePricePlaySound != DateTime.MinValue
                        && DateTime.Now.Subtract(match.LastAlertClosePricePlaySound).TotalSeconds <= 240)
                    {
                        dataGridView.Rows[i].Cells["PenHistory"].Style.BackColor = Color.Yellow;
                    }

                    if (currentMinute > 0
                        && currentMinute <= 100
                        && DataStore.ProductStatus[matchId][currentMinute].Any(item => item.Status.Contains("closePrice")))
                    {
                        dataGridView.Rows[i].Cells["PenHistory"].Style.BackColor = Color.Red;

                        if (DateTime.Now.Subtract(match.LastAlertClosePricePlaySound).TotalSeconds >= 60)
                        {
                            if (DataStore.KeepPlaySoundHasStatusClosePrice)
                            {
                                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alarm-fast-high-pitch-a3.wav");
                            }
                            DataStore.Matchs[matchId].LastAlertClosePricePlaySound = DateTime.Now;
                        }
                    }

                }
                else
                {
                    dataGridView.Rows[i].Cells["PenHistory"].Style.BackColor = Color.White;
                }



            }

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
            //else if (DataStore.ShowPreLiveMatchsOnlyChecked)
            //{
            //    source = goingToLiveMatchs;
            //}

            //source = source.Where(item => 
            //    item.DeleteTime.HasValue 
            //    && item.DeleteTime <= item.GlobalShowtime
            //    && item.LastUpdatePhOuFtLive == DateTime.MinValue
            //).ToList();

            List<Match> matchs = AutoMapper.Mapper.Map<List<Match>>(source);
            matchs = UpdateOrder(matchs);
            matchs = matchs.OrderBy(item => item.IsNoShowMatch)
                .ThenByDescending(item => item.LivePeriod)
                .ThenBy(item => item.GlobalShowtime)
                .ToList();

            if (!DataStore.SemiAutoBetOnly)
            {
                for (int i = 0; i < matchs.Count; i++)
                {
                    matchs[i].FreqBeforeLive = Common.Functions.GetMaxFreqLeague(matchs[i].MatchId);
                    matchs[i].GoalHistory = GetGoalHistory(matchs[i].MatchId);
                    matchs[i].OpenHdps = GetOpenHdps(matchs[i].MatchId);
                    //matchs[i].OpenPrices = GetOpenPrices(matchs[i].MatchId);
                    //matchs[i].IUOO_ft = Common.Functions.CountWd23(matchs[i].MatchId).ToString();
                    matchs[i].IUOO_fh = GetAlertWd21(matchs[i].MatchId);
                    //matchs[i].IUOO_hdp = GetMatchIuooAlert_hdp(matchs[i].MatchId);
                    matchs[i].IUOO_hdp = GetAlertWd20(matchs[i].MatchId);
                    //matchs[i].PenHistory = GetPenHistories(matchs[i].MatchId);
                    matchs[i].PHBeforeLive = Common.Functions.GetProductOuFtCountHistoryBeforeLive(matchs[i].MatchId);
                    matchs[i].PHLive = Common.Functions.GetProductOuFtCountHistoryLive(matchs[i].MatchId);
                    matchs[i].HdpAlert = Common.Functions.GetHdpAlert(matchs[i].MatchId);
                    matchs[i].OverUnderMoneyLine = Common.Functions.GetOverUnderLine(matchs[i].OverUnderMoneyLines);
                    
                    matchs[i].MinMaxFt = Common.Functions.GetProductStatusLog1x2HasPrice(matchs[i].MatchId, Enums.BetType.FullTime1x2);
                    matchs[i].MinMaxFh = Common.Functions.GetProductStatusLog1x2HasPrice(matchs[i].MatchId, Enums.BetType.FirstHalf1x2);

                    matchs[i].MPH = Common.Functions.GetProductStatusLog1x2HasPriceBeforeLive(matchs[i].MatchId, Enums.BetType.FullTime1x2, matchs[i].MPH);
                    matchs[i].SPT = Common.Functions.GetProductStatusLog1x2HasPriceBeforeLive(matchs[i].MatchId, Enums.BetType.FirstHalf1x2, matchs[i].SPT);

                    if (matchs[i].HomeRed > 0)
                    {
                        matchs[i].Home = "(" + matchs[i].HomeRed + ") " + matchs[i].Home;
                    }
                    if (matchs[i].AwayRed > 0)
                    {
                        matchs[i].Away = matchs[i].Away + " (" + matchs[i].AwayRed + ")";
                    }
                    //List<string> Ps = Common.Functions.GetProductIndex(matchs[i].MatchId);

                    //matchs[i].P1 = Ps.Count > 0 ? Ps[0] : "";
                    //matchs[i].P2 = Ps.Count > 1 ? Ps[1] : "";
                    //matchs[i].P3 = Ps.Count > 2 ? Ps[2] : "";
                    //matchs[i].P4 = Ps.Count > 3 ? Ps[3] : "";


                }
            }


            for (int i = 0; i < matchs.Count; i++)
            {
                if (matchs[i].LivePeriod == 0 && matchs[i].IsHT)
                {
                    matchs[i].LivePeriod = 3;
                }
            }

            //matchs = matchs.OrderByDescending(item => item.LivePeriod)
            //.ThenBy(item => item.GlobalShowtime)
            //.ToList();

            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);
            dataGridView.Columns.Clear();
            dataGridView.DataSource = bindingSource;

            List<Models.DataModels.Match> totalMatchsLive = source
                .Where(item => item.HasStreaming)
                .ToList();

            List<Models.DataModels.Match> totalMatchsNoneLive = source
                .Where(item => !item.HasStreaming)
                .ToList();

            List<string> totalLeaugesLive = new List<string>();
            for (int i = 0; i < totalMatchsLive.Count; i++)
            {
                if (!totalLeaugesLive.Contains(totalMatchsLive[i].League))
                {
                    totalLeaugesLive.Add(totalMatchsLive[i].League);
                }
            }
            int totalLeaugeLive = totalLeaugesLive.Count;
            int totalMatchLive = totalMatchsLive.Count;

            List<string> totalLeaugesNoneLive = new List<string>();
            for (int i = 0; i < totalMatchsNoneLive.Count; i++)
            {
                if (!totalLeaugesNoneLive.Contains(totalMatchsNoneLive[i].League))
                {
                    totalLeaugesNoneLive.Add(totalMatchsNoneLive[i].League);
                }
            }
            int totalLeaugeNoneLive = totalLeaugesNoneLive.Count;
            int totalMatchNoneLive = totalMatchsNoneLive.Count;


            dataGridView.Columns["League"].HeaderText = "League (" + totalLeaugeLive + " - " + totalMatchLive + ") (" + totalLeaugeNoneLive + " - " + totalMatchNoneLive + ")";
            dataGridView.Columns["MaxBetFirstShow"].Visible = false;
            dataGridView.Columns["MaxBetBeginLive"].Visible = false;
            dataGridView.Columns["MatchCode"].Visible = false;
            dataGridView.Columns["GlobalShowtime"].Visible = false;
            dataGridView.Columns["FirstShow"].Visible = false;
            dataGridView.Columns["IUOO_ft"].Visible = false;
            dataGridView.Columns["IsHT"].Visible = false;
            dataGridView.Columns["HomeRed"].Visible = false;
            dataGridView.Columns["AwayRed"].Visible = false;
            dataGridView.Columns["OpenPrices"].Visible = false;
            dataGridView.Columns["CountGlobalShowtimeChange"].Visible = false;
            dataGridView.Columns["CountMatchCodeChange"].Visible = false;

            dataGridView.Columns["P1"].Visible = false;
            dataGridView.Columns["P2"].Visible = false;
            dataGridView.Columns["P3"].Visible = false;
            dataGridView.Columns["P4"].Visible = false;
            dataGridView.Columns["TimeSpanFromStart"].Visible = false;
            dataGridView.Columns["MatchId"].Visible = false;
            dataGridView.Columns["PHLive"].Visible = false;
            dataGridView.Columns["PHBeforeLive"].Visible = false;
            dataGridView.Columns["IsNoShowMatch"].Visible = false;
            dataGridView.Columns["Home"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //HomeRed

            //dataGridView.Columns["OverUnderMoneyLines"].Visible = false;

            //dataGridView.Columns.Add(new DataGridViewButtonColumn()
            //{
            //    UseColumnTextForButtonValue = true,
            //    Text = "Under FT",
            //    Name = "BetUnderFt",
            //    HeaderText = "Under FT"
            //});

            //dataGridView.Columns.Add(new DataGridViewButtonColumn()
            //{
            //    UseColumnTextForButtonValue = true,
            //    Text = "Under FH",
            //    Name = "BetUnderFh",
            //    HeaderText = "Under Fh"
            //});

            if (DataStore.SemiAutoBetOnly)
            {
                dataGridView.Columns["GoalHistory"].Visible = false;
                dataGridView.Columns["OpenHdps"].Visible = false;
                dataGridView.Columns["OpenPrices"].Visible = false;
                dataGridView.Columns["IUOO_ft"].Visible = false;
                dataGridView.Columns["IUOO_fh"].Visible = false;
                dataGridView.Columns["IUOO_hdp"].Visible = false;
                dataGridView.Columns["PenHistory"].Visible = false;
                //dataGridView.Columns["BetUnderFt"].Visible = false;
                //dataGridView.Columns["BetUnderFh"].Visible = false;
            }



            SetStyle();
        }

        private List<Match> UpdateOrder(List<Match> matchs)
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


        private delegate void dlgSetSpeakerMainGrid(long matchId);
        public void SetSpeakerMainGrid(long matchId)
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgSetSpeakerMainGrid(SetSpeakerMainGrid), matchId);
                return;
            }
            try
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (dataGridView.Rows[i].Cells["MatchId"].Value.ToString() == matchId.ToString())
                    {
                        if (DataStore.KeepPlaySoundAlUpdatePhOuLive)
                        {
                            //
                            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\oven-tick.wav");
                        }


                        dataGridView.Rows[i].Cells["PenHistory"].Value = Common.Constants.SpeakerChar;
                        DataStore.Matchs[matchId].LastHasSpeaker = DateTime.Now;
                    }
                }
            }
            catch
            {

            }


            //🔊
        }

        private delegate void dlgSetId3StyleMainGrid(long matchId);
        public void SetId3StyleMainGrid(long matchId)
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgSetId3StyleMainGrid(SetId3StyleMainGrid), matchId);
                return;
            }
            try
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (dataGridView.Rows[i].Cells["MatchId"].Value.ToString() == matchId.ToString())
                    {
                        //Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\oringz-w429-349.wav");
                        //dataGridView.Rows[i].Cells["PenHistory"].Style.BackColor = Color.Yellow;
                    }
                }
            }
            catch
            {

            }

        }

        private string GetOpenHdps(long matchId)
        {
            Dictionary<int, List<Models.DataModels.OverUnderScoreTimesV2Item>> data;

            if (!DataStore.OverUnderScoreTimesV3.TryGetValue(matchId, out data)) return "";
            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            if (scores.Count == 0) return "";

            List<Models.DataModels.ProductIdLog> products = DataStore.ProductIdsOfMatch[matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder)
                .OrderBy(item => item.Minute)
                .ToList();

            List<List<string>> rows = new List<List<string>>();

            List<int> hdps = new List<int>();
            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = new List<string>();
                List<string> rowData = data[scores[i]].Select(item => item.OddsId.ToString()).ToList();


                row.Add(scores[i].ToString());
                row.AddRange(rowData);
                rows.Add(row);

            }

            for (int i = 0; i < scores.Count; i++)
            {
                List<string> row = new List<string>();
                List<string> rowData = data[scores[i]].Select(item => item.OddsId.ToString()).ToList();
                row.Add(scores[i].ToString());
                row.AddRange(rowData);

                if (row.Count > 0 && row[1] != "0")
                {
                    hdps.Add(scores[i]);
                }
            }

            int openHdp = 0;
            int firstHdp = GetFirstHdps(matchId);
            if (hdps.Count > 0)
            {
                openHdp = hdps.OrderByDescending(item => item).FirstOrDefault();
            }

            //return openHdp + " " + firstHdp;

            if (openHdp != 0 && firstHdp != 0)
            {
                if (openHdp > firstHdp) return openHdp + " > " + firstHdp;
                else if (openHdp < firstHdp) return openHdp + " < " + firstHdp;
                else return openHdp + " = " + firstHdp;
            }
            return "";

        }

        private int GetFirstHdps(long matchId)
        {
            Dictionary<int, List<Models.DataModels.OverUnderScoreTimesV2Item>> data;

            if (!DataStore.OverUnderScoreTimesV3.TryGetValue(matchId, out data)) return 0;
            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            if (scores.Count == 0) return 0;

            List<Models.DataModels.ProductIdLog> products = DataStore.ProductIdsOfMatch[matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder)
                .OrderBy(item => item.Minute)
                .ToList();

            List<List<string>> rows = new List<List<string>>();
            int firstIndex = 47;
            List<int> hdps = new List<int>();
            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = new List<string>();
                List<string> rowData = data[scores[i]].Select(item => item.OddsId.ToString()).ToList();


                row.Add(scores[i].ToString());
                row.AddRange(rowData);
                rows.Add(row);

                for (int j = 2; j <= 46; j++)
                {
                    if (row[j] != "0" && firstIndex > j)
                    {
                        firstIndex = j;
                        break;
                    }
                }
            }


            if (firstIndex == 47) return 0;

            for (int i = 0; i < scores.Count; i++)
            {
                List<string> row = new List<string>();
                List<string> rowData = data[scores[i]].Select(item => item.OddsId.ToString()).ToList();
                row.Add(scores[i].ToString());
                row.AddRange(rowData);
                if (rowData.Count > 0 && rowData[firstIndex] != "0")
                {
                    hdps.Add(scores[i]);
                }
            }

            if (hdps.Count > 0)
            {
                return hdps.OrderByDescending(item => item).FirstOrDefault();
            }

            return 0;

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

        private string GetMatchIuooAlert_ft(long matchId)
        {
            if (DataStore.MatchIuooAlert.ContainsKey(matchId)
                && DataStore.MatchIuooAlert[matchId].OUFT_Price != 0)
            {
                return DataStore.MatchIuooAlert[matchId].OUFT_Hdp + ": " + DataStore.MatchIuooAlert[matchId].OUFT_Price;
            }
            return "";
        }

        private string GetMatchIuooAlert_fh(long matchId)
        {
            if (DataStore.MatchIuooAlert.ContainsKey(matchId)
                && DataStore.MatchIuooAlert[matchId].OUFH_Price != 0)
            {
                return DataStore.MatchIuooAlert[matchId].OUFH_Hdp + ": " + DataStore.MatchIuooAlert[matchId].OUFH_Price;
            }
            return "";
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

        private string GetAlertWd22(long matchId)
        {
            Models.DataModels.Match match = DataStore.Matchs[matchId];
            LiveBetApp.Models.DataModels.Alert alert;
            if (DataStore.Alert_Wd22.ContainsKey(matchId))
            {
                alert = DataStore.Alert_Wd22[matchId];
                if (alert.TimeSpanFromCreate.TotalSeconds <= 46)
                {
                    if (DateTime.Now.Subtract(match.LastUpdateStepPriceDownPlaySound).TotalSeconds >= 35)
                    {
                        DataStore.Matchs[match.MatchId].LastUpdateStepPriceDownPlaySound = DateTime.Now;
                        //Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alarm-fast-high-pitch.wav");
                    }
                    return Common.Constants.SpeakerChar;
                }
            }
            return "";
        }


        private string GetMatchIuooAlert_hdp(long matchId)
        {
            if (DataStore.MatchIuooAlert.ContainsKey(matchId)
                && DataStore.MatchIuooAlert[matchId].Handicap_Price != 0)
            {
                return DataStore.MatchIuooAlert[matchId].Handicap_Hdp + ": " + DataStore.MatchIuooAlert[matchId].Handicap_Price;
            }
            return "";
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
                    //if (i == goalHistory.Count - 1)
                    //{
                    //    result += goalHistory[i].GoalTeam;
                    //}
                    //else
                    //{
                    //    result += (goalHistory[i].GoalTeam + ".");
                    //}

                }
            }
            return result;
        }

        private string GetShortTx(long matchId, Dictionary<long, Dictionary<int, Models.DataModels.IuooValue>> data)
        {
            string result = "";
            if (data.ContainsKey(matchId))
            {
                List<int> keys = data[matchId].Keys.OrderByDescending(item => item).ToList();
                if (keys.Count > 0)
                {
                    result += (keys[0] + ": ");
                }

                for (int i = 0; i < keys.Count; i++)
                {
                    if (keys[i] % 100 == 50)
                    {
                        result += ("_" + data[matchId][keys[i]].TX + "_");
                    }
                    else
                    {
                        result += data[matchId][keys[i]].TX;
                    }

                    if (i != keys.Count - 1)
                    {
                        result += ".";
                    }
                }
            }
            return result;
        }

        private void SetStyle()
        {
            
            dataGridView.Columns["FreqBeforeLive"].Width = 30;
            dataGridView.Columns["MatchCodeStr"].Width = 75;
            dataGridView.Columns["League"].Width = 235;
            dataGridView.Columns["League"].DefaultCellStyle = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold), ForeColor = Color.Black };
            dataGridView.Columns["GlobalShowtimeStr"].Width = 30;
            dataGridView.Columns["FirstShowStr"].Width = 25;
            dataGridView.Columns["FirstShowIndex"].Width = 15;
            dataGridView.Columns["DiffMinute"].Width = 25;
            dataGridView.Columns["LivePeriod"].Width = 15;
            dataGridView.Columns["OverUnderMoneyLine"].Width = 25;

            dataGridView.Columns["MPH"].Width = 25;
            dataGridView.Columns["SPT"].Width = 25;

            dataGridView.Columns["Order"].Width = 18;
            dataGridView.Columns["MinuteFromStart"].Width = 18;
            dataGridView.Columns["Home"].Width = 110;
            dataGridView.Columns["LiveHomeScore"].Width = 15;
            dataGridView.Columns["LiveAwayScore"].Width = 15;
            dataGridView.Columns["Away"].Width = 110;
            dataGridView.Columns["Home"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["LiveHomeScore"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["LiveAwayScore"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["HasStreaming"].Width = 20;
            dataGridView.Columns["HdpAlert"].Width = 30;
            dataGridView.Columns["IUOO_fh"].Width = 15;
            dataGridView.Columns["IUOO_ft"].Width = 15;
            dataGridView.Columns["IUOO_hdp"].Width = 15;
            dataGridView.Columns["PriceStep"].Width = 15;
            dataGridView.Columns["PriceStepDown"].Width = 15;
            //dataGridView.Columns["PHBeforeLive"].Width = 40;
            //dataGridView.Columns["PHLive"].Width = 50;
            dataGridView.Columns["GoalHistory"].Width = 50;
            dataGridView.Columns["PenHistory"].Width = 15;
            dataGridView.Columns["OpenHdps"].Width = 50;
            dataGridView.Columns["MinMaxFt"].Width = 50;
            dataGridView.Columns["MinMaxFh"].Width = 70;

            //dataGridView.Columns["FirstShowStr"].Width = 30;
            //dataGridView.Columns["MatchCodeStr"].Width = 30;
            //dataGridView.Columns["MaxBetFirstShow"].Width = 15;
            //dataGridView.Columns["MaxBetBeginLive"].Width = 15;
            //dataGridView.Columns["P1"].Width = 8;
            //dataGridView.Columns["P2"].Width = 8;
            //dataGridView.Columns["P3"].Width = 8;
            //dataGridView.Columns["P4"].Width = 8;
            //dataGridView.Columns["Order"].Width = 8;
            //dataGridView.Columns["HdpAlert"].Width = 15;
            //dataGridView.Columns["GlobalShowtimeStr"].Width = 25;
            //dataGridView.Columns["DiffMinute"].Width = 10;
            //dataGridView.Columns["LivePeriod"].Width = 10;
            //dataGridView.Columns["OverUnderMoneyLine"].Width = 40;

            dataGridView.Columns["MinuteFromStart"].DefaultCellStyle = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

            for (int i = 0; i < dataGridView.RowCount; i++)
            {



                string matchId = dataGridView.Rows[i].Cells["MatchId"].Value.ToString();
                Models.DataModels.Match match = DataStore.Matchs[long.Parse(matchId)];

                //if (match.OverUnderMoneyLine == 18)
                //{
                //    dataGridView.Rows[i].Cells["League"].Style = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 6, FontStyle.Bold), ForeColor = Color.Black, BackColor = (i % 2 == 0 ? Color.LightGray : this.dataGridView.DefaultCellStyle.BackColor)  };
                //}
                //else
                //{
                //    dataGridView.Rows[i].Cells["League"].Style = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 6.5f, FontStyle.Regular), ForeColor = Color.Black, BackColor = (i % 2 == 0 ? Color.LightGray : this.dataGridView.DefaultCellStyle.BackColor) };
                //}

                if (DateTime.Now.Subtract(match.LastHasSpeaker).TotalSeconds <= 46)
                {
                    dataGridView.Rows[i].Cells["PenHistory"].Value = Common.Constants.SpeakerChar;
                    if (DateTime.Now.Subtract(match.LastUpdatePhOuFtLivePlaySound).TotalSeconds >= 20)
                    {
                        DataStore.Matchs[match.MatchId].LastUpdatePhOuFtLivePlaySound = DateTime.Now;
                        if (DataStore.KeepPlaySoundAlUpdatePhOuLive)
                        {
                            //
                            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\oven-tick.wav");
                        }

                    }
                }

                //if (DateTime.Now.Subtract(match.LastHasId3).TotalSeconds <= 46)
                //{
                //    dataGridView.Rows[i].Cells["PenHistory"].Style.BackColor = Color.Yellow;

                //    if (DateTime.Now.Subtract(match.LastUpdatePhLivePlaySound).TotalSeconds >= 35)
                //    {
                //        DataStore.Matchs[match.MatchId].LastUpdatePhLivePlaySound = DateTime.Now;
                //        //Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\oringz-w429-349.wav");
                //    }
                //}

                if (DataStore.Alert_Wd22.ContainsKey(match.MatchId)
                    && DataStore.Alert_Wd22[match.MatchId].TimeSpanFromCreate.TotalSeconds <= 46)
                {
                    dataGridView.Rows[i].Cells["IUOO_hdp"].Style.BackColor = Color.Yellow;
                    if (DateTime.Now.Subtract(match.LastUpdateStepPricePlaySound).TotalSeconds >= 35)
                    {
                        DataStore.Matchs[match.MatchId].LastUpdateStepPricePlaySound = DateTime.Now;
                        if (DataStore.KeepPlaySoundAl22)
                        {
                            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alarm-fast-a1.wav");
                        }

                    }
                }


                //if (DataStore.Alert_Wd23.ContainsKey(match.MatchId)
                //    && DataStore.Alert_Wd23[match.MatchId].TimeSpanFromCreate.TotalSeconds <= 46)
                //{
                //    dataGridView.Rows[i].Cells["PriceStepDown"].Style.BackColor = Color.Yellow;
                //}


                if (DataStore.Alert_Wd39.ContainsKey(match.MatchId)
                    && DataStore.Alert_Wd39[match.MatchId].TimeSpanFromCreate.TotalSeconds <= 46)
                {
                    dataGridView.Rows[i].Cells["PriceStepDown"].Style.BackColor = Color.Yellow;
                    if (DateTime.Now.Subtract(match.LastUpdateStepPriceDownPlaySound).TotalSeconds >= 35)
                    {
                        DataStore.Matchs[match.MatchId].LastUpdateStepPriceDownPlaySound = DateTime.Now;
                        dataGridView.Rows[i].Cells["PriceStepDown"].Value = Common.Constants.SpeakerChar;
                        //
                        Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\insight-578.wav");
                    }
                }

                if (match.BeginLive != null && DateTime.Now.Subtract(match.BeginLive.Value).TotalSeconds < 60)
                {
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }

                if (match.LiveAwayScore + match.LiveHomeScore >= 3)
                {
                    dataGridView.Rows[i].Cells["GoalHistory"].Style.BackColor = Color.LightPink;
                }

                //if (match.LivePeriod == 2
                //    || match.IsHT
                //    || match.LivePeriod == -1)
                //{
                //    dataGridView.Rows[i].Cells["BetUnderFh"] = new DataGridViewButtonCell();
                //}


                //if (match.LivePeriod == -1)
                //{
                //    dataGridView.Rows[i].Cells["BetUnderFt"] = new DataGridViewButtonCell();
                //    dataGridView.Rows[i].Cells["BetUnderFh"] = new DataGridViewButtonCell();
                //}

                //if (DataStore.BetDirect.Any(item => item.MatchId == match.MatchId && item.IsFt)
                //    || DataStore.FinishedBetDirect.Any(item => item.MatchId == match.MatchId && item.IsFt))
                //{
                //    dataGridView.Rows[i].Cells["BetUnderFt"] = new DataGridViewButtonCell() { Value = "placed", Style = new DataGridViewCellStyle() { BackColor = Color.Green } };
                //}

                //if (DataStore.BetDirect.Any(item => item.MatchId == match.MatchId && !item.IsFt)
                //    || DataStore.FinishedBetDirect.Any(item => item.MatchId == match.MatchId && !item.IsFt))
                //{
                //    dataGridView.Rows[i].Cells["BetUnderFh"] = new DataGridViewButtonCell() { Value = "placed", Style = new DataGridViewCellStyle() { BackColor = Color.Green } };
                //}

                long firstShowNumDT = long.Parse(match.FirstShow.ToString(Common.Constants.NumDtFormat));

                if (dataGridView.Rows[i].Cells["GlobalShowtimeStr"].Value.ToString().Contains(":00")
                    || dataGridView.Rows[i].Cells["GlobalShowtimeStr"].Value.ToString().Contains(":15")
                    || dataGridView.Rows[i].Cells["GlobalShowtimeStr"].Value.ToString().Contains(":30"))
                {

                }
                else
                {
                    dataGridView.Rows[i].Cells["GlobalShowtimeStr"].Style = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, this.dataGridView.DefaultCellStyle.Font.Size, FontStyle.Bold) };
                }

                if (!DataStore.AppStartLog.Contains(firstShowNumDT)
                    && match.FirstShow.Subtract(match.GlobalShowtime).TotalMinutes >= -30
                    && match.FirstShow.Subtract(match.GlobalShowtime).TotalMinutes < 0)
                {
                    dataGridView.Rows[i].Cells["OpenHdps"].Style.BackColor = Color.Yellow;
                    //dataGridView.Rows[i].Cells["OpenPrices"].Style.BackColor = Color.Yellow;
                    dataGridView.Rows[i].Cells["GlobalShowtimeStr"].Style.BackColor = Color.Yellow;
                }

                if (!DataStore.AppStartLog.Contains(firstShowNumDT)
                    && match.FirstShow.Subtract(match.GlobalShowtime).TotalMinutes >= -60
                    && match.FirstShow.Subtract(match.GlobalShowtime).TotalMinutes < -30)
                {
                    dataGridView.Rows[i].Cells["OpenHdps"].Style.BackColor = Color.LightGreen;
                    //dataGridView.Rows[i].Cells["OpenPrices"].Style.BackColor = Color.LightGreen;
                    dataGridView.Rows[i].Cells["GlobalShowtimeStr"].Style.BackColor = Color.LightGreen;
                }

                if (!DataStore.AppStartLog.Contains(firstShowNumDT)
                    && match.FirstShow.Subtract(match.GlobalShowtime).TotalMinutes >= 1)
                {
                    dataGridView.Rows[i].Cells["OpenHdps"].Style.BackColor = Color.OrangeRed;
                    //dataGridView.Rows[i].Cells["OpenPrices"].Style.BackColor = Color.OrangeRed;
                    dataGridView.Rows[i].Cells["GlobalShowtimeStr"].Style.BackColor = Color.OrangeRed;
                }

                if (match.LivePeriod == 1)
                {
                    dataGridView.Rows[i].Cells["MatchCodeStr"].Style.BackColor = Color.GreenYellow;
                    dataGridView.Rows[i].Cells["LivePeriod"].Style.BackColor = Color.GreenYellow;
                    dataGridView.Rows[i].Cells["MinuteFromStart"].Style.BackColor = Color.GreenYellow;
                    dataGridView.Rows[i].Cells["LiveHomeScore"].Style.BackColor = Color.GreenYellow;
                    dataGridView.Rows[i].Cells["LiveAwayScore"].Style.BackColor = Color.GreenYellow;
                    //dataGridView.Rows[i].Cells["League"].Style.BackColor = Color.GreenYellow;
                }

                if (match.LivePeriod == 2 || match.LivePeriod == 3)
                {
                    dataGridView.Rows[i].Cells["FreqBeforeLive"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["LiveAwayScore"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
                }

                if (match.LivePeriod == 2
                    && (
                        dataGridView.Rows[i].Cells["FreqBeforeLive"].Value.ToString().Contains("00 00")
                        || dataGridView.Rows[i].Cells["FreqBeforeLive"].Value.ToString().Contains("01 00")
                        || dataGridView.Rows[i].Cells["FreqBeforeLive"].Value.ToString().Contains("00 01")
                    )
                )
                {
                    dataGridView.Rows[i].Cells["FreqBeforeLive"].Style.BackColor = Color.GreenYellow;
                }

                if (match.LivePeriod == 2)
                {
                    dataGridView.Rows[i].Cells["LivePeriod"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView.Rows[i].Cells["MinuteFromStart"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (match.LiveAwayScore + match.LiveHomeScore > 0)
                {
                    dataGridView.Rows[i].Cells["LiveAwayScore"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["LiveAwayScore"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
                    dataGridView.Rows[i].Cells["LiveHomeScore"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["LiveHomeScore"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
                }



                if (DataStore.MatchIuooAlert.ContainsKey(match.MatchId))
                {
                    int totalScore = match.LiveAwayScore + match.LiveHomeScore;

                    LiveBetApp.Models.DataModels.Product selectedProductOuFt = DataStore.Products.Values
                        .FirstOrDefault(item => item.MatchId == match.MatchId
                            && item.Bettype == Enums.BetType.FullTimeOverUnder
                            && (((item.Hdp1 + item.Hdp2) * 100) - (totalScore * 100)) == DataStore.MatchIuooAlert[match.MatchId].OUFT_Hdp
                        );

                    LiveBetApp.Models.DataModels.Product selectedProductOuFh = DataStore.Products.Values
                        .FirstOrDefault(item => item.MatchId == match.MatchId
                            && item.Bettype == Enums.BetType.FirstHalfOverUnder
                            && (((item.Hdp1 + item.Hdp2) * 100) - (totalScore * 100)) == DataStore.MatchIuooAlert[match.MatchId].OUFH_Hdp
                        );

                    LiveBetApp.Models.DataModels.Product selectedProductHdpFt_Home = DataStore.Products.Values
                        .FirstOrDefault(item => item.MatchId == match.MatchId
                            && item.Bettype == Enums.BetType.FullTimeHandicap
                            && (item.Hdp1 * 100) == DataStore.MatchIuooAlert[match.MatchId].Handicap_Hdp
                            && item.Hdp2 == 0
                        );

                    LiveBetApp.Models.DataModels.Product selectedProductHdpFt_Away = DataStore.Products.Values
                        .FirstOrDefault(item => item.MatchId == match.MatchId
                            && item.Bettype == Enums.BetType.FullTimeHandicap
                            && (item.Hdp2 * 100) == DataStore.MatchIuooAlert[match.MatchId].Handicap_Hdp
                            && item.Hdp1 == 0
                        );

                    if (selectedProductOuFt != null)
                    {
                        int currentPrice = selectedProductOuFt.Odds1a100;
                        int alertPrice = DataStore.MatchIuooAlert[match.MatchId].OUFT_Price;

                        if ((alertPrice * currentPrice > 0 && currentPrice >= alertPrice)
                            || (alertPrice > 0 && currentPrice < 0))
                        {
                            dataGridView.Rows[i].Cells["IUOO_ft"].Style.BackColor = Color.LightSkyBlue;
                        }
                    }

                    if (selectedProductOuFh != null)
                    {
                        int currentPrice = selectedProductOuFh.Odds1a100;
                        int alertPrice = DataStore.MatchIuooAlert[match.MatchId].OUFH_Price;

                        if ((alertPrice * currentPrice > 0 && currentPrice >= alertPrice)
                            || (alertPrice > 0 && currentPrice < 0))
                        {
                            dataGridView.Rows[i].Cells["IUOO_fh"].Style.BackColor = Color.LightSkyBlue;
                        }
                    }

                    if (selectedProductHdpFt_Home != null)
                    {
                        int currentPrice = selectedProductHdpFt_Home.Odds1a100;
                        int alertPrice = DataStore.MatchIuooAlert[match.MatchId].Handicap_Price;

                        if ((alertPrice * currentPrice > 0 && currentPrice >= alertPrice)
                            || (alertPrice > 0 && currentPrice < 0))
                        {
                            //dataGridView.Rows[i].Cells["IUOO_hdp"].Style.BackColor = Color.LightSkyBlue;
                        }
                    }

                    if (selectedProductHdpFt_Away != null)
                    {
                        int currentPrice = selectedProductHdpFt_Away.Odds2a100;
                        int alertPrice = DataStore.MatchIuooAlert[match.MatchId].Handicap_Price;

                        if ((alertPrice * currentPrice > 0 && currentPrice >= alertPrice)
                            || (alertPrice > 0 && currentPrice < 0))
                        {
                            //dataGridView.Rows[i].Cells["IUOO_hdp"].Style.BackColor = Color.LightSkyBlue;
                        }
                    }

                }

                if (DataStore.GoalHistories.ContainsKey(match.MatchId))
                {
                    Models.DataModels.GoalHistory lastGoalHistory = DataStore.GoalHistories[match.MatchId].LastOrDefault();
                    if (lastGoalHistory != null
                        && lastGoalHistory.LivePeriod == match.LivePeriod
                        && Math.Abs((int)lastGoalHistory.TimeSpanFromStart.TotalMinutes - (int)match.TimeSpanFromStart.TotalMinutes) <= 2)
                    {
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    }
                }

                if (!Common.Functions.HasPriceAtBegin(match.MatchId))
                {
                    //dataGridView.Rows[i].Cells["FirstShowStr"].Style.BackColor = Color.DeepSkyBlue;
                }

                if (DataStore.Alert_Wd24.ContainsKey(match.MatchId))
                {
                    if (DataStore.Alert_Wd24[match.MatchId].TimeSpanFromCreate.TotalMinutes <= 5)
                    {
                        LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd24[match.MatchId];
                        if (alert.TimeSpanFromCreate.TotalSeconds <= 46)
                        {
                            if (DateTime.Now.Subtract(match.LastAlert24PlaySound).TotalSeconds >= 60)
                            {
                                DataStore.Matchs[match.MatchId].LastAlert24PlaySound = DateTime.Now;
                                if (DataStore.KeepPlaySoundAl24)
                                {
                                    Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alert.wav");
                                }

                            }
                        }
                        dataGridView.Rows[i].Cells["HasStreaming"].Style.BackColor = Color.Yellow;
                    }
                    else
                    {
                        dataGridView.Rows[i].Cells["HasStreaming"].Style.BackColor = Color.GreenYellow;
                    }
                }

                if (!dataGridView.Rows[i].Cells["HasStreaming"].Value.ToString().Contains("-"))
                {
                    dataGridView.Rows[i].Cells["HasStreaming"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["HasStreaming"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold), ForeColor = Color.Black };
                }

                if (DateTime.Now.Subtract(match.LastTimeHasTGT225).TotalMinutes <= 2 && match.LastTimeHasTGT225 != DateTime.MinValue)
                {
                    dataGridView.Rows[i].Cells["IUOO_ft"].Style.BackColor = Color.YellowGreen;
                }

                if (DateTime.Now.Subtract(match.LastTime_Ou_050_87).TotalMinutes <= 2 && match.LastTime_Ou_050_87 != DateTime.MinValue)
                {
                    dataGridView.Rows[i].Cells["IUOO_fh"].Style.BackColor = Color.YellowGreen;
                }

                if (match.LivePeriod > 0 || match.IsHT)
                {
                    if (DateTime.Now.Subtract(match.LastUpdatePhOuFtLive).TotalSeconds <= 120)
                    {
                        //dataGridView.Rows[i].Cells["PHLive"].Style.BackColor = Color.Gold;
                    }
                }

                //if (DataStore.Alert_Wd25.ContainsKey(match.MatchId))
                //{
                //    LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd25[match.MatchId];

                //    if ((alert.CustomValue.Contains("up") || alert.CustomValue.Contains("down"))
                //        && (DateTime.Now.Subtract(match.LastAlert25PlaySound).TotalSeconds >= 35))
                //    {
                //        DataStore.Matchs[match.MatchId].LastAlert25PlaySound = DateTime.Now;
                //        if (DataStore.KeepPlaySoundAl25)
                //        {
                //            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\whistle.wav");
                //        }

                //    }

                //    if (alert.CustomValue.Contains("up"))
                //    {
                //        dataGridView.Rows[i].Cells["HdpAlert"].Style.BackColor = Color.Red;
                //    }
                //    else if (alert.CustomValue.Contains("down"))
                //    {
                //        dataGridView.Rows[i].Cells["HdpAlert"].Style.BackColor = Color.LightSkyBlue;
                //    }
                //}

                if (DataStore.Alert_Wd42.ContainsKey(match.MatchId))
                {
                    LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd42[match.MatchId];
                    dataGridView.Rows[i].Cells["HdpAlert"].Style.BackColor = Color.Red;
                    if (alert.TimeSpanFromCreate.TotalSeconds <= 300)
                    {
                        
                        if (DateTime.Now.Subtract(match.LastAlert42_43PlaySound).TotalSeconds >= 60)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert42_43PlaySound = DateTime.Now;
                            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\preview.wav");
                        }
                    }
                }

                if (DataStore.Alert_Wd43.ContainsKey(match.MatchId))
                {
                    LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd43[match.MatchId];
                    dataGridView.Rows[i].Cells["HdpAlert"].Style.BackColor = Color.LightSkyBlue;
                    if (alert.TimeSpanFromCreate.TotalSeconds <= 300)
                    {
                        
                        if (DateTime.Now.Subtract(match.LastAlert42_43PlaySound).TotalSeconds >= 60)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert42_43PlaySound = DateTime.Now;
                            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\preview.wav");
                        }
                    }
                }


                string hdpAlert = "";

                if (DataStore.Alert_Wd44.ContainsKey(match.MatchId))
                {
                    LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd44[match.MatchId];
                    hdpAlert += alert.CustomValue;
                }

                if (DataStore.Alert_Wd38.ContainsKey(match.MatchId))
                {
                    LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd38[match.MatchId];
                    //if (alert.TimeSpanFromCreate.TotalSeconds <= 120)
                    //{
                    //    if (DateTime.Now.Subtract(match.LastAlert38PlaySound).TotalSeconds >= 60)
                    //    {
                    //        DataStore.Matchs[match.MatchId].LastAlert38PlaySound = DateTime.Now;
                    //        if (DataStore.KeepPlaySoundAl38)
                    //        {
                    //            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\mixkit-arabian-mystery-harp-notification.wav");
                    //        }
                    //    }
                    //    dataGridView.Rows[i].Cells["HdpAlert"].Value = "**" + alert.CustomValue;
                    //}
                    //else
                    //{
                    //    dataGridView.Rows[i].Cells["HdpAlert"].Value = alert.CustomValue;
                    //}
                    hdpAlert += alert.CustomValue;
                }

                dataGridView.Rows[i].Cells["HdpAlert"].Value = hdpAlert;

                if (DataStore.Alert_Wd6.ContainsKey(match.MatchId))
                {
                    dataGridView.Rows[i].Cells["Home"].Style.BackColor = Color.Black;
                    dataGridView.Rows[i].Cells["Away"].Style.BackColor = Color.Black;

                    dataGridView.Rows[i].Cells["Home"].Style.ForeColor = Color.White;
                    dataGridView.Rows[i].Cells["Away"].Style.ForeColor = Color.White;
                }

                //if (DataStore.Alert_Wd26.ContainsKey(match.MatchId))
                //{
                //    LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd26[match.MatchId];
                //    if (alert.TimeSpanFromCreate.TotalSeconds <= 120)
                //    {
                //        if (DateTime.Now.Subtract(match.LastAlert26PlaySound).TotalSeconds >= 60)
                //        {
                //            DataStore.Matchs[match.MatchId].LastAlert26PlaySound = DateTime.Now;
                //            if (DataStore.KeepPlaySoundAl26)
                //            {
                //                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\bome.wav");
                //            }
                //        }
                //        dataGridView.Rows[i].Cells["PHBeforeLive"].Style.BackColor = Color.SkyBlue;
                //    }
                //    else
                //    {
                //        dataGridView.Rows[i].Cells["PHBeforeLive"].Style.BackColor = Color.Gold;
                //    }
                //}

                if (DataStore.Alert_Wd34.ContainsKey(match.MatchId))
                {
                    LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd34[match.MatchId];
                    if (alert.TimeSpanFromCreate.TotalSeconds <= 120)
                    {
                        if (DateTime.Now.Subtract(match.LastAlert34PlaySound).TotalSeconds >= 60)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert34PlaySound = DateTime.Now;
                            if (DataStore.KeepPlaySoundAl34)
                            {
                                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\bome.wav");
                            }
                        }
                        //dataGridView.Rows[i].Cells["PHBeforeLive"].Style.BackColor = Color.Gold;
                    }
                    else
                    {
                        //dataGridView.Rows[i].Cells["PHBeforeLive"].Style.BackColor = Color.Red;
                    }
                }

                if (DataStore.Alert_Wd27.ContainsKey(match.MatchId))
                {
                    LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd27[match.MatchId];
                    if (alert.TimeSpanFromCreate.TotalSeconds <= 120)
                    {
                        if (DateTime.Now.Subtract(match.LastAlert27PlaySound).TotalSeconds >= 60)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert27PlaySound = DateTime.Now;
                            if (DataStore.KeepPlaySoundAl27)
                            {
                                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alarm-fast-high-pitch.wav");
                            }
                        }
                        dataGridView.Rows[i].Cells["DiffMinute"].Style.BackColor = Color.SkyBlue;
                        dataGridView.Rows[i].Cells["DiffMinute"].Value = (int)match.GlobalShowtime.Subtract(match.FirstShow).TotalMinutes + " - " + alert.CustomValue;
                    }
                    else
                    {
                        dataGridView.Rows[i].Cells["DiffMinute"].Style.BackColor = Color.Gold;
                        dataGridView.Rows[i].Cells["DiffMinute"].Value = (int)match.GlobalShowtime.Subtract(match.FirstShow).TotalMinutes + " - " + alert.CustomValue;
                    }
                }

                //if (DataStore.Alert_Wd28.ContainsKey(match.MatchId))
                //{
                //    LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd28[match.MatchId];
                //    if (alert.TimeSpanFromCreate.TotalSeconds <= 120)
                //    {
                //        if (DateTime.Now.Subtract(match.LastAlert28PlaySound).TotalSeconds >= 60)
                //        {
                //            DataStore.Matchs[match.MatchId].LastAlert28PlaySound = DateTime.Now;
                //            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\metallic-238.wav");
                //        }
                //        dataGridView.Rows[i].Cells["PriceStep"].Style.BackColor = Color.Blue;
                //    }
                //    else
                //    {
                //        dataGridView.Rows[i].Cells["PriceStep"].Style.BackColor = Color.Gold;
                //    }
                //}

                if (DataStore.Alert_Wd30.ContainsKey(match.MatchId) && match.LivePeriod == 1)
                {
                    LiveBetApp.Models.DataModels.Alert alert = DataStore.Alert_Wd30[match.MatchId];
                    if (alert.TimeSpanFromCreate.TotalSeconds <= 120)
                    {
                        if (DateTime.Now.Subtract(match.LastAlert30PlaySound).TotalSeconds >= 60)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert30PlaySound = DateTime.Now;
                            if (DataStore.KeepPlaySoundAl30)
                            {
                                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\metallic-238.wav");
                            }

                        }
                        dataGridView.Rows[i].Cells["PriceStep"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dataGridView.Rows[i].Cells["PriceStep"].Style.BackColor = Color.Gold;
                    }
                }

                if (DataStore.Alert_Wd29.ContainsKey(match.MatchId))
                {
                    Models.DataModels.Alert alert29 = DataStore.Alert_Wd29[match.MatchId];
                    dataGridView.Rows[i].Cells["OverUnderMoneyLine"].Style.BackColor = Color.Green;
                    if (alert29.TimeSpanFromCreate.TotalSeconds <= 120)
                    {

                        if (DateTime.Now.Subtract(match.LastAlert29PlaySound).TotalSeconds >= 60)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert29PlaySound = DateTime.Now;
                            if (DataStore.KeepPlaySoundAl29)
                            {
                                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\oringz-w429-349.wav");
                            }

                        }

                        if (alert29.CustomValue == "1")
                        {
                            dataGridView.Rows[i].Cells["OverUnderMoneyLine"].Style.BackColor = Color.LightSkyBlue;
                        }
                        else if (alert29.CustomValue == "-1")
                        {
                            dataGridView.Rows[i].Cells["OverUnderMoneyLine"].Style.BackColor = Color.Yellow;
                        }

                    }

                }

                if (DataStore.Alert_Wd31.ContainsKey(match.MatchId))
                {
                    Models.DataModels.Alert alert31 = DataStore.Alert_Wd31[match.MatchId];
                    dataGridView.Rows[i].Cells["LivePeriod"].Style.BackColor = Color.Blue;
                    if (alert31.TimeSpanFromCreate.TotalSeconds <= 120)
                    {
                        if (DateTime.Now.Subtract(match.LastAlert31PlaySound).TotalSeconds >= 60)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert31PlaySound = DateTime.Now;
                            if (DataStore.KeepPlaySoundAl31)
                            {
                                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\bip-bip.wav");
                            }

                        }
                        dataGridView.Rows[i].Cells["LivePeriod"].Style.BackColor = Color.Red;
                    }
                }

                if (DataStore.Alert_Wd32.ContainsKey(match.MatchId))
                {
                    Models.DataModels.Alert alert32 = DataStore.Alert_Wd32[match.MatchId];
                    dataGridView.Rows[i].Cells["LiveHomeScore"].Style.BackColor = Color.Red;
                    dataGridView.Rows[i].Cells["LiveAwayScore"].Style.BackColor = Color.Red;
                    if (alert32.TimeSpanFromCreate.TotalSeconds <= 120)
                    {
                        if (DateTime.Now.Subtract(match.LastAlert32PlaySound).TotalSeconds >= 60)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert32PlaySound = DateTime.Now;
                            if (DataStore.KeepPlaySoundAl32)
                            {
                                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\mixkit-clear-announce.wav");
                            }

                        }
                        dataGridView.Rows[i].Cells["LiveHomeScore"].Style.BackColor = Color.SkyBlue;
                        dataGridView.Rows[i].Cells["LiveAwayScore"].Style.BackColor = Color.SkyBlue;
                    }


                }

                if (DataStore.Alert_Wd33.ContainsKey(match.MatchId) && DataStore.SpecialFilter)
                {
                    Models.DataModels.Alert alert33 = DataStore.Alert_Wd33[match.MatchId];
                    if (alert33.TimeSpanFromCreate.TotalSeconds <= 120)
                    {
                        if (DateTime.Now.Subtract(match.LastAlert33PlaySound).TotalSeconds >= 60)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert33PlaySound = DateTime.Now;
                            //Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\mixkit-clear-announce.wav");
                        }
                        dataGridView.Rows[i].Cells["PenHistory"].Style.BackColor = Color.SkyBlue;
                    }
                }

                if (DataStore.Alert_Wd35.ContainsKey(match.MatchId))
                {
                    Models.DataModels.Alert alert35 = DataStore.Alert_Wd35[match.MatchId];
                    dataGridView.Rows[i].Cells["League"].Style.BackColor = alert35.CustomValue == "red" ? Color.Red :
                        alert35.CustomValue == "yellow" ? Color.Yellow :
                        Color.White;
                    //KeepPlaySoundAl35
                }

                //if (DataStore.Alert_Wd36.ContainsKey(match.MatchId))
                //{
                //    Models.DataModels.Alert alert36 = DataStore.Alert_Wd36[match.MatchId];
                //    if (alert36.TimeSpanFromCreate.TotalSeconds <= 120)
                //    {
                //        if (DateTime.Now.Subtract(match.LastAlert36PlaySound).TotalSeconds >= 60)
                //        {
                //            DataStore.Matchs[match.MatchId].LastAlert36PlaySound = DateTime.Now;
                //            if (DataStore.KeepPlaySoundAl36_37)
                //            {
                //                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\explode3_e23e2.wav");
                //                dataGridView.Rows[i].Cells["PenHistory"].Value = Common.Constants.SpeakerChar;
                //            }
                //        }
                //        dataGridView.Rows[i].Cells["MinMaxFt"].Style.BackColor = Color.Gold;
                //    }
                //    else
                //    {
                //        dataGridView.Rows[i].Cells["MinMaxFt"].Style.BackColor = Color.Red;
                //    }
                //    dataGridView.Rows[i].Cells["MinMaxFt"].Value = alert36.CustomValue;
                //}

                //if (DataStore.Alert_Wd37.ContainsKey(match.MatchId))
                //{
                //    Models.DataModels.Alert alert37 = DataStore.Alert_Wd37[match.MatchId];
                //    if (alert37.TimeSpanFromCreate.TotalSeconds <= 120)
                //    {
                //        if (DateTime.Now.Subtract(match.LastAlert37PlaySound).TotalSeconds >= 60)
                //        {
                //            DataStore.Matchs[match.MatchId].LastAlert37PlaySound = DateTime.Now;
                //            if (DataStore.KeepPlaySoundAl36_37)
                //            {
                //                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\explode3_e23e2.wav");
                //                dataGridView.Rows[i].Cells["PenHistory"].Value = Common.Constants.SpeakerChar;
                //            }
                //        }
                //        dataGridView.Rows[i].Cells["MinMaxFh"].Style.BackColor = Color.Gold;
                //    }
                //    else
                //    {
                //        dataGridView.Rows[i].Cells["MinMaxFh"].Style.BackColor = Color.Red;
                //    }
                //    dataGridView.Rows[i].Cells["MinMaxFh"].Value = alert37.CustomValue;

                //}


                bool hasSpeakerHome = false;
                if (DataStore.Alert_Wd40.ContainsKey(match.MatchId))
                {
                    Models.DataModels.Alert alert40 = DataStore.Alert_Wd40[match.MatchId];
                    if (alert40.TimeSpanFromCreate.TotalSeconds <= 90)
                    {
                        if (DateTime.Now.Subtract(match.LastAlert40PlaySound).TotalSeconds >= 60
                            && DataStore.KeepPlaySoundAl40_41)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert40PlaySound = DateTime.Now;
                            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alarm-1-with-reverberation.wav");
                        }
                        hasSpeakerHome = true;
                    }

                }

                dataGridView.Rows[i].Cells["Home"].Value = 
                    (hasSpeakerHome ? (Common.Constants.SpeakerChar + " ") : " ") 
                    + (match.HomeRed > 0 ? ("(" + match.HomeRed + ") ") : "")
                    + match.Home;

                bool hasSpeakerAway = false;
                if (DataStore.Alert_Wd41.ContainsKey(match.MatchId))
                {
                    Models.DataModels.Alert alert41 = DataStore.Alert_Wd41[match.MatchId];
                    if (alert41.TimeSpanFromCreate.TotalSeconds <= 90)
                    {
                        if (DateTime.Now.Subtract(match.LastAlert41PlaySound).TotalSeconds >= 60
                            && DataStore.KeepPlaySoundAl40_41)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert41PlaySound = DateTime.Now;
                            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\emergency-alarm-with-reverb.wav");
                        }
                        hasSpeakerAway = true;
                    }
                }

                if (DataStore.Alert_Wd45.ContainsKey(match.MatchId))
                {
                    Models.DataModels.Alert alert45 = DataStore.Alert_Wd45[match.MatchId];
                    dataGridView.Rows[i].Cells["MatchCodeStr"].Style.BackColor = Color.Red;
                    if (alert45.TimeSpanFromCreate.TotalSeconds <= 90)
                    {
                        if (DateTime.Now.Subtract(match.LastAlert45PlaySound).TotalSeconds >= 60)
                        {
                            DataStore.Matchs[match.MatchId].LastAlert45PlaySound = DateTime.Now;
                            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\call-to-attention.wav");
                        }
                    }
                }

                if (DataStore.Alert_Wd46.ContainsKey(match.MatchId)
                    && match.LivePeriod == 1
                    && (match.LiveHomeScore + match.LiveAwayScore == 1))
                {
                    dataGridView.Rows[i].Cells["MatchCodeStr"].Style.BackColor = Color.Yellow;
                }

                dataGridView.Rows[i].Cells["Away"].Value =
                    match.Away
                    + (match.AwayRed > 0 ? (" (" + match.AwayRed + ")") : "")
                    + (hasSpeakerAway ? (" " + Common.Constants.SpeakerChar) : "");

                dataGridView.Rows[i].Cells["MinMaxFt"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["MinMaxFt"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold), ForeColor = Color.Black };
                if (Common.Functions.NeedShowColorGetProductStatusLog1x2HasPrice(match, Enums.BetType.FullTime1x2))
                {
                    dataGridView.Rows[i].Cells["MinMaxFt"].Style.BackColor = Color.Yellow;
                }
                else
                {
                    dataGridView.Rows[i].Cells["MinMaxFt"].Style.BackColor = dataGridView.Rows[i].DefaultCellStyle.BackColor;
                }

                //dataGridView.Rows[i].Cells["MinMaxFh"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["MinMaxFh"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold), ForeColor = Color.Black };
                if (Common.Functions.NeedShowColorGetProductStatusLog1x2HasPrice(match, Enums.BetType.FirstHalf1x2))
                {
                    dataGridView.Rows[i].Cells["MinMaxFh"].Style.BackColor = Color.Yellow;
                }
                else
                {
                    dataGridView.Rows[i].Cells["MinMaxFh"].Style.BackColor = dataGridView.Rows[i].DefaultCellStyle.BackColor;
                }

                //matchs[i].MinMaxFt = Common.Functions.GetProductStatusLog1x2HasPrice(matchs[i].MatchId, Enums.BetType.FullTime1x2);
                //matchs[i].MinMaxFh = Common.Functions.GetProductStatusLog1x2HasPrice(matchs[i].MatchId, Enums.BetType.FirstHalf1x2);


                if (match.IsNoShowMatch)
                {
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                    dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }

                int countClosePriceBeforeLive = Common.Functions.CountClosePriceBeforeLive(match.MatchId, 0, 30);
                if (countClosePriceBeforeLive > 0)
                {
                    dataGridView.Rows[i].Cells["SPT"].Style.BackColor = Color.Pink;
                    dataGridView.Rows[i].Cells["SPT"].Value = dataGridView.Rows[i].Cells["SPT"].Value.ToString() + countClosePriceBeforeLive;
                }

                int countClosePriceBeforeLive2 = Common.Functions.CountClosePriceBeforeLive(match.MatchId, 31, 10000);
                if (countClosePriceBeforeLive2 > 0)
                {
                    dataGridView.Rows[i].Cells["MPH"].Style.BackColor = Color.Pink;
                    dataGridView.Rows[i].Cells["MPH"].Value = countClosePriceBeforeLive2;
                }

                if (Common.Functions.HasActionInHalfTime(match.MatchId))
                {
                    dataGridView.Rows[i].Cells["Order"].Style.BackColor = Color.Violet;
                }

                try
                {
                    string orderStr = dataGridView.Rows[i].Cells["Order"].Value.ToString();
                    int order = int.Parse(orderStr);
                    if (order >= 1 && order <= 21)
                    {
                        dataGridView.Rows[i].Cells["Order"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["Order"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold), ForeColor = Color.Black };
                    }
                }
                catch
                {

                }

                if (match.LivePeriod != -1)
                {
                    if (match.Home== "FK Buxoro")
                    {

                    }
                    int teamStronger = Common.Functions.GetTeamStronger(match.MatchId);
                    if (teamStronger == 1)
                    {
                        dataGridView.Rows[i].Cells["Home"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["Home"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold) };
                        dataGridView.Rows[i].Cells["Away"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["Away"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Regular) };
                    }
                    else if (teamStronger == -1)
                    {
                        dataGridView.Rows[i].Cells["Home"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["Home"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Regular) };
                        dataGridView.Rows[i].Cells["Away"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["Away"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold) };
                    }
                    else
                    {
                        dataGridView.Rows[i].Cells["Home"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["Home"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Regular) };
                        dataGridView.Rows[i].Cells["Away"].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells["Away"].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Regular) };
                    }
                }



                //if (DataStore.ShowPreLiveMatchsOnlyChecked)
                //{
                //    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                //    dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;

                //    dataGridView.Rows[i].Cells["League"].Style.BackColor = Color.Black;
                //    dataGridView.Rows[i].Cells["League"].Style.ForeColor = Color.White;


                //    dataGridView.Rows[i].Cells["OpenHdps"].Style.BackColor = Color.Black;
                //    dataGridView.Rows[i].Cells["OpenHdps"].Style.ForeColor = Color.White;

                //    //dataGridView.Rows[i].Cells["OpenPrices"].Style.BackColor = Color.Black;
                //    //dataGridView.Rows[i].Cells["OpenPrices"].Style.ForeColor = Color.White;

                //    dataGridView.Rows[i].Cells["GlobalShowtimeStr"].Style.BackColor = Color.Black;
                //    dataGridView.Rows[i].Cells["GlobalShowtimeStr"].Style.ForeColor = Color.White;

                //}

                dataGridView.Rows[i].Cells["P1"].Style.BackColor = Common.Functions.GetMainFormPcolor(dataGridView.Rows[i].Cells["P1"].Value.ToString());
                dataGridView.Rows[i].Cells["P2"].Style.BackColor = Common.Functions.GetMainFormPcolor(dataGridView.Rows[i].Cells["P2"].Value.ToString());
                dataGridView.Rows[i].Cells["P3"].Style.BackColor = Common.Functions.GetMainFormPcolor(dataGridView.Rows[i].Cells["P3"].Value.ToString());
                dataGridView.Rows[i].Cells["P4"].Style.BackColor = Common.Functions.GetMainFormPcolor(dataGridView.Rows[i].Cells["P4"].Value.ToString());

                dataGridView.Rows[i].Cells["P1"].Value = dataGridView.Rows[i].Cells["P1"].Value.ToString().Replace("*", "");
                dataGridView.Rows[i].Cells["P2"].Value = dataGridView.Rows[i].Cells["P2"].Value.ToString().Replace("*", "");
                dataGridView.Rows[i].Cells["P3"].Value = dataGridView.Rows[i].Cells["P3"].Value.ToString().Replace("*", "");
                dataGridView.Rows[i].Cells["P4"].Value = dataGridView.Rows[i].Cells["P4"].Value.ToString().Replace("*", "");

                dataGridView.Rows[i].Cells["FirstShowIndex"].Style.BackColor = GetFirstShowIndexColor(dataGridView.Rows[i].Cells["FirstShowIndex"].Value.ToString());
                dataGridView.Rows[i].Cells["FirstShowIndex"].Style.ForeColor = GetFirstShowIndexColorForeColor(dataGridView.Rows[i].Cells["FirstShowIndex"].Value.ToString());


            }
        }

        private Color GetFirstShowIndexColor(string cellValue)
        {
            if (cellValue == "1") return Color.Violet;
            else if (cellValue == "2") return Color.Indigo;
            else if (cellValue == "3") return Color.Blue;
            else if (cellValue == "4") return Color.Green;
            else if (cellValue == "5") return Color.Yellow;
            else if (cellValue == "6") return Color.Orange;
            else if (cellValue == "7") return Color.Red;
            return Color.White;
        }

        private Color GetFirstShowIndexColorForeColor(string cellValue)
        {
            if (cellValue == "1") return Color.Black;
            else if (cellValue == "2") return Color.White;
            else if (cellValue == "3") return Color.White;
            else if (cellValue == "4") return Color.Black;
            else if (cellValue == "5") return Color.Black;
            else if (cellValue == "6") return Color.Black;
            else if (cellValue == "7") return Color.Black;
            return Color.Black;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Really close?", "Confirm exit", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
            }
        }

        private void livescoreMatchsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void matchTimmingAlertToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)dataGridView.Rows[e.RowIndex].Cells[0].Value;
            if (e.ColumnIndex == dataGridView.Columns["GlobalShowtimeStr"].Index)
            {
                try
                {
                    using (GlobalShowtimeHistoryForm form = new GlobalShowtimeHistoryForm(matchId))
                    {
                        form.ShowDialog();
                    }
                }
                catch { }

            }
            else if (e.ColumnIndex == dataGridView.Columns["MatchCodeStr"].Index)
            {
                try
                {
                    using (MatchCodeHistoryForm form = new MatchCodeHistoryForm(matchId))
                    {
                        form.ShowDialog();
                    }
                }
                catch { }

            }
            else if (e.ColumnIndex == dataGridView.Columns["MPH"].Index)
            {
                try
                {
                    ProductIdsLogForm form = new ProductIdsLogForm(matchId);
                    form.Show();
                }
                catch { }
            }
            else if (e.ColumnIndex == dataGridView.Columns["SPT"].Index)
            {
                try
                {
                    ProductIdHistoryDetailByTypeForm form = new ProductIdHistoryDetailByTypeForm(matchId);
                    form.Show();
                }
                catch { }
            }
            else if (e.ColumnIndex == dataGridView.Columns["Order"].Index)
            {
                try
                {
                    ProductHistoryInspectorForm form = new ProductHistoryInspectorForm(
                        matchId,
                        0,
                        1
                    );
                    form.Show();
                }
                catch { }
            }
            else if (e.ColumnIndex == dataGridView.Columns["FreqBeforeLive"].Index)
            {
                try
                {
                    PriceFreqForm form = new PriceFreqForm(matchId);
                    form.Show();
                }
                catch { }
            }
            else if (e.ColumnIndex == dataGridView.Columns["MinuteFromStart"].Index)
            {
                try
                {
                    PriceChangeForm form = new PriceChangeForm(matchId);
                    form.Show();
                }
                catch (Exception ex)
                {
                    Common.Functions.WriteExceptionLog(ex);
                }
            }
            //else if (e.ColumnIndex == dataGridView.Columns["FirstShowStr"].Index)
            //{
            //    DateTime dt = DataStore.Matchs[matchId].FirstShow;
            //    try
            //    {
            //        using (FindDecoyForm form = new FindDecoyForm(dt))
            //        {
            //            form.ShowDialog();
            //        }
            //    }
            //    catch { }
            //}
            //else if (e.ColumnIndex == dataGridView.Columns["BetUnderFt"].Index
            //    || e.ColumnIndex == dataGridView.Columns["BetUnderFh"].Index
            //    || e.RowIndex < 0)
            //{
            //    return;
            //}
            else
            {
                OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId, this);
                form.Show();
            }

        }

        private void matchFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatchFilter form = new MatchFilter(this);
            form.Show();
        }

        private void runningTimmingBetOverToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void autoBetMonitoringToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void betByHdpPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timmingBetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllTimmingAutoBetForm form = new AllTimmingAutoBetForm();
            form.Show();
        }

        private void betByHdpPriceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AllAutoBetByHdpPriceForm form = new AllAutoBetByHdpPriceForm();
            form.Show();
        }

        private void betByHdpPriceToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AllBetByHdpPriceForm form = new AllBetByHdpPriceForm();
            form.Show();
        }

        private void timmingBetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AllBetByTimmingForm form = new AllBetByTimmingForm();
            form.Show();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    long matchId = (long)dataGridView.Rows[e.RowIndex].Cells[0].Value;
                    LiveBetApp.Models.DataModels.Match match;
                    if (DataStore.Matchs.TryGetValue(matchId, out match)
                        && match.LivePeriod >= 0)
                    {
                        //if (e.ColumnIndex == dataGridView.Columns["BetUnderFt"].Index)
                        //{
                        //    using (ConfirmDirectBet form = new ConfirmDirectBet(matchId, true))
                        //    {
                        //        form.ShowDialog();
                        //    }
                        //}
                        //else if (e.ColumnIndex == dataGridView.Columns["BetUnderFh"].Index
                        //    && match.LivePeriod == 1)
                        //{
                        //    using (ConfirmDirectBet form = new ConfirmDirectBet(matchId, false))
                        //    {
                        //        form.ShowDialog();
                        //    }
                        //}
                    }

                }

            }
            catch
            {

            }
        }

        private void autoBetSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AutoBetSettingForm form = new AutoBetSettingForm())
            {
                form.ShowDialog();
            }
        }

        private void betFirstHalfByFulltimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllBetFirstHalfByFulltimeForm form = new AllBetFirstHalfByFulltimeForm();
            form.Show();
        }

        private void alertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainAlertForm form = new MainAlertForm();
            form.Show();
        }

        private void bookmarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookmarksForm form = new BookmarksForm();
            form.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void blackListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlacklistForm form = new BlacklistForm();
            form.Show();
        }

        private void betByHdpCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllBetByHdpCloseForm form = new AllBetByHdpCloseForm();
            form.Show();
        }

        private void betAfterGoodPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllBetAfterGoodPrice form = new AllBetAfterGoodPrice();
            form.Show();
        }

        private void betDirectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllDirectBetForm form = new AllDirectBetForm();
            form.Show();
        }

        private void betHandicapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllBetByHandicapPriceForm form = new AllBetByHandicapPriceForm();
            form.Show();
        }

        private void copyAlltoClipboard()
        {
            dataGridView.SelectAll();
            dataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = dataGridView.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void alertSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SoundForm form = new SoundForm())
            {
                form.ShowDialog();
            }
        }

        private void mainFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void inspectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ExportInspectorForm form = new ExportInspectorForm())
            {
                form.ShowDialog();
            }
        }

        private void backupScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BackupScheduleForm form = new BackupScheduleForm())
            {
                form.ShowDialog();
            }
        }


        private void requestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaxBetFullMatchForm form = new MaxBetFullMatchForm(false);
            form.Show();
        }

        private void nonRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaxBetFullMatchForm form = new MaxBetFullMatchForm(true);
            form.Show();
        }

        private void x2ProductHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product1x2HistoryForm form = new Product1x2HistoryForm();
            form.Show();
        }
    }
}
