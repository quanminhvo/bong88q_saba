using App.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Forms.Configs
{
    public partial class MatchToWatchForm : Form
    {
        private MatchToWatch _model;

        public MatchToWatch Model
        {
            set { this._model = value; }
            get
            {
                if (this._model == null) this._model = new MatchToWatch();
                return this._model;
            }
        }

        public MatchToWatchForm(MatchToWatch matchToWatch)
        {
            this.Model = matchToWatch;
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            this.txtTeamAwayName.Text = Model.TeamGuest;
            this.txtTeamHomeName.Text = Model.TeamHost;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void sixTimmingBetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SixTimmingBetForm form = new SixTimmingBetForm(this.Model.TimmingBetButtons))
            {
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    this.Model.TimmingBetButtons = form.Model;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            ConstructModel();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ConstructModel()
        {
            this.Model.TeamHost = this.txtTeamHomeName.Text.Trim().ToUpper();
            this.Model.TeamGuest = this.txtTeamAwayName.Text.Trim().ToUpper();
        }

        private bool ValidateForm()
        {
            bool result = true;
            label1.BackColor = SystemColors.Control;
            label2.BackColor = SystemColors.Control;

            if(this.txtTeamHomeName.Text.Trim().Length == 0)
            {
                label1.BackColor = Color.Red;
                result = false;
            }

            if (this.txtTeamAwayName.Text.Trim().Length == 0)
            {
                label2.BackColor = Color.Red;
                result = false;
            }

            return result;
        }
    }
}
