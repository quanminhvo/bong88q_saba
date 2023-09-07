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
    public partial class AlertForm_17 : Form
    {
        private Thread _executeThread;

        public AlertForm_17()
        {
            InitializeComponent();
        }

        private void AlertForm_17_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Update_WD17();
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

        private delegate void dlgUpdate_WD17();
        private void Update_WD17()
        {
            List<long> matchids = DataStore.Alert_Wd17
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
                matchs[i].CustomValue = DataStore.Alert_Wd17[matchs[i].MatchId].CustomValue;
            }
            
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);

            if (this.dgvWd17.InvokeRequired)
            {
                this.Invoke(new dlgUpdate_WD17(Update_WD17));
            }
            else
            {
                dgvWd17.Columns.Clear();
                dgvWd17.DataSource = bindingSource;
                dgvWd17.Columns["MatchId"].Visible = false;
                dgvWd17.Columns["CustomValue"].Visible = false;
                dgvWd17.Columns.Add(new DataGridViewButtonColumn()
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
            for (int i = 0; i < dgvWd17.RowCount; i++)
            {
                string color = dgvWd17.Rows[i].Cells["CustomValue"].Value.ToString();
                if (color == "1")
                {
                    dgvWd17.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (color == "2")
                {
                    dgvWd17.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                }
            }
        }

        private void dgvWd17_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)dgvWd17.Rows[e.RowIndex].Cells["MatchId"].Value;
            if (e.RowIndex < 0) return;
            OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
            form.Show();
        }

        private void dgvWd17_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvWd17.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    long matchId = (long)dgvWd17.Rows[e.RowIndex].Cells["MatchId"].Value;
                    DataStore.Alert_Wd17[matchId].Deleted = true;
                    Update_WD17();
                }
            }
        }

        private void AlertForm_17_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();

            List<long> matchIds = DataStore.Alert_Wd17.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                DataStore.Alert_Wd17[matchIds[i]].IsNew = false;
            }
        }

    }
}
