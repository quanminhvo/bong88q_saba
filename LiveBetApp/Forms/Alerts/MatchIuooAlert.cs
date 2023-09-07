using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms.Alerts
{
    public partial class MatchIuooAlert : Form
    {
        private long _matchId;
        public MatchIuooAlert(long matchId)
        {
            this._matchId = matchId;
            InitializeComponent();
            if(DataStore.MatchIuooAlert.ContainsKey(_matchId))
            {
                this.numHdpOverFt.Value = DataStore.MatchIuooAlert[_matchId].OUFT_Hdp;
                this.numPriceOverFt.Value = DataStore.MatchIuooAlert[_matchId].OUFT_Price;

                this.numHdpOverFh.Value = DataStore.MatchIuooAlert[_matchId].OUFH_Hdp;
                this.numPriceOverFh.Value = DataStore.MatchIuooAlert[_matchId].OUFH_Price;

                this.numHdpHandicapFt.Value = DataStore.MatchIuooAlert[_matchId].Handicap_Hdp;
                this.numPriceHandicapFt.Value = DataStore.MatchIuooAlert[_matchId].Handicap_Price;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!DataStore.MatchIuooAlert.ContainsKey(_matchId))
            {
                DataStore.MatchIuooAlert.Add(_matchId, new Models.DataModels.MatchIuooAlertItem());
            }

            DataStore.MatchIuooAlert[_matchId].OUFT_Hdp = (int)this.numHdpOverFt.Value;
            DataStore.MatchIuooAlert[_matchId].OUFT_Price = (int)this.numPriceOverFt.Value;

            DataStore.MatchIuooAlert[_matchId].OUFH_Hdp = (int)this.numHdpOverFh.Value;
            DataStore.MatchIuooAlert[_matchId].OUFH_Price = (int)this.numPriceOverFh.Value;

            DataStore.MatchIuooAlert[_matchId].Handicap_Hdp = (int)this.numHdpHandicapFt.Value;
            DataStore.MatchIuooAlert[_matchId].Handicap_Price = (int)this.numPriceHandicapFt.Value;

            this.Close();
        }
    }
}
