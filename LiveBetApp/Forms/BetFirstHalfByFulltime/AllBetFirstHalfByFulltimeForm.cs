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

namespace LiveBetApp.Forms.BetFirstHalfByFulltime
{
    public partial class AllBetFirstHalfByFulltimeForm : Form
    {
        private Thread _executeThread;

        public AllBetFirstHalfByFulltimeForm()
        {
            InitializeComponent();
        }

        private void AllBetFirstHalfByFulltimeForm_Load(object sender, EventArgs e)
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
            if (this.dgvRunning.InvokeRequired)
            {
                this.Invoke(new dlgLoadMainGrid(LoadMainGrid));
            }
            else
            {
                dgvRunning.Columns.Clear();
                BindingSource bindingSource = new BindingSource();
                List<Models.DataModels.BetFirstHalfByFulltime> allBetData = DataStore.BetFirstHalfByFulltime
                    .OrderBy(item => item.MatchId)
                    .ThenBy(item => item.Hdp)
                    .ThenBy(item => item.Price)
                    .ToList();

                List<Models.ViewModels.BetFirstHalfByFulltime> allBetView = new List<Models.ViewModels.BetFirstHalfByFulltime>();
                for (int i = 0; i < allBetData.Count; i++)
                {
                    Match matchOfThisBet;
                    if (DataStore.Matchs.TryGetValue(allBetData[i].MatchId, out matchOfThisBet))
                    {
                        allBetView.Add(new Models.ViewModels.BetFirstHalfByFulltime()
                        {
                            Id = allBetData[i].Id,
                            MatchId = allBetData[i].MatchId,
                            Hdp = allBetData[i].Hdp,
                            Price = allBetData[i].Price,
                            Stake = allBetData[i].Stake,
                            Away = matchOfThisBet.ScoreAway,
                            Home = matchOfThisBet.ScoreHome,
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
                dgvRunning.DataSource = bindingSource;
                dgvRunning.Columns["Id"].Visible = false;
                dgvRunning.Columns["MatchId"].Visible = false;
                dgvRunning.Columns["AutoBetMessage"].Visible = false;
                dgvRunning.Columns["Away"].Width = 200;
                dgvRunning.Columns["Home"].Width = 200;

                dgvRunning.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "Sheet Table",
                    Name = "sheetTableRowBtn",
                    HeaderText = "Sheet Table"
                });

                dgvRunning.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
                });

                dgvRunning.Columns.Add(new DataGridViewButtonColumn()
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
                List<Models.DataModels.BetFirstHalfByFulltime> allBetData = DataStore.FinishedBetFirstHalfByFulltime
                    .OrderBy(item => item.MatchId)
                    .ThenBy(item => item.Hdp)
                    .ThenBy(item => item.Price)
                    .ToList();

                List<LiveBetApp.Models.ViewModels.BetFirstHalfByFulltime> allBetView = new List<Models.ViewModels.BetFirstHalfByFulltime>();
                for (int i = 0; i < allBetData.Count; i++)
                {
                    Match matchOfThisBet;
                    if (DataStore.Matchs.TryGetValue(allBetData[i].MatchId, out matchOfThisBet))
                    {
                        allBetView.Add(new Models.ViewModels.BetFirstHalfByFulltime()
                        {
                            Id = allBetData[i].Id,
                            MatchId = allBetData[i].MatchId,
                            Hdp = allBetData[i].Hdp,
                            Price = allBetData[i].Price,
                            Stake = allBetData[i].Stake,
                            Away = matchOfThisBet.ScoreAway,
                            Home = matchOfThisBet.ScoreHome,
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

        private void AllBetFirstHalfByFulltimeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }

        private void dgvRunning_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvRunning.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Guid id = (Guid)dgvRunning.Rows[e.RowIndex].Cells["Id"].Value;
                        DataStore.BetFirstHalfByFulltime.RemoveAll(item => item.Id == id);
                    }
                    catch { }
                    LoadMainGrid();
                }
            }
            else if (e.ColumnIndex == dgvRunning.Columns["sheetTableRowBtn"].Index)
            {
                Guid id = (Guid)dgvRunning.Rows[e.RowIndex].Cells["Id"].Value;
                Models.DataModels.BetFirstHalfByFulltime rowData = DataStore.BetFirstHalfByFulltime.FirstOrDefault(item => item.Id == id);
                if (rowData == null) return;
                long matchId = rowData.MatchId;
                OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
                form.Show();
            }
            else if (e.ColumnIndex == dgvRunning.Columns["deleteAllRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete ALL?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DataStore.BetFirstHalfByFulltime.RemoveAll(item => true);
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
                        DataStore.FinishedBetFirstHalfByFulltime.RemoveAll(item => item.Id == id);
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
            else if (e.ColumnIndex == dgvFinished.Columns["deleteAllRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete ALL?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DataStore.FinishedBetFirstHalfByFulltime.RemoveAll(item => true);
                    LoadFinishGrid();
                }
            }
        }
    }
}
