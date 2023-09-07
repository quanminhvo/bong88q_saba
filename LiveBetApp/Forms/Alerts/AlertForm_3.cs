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
    public partial class AlertForm_3 : Form
    {
        private Thread _executeThread;

        public AlertForm_3()
        {
            InitializeComponent();
        }

        private void AlertForm_3_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Update_WD3();
                        //Common.Functions.HighlightNewRowsAlert(this.dgvWd3, DataStore.Alert_Wd3);

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

        private delegate void dlgUpdate_WD3();
        private void Update_WD3()
        {
            List<long> matchids = DataStore.Alert_Wd3
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
                matchs[i].CustomValue = DataStore.Alert_Wd3[matchs[i].MatchId].CustomValue;
            }
            
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);

            if (this.dgvWd3.InvokeRequired)
            {
                this.Invoke(new dlgUpdate_WD3(Update_WD3));
            }
            else
            {
                dgvWd3.Columns.Clear();
                dgvWd3.DataSource = bindingSource;
                dgvWd3.Columns["MatchId"].Visible = false;
                dgvWd3.Columns["CustomValue"].Visible = false;
                dgvWd3.Columns.Add(new DataGridViewButtonColumn()
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
            for (int i = 0; i < dgvWd3.RowCount; i++)
            {
                string color = dgvWd3.Rows[i].Cells["CustomValue"].Value.ToString();
                if (color == "4")
                {
                    dgvWd3.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                }
                else if (color == "1")
                {
                    dgvWd3.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (color == "2")
                {
                    dgvWd3.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else if (color == "3")
                {
                    dgvWd3.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
            }
        }

        private void dgvWd3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvWd3.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    long matchId = (long)dgvWd3.Rows[e.RowIndex].Cells["MatchId"].Value;
                    DataStore.Alert_Wd3[matchId].Deleted = true;
                    Update_WD3();
                }
            }
        }

        private void dgvWd3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)dgvWd3.Rows[e.RowIndex].Cells["MatchId"].Value;
            if (e.RowIndex < 0) return;
            OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
            form.Show();
        }

        private void AlertForm_3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();

            List<long> matchIds = DataStore.Alert_Wd3.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                DataStore.Alert_Wd3[matchIds[i]].IsNew = false;
            }
        }

    }
}
