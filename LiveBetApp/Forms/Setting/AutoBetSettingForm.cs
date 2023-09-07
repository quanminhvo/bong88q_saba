using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms.Setting
{
    public partial class AutoBetSettingForm : Form
    {
        public AutoBetSettingForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataStore.RunAutobet = cbRunAutoBet.Checked;
            DataStore.AutobetStake = (int)numAutoBetStake.Value;
            this.Close();
        }

        private void AutoBetSettingForm_Load(object sender, EventArgs e)
        {
            cbRunAutoBet.Checked = DataStore.RunAutobet;
            numAutoBetStake.Value = DataStore.AutobetStake;
        }
    }
}
