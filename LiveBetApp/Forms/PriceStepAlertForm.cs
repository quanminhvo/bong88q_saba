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
    public partial class PriceStepAlertForm : Form
    {
        private long _matchId;
        public PriceStepAlertForm(long matchId)
        {
            _matchId = matchId;
            InitializeComponent();
            this.numPriceStep.Value = DataStore.Matchs[_matchId].PriceStep;
            this.numPriceStepDown.Value = DataStore.Matchs[_matchId].PriceStepDown;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataStore.Matchs[_matchId].PriceStep = (int)this.numPriceStep.Value;
            DataStore.Matchs[_matchId].PriceStepDown = (int)this.numPriceStepDown.Value;
            this.Close();
        }

        private void PriceStepAlertForm_Load(object sender, EventArgs e)
        {

        }
    }
}
