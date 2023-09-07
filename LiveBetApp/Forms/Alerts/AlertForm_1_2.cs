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
    public partial class AlertForm_1_2 : Form
    {
        private Thread _executeThread;

        public AlertForm_1_2()
        {
            InitializeComponent();
        }

        private void AlertFormA_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Update_WD1();
                        Update_WD2();

                        //Common.Functions.HighlightNewRowsAlert(this.dgvWd1, DataStore.Alert_Wd1);
                        //Common.Functions.HighlightNewRowsAlert(this.dgvWd2, DataStore.Alert_Wd2);

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

        private void SetStyle_WD1()
        {
            for (int i = 0; i < dgvWd1.RowCount; i++)
            {
                string value = dgvWd1.Rows[i].Cells["CustomValue"].Value.ToString();
                string fontColor = value.Split('_')[0];
                string fontStyle = value.Split('_')[1];
                string backGroundColor = value.Split('_')[2];


                this.dgvWd1.Rows[i].DefaultCellStyle = new DataGridViewCellStyle(this.dgvWd1.Rows[i].DefaultCellStyle) { 
                    Font = new Font(
                        this.dgvWd1.DefaultCellStyle.Font, 
                        (fontStyle == "bold" ? FontStyle.Bold : FontStyle.Regular)
                    ), 
                    ForeColor = (fontColor == "red" ? Color.Red : Color.Black),
                    BackColor = (backGroundColor == "blue" ? Color.SkyBlue : backGroundColor == "yellow" ? Color.GreenYellow : this.dgvWd1.Rows[i].DefaultCellStyle.BackColor)
                };
            }
        }

        private delegate void dlgUpdate_WD1();
        private void Update_WD1()
        {
            List<long> matchids = DataStore.Alert_Wd1
                .Where(item => item.Value.Deleted == false)
                .OrderBy(item => item.Value.CreateTime)
                .Select(item => item.Key).ToList();
            List<Match> matchFromDatastore = new List<Match>();
            for (int i=0; i<matchids.Count; i++)
            {
                matchFromDatastore.Add(DataStore.Matchs[matchids[i]]);
            }

            List<LiveBetApp.Models.ViewModels.AlertMatch> matchs = AutoMapper.Mapper.Map<List<LiveBetApp.Models.ViewModels.AlertMatch>>(matchFromDatastore);
            for (int i = 0; i < matchs.Count; i++)
            {
                matchs[i].CustomValue = DataStore.Alert_Wd1[matchs[i].MatchId].CustomValue;
            }
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);

            if (this.dgvWd1.InvokeRequired)
            {
                this.Invoke(new dlgUpdate_WD1(Update_WD1));
            }
            else
            {
                dgvWd1.Columns.Clear();
                dgvWd1.DataSource = bindingSource;
                dgvWd1.Columns["MatchId"].Visible = false;
                dgvWd1.Columns["CustomValue"].Visible = false;
                dgvWd1.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
                });

            }
            SetStyle_WD1();
        }

        private delegate void dlgUpdate_WD2();
        private void Update_WD2()
        {
            List<long> matchids = DataStore.Alert_Wd2
                .Where(item => item.Value.Deleted == false)
                .OrderByDescending(item => item.Value.IsNew)
                .Select(item => item.Key)
                .ToList();
            List<Match> matchFromDatastore = new List<Match>();
            for (int i = 0; i < matchids.Count; i++)
            {
                matchFromDatastore.Add(DataStore.Matchs[matchids[i]]);
            }

            List<LiveBetApp.Models.ViewModels.AlertMatch> matchs = AutoMapper.Mapper.Map<List<LiveBetApp.Models.ViewModels.AlertMatch>>(matchFromDatastore);
            for (int i = 0; i < matchs.Count; i++)
            {
                matchs[i].CustomValue = DataStore.Alert_Wd2[matchs[i].MatchId].CustomValue;
            }
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);

            if (this.dgvWd2.InvokeRequired)
            {
                this.Invoke(new dlgUpdate_WD2(Update_WD2));
            }
            else
            {
                dgvWd2.Columns.Clear();
                dgvWd2.DataSource = bindingSource;
                dgvWd2.Columns["MatchId"].Visible = false;
                dgvWd2.Columns["CustomValue"].Visible = false;
                dgvWd2.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
                });
            }
        }

        private void AlertFormA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();

            List<long> matchIds = DataStore.Alert_Wd1.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                DataStore.Alert_Wd1[matchIds[i]].IsNew = false;
            }

            matchIds = DataStore.Alert_Wd2.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                DataStore.Alert_Wd2[matchIds[i]].IsNew = false;
            }
        }

        private void dgbWd1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)dgvWd1.Rows[e.RowIndex].Cells["MatchId"].Value;
            if (e.RowIndex < 0) return;
            OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
            form.Show();
        }

        private void dgbWd1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvWd1.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    long matchId = (long)dgvWd1.Rows[e.RowIndex].Cells["MatchId"].Value;
                    DataStore.Alert_Wd1[matchId].Deleted = true;
                    Update_WD1();
                }
            }
            
        }

        private void dgvWd2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)dgvWd2.Rows[e.RowIndex].Cells["MatchId"].Value;
            if (e.RowIndex < 0) return;
            OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
            form.Show();
        }

        private void dgvWd2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvWd2.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    long matchId = (long)dgvWd2.Rows[e.RowIndex].Cells["MatchId"].Value;
                    DataStore.Alert_Wd2[matchId].Deleted = true;
                    Update_WD2();
                }
            }
        }

    }
}
