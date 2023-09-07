using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetApp.Forms
{
    public partial class BetRowForm : Form
    {
        private ListMatchForm _rootForm;
        private string _homeName;
        private string _awayName;

        public BetRowForm(ListMatchForm rootForm, string homeName, string awayName)
        {
            InitializeComponent();
            this._rootForm = rootForm;
            this._homeName = homeName;
            this._awayName = awayName;
            this.Text = homeName + " vs " + awayName;
        }

        private void btnFullTimeHDC1_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName, 
                this._awayName, 
                Common.Enums.BetAreaIndex.First, 
                Common.Enums.BetAreaButton.FullTimeHandicap))
            {
                form.ShowDialog();
            }
        }

        private void btnFullTimeOU1_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.First,
                Common.Enums.BetAreaButton.FullTimeOverUnder))
            {
                form.ShowDialog();
            }
        }

        private void btnHalfTimeHDC1_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.First,
                Common.Enums.BetAreaButton.HalfTimeHandicap))
            {
                form.ShowDialog();
            }
        }

        private void btnHalfTimeOU1_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.First,
                Common.Enums.BetAreaButton.HalfTimeOverUnder))
            {
                form.ShowDialog();
            }
        }

        private void btnFullTimeHDC2_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.Second,
                Common.Enums.BetAreaButton.FullTimeHandicap))
            {
                form.ShowDialog();
            }
        }

        private void btnFullTimeOU2_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.Second,
                Common.Enums.BetAreaButton.FullTimeOverUnder))
            {
                form.ShowDialog();
            }
        }

        private void btnHalfTimeHDC2_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.Second,
                Common.Enums.BetAreaButton.HalfTimeHandicap))
            {
                form.ShowDialog();
            }
        }

        private void btnHalfTimeOU2_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.Second,
                Common.Enums.BetAreaButton.HalfTimeOverUnder))
            {
                form.ShowDialog();
            }
        }

        private void btnFullTimeHDC3_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.Third,
                Common.Enums.BetAreaButton.FullTimeHandicap))
            {
                form.ShowDialog();
            }
        }

        private void btnFullTimeOU3_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.Third,
                Common.Enums.BetAreaButton.FullTimeOverUnder))
            {
                form.ShowDialog();
            }
        }

        private void btnHalfTimeHDC3_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.Third,
                Common.Enums.BetAreaButton.HalfTimeHandicap))
            {
                form.ShowDialog();
            }
        }

        private void btnHalfTimeOU3_Click(object sender, EventArgs e)
        {
            using (BetCellHistoryForm form = new BetCellHistoryForm(
                this._rootForm,
                this._homeName,
                this._awayName,
                Common.Enums.BetAreaIndex.Third,
                Common.Enums.BetAreaButton.HalfTimeOverUnder))
            {
                form.ShowDialog();
            }
        }
    }
}
