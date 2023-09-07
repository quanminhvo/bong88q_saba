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
using LiveBetApp.Models.DataModels;
using LiveBetApp.Common;

namespace LiveBetApp.Forms.AutoBet
{
    public partial class AllAutoBetByHdpPriceForm : Form
    {
        private Thread _executeThread;
        public AllAutoBetByHdpPriceForm()
        {
            InitializeComponent();
        }

        private void AllAutoBetByHdpPrice_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (DataStore.MainformKeepRefreshingChecked)
                        {
                            LoadMainGrid();
                            LoadFinishGrid();
                        }
                        Thread.Sleep(30000);
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
            }
            else
            {
                dataGridView.Columns.Clear();
                BindingSource bindingSource = new BindingSource();
                List<Models.DataModels.BetByHdpPrice> allBetData = DataStore.BetByHdpPrice
                    .Where(item => item.AutoBetMessage != null && item.AutoBetMessage.Length > 0)
                    .OrderBy(item => item.MatchId)
                    .ThenBy(item => item.LivePeriod)
                    .ThenBy(item => item.Hdp)
                    .ThenBy(item => item.Price)
                    .ToList();

                List<Models.ViewModels.BetByHdpPrice> allBetView = new List<Models.ViewModels.BetByHdpPrice>();
                for (int i = 0; i < allBetData.Count; i++)
                {
                    Match matchOfThisBet;
                    if (DataStore.Matchs.TryGetValue(allBetData[i].MatchId, out matchOfThisBet))
                    {
                        allBetView.Add(new Models.ViewModels.BetByHdpPrice()
                        {
                            Id = allBetData[i].Id,
                            LivePeriod = allBetData[i].LivePeriod,
                            MatchId = allBetData[i].MatchId,
                            Hdp = allBetData[i].Hdp,
                            Price = allBetData[i].Price,
                            Stake = allBetData[i].Stake,
                            Away = matchOfThisBet.ScoreAway,
                            Home = matchOfThisBet.ScoreHome,
                            IsFulltime = allBetData[i].IsFulltime,
                            IsOver = allBetData[i].IsOver,
                            CreateDateTime = allBetData[i].CreateDateTime,
                            ResultMessage = allBetData[i].ResultMessage,
                            AutoBetMessage = allBetData[i].AutoBetMessage,
                            MoneyLine = allBetData[i].MoneyLine
                        });
                    }
                }

                allBetView = allBetView.OrderByDescending(item => item.CreateDateTime).ToList();

                bindingSource.DataSource = Common.Functions.ToDataTable(allBetView);
                dataGridView.DataSource = bindingSource;
                dataGridView.Columns["Id"].Visible = false;
                dataGridView.Columns["MatchId"].Visible = false;
                dataGridView.Columns["Away"].Width = 200;
                dataGridView.Columns["Home"].Width = 200;

                dataGridView.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "Sheet Table",
                    Name = "sheetTableRowBtn",
                    HeaderText = "Sheet Table"
                });

                dataGridView.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
                });

                Functions.HightLightRowHasGoal(dataGridView);
            }
        }

        private delegate void dlgLoadFinishGrid();
        private void LoadFinishGrid()
        {
            if (this.dgvFinished.InvokeRequired)
            {
                this.Invoke(new dlgLoadFinishGrid(LoadFinishGrid));
            }
            else
            {
                dgvFinished.Columns.Clear();
                BindingSource bindingSource = new BindingSource();
                List<Models.DataModels.BetByHdpPrice> allBetData = DataStore.FinishedBetByHdpPrice
                    .Where(item => item.AutoBetMessage != null && item.AutoBetMessage.Length > 0)
                    .OrderBy(item => item.MatchId)
                    .ThenBy(item => item.LivePeriod)
                    .ThenBy(item => item.Hdp)
                    .ThenBy(item => item.Price)
                    .ToList();

                List<LiveBetApp.Models.ViewModels.BetByHdpPrice> allBetView = new List<Models.ViewModels.BetByHdpPrice>();
                for (int i = 0; i < allBetData.Count; i++)
                {
                    Match matchOfThisBet;
                    if (DataStore.Matchs.TryGetValue(allBetData[i].MatchId, out matchOfThisBet))
                    {
                        allBetView.Add(new Models.ViewModels.BetByHdpPrice()
                        {
                            Id = allBetData[i].Id,
                            LivePeriod = allBetData[i].LivePeriod,
                            MatchId = allBetData[i].MatchId,
                            Hdp = allBetData[i].Hdp,
                            Price = allBetData[i].Price,
                            Stake = allBetData[i].Stake,
                            Away = matchOfThisBet.ScoreAway,
                            Home = matchOfThisBet.ScoreHome,
                            IsFulltime = allBetData[i].IsFulltime,
                            IsOver = allBetData[i].IsOver,
                            CreateDateTime = allBetData[i].CreateDateTime,
                            ResultMessage = allBetData[i].ResultMessage,
                            AutoBetMessage = allBetData[i].AutoBetMessage,
                            MoneyLine = allBetData[i].MoneyLine
                        });
                    }
                }

                bindingSource.DataSource = Common.Functions.ToDataTable(allBetView);
                dgvFinished.DataSource = bindingSource;
                dgvFinished.Columns["Id"].Visible = false;
                dgvFinished.Columns["MatchId"].Visible = false;
                dgvFinished.Columns["Away"].Width = 200;
                dgvFinished.Columns["Home"].Width = 200;

                dgvFinished.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "Sheet Table",
                    Name = "sheetTableRowBtn",
                    HeaderText = "Sheet Table"
                });

                dgvFinished.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
                });
            }
        }

        private void AllAutoBetByHdpPrice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Guid id = (Guid)dataGridView.Rows[e.RowIndex].Cells["Id"].Value;
                        DataStore.BetByHdpPrice.RemoveAll(item => item.Id == id);
                    }
                    catch { }
                    LoadMainGrid();
                }
            }
            else if (e.ColumnIndex == dataGridView.Columns["sheetTableRowBtn"].Index)
            {
                Guid id = (Guid)dataGridView.Rows[e.RowIndex].Cells["Id"].Value;
                Models.DataModels.BetByHdpPrice rowData = DataStore.BetByHdpPrice.FirstOrDefault(item => item.Id == id);
                if (rowData == null) return;
                long matchId = rowData.MatchId;
                OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
                form.Show();
            }
        }

        private void dgvFinished_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvFinished.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Guid id = (Guid)dgvFinished.Rows[e.RowIndex].Cells["Id"].Value;
                        DataStore.FinishedBetByHdpPrice.RemoveAll(item => item.Id == id);
                    }
                    catch { }
                    LoadFinishGrid();
                }
            }
            else if (e.ColumnIndex == dgvFinished.Columns["sheetTableRowBtn"].Index)
            {
                Guid id = (Guid)dgvFinished.Rows[e.RowIndex].Cells["Id"].Value;
                Models.DataModels.BetByHdpPrice rowData = DataStore.FinishedBetByHdpPrice.FirstOrDefault(item => item.Id == id);
                if (rowData == null) return;
                long matchId = rowData.MatchId;
                OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
                form.Show();
            }
        }

        private delegate void dlgdataGridView_Sorted();
        private void dataGridView_Sorted(object sender, EventArgs e)
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgLoadMainGrid(LoadMainGrid), new object[] { sender, e });
            }
            else
            {
                Functions.HightLightRowHasGoal(dataGridView);
            }
        }
    }
}
