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

namespace LiveBetApp.Forms.BetByHdpClose
{
    public partial class AddBetByHdpCloseForm : Form
    {
        private long _matchId;
        public AddBetByHdpCloseForm(long matchId)
        {
            InitializeComponent();
            this._matchId = matchId;
        }

        private void AddBetByHdpCloseForm_Load(object sender, EventArgs e)
        {
            InitCbbBetOption();
            LoadMainGrid();
            InitRdCareGoal();
        }

        private void InitCbbBetOption()
        {
            List<Common.Enums.BetByHdpCloseOption> values = LiveBetApp.Common.Functions.EnumUtil.GetValues<Common.Enums.BetByHdpCloseOption>().ToList();

            List<LiveBetApp.Models.ViewModels.ComboboxItem> dataSource = new List<LiveBetApp.Models.ViewModels.ComboboxItem>();
            for (int i = 0; i < values.Count; i++)
            {
                dataSource.Add(new LiveBetApp.Models.ViewModels.ComboboxItem() {
                    Text = values[i].ToString(), 
                    Value = (int)values[i]
                });
            }

            cbbBetOption.DataSource = dataSource;
            cbbBetOption.DisplayMember = "Text";
            cbbBetOption.ValueMember = "Value";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Match match;
            if (DataStore.Matchs.TryGetValue(_matchId, out match))
            {
                DataStore.BetByHdpClose.Add(new Models.DataModels.BetByHdpClose()
                {
                    Id = Guid.NewGuid(),
                    MatchId = _matchId,
                    Home = match.Home,
                    Away = match.Away,
                    BetOption = (Common.Enums.BetByHdpCloseOption)cbbBetOption.SelectedValue,
                    MinuteAfterClose = (int)this.numMinuteAfterClose.Value,
                    FullTimeHdp = (int)this.numHdp.Value,
                    Stake = (int)this.numStake.Value < 3 ? 3 : (int)this.numStake.Value,
                    TotalScore = match.LiveHomeScore + match.LiveAwayScore,
                    TotalRed = match.HomeRed + match.AwayRed,
                    MoneyLine = match.OverUnderMoneyLine,
                    CreateDateTime = DateTime.Now,
                    UpToMinute = (int)numUptoMinute.Value,
                    CareGoal = cbCareGoal.Checked,
                    GoalLimit = GetGoalLimit()
                });
                LoadMainGrid();
            }
        }

        private void LoadMainGrid()
        {
            dataGridView.Columns.Clear();
            BindingSource bindingSource = new BindingSource();
            List<Models.DataModels.BetByHdpClose> betsOfThisMatch = DataStore.BetByHdpClose
                .Where(item => item.MatchId == this._matchId)
                .OrderBy(item => item.FullTimeHdp)
                .ThenBy(item => item.MinuteAfterClose)
                .ThenBy(item => item.BetOption)
                .ToList();
            bindingSource.DataSource = Common.Functions.ToDataTable(betsOfThisMatch);
            dataGridView.DataSource = bindingSource;
            dataGridView.Columns["Id"].Visible = false;
            dataGridView.Columns["MatchId"].Visible = false;
            dataGridView.Columns["AutoBetMessage"].Visible = false;
            dataGridView.Columns["Home"].Visible = false;
            dataGridView.Columns["Away"].Visible = false;
            dataGridView.Columns.Add(new DataGridViewButtonColumn()
            {
                UseColumnTextForButtonValue = true,
                Text = "del",
                Name = "deleteRowBtn",
                HeaderText = "Delete"
            });
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

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Guid id = (Guid)dataGridView.Rows[e.RowIndex].Cells["Id"].Value;
                    DataStore.BetByHdpClose.RemoveAll(item => item.Id == id);
                    LoadMainGrid();
                }
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
                DataStore.BetByHdpClose.RemoveAll(item => item.MatchId == this._matchId);
                LoadMainGrid();
            }
        }
    }
}
