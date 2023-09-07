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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms
{
    public partial class OverUnderScoreTimeV2Form : Form
    {
        private long _matchId;

        public OverUnderScoreTimeV2Form(long matchId)
        {
            this._matchId = matchId;
            InitializeComponent();
        }

        private void OverUnderScoreTimeV2Form_Load(object sender, EventArgs e)
        {
            LoadMainGrid();
        }

        private void LoadMainGrid()
        {
            if (!DataStore.Matchs.ContainsKey(_matchId)) return;


            LoadHeader();
            LoadData();
            SetStyle();

            
        }

        private void LoadHeader()
        {
            Match match = DataStore.Matchs[_matchId];

            this.Text = match.Home + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")" + " | " + match.OverUnderMoneyLine + " | SHEET TABLE V2";
            int columnCount = 91;
            this.dataGridView.Rows.Clear();

            this.dataGridView.ColumnCount = columnCount;
            for (int i = 0; i < columnCount; i++)
            {
                this.dataGridView.Columns[i].Width = 19;
            }
            this.dataGridView.Columns[0].Width = 25;

            ArrayList header = new ArrayList() { "" };
            for (int i = 0; i < columnCount; i++)
            {
                header.Add(((i % 45) + 1).ToString());
            }

            this.dataGridView.Rows.Add(header.ToArray());
        }

        private void LoadData()
        {
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data;

            if (!DataStore.OverUnderScoreTimesV2.TryGetValue(_matchId, out data)) return;

            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            if (scores.Count == 0) return;

            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = data[scores[i]].Select(item => item.Over == 0 ? "" : item.Over.ToString()).ToList();
                row[0] = scores[i].ToString();
                this.dataGridView.Rows.Add(row.ToArray());
            }
        }

        private void SetStyle()
        {
            SetOddEvenColor();
            SetMinusStyle();
        }

        private void SetMinusStyle()
        {
            for (int i = 1; i < dataGridView.RowCount; i++)
            {
                for (int j = 1; j < dataGridView.ColumnCount; j++)
                {
                    if (this.dataGridView.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dataGridView.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dataGridView.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dataGridView.Rows[i].Cells[j].Style) { Font = new Font(this.dataGridView.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Black };
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

        private void SetOddEvenColor()
        {
            for (int i = 0; i < this.dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < this.dataGridView.ColumnCount; j++)
                {
                    this.dataGridView.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                    this.dataGridView.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                }
            }
        }

        private void SetGoalColors()
        {
            List<GoalHistory> goalHistories;
            if (DataStore.GoalHistories.TryGetValue(_matchId, out goalHistories))
            {
                int rowCount = this.dataGridView.Rows.Count;
                for (int i = 0; i < goalHistories.Count; i++)
                {
                    int column = (int)goalHistories[i].TimeSpanFromStart.TotalMinutes;
                    if (column > 45) continue;
                    column = column + (goalHistories[i].LivePeriod - 1) * 45;

                    for (int j = 0; j < rowCount; j++)
                    {
                        this.dataGridView.Rows[j].Cells[column].Style.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SetOddEvenColor();

                if (e.ColumnIndex == 0 || e.RowIndex == 0) return;
                if (this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "") return;

                int selectedScore = int.Parse(this.dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                OverUnderScoreTimesV2Item inItem = DataStore.OverUnderScoreTimesV2[_matchId][selectedScore][e.ColumnIndex];

                for (int i = 1; i < this.dataGridView.Rows.Count; i++)
                {
                    int score = int.Parse(this.dataGridView.Rows[i].Cells[0].Value.ToString());
                    for (int j = e.ColumnIndex; j < this.dataGridView.ColumnCount; j++)
                    {
                        if (this.dataGridView.Rows[i].Cells[j].Value.ToString() != "")
                        {
                            OverUnderScoreTimesV2Item outItem = DataStore.OverUnderScoreTimesV2[_matchId][score][j];

                            bool inByUnderOutByOver = (
                                (e.RowIndex <= i)
                                && ((inItem.Under < 0 && outItem.Over < 0) || (inItem.Under * outItem.Over < 0 && inItem.Under + outItem.Over >= 0))
                            );
                            bool inByOverOutByUnder = (
                                (i <= e.RowIndex)
                                && ((inItem.Over < 0 && outItem.Under < 0) || (inItem.Over * outItem.Under < 0 && inItem.Over + outItem.Under >= 0))
                            );

                            if (inByUnderOutByOver && inByOverOutByUnder)
                            {
                                this.dataGridView.Rows[i].Cells[j].Style.BackColor = Color.Magenta;
                            }
                            else if (inByUnderOutByOver)
                            {
                                this.dataGridView.Rows[i].Cells[j].Style.BackColor = Color.Blue;
                                this.dataGridView.Rows[i].Cells[j].Style.ForeColor = Color.White;
                            }
                            else if (inByOverOutByUnder)
                            {
                                this.dataGridView.Rows[i].Cells[j].Style.BackColor = Color.Red;
                                this.dataGridView.Rows[i].Cells[j].Style.ForeColor = Color.White;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 || e.RowIndex == 0) return;
                if (this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "") return;
                int selectedScore = int.Parse(this.dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                OverUnderScoreTimesV2Item selectedItem = DataStore.OverUnderScoreTimesV2[_matchId][selectedScore][e.ColumnIndex];
                DataGridViewCell cell = this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = selectedItem.Under.ToString();
            }
            catch
            {

            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMainGrid();
        }
    }
}
