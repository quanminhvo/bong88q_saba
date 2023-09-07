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

namespace LiveBetApp.Forms.BetAfterGoodPrice
{
    public partial class AllBetAfterGoodPrice : Form
    {
        private Thread _executeThread;

        public AllBetAfterGoodPrice()
        {
            InitializeComponent();
        }

        private void AllBetAfterGoodPrice_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        LoadMainGrid();
                        LoadFinishGrid();
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(30000);
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
                List<Models.DataModels.BetAfterGoodPrice> allBetData = DataStore.BetAfterGoodPrice
                    .OrderBy(item => item.MatchId)
                    .ThenBy(item => item.LivePeriod)
                    .ThenBy(item => item.Hdp)
                    .ThenBy(item => item.Price)
                    .ToList();

                List<Models.ViewModels.BetAfterGoodPrice> allBetView = new List<Models.ViewModels.BetAfterGoodPrice>();
                for (int i = 0; i < allBetData.Count; i++)
                {
                    Match matchOfThisBet;
                    if (DataStore.Matchs.TryGetValue(allBetData[i].MatchId, out matchOfThisBet))
                    {
                        allBetView.Add(new Models.ViewModels.BetAfterGoodPrice()
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
                            MoneyLine = allBetData[i].MoneyLine,
                            UpToMinute = allBetData[i].UpToMinute,
                            CareGoal = allBetData[i].CareGoal,
                        });
                    }
                }

                allBetView = allBetView.OrderByDescending(item => item.CreateDateTime).ToList();

                bindingSource.DataSource = Common.Functions.ToDataTable(allBetView);
                dataGridView.DataSource = bindingSource;
                dataGridView.Columns["Id"].Visible = false;
                dataGridView.Columns["MatchId"].Visible = false;
                dataGridView.Columns["AutoBetMessage"].Visible = false;
                dataGridView.Columns["FirstMinuteHasGoodPrice"].Visible = false;
                dataGridView.Columns["Price"].Visible = false;
                dataGridView.Columns["UpToMinute"].Visible = false;
                dataGridView.Columns["CareGoal"].Visible = false;
                dataGridView.Columns["TotalScore"].Visible = false;
                dataGridView.Columns["TotalRed"].Visible = false;
                dataGridView.Columns["AutoBetMessage"].Visible = false;
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

                dataGridView.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del all",
                    Name = "deleteAllRowBtn",
                    HeaderText = "Delete All"
                });
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
                List<Models.DataModels.BetAfterGoodPrice> allBetData = DataStore.FinishedBetAfterGoodPrice
                    .OrderBy(item => item.MatchId)
                    .ThenBy(item => item.LivePeriod)
                    .ThenBy(item => item.Hdp)
                    .ThenBy(item => item.Price)
                    .ToList();

                List<LiveBetApp.Models.ViewModels.BetAfterGoodPrice> allBetView = new List<Models.ViewModels.BetAfterGoodPrice>();
                for (int i = 0; i < allBetData.Count; i++)
                {
                    Match matchOfThisBet;
                    if (DataStore.Matchs.TryGetValue(allBetData[i].MatchId, out matchOfThisBet))
                    {
                        allBetView.Add(new Models.ViewModels.BetAfterGoodPrice()
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
                            MoneyLine = allBetData[i].MoneyLine,
                            UpToMinute = allBetData[i].UpToMinute,
                            CareGoal = allBetData[i].CareGoal
                        });
                    }
                }

                bindingSource.DataSource = Common.Functions.ToDataTable(allBetView);
                dgvFinished.DataSource = bindingSource;
                dgvFinished.Columns["Id"].Visible = false;
                dgvFinished.Columns["MatchId"].Visible = false;
                dgvFinished.Columns["AutoBetMessage"].Visible = false;
                dgvFinished.Columns["FirstMinuteHasGoodPrice"].Visible = false;
                dgvFinished.Columns["Price"].Visible = false;
                dgvFinished.Columns["UpToMinute"].Visible = false;
                dgvFinished.Columns["CareGoal"].Visible = false;
                dgvFinished.Columns["TotalScore"].Visible = false;
                dgvFinished.Columns["TotalRed"].Visible = false;
                dgvFinished.Columns["AutoBetMessage"].Visible = false;
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

                dgvFinished.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del all",
                    Name = "deleteAllRowBtn",
                    HeaderText = "Delete All"
                });
            }
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
                        DataStore.BetAfterGoodPrice.RemoveAll(item => item.Id == id);
                    }
                    catch { }
                    LoadMainGrid();
                }
            }
            else if (e.ColumnIndex == dataGridView.Columns["sheetTableRowBtn"].Index)
            {
                Guid id = (Guid)dataGridView.Rows[e.RowIndex].Cells["Id"].Value;
                Models.DataModels.BetAfterGoodPrice rowData = DataStore.BetAfterGoodPrice.FirstOrDefault(item => item.Id == id);
                if (rowData == null) return;
                long matchId = rowData.MatchId;
                OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
                form.Show();
            }
            else if (e.ColumnIndex == dataGridView.Columns["deleteAllRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete ALL?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DataStore.BetAfterGoodPrice.RemoveAll(item => true);
                    LoadMainGrid();
                }
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
                        DataStore.FinishedBetAfterGoodPrice.RemoveAll(item => item.Id == id);
                    }
                    catch { }
                    LoadFinishGrid();
                }
            }
            else if (e.ColumnIndex == dgvFinished.Columns["sheetTableRowBtn"].Index)
            {
                Guid id = (Guid)dgvFinished.Rows[e.RowIndex].Cells["Id"].Value;
                Models.DataModels.BetAfterGoodPrice rowData = DataStore.FinishedBetAfterGoodPrice.FirstOrDefault(item => item.Id == id);
                if (rowData == null) return;
                long matchId = rowData.MatchId;
                OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
                form.Show();
            }
            else if (e.ColumnIndex == dgvFinished.Columns["deleteAllRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete ALL?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DataStore.FinishedBetAfterGoodPrice.RemoveAll(item => true);
                    LoadFinishGrid();
                }
            }
        }

        private void AllBetAfterGoodPrice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }
    }
}
