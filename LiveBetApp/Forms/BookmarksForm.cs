using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms
{
    public partial class BookmarksForm : Form
    {
        public BookmarksForm()
        {
            InitializeComponent();
        }

        private void BookmarksForm_Load(object sender, EventArgs e)
        {
            LoadMainGrid();
        }

        private void LoadMainGrid()
        {
            List<long> matchids = DataStore.BookmarkedMatchs.ToList();

            List<Match> matchFromDatastore = new List<Match>();
            for (int i = 0; i < matchids.Count; i++)
            {
                matchFromDatastore.Add(DataStore.Matchs[matchids[i]]);
            }

            List<LiveBetApp.Models.ViewModels.BookmarkedMatch> matchs = AutoMapper.Mapper.Map<List<LiveBetApp.Models.ViewModels.BookmarkedMatch>>(matchFromDatastore);
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(matchs);

            mainDgv.Columns.Clear();
            mainDgv.DataSource = bindingSource;
            mainDgv.Columns["MatchId"].Visible = false;
            mainDgv.Columns.Add(new DataGridViewButtonColumn()
            {
                UseColumnTextForButtonValue = true,
                Text = "del",
                Name = "deleteRowBtn",
                HeaderText = "Delete"
            });

        }

        private void mainDgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long matchId = (long)mainDgv.Rows[e.RowIndex].Cells["MatchId"].Value;
            if (e.RowIndex < 0) return;
            OverUnderScoreTimeForm form = new OverUnderScoreTimeForm(matchId);
            form.Show();
        }

        private void mainDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == mainDgv.Columns["deleteRowBtn"].Index)
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    long matchId = (long)mainDgv.Rows[e.RowIndex].Cells["MatchId"].Value;
                    DataStore.BookmarkedMatchs.RemoveAll(item => item == matchId);
                    Match match;
                    if (DataStore.Matchs.TryGetValue(matchId, out match))
                    {
                        string path = Common.Functions.GetBookmarkPath(match);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }                    
                    LoadMainGrid();
                }
            }
        }
    }
}
