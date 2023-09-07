using LiveBetApp.Models.DataModels;
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
    public partial class ProductIdHistoryDetailByTypeForm : Form
    {
        private long _matchId;
        private Thread _executeThread;
        private Thread _executeThreadCheckNewAction;
        public ProductIdHistoryDetailByTypeForm(long matchId)
        {
            _matchId = matchId;
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Width = Screen.PrimaryScreen.Bounds.Width - 750 - 5;
            this.Height = Screen.PrimaryScreen.Bounds.Height / 2;
            int startX = Screen.PrimaryScreen.Bounds.Width - this.Width < 0 ?
                0 :
                Screen.PrimaryScreen.Bounds.Width - this.Width;

            int startY = Screen.PrimaryScreen.Bounds.Height - this.Height - 30;
            this.Location = new Point(0, startY);
            SetAlertNewActionText();
        }

        private void ProductIdHistoryDetailByTypeForm_Load(object sender, EventArgs e)
        {
            if (DataStore.Matchs.ContainsKey(_matchId))
            {
                Match match = DataStore.Matchs[_matchId];
                this.Text = "2 - " + match.Home + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")"
                    + " | " + Common.Functions.GetOverUnderMoneyLinesString(_matchId)
                    + " | " + match.League
                    + " | Status by Product Type";
                if (match.LivePeriod == 2)
                {
                    LoadSplitContainer(2);
                }
                else
                {
                    LoadSplitContainer(1);
                }
            }

            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        LoadDataHistoryFh(this.dgvFh);
                        LoadDataHistoryFt(this.dgvFtH1, 1);
                        LoadDataHistoryFt(this.dgvFtH2, 2);

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

            _executeThreadCheckNewAction = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (DataStore.Matchs.ContainsKey(_matchId))  
                        {
                            Match match = DataStore.Matchs[_matchId];
                            int totalSec = (int)DateTime.Now.Subtract(match.LastimeHasNewProductAction).TotalSeconds;
                            if (match.AlertNewProductAction && totalSec <= 60)
                            {
                                Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alarm-26718.wav");
                            }
                            if (match.LivePeriod < 0) break;
                        }
                        Thread.Sleep(60000);
                    }
                    catch 
                    {
                        Thread.Sleep(60000);
                    }
                }
            });
            _executeThreadCheckNewAction.IsBackground = true;
            _executeThreadCheckNewAction.Start();

        }

        private delegate void dlgLoadDataHistoryFh(DataGridView dgv);
        private void LoadDataHistoryFh(DataGridView dgv)
        {
            if (dgv.InvokeRequired)
            {
                this.Invoke(new dlgLoadDataHistoryFh(LoadDataHistoryFh), dgv);
            }
            else
            {
                if (!DataStore.ProductStatus.ContainsKey(_matchId)) return;
                dgv.Rows.Clear();

                LoadData(dgv, Common.Enums.BetType.FirstHalfHandicap, 1);
                LoadData(dgv, Common.Enums.BetType.FirstHalfOverUnder, 1);
                LoadData(dgv, Common.Enums.BetType.FirstHalf1x2, 1);

                Match match = DataStore.Matchs[_matchId];
                int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
                if (currentMinute > 101) currentMinute = 100;
                if (currentMinute < 0) currentMinute = 0;

                if (currentMinute > 0 && currentMinute <= 100)
                {
                    for (int row = 0; row < dgv.Rows.Count; row++)
                    {
                        dgv.Rows[row].Cells[currentMinute].Style.BackColor = Color.Gray;
                    }
                }

                //List<GoalHistory> goalHistories;
                //if (DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories))
                //{
                //    int rowCount = dgv.Rows.Count;
                //    for (int i = 0; i < goalHistories.Count; i++)
                //    {
                //        int column = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes;
                //        if (column > 45) column = 45;

                //        if (goalHistories[i].LivePeriod == 1)
                //        {
                //            dgv.Rows[0].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                //        }
                //        else if (goalHistories[i].LivePeriod == 2)
                //        {
                //            column += 46;
                //            if (dgv.Rows[0].Cells[column].Style.BackColor == System.Drawing.Color.Red)
                //            {
                //                dgv.Rows[0].Cells[column].Style.BackColor = System.Drawing.Color.Orange;
                //            }
                //            else
                //            {
                //                dgv.Rows[0].Cells[column].Style.BackColor = System.Drawing.Color.Yellow;
                //            }
                //        }
                //    }
                //}

                for (int row = 1; row < dgv.Rows.Count; row++)
                {
                    if (row % 4 == 0) continue;
                    for (int column = 1; column < dgv.Rows[row].Cells.Count; column++)
                    {
                        if (dgv.Rows[row].Cells[column].Value != null)
                        {
                            string cellValue = dgv.Rows[row].Cells[column].Value.ToString();
                            if (cellValue != null && cellValue.Length > 0)
                            {
                                if (cellValue.Contains("r"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.Red;
                                }
                                else if (cellValue.Contains("y"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.Yellow;
                                }
                                else if (cellValue.Contains("o"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.Orange;
                                }
                                else if (cellValue.Contains("p"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.Pink;
                                }
                                else if (cellValue.Contains("g"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.LightGreen;
                                }
                                else if (cellValue.Contains("aa"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(255, 181, 101, 29);
                                }
                                else if (cellValue.Contains("cc"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.SkyBlue;
                                }
                                else if (cellValue.Contains("dd"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.SteelBlue;
                                }
                                else if (cellValue.Contains("b"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.SkyBlue;
                                }
                                else
                                {

                                }
                                dgv.Rows[row].Cells[column].Value = cellValue
                                    .Replace("r", "")
                                    .Replace("y", "")
                                    .Replace("o", "")
                                    .Replace("p", "")
                                    .Replace("g", "")
                                    .Replace("aa", "")
                                    .Replace("cc", "")
                                    .Replace("dd", "")
                                    .Replace("b", "")
                                ;

                            }
                        }

                    }

                }


                Common.Functions.FreezeBand(dgv.Columns[0]);
            }


        }

        private delegate void dlgLoadDataHistoryFt(DataGridView dgv, int livePeriod);
        private void LoadDataHistoryFt(DataGridView dgv, int livePeriod)
        {
            if (dgv.InvokeRequired)
            {
                this.Invoke(new dlgLoadDataHistoryFt(LoadDataHistoryFt), dgv, livePeriod);
            }
            else
            {
                if (!DataStore.ProductStatus.ContainsKey(_matchId)) return;
                dgv.Rows.Clear();

                LoadData(dgv, Common.Enums.BetType.FullTimeHandicap, livePeriod);
                LoadData(dgv, Common.Enums.BetType.FullTimeOverUnder, livePeriod);
                LoadData(dgv, Common.Enums.BetType.FullTime1x2, livePeriod);

                Match match = DataStore.Matchs[_matchId];
                int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
                if (currentMinute > 101) currentMinute = 100;
                if (currentMinute < 0) currentMinute = 0;
                if (currentMinute > 0 && currentMinute <= 100)
                {
                    for (int row = 0; row < dgv.Rows.Count; row++)
                    {
                        dgv.Rows[row].Cells[currentMinute].Style.BackColor = Color.Gray;
                    }
                }

                for (int row = 1; row < dgv.Rows.Count; row++)
                {
                    if (row % 4 == 0) continue;
                    for (int column = 1; column < dgv.Rows[row].Cells.Count; column++)
                    {
                        if (dgv.Rows[row].Cells[column].Value != null)
                        {
                            string cellValue = dgv.Rows[row].Cells[column].Value.ToString();
                            if (cellValue != null && cellValue.Length > 0)
                            {
                                if (cellValue.Contains("r"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.Red;
                                }
                                else if (cellValue.Contains("y"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.Yellow;
                                }
                                else if (cellValue.Contains("o"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.Orange;
                                }
                                else if (cellValue.Contains("p"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.Pink;
                                }
                                else if (cellValue.Contains("g"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.LightGreen;
                                }
                                else if (cellValue.Contains("aa"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(255, 181, 101, 29);
                                }
                                else if (cellValue.Contains("cc"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.SkyBlue;
                                }
                                else if (cellValue.Contains("dd"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.SteelBlue;
                                }
                                else if (cellValue.Contains("b"))
                                {
                                    dgv.Rows[row].Cells[column].Style.BackColor = Color.SkyBlue;
                                }
                                else
                                {

                                }
                                dgv.Rows[row].Cells[column].Value = cellValue
                                    .Replace("r", "")
                                    .Replace("y", "")
                                    .Replace("o", "")
                                    .Replace("p", "")
                                    .Replace("g", "")
                                    .Replace("aa", "")
                                    .Replace("cc", "")
                                    .Replace("dd", "")
                                    .Replace("b", "")
                                ;

                            }
                        }
                    }
                }
                Common.Functions.FreezeBand(dgv.Columns[0]);
            }
        }

        private string GetColorClosePrice(int countUpdate, int countInsert, int countDelete, bool hasDeleteFirst, List<ProductStatusLog> dataAtMinute, int countClosePrice)
        {
            string colorRunning = "";
            ProductStatusLog productOu = dataAtMinute.FirstOrDefault(item => item.Type == Common.Enums.BetType.FullTimeOverUnder);
            if (productOu != null
                && productOu.TotalOuLine > 0
                && countClosePrice > 0)
            {
                if (countClosePrice - productOu.TotalOuLine >= 2)
                {
                    return "aa";
                }
            }
            
            if (countUpdate > 0 && countInsert == 0 && countDelete == 0)
            {
                colorRunning = "p";
            }
            else if (countUpdate > 0 && countInsert > 0 && countDelete == 0)
            {
                colorRunning = "b";
            }
            //else if (countUpdate > 0 && countInsert > 0 && countDelete > 0)
            //{
            //    colorRunning = "aa";
            //}
            else if (countUpdate == 0 && countInsert == 0 && countDelete == 0)
            {
                colorRunning = "";
            }
            else if (countUpdate == 0 && countInsert > 0 && countDelete == 0)
            {
                colorRunning = "g";
            }
            else if (countUpdate == 0 && countInsert > 0 && countDelete > 0)
            {
                if (hasDeleteFirst)
                {
                    colorRunning = "dd";
                }
                else
                {
                    colorRunning = "cc";
                }
            }
            else if (countUpdate == 0 && countInsert == 0 && countDelete > 0)
            {
                colorRunning = "y";
            }
            else if (countUpdate > 0 && countInsert == 0 && countDelete > 0)
            {
                colorRunning = "o";
            }
            return colorRunning;
        }

        private string GetColor(int countUpdate , int countInsert, int countDelete, bool hasDeleteFirst)
        {
            string colorRunning = "";
            if (countUpdate > 0 && countInsert == 0 && countDelete == 0)
            {
                colorRunning = "r";
            }
            else if (countUpdate > 0 && countInsert > 0 && countDelete == 0)
            {
                colorRunning = "b";
            }
            else if (countUpdate > 0 && countInsert > 0 && countDelete > 0)
            {
                colorRunning = "aa";
            }
            else if (countUpdate == 0 && countInsert == 0 && countDelete == 0)
            {
                colorRunning = "";
            }
            else if (countUpdate == 0 && countInsert > 0 && countDelete == 0)
            {
                colorRunning = "g";
            }
            else if (countUpdate == 0 && countInsert > 0 && countDelete > 0)
            {
                if (hasDeleteFirst)
                {
                    colorRunning = "dd";
                }
                else
                {
                    colorRunning = "cc";
                }
            }
            else if (countUpdate == 0 && countInsert == 0 && countDelete > 0)
            {
                colorRunning = "y";
            }
            else if (countUpdate > 0 && countInsert == 0 && countDelete > 0)
            {
                colorRunning = "o";
            }
            return colorRunning;
        }

        private bool HasDeleteFirst(List<ProductStatusLog> dataAtMinute)
        {
            for (int i=0; i<dataAtMinute.Count; i++)
            {
                if (dataAtMinute[i].ServerAction == "delete")
                {
                    return true;
                }
                else if (dataAtMinute[i].ServerAction == "insert")
                {
                    return false;
                }
            }
            return false;
        }

        private void LoadData(DataGridView dgv, Common.Enums.BetType betType, int livePeriod)
        {
            List<List<ProductStatusLog>> data = DataStore.ProductStatus[_matchId];
            List<string> headerRow = new List<string>() { betType.ToString() };
            List<string> dataRowRunning = new List<string>() { "running" };
            List<string> dataRowSuspend = new List<string>() { "suspend" };
            List<string> dataRowClosePrice = new List<string>() { "closePrice" };


            dgv.ColumnCount = 104;
            dgv.Columns[0].Width = 45;
            dgv.Columns[103].Width = 18;
            for (int i = 1; i <= 102; i++)
            {
                int realMinute = 0;
                realMinute = i - 1;
                headerRow.Add((i - 1).ToString());

                List<ProductStatusLog> dataAtMinute = data[realMinute].Where(item => 
                    item.LivePeriod == livePeriod
                    || (livePeriod == 1 && !item.IsHt && item.LivePeriod == 0
                        && (
                                betType == Common.Enums.BetType.FirstHalf1x2
                                || betType == Common.Enums.BetType.FirstHalfHandicap
                                || betType == Common.Enums.BetType.FirstHalfOverUnder
                            )
                        )
                    || (livePeriod == 1 
                        && (item.IsHt || item.LivePeriod == 0)
                        && (
                            betType == Common.Enums.BetType.FullTime1x2
                            || betType == Common.Enums.BetType.FullTimeHandicap
                            || betType == Common.Enums.BetType.FullTimeOverUnder
                        )
                    )
                ).ToList();

                int countRunning = dataAtMinute.Count(item => item.Status == "running" && item.Type == betType);
                int countSuspend = dataAtMinute.Count(item => item.Status == "suspend" && item.Type == betType);
                int countClosePrice = dataAtMinute.Count(item => item.Status == "closePrice" && item.Type == betType);

                bool hasRunning1x2Price = dataAtMinute.Any(item =>
                    item.Status == "running"
                    && item.Type == betType
                    && (betType == Common.Enums.BetType.FullTime1x2 || betType == Common.Enums.BetType.FirstHalf1x2)
                    && item.Price != 0
                );

                bool hasSuspend1x2Price = dataAtMinute.Any(item =>
                    item.Status == "suspend"
                    && item.Type == betType
                    && (betType == Common.Enums.BetType.FullTime1x2 || betType == Common.Enums.BetType.FirstHalf1x2)
                    && item.Price != 0
                );

                bool hasClosePrice1x2Price = dataAtMinute.Any(item =>
                    item.Status == "closePrice"
                    && item.Type == betType
                    && (betType == Common.Enums.BetType.FullTime1x2 || betType == Common.Enums.BetType.FirstHalf1x2)
                    && item.Price != 0
                );

                bool hasDeleteFirstRunning = HasDeleteFirst(dataAtMinute.Where(item => item.Status == "running" && item.Type == betType).ToList());
                bool hasDeleteFirstSuspend = HasDeleteFirst(dataAtMinute.Where(item => item.Status == "suspend" && item.Type == betType).ToList());
                bool hasDeleteFirstClosePrice = HasDeleteFirst(dataAtMinute.Where(item => item.Status == "closePrice" && item.Type == betType).ToList());


                int countRunning_Insert = dataAtMinute.Count(item =>
                    item.Status == "running"
                    && item.ServerAction == "insert"
                    && item.Type == betType
                );

                int countRunning_Delete = dataAtMinute.Count(item =>
                    item.Status == "running"
                    && item.ServerAction == "delete"
                    && item.Type == betType
                );

                int countRunning_Update = dataAtMinute.Count(item =>
                    item.Status == "running"
                    && item.ServerAction == "update"
                    && item.Type == betType
                );

                int countSuspend_Delete = dataAtMinute.Count(item =>
                    item.Status == "suspend"
                    && item.ServerAction == "delete"
                    && item.Type == betType
                );

                int countSuspend_Insert = dataAtMinute.Count(item =>
                    item.Status == "suspend"
                    && item.ServerAction == "insert"
                    && item.Type == betType
                );

                int countSuspend_Update = dataAtMinute.Count(item =>
                    item.Status == "suspend"
                    && (item.ServerAction == "update")
                    && item.Type == betType
                );

                int countClosePrice_Delete = dataAtMinute.Count(item =>
                    item.Status == "closePrice"
                    && item.ServerAction == "delete"
                    && item.Type == betType
                );

                int countClosePrice_Insert = dataAtMinute.Count(item =>
                    item.Status == "closePrice"
                    && item.ServerAction == "insert"
                    && item.Type == betType
                );

                int countClosePrice_Update = dataAtMinute.Count(item =>
                    item.Status == "closePrice"
                    && (item.ServerAction == "update")
                    && item.Type == betType
                );

                string colorRunning = GetColor(countRunning_Update, countRunning_Insert, countRunning_Delete, hasDeleteFirstRunning);
                string colorSuspend = GetColor(countSuspend_Update, countSuspend_Insert, countSuspend_Delete, hasDeleteFirstSuspend);
                string colorClosePrice = GetColorClosePrice(countClosePrice_Update, countClosePrice_Insert, countClosePrice_Delete, hasDeleteFirstClosePrice, dataAtMinute, countClosePrice);

                dataRowRunning.Add((countRunning > 0 ? countRunning.ToString() + colorRunning : "") + (hasRunning1x2Price ? "+" : ""));
                dataRowSuspend.Add((countSuspend > 0 ? countSuspend.ToString() + colorSuspend : "") + (hasSuspend1x2Price ? "+" : ""));
                dataRowClosePrice.Add((countClosePrice > 0 ? countClosePrice.ToString() + colorClosePrice : "") + (hasClosePrice1x2Price ? "+" : ""));

                dgv.Columns[i].Width = 18;
            }
            dgv.Rows.Add(headerRow.ToArray());
            dgv.Rows.Add(dataRowRunning.ToArray());
            dgv.Rows.Add(dataRowSuspend.ToArray());
            dgv.Rows.Add(dataRowClosePrice.ToArray());
        }

        private void ProductIdHistoryDetailByTypeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
            if (_executeThreadCheckNewAction != null && _executeThreadCheckNewAction.IsAlive) _executeThreadCheckNewAction.Abort();
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
            catch
            {

            }
        }

        private void h1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSplitContainer(1);
        }

        private void h2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSplitContainer(2);
        }

        private void LoadSplitContainer(int livePeriod)
        {
            if (livePeriod == 1)
            {
                //this.splitContainer1.Panel1Collapsed = false;
                //this.splitContainer1.Panel2Collapsed = false;
                //this.splitContainer2.Panel1Collapsed = false;
                //this.splitContainer2.Panel2Collapsed = true;

                this.splitContainer1.Panel1.Padding = new Padding(0, 25, 0, 0);

                this.splitContainer1.SplitterDistance = this.Height / 2 - 25;
                this.splitContainer2.SplitterDistance = this.Height / 2 - 25;

                this.h1ToolStripMenuItem.Text = "(H1)";
                this.h2ToolStripMenuItem.Text = "H2";
            }
            else if (livePeriod == 2)
            {
                //this.splitContainer1.Panel1Collapsed = true;
                //this.splitContainer1.Panel2Collapsed = false;
                //this.splitContainer2.Panel1Collapsed = false;
                //this.splitContainer2.Panel2Collapsed = false;

                this.splitContainer1.SplitterDistance = 25;
                this.splitContainer2.SplitterDistance = this.Height / 2 - 25;

                this.splitContainer1.Panel1.Padding = new Padding(0, 0, 0, 0);


                this.h1ToolStripMenuItem.Text = "H1";
                this.h2ToolStripMenuItem.Text = "(H2)";
            }
            else if (livePeriod == 0 || livePeriod == -1)
            {

            }
        }

        private void alertNewActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataStore.Matchs[_matchId].AlertNewProductAction = !DataStore.Matchs[_matchId].AlertNewProductAction;
            SetAlertNewActionText();
        }

        private void SetAlertNewActionText()
        {
            if (DataStore.Matchs[_matchId].AlertNewProductAction)
            {
                this.alertNewActionToolStripMenuItem.Text = "Alert New Action (being on)";
            }
            else
            {
                this.alertNewActionToolStripMenuItem.Text = "Alert New Action (being off)";
            }
        }

        private void dgvFh_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductHistoryInspectorForm form = new ProductHistoryInspectorForm(
                this._matchId, 
                e.ColumnIndex - 1, 
                1
            );
            form.Show();
        }

        private void dgvFtH1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductHistoryInspectorForm form = new ProductHistoryInspectorForm(
                this._matchId,
                e.ColumnIndex - 1,
                1
            );
            form.Show();
        }

        private void dgvFtH2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductHistoryInspectorForm form = new ProductHistoryInspectorForm(
                this._matchId,
                e.ColumnIndex - 1,
                2
            );
            form.Show();
        }
    }
}
