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


namespace LiveBetApp.Forms.Alerts
{
    public partial class AlertForm_12 : Form
    {
        private Thread _executeThread;
        public AlertForm_12()
        {
            InitializeComponent();
        }

        private void AlertForm_12_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Update_WD12();
                        Common.Functions.HighlightNewRowsAlert(this.dgvWd12, DataStore.Alert_Wd12);

                        Thread.Sleep(60000);
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

        private delegate void dlgUpdate_WD12();
        private void Update_WD12()
        {
            List<long> matchids = DataStore.Alert_Wd12
                .Where(item => item.Value.Deleted == false)
                .OrderByDescending(item => item.Value.IsNew)
                .Select(item => item.Key).ToList();
            List<Match> matchFromDatastore = new List<Match>();
            for (int i = 0; i < matchids.Count; i++)
            {
                matchFromDatastore.Add(DataStore.Matchs[matchids[i]]);
            }

            List<LiveBetApp.Models.ViewModels.AlertMatch> matchs = AutoMapper.Mapper.Map<List<LiveBetApp.Models.ViewModels.AlertMatch>>(matchFromDatastore);
            for (int i = 0; i < matchs.Count; i++)
            {
                matchs[i].CustomValue = DataStore.Alert_Wd12[matchs[i].MatchId].CustomValue;
            }

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);

            if (this.dgvWd12.InvokeRequired)
            {
                this.Invoke(new dlgUpdate_WD12(Update_WD12));
            }
            else
            {
                dgvWd12.Columns.Clear();
                dgvWd12.DataSource = bindingSource;
                dgvWd12.Columns["MatchId"].Visible = false;
                dgvWd12.Columns["CustomValue"].Visible = false;
                dgvWd12.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
                });
            }
        }

        private void dgvWd12_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvWd12.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    long matchId = (long)dgvWd12.Rows[e.RowIndex].Cells["MatchId"].Value;
                    DataStore.Alert_Wd12[matchId].Deleted = true;
                    Update_WD12();
                }
            }
        }

        private void dgvWd12_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)dgvWd12.Rows[e.RowIndex].Cells["MatchId"].Value;
            if (e.RowIndex < 0) return;
            OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
            form.Show();
        }

        private void AlertForm_12_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();

            List<long> matchIds = DataStore.Alert_Wd12.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                DataStore.Alert_Wd12[matchIds[i]].IsNew = false;
            }
        }

    }
}
