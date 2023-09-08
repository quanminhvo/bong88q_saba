using AutoMapper.Execution;
using LiveBetApp.Common;
using LiveBetApp.Forms.Alerts;
using LiveBetApp.Forms.BetAfterGoodPrice;
using LiveBetApp.Forms.BetByHdpClose;
using LiveBetApp.Forms.BetByHdpPrice;
using LiveBetApp.Forms.BetByQuick;
using LiveBetApp.Forms.BetByTimming;
using LiveBetApp.Forms.BetFirstHalfByFulltime;
using LiveBetApp.Forms.BetHandicapByPrice;
using LiveBetApp.Forms.TimmingBet;
using LiveBetApp.Models.DataModels;
using System;
using System.Collections;
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
using static LiveBetApp.Common.Enums;

namespace LiveBetApp.Forms
{
    public partial class OverUnderScoreTimeForm : Form
    {
        private long _matchId;
        private Thread _executeThread;
        private bool _enableXtraView;
        private bool _enableXtraViewPriceUp;
        private bool _enableXtraViewPriceDown;
        private MainForm _mainForm = null;

        private List<IuooValue_3> _dataIuoo_1;
        private List<IuooValue_3> _dataIuoo_2;

        public OverUnderScoreTimeForm(long matchId, MainForm mainForm = null)
        {
            this._mainForm = mainForm;
            DataStore.OpeningDetailMatch.Add(matchId);
            this._matchId = matchId;
            this._enableXtraView = false;
            this._enableXtraViewPriceUp = false;
            this._enableXtraViewPriceDown = true;
            
            InitializeComponent();
            this.Location = new Point(0, 0);
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            ReloadQuickBet();
            Common.Functions.SetMiniStyle(this.dataGridView);
            Common.Functions.SetMiniStyle(this.dgvFirstHalf);
            Common.Functions.SetMiniStyle(this.dgvUnderPrice);
            //Common.Functions.SetMiniStyle(this.dgvSubtractFt90);
            //Common.Functions.SetMiniStyle(this.dgvSubtractFh);
            //Common.Functions.SetMiniStyle(this.dgvSummaryTable);
            //Common.Functions.SetMiniStyle(this.dgvSummaryTableV2);
            //Common.Functions.SetMiniStyle(this.dgvSubtractFt45);

            tableLayoutPanel3.ColumnStyles[0].SizeType = SizeType.Absolute;
            if (DataStore.Theme == Enums.VisualThemes.TV_FULL_HD)
            {
                tableLayoutPanel3.ColumnStyles[0].Width = 1600;
            }
            else if (DataStore.Theme == Enums.VisualThemes.TV_HD)
            {
                tableLayoutPanel3.ColumnStyles[0].Width = 1350;
            }
            else
            {
                tableLayoutPanel3.ColumnStyles[0].Width = 1150;
            }

        }

