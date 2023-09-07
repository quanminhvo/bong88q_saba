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

namespace LiveBetApp.Forms.BetHandicapByPrice
{
    public partial class AddBetByHandicapPriceForm : Form
    {
        private long _matchId;
        public AddBetByHandicapPriceForm(long matchId)
        {
            this._matchId = matchId;
            InitializeComponent();
            Init();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            Match match;
            if (DataStore.Matchs.TryGetValue(_matchId, out match))
            {
                if (match.LivePeriod < 0)
                {
                    return;
                }

                DataStore.BetByHandicapHdpPrice.Add(new Models.DataModels.BetByHdpPrice()
                {
                    Id = Guid.NewGuid(),
                    MatchId = _matchId,
                    //LivePeriod = (int)this.cbbLivePeriod.SelectedValue,
                    Hdp = (int)this.numHdp.Value,
                    Price = (int)numPrice.Value,
                    Stake = (int)this.numStake.Value < 3 ? 3 : (int)this.numStake.Value,
                    UpToMinute = 150,
                    IsOver = (bool)this.cbbOU.SelectedValue,
                    TeamStronger = ((Common.Enums.BetHandicapTeamStronger)cbbTeamStronger.SelectedValue),
                    IsFulltime = (bool)this.cbbFtHt.SelectedValue,
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

        private void LoadMainGrid()
        {
            BindingSource bindingSource = new BindingSource();
            List<Models.DataModels.BetByHdpPrice> betsOfThisMatch = DataStore.BetByHandicapHdpPrice
                .Where(item => item.MatchId == this._matchId)
                .OrderBy(item => item.LivePeriod)
                .ThenBy(item => item.Hdp)
                .ThenBy(item => item.Price)
                .ToList();
            List<Models.ViewModels.BetByHdpPriceViewModel> betsOfThisMatchViewModels = AutoMapper.Mapper.Map<List<Models.ViewModels.BetByHdpPriceViewModel>>(betsOfThisMatch);
            
            bindingSource.DataSource = Common.Functions.ToDataTable(betsOfThisMatchViewModels);
            dataGridView.DataSource = bindingSource;

        }

        private void Init()
        {
            InitCbbSet();
            InitCbbOU();
            InitCbbFtHt();
            InitCbbTeamStronger();
            InitRdCareGoal();
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
            dataGridView.Columns["AutoBetMessage"].Visible = false;
        }

        private void InitCbbSet()
        {
            //cbbLivePeriod.DataSource = new List<LiveBetApp.Models.ViewModels.ComboboxItem>() {
            //    new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "1H", Value = 1},
            //    new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "2H", Value = 2}
            //};
            //cbbLivePeriod.DisplayMember = "Text";
            //cbbLivePeriod.ValueMember = "Value";
        }

        private void InitCbbOU()
        {
            List<LiveBetApp.Models.ViewModels.ComboboxItem> overUnder = new List<LiveBetApp.Models.ViewModels.ComboboxItem>();

            overUnder.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "Home", Value = true });
            overUnder.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "Away", Value = false });

            cbbOU.DataSource = overUnder;
            cbbOU.DisplayMember = "Text";
            cbbOU.ValueMember = "Value";
        }

        private void InitCbbTeamStronger()
        {
            List<LiveBetApp.Models.ViewModels.ComboboxItem> items = new List<LiveBetApp.Models.ViewModels.ComboboxItem>();


            items.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "Home", Value = LiveBetApp.Common.Enums.BetHandicapTeamStronger.Home });
            items.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "Same", Value = LiveBetApp.Common.Enums.BetHandicapTeamStronger.Same });
            items.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "Away", Value = LiveBetApp.Common.Enums.BetHandicapTeamStronger.Away });

            cbbTeamStronger.DataSource = items;
            cbbTeamStronger.DisplayMember = "Text";
            cbbTeamStronger.ValueMember = "Value";
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

        private void cbbFtHt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if ((bool)this.cbbFtHt.SelectedValue)
                //{
                //    this.cbbLivePeriod.Enabled = true;
                //}
                //else
                //{
                //    this.cbbLivePeriod.Enabled = false;
                //    this.cbbLivePeriod.SelectedIndex = 0;
                //}
            }
            catch
            {

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
                    DataStore.BetByHandicapHdpPrice.RemoveAll(item => item.Id == id);
                    LoadMainGrid();
                }
            }
        }

        private void AddBetByHandicapPriceForm_Load(object sender, EventArgs e)
        {

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
                DataStore.BetByHandicapHdpPrice.RemoveAll(item => item.MatchId == this._matchId);
                LoadMainGrid();
            }
        }
    }
}
