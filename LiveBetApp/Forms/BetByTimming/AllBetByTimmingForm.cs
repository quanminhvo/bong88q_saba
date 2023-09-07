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

namespace LiveBetApp.Forms.BetByTimming
{
    public partial class AllBetByTimmingForm : Form
    {
        private Thread _executeThread;
        public AllBetByTimmingForm()
        {
            InitializeComponent();
        }

        private void AllBetByTimmingForm_Load(object sender, EventArgs e)
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
                List<Models.DataModels.BetByTimming> allBetData = DataStore.BetByTimming
                    .OrderBy(item => item.MatchId)
                    .ThenBy(item => item.Minute)
                    .ThenBy(item => item.BetOption)
                    .ToList();

                bindingSource.DataSource = Common.Functions.ToDataTable(allBetData);
                dataGridView.DataSource = bindingSource;
                dataGridView.Columns["Id"].Visible = false;
                dataGridView.Columns["MatchId"].Visible = false;
                dataGridView.Columns["AutoBetMessage"].Visible = false;

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
                List<Models.DataModels.BetByTimming> allBetData = DataStore.FinishedBetByTimming
                    .OrderBy(item => item.MatchId)
                    .ThenBy(item => item.Minute)
                    .ThenBy(item => item.BetOption)
                    .ToList();

                bindingSource.DataSource = Common.Functions.ToDataTable(allBetData);
                dgvFinished.DataSource = bindingSource;
                dgvFinished.Columns["Id"].Visible = false;
                dgvFinished.Columns["MatchId"].Visible = false;
                dgvFinished.Columns["AutoBetMessage"].Visible = false;

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

        private void AllBetByTimmingForm_FormClosing(object sender, FormClosingEventArgs e)
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
                    Guid id = (Guid)dataGridView.Rows[e.RowIndex].Cells["Id"].Value;
                    DataStore.BetByTimming.RemoveAll(item => item.Id == id);
                    LoadMainGrid();
                }
            }
            else if (e.ColumnIndex == dataGridView.Columns["sheetTableRowBtn"].Index)
            {
                Guid id = (Guid)dataGridView.Rows[e.RowIndex].Cells["Id"].Value;
                Models.DataModels.BetByTimming rowData = DataStore.BetByTimming.FirstOrDefault(item => item.Id == id);
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
                    DataStore.BetByTimming.RemoveAll(item => true);
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
                    Guid id = (Guid)dgvFinished.Rows[e.RowIndex].Cells["Id"].Value;
                    DataStore.FinishedBetByTimming.RemoveAll(item => item.Id == id);
                    LoadFinishGrid();
                }
            }
            else if (e.ColumnIndex == dgvFinished.Columns["sheetTableRowBtn"].Index)
            {
                Guid id = (Guid)dgvFinished.Rows[e.RowIndex].Cells["Id"].Value;
                Models.DataModels.BetByTimming rowData = DataStore.FinishedBetByTimming.FirstOrDefault(item => item.Id == id);
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
                    DataStore.FinishedBetByTimming.RemoveAll(item => true);
                    LoadFinishGrid();
                }
            }
        }
    }
}
