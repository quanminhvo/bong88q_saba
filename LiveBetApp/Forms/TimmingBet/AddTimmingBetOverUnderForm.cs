using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms.TimmingBet
{
    public partial class AddTimmingBetOverUnderForm : Form
    {
        private long _matchId;

        public AddTimmingBetOverUnderForm(long matchId)
        {
            _matchId = matchId;
            InitializeComponent();
            Init();
        }

        private void AddTimmingBetOverForm_Load(object sender, EventArgs e)
        {
            Match match;
            if (DataStore.Matchs.TryGetValue(_matchId, out match))
            {
                this.Text = match.Home + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")" + " | TIMING BET OVER";
            }
        }

        private void Init()
        {

            InitCbbSet();
            InitCbbMinute();
            InitCbbOU();
            InitCbbFtHt();
            LoadMainGrid();

            dataGridView.Columns.Add(new DataGridViewButtonColumn()
            {
                UseColumnTextForButtonValue = true,
                Text = "del",
                Name = "deleteRowBtn",
                HeaderText = "Delete"
            });
            dataGridView.Columns["Id"].Visible = false;
            dataGridView.Columns["MatchId"].Visible = false; 


        }

        private void LoadMainGrid()
        {
            BindingSource bindingSource = new BindingSource();
            List<TimmingBetOverUnder> betsOfThisMatch = DataStore.TimmingBetOverUnder
                .Where(item => item.MatchId == this._matchId)
                .OrderBy(item => item.LivePeriod)
                .ThenBy(item => item.Minute)
                .ToList();
            bindingSource.DataSource = Common.Functions.ToDataTable(betsOfThisMatch);
            dataGridView.DataSource = bindingSource;

        }

        private void InitCbbSet()
        {
            cbbLivePeriod.DataSource = new List<LiveBetApp.Models.ViewModels.ComboboxItem>() {
                new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "FH", Value = 1},
                new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "FT", Value = 2}
            };
            cbbLivePeriod.DisplayMember = "Text";
            cbbLivePeriod.ValueMember = "Value";
        }

        private void InitCbbMinute()
        {
            List<LiveBetApp.Models.ViewModels.ComboboxItem> listMinute = new List<LiveBetApp.Models.ViewModels.ComboboxItem>();
            for (int i = 1; i <= 45; i++)
            {
                listMinute.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = i + "'", Value = i });
            }
            cbbMinute.DataSource = listMinute;
            cbbMinute.DisplayMember = "Text";
            cbbMinute.ValueMember = "Value";
        }

        private void InitCbbOU()
        {
            List<LiveBetApp.Models.ViewModels.ComboboxItem> overUnder = new List<LiveBetApp.Models.ViewModels.ComboboxItem>();

            overUnder.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "Over", Value = true });
            overUnder.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "Under", Value = false });

            cbbOU.DataSource = overUnder;
            cbbOU.DisplayMember = "Text";
            cbbOU.ValueMember = "Value";
        }

        private void InitCbbFtHt()
        {
            List<LiveBetApp.Models.ViewModels.ComboboxItem> fulltimeHalftime = new List<LiveBetApp.Models.ViewModels.ComboboxItem>();

            fulltimeHalftime.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "Fulltime", Value = true });
            fulltimeHalftime.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "First Half", Value = false });

            cbbFtHt.DataSource = fulltimeHalftime;
            cbbFtHt.DisplayMember = "Text";
            cbbFtHt.ValueMember = "Value";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Match match;
            if (DataStore.Matchs.TryGetValue(_matchId, out match))
            {
                if (match.LivePeriod < (int)this.cbbLivePeriod.SelectedValue && match.LivePeriod < 0)
                {
                    return;
                }
                else if ((int)match.TimeSpanFromStart.TotalMinutes >= (int)this.cbbMinute.SelectedValue)
                {
                    return;
                }

                DataStore.TimmingBetOverUnder.Add(new Models.DataModels.TimmingBetOverUnder()
                {
                    Id = Guid.NewGuid(),
                    MatchId = _matchId,
                    LivePeriod = (int)this.cbbLivePeriod.SelectedValue,
                    Minute = (int)this.cbbMinute.SelectedValue,
                    Stake = (int)this.numStake.Value < 3 ? 3 : (int)this.numStake.Value,
                    IsOver = (bool)this.cbbOU.SelectedValue,
                    IsFulltime = (bool)this.cbbFtHt.SelectedValue,
                    TotalScore = match.LiveHomeScore + match.LiveAwayScore,
                    TotalRed = match.HomeRed + match.AwayRed,
                    CreateDateTime = DateTime.Now,
                    CareGoal = cbCareGoal.Checked
                });
                LoadMainGrid();
            }

        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Guid id = (Guid)dataGridView.Rows[e.RowIndex].Cells["Id"].Value;
                    DataStore.TimmingBetOverUnder.RemoveAll(item => item.Id == id);
                    LoadMainGrid();
                }
            }
        }

        private void cbbFtHt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((bool)this.cbbFtHt.SelectedValue)
                {
                    this.cbbLivePeriod.Enabled = true;
                }
                else
                {
                    this.cbbLivePeriod.Enabled = false;
                    this.cbbLivePeriod.SelectedIndex = 0;
                }
            }
            catch
            {

            }

        }

        private void btnStake3_Click(object sender, EventArgs e)
        {
            this.numStake.Value = 100;
        }

        private void btnStake100_Click(object sender, EventArgs e)
        {
            this.numStake.Value = 300;
        }

        private void btnStake500_Click(object sender, EventArgs e)
        {
            this.numStake.Value = 500;
        }

        private void btnStake1000_Click(object sender, EventArgs e)
        {
            this.numStake.Value = 1000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataStore.TimmingBetOverUnder.RemoveAll(item => item.MatchId == this._matchId);
                LoadMainGrid();
            }
        }
    }
}
