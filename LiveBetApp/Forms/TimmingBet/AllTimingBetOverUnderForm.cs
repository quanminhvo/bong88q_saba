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

namespace LiveBetApp.Forms.TimmingBet
{
    public partial class AllTimingBetOverUnderForm : Form
    {
        private Thread _executeThread;

        public AllTimingBetOverUnderForm()
        {
            InitializeComponent();
        }

        private void AllTimingBetOverForm_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        LoadMainGrid();
                        LoadFinishGrid();
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
                List<TimmingBetOverUnder> allBetData = DataStore.TimmingBetOverUnder
                    .OrderBy(item => item.MatchId)
                    .ThenBy(item => item.LivePeriod)
                    .ThenBy(item => item.Minute)
                    .ToList();

                List<LiveBetApp.Models.ViewModels.TimmingBetOverUnder> allBetView = new List<Models.ViewModels.TimmingBetOverUnder>();
                for (int i = 0; i < allBetData.Count; i++)
                {
                    Match matchOfThisBet;
                    if (DataStore.Matchs.TryGetValue(allBetData[i].MatchId, out matchOfThisBet))
                    {
                        allBetView.Add(new LiveBetApp.Models.ViewModels.TimmingBetOverUnder()
                        {
                            Id = allBetData[i].Id,
                            LivePeriod = allBetData[i].LivePeriod,
                            MatchId = allBetData[i].MatchId,
                            Minute = allBetData[i].Minute,
                            Stake = allBetData[i].Stake,
                            Away = matchOfThisBet.Away,
                            Home = matchOfThisBet.Home,
                            IsFulltime = allBetData[i].IsFulltime,
                            IsOver = allBetData[i].IsOver,
                            CreateDateTime = allBetData[i].CreateDateTime,
                            ResultMessage = allBetData[i].ResultMessage,
                            AutoBetMessage = allBetData[i].AutoBetMessage
                        });
                    }
                }

                allBetView = allBetView.OrderByDescending(item => item.CreateDateTime).ToList();

                bindingSource.DataSource = Common.Functions.ToDataTable(allBetView);
                dataGridView.DataSource = bindingSource;
                dataGridView.Columns["Id"].Visible = false;
                dataGridView.Columns["MatchId"].Visible = false;
                dataGridView.Columns["AutoBetMessage"].Visible = false;
                dataGridView.Columns["Away"].Width = 200;
                dataGridView.Columns["Home"].Width = 200;

                dataGridView.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
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
                        DataStore.TimmingBetOverUnder.RemoveAll(item => item.Id == id);
                    }
                    catch { }
                    LoadMainGrid();
                }
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
                List<TimmingBetOverUnder> allBetData = DataStore.FinishedTimmingBetOverUnder
                    .OrderBy(item => item.MatchId)
                    .ThenBy(item => item.LivePeriod)
                    .ThenBy(item => item.Minute)
                    .ToList();

                List<LiveBetApp.Models.ViewModels.TimmingBetOverUnder> allBetView = new List<Models.ViewModels.TimmingBetOverUnder>();
                for (int i = 0; i < allBetData.Count; i++)
                {
                    Match matchOfThisBet;
                    if (DataStore.Matchs.TryGetValue(allBetData[i].MatchId, out matchOfThisBet))
                    {
                        allBetView.Add(new LiveBetApp.Models.ViewModels.TimmingBetOverUnder()
                        {
                            Id = allBetData[i].Id,
                            LivePeriod = allBetData[i].LivePeriod,
                            MatchId = allBetData[i].MatchId,
                            Minute = allBetData[i].Minute,
                            Stake = allBetData[i].Stake,
                            Away = matchOfThisBet.Away,
                            Home = matchOfThisBet.Home,
                            IsOver = allBetData[i].IsOver,
                            IsFulltime = allBetData[i].IsFulltime,
                            ResultMessage = allBetData[i].ResultMessage,
                            CreateDateTime = allBetData[i].CreateDateTime,
                            AutoBetMessage = allBetData[i].AutoBetMessage
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
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
                });
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
                        DataStore.FinishedTimmingBetOverUnder.RemoveAll(item => item.Id == id);
                    }
                    catch { }
                    LoadFinishGrid();
                }
            }
        }

        private void AllTimingBetOverForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }

    }
}
