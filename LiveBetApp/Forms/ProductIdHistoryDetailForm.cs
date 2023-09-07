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
    public partial class ProductIdHistoryDetailForm : Form
    {
        private long _matchId;
        private Thread _executeThread;
        public ProductIdHistoryDetailForm(long matchId)
        {
            _matchId = matchId;
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Width = Screen.PrimaryScreen.Bounds.Width / 2;
            this.Height = Screen.PrimaryScreen.Bounds.Height / 100 * 33;
            int startX = Screen.PrimaryScreen.Bounds.Width - this.Width < 0 ?
                0 :
                Screen.PrimaryScreen.Bounds.Width - this.Width;

            int startY = Screen.PrimaryScreen.Bounds.Height - this.Height - 30;
            this.Location = new Point(startX, startY);
        }

        private void ProductIdHistoryDetailForm_Load(object sender, EventArgs e)
        {
            if (DataStore.Matchs.ContainsKey(_matchId))
            {
                Match match = DataStore.Matchs[_matchId];
                this.Text = "2 - " + match.Home + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")"
                    + " | " + Common.Functions.GetOverUnderMoneyLinesString(_matchId)
                    + " | " + match.League
                    + " | Status by Server action";
            }


            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        LoadDataHistory(this.dgvStatusHistoryInsert);
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


        private delegate void dlgLoadDataHistory(DataGridView dgv);
        private void LoadDataHistory(DataGridView dgv)
        {
            if (dgv.InvokeRequired)
            {
                this.Invoke(new dlgLoadDataHistory(LoadDataHistory), dgv);
            }
            else
            {
                if (!DataStore.ProductStatus.ContainsKey(_matchId)) return;
                dgv.Rows.Clear();

                LoadData(dgv, "insert");
                LoadData(dgv, "update");
                LoadData(dgv, "delete");

                Match match = DataStore.Matchs[_matchId];
                int currennMinute100 = Common.Functions.GetCurrentMinute100(match);
                if (currennMinute100 > 0 && currennMinute100 <= 100)
                {
                    for (int row = 0; row < dgv.Rows.Count; row++)
                    {
                        dgv.Rows[row].Cells[currennMinute100].Style.BackColor = Color.Gray;
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
                    for (int column = 1; column < dgv.Rows[row].Cells.Count; column ++)
                    {
                        if (dgv.Rows[row].Cells[column].Value != null)
                        {
                            string cellValue = dgv.Rows[row].Cells[column].Value.ToString();
                            if (cellValue != null && cellValue.Length > 0)
                            {
                                dgv.Rows[row].Cells[column].Style.BackColor = Color.Orange;
                            }
                        }

                    }
                    
                }

                Common.Functions.FreezeBand(dgv.Columns[0]);
            }


        }

        private void LoadData(DataGridView dgv, string serverAction)
        {
            List<List<ProductStatusLog>> data = DataStore.ProductStatus[_matchId];
            List<string> headerRow = new List<string>() { serverAction };
            List<string> dataRowRunning = new List<string>() { "running" };
            List<string> dataRowSuspend = new List<string>() { "suspend" };
            List<string> dataRowClosePrice = new List<string>() { "closePrice" };


            dgv.ColumnCount = 104;
            dgv.Columns[0].Width = 45;
            dgv.Columns[103].Width = 18;
            for (int i = 1; i <= 102; i++)
            {
                int realMinute = 0;
                if (i <= 46)
                {
                    realMinute = i - 1;
                    headerRow.Add((i - 1).ToString());
                }
                else if (i == 47)
                {
                    realMinute = 101;
                    headerRow.Add("ht");
                }
                else if (i > 47)
                {
                    realMinute = i - 2;
                    headerRow.Add((i - 47).ToString());
                }


                int countRunning = data[realMinute].Count(item => item.Status == "running" && item.ServerAction == serverAction);
                int countSuspend = data[realMinute].Count(item => item.Status == "suspend" && item.ServerAction == serverAction);
                int countClosePrice = data[realMinute].Count(item => item.Status == "closePrice" && item.ServerAction == serverAction);


                dataRowRunning.Add(countRunning > 0 ? countRunning.ToString() : "");
                dataRowSuspend.Add(countSuspend > 0 ? countSuspend.ToString() : "");
                dataRowClosePrice.Add(countClosePrice > 0 ? countClosePrice.ToString() : "");


                dgv.Columns[i].Width = 18;
            }
            dgv.Rows.Add(headerRow.ToArray());
            dgv.Rows.Add(dataRowRunning.ToArray());
            dgv.Rows.Add(dataRowSuspend.ToArray());
            dgv.Rows.Add(dataRowClosePrice.ToArray());
        }

        private void ProductIdHistoryDetailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }
    }
}
