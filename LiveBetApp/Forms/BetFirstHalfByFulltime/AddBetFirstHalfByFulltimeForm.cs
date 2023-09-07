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

namespace LiveBetApp.Forms.BetFirstHalfByFulltime
{
    public partial class AddBetFirstHalfByFulltimeForm : Form
    {
        private long _matchId;

        public AddBetFirstHalfByFulltimeForm(long matchId)
        {
            this._matchId = matchId;
            InitializeComponent();
            Init();
            InitRdCareGoal();
        }

        private void AddBetFirstHalfByFulltimeForm_Load(object sender, EventArgs e)
        {

        }

        private void Init()
        {
            InitCbbOU();
            InitCbbHdp();
            LoadMainGrid();

            dgvMain.Columns.Add(new DataGridViewButtonColumn()
            {
                UseColumnTextForButtonValue = true,
                Text = "del",
                Name = "deleteRowBtn",
                HeaderText = "Delete"
            });
            dgvMain.Columns["Id"].Visible = false;
            dgvMain.Columns["MatchId"].Visible = false;
            dgvMain.Columns["AutoBetMessage"].Visible = false;
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

        private void InitCbbHdp()
        {
            List<LiveBetApp.Models.ViewModels.ComboboxItem> hdp = new List<LiveBetApp.Models.ViewModels.ComboboxItem>();

            hdp.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "Max", Value = true });
            hdp.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "Min", Value = false });

            cbbHdp.DataSource = hdp;
            cbbHdp.DisplayMember = "Text";
            cbbHdp.ValueMember = "Value";
        }

        private void LoadMainGrid()
        {
            BindingSource bindingSource = new BindingSource();
            List<Models.DataModels.BetFirstHalfByFulltime> betsOfThisMatch = DataStore.BetFirstHalfByFulltime
                .Where(item => item.MatchId == this._matchId)
                .OrderBy(item => item.Hdp)
                .ThenBy(item => item.Price)
                .ToList();
            bindingSource.DataSource = Common.Functions.ToDataTable(betsOfThisMatch);
            dgvMain.DataSource = bindingSource;

        }

        private void InitRdCareGoal()
        {
            SetEnableRdCareGoal(false);
        }

        private void SetEnableRdCareGoal(bool enable)
        {
            rdCareGoal_1.Enabled = enable;
            rdCareGoal_2.Enabled = enable;
            rdCareGoal_3.Enabled = enable;
            rdCareGoal_4.Enabled = enable;
        }

        private int GetGoalLimit()
        {
            if (cbCareGoal.Checked)
            {
                return 100;
            }
            else
            {
                if (rdCareGoal_1.Checked) return 1;
                if (rdCareGoal_2.Checked) return 2;
                if (rdCareGoal_3.Checked) return 3;
                if (rdCareGoal_4.Checked) return 4;
            }
            return 100;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Match match;
            if (DataStore.Matchs.TryGetValue(_matchId, out match))
            {
                DataStore.BetFirstHalfByFulltime.Add(new Models.DataModels.BetFirstHalfByFulltime()
                {
                    Id = Guid.NewGuid(),
                    MatchId = _matchId,
                    Hdp = (int)this.numFulltimeHdp.Value,
                    Price = (int)numPrice.Value,
                    Stake = (int)this.numStake.Value < 3 ? 3 : (int)this.numStake.Value,
                    IsOver = (bool)this.cbbOU.SelectedValue,
                    BetMax = (bool)this.cbbHdp.SelectedValue,
                    TotalScore = match.LiveHomeScore + match.LiveAwayScore,
                    TotalRed = match.HomeRed + match.AwayRed,
                    MoneyLine = match.OverUnderMoneyLine,
                    CreateDateTime = DateTime.Now,
                    CareGoal = cbCareGoal.Checked,
                    GoalLimit = GetGoalLimit()
                });
                LoadMainGrid();
            }
        }

        private void cbCareGoal_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCareGoal.Checked)
            {
                SetEnableRdCareGoal(false);
            }
            else
            {
                SetEnableRdCareGoal(true);
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

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataStore.BetFirstHalfByFulltime.RemoveAll(item => item.MatchId == this._matchId);
                LoadMainGrid();
            }
        }
    }
}
