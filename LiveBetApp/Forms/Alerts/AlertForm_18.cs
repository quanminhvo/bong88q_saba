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
    public partial class AlertForm_18 : Form
    {
        private Thread _executeThread;
        public AlertForm_18()
        {
            InitializeComponent();
        }

        private void AlertForm_18_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Update_WD18();
                        //Common.Functions.HighlightNewRowsAlert(this.dgvWd17, DataStore.Alert_Wd17);

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

        private delegate void dlgUpdate_WD18();
        private void Update_WD18()
        {
            List<long> matchids = DataStore.Alert_Wd18
                .Where(item => item.Value.Deleted == false)
                .OrderBy(item => item.Value.CreateTime)
                .Select(item => item.Key).ToList();
            List<Match> matchFromDatastore = new List<Match>();
            for (int i = 0; i < matchids.Count; i++)
            {
                matchFromDatastore.Add(DataStore.Matchs[matchids[i]]);
            }

            List<LiveBetApp.Models.ViewModels.AlertMatch> matchs = AutoMapper.Mapper.Map<List<LiveBetApp.Models.ViewModels.AlertMatch>>(matchFromDatastore);
            for (int i = 0; i < matchs.Count; i++)
            {
                matchs[i].CustomValue = DataStore.Alert_Wd18[matchs[i].MatchId].CustomValue;
            }

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);

            if (this.dgvWd18.InvokeRequired)
            {
                this.Invoke(new dlgUpdate_WD18(Update_WD18));
            }
            else
            {
                dgvWd18.Columns.Clear();
                dgvWd18.DataSource = bindingSource;
                dgvWd18.Columns["MatchId"].Visible = false;
                dgvWd18.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
                });
            }
            SetStyle();
        }
        private void SetStyle()
        {
            for (int i = 0; i < dgvWd18.RowCount; i++)
            {
                long matchId = (long)dgvWd18.Rows[i].Cells["MatchId"].Value;
                if (DataStore.Matchs.ContainsKey(matchId)
                    && (DataStore.Matchs[matchId].LivePeriod > 0 || DataStore.Matchs[matchId].IsHT))
                {
                    dgvWd18.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
                else
                {
                    dgvWd18.Rows[i].DefaultCellStyle.BackColor = dgvWd18.DefaultCellStyle.BackColor;
                }
            }
        }

        private void dgvWd18_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)dgvWd18.Rows[e.RowIndex].Cells["MatchId"].Value;
            if (e.RowIndex < 0) return;
            OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
            form.Show();
        }

        private void dgvWd18_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvWd18.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    long matchId = (long)dgvWd18.Rows[e.RowIndex].Cells["MatchId"].Value;
                    DataStore.Alert_Wd18[matchId].Deleted = true;
                    Update_WD18();
                }
            }
        }

        private void AlertForm_18_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();

            List<long> matchIds = DataStore.Alert_Wd18.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                DataStore.Alert_Wd18[matchIds[i]].IsNew = false;
            }
        }
    }
}
