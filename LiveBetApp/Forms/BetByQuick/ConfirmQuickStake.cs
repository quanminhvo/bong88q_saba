using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms.BetByQuick
{
    public partial class ConfirmQuickStake : Form
    {
        private long _matchId { get; set; }
        private int _hdp { get; set; }
        private int _minute { get; set; }

        private bool _isOverEndPeriod { get; set; }
        private bool _isFullTime { get; set; }

        private OverUnderScoreTimeForm _form;

        public ConfirmQuickStake(long matchId, int hdp, int minute, bool isOverEndPeriod, bool isFullTime, OverUnderScoreTimeForm form)
        {
            _matchId = matchId;
            _hdp = hdp;
            _minute = minute;
            _form = form;
            _isOverEndPeriod = isOverEndPeriod;
            _isFullTime = isFullTime;
            InitializeComponent();
            if (_isOverEndPeriod)
            {
                numPrice.Enabled = true;
                numPrice.Visible = true;
                lblPrice.Visible = true;
                lblPriceDesc.Visible = true;
            }
            else
            {
                numPrice.Enabled = false;
                numPrice.Visible = false;
                lblPrice.Visible = false;
                lblPriceDesc.Visible = false;
            }
        }

        private void ConfirmQuickStake_Load(object sender, EventArgs e)
        {
            this.cbCareGoal.Checked = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_isOverEndPeriod)
            {
                if (!DataStore.BetByHdpPrice.Any(item => item.MatchId == _matchId && item.AutoBetMessage == "OverEnd" + _isFullTime))
                {
                    DataStore.BetByHdpPrice.Add(new Models.DataModels.BetByHdpPrice()
                    {
                        Id = Guid.NewGuid(),
                        AutoBetMessage = "OverEnd" + _isFullTime,
                        CareGoal = cbCareGoal.Checked,
                        CreateDateTime = DateTime.Now,
                        GoalLimit = 100,
                        IsFulltime = _isFullTime,
                        IsOver = true,
                        LivePeriod = _isFullTime ? 2 : 1,
                        MatchId = _matchId,
                        Price = (int)numPrice.Value,
                        Stake = (int)numStake.Value,
                        TotalScore = 100,
                        UpToMinute = 100,
                        TotalRed = 100,
                        MoneyLine = 0,
                        Hdp = _hdp
                    });
                }

            }
            else
            {
                if (!DataStore.BetByQuick.Any(item => item.MatchId == _matchId && item.Hdp == _hdp))
                {
                    Models.DataModels.Match match;
                    if (DataStore.Matchs.TryGetValue(_matchId, out match))
                    {
                        DataStore.BetByQuick.Add(new Models.DataModels.BetByQuick()
                        {
                            Id = Guid.NewGuid(),
                            MatchId = _matchId,
                            Hdp = _hdp,
                            Stake = (int)numStake.Value,
                            Minute = _minute,
                            Planted = false,
                            CareGoal = cbCareGoal.Checked,
                            TotalGoal = match.LiveHomeScore + match.LiveAwayScore
                        });
                    }
                }
            }

            _form.ReloadQuickBet();
            this.Close();
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
    }
}
