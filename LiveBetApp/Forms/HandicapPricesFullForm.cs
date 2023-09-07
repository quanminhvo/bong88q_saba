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
    public partial class HandicapPricesFullForm : Form
    {
        private long _matchId;
        private List<int> _minutesToCaptureHandicapPrice;

        public HandicapPricesFullForm(long matchId)
        {
            _matchId = matchId;
            InitializeComponent();
        }

        private void HandicapPricesFullForm_Load(object sender, EventArgs e)
        {
            _minutesToCaptureHandicapPrice = new List<int>() { 1,2,3,4,5,6,7,8,9,10, 46,47,48,49,50,51,52,53,54,55,56};
            LoadHomeGrid();
        }

        private delegate void dlgLoadHomeGrid();
        private void LoadHomeGrid()
        {
            if (this.dgvHome.InvokeRequired)
            {
                this.Invoke(new dlgLoadHomeGrid(LoadHomeGrid));
                return;
            }
            this.dgvHome.Rows.Clear();
            Match match = DataStore.Matchs[_matchId];
            this.Text = match.Home
                + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")"
                + " | " + match.League
                + " | BRIEF HANDICAP TABLE";

            if (!DataStore.HandicapScoreTimes.ContainsKey(_matchId)) return;

            List<long> productIds = DataStore.HandicapScoreTimes[_matchId].Keys.ToList();

            

            this.dgvHome.ColumnCount = (_minutesToCaptureHandicapPrice.Count * 2) + 2;
            for (int i = 0; i < this.dgvHome.ColumnCount; i++)
            {
                this.dgvHome.Columns[i].Width = 30;
            }
            this.dgvHome.Columns[0].Width = 100;

            //26+45 -> 37+45
            //1 -> 11
            ArrayList header = new ArrayList() { "" };
            for (int i = 0; i < _minutesToCaptureHandicapPrice.Count; i++)
            {

                if (_minutesToCaptureHandicapPrice[i] > 45)
                {
                    header.Add("");
                    header.Add("2H " + (_minutesToCaptureHandicapPrice[i] - 45).ToString());
                }
                else
                {
                    header.Add("");
                    header.Add("1H " + _minutesToCaptureHandicapPrice[i].ToString());
                }

            }
            this.dgvHome.Rows.Add(header.ToArray());

            for (int i = 0; i < productIds.Count; i++)
            {
                ArrayList rowHome = new ArrayList() { match.Home };
                ArrayList rowAway = new ArrayList() { match.Away };

                for (int j = 0; j < _minutesToCaptureHandicapPrice.Count; j++)
                {
                    HandicapLifeTimeHistory history = DataStore.HandicapScoreTimes[_matchId][productIds[i]][_minutesToCaptureHandicapPrice[j]];
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

                this.dgvHome.Rows.Add(rowHome.ToArray());
                this.dgvHome.Rows.Add(rowAway.ToArray());
            }

            for (int i = 1; i <= productIds.Count; i++)
            {
                if (i % 2 == 0)
                {
                    this.dgvHome.Rows[(i * 2)].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    this.dgvHome.Rows[(i * 2) - 1].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    this.dgvHome.Rows[(i * 2)].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                    this.dgvHome.Rows[(i * 2) - 1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                }
            }

            for (int i = 1; i < dgvHome.RowCount; i++)
            {
                for (int j = 1; j < dgvHome.ColumnCount; j++)
                {
                    if (this.dgvHome.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvHome.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvHome.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvHome.Rows[i].Cells[j].Style) { Font = new Font(this.dgvHome.DefaultCellStyle.Font, FontStyle.Bold), ForeColor = Color.Blue };
                        }
                        else
                        {
                            this.dgvHome.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvHome.Rows[i].Cells[j].Style) { Font = new Font(this.dgvHome.DefaultCellStyle.Font, FontStyle.Regular), ForeColor = Color.Black };
                        }
                        this.dgvHome.Rows[i].Cells[j].Value = this.dgvHome.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }

                }
            }

        }
    }
}
