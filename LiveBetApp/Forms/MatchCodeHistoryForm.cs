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
    public partial class MatchCodeHistoryForm : Form
    {
        private long _matchId;
        public MatchCodeHistoryForm(long matchId)
        {
            _matchId = matchId;
            InitializeComponent();
        }

        private void MatchCodeHistoryForm_Load(object sender, EventArgs e)
        {
            if (DataStore.Matchs.ContainsKey(_matchId))
            {
                Match match = DataStore.Matchs[_matchId];
                this.Text = match.Home + " (" + match.LiveHomeScore + ")"
                    + " - " + match.Away + " (" + match.LiveAwayScore + ")";
            }
            if (DataStore.MatchCodeHistories.ContainsKey(_matchId))
            {
                List<MatchCodeHistory> data = DataStore.MatchCodeHistories[_matchId]
                    .OrderBy(item => item.UpdateTime)
                    .ToList();

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = Common.Functions.ToDataTable(data);
                dgvMain.Columns.Clear();
                dgvMain.DataSource = bindingSource;
                dgvMain.Columns["UpdateTime"].Visible = false;
            }
        }
    }
}
