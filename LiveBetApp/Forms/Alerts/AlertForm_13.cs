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
    public partial class AlertForm_13 : Form
    {
        private Thread _executeThread;

        public AlertForm_13 ()
        {
            InitializeComponent();
        }

        private void AlertForm_13_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Update_WD13();
                        Common.Functions.HighlightNewRowsAlert(this.dgvWd13, DataStore.Alert_Wd13);

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

        private delegate void dlgUpdate_WD13();
        private void Update_WD13()
        {
            List<long> matchids = DataStore.Alert_Wd13
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
                matchs[i].CustomValue = DataStore.Alert_Wd13[matchs[i].MatchId].CustomValue;
            }
            
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);

            if (this.dgvWd13.InvokeRequired)
            {
                this.Invoke(new dlgUpdate_WD13(Update_WD13));
            }
            else
            {
                dgvWd13.Columns.Clear();
                dgvWd13.DataSource = bindingSource;
                dgvWd13.Columns["MatchId"].Visible = false;
                //dgvWd13.Columns["CustomValue"].Visible = false;
                dgvWd13.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
                });
            }
        }

        private void dgvWd13_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvWd13.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    long matchId = (long)dgvWd13.Rows[e.RowIndex].Cells["MatchId"].Value;
                    DataStore.Alert_Wd13[matchId].Deleted = true;
                    Update_WD13();
                }
            }
        }

        private void dgvWd13_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)dgvWd13.Rows[e.RowIndex].Cells["MatchId"].Value;
            if (e.RowIndex < 0) return;
            OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
            form.Show();
        }

        private void AlertForm_13_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();

            List<long> matchIds = DataStore.Alert_Wd13.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                DataStore.Alert_Wd13[matchIds[i]].IsNew = false;
            }
        }

    }
}
