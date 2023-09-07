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

namespace LiveBetApp.Forms
{
    public partial class FindDecoyForm : Form
    {
        public FindDecoyForm(DateTime dt)
        {
            InitializeComponent();
            dtSearch.Format = DateTimePickerFormat.Custom;
            dtSearch.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            dtSearch.Value = dt;
        }

        private void FindDecoyForm_Load(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch
            {

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch
            {

            }
        }

        private void Search()
        {
            DateTime dt = dtSearch.Value;

            List<long> keys = DataStore.ProductIdsOfMatch.Keys.ToList();
            List<DecoyProduct> viewModel = new List<DecoyProduct>();

            for (int i = 0; i < keys.Count; i++)
            {
                if (!DataStore.Matchs.ContainsKey(keys[i])) continue;

                LiveBetApp.Models.DataModels.Match match = DataStore.Matchs[keys[i]];

                if(match.League.Contains(" - ")
                    || match.League.ToUpper().Contains(" - CORNERS")
                    || match.League.ToUpper().Contains("SABA"))
                {
                    continue;
                }

                List<ProductIdLog> row = DataStore.ProductIdsOfMatch[keys[i]];
                for (int j = 0; j < row.Count; j++)
                {
                    if (row[j].ProductBetType == Common.Enums.BetType.FirstHalf1x2
                        || row[j].ProductBetType == Common.Enums.BetType.FullTime1x2
                        || row[j].ProductBetType == Common.Enums.BetType.Unknown) continue;




                    int minuteDiff = (int)Math.Abs(row[j].CreateDatetime.Subtract(dt).TotalMinutes);
                    if (row[j].Minute < 0 && minuteDiff < 10)
                    {
                        viewModel.Add(new DecoyProduct()
                        {
                            Away = match.Away,
                            FirstShow = match.FirstShow,
                            GlobalShowtime = match.GlobalShowtime,
                            Hdp = row[j].Hdp,
                            HdpS = row[j].HdpS,
                            Home = match.Home,
                            Leauge = match.League,
                            LiveAwayScore = match.LiveAwayScore,
                            LiveHomeScore = match.LiveHomeScore,
                            Odds1a100 = row[j].Odds1a100,
                            Odds2a100 = row[j].Odds2a100,
                            Minute = row[j].Minute,
                            ProductBetType = row[j].ProductBetType,
                            LivePeriod = match.LivePeriod,
                            IsHt = match.IsHT,
                            TimeAddProduct = row[j].CreateDatetime
                        });
                    }
                }
            }

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(viewModel);

            dgvMain.Columns.Clear();
            dgvMain.DataSource = bindingSource;

            dgvMain.Columns["Home"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMain.Columns["LiveHomeScore"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMain.Columns["LiveAwayScore"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgvMain.Columns["Home"].Width = 40;
            dgvMain.Columns["LiveHomeScore"].Width = 8;
            dgvMain.Columns["LiveAwayScore"].Width = 8;
            dgvMain.Columns["Away"].Width = 40;

            dgvMain.Columns["LivePeriod"].Width = 8;
            dgvMain.Columns["IsHt"].Width = 8;
            dgvMain.Columns["GlobalShowtime"].Width = 30;
            dgvMain.Columns["FirstShow"].Width = 30;
            dgvMain.Columns["ProductBetType"].Width = 25;
            dgvMain.Columns["TimeAddProduct"].Width = 25;
            

            dgvMain.Columns["Minute"].Width = 20;
            dgvMain.Columns["Hdp"].Width = 10;
            dgvMain.Columns["HdpS"].Width = 10;
            dgvMain.Columns["Odds1a100"].Width = 10;
            dgvMain.Columns["Odds2a100"].Width = 30;



            for (int i = 0; i < dgvMain.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    dgvMain.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }
    }
}
