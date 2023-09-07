using AutoMapper.Execution;
using LiveBetApp.Common;
using LiveBetApp.Forms.Alerts;
using LiveBetApp.Forms.BetAfterGoodPrice;
using LiveBetApp.Forms.BetByHdpClose;
using LiveBetApp.Forms.BetByHdpPrice;
using LiveBetApp.Forms.BetByQuick;
using LiveBetApp.Forms.BetByTimming;
using LiveBetApp.Forms.BetFirstHalfByFulltime;
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

namespace LiveBetApp.Forms
{
    public partial class ProductIdsLogForm : Form
    {
        private long _matchId;
        private Thread _executeThread;
        private List<Color> _colors = new List<Color>() {
            Color.Gold,
            Color.GreenYellow,
            Color.SkyBlue,
            Color.LightGreen,
            Color.LightYellow,
            Color.LightPink,
            Color.DarkGray,
            Color.ForestGreen,
            Color.SteelBlue,
            Color.Orange,
            Color.Lime,
            Color.SeaGreen
        };


        private List<ProductIdLog> ProductIdLogOuFt { get; set; }
        private List<ProductIdLog> ProductIdLogOuFh { get; set; }
        private List<ProductIdLog> ProductIdLogHdpFt { get; set; }
        private List<ProductIdLog> ProductIdLogHdpFh { get; set; }

        public ProductIdsLogForm(long matchId)
        {
            this._matchId = matchId;
            this.ProductIdLogOuFt = new List<ProductIdLog>();
            this.ProductIdLogOuFh = new List<ProductIdLog>();
            InitializeComponent();
            this.dgvHdpFt.DefaultCellStyle = new DataGridViewCellStyle(this.dgvHdpFt.DefaultCellStyle) { Font = new Font(this.dgvHdpFt.DefaultCellStyle.Font.FontFamily, 7.8f) };
            this.dgvHdpFh.DefaultCellStyle = new DataGridViewCellStyle(this.dgvHdpFh.DefaultCellStyle) { Font = new Font(this.dgvHdpFh.DefaultCellStyle.Font.FontFamily, 7.8f) };
            this.dgvOuFt.DefaultCellStyle = new DataGridViewCellStyle(this.dgvOuFt.DefaultCellStyle) { Font = new Font(this.dgvOuFt.DefaultCellStyle.Font.FontFamily, 7.8f) };
            this.dgvOuFh.DefaultCellStyle = new DataGridViewCellStyle(this.dgvOuFh.DefaultCellStyle) { Font = new Font(this.dgvOuFh.DefaultCellStyle.Font.FontFamily, 7.8f) };


            this.StartPosition = FormStartPosition.Manual;
            this.Width = 750;
            this.Height = Screen.PrimaryScreen.Bounds.Height / 100 * 60;
            int startX = Screen.PrimaryScreen.Bounds.Width - this.Width < 0 ? 
                0 : 
                Screen.PrimaryScreen.Bounds.Width - this.Width;

            int startY = Screen.PrimaryScreen.Bounds.Height - this.Height - 30;
            this.Location = new Point(startX, startY);
        }

        private void ProductIdsLogForm_Load(object sender, EventArgs e)
        {
            if (DataStore.Matchs.ContainsKey(_matchId))
            {
                Match match = DataStore.Matchs[_matchId];
                this.Text = "1 - " + match.Home + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")"
                    + " | " + Common.Functions.GetOverUnderMoneyLinesString(_matchId)
                    + " | " + match.League;
            }

            if (DataStore.SemiAutoBetOnly)
            {
                return;
            }

            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        LoadDgvOuFh();
                        LoadDgvOuFt();
                        LoadDgvHdpFt();
                        LoadDgvHdpFh();
                        LoadIdsGrid();
                        LoadIdsGridHt();
                        LoadIdsGridFh();
                        LoadIdsGridHdpFt();
                        LoadIdsGridHdpFh();
                        LoadTrackBeforeLive();
                        //LoadDataHistory();
                        Thread.Sleep(10000);
                        if (DataStore.Matchs.ContainsKey(_matchId))
                        {
                            Match match = DataStore.Matchs[_matchId];
                            if (match.LivePeriod < 0) break;
                        }
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

        private void SetStyleMinute(DataGridView dgv, int minute, bool isColorUpDown)
        {
            if (dgv.Name.ToLower().Contains("hdp"))
            {
                dgv.Columns["HdpS"].Width = 40;
            }
            else
            {
                dgv.Columns["HdpS"].Width = 25;
            }
            
            dgv.Columns["Odds2a100"].Width = 30;
            dgv.Columns["Minute"].Width = 25;
            dgv.Columns["CD"].Width = 40;
            

            //public long OddsId { get; set; }
            //public string HdpS { get; set; }
            //public int Hdp { get; set; }
            //public int Odds1a100 { get; set; }
            //public int Odds2a100 { get; set; }
            //public LiveBetApp.Common.Enums.BetType ProductBetType { get; set; }
            //public int Minute { get; set; }
            //public int LivePeriod { get; set; }
            //public string STT { get; set; }

            List<int> openhdps = new List<int>();
            List<int> preOpenhdps = new List<int>();

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                int m = 0;
                if (int.TryParse(dgv.Rows[i].Cells["Minute"].Value.ToString(), out m))
                {

                    if (m < 0)
                    {
                        int hdp = 0;
                        if (int.TryParse(dgv.Rows[i].Cells["Hdp"].Value.ToString(), out hdp))
                        {
                            preOpenhdps.Add(hdp);
                        }
                    }

                    if (m >= minute)
                    {
                        dgv.Rows[i].Cells["Minute"].Style = new DataGridViewCellStyle(dgv.Rows[i].Cells["Minute"].Style)
                        {
                            Font = new Font(dgv.DefaultCellStyle.Font, FontStyle.Bold),
                            ForeColor = Color.Black
                        };
                    }

                    if (m >= 0 && m <= 1)
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                        int hdp = 0;
                        if (int.TryParse(dgv.Rows[i].Cells["Hdp"].Value.ToString(), out hdp))
                        {
                            openhdps.Add(hdp);
                        }
                    }
                }

                if (dgv.Rows[i].Cells["STT"].Value.ToString().Contains("(-"))
                {
                    dgv.Rows[i].Cells["STT"].Style = new DataGridViewCellStyle(dgv.Rows[i].Cells["STT"].Style)
                    {
                        Font = new Font(dgv.DefaultCellStyle.Font, FontStyle.Bold),
                        ForeColor = Color.Red
                    };
                }

                if (dgv.Rows[i].Cells["Odds2a100"].Value.ToString().Contains("-"))
                {
                    dgv.Rows[i].Cells["Odds2a100"].Style = new DataGridViewCellStyle(dgv.Rows[i].Cells["Odds2a100"].Style)
                    {
                        Font = new Font(dgv.DefaultCellStyle.Font, FontStyle.Bold),
                        ForeColor = Color.Red
                    };
                }
            }

            

