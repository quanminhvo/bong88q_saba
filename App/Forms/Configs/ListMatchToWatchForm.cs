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
    public partial class ListMatchToWatchForm : Form
    {
        private MainForm _mainForm;
        private List<MatchToWatch> _matchsToWatch;

        public List<MatchToWatch> Model
        {
            set { this._matchsToWatch = value; }
            get
            {
                if (this._matchsToWatch == null) this._matchsToWatch = new List<MatchToWatch>();
                return this._matchsToWatch;
            }
        }

        public ListMatchToWatchForm()
        {
            InitializeComponent();
        }

        public void InitMainGrid()
        {
            this.dataGridView.AutoGenerateColumns = true;
            this.dataGridView.DataSource = new BindingSource()
            {
                DataSource = Common.ToDataTable(Model)
            };
        }

        public ListMatchToWatchForm(List<MatchToWatch> matchsToWatch)
        {
            InitializeComponent();
            this.Model = matchsToWatch;
            InitMainGrid();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatchToWatch matchToWatch = new MatchToWatch();
            using (MatchToWatchForm form = new MatchToWatchForm(matchToWatch))
            {
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    this.Model.Add(matchToWatch);
                    InitMainGrid();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _matchsToWatch.Count) return;

            string selectedMatchID = this.dataGridView.Rows[e.RowIndex].Cells["MatchID"].Value.ToString();
            MatchToWatch selectedMatch = _matchsToWatch.FirstOrDefault(item => item.MatchID == selectedMatchID);

            using (MatchToWatchForm form = new MatchToWatchForm(selectedMatch))
            {
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    InitMainGrid();
                }
            }
        }
    }
}
