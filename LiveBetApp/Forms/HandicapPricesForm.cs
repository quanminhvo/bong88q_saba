using LiveBetApp.Models.DataModels;
using System;
using System.Collections;
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
    public partial class HandicapPricesForm : Form
    {
        private Thread _executeThread;
        private long _matchId;
        public HandicapPricesForm(long matchId)
        {
            this._matchId = matchId;
            InitializeComponent();
        }



        private void HandicapPricesForm_Load(object sender, EventArgs e)
        {

            try
            {
                LoadMainGrid();
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }

            _executeThread = new Thread(() =>
            {

                while (true)
                {
                    try
                    {
                        LoadMainGrid();
                        Thread.Sleep(10000);
                    }
                    catch (Exception ex)
                    {
                        Common.Functions.WriteExceptionLog(ex);
                    }
                }

            });

            _executeThread.IsBackground = true;
            _executeThread.Start();

            
        }

        private delegate void dlgLoadMainGrid();
        private void LoadMainGrid()
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgLoadMainGrid(LoadMainGrid));
                return;
            }
            this.dataGridView.Rows.Clear();
            Match match = DataStore.Matchs[_matchId];
            this.Text = match.Home 
                + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")"
                + " | " + match.League
                + " | BRIEF HANDICAP TABLE";

            if (!DataStore.HandicapScoreTimes.ContainsKey(_matchId)) return;

            List<long> productIds = DataStore.HandicapScoreTimes[_matchId].Keys.ToList();

            this.dataGridView.ColumnCount = (Common.Constants.MinutesToCaptureHandicapPrice.Length * 2) + 2;
            for (int i = 0; i < this.dataGridView.ColumnCount; i++)
            {
                this.dataGridView.Columns[i].Width = 30;
            }
            this.dataGridView.Columns[0].Width = 100;


            ArrayList header = new ArrayList() { "" };
            for (int i = 0; i < Common.Constants.MinutesToCaptureHandicapPrice.Length; i++)
            {
                if (Common.Constants.MinutesToCaptureHandicapPrice[i] > 45)
                {
                    header.Add("");
                    header.Add("2H " + (Common.Constants.MinutesToCaptureHandicapPrice[i] - 45).ToString());
                }
                else
                {
                    header.Add("");
                    header.Add("1H " + Common.Constants.MinutesToCaptureHandicapPrice[i].ToString());
                }

            }
            this.dataGridView.Rows.Add(header.ToArray());

            for (int i = 0; i < productIds.Count; i++)
            {
                ArrayList rowHome = new ArrayList() { match.Home };
                ArrayList rowAway = new ArrayList() { match.Away };

                for (int j = 0; j < Common.Constants.MinutesToCaptureHandicapPrice.Length; j++)
                {
                    HandicapLifeTimeHistory history = DataStore.HandicapScoreTimes[_matchId][productIds[i]][Common.Constants.MinutesToCaptureHandicapPrice[j]];
                    if (history != null)
                    {
                        rowHome.Add(history.Hdp1 == 0 ? "" : history.Hdp1.ToString());
                        rowHome.Add(history.Odds1a);
                        rowAway.Add(history.Hdp2 == 0 ? "" : history.Hdp2.ToString());
                        rowAway.Add(history.Odds2a);
                    }
                    else
                    {
                        rowHome.Add("");
                        rowHome.Add("");
                        rowAway.Add("");
                        rowAway.Add("");
                    }

                }

                this.dataGridView.Rows.Add(rowHome.ToArray());
                this.dataGridView.Rows.Add(rowAway.ToArray());
            }

            for (int i = 1; i <= productIds.Count; i++)
            {
                if (i % 2 == 0)
                {
                    this.dataGridView.Rows[(i * 2)].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    this.dataGridView.Rows[(i * 2) - 1].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    this.dataGridView.Rows[(i * 2)].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                    this.dataGridView.Rows[(i * 2) - 1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
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
                            this.dataGridView.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells[j].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Blue };
                        }
                        else
                        {
                            this.dataGridView.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells[j].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Regular), ForeColor = Color.Black };
                        }
                        this.dataGridView.Rows[i].Cells[j].Value = this.dataGridView.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }

                }
            }

        }

        private void HandicapPricesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }

    }
}
