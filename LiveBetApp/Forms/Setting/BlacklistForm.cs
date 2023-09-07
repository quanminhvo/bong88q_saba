using LiveBetApp.Models.DataModels;
using LiveBetApp.Models.ViewModels;
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
    public partial class BlacklistForm : Form
    {
        public BlacklistForm()
        {
            InitializeComponent();
            this.Height = Screen.PrimaryScreen.Bounds.Height - 100;
        }

        private void BlacklistForm_Load(object sender, EventArgs e)
        {
            LoadBlackList();
        }

        private void LoadBlackList()
        {
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(DataStore.Blacklist.OrderBy(item => item.League).ToList());

            dgvMain.Columns.Clear();
            dgvMain.DataSource = bindingSource;
            dgvMain.Columns["Id"].Visible = false;
            dgvMain.Columns["League"].ReadOnly = true;
            dgvMain.Columns.Add(new DataGridViewButtonColumn()
            {
                UseColumnTextForButtonValue = true,
                Text = "del",
                Name = "deleteRowBtn",
                HeaderText = "Delete"
            });
            dgvMain.Columns["IsActive"].Width = 20;
            dgvMain.Columns["deleteRowBtn"].Width = 50;
            SetStyle();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string league = txtLeauge.Text.ToUpper();
            if (league != null && league.Length > 0 && !DataStore.Blacklist.Any(item => item.League.ToString().ToUpper() == league))
            {
                DataStore.Blacklist.Add(new BlackList()
                {
                    Id = Guid.NewGuid(),
                    League = league,
                    IsActive = true
                });
                LoadBlackList();
            }
            
        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvMain.Columns["IsActive"].Index)
            {
                string league = dgvMain.Rows[e.RowIndex].Cells[dgvMain.Columns["League"].Index].Value.ToString();
                bool isActive = (bool)dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                SetActiveBlackList(league, isActive);
            }
            else if (e.ColumnIndex == dgvMain.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Guid id = (Guid)dgvMain.Rows[e.RowIndex].Cells["Id"].Value;
                    DataStore.Blacklist.RemoveAll(item => item.Id == id);
                    LoadBlackList();
                }
            }

        }

        private void SetActiveBlackList(string league, bool isActive)
        {
            league = league.ToUpper();
            for (int i=0; i<DataStore.Blacklist.Count; i++)
            {
                if (DataStore.Blacklist[i].League.ToUpper() == league)
                {
                    DataStore.Blacklist[i].IsActive = isActive;
                }
            }
        }

        private void SetStyle()
        {
            try
            {
                int colLeagueIndex = dgvMain.Columns["League"].Index;
                if (dgvMain.Rows.Count == 0) return;
                dgvMain.Rows[0].Cells[colLeagueIndex].Style.BackColor = Color.Gray;
                for (int i = 0; i < dgvMain.Rows.Count - 1; i++)
                {
                    string currentLeague = dgvMain.Rows[i].Cells[colLeagueIndex].Value.ToString();
                    string nextLeague = dgvMain.Rows[i + 1].Cells[colLeagueIndex].Value.ToString();
                    if (currentLeague[0] != nextLeague[0])
                    {
                        dgvMain.Rows[i+1].Cells[colLeagueIndex].Style.BackColor = Color.Gray;
                        if (i == dgvMain.Rows.Count - 1)
                        {
                            dgvMain.Rows[i].Cells[colLeagueIndex].Style.BackColor = Color.Gray;
                        }
                    }
                }
            }
            catch
            {

            }

        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete ALL?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataStore.Blacklist = new List<BlackList>();
                LoadBlackList();
            }
        }
    }
}
