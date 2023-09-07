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
    public partial class AlertForm_15 : Form
    {
        private Thread _executeThread;

        public AlertForm_15()
        {
            InitializeComponent();
        }

        private void AlertForm_15_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Update_WD15();
                        ////Common.Functions.HighlightNewRowsAlert(this.dgvWd15, DataStore.Alert_Wd15);

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

        private delegate void dlgUpdate_WD15();
        private void Update_WD15()
        {
            List<long> matchids = DataStore.Alert_Wd15
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
                matchs[i].CustomValue = DataStore.Alert_Wd15[matchs[i].MatchId].CustomValue;
            }
            
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);

            if (this.dgvWd15.InvokeRequired)
            {
                this.Invoke(new dlgUpdate_WD15(Update_WD15));
            }
            else
            {
                dgvWd15.Columns.Clear();
                dgvWd15.DataSource = bindingSource;
                dgvWd15.Columns["MatchId"].Visible = false;
                dgvWd15.Columns["CustomValue"].Visible = false;
                dgvWd15.Columns.Add(new DataGridViewButtonColumn()
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
            for (int i = 0; i < dgvWd15.RowCount; i++)
            {
                string color = dgvWd15.Rows[i].Cells["CustomValue"].Value.ToString();
                if (color == "LightPink")
                {
                    dgvWd15.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                }
                else if (color == "SkyBlue")
                {
                    dgvWd15.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                }
                else if (color == "LightYellow")
                {
                    dgvWd15.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else if (color == "LightGreen")
                {
                    dgvWd15.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        private void dgvWd15_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvWd15.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    long matchId = (long)dgvWd15.Rows[e.RowIndex].Cells["MatchId"].Value;
                    DataStore.Alert_Wd15[matchId].Deleted = true;
                    Update_WD15();
                }
            }
        }

        private void dgvWd15_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)dgvWd15.Rows[e.RowIndex].Cells["MatchId"].Value;
            if (e.RowIndex < 0) return;
            OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
            form.Show();
        }

        private void AlertForm_15_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();

            List<long> matchIds = DataStore.Alert_Wd15.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                DataStore.Alert_Wd15[matchIds[i]].IsNew = false;
            }
        }

    }
}