        private void OverUnderScoreTimeForm_Load(object sender, EventArgs e)
        {
            if (DataStore.BookmarkedMatchs.Contains(this._matchId))
            {
                this.bookmarkToolStripMenuItem.Text = "Bookmark_ed";
            }

            UpdateMenuBar();

            if (DataStore.SemiAutoBetOnly)
            {
                if (DataStore.Matchs.ContainsKey(_matchId))
                {
                    Match match = DataStore.Matchs[_matchId];
                    this.Text = match.Home + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")"
                        + " | " + Common.Functions.GetOverUnderMoneyLinesString(_matchId)
                        + " | " + match.League
                        + " | SHEET TABLE";
                }
                this.viewToolStripMenuItem.Visible = false;
                this.xtraViewToolStripMenuItem1.Visible = false;
                this.iUOOAlertToolStripMenuItem.Visible = false;
                //this.Height = 70;
                //this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //this.MaximumSize = new Size(int.MaxValue, 70);
                return;
            }

            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        LoadProductCountHistory();
                        LoadMainGrid();
                        LoadUnderPriceGrid();
                        ColorIuoo();
                        LoadOverUnderScoreTimesFirstHalfGrid();
                        LoadOverUnderScoreRemainingTimeV2();
                        LoadOverUnderScoreRemainingTimeFhV2();
                        LoadInsertFtH1Grid();

                        //DataStore.MatchNumberOfProductFulltimeOU
                        //DataStore.MatchMaxNumberOfProductFulltimeOU
                        //if (DataStore.MatchMaxNumberOfProductFulltimeOU.ContainsKey(_matchId)
                        //    && DataStore.MatchMaxNumberOfProductFulltimeOU[_matchId] <= 2)
                        //{
                        LoadIuooGrid();
                        LoadIuooGrid_2();
                        //LoadDgvSumTx();
                        LoadDgvSummaryTable();
                        LoadDgvSummaryTableV2();
                        //}

                        if (DataStore.Matchs.ContainsKey(_matchId))
                        {
                            Match match = DataStore.Matchs[_matchId];
                            if (match.LivePeriod < 0) break;
                        }
                        Thread.Sleep(10000);
                    }
                    catch (Exception ex)
                    {
                        Common.Functions.WriteExceptionLog(ex);
                        Thread.Sleep(10000);
                    }
                }
            });

            _executeThread.IsBackground = true;
            _executeThread.Start();

        }

        private delegate void dlgLoadDgvSummaryTableV2();
        private void LoadDgvSummaryTableV2()
        {
            if (this.dgvSummaryTableV2.InvokeRequired)
            {
                this.Invoke(new dlgLoadDgvSummaryTableV2(LoadDgvSummaryTableV2));
                return;
            }

            int rowCount = 0;
            int columnCount = 0;
            List<int> hdpsBeforeLive = GetHdpsBeforeLive();
            int openMax = Common.Functions.GetHdpsOpenMax(_matchId);
            int hdpNgl = Common.Functions.GetNglByOpenMax(openMax) * 25;
            List<int> latestHdps = GetLatestHdps();
            List<int> latestHalftimeHdps = GetLatestHalftimeHdps();
            List<SummaryItemGoalV2> summaryItemGoalV2s = GetSummaryItemGoalV2s();
            int maxRowItemGoalCount = 1;
            if (summaryItemGoalV2s.Count > 0)
            {
                maxRowItemGoalCount = summaryItemGoalV2s.Max(item => ((item.NewMaxRealHdp - item.NewMinRealHdp) / 25)) + 1;
            }

            rowCount = hdpsBeforeLive.Count > rowCount ? hdpsBeforeLive.Count : rowCount;
            rowCount = maxRowItemGoalCount > rowCount ? maxRowItemGoalCount : rowCount;
            columnCount = 4 + summaryItemGoalV2s.Count * 3;

            this.dgvSummaryTableV2.Rows.Clear();
            this.dgvSummaryTableV2.ColumnCount = columnCount;

            for (int i = 0; i < this.dgvSummaryTableV2.ColumnCount; i++)
            {
                this.dgvSummaryTableV2.Columns[i].Width = 28;
            }

            for (int i = 0; i < rowCount; i++)
            {
                List<string> row = new List<string>();
                if (hdpsBeforeLive.Count > i) row.Add(Common.Functions.HdpToNumberConvert(hdpsBeforeLive[i]).ToString());
                else row.Add("");

                row.Add("");
                row.Add("");

                if (i == 0)
                {
                    row.Add(Common.Functions.HdpToNumberConvert(openMax).ToString());
                }
                else
                {
                    row.Add("");
                }

                for (int j = 0; j < summaryItemGoalV2s.Count; j++)
                {
                    if (i == 0)
                    {
                        row.Add(Common.Functions.HdpToNumberConvert(summaryItemGoalV2s[j].GoalHdp).ToString());
                        row.Add(Common.Functions.HdpToNumberConvert(summaryItemGoalV2s[j].NewMaxHdp).ToString());
                    }
                    else
                    {
                        row.Add("");
                        row.Add("");
                    }


                    List<int> realHdps = ExtractHdps(summaryItemGoalV2s[j].NewMinRealHdp, summaryItemGoalV2s[j].NewMaxRealHdp);
                    if (realHdps.Count > i)
                    {
                        row.Add(Common.Functions.HdpToNumberConvert(realHdps[i]).ToString());
                    }
                    else
                    {
                        row.Add("");
                    }
                    
                }

                this.dgvSummaryTableV2.Rows.Add(row.ToArray());

            }


            List<string> emptyRow = new List<string>();
            emptyRow.Add("");
            emptyRow.Add("");
            emptyRow.Add("");
            emptyRow.Add("");
            for (int i = 0; i < summaryItemGoalV2s.Count; i++)
            {
                emptyRow.Add("");
                emptyRow.Add("");
                emptyRow.Add("");
            }

            this.dgvSummaryTableV2.Rows.Add(emptyRow.ToArray());
            this.dgvSummaryTableV2.Rows.Add(emptyRow.ToArray());

            List<string> levelRow = new List<string>();
            levelRow.Add("");
            levelRow.Add("");
            levelRow.Add("");
            levelRow.Add(Common.Functions.GetLevelByOpenMax(openMax).ToString());
            for (int i = 0; i < summaryItemGoalV2s.Count; i++)
            {
                levelRow.Add(Common.Functions.GetLevelByOpenMax(summaryItemGoalV2s[i].GoalHdp).ToString());
                levelRow.Add(Common.Functions.GetLevelByOpenMax(summaryItemGoalV2s[i].NewMaxHdp).ToString());
                levelRow.Add("");
            }

            this.dgvSummaryTableV2.Rows.Add(levelRow.ToArray());


            List<string> subtractRow = new List<string>();
            subtractRow.Add(latestHdps.Count > 0 ? Common.Functions.HdpToNumberConvert(latestHdps[0]).ToString() : "");
            subtractRow.Add(Common.Functions.HdpToNumberConvert(hdpNgl).ToString());
            subtractRow.Add(latestHalftimeHdps.Count > 0 ? Common.Functions.HdpToNumberConvert(latestHalftimeHdps[0]).ToString() : "");
            subtractRow.Add(Common.Functions.HdpToNumberConvert(openMax - 50).ToString());
            for (int i = 0; i < summaryItemGoalV2s.Count; i++)
            {
                int myBase = 50 + (4 * (i + 1)) * 25;
                subtractRow.Add(Common.Functions.HdpToNumberConvert(summaryItemGoalV2s[i].HdpWithoutGoal).ToString());
                subtractRow.Add(Common.Functions.HdpToNumberConvert(summaryItemGoalV2s[i].NewMaxHdp - myBase).ToString());
                subtractRow.Add(Common.Functions.HdpToNumberConvert(summaryItemGoalV2s[i].MaxHdpBeforeNextGoal).ToString());
            }
            this.dgvSummaryTableV2.Rows.Add(subtractRow.ToArray());

            List<string> subtractRow2nd = new List<string>();
            subtractRow2nd.Add(latestHdps.Count > 1 ? Common.Functions.HdpToNumberConvert(latestHdps[1]).ToString() : "");
            subtractRow2nd.Add(Common.Functions.HdpToNumberConvert(openMax - hdpNgl).ToString());
            subtractRow2nd.Add(latestHalftimeHdps.Count > 1 ? Common.Functions.HdpToNumberConvert(latestHalftimeHdps[1]).ToString() : "");
            subtractRow2nd.Add(Common.Functions.HdpToNumberConvert(50).ToString());
            for (int i = 0; i < summaryItemGoalV2s.Count; i++)
            {
                int myBase = 50 + (4 * (i + 1)) * 25;
                subtractRow2nd.Add(Common.Functions.HdpToNumberConvert(summaryItemGoalV2s[i].HdpWithoutGoal-25).ToString());
                subtractRow2nd.Add(Common.Functions.HdpToNumberConvert(myBase).ToString());
                subtractRow2nd.Add(Common.Functions.HdpToNumberConvert(summaryItemGoalV2s[i].MaxHdpBeforeNextGoal - 25).ToString());
            }
            this.dgvSummaryTableV2.Rows.Add(subtractRow2nd.ToArray());

            List<string> subRow = new List<string>();
            subRow.Add("");
            subRow.Add("");
            subRow.Add("");
            subRow.Add("");
            for (int i = 0; i < summaryItemGoalV2s.Count; i++)
            {
                int myBase = (4 * (i + 1)) * 25;
                subRow.Add("");
                subRow.Add(Common.Functions.HdpToNumberConvert(hdpNgl + myBase).ToString());
                subRow.Add("");
            }
            this.dgvSummaryTableV2.Rows.Add(subRow.ToArray());

            SetDgvSummaryTableV2Style();
        }

        private List<int> GetLatestHdps()
        {
            List<int> result = new List<int>();

            if (DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId))
            {
                Dictionary<int, List<OverUnderScoreTimesV2Item>> dataV2 = DataStore.OverUnderScoreTimesV2[_matchId];
                List<int> keys = dataV2.Keys.OrderByDescending(item => item).ToList();
                for (int minute = 90; minute >= 1; minute--)
                {
                    bool hasPrice = false;
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (dataV2[keys[i]][minute].Over != 0)
                        {
                            hasPrice = true;
                            break;
                        }
                    }
                    if (hasPrice)
                    {
                        for (int i = 0; i < keys.Count; i++)
                        {
                            if (dataV2[keys[i]][minute].Over != 0)
                            {
                                result.Add(keys[i]);
                            }
                        }
                    }
                }
            }

            return result;
        }

        private List<int> GetLatestHalftimeHdps()
        {
            List<int> result = new List<int>();

            if (DataStore.OverUnderScoreHalftime.ContainsKey(_matchId))
            {
                Dictionary<int, List<OverUnderScoreTimesV3Item>> dataV2 = DataStore.OverUnderScoreHalftime[_matchId];
                List<int> keys = dataV2.Keys.OrderByDescending(item => item).ToList();
                for (int minute = 29; minute >= 1; minute--)
                {
                    bool hasPrice = false;
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (dataV2[keys[i]][minute].Over != 0)
                        {
                            hasPrice = true;
                            break;
                        }
                    }
                    if (hasPrice)
                    {
                        for (int i = 0; i < keys.Count; i++)
                        {
                            if (dataV2[keys[i]][minute].Over != 0)
                            {
                                result.Add(keys[i]);
                            }
                        }
                        break;
                    }
                }
            }

            return result;
        }

        private delegate void dlgLoadProductCountHistory();
        private void LoadProductCountHistory()
        {
            if (this.lblOuFtBeforeLive.InvokeRequired)
            {
                this.Invoke(new dlgLoadProductCountHistory(LoadProductCountHistory));
                return;
            }

            this.lblOuFtBeforeLive.Text =   "OuFt: " + Common.Functions.GetGetProductCountHistory(_matchId, 
                DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder && item.Minute < 0)
                    .ToList(), 0, true);
            this.lblOuFtLive.Text =         "OuFt: " + Common.Functions.GetProductOuFtCountHistoryLive(_matchId);
            this.lblOuFtLiveMinute.Text =   "OuFt: " + Common.Functions.GetProductCountHistoryMinuteSpecial(_matchId, DataStore.ProductIdsOuFtOfMatchHistoryLive, Enums.BetType.FullTimeOverUnder);

            this.lblOuFhBeforeLive.Text =   "OuFh: " + Common.Functions.GetGetProductCountHistory(_matchId, 
                DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder && item.Minute < 0)
                    .ToList()
                , 0, true
            );
            this.lblOuFhLive.Text =         "OuFh: " + Common.Functions.GetGetProductCountHistory(_matchId, 
                DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder && item.Minute >= 0)
                    .ToList(),
                DataStore.ProductIdsOfMatch[_matchId].Count(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder && item.Minute < 0)
                , true
            );
            this.lblOuFhLiveMinute.Text =   "OuFh: " + Common.Functions.GetProductCountHistoryMinuteSpecial(_matchId, DataStore.ProductIdsOuFhOfMatchHistoryLive, Enums.BetType.FirstHalfOverUnder);

            this.lblHdpFtBeforeLive.Text =  "HdpFt: " + Common.Functions.GetGetProductCountHistory(_matchId, 
                DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeHandicap && item.Minute < 0)
                    .ToList(), 0, false);
            this.lblHdpFtLive.Text =        "HdpFt: " + Common.Functions.GetGetProductCountHistory(_matchId, 
                DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeHandicap && item.Minute >= 0)
                    .ToList(),
                DataStore.ProductIdsOfMatch[_matchId].Count(item => item.ProductBetType == Enums.BetType.FullTimeHandicap && item.Minute < 0)
                , false
            );
            this.lblHdpFtLiveMinute.Text =  "HdpFt: " + Common.Functions.GetProductCountHistoryMinuteSpecial(_matchId, DataStore.ProductIdsHdpFtOfMatchHistoryLive, Enums.BetType.FullTimeHandicap);

            this.lblHdpFhBeforeLive.Text =  "HdpFh: " + Common.Functions.GetGetProductCountHistory(_matchId, 
                DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FirstHalfHandicap && item.Minute < 0)
                    .ToList(), 0, false);
            this.lblHdpFhLive.Text =        "HdpFh: " + Common.Functions.GetGetProductCountHistory(_matchId, 
                DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FirstHalfHandicap && item.Minute >= 0)
                    .ToList(),
                 DataStore.ProductIdsOfMatch[_matchId].Count(item => item.ProductBetType == Enums.BetType.FirstHalfHandicap && item.Minute < 0)
                 , false
            );
            this.lblHdpFhLiveMinute.Text =  "HdpFh: " + Common.Functions.GetProductCountHistoryMinuteSpecial(_matchId, DataStore.ProductIdsHdpFhOfMatchHistoryLive, Enums.BetType.FirstHalfHandicap);

            try
            {
                Match match = DataStore.Matchs[_matchId];
                if (match.LivePeriod > 0 || match.IsHT)
                {
                    if (DateTime.Now.Subtract(match.LastUpdatePhOuFtLive).TotalSeconds <= 120)
                    {
                        lblOuFtLive.BackColor = Color.Gold;
                        if (match.LastUpdatePhOuFtLivePlaySound != match.LastUpdatePhOuFtLive)
                        {
                            try
                            {
                                DataStore.Matchs[_matchId].LastUpdatePhOuFtLivePlaySound = match.LastUpdatePhOuFtLive;
                                if (this._mainForm != null) this._mainForm.SetSpeakerMainGrid(_matchId);
                            }
                            catch
                            {

                            }

                        }
                    }
                    else
                    {
                        lblOuFtLive.BackColor = Label.DefaultBackColor;
                    }

                    if (DateTime.Now.Subtract(match.LastUpdatePhOuFhLive).TotalSeconds <= 120)
                    {
                        lblOuFhLive.BackColor = Color.Gold;
                        if (this._mainForm != null) this._mainForm.SetId3StyleMainGrid(_matchId);
                    }
                    else
                    {
                        lblOuFhLive.BackColor = Label.DefaultBackColor;
                    }

                    if (DateTime.Now.Subtract(match.LastUpdatePhHdpFhLive).TotalSeconds <= 120)
                    {
                        lblHdpFhLive.BackColor = Color.Gold;
                        if (this._mainForm != null) this._mainForm.SetId3StyleMainGrid(_matchId);
                    }
                    else
                    {
                        lblHdpFhLive.BackColor = Label.DefaultBackColor;
                    }

                    if (DateTime.Now.Subtract(match.LastUpdatePhHdpFtLive).TotalSeconds <= 120)
                    {
                        lblHdpFtLive.BackColor = Color.Gold;
                        if (this._mainForm != null) this._mainForm.SetId3StyleMainGrid(_matchId);
                    }
                    else
                    {
                        lblHdpFtLive.BackColor = Label.DefaultBackColor;
                    }
                }
            }
            catch
            {

            }
        }

        private void SetDgvSummaryTableV2Style()
        {
            Color color = Color.LightGray;
            for (int i = 1; i < this.dgvSummaryTableV2.Columns.Count; i += 3)
            {
                color = color == Color.LightGray ? Color.LightGreen : Color.LightGray;
                if (i == 1) color = Color.White;
                for (int j = 0; j < this.dgvSummaryTableV2.Rows.Count; j++)
                {
                    this.dgvSummaryTableV2.Rows[j].Cells[i + 0].Style.BackColor = color;
                    this.dgvSummaryTableV2.Rows[j].Cells[i + 1].Style.BackColor = color;
                    this.dgvSummaryTableV2.Rows[j].Cells[i + 2].Style.BackColor = color;
                }
            }

            if (this.dgvSummaryTableV2.Rows.Count > 2)
            {
                this.dgvSummaryTableV2.Rows[this.dgvSummaryTableV2.Rows.Count - 2].Cells[0].Style.ForeColor = Color.Red;
                this.dgvSummaryTableV2.Rows[this.dgvSummaryTableV2.Rows.Count - 2].Cells[2].Style.ForeColor = Color.Red;
            }

            if (this.dgvSummaryTableV2.Rows.Count > 3)
            {
                this.dgvSummaryTableV2.Rows[this.dgvSummaryTableV2.Rows.Count - 3].Cells[0].Style.ForeColor = Color.Red;
                this.dgvSummaryTableV2.Rows[this.dgvSummaryTableV2.Rows.Count - 3].Cells[2].Style.ForeColor = Color.Red;
            }

            for (int j = 6; j < this.dgvSummaryTableV2.Columns.Count; j+=3)
            {
                if (this.dgvSummaryTableV2.Rows.Count > 2)
                {
                    this.dgvSummaryTableV2.Rows[this.dgvSummaryTableV2.Rows.Count - 2].Cells[j].Style.ForeColor = Color.Red;
                }

                if (this.dgvSummaryTableV2.Rows.Count > 3)
                {
                    this.dgvSummaryTableV2.Rows[this.dgvSummaryTableV2.Rows.Count - 3].Cells[j].Style.ForeColor = Color.Red;
                }
            }

        }

        private List<int> ExtractHdps(int hdpFrom, int hdpTo)
        {
            List<int> result = new List<int>();

            for (int i = hdpTo; i >= hdpFrom; i -= 25)
            {
                result.Add(i);
            }

            return result;
        }

        private List<int> GetHdpsBeforeLive()
        {
            List<int> hdps = new List<int>();
            if (DataStore.OverUnderScoreTimesBeforeLive.ContainsKey(_matchId))
            {
                hdps = DataStore.OverUnderScoreTimesBeforeLive[_matchId].Keys
                    .OrderByDescending(item => item)
                    .ToList();
            }
            return hdps;
        }

        private List<SummaryItemGoalV2> GetSummaryItemGoalV2s()
        {
            List<GoalHistory> goalHistories = new List<GoalHistory>();
            List<SummaryItemGoalV2> result = new List<SummaryItemGoalV2>();
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data = new Dictionary<int, List<OverUnderScoreTimesV2Item>>();
            Dictionary<int, List<OverUnderScoreTimesV2Item>> dataV2 = new Dictionary<int, List<OverUnderScoreTimesV2Item>>();
            if (!DataStore.GoalHistories.ContainsKey(_matchId)) return result;
            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId)) return result;
            if (!DataStore.OverUnderScoreTimesV2.ContainsKey(_matchId)) return result;
            goalHistories = DataStore.GoalHistories[_matchId];
            data = DataStore.OverUnderScoreTimesV3[_matchId];
            dataV2 = DataStore.OverUnderScoreTimesV2[_matchId];
            List<int> hdps = data.Keys.OrderByDescending(item => item).ToList();

            for (int i = 0; i < goalHistories.Count; i++)
            {
                SummaryItemGoalV2 summaryItemGoalV2 = new SummaryItemGoalV2();
                int minute = (goalHistories[i].LivePeriod - 1) * 45 + (int)goalHistories[i].TimeSpanFromStart.TotalMinutes;
                if (minute >= 88) continue;
                if (minute < 0) continue;
                for (int j = 0; j < hdps.Count; j++)
                {
                    if (data[hdps[j]][minute].Over != 0)
                    {
                        int goalHdp = 0;
                        if (data[hdps[j]][minute + 1].Over != 0
                            && data[hdps[j]][minute + 2].Over != 0)
                        {
                            goalHdp = hdps[j];
                        }
                        else if (hdps.Count > j + 1
                            && data.ContainsKey(hdps[j+1])
                            && data[hdps[j + 1]][minute + 1].Over != 0
                            && data[hdps[j + 1]][minute + 2].Over != 0)
                        {
                            goalHdp = hdps[j + 1];
                        }
                        else
                        {
                            goalHdp = hdps[j];
                        }


                        result.Add(new SummaryItemGoalV2() {
                            GoalHdp = goalHdp + (i) * 100,
                            HdpWithoutGoal = hdps[j],
                            MaxHdpBeforeNextGoal = FindMaxHdpBeforeGoal(i, i+1, goalHistories, dataV2),
                            NewMaxHdp = hdps[j] + (i + 1) * 100,
                            NewMaxRealHdp = FindMaxHdpAtMinute(data, minute + 1) + (i + 1) * 100,
                            NewMinRealHdp = FindMinHdpAtMinute(data, minute + 1) + (i + 1) * 100,
                        });

                        break;
                    }
                }
            
            }

            return result;


        }

        // added goal
        private int FindMaxHdpBeforeGoal(int fromGoalTh, int toGoalTh,List<GoalHistory> goalHistories, Dictionary<int, List<OverUnderScoreTimesV2Item>> dataV2)
        {
            int fromMinute = (goalHistories[fromGoalTh].LivePeriod - 1) * 45 + (int)goalHistories[fromGoalTh].TimeSpanFromStart.TotalMinutes;
            int toMinute = goalHistories.Count > toGoalTh ?
                (goalHistories[toGoalTh].LivePeriod - 1) * 45 + (int)goalHistories[toGoalTh].TimeSpanFromStart.TotalMinutes :
                90;
            toMinute = toMinute > 90 ? 90 : toMinute;
            fromMinute = fromMinute > 90 ? 90 : fromMinute;
            List<int> keys = dataV2.Keys.OrderByDescending(item => item).ToList();
            for (int i = toMinute ; i > fromMinute ; i--)
            {
                for (int j = 0; j < keys.Count; j++)
                {
                    if (dataV2[keys[j]][i].Over != 0)
                    {
                        return keys[j];
                    }
                }
            }

            return 0;
        }

        private int FindMaxHdpAtMinute(Dictionary<int, List<OverUnderScoreTimesV2Item>> data, int minute)
        {
            List<int> keys = data.Keys.OrderByDescending(item => item).ToList();
            for (int i=0; i<keys.Count; i++)
            {
                if (data[keys[i]].Count > minute &&
                    data[keys[i]][minute].Over != 0)
                {
                    return keys[i];
                }
            }
            return 0;
        }

        private int FindMinHdpAtMinute(Dictionary<int, List<OverUnderScoreTimesV2Item>> data, int minute)
        {
            List<int> keys = data.Keys.OrderBy(item => item).ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                if (data[keys[i]].Count > minute &&
                    data[keys[i]][minute].Over != 0)
                {
                    return keys[i];
                }
            }
            return 0;
        }

        private delegate void dlgLoadDgvSummaryTable();
        private void LoadDgvSummaryTable()
        {
            if (this.dgvSummaryTable.InvokeRequired)
            {
                this.Invoke(new dlgLoadDgvSummaryTable(LoadDgvSummaryTable));
            }
            else if (DataStore.Excel4th.ContainsKey(_matchId))
            {
                this.dgvSummaryTable.Rows.Clear();
                this.dgvSummaryTable.ColumnCount = 10;
                for (int i = 0; i < this.dgvSummaryTable.ColumnCount; i++)
                {
                    this.dgvSummaryTable.Columns[i].Width = 25;
                }
                this.dgvSummaryTable.Columns[0].Width = 28;
                Dictionary<int, OverUnderSummary> summaryData = DataStore.Excel4th[_matchId];
                List<int> keys = summaryData.Keys.OrderByDescending(item => item).ToList();

                for (int i = 0; i < keys.Count; i++)
                {
                    OverUnderSummary item = summaryData[keys[i]];

                    List<string> row = new List<string>();
                    for (int j = 0; j < this.dgvSummaryTable.ColumnCount; j++)
                    {
                        row.Add("");
                    }
                    row[0] = keys[i].ToString();
                    row[1] = item.VeryFirstPrice.Over == 0 ? "" : item.VeryFirstPrice.Over.ToString();
                    row[2] = item.At90BeforeLive.Over.ToString();
                    row[3] = item.BeforeLivePrice.Over.ToString();
                    row[4] = item.FirstPriceInLive.Over.ToString();
                    row[5] = "";
                    row[6] = item.VeryFirstPrice.Under == 0 ? "" : item.VeryFirstPrice.Under.ToString();
                    row[7] = item.At90BeforeLive.Under.ToString();
                    row[8] = item.BeforeLivePrice.Under.ToString();
                    row[9] = item.FirstPriceInLive.Under.ToString();

                    this.dgvSummaryTable.Rows.Add(row.ToArray());

                }
                SetStyleSummaryTable();
            }

        }

        private void SetStyleSummaryTable()
        {
            for (int i = 0; i < dgvSummaryTable.RowCount; i++)
            {
                for (int j = 1; j < dgvSummaryTable.ColumnCount; j++)
                {
                    if (this.dgvSummaryTable.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvSummaryTable.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvSummaryTable.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvSummaryTable.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvSummaryTable.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvSummaryTable.Rows[i].Cells[j].Style.Font != null && this.dgvSummaryTable.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Bold)
                                    | (this.dgvSummaryTable.Rows[i].Cells[j].Style.Font != null && this.dgvSummaryTable.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        else
                        {
                            this.dgvSummaryTable.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvSummaryTable.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvSummaryTable.DefaultCellStyle.Font, FontStyle.Regular
                                    | (this.dgvSummaryTable.Rows[i].Cells[j].Style.Font != null && this.dgvSummaryTable.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Regular)
                                    | (this.dgvSummaryTable.Rows[i].Cells[j].Style.Font != null && this.dgvSummaryTable.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Regular)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        this.dgvSummaryTable.Rows[i].Cells[j].Value = this.dgvSummaryTable.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }

                    if (j == 1 || j == 6)
                    {
                        this.dgvSummaryTable.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.GreenYellow;
                    }
                    else if (j == 2 || j == 7)
                    {
                        this.dgvSummaryTable.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.SkyBlue;
                    }
                    else if (j == 3 || j == 8)
                    {
                        this.dgvSummaryTable.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.LightGreen;
                    }
                    else if (j == 4 || j == 9)
                    {
                        this.dgvSummaryTable.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.LightGray;
                    }

                }
            }
        }

        private delegate void dlgLoadOverUnderScoreRemainingTimeFhV2();

        private void LoadOverUnderScoreRemainingTimeFhV2()
        {
            return;
            //if (this.dgvOuScoreRemainingTimeFh.InvokeRequired)
            //{
            //    this.Invoke(new dlgLoadOverUnderScoreRemainingTimeFhV2(LoadOverUnderScoreRemainingTimeFhV2));
            //}
            //else
            //{
            //    this.dgvOuScoreRemainingTimeFh.Rows.Clear();
            //    List<GoalHistory> goalHistories = new List<GoalHistory>();
            //    DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories);
            //    if (goalHistories == null) goalHistories = new List<GoalHistory>();
            //    goalHistories = goalHistories.Where(item => item.LivePeriod == 1).ToList();

            //    Dictionary<int, List<OverUnderScoreTimesV2Item>> data;
            //    Dictionary<int, List<int>> dataGrid = new Dictionary<int, List<int>>();
            //    Dictionary<int, List<int>> dataGridCountMinus = new Dictionary<int, List<int>>();

            //    if (!DataStore.OverUnderScoreTimesFirstHalfV2.TryGetValue(_matchId, out data)) return;
            //    List<int> keys = data.Keys.OrderByDescending(item => item).ToList();

            //    List<int> goalMinute = new List<int>();
            //    for (int i = 0; i < goalHistories.Count(); i++)
            //    {
            //        int minute = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes + (goalHistories[i].LivePeriod - 1) * 45;
            //        goalMinute.Add(minute);
            //    }

            //    goalMinute.Add(90);

            //    for (int i = 0; i < goalMinute.Count; i++)
            //    {
            //        int toMinute = goalMinute[i];
            //        int fromMinute = 0;
            //        if (i > 0) fromMinute = (int)goalHistories[i - 1].TimeSpanFromStart.TotalMinutes + (goalHistories[i - 1].LivePeriod - 1) * 45;
            //        for (int j = 0; j < keys.Count; j++)
            //        {
            //            List<int> row = data[keys[j]]
            //                .Skip(fromMinute + 1)
            //                .Take(toMinute - fromMinute)
            //                .Select(item => item.Under)
            //                .ToList();

            //            if (!dataGrid.ContainsKey(keys[j]))
            //            {
            //                dataGrid.Add(keys[j], new List<int>());
            //                dataGridCountMinus.Add(keys[j], new List<int>());
            //            }
            //            dataGrid[keys[j]].Add(row.Count(item => item != 0));
            //            dataGridCountMinus[keys[j]].Add(row.Count(item => item < 0));
            //        }
            //    }

            //    this.dgvOuScoreRemainingTimeFh.ColumnCount = goalHistories.Count + 3;
            //    for (int i = 0; i < this.dgvOuScoreRemainingTimeFh.ColumnCount; i++)
            //    {
            //        this.dgvOuScoreRemainingTimeFh.Columns[i].Width = 40;
            //    }
            //    this.dgvOuScoreRemainingTimeFh.Columns[0].Width = 30;
            //    this.dgvOuScoreRemainingTimeFh.Columns[this.dgvOuScoreRemainingTimeFh.ColumnCount - 1].Width = 20;

            //    ArrayList firstRow = new ArrayList();
            //    for (int i = 0; i < this.dgvOuScoreRemainingTimeFh.ColumnCount; i++)
            //    {
            //        firstRow.Add("");
            //    }
            //    firstRow[0] = keys.Count;
            //    this.dgvOuScoreRemainingTimeFh.Rows.Add(firstRow.ToArray());
            //    List<int> sumCol = new List<int>();
            //    for (int i = 0; i < this.dgvOuScoreRemainingTimeFh.ColumnCount; i++)
            //    {
            //        sumCol.Add(0);
            //    }
            //    for (int i = 0; i < keys.Count; i++)
            //    {
            //        ArrayList row = new ArrayList() { keys[i] };
            //        int sumRow = 0;
            //        for (int j = 0; j < dataGrid[keys[i]].Count; j++)
            //        {
            //            if (dataGrid[keys[i]][j] + dataGridCountMinus[keys[i]][j] > 0)
            //            {
            //                string item = dataGrid[keys[i]][j] + " | " + dataGridCountMinus[keys[i]][j];
            //                row.Add(item);
            //                sumRow += dataGrid[keys[i]][j];
            //                sumCol[j] += dataGrid[keys[i]][j];
            //            }
            //            else
            //            {
            //                row.Add("");
            //            }

            //        }
            //        row.Add(sumRow);
            //        this.dgvOuScoreRemainingTimeFh.Rows.Add(row.ToArray());
            //    }

            //    for (int i = 0; i < this.dgvOuScoreRemainingTimeFh.Rows.Count; i++)
            //    {
            //        for (int j = 1; j < this.dgvOuScoreRemainingTimeFh.ColumnCount - 1; j++)
            //        {
            //            string cellValue = this.dgvOuScoreRemainingTimeFh.Rows[i].Cells[j].Value.ToString();
            //            string numValue = "";
            //            if (cellValue.Length > 0)
            //            {
            //                numValue = cellValue.Split('|')[0].Replace(" ", "");
            //                int number = int.Parse(numValue);
            //                if (number >= 18)
            //                {
            //                    this.dgvOuScoreRemainingTimeFh.Rows[i].Cells[j].Style.BackColor = Color.Red;
            //                }
            //            }
            //        }
            //        this.dgvOuScoreRemainingTimeFh.Rows[i].Cells[this.dgvOuScoreRemainingTimeFh.ColumnCount - 1].Style.BackColor = Color.LightGray;
            //    }
            //    if (this.dgvOuScoreRemainingTimeFh.Rows.Count >= 4)
            //    {
            //        for (int j = 1; j < this.dgvOuScoreRemainingTimeFh.ColumnCount - 1; j++)
            //        {
            //            for (int i = 0; i < this.dgvOuScoreRemainingTimeFh.Rows.Count - 4; i++)
            //            {
            //                int a = 0;
            //                int b = 0;
            //                int c = 0;
            //                int d = 0;

            //                if (this.dgvOuScoreRemainingTimeFh.Rows[i].Cells[j].Value.ToString().Length > 0)
            //                {
            //                    a = int.Parse(this.dgvOuScoreRemainingTimeFh.Rows[i].Cells[j].Value.ToString().Split('|')[0].Replace(" ", ""));
            //                }

            //                if (this.dgvOuScoreRemainingTimeFh.Rows[i + 1].Cells[j].Value.ToString().Length > 0)
            //                {
            //                    b = int.Parse(this.dgvOuScoreRemainingTimeFh.Rows[i + 1].Cells[j].Value.ToString().Split('|')[0].Replace(" ", ""));
            //                }

            //                if (this.dgvOuScoreRemainingTimeFh.Rows[i + 2].Cells[j].Value.ToString().Length > 0)
            //                {
            //                    c = int.Parse(this.dgvOuScoreRemainingTimeFh.Rows[i + 2].Cells[j].Value.ToString().Split('|')[0].Replace(" ", ""));
            //                }

            //                if (this.dgvOuScoreRemainingTimeFh.Rows[i + 3].Cells[j].Value.ToString().Length > 0)
            //                {
            //                    d = int.Parse(this.dgvOuScoreRemainingTimeFh.Rows[i + 3].Cells[j].Value.ToString().Split('|')[0].Replace(" ", ""));
            //                }

            //                if (a * b * c * d != 0
            //                    && a <= b
            //                    && b <= c
            //                    && c <= d)
            //                {
            //                    this.dgvOuScoreRemainingTimeFh.Rows[i].Cells[j].Style.BackColor =
            //                        this.dgvOuScoreRemainingTimeFh.Rows[i].Cells[j].Style.BackColor == Color.Red ?
            //                        Color.Orange : Color.Yellow;

            //                    this.dgvOuScoreRemainingTimeFh.Rows[i + 1].Cells[j].Style.BackColor =
            //                        this.dgvOuScoreRemainingTimeFh.Rows[i + 1].Cells[j].Style.BackColor == Color.Red ?
            //                        Color.Orange : Color.Yellow;

            //                    this.dgvOuScoreRemainingTimeFh.Rows[i + 2].Cells[j].Style.BackColor =
            //                        this.dgvOuScoreRemainingTimeFh.Rows[i + 2].Cells[j].Style.BackColor == Color.Red ?
            //                        Color.Orange : Color.Yellow;

            //                    this.dgvOuScoreRemainingTimeFh.Rows[i + 3].Cells[j].Style.BackColor =
            //                        this.dgvOuScoreRemainingTimeFh.Rows[i + 3].Cells[j].Style.BackColor == Color.Red ?
            //                        Color.Orange : Color.Yellow;
            //                }
            //            }

            //        }
            //    }

            //    ArrayList rowSumBottom = new ArrayList() { "" };
            //    for (int i = 0; i < sumCol.Count; i++)
            //    {
            //        if (sumCol[i] > 0)
            //        {
            //            rowSumBottom.Add(sumCol[i]);
            //        }
            //        else
            //        {
            //            rowSumBottom.Add("");
            //        }

            //    }
            //    this.dgvOuScoreRemainingTimeFh.Rows.Add(rowSumBottom.ToArray());

            //}
        }

        private delegate void dlgLoadOverUnderScoreRemainingTimeV2();

        private void LoadOverUnderScoreRemainingTimeV2()
        {
            if (this.dgvOuScoreRemainingTime.InvokeRequired)
            {
                this.Invoke(new dlgLoadOverUnderScoreRemainingTimeV2(LoadOverUnderScoreRemainingTimeV2));
            }
            else
            {
                this.dgvOuScoreRemainingTime.Rows.Clear();
                List<GoalHistory> goalHistories = new List<GoalHistory>();
                DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories);
                if (goalHistories == null) goalHistories = new List<GoalHistory>();

                Dictionary<int, List<OverUnderScoreTimesV2Item>> data;
                Dictionary<int, List<int>> dataGrid = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> dataGridCountMinus = new Dictionary<int, List<int>>();

                if (!DataStore.OverUnderScoreTimesV2.TryGetValue(_matchId, out data)) return;
                List<int> keys = data.Keys.OrderByDescending(item => item).ToList();

                List<int> goalMinute = new List<int>();
                for (int i = 0; i < goalHistories.Count(); i++)
                {
                    int minute = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes + (goalHistories[i].LivePeriod - 1) * 45;
                    goalMinute.Add(minute);
                }

                goalMinute.Add(90);

                for (int i = 0; i < goalMinute.Count; i++)
                {
                    int toMinute = goalMinute[i];
                    int fromMinute = 0;
                    if (i > 0) fromMinute = (int)goalHistories[i - 1].TimeSpanFromStart.TotalMinutes + (goalHistories[i - 1].LivePeriod - 1) * 45;
                    for (int j = 0; j < keys.Count; j++)
                    {
                        List<int> row = data[keys[j]]
                            .Skip(fromMinute + 1)
                            .Take(toMinute - fromMinute)
                            .Select(item => item.Under)
                            .ToList();

                        if (!dataGrid.ContainsKey(keys[j]))
                        {
                            dataGrid.Add(keys[j], new List<int>());
                            dataGridCountMinus.Add(keys[j], new List<int>());
                        }
                        dataGrid[keys[j]].Add(row.Count(item => item != 0));
                        dataGridCountMinus[keys[j]].Add(row.Count(item => item < 0));
                    }
                }

                this.dgvOuScoreRemainingTime.ColumnCount = goalHistories.Count + 3;
                for (int i = 0; i < this.dgvOuScoreRemainingTime.ColumnCount; i++)
                {
                    this.dgvOuScoreRemainingTime.Columns[i].Width = 40;
                }
                this.dgvOuScoreRemainingTime.Columns[0].Width = 30;
                this.dgvOuScoreRemainingTime.Columns[this.dgvOuScoreRemainingTime.ColumnCount - 1].Width = 20;

                ArrayList firstRow = new ArrayList();
                for (int i=0; i< this.dgvOuScoreRemainingTime.ColumnCount; i++)
                {
                    firstRow.Add("");
                }
                firstRow[0] = keys.Count;
                this.dgvOuScoreRemainingTime.Rows.Add(firstRow.ToArray());
                List<int> sumCol = new List<int>();
                for (int i=0; i< this.dgvOuScoreRemainingTime.ColumnCount; i++)
                {
                    sumCol.Add(0);
                }    
                for (int i = 0; i < keys.Count; i++)
                {
                    ArrayList row = new ArrayList() { keys[i] };
                    int sumRow = 0;
                    for (int j = 0; j < dataGrid[keys[i]].Count; j++)
                    {
                        if (dataGrid[keys[i]][j] + dataGridCountMinus[keys[i]][j] > 0)
                        {
                            string item = dataGrid[keys[i]][j] + " | " + dataGridCountMinus[keys[i]][j];
                            row.Add(item);
                            sumRow += dataGrid[keys[i]][j];
                            sumCol[j] += dataGrid[keys[i]][j];
                        }
                        else
                        {
                            row.Add("");
                        }

                    }
                    row.Add(sumRow);
                    this.dgvOuScoreRemainingTime.Rows.Add(row.ToArray());
                }

                for (int i=0; i<this.dgvOuScoreRemainingTime.Rows.Count; i++)
                {
                    for (int j = 1; j < this.dgvOuScoreRemainingTime.ColumnCount - 1; j++)
                    {
                        string cellValue = this.dgvOuScoreRemainingTime.Rows[i].Cells[j].Value.ToString();
                        string numValue = "";
                        if (cellValue.Length > 0)
                        {
                            numValue = cellValue.Split('|')[0].Replace(" ", "");
                            int number = int.Parse(numValue);
                            if (number >= 18)
                            {
                                this.dgvOuScoreRemainingTime.Rows[i].Cells[j].Style.BackColor = Color.Red;
                            }
                        }
                    }
                    this.dgvOuScoreRemainingTime.Rows[i].Cells[this.dgvOuScoreRemainingTime.ColumnCount - 1].Style.BackColor = Color.LightGray;
                }
                if (this.dgvOuScoreRemainingTime.Rows.Count >= 4)
                {
                    for (int j = 1; j < this.dgvOuScoreRemainingTime.ColumnCount - 1; j++)
                    {
                        for (int i = 0; i < this.dgvOuScoreRemainingTime.Rows.Count - 4; i++)
                        {
                            int a = 0;
                            int b = 0;
                            int c = 0;
                            int d = 0;

                            if (this.dgvOuScoreRemainingTime.Rows[i].Cells[j].Value.ToString().Length > 0)
                            {
                                a = int.Parse(this.dgvOuScoreRemainingTime.Rows[i].Cells[j].Value.ToString().Split('|')[0].Replace(" ", ""));
                            }

                            if (this.dgvOuScoreRemainingTime.Rows[i + 1].Cells[j].Value.ToString().Length > 0)
                            {
                                b = int.Parse(this.dgvOuScoreRemainingTime.Rows[i + 1].Cells[j].Value.ToString().Split('|')[0].Replace(" ", ""));
                            }

                            if (this.dgvOuScoreRemainingTime.Rows[i + 2].Cells[j].Value.ToString().Length > 0)
                            {
                                c = int.Parse(this.dgvOuScoreRemainingTime.Rows[i + 2].Cells[j].Value.ToString().Split('|')[0].Replace(" ", ""));
                            }

                            if (this.dgvOuScoreRemainingTime.Rows[i + 3].Cells[j].Value.ToString().Length > 0)
                            {
                                d = int.Parse(this.dgvOuScoreRemainingTime.Rows[i + 3].Cells[j].Value.ToString().Split('|')[0].Replace(" ", ""));
                            }

                            if (a*b*c*d != 0
                                && a <= b
                                && b <= c
                                && c <= d)
                            {
                                this.dgvOuScoreRemainingTime.Rows[i].Cells[j].Style.BackColor =
                                    this.dgvOuScoreRemainingTime.Rows[i].Cells[j].Style.BackColor == Color.Red ?
                                    Color.Orange : Color.Yellow;

                                this.dgvOuScoreRemainingTime.Rows[i + 1].Cells[j].Style.BackColor =
                                    this.dgvOuScoreRemainingTime.Rows[i + 1].Cells[j].Style.BackColor == Color.Red ?
                                    Color.Orange : Color.Yellow;

                                this.dgvOuScoreRemainingTime.Rows[i + 2].Cells[j].Style.BackColor =
                                    this.dgvOuScoreRemainingTime.Rows[i + 2].Cells[j].Style.BackColor == Color.Red ?
                                    Color.Orange : Color.Yellow;

                                this.dgvOuScoreRemainingTime.Rows[i + 3].Cells[j].Style.BackColor =
                                    this.dgvOuScoreRemainingTime.Rows[i + 3].Cells[j].Style.BackColor == Color.Red ?
                                    Color.Orange : Color.Yellow;
                            }
                        }

                    }
                }

                ArrayList rowSumBottom = new ArrayList() { "" };
                for (int i=0; i< sumCol.Count; i++)
                {
                    if (sumCol[i] > 0)
                    {
                        rowSumBottom.Add(sumCol[i]);
                    }
                    else
                    {
                        rowSumBottom.Add("");
                    }
                    
                }
                this.dgvOuScoreRemainingTime.Rows.Add(rowSumBottom.ToArray());

            }
        }

        private delegate void dlgLoadMainGrid();
        private void LoadMainGrid()
        {

            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgLoadMainGrid(LoadMainGrid));
            }
            else
            {
                if (!DataStore.Matchs.ContainsKey(_matchId)) return;
                Match match = DataStore.Matchs[_matchId];
                this.Text = match.Home + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")"
                    + " | " + Common.Functions.GetOverUnderMoneyLinesString(_matchId)
                    + " | " + match.League
                    + " | SHEET TABLE";
                int columnCount = Constants.DefaultMinutePerSet * Constants.OverUnderScoreTime_ColumnPerMinute + 5;
                this.dataGridView.Rows.Clear();
                this.dgvSubtractFt45.Rows.Clear();
                this.dgvSubtractFt90.Rows.Clear();

                this.dataGridView.ColumnCount = columnCount;
                this.dgvSubtractFt45.ColumnCount = columnCount;
                this.dgvSubtractFt90.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    this.dataGridView.Columns[i].Width = Common.Functions.ColWidthStyle();
                    this.dgvSubtractFt45.Columns[i].Width = Common.Functions.ColWidthStyle();
                    this.dgvSubtractFt90.Columns[i].Width = Common.Functions.ColWidthStyle();
                }
                this.dataGridView.Columns[0].Width = Common.Functions.ColWidthStyle() + 10;
                this.dgvSubtractFt45.Columns[0].Width = Common.Functions.ColWidthStyle() + 10;
                this.dgvSubtractFt90.Columns[0].Width = Common.Functions.ColWidthStyle() + 10;

                ArrayList header = new ArrayList() { "", "_" };
                for (int i = 1; i < columnCount - 3; i++)
                {
                    header.Add(((i - 1) / Constants.OverUnderScoreTime_ColumnPerMinute).ToString());
                }
                header.Add("ht-02");
                header.Add("end");

                this.dataGridView.Rows.Add(header.ToArray());

                LoadData();
                SetStyle(match);
                //LoadStatusLineMainGrid();
            }
        }

        private delegate void dlgLoadInsertFtH1Grid();
        private void LoadInsertFtH1Grid()
        {
            if (this.dgvInsertFtH1.InvokeRequired)
            {
                this.Invoke(new dlgLoadInsertFtH1Grid(LoadInsertFtH1Grid));
                return;
            }

            List<List<ProductStatusLog>> data = DataStore.ProductStatus[_matchId];
            List<int> indexOfProductHasInsert = new List<int>();
            List<int> hdpHasInsert = new List<int>();
            List<int> indexOfProductEndding = new List<int>();
            Dictionary<int, List<int>> dataMap = new Dictionary<int, List<int>>();
            Dictionary<int, List<ProductStatusLog>> dataMapItem = new Dictionary<int, List<ProductStatusLog>>();

            List<int> hdps = new List<int>();

            List<ProductIdLog> products = DataStore.ProductIdsOfMatch[_matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                .OrderBy(item => item.Minute)
                .ToList();

            List<ProductStatusLog> productStatusLogs = new List<ProductStatusLog>();
            for (int i = 1; i <= 99; i++)
            {
                productStatusLogs.AddRange(data[i].Where(item => item.Type == BetType.FullTimeOverUnder).ToList());
            }
            for (int i = 0; i < productStatusLogs.Count; i++)
            {
                int hdp = productStatusLogs[i].Hdp100;
                int indexOfProduct = 0;
                if (int.TryParse(Common.Functions.IndexOfProduct(products, productStatusLogs[i].OddsId.ToString()), out indexOfProduct))
                {

                    if (!indexOfProductHasInsert.Contains(indexOfProduct))
                    {
                        indexOfProductHasInsert.Add(indexOfProduct);
                    }

                    if (!hdpHasInsert.Any(item => item == hdp))
                    {
                        hdpHasInsert.Add(hdp);
                    }

                    if ((productStatusLogs[i].Hdp100 - productStatusLogs[i].TotalGoal * 100 == 50 || productStatusLogs[i].Hdp100 - productStatusLogs[i].TotalGoal * 100 == 75)
                        && (!indexOfProductEndding.Any(item => item == indexOfProduct))
                        && productStatusLogs[i].LivePeriod == 2
                        && productStatusLogs[i].Status != "delete")
                    {
                        indexOfProductEndding.Add(indexOfProduct);
                    }

                    if (!dataMap.ContainsKey(hdp))
                    {
                        dataMap.Add(hdp, new List<int>());
                        dataMapItem.Add(hdp, new List<ProductStatusLog>());
                    }
                    dataMap[hdp].Add(indexOfProduct);

                    if (!dataMapItem[hdp].Any(item => 
                        item.TotalGoal == productStatusLogs[i].TotalGoal
                        && (item.Hdp100 - item.TotalGoal * 100) == (productStatusLogs[i].Hdp100 - productStatusLogs[i].TotalGoal * 100)
                    ))
                    {
                        dataMapItem[hdp].Add(productStatusLogs[i]);
                    }


                }
            }




            //for (int i = 1; i <= 102; i++)
            //{
            //    int realMinute = 0;
            //    realMinute = i - 1;
                
            //    List<ProductStatusLog> dataAtMinute = data[realMinute].Where(item =>
            //        item.LivePeriod == livePeriod
            //        && item.Type == betType
            //    ).ToList();

            //    if (!dataAtMinute.Any(item => item.ServerAction == "insert"))
            //    {
            //        continue;
            //    }

            //    for (int j=0; j<dataAtMinute.Count; j++)
            //    {
            //        if (dataAtMinute[j].ServerAction == "insert")
            //        {
            //            int indexOfProduct = 0;
            //            if (int.TryParse(Common.Functions.IndexOfProduct(products, dataAtMinute[j].OddsId.ToString()), out indexOfProduct))
            //            {
            //                if (!indexOfProductHasInsert.Contains(indexOfProduct))
            //                {
            //                    indexOfProductHasInsert.Add(indexOfProduct);
            //                }

            //                if (!hdpHasInsert.Any(item => item == dataAtMinute[j].Hdp100))
            //                {
            //                    hdpHasInsert.Add(dataAtMinute[j].Hdp100);
            //                }

            //                if (!dataMap.ContainsKey(dataAtMinute[j].Hdp100))
            //                {
            //                    dataMap.Add(dataAtMinute[j].Hdp100, new List<int>());
            //                }
            //                dataMap[dataAtMinute[j].Hdp100].Add(indexOfProduct);

            //            }
                        
                        

            //        }
            //    }

            //}

            this.dgvInsertFtH1.Rows.Clear();
            indexOfProductHasInsert = indexOfProductHasInsert.OrderBy(item => item).ToList();
            int columnCount = indexOfProductHasInsert.Count + 1;
            dgvInsertFtH1.ColumnCount = columnCount;
            for (int i = 0; i < columnCount; i++)
            {
                this.dgvInsertFtH1.Columns[i].Width = 20;
            }
            this.dgvInsertFtH1.Columns[0].Width = 30;
            ArrayList header = new ArrayList() { "" };
            for (int i = 0; i < indexOfProductHasInsert.Count; i++)
            {
                header.Add(indexOfProductHasInsert[i]);
            }
            this.dgvInsertFtH1.Rows.Add(header.ToArray());
            hdpHasInsert = hdpHasInsert.OrderByDescending(item => item).ToList();
            for (int i = 0; i < hdpHasInsert.Count; i++)
            {
                ArrayList rowData = new ArrayList() { hdpHasInsert[i] };
                for (int j = 0; j < indexOfProductHasInsert.Count; j++)
                {
                    int count = dataMap[hdpHasInsert[i]].Count(item => item == indexOfProductHasInsert[j]);
                    if (count > 0)
                    {

                        int indexOfProduct = 0;
 
                        var test = dataMapItem[hdpHasInsert[i]].Count(item => int.TryParse(Common.Functions.IndexOfProduct(products, item.OddsId.ToString()), out indexOfProduct) && indexOfProduct == indexOfProductHasInsert[j]);
                        rowData.Add(test);

                    }
                    else
                    {
                        rowData.Add("");
                    }
                }
                this.dgvInsertFtH1.Rows.Add(rowData.ToArray());
                if(hdpHasInsert[i]%100 == 25)
                {
                    this.dgvInsertFtH1.Rows[i + 1].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
            if (this.dgvInsertFtH1.RowCount > 0)
            {
                for (int j = 0; j < this.dgvInsertFtH1.ColumnCount; j++)
                {
                    string hdpStr = this.dgvInsertFtH1.Rows[0].Cells[j].Value.ToString();
                    int hdp = 0;
                    if (int.TryParse(hdpStr, out hdp) && indexOfProductEndding.Contains(hdp))
                    {
                        this.dgvInsertFtH1.Rows[0].Cells[j].Style.BackColor = Color.Orange;
                    }
                }
            }

            try
            {
                List<Product> runningProduct = DataStore.Products.Values.Where(item => item.Bettype == BetType.FullTimeOverUnder && item.MatchId == _matchId).ToList();
                for (int i = 0; i < this.dgvInsertFtH1.RowCount; i++)
                {
                    int rowHdp;
                    if (int.TryParse(this.dgvInsertFtH1.Rows[i].Cells[0].Value.ToString(), out rowHdp))
                    {
                        if (runningProduct.Any(item => (item.Hdp1 * 100) == rowHdp))
                        {
                            this.dgvInsertFtH1.Rows[i].Cells[0].Style.BackColor = Color.Red;
                        }
                    }
                }
            }
            catch
            {

            }

            try
            {
                if (this.dgvInsertFtH1.RowCount > 0 && this.dgvInsertFtH1.Rows[0].Cells.Count > 0)
                {
                    List<Color> colors = new List<Color>()
                    {
                        Color.FromArgb(128,0,0),
                        Color.FromArgb(178,34,34),
                        Color.FromArgb(255,0,0),
                        Color.FromArgb(255,160,122),
                    };
                    List<Product> runningProduct = DataStore.Products.Values.Where(item => item.Bettype == BetType.FullTimeOverUnder && item.MatchId == _matchId).OrderByDescending(item => item.Hdp1 + item.Hdp2).ToList();
                    int indexOfProduct = 0;
                    for (int i = 0; i < runningProduct.Count; i++)
                    {
                        if (int.TryParse(Common.Functions.IndexOfProduct(products, runningProduct[i].OddsId.ToString()), out indexOfProduct))
                        {
                            for (int j=1; j< this.dgvInsertFtH1.Rows[0].Cells.Count; j++)
                            {
                                if (this.dgvInsertFtH1.Rows[0].Cells[j].Value.ToString() == indexOfProduct.ToString())
                                {
                                    this.dgvInsertFtH1.Rows[0].Cells[j].Style.BackColor = colors[i%4];
                                    break;
                                }
                            }
                        }
                    }
                }


            }
            catch
            {

            }

            try
            {
                if (DataStore.GoalHistories.ContainsKey(_matchId) && DataStore.GoalHistories[_matchId].Count > 0)
                {
                    GoalHistory goalHistory = DataStore.GoalHistories[_matchId].OrderBy(item => item.TimeSpanFromStart).FirstOrDefault();
                    for (int i = 0; i < this.dgvInsertFtH1.RowCount; i++)
                    {
                        for (int j = 0; j < this.dgvInsertFtH1.ColumnCount; j++)
                        {
                            if (this.dgvInsertFtH1.Rows[i].Cells[j].Value.ToString() == "x")
                            {
                                int hdp = int.Parse(this.dgvInsertFtH1.Rows[i].Cells[0].Value.ToString());
                                string productIndex = this.dgvInsertFtH1.Rows[0].Cells[j].Value.ToString();
                                if (goalHistory.ProductStatusLogs.Any(item => Common.Functions.IndexOfProduct(products, item.OddsId.ToString()) == productIndex && item.Hdp100 == hdp))
                                {
                                    this.dgvInsertFtH1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }




        }

        private delegate void dlgLoadOverUnderScoreTimesFirstHalfGrid();
        private void LoadOverUnderScoreTimesFirstHalfGrid()
        {

            if (this.dgvFirstHalf.InvokeRequired)
            {
                this.Invoke(new dlgLoadOverUnderScoreTimesFirstHalfGrid(LoadOverUnderScoreTimesFirstHalfGrid));
            }
            else
            {
                if (!DataStore.Matchs.ContainsKey(_matchId)) return;

                Dictionary<int, List<string>> data;
                if (!DataStore.OverUnderScoreTimesFirstHalf.TryGetValue(_matchId, out data))
                {
                    return;
                }

                int columnCount = Constants.DefaultMinutePerSet * Constants.OverUnderScoreTime_ColumnPerMinute + 3;
                this.dgvFirstHalf.Rows.Clear();
                this.dgvSubtractFh.Rows.Clear();

                this.dgvFirstHalf.ColumnCount = columnCount;
                this.dgvSubtractFh.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    this.dgvFirstHalf.Columns[i].Width = Common.Functions.ColWidthStyle();
                    this.dgvSubtractFh.Columns[i].Width = Common.Functions.ColWidthStyle();
                }
                this.dgvFirstHalf.Columns[0].Width = Common.Functions.ColWidthStyle()+10;
                this.dgvSubtractFh.Columns[0].Width = Common.Functions.ColWidthStyle()+10;

                ArrayList header = new ArrayList() { "", "_" };
                for (int i = 1; i < columnCount-1; i++)
                {
                    header.Add(((i - 1) / Constants.OverUnderScoreTime_ColumnPerMinute).ToString());
                }

                this.dgvFirstHalf.Rows.Add(header.ToArray());

                List<int> scores = data.Keys.OrderBy(item => item).ToList();
                List<ProductIdLog> productIdLogs = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder && item.Minute <= -3)
                    .ToList();

                for (int i = 0; i < productIdLogs.Count; i++)
                {
                    if (!scores.Contains(productIdLogs[i].Hdp))
                    {
                        scores.Add(productIdLogs[i].Hdp);
                    }
                }
                scores = scores.OrderBy(item => item).ToList();

                if (scores.Count == 0) return;


                List<List<string>> subtractHdpFt = GetSubtractHdpFh();
                List<string> row_1 = subtractHdpFt[0].Take(48).ToList();
                List<string> row_2 = subtractHdpFt[1].Take(48).ToList();

                this.dgvSubtractFh.Rows.Add(row_1.ToArray());
                this.dgvSubtractFh.Rows.Add(row_2.ToArray());
                this.dgvSubtractFh.Rows.Add(Common.Functions.SumOverUnderScoreTimesFreqFh45(_matchId).ToArray());

                for (int i = scores.Count - 1; i >= 0; i--)
                {
                    List<string> row = new List<string>();
                    row.Add(scores[i].ToString());


                    ProductIdLog productIdLog = productIdLogs.FirstOrDefault(item => item.Hdp == scores[i]);
                    if (productIdLog != null)
                    {
                        row.Add(productIdLog.Odds1a100.ToString());
                    }
                    else
                    {
                        row.Add("");
                    }

                    //working

                    if (data.ContainsKey(scores[i]))
                    {
                        row.AddRange(data[scores[i]].ToList());
                    }
                    else
                    {
                        for (int j = 0; j < this.dataGridView.ColumnCount - 1; j++)
                        {
                            row.Add("");
                        }
                    }


                    try
                    {
                        row.Add(data[scores[i]][7]);
                    }
                    catch
                    {
                        row.Add("");
                    }
                    this.dgvFirstHalf.Rows.Add(row.ToArray());
                }

                for (int i = 0; i < this.dgvFirstHalf.Rows.Count; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        this.dgvFirstHalf.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                    }
                }


                //for (int i = 1; i < dgvFirstHalf.RowCount; i++)
                //{
                //    for (int j = 1; j < dgvFirstHalf.ColumnCount; j++)
                //    {
                //        if (this.dgvFirstHalf.Rows[i].Cells[j].Value != null && this.dgvFirstHalf.Rows[i].Cells[j].Value.ToString().Length > 0)
                //        {
                //            int k = j + 1;
                //            string cellValue = this.dgvFirstHalf.Rows[i].Cells[j].Value.ToString();
                //            while (k < dgvFirstHalf.ColumnCount)
                //            {
                //                if (this.dgvFirstHalf.Rows[i].Cells[k].Value.ToString() == cellValue)
                //                {
                //                    this.dgvFirstHalf.Rows[i].Cells[k].Style.BackColor = System.Drawing.Color.LightGreen;
                //                    this.dgvFirstHalf.Rows[i].Cells[k - 1].Style.BackColor = System.Drawing.Color.LightGreen;
                //                }
                //                else
                //                {
                //                    break;
                //                }
                //                k++;
                //            }
                //        }

                //    }
                //}

                List<GoalHistory> goalHistories;
                if (DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories))
                {
                    int rowCount = this.dgvFirstHalf.Rows.Count;
                    for (int i = 0; i < goalHistories.Count; i++)
                    {
                        int column = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes + 1;
                        if (column > 45) column = 45;
                        for (int j = 0; j < rowCount; j++)
                        {
                            if (goalHistories[i].LivePeriod == 1)
                            {
                                this.dgvFirstHalf.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }

                for (int i = 1; i < dgvFirstHalf.RowCount; i++)
                {
                    for (int j = 1; j < dgvFirstHalf.ColumnCount; j++)
                    {
                        if (this.dgvFirstHalf.Rows[i].Cells[j].Value != null)
                        {
                            if (this.dgvFirstHalf.Rows[i].Cells[j].Value.ToString().Contains("-"))
                            {
                                this.dgvFirstHalf.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvFirstHalf.Rows[i].Cells[j].Style) { Font = new Font(this.dgvFirstHalf.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
                            }
                            else
                            {
                                this.dgvFirstHalf.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvFirstHalf.Rows[i].Cells[j].Style) { Font = new Font(this.dgvFirstHalf.DefaultCellStyle.Font, FontStyle.Regular), ForeColor = Color.Black };
                            }
                            this.dgvFirstHalf.Rows[i].Cells[j].Value = this.dgvFirstHalf.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                        }

                    }
                }

                for (int i = 0; i < dgvSubtractFh.RowCount; i++)
                {
                    for (int j = 1; j < dgvSubtractFh.ColumnCount; j++)
                    {
                        if (this.dgvSubtractFh.Rows[i].Cells[j].Value != null)
                        {

                            if (this.dgvSubtractFh.Rows[i].Cells[j].Value.ToString().Contains("-"))
                            {
                                this.dgvSubtractFh.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvSubtractFh.Rows[i].Cells[j].Style)
                                {
                                    Font = new Font(this.dgvSubtractFh.DefaultCellStyle.Font, FontStyle.Strikeout)
                                };
                            }
                            else
                            {
                                this.dgvSubtractFh.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvSubtractFh.Rows[i].Cells[j].Style)
                                {
                                    Font = new Font(this.dgvSubtractFh.DefaultCellStyle.Font, FontStyle.Regular)
                                };
                            }
                            this.dgvSubtractFh.Rows[i].Cells[j].Value = this.dgvSubtractFh.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                        }

                    }
                }

                List<int> maxHdps = Common.Functions.MaxHdpFhEveryMinute(_matchId);
                Dictionary<int, List<OverUnderScoreTimesV2Item>> overUnderScoreTimesV3 = null;
                if (DataStore.OverUnderScoreTimesFirstHalfV3.ContainsKey(_matchId))
                {
                    overUnderScoreTimesV3 = DataStore.OverUnderScoreTimesFirstHalfV3[_matchId];
                }


                for (int j = 1; j <= 45; j++)
                {
                    if (maxHdps[j] % 100 == 0 && maxHdps[j] > 0)
                    {
                        if (this.dgvSubtractFh.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFh.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                            this.dgvSubtractFh.Rows[0].Cells[j + 2].Style.BackColor = Color.LightPink;
                        if (this.dgvSubtractFh.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFh.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                            this.dgvSubtractFh.Rows[1].Cells[j + 2].Style.BackColor = Color.SkyBlue;
                    }
                    else if (maxHdps[j] % 100 == 25)
                    {
                        if (this.dgvSubtractFh.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFh.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                            this.dgvSubtractFh.Rows[0].Cells[j + 2].Style.BackColor = Color.LightYellow;
                        if (this.dgvSubtractFh.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFh.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                            this.dgvSubtractFh.Rows[1].Cells[j + 2].Style.BackColor = Color.LightPink;
                    }
                    else if (maxHdps[j] % 100 == 50 && maxHdps[j] > 50)
                    {
                        if (this.dgvSubtractFh.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFh.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                            this.dgvSubtractFh.Rows[0].Cells[j + 2].Style.BackColor = Color.LightGreen;
                        if (this.dgvSubtractFh.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFh.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                            this.dgvSubtractFh.Rows[1].Cells[j + 2].Style.BackColor = Color.LightYellow;
                    }
                    else if (maxHdps[j] % 100 == 75 && maxHdps[j] > 75)
                    {
                        if (this.dgvSubtractFh.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFh.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                            this.dgvSubtractFh.Rows[0].Cells[j + 2].Style.BackColor = Color.SkyBlue;
                        if (this.dgvSubtractFh.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFh.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                            this.dgvSubtractFh.Rows[1].Cells[j + 2].Style.BackColor = Color.LightGreen;
                    }
                    else if (maxHdps[j] == 75)
                    {
                        if (this.dgvSubtractFh.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFh.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                            this.dgvSubtractFh.Rows[0].Cells[j + 2].Style.BackColor = Color.Gray;
                        if (this.dgvSubtractFh.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFh.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                            this.dgvSubtractFh.Rows[1].Cells[j + 2].Style.BackColor = Color.Gray;
                    }

                    if (overUnderScoreTimesV3 != null)
                    {
                        if (overUnderScoreTimesV3.ContainsKey(maxHdps[j])
                            && overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 25))
                        {
                            int firstOver = overUnderScoreTimesV3[maxHdps[j]][j].Over;
                            int secondOver = overUnderScoreTimesV3[maxHdps[j] - 25][j].Over;
                            if (firstOver < 0 || secondOver < 0)
                            {
                                this.dgvSubtractFh.Rows[0].Cells[j + 2].Style = new DataGridViewCellStyle(this.dgvSubtractFh.Rows[0].Cells[j + 2].Style)
                                {
                                    Font = new Font(
                                        this.dgvSubtractFh.DefaultCellStyle.Font, FontStyle.Bold
                                        | (this.dgvSubtractFh.Rows[0].Cells[j + 2].Style.Font != null && this.dgvSubtractFh.Rows[0].Cells[j + 2].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
                                    ),
                                    ForeColor = Color.Black
                                };
                            }
                        }

                        if (overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 25)
                            && overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 50))
                        {
                            int firstOver = overUnderScoreTimesV3[maxHdps[j] - 25][j].Over;
                            int secondOver = overUnderScoreTimesV3[maxHdps[j] - 50][j].Over;
                            if (firstOver < 0 || secondOver < 0)
                            {
                                this.dgvSubtractFh.Rows[1].Cells[j + 2].Style = new DataGridViewCellStyle(this.dgvSubtractFh.Rows[1].Cells[j + 2].Style)
                                {
                                    Font = new Font(
                                        this.dgvSubtractFh.DefaultCellStyle.Font, FontStyle.Bold
                                        | (this.dgvSubtractFh.Rows[1].Cells[j + 2].Style.Font != null && this.dgvSubtractFh.Rows[1].Cells[j + 2].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
                                    ),
                                    ForeColor = Color.Black
                                };
                            }
                        }

                        if (overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 75)
                            && overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 50))
                        {
                            int firstOver = overUnderScoreTimesV3[maxHdps[j] - 75][j].Over;
                            int secondOver = overUnderScoreTimesV3[maxHdps[j] - 50][j].Over;
                            if (firstOver < 0 || secondOver < 0)
                            {
                                this.dgvSubtractFh.Rows[2].Cells[j + 2].Style = new DataGridViewCellStyle(this.dgvSubtractFh.Rows[2].Cells[j + 2].Style)
                                {
                                    Font = new Font(
                                        this.dgvSubtractFh.DefaultCellStyle.Font, FontStyle.Bold
                                        | (this.dgvSubtractFh.Rows[2].Cells[j + 2].Style.Font != null && this.dgvSubtractFh.Rows[2].Cells[j + 2].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
                                    ),
                                    ForeColor = Color.Black
                                };
                            }
                        }
                    }
                }

                Dictionary<int, List<string>> dataOver = DataStore.OverUnderScoreTimesFirstHalf[_matchId];
                Dictionary<int, List<string>> dataUnder = DataStore.UnderScoreTimesFirstHalf[_matchId];
                List<int> ouKeys = dataOver.Keys.ToList();

                for (int i = 1; i <= 45; i++)
                {
                    for (int j = 0; j < ouKeys.Count; j++)
                    {
                        int currentMinute = Common.Functions.OverUnderLineAtMinute(ouKeys[j], i, dataOver, dataUnder);
                        int previousMinute = Common.Functions.OverUnderLineAtMinute(ouKeys[j], i - 1, dataOver, dataUnder);
                        if (currentMinute != 0 && previousMinute != 0)
                        {
                            if (currentMinute > previousMinute)
                            {
                                this.dgvFirstHalf.Rows[0].Cells[i + 2].Style.BackColor = Color.Green;
                            }
                            else if (currentMinute < previousMinute)
                            {
                                this.dgvFirstHalf.Rows[0].Cells[i + 2].Style.BackColor = Color.SkyBlue;
                            }
                        }
                    }
                }


                //LoadStatusLineFirstHalfGrid();
                if (this._enableXtraView)
                {
                    LoadFastMoveColor(dgvFirstHalf);
                    LoadSlowMoveColor(dgvFirstHalf);
                    
                }

                if (this._enableXtraViewPriceUp)
                {
                    LoadFast2CellMove(dgvFirstHalf);
                }

                if (this._enableXtraViewPriceDown)
                {
                    LoadDownCellMove(dgvFirstHalf);
                }

                Match match = DataStore.Matchs[_matchId];

                BoldMaxValueWithinTenMinutes(dgvSubtractFh, (int)match.TimeSpanFromStart.TotalMinutes);
            }
        }


        private Dictionary<int, int> GetAvailableOpenUnderPrice()
        {
            Dictionary<int, int> openUnderPrice = new Dictionary<int, int>();
            if (DataStore.OverUnderScoreTimesV2.ContainsKey(_matchId))
            {
                List<int> scores = DataStore.OverUnderScoreTimesV2[_matchId].Keys.ToList();
                if (scores.Count > 0)
                {

                    foreach (int score in scores)
                    {
                        if (DataStore.OverUnderScoreTimesV2[_matchId][score][0].Under != 0)
                        {
                            openUnderPrice.Add(score, DataStore.OverUnderScoreTimesV2[_matchId][score][0].Under);
                        }
                    }
                }
            }

            return openUnderPrice;
        }

        private void ColorIuoo()
        {

            List<Color> colors = new List<Color>() { Color.DarkGray, Color.ForestGreen, Color.SkyBlue, Color.SteelBlue };
            int colorIndex = 0;

            Dictionary<int, int> openUnderPrice = GetAvailableOpenUnderPrice();
            foreach (KeyValuePair<int, int> item in openUnderPrice)
            {
                colorIndex++;
                if (colorIndex >= colors.Count) colorIndex = 0;

                for (int j = 0; j < this.dgvUnderPrice.RowCount; j++)
                {
                    string cell = this.dgvUnderPrice.Rows[j].Cells[0].Value.ToString();
                    if (cell.Length > 0 && int.Parse(cell) == item.Key)
                    {
                        this.dgvUnderPrice.Rows[j].Cells[1].Style.BackColor = colors[colorIndex];
                        break;
                    }
                }

                if (DataStore.OverUnderScoreTimesV2.ContainsKey(_matchId))
                {
                    List<int> keys = DataStore.OverUnderScoreTimesV2[_matchId].Keys.Where(p => p <= item.Key).ToList();
                    if (keys.Count > 0)
                    {
                        foreach (int key in keys)
                        {
                            List<OverUnderScoreTimesV2Item> row = DataStore.OverUnderScoreTimesV2[_matchId][key].ToList();
                            for (int i = 0; i < row.Count; i++)
                            {
                                if (Common.Functions.IuooCanEsc(item.Value, row[i].Over))
                                {
                                    int minute = (i > 45 ? i % 45 : i) + 1;

                                    for (int j = 0; j < this.dataGridView.RowCount; j++)
                                    {
                                        string cell = this.dataGridView.Rows[j].Cells[minute].Value.ToString();
                                        if (cell.Length > 0
                                            && Math.Abs(int.Parse(cell)) == Math.Abs(row[i].Over))
                                        {
                                            this.dataGridView.Rows[j].Cells[minute].Style.BackColor = colors[colorIndex];

                                            //break;
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }

                }
            }

        }




        private void SetStyle(Match match)
        {
            int columnCount = Constants.DefaultMinutePerSet * Constants.OverUnderScoreTime_ColumnPerMinute + 3;

            for (int i = 0; i < this.dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    this.dataGridView.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                }
            }

            List<GoalHistory> goalHistories;
            if (DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories))
            {
                int rowCount = this.dataGridView.Rows.Count;
                for (int i = 0; i < goalHistories.Count; i++)
                {
                    int column = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes + 1;
                    if (column > 45) column = 45;
                    for (int j = 0; j < rowCount; j++)
                    {
                        if (goalHistories[i].LivePeriod == 1)
                        {
                            this.dataGridView.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                        }
                        else if (goalHistories[i].LivePeriod == 2)
                        {
                            if (this.dataGridView.Rows[j].Cells[column].Style.BackColor == System.Drawing.Color.Red)
                            {
                                this.dataGridView.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Orange;
                            }
                            else
                            {
                                this.dataGridView.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Yellow;
                            }
                        }


                    }
                }
            }

            List<CardHistory> cardHistories;
            if (DataStore.CardHistories.TryGetValue(_matchId, out cardHistories))
            {
                int rowCount = this.dataGridView.Rows.Count;
                for (int i = 0; i < cardHistories.Count; i++)
                {
                    int column = (int)cardHistories[i].TimeSpanFromStart.TotalMinutes + 1;
                    if (column > 45) column = 45;
                    this.dataGridView.Rows[0].Cells[column].Style = new DataGridViewCellStyle(this.dataGridView.Rows[0].Cells[column].Style)
                    {
                        Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold | FontStyle.Italic),
                        ForeColor = Color.Blue
                    };
                }

            }


            //for (int i = 1; i < dataGridView.RowCount; i++)
            //{
            //    for (int j = 1; j < dataGridView.ColumnCount; j++)
            //    {
            //        if (this.dataGridView.Rows[i].Cells[j].Value != null && this.dataGridView.Rows[i].Cells[j].Value.ToString().Length > 0)
            //        {
            //            int k = j + 1;
            //            string cellValue = this.dataGridView.Rows[i].Cells[j].Value.ToString();
            //            while (k < dataGridView.ColumnCount)
            //            {
            //                if (this.dataGridView.Rows[i].Cells[k].Value.ToString() == cellValue)
            //                {
            //                    this.dataGridView.Rows[i].Cells[k].Style.BackColor = System.Drawing.Color.LightGreen;
            //                    this.dataGridView.Rows[i].Cells[k - 1].Style.BackColor = System.Drawing.Color.LightGreen;
            //                }
            //                else
            //                {
            //                    break;
            //                }
            //                k++;
            //            }
            //        }

            //    }
            //}

            Dictionary<int, List<string>> dataOver = DataStore.OverUnderScoreTimes[_matchId];
            Dictionary<int, List<string>> dataUnder = DataStore.UnderScoreTimes[_matchId];
            List<int> ouKeys = dataOver.Keys.ToList();
           
            for (int i = 1; i <= 45; i++)
            {
                for (int j = 0; j < ouKeys.Count; j++)
                {
                    int currentMinute = Common.Functions.OverUnderLineAtMinute(ouKeys[j], i, dataOver, dataUnder);
                    int previousMinute = Common.Functions.OverUnderLineAtMinute(ouKeys[j], i-1, dataOver, dataUnder);
                    if (currentMinute != 0 && previousMinute != 0)
                    {
                        if(currentMinute > previousMinute)
                        {
                            this.dataGridView.Rows[0].Cells[i + 2].Style.BackColor = Color.Green;
                        }
                        else if (currentMinute < previousMinute)
                        {
                            this.dataGridView.Rows[0].Cells[i + 2].Style.BackColor = Color.SkyBlue;
                        }
                    }
                }
            }




            for (int i = 1; i < dataGridView.RowCount; i++)
            {
                for (int j = 1; j < dataGridView.ColumnCount; j++)
                {
                    if (this.dataGridView.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dataGridView.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dataGridView.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells[j].Style) {
                                Font = new Font(
                                    this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dataGridView.Rows[i].Cells[j].Style.Font != null && this.dataGridView.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Bold)
                                    | (this.dataGridView.Rows[i].Cells[j].Style.Font != null && this.dataGridView.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Bold)
                                ), ForeColor = Color.Black
                            };
                        }
                        else
                        {
                            this.dataGridView.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells[j].Style) {
                                Font = new Font(
                                    this.dataGridView.DefaultCellStyle.Font, FontStyle.Regular
                                    | (this.dataGridView.Rows[i].Cells[j].Style.Font != null && this.dataGridView.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Regular)
                                    | (this.dataGridView.Rows[i].Cells[j].Style.Font != null && this.dataGridView.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Regular)
                                ), ForeColor = Color.Black
                            };
                        }
                        this.dataGridView.Rows[i].Cells[j].Value = this.dataGridView.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }

                }
            }

            List<int> maxHdps = Common.Functions.MaxHdpFtEveryMinute(_matchId);

            Dictionary<int, List<OverUnderScoreTimesV2Item>> overUnderScoreTimesV3 = null;
            if (DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId))
            {
                overUnderScoreTimesV3 = DataStore.OverUnderScoreTimesV3[_matchId];
            }


            for (int i = 0; i < this.dgvSubtractFt45.RowCount; i++)
            {
                for (int j = 1; j < dgvSubtractFt45.ColumnCount; j++)
                {
                    if (this.dgvSubtractFt45.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvSubtractFt45.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvSubtractFt45.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvSubtractFt45.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(this.dgvSubtractFt45.DefaultCellStyle.Font, FontStyle.Strikeout)
                            };
                        }
                        else
                        {
                            this.dgvSubtractFt45.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvSubtractFt45.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(this.dgvSubtractFt45.DefaultCellStyle.Font, FontStyle.Regular)
                            };
                        }
                        this.dgvSubtractFt45.Rows[i].Cells[j].Value = this.dgvSubtractFt45.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }
                }
            }

            for (int j = 1; j <= 45; j++ )
            {
                if (maxHdps[j] % 100 == 0)
                {
                    if (this.dgvSubtractFt45.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[0].Cells[j + 2].Style.BackColor = Color.LightPink;
                    if (this.dgvSubtractFt45.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[1].Cells[j + 2].Style.BackColor = Color.SkyBlue;
                    if (this.dgvSubtractFt45.Rows[2].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[2].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[2].Cells[j + 2].Style.BackColor = Color.LightGreen;
                }
                else if (maxHdps[j] % 100 == 25)
                {
                    if (this.dgvSubtractFt45.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[0].Cells[j + 2].Style.BackColor = Color.LightYellow;
                    if (this.dgvSubtractFt45.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[1].Cells[j + 2].Style.BackColor = Color.LightPink;
                    if (this.dgvSubtractFt45.Rows[2].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[2].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[2].Cells[j + 2].Style.BackColor = Color.SkyBlue;
                }
                else if (maxHdps[j] % 100 == 50 && maxHdps[j] > 50)
                {
                    if (this.dgvSubtractFt45.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[0].Cells[j + 2].Style.BackColor = Color.LightGreen;
                    if (this.dgvSubtractFt45.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[1].Cells[j + 2].Style.BackColor = Color.LightYellow;
                    if (this.dgvSubtractFt45.Rows[2].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[2].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[2].Cells[j + 2].Style.BackColor = Color.LightPink;
                }
                else if (maxHdps[j] % 100 == 75 && maxHdps[j] > 75)
                {
                    if (this.dgvSubtractFt45.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[0].Cells[j + 2].Style.BackColor = Color.SkyBlue;
                    if (this.dgvSubtractFt45.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[1].Cells[j + 2].Style.BackColor = Color.LightGreen;
                    if (this.dgvSubtractFt45.Rows[2].Cells[j + 2].Value != null && this.dgvSubtractFt45.Rows[2].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt45.Rows[2].Cells[j + 2].Style.BackColor = Color.LightYellow;
                }

                if (overUnderScoreTimesV3 != null)
                {
                    if (overUnderScoreTimesV3.ContainsKey(maxHdps[j])
                        && overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 25))
                    {
                        int firstOver = overUnderScoreTimesV3[maxHdps[j]][j].Over;
                        int secondOver = overUnderScoreTimesV3[maxHdps[j] - 25][j].Over;
                        if (firstOver < 0 || secondOver < 0)
                        {
                            this.dgvSubtractFt45.Rows[0].Cells[j + 2].Style = new DataGridViewCellStyle(this.dgvSubtractFt45.Rows[0].Cells[j + 2].Style)
                            {
                                Font = new Font(
                                    this.dgvSubtractFt45.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvSubtractFt45.Rows[0].Cells[j + 2].Style.Font != null && this.dgvSubtractFt45.Rows[0].Cells[j + 2].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                    }

                    if (overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 25)
                        && overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 50))
                    {
                        int firstOver = overUnderScoreTimesV3[maxHdps[j] - 25][j].Over;
                        int secondOver = overUnderScoreTimesV3[maxHdps[j] - 50][j].Over;
                        if (firstOver < 0 || secondOver < 0)
                        {
                            this.dgvSubtractFt45.Rows[1].Cells[j + 2].Style = new DataGridViewCellStyle(this.dgvSubtractFt45.Rows[1].Cells[j + 2].Style)
                            {
                                Font = new Font(
                                    this.dgvSubtractFt45.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvSubtractFt45.Rows[1].Cells[j + 2].Style.Font != null && this.dgvSubtractFt45.Rows[1].Cells[j + 2].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                    }

                    if (overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 75)
                        && overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 50))
                    {
                        int firstOver = overUnderScoreTimesV3[maxHdps[j] - 75][j].Over;
                        int secondOver = overUnderScoreTimesV3[maxHdps[j] - 50][j].Over;
                        if (firstOver < 0 || secondOver < 0)
                        {
                            this.dgvSubtractFt45.Rows[2].Cells[j + 2].Style = new DataGridViewCellStyle(this.dgvSubtractFt45.Rows[2].Cells[j + 2].Style)
                            {
                                Font = new Font(
                                    this.dgvSubtractFt45.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvSubtractFt45.Rows[2].Cells[j + 2].Style.Font != null && this.dgvSubtractFt45.Rows[2].Cells[j + 2].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                    }
                }


            }

            for (int i = 0; i < this.dgvSubtractFt90.RowCount; i++)
            {
                for (int j = 1; j < dgvSubtractFt90.ColumnCount; j++)
                {
                    if (this.dgvSubtractFt90.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvSubtractFt90.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvSubtractFt90.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvSubtractFt90.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(this.dgvSubtractFt90.DefaultCellStyle.Font, FontStyle.Strikeout)
                            };
                        }
                        else
                        {
                            this.dgvSubtractFt90.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvSubtractFt90.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(this.dgvSubtractFt90.DefaultCellStyle.Font, FontStyle.Regular)
                            };
                        }
                        this.dgvSubtractFt90.Rows[i].Cells[j].Value = this.dgvSubtractFt90.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }

                }
            }

            for (int j = 1; j <= 45; j++)
            {
                if (maxHdps[j + 45] % 100 == 0)
                {
                    if (this.dgvSubtractFt90.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[0].Cells[j + 2].Style.BackColor = Color.LightPink;
                    if (this.dgvSubtractFt90.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[1].Cells[j + 2].Style.BackColor = Color.SkyBlue;
                    if (this.dgvSubtractFt90.Rows[2].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[2].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[2].Cells[j + 2].Style.BackColor = Color.LightGreen;
                }
                else if (maxHdps[j + 45] % 100 == 25)
                {
                    if (this.dgvSubtractFt90.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[0].Cells[j + 2].Style.BackColor = Color.LightYellow;
                    if (this.dgvSubtractFt90.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[1].Cells[j + 2].Style.BackColor = Color.LightPink;
                    if (this.dgvSubtractFt90.Rows[2].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[2].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[2].Cells[j + 2].Style.BackColor = Color.SkyBlue;
                }
                else if (maxHdps[j + 45] % 100 == 50 && maxHdps[j + 45] > 50)
                {
                    if (this.dgvSubtractFt90.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[0].Cells[j + 2].Style.BackColor = Color.LightGreen;
                    if (this.dgvSubtractFt90.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[1].Cells[j + 2].Style.BackColor = Color.LightYellow;
                    if (this.dgvSubtractFt90.Rows[2].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[2].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[2].Cells[j + 2].Style.BackColor = Color.LightPink;
                }
                else if (maxHdps[j + 45] % 100 == 75 && maxHdps[j + 45] > 75)
                {
                    if (this.dgvSubtractFt90.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[0].Cells[j + 2].Style.BackColor = Color.SkyBlue;
                    if (this.dgvSubtractFt90.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[1].Cells[j + 2].Style.BackColor = Color.LightGreen;
                    if (this.dgvSubtractFt90.Rows[2].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[2].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[2].Cells[j + 2].Style.BackColor = Color.LightYellow;
                }
                else if (maxHdps[j + 45] == 75)
                {
                    if (this.dgvSubtractFt90.Rows[0].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[0].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[0].Cells[j + 2].Style.BackColor = Color.Gray;
                    if (this.dgvSubtractFt90.Rows[1].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[1].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[1].Cells[j + 2].Style.BackColor = Color.Gray;
                    if (this.dgvSubtractFt90.Rows[2].Cells[j + 2].Value != null && this.dgvSubtractFt90.Rows[2].Cells[j + 2].Value.ToString().Length > 0)
                        this.dgvSubtractFt90.Rows[2].Cells[j + 2].Style.BackColor = Color.Gray;
                }

                if (overUnderScoreTimesV3 != null)
                {
                    if (overUnderScoreTimesV3.ContainsKey(maxHdps[j + 45])
                        && overUnderScoreTimesV3.ContainsKey(maxHdps[j + 45] - 25))
                    {
                        int firstOver = overUnderScoreTimesV3[maxHdps[j + 45]][j+45].Over;
                        int secondOver = overUnderScoreTimesV3[maxHdps[j + 45] - 25][j + 45].Over;
                        if (firstOver < 0 || secondOver < 0)
                        {
                            this.dgvSubtractFt90.Rows[0].Cells[j + 2].Style = new DataGridViewCellStyle(this.dgvSubtractFt90.Rows[0].Cells[j + 2].Style)
                            {
                                Font = new Font(
                                    this.dgvSubtractFt90.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvSubtractFt90.Rows[0].Cells[j + 2].Style.Font != null && this.dgvSubtractFt90.Rows[0].Cells[j + 2].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                    }

                    if (overUnderScoreTimesV3.ContainsKey(maxHdps[j + 45] - 25)
                        && overUnderScoreTimesV3.ContainsKey(maxHdps[j + 45] - 50))
                    {
                        int firstOver = overUnderScoreTimesV3[maxHdps[j + 45] - 25][j + 45].Over;
                        int secondOver = overUnderScoreTimesV3[maxHdps[j + 45] - 50][j + 45].Over;
                        if (firstOver < 0 || secondOver < 0)
                        {
                            this.dgvSubtractFt90.Rows[1].Cells[j + 2].Style = new DataGridViewCellStyle(this.dgvSubtractFt90.Rows[1].Cells[j + 2].Style)
                            {
                                Font = new Font(
                                    this.dgvSubtractFt90.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvSubtractFt90.Rows[1].Cells[j + 2].Style.Font != null && this.dgvSubtractFt90.Rows[1].Cells[j + 2].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                    }

                    if (overUnderScoreTimesV3.ContainsKey(maxHdps[j + 45] - 75)
                        && overUnderScoreTimesV3.ContainsKey(maxHdps[j + 45] - 50))
                    {
                        int firstOver = overUnderScoreTimesV3[maxHdps[j + 45] - 75][j + 45].Over;
                        int secondOver = overUnderScoreTimesV3[maxHdps[j + 45] - 50][j + 45].Over;
                        if (firstOver < 0 || secondOver < 0)
                        {
                            this.dgvSubtractFt90.Rows[2].Cells[j + 2].Style = new DataGridViewCellStyle(this.dgvSubtractFt90.Rows[2].Cells[j + 2].Style)
                            {
                                Font = new Font(
                                    this.dgvSubtractFt90.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvSubtractFt90.Rows[2].Cells[j + 2].Style.Font != null && this.dgvSubtractFt90.Rows[2].Cells[j + 2].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                    }
                }
            }

            for (int j = 1; j <= 46; j++)
            {
                int count = 0;
                if(this.dgvSubtractFt45.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
                {
                    count++;
                }
                if (this.dgvSubtractFt45.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
                {
                    count++;
                }
                if (this.dgvSubtractFt45.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
                {
                    count++;
                }
                int baseValue = count * 25;
                int value = 0;
                int.TryParse(this.dgvSubtractFt45.Rows[3].Cells[j + 1].Value.ToString(), out value);
                if (value > baseValue) this.dgvSubtractFt45.Rows[3].Cells[j + 1].Style.BackColor = Color.Orange;

            }

            for (int j = 1; j <= 45; j++)
            {
                int count = 0;
                if (this.dgvSubtractFt90.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
                {
                    count++;
                }
                if (this.dgvSubtractFt90.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
                {
                    count++;
                }
                if (this.dgvSubtractFt90.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
                {
                    count++;
                }
                int baseValue = count * 25;
                int value = 0;
                int.TryParse(this.dgvSubtractFt90.Rows[3].Cells[j + 1].Value.ToString(), out value);
                if (value > baseValue) this.dgvSubtractFt90.Rows[3].Cells[j + 1].Style.BackColor = Color.Orange;

            }

            BoldMaxValueWithinTenMinutes(dgvSubtractFt45, (int)match.TimeSpanFromStart.TotalMinutes);
            BoldMaxValueWithinTenMinutes(dgvSubtractFt90, (int)match.TimeSpanFromStart.TotalMinutes);
            



            if (this._enableXtraView)
            {
                LoadFastMoveColor(dataGridView);
                LoadSlowMoveColor(dataGridView);
                LoadUpDownPeekValley();
            }

            if (this._enableXtraViewPriceUp)
            {
                LoadFast2CellMove(dataGridView);
            }

            if (this._enableXtraViewPriceDown)
            {
                LoadDownCellMove(dataGridView);
            }

            SetStyleDgvSubtractFt45Ht(maxHdps, overUnderScoreTimesV3);
            
        }

        private void SetStyleDgvSubtractFt45Ht(List<int> maxHdps, Dictionary<int, List<OverUnderScoreTimesV2Item>> overUnderScoreTimesV3)
        {
            int j = 92;
            int visualMinite = 48;

            if (maxHdps[j] % 100 == 0)
            {
                if (this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Style.BackColor = Color.LightPink;
                if (this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Style.BackColor = Color.SkyBlue;
                if (this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Style.BackColor = Color.LightGreen;
            }
            else if (maxHdps[j] % 100 == 25)
            {
                if (this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Style.BackColor = Color.LightYellow;
                if (this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Style.BackColor = Color.LightPink;
                if (this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Style.BackColor = Color.SkyBlue;
            }
            else if (maxHdps[j] % 100 == 50 && maxHdps[j] > 50)
            {
                if (this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Style.BackColor = Color.LightGreen;
                if (this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Style.BackColor = Color.LightYellow;
                if (this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Style.BackColor = Color.LightPink;
            }
            else if (maxHdps[j] % 100 == 75 && maxHdps[j] > 75)
            {
                if (this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Style.BackColor = Color.SkyBlue;
                if (this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Style.BackColor = Color.LightGreen;
                if (this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Value != null && this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Value.ToString().Length > 0)
                    this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Style.BackColor = Color.LightYellow;
            }

            //if (overUnderScoreTimesV3 != null)
            //{
            //    if (overUnderScoreTimesV3.ContainsKey(maxHdps[j])
            //        && overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 25))
            //    {
            //        int firstOver = overUnderScoreTimesV3[maxHdps[j]][j].Over;
            //        int secondOver = overUnderScoreTimesV3[maxHdps[j] - 25][j].Over;
            //        if (firstOver < 0 || secondOver < 0)
            //        {
            //            this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Style = new DataGridViewCellStyle(this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Style)
            //            {
            //                Font = new Font(
            //                    this.dgvSubtractFt45.DefaultCellStyle.Font, FontStyle.Bold
            //                    | (this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Style.Font != null && this.dgvSubtractFt45.Rows[0].Cells[visualMinite].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
            //                ),
            //                ForeColor = Color.Black
            //            };
            //        }
            //    }

            //    if (overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 25)
            //        && overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 50))
            //    {
            //        int firstOver = overUnderScoreTimesV3[maxHdps[j] - 25][j].Over;
            //        int secondOver = overUnderScoreTimesV3[maxHdps[j] - 50][j].Over;
            //        if (firstOver < 0 || secondOver < 0)
            //        {
            //            this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Style = new DataGridViewCellStyle(this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Style)
            //            {
            //                Font = new Font(
            //                    this.dgvSubtractFt45.DefaultCellStyle.Font, FontStyle.Bold
            //                    | (this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Style.Font != null && this.dgvSubtractFt45.Rows[1].Cells[visualMinite].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
            //                ),
            //                ForeColor = Color.Black
            //            };
            //        }
            //    }

            //    if (overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 75)
            //        && overUnderScoreTimesV3.ContainsKey(maxHdps[j] - 50))
            //    {
            //        int firstOver = overUnderScoreTimesV3[maxHdps[j] - 75][j].Over;
            //        int secondOver = overUnderScoreTimesV3[maxHdps[j] - 50][j].Over;
            //        if (firstOver < 0 || secondOver < 0)
            //        {
            //            this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Style = new DataGridViewCellStyle(this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Style)
            //            {
            //                Font = new Font(
            //                    this.dgvSubtractFt45.DefaultCellStyle.Font, FontStyle.Bold
            //                    | (this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Style.Font != null && this.dgvSubtractFt45.Rows[2].Cells[visualMinite].Style.Font.Strikeout ? FontStyle.Strikeout : FontStyle.Bold)
            //                ),
            //                ForeColor = Color.Black
            //            };
            //        }
            //    }
            //}
            
        }
        private void SetStyleUnderPriceGrid()
        {
            int columnCount = Constants.DefaultMinutePerSet * Constants.OverUnderScoreTime_ColumnPerMinute + 3;

            for (int i = 0; i < this.dgvUnderPrice.Rows.Count; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    this.dgvUnderPrice.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                }
            }

            List<GoalHistory> goalHistories;
            if (DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories))
            {
                int rowCount = this.dgvUnderPrice.Rows.Count;
                for (int i = 0; i < goalHistories.Count; i++)
                {
                    int column = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes + 1;
                    if (column > 45) continue;
                    for (int j = 0; j < rowCount; j++)
                    {
                        if (goalHistories[i].LivePeriod == 1)
                        {
                            this.dgvUnderPrice.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                        }
                        else if (goalHistories[i].LivePeriod == 2)
                        {
                            if (this.dgvUnderPrice.Rows[j].Cells[column].Style.BackColor == System.Drawing.Color.Red)
                            {
                                this.dgvUnderPrice.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Orange;
                            }
                            else
                            {
                                this.dgvUnderPrice.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Yellow;
                            }
                        }


                    }
                }
            }

            for (int i = 1; i < dgvUnderPrice.RowCount; i++)
            {
                for (int j = 1; j < dgvUnderPrice.ColumnCount; j++)
                {
                    if (this.dgvUnderPrice.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvUnderPrice.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvUnderPrice.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvUnderPrice.Rows[i].Cells[j].Style) { Font = new Font(this.dgvUnderPrice.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
                        }
                        else
                        {
                            this.dgvUnderPrice.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvUnderPrice.Rows[i].Cells[j].Style) { Font = new Font(this.dgvUnderPrice.DefaultCellStyle.Font, FontStyle.Regular), ForeColor = Color.Black };
                        }
                        this.dgvUnderPrice.Rows[i].Cells[j].Value = this.dgvUnderPrice.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }

                }
            }
        }

        private void LoadData()
        {
            Dictionary<int, List<string>> data;

            if (!DataStore.OverUnderScoreTimes.TryGetValue(_matchId, out data)) return;
            List<int> scores = data.Keys.OrderBy(item => item).ToList();

            List<ProductIdLog> productIdLogs = DataStore.ProductIdsOfMatch[_matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder && item.Minute <= -3)
                .ToList();

            for (int i = 0; i < productIdLogs.Count; i++)
            {
                if (!scores.Contains(productIdLogs[i].Hdp))
                {
                    scores.Add(productIdLogs[i].Hdp);
                }
            }

            scores = scores.OrderBy(item => item).ToList();

            if (scores.Count == 0) return;



            List<List<string>> subtractHdpFt = GetSubtractHdpFt();
            List<string> row_1 = subtractHdpFt[0].Take(48).ToList();
            List<string> row_2 = subtractHdpFt[1].Take(48).ToList();
            List<string> row_3 = subtractHdpFt[2].Take(48).ToList();
            List<string> row_4 = subtractHdpFt[3].Take(48).ToList();
            row_1.Add(subtractHdpFt[0][93]);
            row_2.Add(subtractHdpFt[1][93]);
            row_3.Add(subtractHdpFt[2][93]);
            row_4.Add(subtractHdpFt[3][93]);


            this.dgvSubtractFt45.Rows.Add(row_1.ToArray());
            this.dgvSubtractFt45.Rows.Add(row_2.ToArray());
            this.dgvSubtractFt45.Rows.Add(row_3.ToArray());
            this.dgvSubtractFt45.Rows.Add(row_4.ToArray());
            this.dgvSubtractFt45.Rows.Add(Common.Functions.SumOverUnderScoreTimesFreqFt45(_matchId).ToArray());

            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = new List<string>();
                row.Add(scores[i].ToString());

                ProductIdLog productIdLog = productIdLogs.FirstOrDefault(item => item.Hdp == scores[i]);
                if (productIdLog != null)
                {
                    row.Add(productIdLog.Odds1a100.ToString());
                }
                else
                {
                    row.Add("");
                }
                
                //working

                if (data.ContainsKey(scores[i]))
                {
                    row.AddRange(data[scores[i]].ToList());
                }
                else
                {
                    for (int j=0; j<this.dataGridView.ColumnCount-1; j++)
                    {
                        row.Add("");
                    }
                }

                
                if (DataStore.OverUnderScoreHalfTimes.ContainsKey(_matchId)
                    && DataStore.OverUnderScoreHalfTimes[_matchId].ContainsKey(scores[i]))
                {
                    row.Add(DataStore.OverUnderScoreHalfTimes[_matchId][scores[i]][2]);
                }
                else
                {
                    row.Add("");
                }

                if (DataStore.FinishPriceOverUnderScoreTimes.ContainsKey(_matchId)
                    && DataStore.FinishPriceOverUnderScoreTimes[_matchId].ContainsKey(scores[i]))
                {
                    row.Add(DataStore.FinishPriceOverUnderScoreTimes[_matchId][scores[i]].Over.ToString());
                }
                else
                {
                    row.Add("");
                }

                this.dataGridView.Rows.Add(row.ToArray());
            }

            List<string> row_5 = subtractHdpFt[0].Skip(45).Take(48).ToList();
            List<string> row_6 = subtractHdpFt[1].Skip(45).Take(48).ToList();
            List<string> row_7 = subtractHdpFt[2].Skip(45).Take(48).ToList();
            List<string> row_8 = subtractHdpFt[3].Skip(45).Take(48).ToList();
            row_5[0] = row_6[0] = row_7[0] = row_8[0] = row_5[1] = row_6[1] = row_7[1] = row_8[1] = row_5[2] = row_6[2] = row_7[2] = row_8[2] = "";

            this.dgvSubtractFt90.Rows.Add(row_5.ToArray());
            this.dgvSubtractFt90.Rows.Add(row_6.ToArray());
            this.dgvSubtractFt90.Rows.Add(row_7.ToArray());
            this.dgvSubtractFt90.Rows.Add(row_8.ToArray());
            this.dgvSubtractFt90.Rows.Add(Common.Functions.SumOverUnderScoreTimesFreqFt90(_matchId).ToArray());

        }

        private void BoldMaxValueWithinTenMinutes(DataGridView dgv, int currentMinute)
        {
            if (dgv == null) return;
            if (dgv.RowCount <= 1) return;
            if (currentMinute >= 46) return;
            if (currentMinute <= 0) return;
            int lastRowIndex = dgv.RowCount - 1;
            int fromMinute = currentMinute - 10 <= 0 ? 1 : currentMinute - 10;
            int maxValue = 0;
            for (int j = fromMinute + 2; j <= currentMinute + 2; j++)
            {
                int value = 0;
                if (int.TryParse(dgv[j, lastRowIndex].Value.ToString(), out value))
                {
                    if (value > maxValue)
                    {
                        maxValue = value;
                    }
                }
            }

            for (int j = fromMinute + 2; j <= currentMinute + 2; j++)
            {
                int value = 0;
                if (int.TryParse(dgv[j, lastRowIndex].Value.ToString(), out value))
                {
                    if (value == maxValue)
                    {
                        dgv[j, lastRowIndex].Style = new DataGridViewCellStyle(dgv[j, lastRowIndex].Style) { Font = new Font(this.dgvIuoo.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
                    }
                }
            }

        }


        private List<List<string>> GetSubtractHdpFt()
        {
            return Functions.GetSubtractHdpFt(_matchId);
        }

        private List<List<string>> GetSubtractHdpFh()
        {
            List<List<string>> result = new List<List<string>>();
            result.Add(new List<string>());
            result.Add(new List<string>());
            result.Add(new List<string>());

            for (int i = 0; i <= 47; i++)
            {
                result[0].Add("");
                result[1].Add("");
                result[2].Add("");
            }

            if (!DataStore.OverUnderScoreTimesFirstHalf.ContainsKey(_matchId)) return result;

            Match match = DataStore.Matchs[_matchId];

            Dictionary<int, List<string>> data = DataStore.OverUnderScoreTimesFirstHalf[_matchId];

            List<int> keys = data.Keys.OrderByDescending(item => item).ToList();

            List<ProductIdLog> productIdLogs = DataStore.ProductIdsOfMatch[_matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder && item.Minute <= -3)
                .ToList();

            for (int i = 0; i < productIdLogs.Count; i++)
            {
                if (!keys.Contains(productIdLogs[i].Hdp))
                {
                    keys.Add(productIdLogs[i].Hdp);
                }
            }
            keys = keys.OrderByDescending(item => item).ToList();

            //working





            for (int minute = 0; minute <= 45; minute++)
            {
                int keyIndex = 0;
                for (int key = 0; key < keys.Count - 1; key++)
                {
                    if (data.ContainsKey(keys[key])
                        && data.ContainsKey(keys[key + 1]))
                    {
                        if (data[keys[key]][minute].Length > 0
                            && data[keys[key + 1]][minute].Length > 0)
                        {
                            result[keyIndex][minute + 2] = Common.Functions.CalculateSubtractValue(int.Parse(data[keys[key]][minute]), int.Parse(data[keys[key + 1]][minute]), match.OverUnderMoneyLine).ToString();
                            keyIndex++;
                            if (keyIndex == 3) break;
                        }
                    }
                }
            }

            for (int minute = 0; minute <= 1; minute++)
            {
                int keyIndex = 0;
                for (int key = 0; key < keys.Count - 1; key++)
                {
                    ProductIdLog currentProductIdLog = productIdLogs.FirstOrDefault(item => item.Hdp == keys[key]);
                    ProductIdLog belowProductIdLog = productIdLogs.FirstOrDefault(item => item.Hdp == keys[key + 1]);
                    if (currentProductIdLog != null && belowProductIdLog != null)
                    {
                        result[keyIndex][1] = Common.Functions.CalculateSubtractValue(currentProductIdLog.Odds1a100, belowProductIdLog.Odds1a100, match.OverUnderMoneyLine).ToString();
                        keyIndex++;
                        if (keyIndex == 3) break;
                    }
                }
            }

            return result;
        }

        private void LoadDataUnderGrid()
        {
            Dictionary<int, List<string>> data;

            if (!DataStore.UnderScoreTimes.TryGetValue(_matchId, out data)) return;
            List<int> scores = data.Keys.OrderBy(item => item).ToList();

            List<ProductIdLog> productIdLogs = DataStore.ProductIdsOfMatch[_matchId]
                .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder && item.Minute <= -3)
                .ToList();

            for (int i = 0; i < productIdLogs.Count; i++)
            {
                if (!scores.Contains(productIdLogs[i].Hdp))
                {
                    scores.Add(productIdLogs[i].Hdp);
                }
            }
            //working

            if (scores.Count == 0) return;

            scores = scores.OrderBy(item => item).ToList();

            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = new List<string>();
                row.Add(scores[i].ToString());

                ProductIdLog productIdLog = productIdLogs.FirstOrDefault(item => item.Hdp == scores[i]);
                if (productIdLog != null)
                {
                    row.Add(productIdLog.Odds2a100.ToString());
                }
                else
                {
                    row.Add("");
                }

                //working

                if (data.ContainsKey(scores[i]))
                {
                    row.AddRange(data[scores[i]].ToList());
                }
                else
                {
                    for (int j = 0; j < this.dataGridView.ColumnCount - 1; j++)
                    {
                        row.Add("");
                    }
                }

                if (DataStore.UnderScoreHalfTimes.ContainsKey(_matchId))
                {
                    if (DataStore.UnderScoreHalfTimes[_matchId].ContainsKey(scores[i]))
                    {
                        row.Add(DataStore.UnderScoreHalfTimes[_matchId][scores[i]][2]);
                    }
                    else
                    {
                        row.Add("");
                    }
                }
                else
                {
                    row.Add("");
                }

                this.dgvUnderPrice.Rows.Add(row.ToArray());
            }
        }

        private delegate void dlgLoadUnderPriceGrid();
        private void LoadUnderPriceGrid()
        {
            if (this.dgvUnderPrice.InvokeRequired)
            {
                this.Invoke(new dlgLoadUnderPriceGrid(LoadUnderPriceGrid));
            }
            else
            {
                if (!DataStore.Matchs.ContainsKey(_matchId)) return;
                Match match = DataStore.Matchs[_matchId];
                int columnCount = Constants.DefaultMinutePerSet * Constants.OverUnderScoreTime_ColumnPerMinute + 4;
                this.dgvUnderPrice.Rows.Clear();

                this.dgvUnderPrice.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    this.dgvUnderPrice.Columns[i].Width = Common.Functions.ColWidthStyle();
                }
                this.dgvUnderPrice.Columns[0].Width = Common.Functions.ColWidthStyle()+10;

                ArrayList header = new ArrayList() { "", "_" };
                for (int i = 1; i < columnCount - 2; i++)
                {
                    header.Add(((i - 1) / Constants.OverUnderScoreTime_ColumnPerMinute).ToString());
                }
                header.Add("ht-02");

                this.dgvUnderPrice.Rows.Add(header.ToArray());

                LoadDataUnderGrid();

                SetStyleUnderPriceGrid();
            }
        }

        private delegate void dlgLoadIuooGrid();
        private void LoadIuooGrid()
        {
            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId)) return;

            if (this.dgvIuoo.InvokeRequired)
            {
                this.Invoke(new dlgLoadIuooGrid(LoadIuooGrid));
            }
            else
            {
                BindingSource bindingSource = new BindingSource();
                int maxNumberOfHdp = Common.Functions.MaxNumberOfHdp(_matchId);

                //if (maxNumberOfHdp == 2)
                //{
                //    data_2 = GetDataIuoo_20191011_2();
                //    bindingSource.DataSource = Common.Functions.ToDataTable(data_2);
                //    dgvIuoo.Columns.Clear();
                //    dgvIuoo.DataSource = bindingSource;
                //    SetStyleIuooGrid_2();
                //}
                //else if (maxNumberOfHdp == 3)
                {
                    //data_3 = GetDataIuoo_ByGoal_3(1, false, maxNumberOfHdp);
                    _dataIuoo_1 = Common.Functions.GetDataIuoo_ByGoal_3(_matchId, 1, false, maxNumberOfHdp);
                    bindingSource.DataSource = Common.Functions.ToDataTable(_dataIuoo_1);
                    dgvIuoo.Columns.Clear();
                    dgvIuoo.DataSource = bindingSource;
                    SetStyleIuooGrid_3(dgvIuoo);

                    
                    //var b = GetDataIuoo_ByGoal_3(2, false, maxNumberOfHdp);

                }
                //else if (maxNumberOfHdp == 4)
                //{
                //    data_3 = GetDataIuoo_20191011_3(true);
                //    bindingSource.DataSource = Common.Functions.ToDataTable(data_3);
                //    dgvIuoo.Columns.Clear();
                //    dgvIuoo.DataSource = bindingSource;
                //    SetStyleIuooGrid_3();
                //}
            }
        }

        private delegate void dlgLoadIuooGrid_2();
        private void LoadIuooGrid_2()
        {
            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId)) return;

            if (this.dgvIuoo_2.InvokeRequired)
            {
                this.Invoke(new dlgLoadIuooGrid_2(LoadIuooGrid_2));
            }
            else
            {
                BindingSource bindingSource = new BindingSource();
                int maxNumberOfHdp = Common.Functions.MaxNumberOfHdp(_matchId);

                //data_3 = GetDataIuoo_ByGoal_3(2, false, maxNumberOfHdp);
                _dataIuoo_2 = Common.Functions.GetDataIuoo_ByGoal_3(_matchId, 2, false, maxNumberOfHdp);
                for (int i=0;i< _dataIuoo_2.Count; i++)
                {
                    _dataIuoo_2[i].Hdp += 100;
                    if (_dataIuoo_2[i].TGT > 0)
                    {
                        _dataIuoo_2[i].TX = _dataIuoo_2[i].TGT - _dataIuoo_2[i].TGX + 1;
                    }
                    
                }

                bindingSource.DataSource = Common.Functions.ToDataTable(_dataIuoo_2);
                dgvIuoo_2.Columns.Clear();
                dgvIuoo_2.DataSource = bindingSource;
                SetStyleIuooGrid_3(dgvIuoo_2);

                
            }
        }


        private void SetStyleIuooGrid_3(DataGridView dgv)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].Width = 24;
            }
            dgv.Columns[0].Width = 30;
            dgv.Columns[1].Width = 20;
            dgv.Columns[2].Width = 20;

            dgv.Columns["TW_a"].Visible = false;
            dgv.Columns["TW_b"].Visible = false;
            dgv.Columns["TW_c"].Visible = false;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {



                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    if ((dgv.Rows[i].Cells[j].Value.ToString() != "0" && (j == 7 || j == 8 || j == 10 || j == 12 || j == 14)))
                    {
                        dgv.Rows[i].Cells[j].Style = new DataGridViewCellStyle(dgv.Rows[i].Cells[j].Style) { Font = new Font(this.dgvIuoo.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
                    }

                    dgv.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                    if (j == 9 || j == 10)
                    {
                        dgv.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.SkyBlue;
                    }

                    if (j == 12 || j == 11)
                    {
                        dgv.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.YellowGreen;
                    }

                    if (j == 14 || j == 13)
                    {
                        dgv.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Yellow;
                    }

                    if (i == 0 && j == 6)
                    {
                        dgv.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.SkyBlue;
                    }

                    if (i == 1 && j == 6)
                    {
                        dgv.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.YellowGreen;
                    }

                    if (i == 2 && j == 6)
                    {
                        dgv.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Yellow;
                    }
                }
                if (dgv.Rows[i].Cells[3].Value.ToString().Length > 0
                    && !dgv.Rows[i].Cells[3].Value.ToString().Contains("-"))
                {
                    dgv.Rows[i].Cells[3].Style.BackColor = Color.Red;
                }
            }
        }

        private List<IuooValue_3> GetDataIuoo_ByGoal_3(int goalTh, bool hasFourHdpCount, int maxNumberOfHdp)
        {
            List<int> Hdps = DataStore.OverUnderScoreTimes[_matchId]
                .Keys
                .OrderByDescending(item => item)
                .ToList();

            Dictionary<int, IuooValue_3> data = new Dictionary<int, IuooValue_3>();

            for (int i = 0; i < Hdps.Count; i++)
            {
                data.Add(Hdps[i], CalculateIuooValue_ByMinute_3(Hdps[i], goalTh));
            }

            if (!DataStore.OverUnderScoreTimes.ContainsKey(_matchId)
                || DataStore.OverUnderScoreTimes[_matchId].Keys.Count == 0)
            {
                return new List<IuooValue_3>();
            }

            int goalLimit = goalTh - 1;
            int limitMinute = 0;
            int fromMinute = 1;

            if ((DataStore.GoalHistories.ContainsKey(_matchId)
                && DataStore.GoalHistories[_matchId].Count >= goalLimit))
            {
                if (DataStore.GoalHistories[_matchId].Count == goalLimit)
                {
                    limitMinute = 90;
                }
                else
                {
                    limitMinute = (int)DataStore.GoalHistories[_matchId][goalLimit].TimeSpanFromStart.TotalMinutes + (45 * (DataStore.GoalHistories[_matchId][goalLimit].LivePeriod - 1));
                    limitMinute = ((limitMinute >= 90) ? 90 : (limitMinute + 1));
                }

                if (goalLimit == 0)
                {
                    fromMinute = 1;
                }
                else
                {
                    GoalHistory lastGoal = DataStore.GoalHistories[_matchId][goalLimit - 1];
                    fromMinute = (int)lastGoal.TimeSpanFromStart.TotalMinutes;
                    if (lastGoal.LivePeriod == 2)
                    {
                        fromMinute += 45;
                    }

                    if (goalTh > 1)
                    {
                        fromMinute+=2;
                    }
                }
            }
            else if (goalLimit == 0)
            {
                limitMinute = 90;
            }

            int maxHdp = FindMaxHdpOfIuoo(fromMinute, limitMinute, Hdps);
            maxHdp = (hasFourHdpCount && (maxHdp - 25 > 0)) ? maxHdp - 25 : maxHdp;

            if (!data.ContainsKey(maxHdp) || !data.ContainsKey(maxHdp - 25))
            {
                return new List<IuooValue_3>();
            }

            int selectedHdp = data[maxHdp].TGT < data[maxHdp - 25].TGT ? maxHdp : maxHdp - 25;

            Hdps = Hdps.Where(item => item > 0 && item <= maxHdp)
                .OrderByDescending(item => item)
                .ToList();

            for (int i = 0; i < Hdps.Count; i++)
            {
                if (Hdps[i] < maxHdp)
                {
                    data[Hdps[i]].TW_a = data[maxHdp].TGT * (i + 1);
                }

                if (Hdps[i] < maxHdp - 25)
                {
                    data[Hdps[i]].TW_b = data[maxHdp - 25].TGT * i;
                }

                if (data.ContainsKey(maxHdp - 50))
                {
                    if (Hdps[i] < maxHdp - 50)
                    {
                        data[Hdps[i]].TW_c = data[maxHdp - 50].TGT * i;
                    }
                }


                if (maxNumberOfHdp <= 2)
                {
                    if ((Hdps[i] == maxHdp || Hdps[i] == maxHdp - 25) && data[Hdps[i]].TX > 0)
                    {
                        data[Hdps[i]].TX = data[Hdps[i]].TGT;
                    }
                }
                else if (maxNumberOfHdp > 2)
                {
                    if ((Hdps[i] == maxHdp || Hdps[i] == maxHdp - 25 || Hdps[i] == maxHdp - 50) && data[Hdps[i]].TX > 0)
                    {
                        data[Hdps[i]].TX = data[Hdps[i]].TGT;
                    }
                }



                data[Hdps[i]] = UpdateTwl_3(data[maxHdp - 0].GX, data[Hdps[i]], 1, fromMinute, limitMinute);
                data[Hdps[i]] = UpdateTwl_3(data[maxHdp - 25].GX, data[Hdps[i]], 2, fromMinute, limitMinute);
                if (data.ContainsKey(maxHdp - 50))
                {
                    data[Hdps[i]] = UpdateTwl_3(data[maxHdp - 50].GX, data[Hdps[i]], 3, fromMinute, limitMinute);
                }
            }

            return data.Values.Where(item => item.Hdp > 0 && item.Hdp <= maxHdp && item.GX != 0).ToList();
        }
        private IuooValue_3 UpdateTwl_3(int priceUnder, IuooValue_3 iuoo, int update_character, int fromMinute, int limitMinute)
        {
            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId)
                || !DataStore.OverUnderScoreTimesV3[_matchId].ContainsKey(iuoo.Hdp))
            {
                return iuoo;
            }

            List<OverUnderScoreTimesV2Item> row = DataStore.OverUnderScoreTimesV3[_matchId][iuoo.Hdp];

            for (int i = fromMinute; i <= limitMinute; i++)
            {
                if ((row[i].Over + priceUnder >= 0 && row[i].Over * priceUnder < 0)
                    || (row[i].Over < 0 && priceUnder < 0))
                {
                    if (update_character == 1)
                    {
                        iuoo.TWL_a = i;
                    }
                    else if (update_character == 2)
                    {
                        iuoo.TWL_b = i;
                    }
                    else
                    {
                        iuoo.TWL_c = i;
                    }
                    return iuoo;
                }
            }

            return iuoo;
        }

        private IuooValue_3 CalculateIuooValue_ByMinute_3(int hdp, int goalTh)
        {
            IuooValue_3 result = new IuooValue_3();

            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId)
                || !DataStore.OverUnderScoreTimesV3[_matchId].ContainsKey(hdp))
            {
                return result;
            }

            List<OverUnderScoreTimesV2Item> row = DataStore.OverUnderScoreTimesV3[_matchId][hdp];


            int goalLimit = goalTh - 1;
            int limitMinute = 0;
            int fromMinute = 1;
            if (DataStore.GoalHistories.ContainsKey(_matchId)
                && DataStore.GoalHistories[_matchId].Count >= goalLimit)
            {
                if (goalLimit == DataStore.GoalHistories[_matchId].Count)
                {
                    limitMinute = 90;
                }
                else
                {
                    limitMinute = (int)DataStore.GoalHistories[_matchId][goalLimit].TimeSpanFromStart.TotalMinutes + (45 * (DataStore.GoalHistories[_matchId][goalLimit].LivePeriod - 1));
                    limitMinute = ((limitMinute >= 90) ? 90 : (limitMinute + 1));
                }

                if (goalLimit == 0)
                {
                    fromMinute = 1;
                }
                else
                {
                    GoalHistory lastGoal = DataStore.GoalHistories[_matchId][goalLimit - 1];
                    fromMinute = (int)lastGoal.TimeSpanFromStart.TotalMinutes;
                    if (lastGoal.LivePeriod == 2)
                    {
                        fromMinute += 45;
                    }

                    if (goalTh > 1)
                    {
                        fromMinute+=2;
                    }
                }

            }
            else if (goalLimit == 0)
            {
                limitMinute = 90;
            }



            result.Hdp = hdp;
            result.K = 0;
            for (int i = fromMinute; i <= limitMinute; i++)
            {
                if (row[i].Over == 0)
                {

                }
                else
                {
                    result.K = i - 1;
                    break;
                }
            }

            for (int i = fromMinute; i <= limitMinute; i++)
            {
                if (row[i].Under != 0)
                {
                    result.GX = row[i].Under;
                    result.TGX = i;
                    break;
                }
            }

            result.R = 0;
            for (int i = result.K + 1; i < limitMinute; i++)
            {
                if (row[i].Over != 0)
                {
                    result.R++;
                    if (result.TGT == 0 &&
                        ((row[i].Over + result.GX >= 0 && row[i].Over * result.GX < 0) || (row[i].Over < 0 && result.GX < 0)))
                    {
                        result.TGT = i;
                    }
                }
                if (row[i].Over == 0)
                {
                    if (i + 1 < limitMinute)
                    {
                        if (row[i + 1].Over == 0)
                        {
                            result.TCLOSE = i - 1;
                            break;
                        }
                    }
                    else
                    {
                        result.TCLOSE = i - 1;
                        break;
                    }

                }

            }

            if (result.TGT > 0)
            {
                result.TX = result.TGT - result.TGX + 1;
            }

            return result;
        }

        private int FindMaxHdpOfIuoo(int fromMinute, int limitMinute, List<int> Hdps)
        {
            int maxHdp = 0;

            for (int i = fromMinute; i <= limitMinute; i++)
            {
                for (int j = 0; j < Hdps.Count; j++)
                {
                    if (DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId)
                        && DataStore.OverUnderScoreTimesV3[_matchId].ContainsKey(Hdps[j])
                        && DataStore.OverUnderScoreTimesV3[_matchId][Hdps[j]][i].Over != 0
                        && DataStore.OverUnderScoreTimesV3[_matchId][Hdps[j]][i].Under != 0)
                    {
                        return Hdps[j];
                    }
                }
            }

            return maxHdp;
        }

        private int FindMaxHdpOfIuoo(int limitMinute, List<int> Hdps)
        {
            int maxHdp = 0;

            for (int i = 1; i <= limitMinute; i++)
            {
                for (int j = 0; j < Hdps.Count; j++)
                {
                    if (DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId)
                        && DataStore.OverUnderScoreTimesV3[_matchId].ContainsKey(Hdps[j])
                        && DataStore.OverUnderScoreTimesV3[_matchId][Hdps[j]][i].Over != 0
                        && DataStore.OverUnderScoreTimesV3[_matchId][Hdps[j]][i].Under != 0)
                    {
                        return Hdps[j];
                    }
                }
            }

            return maxHdp;
        }

        private List<IuooValue_3> GetDataIuoo_20191011_3(bool hasFourHdpCount, int maxNumberOfHdp)
        {
            List<int> Hdps = DataStore.OverUnderScoreTimes[_matchId]
                .Keys
                .OrderByDescending(item => item)
                .ToList();

            Dictionary<int, IuooValue_3> data = new Dictionary<int, IuooValue_3>();

            for (int i = 0; i < Hdps.Count; i++)
            {
                data.Add(Hdps[i], CalculateIuooValue_3(Hdps[i]));
            }

            if (!DataStore.OverUnderScoreTimes.ContainsKey(_matchId)
                || DataStore.OverUnderScoreTimes[_matchId].Keys.Count == 0)
            {
                return new List<IuooValue_3>();
            }



            int limitMinute = 90;
            if (DataStore.GoalHistories.ContainsKey(_matchId)
                && DataStore.GoalHistories[_matchId].Count > 0)
            {
                limitMinute = (int)DataStore.GoalHistories[_matchId][0].TimeSpanFromStart.TotalMinutes + (45 * (DataStore.GoalHistories[_matchId][0].LivePeriod - 1));
                limitMinute = ((limitMinute >= 90) ? 90 : (limitMinute + 1));
            }

            int maxHdp = FindMaxHdpOfIuoo(limitMinute, Hdps);
            maxHdp = (hasFourHdpCount && (maxHdp - 25 > 0)) ? maxHdp - 25 : maxHdp;

            if (!data.ContainsKey(maxHdp) || !data.ContainsKey(maxHdp - 25) || !data.ContainsKey(maxHdp - 50))
            {
                return new List<IuooValue_3>();
            }

            int selectedHdp = data[maxHdp].TGT < data[maxHdp - 25].TGT ? maxHdp : maxHdp - 25;

            Hdps = Hdps.Where(item => item > 0 && item <= maxHdp)
                .OrderByDescending(item => item)
                .ToList();

            for (int i = 0; i < Hdps.Count; i++)
            {
                if (Hdps[i] < maxHdp)
                {
                    data[Hdps[i]].TW_a = data[maxHdp].TGT * (i + 1);
                }

                if (Hdps[i] < maxHdp - 25)
                {
                    data[Hdps[i]].TW_b = data[maxHdp - 25].TGT * i;
                }

                if (Hdps[i] < maxHdp - 50)
                {
                    data[Hdps[i]].TW_c = data[maxHdp - 50].TGT * i;
                }

                if (maxNumberOfHdp <= 2)
                {
                    if ((Hdps[i] == maxHdp || Hdps[i] == maxHdp - 25) && data[Hdps[i]].TX > 0)
                    {
                        data[Hdps[i]].TX = data[Hdps[i]].TGT;
                    }
                }
                else if (maxNumberOfHdp > 2)
                {
                    if ((Hdps[i] == maxHdp || Hdps[i] == maxHdp - 25 || Hdps[i] == maxHdp - 50) && data[Hdps[i]].TX > 0)
                    {
                        data[Hdps[i]].TX = data[Hdps[i]].TGT;
                    }
                }



                data[Hdps[i]] = UpdateTwl_3(data[maxHdp -  0].GX, data[Hdps[i]], 1, limitMinute);
                data[Hdps[i]] = UpdateTwl_3(data[maxHdp - 25].GX, data[Hdps[i]], 2, limitMinute);
                data[Hdps[i]] = UpdateTwl_3(data[maxHdp - 50].GX, data[Hdps[i]], 3, limitMinute);
            }

            return data.Values.Where(item => item.Hdp > 0 && item.Hdp <= maxHdp).ToList();
        }

        private IuooValue_3 UpdateTwl_3(int priceUnder, IuooValue_3 iuoo, int update_character, int limitMinute)
        {
            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId)
                || !DataStore.OverUnderScoreTimesV3[_matchId].ContainsKey(iuoo.Hdp))
            {
                return iuoo;
            }

            List<OverUnderScoreTimesV2Item> row = DataStore.OverUnderScoreTimesV3[_matchId][iuoo.Hdp];

            for (int i = 1; i <= limitMinute; i++)
            {
                if ((row[i].Over + priceUnder >= 0 && row[i].Over * priceUnder < 0)
                    || (row[i].Over < 0 && priceUnder < 0))
                {
                    if (update_character == 1)
                    {
                        iuoo.TWL_a = i;
                    }
                    else if (update_character == 2)
                    {
                        iuoo.TWL_b = i;
                    }
                    else
                    {
                        iuoo.TWL_c = i;
                    }
                    return iuoo;
                }
            }

            return iuoo;
        }

        private IuooValue_3 CalculateIuooValue_3(int hdp)
        {
            IuooValue_3 result = new IuooValue_3();

            if (!DataStore.OverUnderScoreTimesV3.ContainsKey(_matchId)
                || !DataStore.OverUnderScoreTimesV3[_matchId].ContainsKey(hdp))
            {
                return result;
            }

            List<OverUnderScoreTimesV2Item> row = DataStore.OverUnderScoreTimesV3[_matchId][hdp];

            int limitMinute = 90;
            if (DataStore.GoalHistories.ContainsKey(_matchId)
                && DataStore.GoalHistories[_matchId].Count > 0)
            {
                limitMinute = (int)DataStore.GoalHistories[_matchId][0].TimeSpanFromStart.TotalMinutes + (45 * (DataStore.GoalHistories[_matchId][0].LivePeriod - 1));
                limitMinute = ((limitMinute >= 90) ? 90 : (limitMinute + 1));
            }

            result.Hdp = hdp;
            result.K = 0;
            for (int i = 2; i <= limitMinute; i++)
            {
                if (row[i].Over == 0)
                {
                    
                }
                else
                {
                    result.K = i - 1;
                    break;
                }
            }

            for (int i = 0; i <= limitMinute; i++)
            {
                if (row[i].Under != 0)
                {
                    result.GX = row[i].Under;
                    result.TGX = i;
                    break;
                }
            }

            result.R = 0;
            for (int i = result.K + 1; i < limitMinute; i++)
            {
                if (row[i].Over != 0)
                {
                    result.R++;
                    if (result.TGT == 0 &&
                        ((row[i].Over + result.GX >= 0 && row[i].Over * result.GX < 0) || (row[i].Over < 0 && result.GX < 0)))
                    {
                        result.TGT = i;
                    }
                }
                if (row[i].Over == 0)
                {
                    if (i + 1 < limitMinute)
                    {
                        if (row[i + 1].Over == 0)
                        {
                            result.TCLOSE = i - 1;
                            break;
                        }
                    }
                    else
                    {
                        result.TCLOSE = i - 1;
                        break;
                    }

                }

            }

            if (result.TGT > 0)
            {
                result.TX = result.TGT - result.TGX + 1;
            }

            return result;
        }

        private void LoadFast2CellMove(DataGridView dgv)
        {
            for (int i = 1; i < dgv.RowCount; i++)
            {
                for (int j = 5; j < dgv.ColumnCount; j++)
                {
                    int current;
                    int previous;

                    if (dgv.Rows[i].Cells[j].Value != null
                        && dgv.Rows[i].Cells[j - 2].Value != null
                        && int.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out current)
                        && int.TryParse(dgv.Rows[i].Cells[j - 2].Value.ToString(), out previous)
                        && current != 0
                        && previous != 0)
                    {
                        if (dgv.Rows[i].Cells[j].Style.Font.Bold)
                        {
                            current = -current;
                        }

                        if (dgv.Rows[i].Cells[j - 2].Style.Font.Bold)
                        {
                            previous = -previous;
                        }

                        if(Common.Functions.Fast2CellMove(current, previous, DataStore.Matchs[_matchId].PriceStep))
                        {
                            dgv.Rows[i].Cells[j].Style.ForeColor = Color.White;
                            dgv.Rows[i].Cells[j].Style.BackColor = Color.Black;

                            dgv.Rows[i].Cells[j-2].Style.ForeColor = Color.White;
                            dgv.Rows[i].Cells[j-2].Style.BackColor = Color.Black;
                        }

                    }
                }
            }
        }

        private void LoadDownCellMove(DataGridView dgv)
        {
            for (int i = 1; i < dgv.RowCount; i++)
            {
                for (int j = 4; j < dgv.ColumnCount; j++)
                {
                    int current;
                    int previous;

                    if (dgv.Rows[i].Cells[j].Value != null
                        && dgv.Rows[i].Cells[j - 1].Value != null
                        && int.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out current)
                        && int.TryParse(dgv.Rows[i].Cells[j - 1].Value.ToString(), out previous)
                        && current != 0
                        && previous != 0)
                    {
                        if (dgv.Rows[i].Cells[j].Style.Font.Bold)
                        {
                            current = -current;
                        }

                        if (dgv.Rows[i].Cells[j-1].Style.Font.Bold)
                        {
                            previous = -previous;
                        }

                        if(Common.Functions.DownCellMove(previous, current, DataStore.Matchs[_matchId].PriceStepDown))
                        {
                            dgv.Rows[i].Cells[j].Style.ForeColor = Color.Yellow;
                            dgv.Rows[i].Cells[j].Style.BackColor = Color.Black;

                            dgv.Rows[i].Cells[j - 1].Style.ForeColor = Color.Yellow;
                            dgv.Rows[i].Cells[j - 1].Style.BackColor = Color.Black;
                        }
                    }

                }
            }
        }

        private void LoadSlowMoveColor(DataGridView dgv)
        {
            for (int i = 1; i < dgv.RowCount; i++)
            {
                for (int j = 2; j < dgv.ColumnCount; j++)
                {
                    int current;
                    int previous;

                    if (dgv.Rows[i].Cells[j].Value != null
                        && dgv.Rows[i].Cells[j - 1].Value != null
                        && int.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out current)
                        && int.TryParse(dgv.Rows[i].Cells[j - 1].Value.ToString(), out previous)
                        && current != 0
                        && previous != 0
                        && Common.Functions.SlowOverUnderPriceStep(current, previous))
                    {
                        dgv.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.HotPink;
                        dgv.Rows[i].Cells[j - 1].Style.BackColor = System.Drawing.Color.HotPink;
                    }
                }
            }
        }

        private void LoadFastMoveColor(DataGridView dgv)
        {
            for (int i = 1; i < dgv.RowCount; i++)
            {
                for (int j = 2; j < dgv.ColumnCount; j++)
                {
                    int current;
                    int previous;

                    if (dgv.Rows[i].Cells[j].Value != null
                        && dgv.Rows[i].Cells[j - 1].Value != null
                        && int.TryParse(dgv.Rows[i].Cells[j].Value.ToString(), out current)
                        && int.TryParse(dgv.Rows[i].Cells[j - 1].Value.ToString(), out previous)
                        && current != 0
                        && previous != 0
                        && Common.Functions.FastOverUnderPriceStep(current, previous))
                    {
                        dgv.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.LightGreen;
                        dgv.Rows[i].Cells[j - 1].Style.BackColor = System.Drawing.Color.LightGreen;
                    }
                }
            }
        }

        private void LoadUpDownPeekValley()
        {
            Match match = DataStore.Matchs[_matchId];
            int minute;

            List<List<int>> data = new List<List<int>>();
            for (int i=2; i<=44; i++)
            {
                minute = i;
                data = Common.Functions.ExtractDataUpDownPeekValley(minute, _matchId);
                if (Common.Functions.CheckLoadUpDownPeekValley(data))
                {
                    SetStyleUpDownPeekValley(data, i);
                    // get available keys in v2 then color v1 by these keys and minute
                }

                minute = i + 45;
                data = Common.Functions.ExtractDataUpDownPeekValley(minute, _matchId);
                if (Common.Functions.CheckLoadUpDownPeekValley(data))
                {
                    SetStyleUpDownPeekValley(data, i);
                    // get available keys in v2 then color v1 by these keys and minute
                }
            }

        }

        private void SetStyleUpDownPeekValley(List<List<int>> data, int minute)
        {
            int checkedRows = 0;
            int previous;
            int current;
            int next;
            
            for (int i = 1; i < dataGridView.RowCount - data.Count; i++)
            {
                checkedRows = 0;
                for (int j = 0; j < data.Count; j++)
                {
                    if (this.dataGridView.Rows[i + j].Cells[minute - 1].Value != null)
                    {
                        int.TryParse(this.dataGridView.Rows[i + j].Cells[minute - 1].Value.ToString(), out previous);
                    }
                    else
                    {
                        previous = 0;
                    }

                    if (this.dataGridView.Rows[i + j].Cells[minute].Value != null)
                    {
                        int.TryParse(this.dataGridView.Rows[i + j].Cells[minute].Value.ToString(), out current);
                    }
                    else
                    {
                        current = 0;
                    }

                    if (this.dataGridView.Rows[i + j].Cells[minute + 1].Value != null)
                    {
                        int.TryParse(this.dataGridView.Rows[i + j].Cells[minute + 1].Value.ToString(), out next);
                    }
                    else
                    {
                        next = 0;
                    }

                    if (previous == data[j][0]
                        && current == data[j][1]
                        && next == data[j][2]
                    )
                    {
                        checkedRows++;
                    }
                }

                if (checkedRows == data.Count)
                {
                    Font f = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Italic | FontStyle.Underline);
                    for (int j = 0; j < data.Count; j++)
                    {
                        this.dataGridView.Rows[i + j].Cells[minute - 1].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i + j].Cells[minute - 1].Style) { Font = f, ForeColor = Color.Black };
                        this.dataGridView.Rows[i + j].Cells[minute    ].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i + j].Cells[minute    ].Style) { Font = f, ForeColor = Color.Black };
                        this.dataGridView.Rows[i + j].Cells[minute + 1].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i + j].Cells[minute + 1].Style) { Font = f, ForeColor = Color.Black };
                    }

                }

            }
        }

        private void OverUnderScoreTimeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();

            DataStore.OpeningDetailMatch.RemoveAll(item => item == _matchId);
        }

        private void timmingBetOverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (AddBetByTimmingForm form = new AddBetByTimmingForm(this._matchId))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
        }

        private void handicapPricesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataStore.ShowSabaOnly)
                {
                    using (HandicapPricesFullForm form = new HandicapPricesFullForm(this._matchId))
                    {
                        form.ShowDialog();
                    }
                }
                else
                {
                    using (HandicapPricesForm form = new HandicapPricesForm(this._matchId))
                    {
                        form.ShowDialog();
                    }
                }


            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
        }

        private void betByHdpPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddBetByHdpPriceForm form = new AddBetByHdpPriceForm(this._matchId);
                form.Show();
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
        }

        private void sheetTableV2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OverUnderScoreTimeV2Form form = new OverUnderScoreTimeV2Form(_matchId);
            form.Show();
        }

        private void betFirstHalfByFulltimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBetFirstHalfByFulltimeForm form = new AddBetFirstHalfByFulltimeForm(_matchId);
            form.Show();
        }

        private void bookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Match match = DataStore.Matchs[this._matchId];

            CaptureScreen(Common.Functions.GetBookmarkPath(match));

            if (!DataStore.BookmarkedMatchs.Contains(this._matchId))
            {
                DataStore.BookmarkedMatchs.Add(this._matchId);
                bookmarkToolStripMenuItem.Text = "Bookmark_ed";
            }
        }

        private void CaptureScreen(string path)
        {
            Bitmap memoryImage;
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            memoryImage.Save(path);
        }

        private void UpdateMenuBar()
        {
            this.xtraViewToolStripMenuItem1.Text = this._enableXtraView ? "Disable XtraView" : "Enable XtraView";
            this.xtraViewPriceUpToolStripMenuItem.Text = this._enableXtraViewPriceUp ? "Disable XtraView - Price Up" : "Enable XtraView - Price Up";
            this.xtraViewPriceDownToolStripMenuItem.Text = this._enableXtraViewPriceDown ? "Disable XtraView - Price Down" : "Enable XtraView - Price Down";
        }

        private void xtraViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void betByHdpCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddBetByHdpCloseForm form = new AddBetByHdpCloseForm(this._matchId);
                form.Show();
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
        }

        private void iUOOAlertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (MatchIuooAlert form = new MatchIuooAlert(this._matchId))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
        }

        private void halftimeOverUnderPricesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OverUnderScoreHalftimeForm form = new OverUnderScoreHalftimeForm(this._matchId))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
            
        }

        private void betByQuick050ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmQuickStake form = new ConfirmQuickStake(_matchId, 50, DataStore.QuickBetMinute, false, false, this);
            form.ShowDialog();

        }

        private void betByQuick075ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmQuickStake form = new ConfirmQuickStake(_matchId, 75, DataStore.QuickBetMinute, false, false, this);
            form.ShowDialog();
        }

        private void betByQuick100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmQuickStake form = new ConfirmQuickStake(_matchId, 100, DataStore.QuickBetMinute, false, false, this);
            form.ShowDialog();
        }

        private void cancelAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Cancel all?", "Confirm cancel", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                DataStore.BetByQuick.RemoveAll(item => item.MatchId == _matchId);
                betByQuick050ToolStripMenuItem.Enabled = true;
                betByQuick075ToolStripMenuItem.Enabled = true;
                betByQuick100ToolStripMenuItem.Enabled = true;
            }
        }

        private void switchTo3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataStore.QuickBetMinute == 2)
            {
                DataStore.QuickBetMinute = 1;
            }
            else if (DataStore.QuickBetMinute == 1)
            {
                DataStore.QuickBetMinute = 0;
            }
            else
            {
                DataStore.QuickBetMinute = 2;
            }

            ReloadQuickBet();
        }

        public void ReloadQuickBet()
        {
            switchQuickBetToolStripMenuItem.Text = "Switch To " + DataStore.QuickBetMinute;
            if (DataStore.BetByQuick.Any(item => item.MatchId == _matchId && item.Hdp == 50))
            {
                betByQuick050ToolStripMenuItem.Enabled = false;
            }
            else betByQuick050ToolStripMenuItem.Enabled = true;

            if (DataStore.BetByQuick.Any(item => item.MatchId == _matchId && item.Hdp == 75))
            {
                betByQuick075ToolStripMenuItem.Enabled = false;
            }
            else betByQuick075ToolStripMenuItem.Enabled = true;

            if (DataStore.BetByQuick.Any(item => item.MatchId == _matchId && item.Hdp == 100))
            {
                betByQuick100ToolStripMenuItem.Enabled = false;
            }
            else betByQuick100ToolStripMenuItem.Enabled = true;


            if (DataStore.BetByHdpPrice.Any(item => item.MatchId == _matchId && item.AutoBetMessage == ("OverEnd" + true)))
            {
                ftToolStripMenuItem.Enabled = false;
            }
            else
            {
                ftToolStripMenuItem.Enabled = true;
            }

            if (DataStore.BetByHdpPrice.Any(item => item.MatchId == _matchId && item.AutoBetMessage == ("OverEnd" + false)))
            {
                fhToolStripMenuItem.Enabled = false;
            }
            else
            {
                fhToolStripMenuItem.Enabled = true;
            }
        }

        private void dgvSummaryTableV2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            e.Paint(e.CellBounds, DataGridViewPaintParts.ContentForeground);

            int newColIndex = e.ColumnIndex - 3;
            if (newColIndex % 3 == 1)
            {
                var r = e.CellBounds;
                using (var pen = new Pen(Color.Black))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    pen.Width = 2;
                    e.Graphics.DrawLine(pen, r.Left, r.Top, r.Left, r.Bottom);
                }
            }

            int rowCount = dgvSummaryTableV2.RowCount;
            if (e.RowIndex == rowCount - 4 || e.RowIndex == rowCount - 3)
            {
                var r = e.CellBounds;
                using (var pen = new Pen(Color.Black))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    pen.Width = 2;
                    e.Graphics.DrawLine(pen, r.Left, r.Top, r.Right, r.Top);
                }
            }

            

            //e.Paint(
            //    e.CellBounds,
            //    DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground
            //);
            //var r = e.CellBounds;
            //using (var pen = new Pen(Color.Black))
            //{
            //    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //    pen.Width = 2;
            //    e.Graphics.DrawLine(pen, r.Left, r.Top, r.Right, r.Bottom);
            //}
            //r.Inflate(-1, -1);
            //if (e.ColumnIndex == 0)
            //{
            //    TextBoxRenderer.DrawTextBox(
            //        e.Graphics, 
            //        r, 
            //        $"{e.FormattedValue}",
            //        e.CellStyle.Font, 
            //        System.Windows.Forms.VisualStyles.TextBoxState.Normal
            //    );
            //}
            //else
            //{
            //    e.Paint(e.CellBounds, DataGridViewPaintParts.ContentForeground);
            //}

            //e.Handled = true;
        }

        private void betAfterGoodPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddBetAfterGoodPriceForm form = new AddBetAfterGoodPriceForm(this._matchId);
                form.Show();
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
        }

        private void productHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

        private void priceStepAlertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (PriceStepAlertForm form = new PriceStepAlertForm(this._matchId))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
            
        }

        private void xtraViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this._enableXtraView = !this._enableXtraView;
            UpdateMenuBar();
            LoadMainGrid();
            LoadOverUnderScoreTimesFirstHalfGrid();
        }

        private void xtraViewPriceUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._enableXtraViewPriceUp = !this._enableXtraViewPriceUp;
            UpdateMenuBar();
            LoadMainGrid();
            LoadOverUnderScoreTimesFirstHalfGrid();
        }

        private void xtraViewPriceDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._enableXtraViewPriceDown = !this._enableXtraViewPriceDown;
            UpdateMenuBar();
            LoadMainGrid();
            LoadOverUnderScoreTimesFirstHalfGrid();
        }

        private void betHandicapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddBetByHandicapPriceForm form = new AddBetByHandicapPriceForm(this._matchId);
                form.Show();
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
        }

        private void priceChangeFreqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PriceChangeForm form = new PriceChangeForm(this._matchId);
                form.Show();
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
        }

        private void mainProductHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProductIdsLogForm form = new ProductIdsLogForm(this._matchId);
                form.Show();
            }
            catch
            {

            }
        }

        private void statusByProductTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProductIdHistoryDetailByTypeForm form = new ProductIdHistoryDetailByTypeForm(this._matchId);
                form.Show();
            }
            catch
            {

            }
        }

        private void statusByIUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProductIdHistoryDetailForm form = new ProductIdHistoryDetailForm(this._matchId);
                form.Show();
            }
            catch
            {

            }
        }

        private void inspectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (ProductHistoryInspectorForm form = new ProductHistoryInspectorForm(this._matchId, 0, 0))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        private void priceChangeFreqToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                using (PriceFreqForm form = new PriceFreqForm(this._matchId))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void fhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmQuickStake form = new ConfirmQuickStake(_matchId, 50, DataStore.QuickBetMinute, true, false, this);
            form.ShowDialog();
        }

        private void ftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmQuickStake form = new ConfirmQuickStake(_matchId, 50, DataStore.QuickBetMinute, true, true, this);
            form.ShowDialog();
        }

        private void cancel050ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want to delete?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                DataStore.BetByHdpPrice.RemoveAll(item => item.MatchId == _matchId && item.AutoBetMessage.Contains("OverEnd"));
            }
        }

        private void handicapHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandicapHistory form = new HandicapHistory(_matchId);
            form.ShowDialog();
        }
    }
}
