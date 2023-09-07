using LiveBetApp.Common;
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
    public partial class PriceChangeForm : Form
    {
        private long _matchId;
        private Thread _executeThread;
        public PriceChangeForm(long matchId)
        {
            _matchId = matchId;
            InitializeComponent();
        }

        private void PriceChangeForm_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        LoadMainGrid();
                        LoadMainGridFh();

                        for (int i = 1; i <= 41; i++)
                        {
                            mainDgv.Columns[i].Visible = false;
                            mainDgv.Columns[i + 46].Visible = false;


                            dgvFh.Columns[i].Visible = false;
                            //dgvFh.Columns[i + 46].Visible = false;
                        }

                        Thread.Sleep(60000);
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(60000);
                    }
                }
            });

            _executeThread.IsBackground = true;
            _executeThread.Start();

        }

        private delegate void dlgLoadMainGrid();
        private void LoadMainGrid()
        {
            if (this.mainDgv.InvokeRequired)
            {
                this.Invoke(new dlgLoadMainGrid(LoadMainGrid));
            }
            else
            {

                if (!DataStore.Matchs.ContainsKey(_matchId)) return;

                Dictionary<int, List<int>> data;
                if (!DataStore.OverUnderScoreTimesFreq.TryGetValue(_matchId, out data))
                {
                    return;
                }

                int columnCount = 102;
                this.mainDgv.Rows.Clear();
                this.mainDgv.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    this.mainDgv.Columns[i].Width = Common.Functions.ColWidthStyle();
                }
                this.mainDgv.Columns[0].Width = Common.Functions.ColWidthStyle() + 10;

                ArrayList header = new ArrayList() { "" };
                for (int i = 1; i < columnCount; i++)
                {
                    if (i - 1 <= 45)
                    {
                        header.Add((i - 1).ToString());
                    }
                    else
                    {
                        header.Add((i - 46).ToString());
                    }
                    
                }

                this.mainDgv.Rows.Add(header.ToArray());

                List<int> scores = data.Keys.OrderBy(item => item).ToList();
                scores = scores.OrderBy(item => item).ToList();

                if (scores.Count == 0) return;

                List<int> sumRow = new List<int>();
                for (int i = 0; i <= 101; i++ )
                {
                    sumRow.Add(0);
                }
                for (int i = scores.Count - 1; i >= 0; i--)
                {
                    List<string> row = new List<string>();
                    row.Add(scores[i].ToString());
                    if (data.ContainsKey(scores[i]))
                    {
                        row.AddRange(data[scores[i]].ConvertAll<string>(x => x > 0 ? x.ToString() : ""));
                        for (int j=0; j<data[scores[i]].Count; j++)
                        {
                            sumRow[j+1] += data[scores[i]][j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < this.mainDgv.ColumnCount - 1; j++)
                        {
                            row.Add("");
                        }
                    }
                    this.mainDgv.Rows.Add(row.ToArray());
                }

                this.mainDgv.Rows.Add(sumRow.ConvertAll<string>(x => x > 0 ? x.ToString() : "-").ToArray());

                for (int i = 0; i < this.mainDgv.Rows.Count; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        this.mainDgv.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                    }
                }

                List<GoalHistory> goalHistories;
                if (DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories))
                {
                    int rowCount = this.mainDgv.Rows.Count;
                    for (int i = 0; i < goalHistories.Count; i++)
                    {
                        int column = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes;
                        if (goalHistories[i].LivePeriod == 2)
                        {
                            column += 45;
                        }
                        if (column > 100) column = 100;
                        for (int j = 0; j < rowCount; j++)
                        {
                            if (goalHistories[i].LivePeriod == 1)
                            {
                                this.mainDgv.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                            }
                            else if (goalHistories[i].LivePeriod == 2)
                            {
                                this.mainDgv.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Yellow;
                            }
                        }
                    }
                }

                this.mainDgv.Rows[this.mainDgv.RowCount - 1].DefaultCellStyle = new DataGridViewCellStyle(this.mainDgv.Rows[this.mainDgv.RowCount - 1].DefaultCellStyle) { Font = new Font(this.mainDgv.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
                for (int i = 1; i <= 40; i++)
                {
                    mainDgv.Columns[i].Visible = false;

                }

                for (int i = 1; i <= 38; i++)
                {
                    mainDgv.Columns[i + 47].Visible = false;
                }

            }
        }

        private delegate void dlgLoadMainGridFh();
        private void LoadMainGridFh()
        {
            if (this.dgvFh.InvokeRequired)
            {
                this.Invoke(new dlgLoadMainGridFh(LoadMainGridFh));
            }
            else
            {

                if (!DataStore.Matchs.ContainsKey(_matchId)) return;

                Dictionary<int, List<int>> data;
                if (!DataStore.OverUnderScoreTimesFreqFh.TryGetValue(_matchId, out data))
                {
                    return;
                }

                int columnCount = 57;
                this.dgvFh.Rows.Clear();
                this.dgvFh.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    this.dgvFh.Columns[i].Width = Common.Functions.ColWidthStyle();
                }
                this.dgvFh.Columns[0].Width = Common.Functions.ColWidthStyle() + 10;

                ArrayList header = new ArrayList() { "" };
                for (int i = 1; i < columnCount; i++)
                {
                    header.Add(((i - 1) / Constants.OverUnderScoreTime_ColumnPerMinute).ToString());
                }

                this.dgvFh.Rows.Add(header.ToArray());

                List<int> scores = data.Keys.OrderBy(item => item).ToList();
                scores = scores.OrderBy(item => item).ToList();

                if (scores.Count == 0) return;
                List<int> sumRow = new List<int>();
                for (int i = 0; i <= 56; i++)
                {
                    sumRow.Add(0);
                }
                for (int i = scores.Count - 1; i >= 0; i--)
                {
                    List<string> row = new List<string>();
                    row.Add(scores[i].ToString());

                    if (data.ContainsKey(scores[i]))
                    {
                        row.AddRange(data[scores[i]].ConvertAll<string>(x => x > 0 ? x.ToString() : ""));
                        for (int j = 0; j < data[scores[i]].Count; j++)
                        {
                            sumRow[j + 1] += data[scores[i]][j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < this.dgvFh.ColumnCount - 1; j++)
                        {
                            row.Add("");
                        }
                    }

                    this.dgvFh.Rows.Add(row.ToArray());
                }

                this.dgvFh.Rows.Add(sumRow.ConvertAll<string>(x => x > 0 ? x.ToString() : "-").ToArray());

                for (int i = 0; i < this.dgvFh.Rows.Count; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        this.dgvFh.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                    }
                }

                List<GoalHistory> goalHistories;
                if (DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories))
                {
                    int rowCount = this.dgvFh.Rows.Count;
                    for (int i = 0; i < goalHistories.Count; i++)
                    {
                        int column = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes;
                        if (column > 55) column = 55;
                        for (int j = 0; j < rowCount; j++)
                        {
                            if (goalHistories[i].LivePeriod == 1)
                            {
                                this.dgvFh.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
                this.dgvFh.Rows[this.dgvFh.RowCount - 1].DefaultCellStyle = new DataGridViewCellStyle(this.dgvFh.Rows[this.dgvFh.RowCount - 1].DefaultCellStyle) { Font = new Font(this.dgvFh.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
                for (int i = 1; i <= 40; i++)
                {
                    dgvFh.Columns[i].Visible = false;
                }
            }



        }

        private void PriceChangeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }
    }
}
