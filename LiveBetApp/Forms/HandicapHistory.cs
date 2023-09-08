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

namespace LiveBetApp.Forms
{
    public partial class HandicapHistory : Form
    {
        private long _matchId;
        public HandicapHistory(long matchId)
        {
            InitializeComponent();
            _matchId = matchId;
        }

        private void HandicapHistory_Load(object sender, EventArgs e)
        {
            if (DataStore.Matchs.ContainsKey(_matchId))
            {
                Match match = DataStore.Matchs[_matchId];
                this.Text = match.Home + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")"
                    + " | " + Common.Functions.GetOverUnderMoneyLinesString(_matchId)
                    + " | " + match.League; ;
            }
            LoadDataFt();
            LoadDataFh();
        }

        private void LoadDataFt()
        {
            if (!DataStore.ProductHandicapFulltimeHistory.ContainsKey(_matchId))
            {
                return;
            }
            List<HandicapLifeTimeHistoryV3> data = DataStore.ProductHandicapFulltimeHistory[_matchId];

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(data);
            dgvFt.DataSource = bindingSource;
            dgvFt.Columns["OddsId"].Visible = false;
            dgvFt.Columns["TimeSpanFromStart"].Visible = false;
        }

        private void LoadDataFh()
        {
            if (!DataStore.ProductHandicapFirstHalfHistory.ContainsKey(_matchId))
            {
                return;
            }
            List<HandicapLifeTimeHistoryV3> data = DataStore.ProductHandicapFirstHalfHistory[_matchId];

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(data);
            dgvFh.DataSource = bindingSource;
            dgvFh.Columns["OddsId"].Visible = false;
            dgvFh.Columns["TimeSpanFromStart"].Visible = false;
        }

    }
}
