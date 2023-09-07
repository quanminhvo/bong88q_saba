﻿using LiveBetApp.Models.DataModels;
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
    public partial class AlertForm_7 : Form
    {
        private Thread _executeThread;

        public AlertForm_7()
        {
            InitializeComponent();
        }

        private void AlertForm_7_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Update_WD7();
                        Common.Functions.HighlightNewRowsAlert(this.dgvWd7, DataStore.Alert_Wd7);

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

        private delegate void dlgUpdate_WD7();
        private void Update_WD7()
        {
            List<long> matchids = DataStore.Alert_Wd7
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
                matchs[i].CustomValue = DataStore.Alert_Wd7[matchs[i].MatchId].CustomValue;
            }
            
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);

            if (this.dgvWd7.InvokeRequired)
            {
                this.Invoke(new dlgUpdate_WD7(Update_WD7));
            }
            else
            {
                dgvWd7.Columns.Clear();
                dgvWd7.DataSource = bindingSource;
                dgvWd7.Columns["MatchId"].Visible = false;
                dgvWd7.Columns["CustomValue"].Visible = false;
                dgvWd7.Columns.Add(new DataGridViewButtonColumn()
                {
                    UseColumnTextForButtonValue = true,
                    Text = "del",
                    Name = "deleteRowBtn",
                    HeaderText = "Delete"
                });
            }
        }

        private void dgvWd7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvWd7.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    long matchId = (long)dgvWd7.Rows[e.RowIndex].Cells["MatchId"].Value;
                    DataStore.Alert_Wd7[matchId].Deleted = true;
                    Update_WD7();
                }
            }
        }

        private void AlertForm_7_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();

            List<long> matchIds = DataStore.Alert_Wd7.Keys.ToList();
            for (int i = 0; i < matchIds.Count; i++)
            {
                DataStore.Alert_Wd7[matchIds[i]].IsNew = false;
            }
        }

        private void dgvWd7_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)dgvWd7.Rows[e.RowIndex].Cells["MatchId"].Value;
            if (e.RowIndex < 0) return;
            OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
            form.Show();
        }

    }
}
