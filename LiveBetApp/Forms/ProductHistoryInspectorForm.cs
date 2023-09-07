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
    public partial class ProductHistoryInspectorForm : Form
    {
        private long _matchId;
        private long _minute;
        private long _livePeriod;
        public ProductHistoryInspectorForm(long matchId, long minute, long livePeriod)
        {
            InitializeComponent();
            _matchId = matchId;
            _minute = minute;
            _livePeriod = livePeriod;

            this.StartPosition = FormStartPosition.Manual;
            this.Width = 600;
            this.Height = Screen.PrimaryScreen.Bounds.Height - 50;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - Width, 0);


        }

        private void LoadData(List<ProductStatusLog> rowData)
        {

            List<LiveBetApp.Models.ViewModels.ProductStatusLog> rowDataView = AutoMapper.Mapper.Map<List<LiveBetApp.Models.ViewModels.ProductStatusLog>>(rowData);

            for (int i = 0; i < rowDataView.Count; i++)
            {
                rowDataView[i].STT = (i + 1).ToString();
            }

            for (int i = 0; i < rowDataView.Count; i++)
            {
                if (rowDataView[i].LivePeriod == 0 
                    && !rowDataView[i].IsHt)
                {
                    for (int j = i; j < rowDataView.Count; j++)
                    {
                        rowDataView[j].DtLogStr = rowDataView[j].DtLog.ToString("HH:mm");
                        if (rowDataView[j].LivePeriod != 0 || rowDataView[j].IsHt)
                        {
                            break;
                        }
                    }
                    break;
                }
            }

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.Functions.ToDataTable(rowDataView);
            dgvMain.DataSource = bindingSource;
            SetStyle();
            SetStyle();


        }

        private void SetStyle()
        {
            Match match = DataStore.Matchs[_matchId];
            dgvMain.Columns["STT"].Width = 30;
            dgvMain.Columns["Status"].Width = 100;
            dgvMain.Columns["ServerAction"].Width = 70;
            dgvMain.Columns["TypeStr"].Width = 120;
            dgvMain.Columns["Hdp1"].Width = 35;
            dgvMain.Columns["Hdp2"].Width = 35;
            dgvMain.Columns["Hdp100"].Width = 35;
            dgvMain.Columns["Price"].Width = 35;


            dgvMain.Columns["Type"].Visible = false;
            dgvMain.Columns["LivePeriod"].Visible = false;
            dgvMain.Columns["IsHt"].Visible = false;
            dgvMain.Columns["DtLog"].Visible = false;
            dgvMain.Columns["Hdp100"].Visible = false;

            for (int i = 0; i < dgvMain.RowCount; i++)
            {
                string type = dgvMain.Rows[i].Cells["Type"].Value.ToString();
                string status = dgvMain.Rows[i].Cells["Status"].Value.ToString();
                string serverAction = dgvMain.Rows[i].Cells["ServerAction"].Value.ToString();
                string price = dgvMain.Rows[i].Cells["price"].Value.ToString();
                int hdp1 =(int)(float.Parse(dgvMain.Rows[i].Cells["hdp1"].Value.ToString()) * 100);
                int hdp2 = (int)(float.Parse(dgvMain.Rows[i].Cells["hdp2"].Value.ToString()) * 100);

                if (type == ((int)Common.Enums.BetType.FullTimeOverUnder).ToString())
                {
                    dgvMain.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (type == ((int)Common.Enums.BetType.FirstHalfOverUnder).ToString())
                {
                    dgvMain.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }

                if (hdp1 % 100 == 50)
                {
                    dgvMain.Rows[i].Cells["Hdp1"].Style = new DataGridViewCellStyle(dgvMain.Rows[i].Cells["Hdp1"].Style)
                    {
                        Font = new Font(this.dgvMain.DefaultCellStyle.Font, FontStyle.Bold),
                        ForeColor = Color.Black
                    };

                    dgvMain.Rows[i].Cells["Price"].Style = new DataGridViewCellStyle(dgvMain.Rows[i].Cells["Price"].Style)
                    {
                        Font = new Font(this.dgvMain.DefaultCellStyle.Font, FontStyle.Bold),
                        ForeColor = Color.Black
                    };
                }

                if (hdp2 % 100 == 50)
                {
                    dgvMain.Rows[i].Cells["Hdp2"].Style = new DataGridViewCellStyle(dgvMain.Rows[i].Cells["Hdp2"].Style)
                    {
                        Font = new Font(this.dgvMain.DefaultCellStyle.Font, FontStyle.Bold),
                        ForeColor = Color.Black
                    };

                    dgvMain.Rows[i].Cells["Price"].Style = new DataGridViewCellStyle(dgvMain.Rows[i].Cells["Price"].Style)
                    {
                        Font = new Font(this.dgvMain.DefaultCellStyle.Font, FontStyle.Bold),
                        ForeColor = Color.Black
                    };
                }


                if (type == ((int)Common.Enums.BetType.FirstHalf1x2).ToString()
                    || type == ((int)Common.Enums.BetType.FullTime1x2).ToString())
                {
                    dgvMain.Rows[i].Cells["TypeStr"].Style = new DataGridViewCellStyle(dgvMain.Rows[i].Cells["TypeStr"].Style)
                    {
                        Font = new Font(this.dgvMain.DefaultCellStyle.Font, FontStyle.Bold),
                        ForeColor = Color.Black
                    };

                    if (price != "0")
                    {
                        dgvMain.Rows[i].Cells["Price"].Style = new DataGridViewCellStyle(dgvMain.Rows[i].Cells["Price"].Style)
                        {
                            Font = new Font(this.dgvMain.DefaultCellStyle.Font, FontStyle.Bold),
                            ForeColor = Color.Black
                        };
                    }

                }

                if (status == "closePrice")
                {
                    dgvMain.Rows[i].Cells["Status"].Style.BackColor = Color.Pink;
                }
                else if (status.ToLower() == "suspend")
                {
                    dgvMain.Rows[i].Cells["Status"].Style = new DataGridViewCellStyle(
                        dgvMain.Rows[i].DefaultCellStyle)
                    {
                        Font = new Font(this.dgvMain.DefaultCellStyle.Font, FontStyle.Bold),
                        ForeColor = Color.Black
                    };
                    dgvMain.Rows[i].Cells["Status"].Value = "SUSPEND";
                }

                if (serverAction == "delete")
                {
                    dgvMain.Rows[i].Cells["ServerAction"].Style.BackColor = Color.GreenYellow;
                }

                if (serverAction == "insert")
                {
                    dgvMain.Rows[i].Cells["ServerAction"].Style.BackColor = Color.LightGreen;
                }

                DateTime DtLog = DateTime.Parse(dgvMain.Rows[i].Cells["DtLog"].Value.ToString());
                if (match.GlobalShowtime.Subtract(DtLog).TotalMinutes >= 0
                    && match.GlobalShowtime.Subtract(DtLog).TotalMinutes <= 240)
                {
                    dgvMain.Rows[i].Cells["DtLogStr"].Style = new DataGridViewCellStyle()
                    {
                        Font = new Font(
                            this.dgvMain.DefaultCellStyle.Font.FontFamily,
                            this.dgvMain.DefaultCellStyle.Font.Size,
                            FontStyle.Bold
                        ),
                        ForeColor = Color.Black
                    };
                }
            }

            
        }

        private void ProductHistoryInspectorForm_Load(object sender, EventArgs e)
        {
            if (DataStore.Matchs.ContainsKey(_matchId))
            {
                Match match = DataStore.Matchs[_matchId];
                this.Text = match.Home + " (" + match.LiveHomeScore + ")" + " - " + match.Away + " (" + match.LiveAwayScore + ")"
                    + " | " + Common.Functions.GetOverUnderMoneyLinesString(_matchId)
                    + " | " + match.League
                    + " | INSPECTOR";


                this.numFromMinute.Value = _minute;
                this.numToMinute.Value = _minute;
                if (_livePeriod == 1)
                {
                    btnH1.PerformClick();
                }
                else if (_livePeriod == 2)
                {
                    btnH2.PerformClick();
                }
                else if (_livePeriod == 3)
                {
                    btnNgl.PerformClick();
                }
                else
                {
                    int currentMinute = Convert.ToInt32(DateTime.Now.Subtract(match.LiveTimer).TotalMinutes);
                    if (currentMinute < 0) currentMinute = 0;
                    if (currentMinute > 90) currentMinute = 90;
                    this.numFromMinute.Value = currentMinute;
                    this.numToMinute.Value = currentMinute;
                }
            }
        }

        private void btnNgl_Click(object sender, EventArgs e)
        {
            dgvMain.Columns.Clear();
            int fromMinute = 1;
            int toMinute = 30;
            this.numFromMinute.Value = fromMinute;
            this.numToMinute.Value = toMinute;

            if (fromMinute <= toMinute
                && toMinute <= 100
                && fromMinute >= 0
                && DataStore.ProductStatus.ContainsKey(_matchId))
            {
                List<List<ProductStatusLog>> data = DataStore.ProductStatus[_matchId];
                List<ProductStatusLog> rowData = new List<ProductStatusLog>();
                for (int i = fromMinute; i <= toMinute; i++)
                {
                    rowData.AddRange(data[i + 55]
                        .Where(item => item.LivePeriod == 0 && item.IsHt)
                        .ToList()
                    );
                }
                LoadData(rowData);
            }

        }

        private void btnH1_Click(object sender, EventArgs e)
        {
            dgvMain.Columns.Clear();
            int fromMinute = (int)numFromMinute.Value;
            int toMinute = (int)numToMinute.Value;
            if (fromMinute <= toMinute
                && toMinute <= 100
                && fromMinute >= 0
                && DataStore.ProductStatus.ContainsKey(_matchId))
            {
                List<List<ProductStatusLog>> data = DataStore.ProductStatus[_matchId];
                List<ProductStatusLog> rowData = new List<ProductStatusLog>();
                for (int i = fromMinute; i <= toMinute; i++)
                {
                    rowData.AddRange(data[i]
                        .Where(item => item.LivePeriod == 1 || (item.LivePeriod == 0 && !item.IsHt))
                        .ToList()
                    );
                }
                LoadData(rowData);
            }

        }

        private void btnH2_Click(object sender, EventArgs e)
        {
            dgvMain.Columns.Clear();
            int fromMinute = (int)numFromMinute.Value;
            int toMinute = (int)numToMinute.Value;
            if (fromMinute <= toMinute
                && toMinute <= 100 
                && fromMinute >= 0 
                && DataStore.ProductStatus.ContainsKey(_matchId))
            {
                List<List<ProductStatusLog>> data = DataStore.ProductStatus[_matchId];
                List<ProductStatusLog> rowData = new List<ProductStatusLog>();
                for (int i = fromMinute; i <= toMinute; i++)
                {
                    rowData.AddRange(data[i]
                        .Where(item => item.LivePeriod == 2)
                        .ToList()
                    );
                }
                LoadData(rowData);
            }
        }

        private void numFromMinute_Enter(object sender, EventArgs e)
        {
            numFromMinute.Select(0, numFromMinute.Text.Length);
        }

        private void numToMinute_Enter(object sender, EventArgs e)
        {
            numToMinute.Select(0, numToMinute.Text.Length);
        }

        private void dgvMain_Sorted(object sender, EventArgs e)
        {
            SetStyle();
        }
    }
}