            if (isColorUpDown && openhdps.Count > 0 && preOpenhdps.Count > 0)
            {
                int maxPreOpenhdps = preOpenhdps.Max();
                int minPreOpenhdps = preOpenhdps.Min();
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    int m = 0;
                    int hdp = 0;
                    if (int.TryParse(dgv.Rows[i].Cells["Minute"].Value.ToString(), out m)
                        && int.TryParse(dgv.Rows[i].Cells["Hdp"].Value.ToString(), out hdp))
                    {
                        if (m >= 0 && m <= 1)
                        { 
                            if (hdp > maxPreOpenhdps)
                            {
                                dgv.Rows[i].Cells["HdpS"].Style.BackColor = Color.Green;
                            }
                            if (hdp < minPreOpenhdps)
                            {
                                dgv.Rows[i].Cells["HdpS"].Style.BackColor = Color.Orange;
                            }
                        }
                    }
                }

            }

        }

        private delegate void dlgLoadIdsGridHt();
        private void LoadIdsGridHt()
        {
            if (this.dataGridViewHt.InvokeRequired)
            {
                this.Invoke(new dlgLoadIdsGridHt(LoadIdsGridHt));
            }
            else
            {
                int columnCount = 31;
                this.dataGridViewHt.Rows.Clear();
                this.dataGridViewHt.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    this.dataGridViewHt.Columns[i].Width = 15;
                }
                this.dataGridViewHt.Columns[0].Width = 20;

                ArrayList header = new ArrayList() { "" };
                for (int i = 1; i <= columnCount; i++)
                {
                    header.Add(i.ToString());
                }

                this.dataGridViewHt.Rows.Add(header.ToArray());

                Dictionary<int, List<OverUnderScoreTimesV3Item>> data;

                if (!DataStore.OverUnderScoreHalftime.TryGetValue(_matchId, out data)) return;
                List<int> scores = data.Keys.OrderBy(item => item).ToList();
                if (scores.Count == 0) return;

                List<ProductIdLog> products = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                    .OrderBy(item => item.Minute)
                    .ToList();

                for (int i = scores.Count - 1; i >= 0; i--)
                {
                    List<string> row = new List<string>();
                    List<string> rowData = data[scores[i]].Select(item => item.OddsId.ToString()).ToList();

                    for (int j = 0; j <= 30; j++)
                    {
                        if (rowData[j] != "0")
                        {
                            rowData[j] = Common.Functions.IndexOfProduct(products, rowData[j]);

                        }
                        else
                        {
                            rowData[j] = "";
                        }
                    }
                    row.Add(scores[i].ToString());
                    row.AddRange(rowData);
                    this.dataGridViewHt.Rows.Add(row.ToArray());


                }


                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(236, 236, 236);
                    }
                }

                for (int i = 1; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 1; j < dataGridView.ColumnCount; j++)
                    {
                        int cellValue = 0;
                        object cellContent = dataGridView.Rows[i].Cells[j].Value;
                        if (cellContent != null && cellContent.ToString().Length > 0 && cellContent.ToString() != "0")
                        {
                            try
                            {
                                if (int.TryParse(cellContent.ToString(), out cellValue)
                                    && ProductIdLogOuFt[cellValue - 1].Minute >= 2)
                                {
                                    int colorIndex = (cellValue % _colors.Count);
                                    dataGridView.Rows[i].Cells[j].Style.BackColor = _colors[colorIndex];
                                }
                            }
                            catch
                            {

                            }
                        }

                    }
                }

            }
        }

        //private delegate void dlgLoadDataHistory();
        //private void LoadDataHistory()
        //{
        //    if (this.dgvStatusHistory.InvokeRequired)
        //    {
        //        this.Invoke(new dlgLoadDataHistory(LoadDataHistory));
        //    }
        //    else
        //    {
        //        if (!DataStore.ProductStatus.ContainsKey(_matchId)) return;
        //        this.dgvStatusHistory.Rows.Clear();
        //        List<List<ProductStatusLog>> data = DataStore.ProductStatus[_matchId];
        //        List<string> headerRow = new List<string>() { "" };
        //        List<string> dataRowRunning = new List<string>() { "running" };
        //        List<string> dataRowSuspend = new List<string>() { "suspend" };
        //        List<string> dataRowClosePrice = new List<string>() { "closePrice" };


        //        this.dgvStatusHistory.ColumnCount = 104;
        //        this.dgvStatusHistory.Columns[0].Width = 20;
        //        this.dgvStatusHistory.Columns[103].Width = 221;
        //        for (int i = 1; i <= 102; i++)
        //        {
        //            int realMinute = 0;
        //            if (i<=46)
        //            {
        //                realMinute = i - 1;
        //                headerRow.Add((i - 1).ToString());
        //            }
        //            else if (i == 47)
        //            {
        //                realMinute = 101;
        //                headerRow.Add("ht");
        //            }
        //            else if (i > 47)
        //            {
        //                realMinute = i - 2;
        //                headerRow.Add((i - 47).ToString());
        //            }


        //            int countRunning = data[realMinute].Count(item => item.Status == "running");
        //            int countSuspend = data[realMinute].Count(item => item.Status == "suspend");
        //            int countClosePrice = data[realMinute].Count(item => item.Status == "closePrice");


        //            dataRowRunning.Add(countRunning > 0 ? countRunning.ToString() : "");
        //            dataRowSuspend.Add(countSuspend > 0 ? countSuspend.ToString() : "");
        //            dataRowClosePrice.Add(countClosePrice > 0 ? countClosePrice.ToString() : "");


        //            this.dgvStatusHistory.Columns[i].Width = 15;
        //        }
        //        this.dgvStatusHistory.Rows.Add(headerRow.ToArray());
        //        this.dgvStatusHistory.Rows.Add(dataRowRunning.ToArray());
        //        this.dgvStatusHistory.Rows.Add(dataRowSuspend.ToArray());
        //        this.dgvStatusHistory.Rows.Add(dataRowClosePrice.ToArray());
        //        Match match = DataStore.Matchs[_matchId];
        //        int currennMinute100 = Common.Functions.GetCurrentMinute100(match);
        //        if (currennMinute100 > 0 && currennMinute100 <= 100)
        //        {
        //            for (int row = 0; row < this.dgvStatusHistory.Rows.Count; row++)
        //            {
        //                this.dgvStatusHistory.Rows[row].Cells[currennMinute100].Style.BackColor = Color.Gray;
        //            }
        //        }
        //        Common.Functions.FreezeBand(this.dgvStatusHistory.Columns[0]);
        //    }


        //}


        private delegate void dlgLoadIdsGrid();
        private void LoadIdsGrid()
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgLoadIdsGrid(LoadIdsGrid));
            }
            else
            {
                int columnCount = 48;
                this.dataGridView.Rows.Clear();
                this.dataGridView.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    this.dataGridView.Columns[i].Width = 15;
                }
                this.dataGridView.Columns[0].Width = 20;
                
                ArrayList header = new ArrayList() { "" };
                for (int i = 0; i <= columnCount; i++)
                {
                    header.Add(i.ToString());
                }
                header[47] = "ht-02";
                this.dataGridView.Rows.Add(header.ToArray());

                Dictionary<int, List<OverUnderScoreTimesV2Item>> data;

                if (!DataStore.OverUnderScoreTimesV3.TryGetValue(_matchId, out data)) return;
                List<int> scores = data.Keys.OrderBy(item => item).ToList();
                if (scores.Count == 0) return;

                List<ProductIdLog> products = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                    .OrderBy(item => item.Minute)
                    .ToList();

                

                for (int i = scores.Count - 1; i >= 0; i--)
                {
                    List<string> row = new List<string>();
                    List<string> rowData = data[scores[i]].Select(item => item.OddsId.ToString()).ToList();
                    for (int j=46; j<=90; j++)
                    {
                        if (rowData[j] != "0")
                        {
                            rowData[j - 45] = rowData[j];
                        }
                    }

                    for (int j=0; j<=45; j++)
                    {
                        if (rowData[j] != "0")
                        {
                            rowData[j] = Common.Functions.IndexOfProduct(products, rowData[j]);
                        }
                        else
                        {
                            rowData[j] = "";
                        }
                    }


                    if (DataStore.OverUnderScoreHalftime.ContainsKey(_matchId)
                        && DataStore.OverUnderScoreHalftime[_matchId].ContainsKey(scores[i]))
                    {
                        string idIndex = Common.Functions.IndexOfProduct(products, DataStore.OverUnderScoreHalftime[_matchId][scores[i]][2].OddsId.ToString());
                        rowData[46] = idIndex;
                    }
                    else
                    {
                        rowData[46] = "";
                    }

                    row.Add(scores[i].ToString());
                    row.AddRange(rowData);
                    this.dataGridView.Rows.Add(row.ToArray());
                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(236,236,236);
                    }
                }

                for (int i = 1; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 1; j < dataGridView.ColumnCount; j++)
                    {
                        int cellValue = 0;
                        object cellContent = dataGridView.Rows[i].Cells[j].Value;
                        if (cellContent != null && cellContent.ToString().Length > 0 && cellContent.ToString() != "0")
                        {
                            try
                            {
                                if (int.TryParse(cellContent.ToString(), out cellValue)
                                    && ProductIdLogOuFt[cellValue - 1].Minute >= 2)
                                {
                                    int colorIndex = (cellValue % _colors.Count);
                                    dataGridView.Rows[i].Cells[j].Style.BackColor = _colors[colorIndex];
                                }
                            }
                            catch
                            {

                            }
                        }

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

                        if (goalHistories[i].LivePeriod == 1)
                        {
                            this.dataGridView.Rows[0].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                        }
                        else if (goalHistories[i].LivePeriod == 2)
                        {
                            if (this.dataGridView.Rows[0].Cells[column].Style.BackColor == System.Drawing.Color.Red)
                            {
                                this.dataGridView.Rows[0].Cells[column].Style.BackColor = System.Drawing.Color.Orange;
                            }
                            else
                            {
                                this.dataGridView.Rows[0].Cells[column].Style.BackColor = System.Drawing.Color.Yellow;
                            }
                        }


                        for (int j = scores.Count - 1; j >= 0; j--)
                        {
                            List<string> row = new List<string>();
                            List<string> rowData = data[scores[j]].Select(item => item.OddsId.ToString()).ToList();
                            for (int k = 46; k <= 90; k++)
                            {
                                if (rowData[k] != "0")
                                {
                                    rowData[k - 45] = rowData[k];
                                }
                            }

                            if (goalHistories[i].ProductOuId.Any(item => item.ToString() == rowData[column].ToString()))
                            {
                                for (int rowIndex = 0; rowIndex < this.dataGridView.Rows.Count; rowIndex++)
                                {
                                    if (this.dataGridView.Rows[rowIndex].Cells[0].Value.ToString() == scores[j].ToString())
                                    {
                                        this.dataGridView.Rows[rowIndex].Cells[column].Style = new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold), BackColor = this.dataGridView.Rows[rowIndex].Cells[column].Style.BackColor };
                                        //new DataGridViewCellStyle(this.dataGridView.DefaultCellStyle) { Font = new Font(this.dataGridView.DefaultCellStyle.Font.FontFamily, 7.8f, FontStyle.Strikeout), BackColor = this.dataGridView.Rows[rowIndex].Cells[column].Style.BackColor};
                                    }
                                }
                                    
                                
                            }
                        }



                    }
                }

            }
        }

        private delegate void dlgLoadIdsGridFh();
        private void LoadIdsGridFh()
        {
            if (this.dgvFhOu.InvokeRequired)
            {
                this.Invoke(new dlgLoadIdsGridFh(LoadIdsGridFh));
            }
            else
            {
                int columnCount = 47;
                this.dgvFhOu.Rows.Clear();
                this.dgvFhOu.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    this.dgvFhOu.Columns[i].Width = 15;
                }
                this.dgvFhOu.Columns[0].Width = 20;

                ArrayList header = new ArrayList() { "" };
                for (int i = 0; i <= columnCount; i++)
                {
                    header.Add(i.ToString());
                }

                this.dgvFhOu.Rows.Add(header.ToArray());

                Dictionary<int, List<OverUnderScoreTimesV2Item>> data;

                if (!DataStore.OverUnderScoreTimesFirstHalfV3.TryGetValue(_matchId, out data)) return;
                List<int> scores = data.Keys.OrderBy(item => item).ToList();
                if (scores.Count == 0) return;

                List<ProductIdLog> products = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder)
                    .OrderBy(item => item.Minute)
                    .ToList();

                for (int i = scores.Count - 1; i >= 0; i--)
                {
                    List<string> row = new List<string>();
                    List<string> rowData = data[scores[i]].Select(item => item.OddsId.ToString()).ToList();
                    for (int j = 46; j <= 90; j++)
                    {
                        if (rowData[j] != "0")
                        {
                            rowData[j - 45] = rowData[j];
                        }
                    }

                    for (int j = 0; j <= 45; j++)
                    {
                        if (rowData[j] != "0")
                        {
                            rowData[j] = Common.Functions.IndexOfProduct(products, rowData[j]);
                        }
                        else
                        {
                            rowData[j] = "";
                        }
                    }

                    row.Add(scores[i].ToString());
                    row.AddRange(rowData);
                    this.dgvFhOu.Rows.Add(row.ToArray());
                }

                for (int i = 0; i < dgvFhOu.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        dgvFhOu.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(236, 236, 236);
                    }
                }

                for (int i = 1; i < dgvFhOu.Rows.Count; i++)
                {
                    for (int j = 1; j < dgvFhOu.ColumnCount; j++)
                    {
                        int cellValue = 0;
                        object cellContent = dgvFhOu.Rows[i].Cells[j].Value;
                        if (cellContent != null && cellContent.ToString().Length > 0 && cellContent.ToString() != "0")
                        {
                            try
                            {
                                if (int.TryParse(cellContent.ToString(), out cellValue) && ProductIdLogOuFh[cellValue - 1].Minute >= 2)
                                {
                                    int colorIndex = (cellValue % _colors.Count);
                                    dgvFhOu.Rows[i].Cells[j].Style.BackColor = _colors[colorIndex];
                                }
                            }
                            catch
                            {

                            }

                        }

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

                        if (goalHistories[i].LivePeriod == 1)
                        {
                            this.dgvFhOu.Rows[0].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                        }
                    }
                }

            }
        }

        private delegate void dlgLoadTrackBeforeLive();
        private void LoadTrackBeforeLive()
        {
            if (this.dataGridViewTrackBeforeLive.InvokeRequired)
            {
                this.Invoke(new dlgLoadTrackBeforeLive(LoadTrackBeforeLive));
            }
            else
            {
                if (!DataStore.TrackOverUnderBeforeLive.ContainsKey(_matchId)) return;
                List<TrackOverUnderBeforeLiveItem> data = DataStore.TrackOverUnderBeforeLive[_matchId];
                List<int> availableHdps = new List<int>();
                List<string> header = new List<string>() { "" };
                for (int i = 0; i < data.Count; i++)
                {
                    header.Add(data[i].MinuteBeforeLive.ToString());
                    for (int j=0; j<data[i].Hdps_100.Count; j++)
                    {
                        int hdp = data[i].Hdps_100[j].Key;
                        if (!availableHdps.Contains(hdp))
                        {
                            availableHdps.Add(hdp);
                        }
                    }
                }

                int columnCount = header.Count;
                this.dataGridViewTrackBeforeLive.Rows.Clear();
                this.dataGridViewTrackBeforeLive.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    this.dataGridViewTrackBeforeLive.Columns[i].Width = 30;
                }
                this.dataGridViewTrackBeforeLive.Columns[0].Width = 20;


                this.dataGridViewTrackBeforeLive.Rows.Add(header.ToArray());
                List<List<string>> dgvData = new List<List<string>>();

                List<ProductIdLog> products = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                    .OrderBy(item => item.Minute)
                    .ToList();

                for (int i=0; i<availableHdps.Count; i++)
                {
                    List<string> row = new List<string>();
                    int hdp = availableHdps[i];
                    row.Add(hdp.ToString());
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (data[j].Hdps_100.Any(item => item.Key == hdp))
                        {
                            KeyValuePair<int, long> product = data[j].Hdps_100.FirstOrDefault(item => item.Key == hdp);
                            row.Add(Common.Functions.IndexOfProduct(products, product.Value.ToString()).ToString());
                        }
                        else
                        {
                            row.Add("");
                        }
                    }
                    this.dataGridViewTrackBeforeLive.Rows.Add(row.ToArray());
                }
            }
            if (this.dataGridViewTrackBeforeLive.Columns.Count >= 2)
            {
                this.dataGridViewTrackBeforeLive.Columns[1].Visible = false;
            }
            
        }

        private delegate void dlgLoadIdsGridHdpFh();
        private void LoadIdsGridHdpFh()
        {
            if (this.dgvFhHdp.InvokeRequired)
            {
                this.Invoke(new dlgLoadIdsGridHdpFh(LoadIdsGridHdpFh));
            }
            else
            {
                //dgvFhHdp
                int columnCount = 47;
                this.dgvFhHdp.Rows.Clear();
                this.dgvFhHdp.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    this.dgvFhHdp.Columns[i].Width = 15;
                }
                this.dgvFhHdp.Columns[0].Width = 20;

                ArrayList header = new ArrayList() { "" };
                for (int i = 0; i <= columnCount; i++)
                {
                    header.Add(i.ToString());
                }

                this.dgvFhHdp.Rows.Add(header.ToArray());

                //Dictionary<int, List<OverUnderScoreTimesV2Item>> data;
                Dictionary<long, List<HandicapLifeTimeHistory>> data;

                if (!DataStore.HandicapFhScoreTimes.TryGetValue(_matchId, out data)) return;

                List<int> scores = new List<int>();
                Dictionary<int, List<long>> viewData = new Dictionary<int, List<long>>();
                List<long> keys = data.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    List<HandicapLifeTimeHistory> row = data[keys[i]];
                    for (int j = 0; j < row.Count; j++)
                    {
                        if (row[j] != null &&
                            !scores.Contains(row[j].Hdp_100))
                        {
                            scores.Add(row[j].Hdp_100);
                        }
                    }
                }



                if (scores.Count == 0) return;

                scores = scores.OrderBy(item => item).ToList();


                for (int i = 0; i < scores.Count; i++)
                {
                    List<long> row = new List<long>();
                    for (int j = 0; j <= 45; j++)
                    {
                        row.Add(0);
                    }
                    viewData.Add(scores[i], row);
                }

                for (int i = 0; i < keys.Count; i++)
                {
                    List<HandicapLifeTimeHistory> row = data[keys[i]];
                    for (int j = 0; j < row.Count; j++)
                    {
                        if (row[j] != null)
                        {
                            viewData[row[j].Hdp_100][j] = row[j].OddsId;
                        }
                    }
                }

                List<ProductIdLog> products = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FirstHalfHandicap)
                    .OrderBy(item => item.Minute)
                    .ToList();

                for (int i = 0; i < scores.Count; i++)
                {
                    List<string> row = new List<string>();
                    List<string> rowData = viewData[scores[i]].Select(item => item.ToString()).ToList();


                    for (int j = 0; j <= 45; j++)
                    {
                        if (rowData[j] != "0")
                        {
                            rowData[j] = Common.Functions.IndexOfProduct(products, rowData[j]);
                        }
                        else
                        {
                            rowData[j] = "";
                        }
                    }


                    row.Add(scores[i].ToString());
                    row.AddRange(rowData);
                    this.dgvFhHdp.Rows.Add(row.ToArray());
                }


                List<GoalHistory> goalHistories;
                if (DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories))
                {
                    int rowCount = this.dgvFhHdp.Rows.Count;
                    for (int i = 0; i < goalHistories.Count; i++)
                    {
                        if (goalHistories[i].LivePeriod != 1) continue;
                        int column = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes + 1;
                        if (column > 45) column = 45;

                        if (goalHistories[i].LivePeriod == 1)
                        {
                            this.dgvFhHdp.Rows[0].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                        }
                    }
                }

                Common.Functions.FreezeBand(this.dgvFhHdp.Columns[0]);
                for (int i = 0; i < dgvFhHdp.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        dgvFhHdp.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(236, 236, 236);
                    }
                }


                for (int i = 1; i < dgvFhHdp.Rows.Count; i++)
                {
                    for (int j = 1; j < dgvFhHdp.ColumnCount; j++)
                    {
                        int cellValue = 0;
                        object cellContent = dgvFhHdp.Rows[i].Cells[j].Value;
                        if (cellContent != null && cellContent.ToString().Length > 0 && cellContent.ToString() != "0")
                        {
                            try
                            {
                                if (int.TryParse(cellContent.ToString(), out cellValue)
                                    && ProductIdLogHdpFh[cellValue - 1].Minute >= 2)
                                {
                                    int colorIndex = (cellValue % _colors.Count);
                                    dgvFhHdp.Rows[i].Cells[j].Style.BackColor = _colors[colorIndex];
                                }
                            }
                            catch
                            {

                            }
                        }

                    }
                }

            }
        }

        private delegate void dlgLoadIdsGridHdpFt();
        private void LoadIdsGridHdpFt()
        {
            if (this.dgvFh.InvokeRequired)
            {
                this.Invoke(new dlgLoadIdsGridHdpFt(LoadIdsGridHdpFt));
            }
            else
            {
                int columnCount = 102;
                this.dgvFh.Rows.Clear();
                this.dgvFh.ColumnCount = columnCount + 1;
                for (int i = 0; i < columnCount; i++)
                {
                    this.dgvFh.Columns[i].Width = 15;
                }
                this.dgvFh.Columns[0].Width = 20;
                this.dgvFh.Columns[columnCount].Width = 222;

                ArrayList header = new ArrayList() { "" };
                for (int i = 0; i < columnCount-1; i++)
                {
                    header.Add(i.ToString());
                }

                this.dgvFh.Rows.Add(header.ToArray());

                //Dictionary<int, List<OverUnderScoreTimesV2Item>> data;
                Dictionary<long, List<HandicapLifeTimeHistory>> data;

                if (!DataStore.HandicapScoreTimes.TryGetValue(_matchId, out data)) return;

                List<int> scores = new List<int>();
                Dictionary<int, List<long>> viewData = new Dictionary<int, List<long>>();
                List<long> keys = data.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    List<HandicapLifeTimeHistory> row = data[keys[i]];
                    for (int j = 0; j < row.Count; j++)
                    {
                        if (row[j] != null &&
                            !scores.Contains(row[j].Hdp_100))
                        {
                            scores.Add(row[j].Hdp_100);
                        }
                    }
                }



                if (scores.Count == 0) return;

                scores = scores.OrderBy(item => item).ToList();


                for (int i = 0; i < scores.Count; i++)
                {
                    List<long> row = new List<long>();
                    for (int j = 0; j <= 100; j++ )
                    {
                        row.Add(0);
                    }
                    viewData.Add(scores[i], row);
                }

                for (int i = 0; i < keys.Count; i++)
                {
                    List<HandicapLifeTimeHistory> row = data[keys[i]];
                    for (int j = 0; j < row.Count; j++)
                    {
                        if (row[j] != null)
                        {
                            viewData[row[j].Hdp_100][j] = row[j].OddsId;
                        }
                    }
                }

                List<ProductIdLog> products = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeHandicap)
                    .OrderBy(item => item.Minute)
                    .ToList();

                for (int i = 0; i < scores.Count; i++)
                {
                    List<string> row = new List<string>();
                    List<string> rowData = viewData[scores[i]].Select(item => item.ToString()).ToList();


                    for (int j = 0; j <= 100; j++)
                    {
                        if (rowData[j] != "0")
                        {
                            rowData[j] = Common.Functions.IndexOfProduct(products, rowData[j]);
                        }
                        else
                        {
                            rowData[j] = "";
                        }
                    }


                    row.Add(scores[i].ToString());
                    row.AddRange(rowData);
                    this.dgvFh.Rows.Add(row.ToArray());
                }


                List<GoalHistory> goalHistories;
                if (DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories))
                {
                    int rowCount = this.dgvFh.Rows.Count;
                    for (int i = 0; i < goalHistories.Count; i++)
                    {
                        int column = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes + 1;
                        if (column > 55) column = 55;

                        if (goalHistories[i].LivePeriod == 1)
                        {
                            this.dgvFh.Rows[0].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                        }
                        else if (goalHistories[i].LivePeriod == 2)
                        {
                            this.dgvFh.Rows[0].Cells[column + 45].Style.BackColor = System.Drawing.Color.Yellow;
                        }
                    }
                }

                Common.Functions.FreezeBand(this.dgvFh.Columns[0]);
                for (int i = 0; i < dgvFh.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        dgvFh.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(236, 236, 236);
                    }
                }


                for (int i = 1; i < dgvFh.Rows.Count; i++)
                {
                    for (int j = 1; j < dgvFh.ColumnCount; j++)
                    {
                        int cellValue = 0;
                        object cellContent = dgvFh.Rows[i].Cells[j].Value;
                        if (cellContent != null && cellContent.ToString().Length > 0 && cellContent.ToString() != "0")
                        {
                            try
                            {
                                if (int.TryParse(cellContent.ToString(), out cellValue)
                                    && ProductIdLogHdpFt[cellValue - 1].Minute >= 2)
                                {
                                    int colorIndex = (cellValue % _colors.Count);
                                    dgvFh.Rows[i].Cells[j].Style.BackColor = _colors[colorIndex];
                                }
                            }
                            catch
                            {

                            }
                        }
                    }

                    for (int j = 1; j < dgvFh.ColumnCount - 1; j++)
                    {
                        int currentCellValue = 0;
                        object currentCellContent = dgvFh.Rows[i].Cells[j].Value;

                        int nextCellValue = 0;
                        object nextCellContent = dgvFh.Rows[i].Cells[j + 1].Value;

                        if (currentCellContent != null && nextCellContent != null)
                        {
                            if (int.TryParse(currentCellContent.ToString(), out currentCellValue)
                                && int.TryParse(nextCellContent.ToString(), out nextCellValue)
                                && currentCellValue != 0 
                                && nextCellValue != 0
                                && currentCellValue != nextCellValue)
                            {
                                dgvFh.Rows[0].Cells[j].Style = new DataGridViewCellStyle(dgvFh.Rows[0].Cells[j].Style) { 
                                    Font = new Font(dgvFh.DefaultCellStyle.Font, FontStyle.Bold), 
                                    BackColor = Color.Blue
                                };
                            }
                        }

                    }

                }

            }
        }



        private delegate void dlgLoadDgvOuFh();
        private void LoadDgvOuFh()
        {
            if (dgvOuFh.InvokeRequired)
            {
                this.Invoke(new dlgLoadDgvOuFh(LoadDgvOuFh));
                return;
            }
            if (DataStore.ProductIdsOfMatch.ContainsKey(_matchId))
            {
                List<ProductIdLog> data = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FirstHalfOverUnder)
                    .OrderBy(item => item.Minute)
                    .ToList();

                for (int i=0; i<data.Count; i++)
                {
                    if (data[i].Minute < 0)
                    {
                        data[i].STT = (i + 1) + " (" + data[i].Odds1a100.ToString() + ")";
                    }
                    else
                    {
                        data[i].STT = (i + 1).ToString();
                    }
                }
                ProductIdLogOuFh = data;
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = Common.Functions.ToDataTable(data);
                dgvOuFh.Columns.Clear();
                dgvOuFh.DataSource = bindingSource;
                dgvOuFh.Columns["ProductBetType"].Visible = false;
                dgvOuFh.Columns["OddsId"].Visible = false;
                dgvOuFh.Columns["Hdp"].Visible = false;
                dgvOuFh.Columns["LivePeriod"].Visible = false;
                dgvOuFh.Columns["Odds1a100"].Visible = false;
                dgvOuFh.Columns["Odds2a100"].HeaderText = "OP";
                dgvOuFh.Columns["CreateDatetime"].Visible = false;
                SetStyleMinute(dgvOuFh, 3, true);
            }
        }

        private delegate void dlgLoadDgvOuFt();
        private void LoadDgvOuFt()
        {
            if (dgvOuFt.InvokeRequired)
            {
                this.Invoke(new dlgLoadDgvOuFt(LoadDgvOuFt));
                return;
            }
            if (DataStore.ProductIdsOfMatch.ContainsKey(_matchId))
            {
                List<ProductIdLog> data = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeOverUnder)
                    .OrderBy(item => item.Minute)
                    .ToList();
                BindingSource bindingSource = new BindingSource();

                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].Minute < 0)
                    {
                        data[i].STT = (i + 1) + " (" + data[i].Odds1a100.ToString() + ")";
                    }
                    else
                    {
                        data[i].STT = (i + 1).ToString();
                    }
                    
                }

                ProductIdLogOuFt = data;

                bindingSource.DataSource = Common.Functions.ToDataTable(data);
                dgvOuFt.Columns.Clear();
                dgvOuFt.DataSource = bindingSource;
                dgvOuFt.Columns["ProductBetType"].Visible = false;
                dgvOuFt.Columns["LivePeriod"].Visible = false;
                dgvOuFt.Columns["Hdp"].Visible = false;
                dgvOuFt.Columns["OddsId"].Visible = false;
                dgvOuFt.Columns["Odds1a100"].Visible = false;
                dgvOuFt.Columns["Odds2a100"].HeaderText = "OP";
                dgvOuFt.Columns["CreateDatetime"].Visible = false;
                SetStyleMinute(dgvOuFt, 3, true);
            }
        }

        private delegate void dlgLoadDgvHdpFt();
        private void LoadDgvHdpFt()
        {
            if (dgvHdpFt.InvokeRequired)
            {
                this.Invoke(new dlgLoadDgvHdpFt(LoadDgvHdpFt));
                return;
            }
            if (DataStore.ProductIdsOfMatch.ContainsKey(_matchId))
            {
                List<ProductIdLog> data = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FullTimeHandicap)
                    .OrderBy(item => item.Minute)
                    .ToList();
                BindingSource bindingSource = new BindingSource();

                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].Minute < 0)
                    {
                        data[i].STT = (i + 1) + " - " + data[i].HdpS.ToString() + " (" + data[i].Odds1a100.ToString() + ")";
                    }
                    else
                    {
                        data[i].STT = (i + 1).ToString();
                    }
                }


                ProductIdLogHdpFt = data;

                bindingSource.DataSource = Common.Functions.ToDataTable(data);
                dgvHdpFt.Columns.Clear();
                dgvHdpFt.DataSource = bindingSource;
                dgvHdpFt.Columns["ProductBetType"].Visible = false;
                dgvHdpFt.Columns["LivePeriod"].Visible = false;
                dgvHdpFt.Columns["Hdp"].Visible = false;
                dgvHdpFt.Columns["OddsId"].Visible = false;
                dgvHdpFt.Columns["Odds1a100"].Visible = false;
                dgvHdpFt.Columns["Odds2a100"].HeaderText = "OP";
                dgvHdpFt.Columns["CreateDatetime"].Visible = false;
                SetStyleMinute(dgvHdpFt, 3, true);
            }
        }

        private delegate void dlgLoadDgvHdpFh();
        private void LoadDgvHdpFh()
        {
            if (dgvHdpFh.InvokeRequired)
            {
                this.Invoke(new dlgLoadDgvHdpFh(LoadDgvHdpFh));
                return;
            }
            if (DataStore.ProductIdsOfMatch.ContainsKey(_matchId))
            {
                List<ProductIdLog> data = DataStore.ProductIdsOfMatch[_matchId]
                    .Where(item => item.ProductBetType == Enums.BetType.FirstHalfHandicap)
                    .OrderBy(item => item.Minute)
                    .ToList();
                BindingSource bindingSource = new BindingSource();

                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].Minute < 0)
                    {
                        data[i].STT = (i + 1) + " - " + data[i].HdpS.ToString() + " (" + data[i].Odds1a100.ToString() + ")";
                    }
                    else
                    {
                        data[i].STT = (i + 1).ToString();
                    }
                }

                ProductIdLogHdpFh = data;

                bindingSource.DataSource = Common.Functions.ToDataTable(data);
                dgvHdpFh.Columns.Clear();
                dgvHdpFh.DataSource = bindingSource;
                dgvHdpFh.Columns["ProductBetType"].Visible = false;
                dgvHdpFh.Columns["LivePeriod"].Visible = false;
                dgvHdpFh.Columns["OddsId"].Visible = false;
                dgvHdpFh.Columns["Hdp"].Visible = false;
                dgvHdpFh.Columns["Odds1a100"].Visible = false;
                dgvHdpFh.Columns["Odds2a100"].HeaderText = "OP";
                dgvHdpFh.Columns["CreateDatetime"].Visible = false;
                SetStyleMinute(dgvHdpFh, 3, true);
            }
        }

        private void ProductIdsLogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }
    }
}
