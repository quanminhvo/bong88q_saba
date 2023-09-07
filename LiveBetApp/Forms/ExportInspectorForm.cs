using LiveBetApp.Common;
using LiveBetApp.Models.ViewModels;
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
    public partial class ExportInspectorForm : Form
    {
        public ExportInspectorForm()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Common.Functions.SaveToCsv(GetProductStatusLogExport(), "DataInspector.csv");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");  
            }
        }

        private List<ProductStatusLogExport> GetProductStatusLogExport()
        {
            List<ProductStatusLogExport> result = new List<ProductStatusLogExport>();
            var matchs = DataStore.Matchs.Values.Where(item =>
                !item.League.ToLower().Contains("e-football")
                && !item.League.ToLower().Contains("saba soccer pingoal")
                && !item.League.ToUpper().Contains("SABA")
            ).ToList();
            for (int i = 0; i < matchs.Count; i++)
            {
                try
                {
                    List<List<Models.DataModels.ProductStatusLog>> productStatusLogOfMatch = DataStore.ProductStatus[matchs[i].MatchId];
                    List<Models.DataModels.ProductStatusLog> productStatusLogAtZero = productStatusLogOfMatch[0].Where(item => item.LivePeriod == 0 && !item.IsHt).ToList();
                    for (int j = 0; j < productStatusLogAtZero.Count; j++)
                    {
                        if (productStatusLogAtZero[j].DtLog < dtFilterFrom.Value
                            || productStatusLogAtZero[j].DtLog > dtFilterTo.Value)
                        {
                            continue;
                        }
                        string typeStr = productStatusLogAtZero[j].Type == Enums.BetType.FullTime1x2 ? "FT 1x2" :
                            productStatusLogAtZero[j].Type == Enums.BetType.FullTimeHandicap ? "FT Handicap" :
                            productStatusLogAtZero[j].Type == Enums.BetType.FullTimeOverUnder ? "FT OverUnder" :
                            productStatusLogAtZero[j].Type.ToString();

                        result.Add(new ProductStatusLogExport()
                        {
                            League = matchs[i].League,
                            Away = matchs[i].Away,
                            Home = matchs[i].Home,

                            GlobalShowTime = productStatusLogAtZero[j].GlobalShowtime,
                            DtLog = productStatusLogAtZero[j].DtLog,
                            Desc = productStatusLogAtZero[j].Status + " - "
                                + productStatusLogAtZero[j].ServerAction + " - "
                                + typeStr + " - "
                                + productStatusLogAtZero[j].Hdp1 + " - "
                                + productStatusLogAtZero[j].Hdp2 + " - "
                                + productStatusLogAtZero[j].Hdp100 + " - "
                                + productStatusLogAtZero[j].Price + " - "
                                + productStatusLogAtZero[j].MaxBet
                        });
                    }
                }
                catch { }   
            }
            return result
                .OrderBy(item => item.DtLog)
                .ThenBy(item => item.GlobalShowTime)
                .ToList();
        }

        private void ExportInspectorForm_Load(object sender, EventArgs e)
        {
            this.dtFilterFrom.Value = DateTime.Now;
            this.dtFilterTo.Value = DateTime.Now;
        }
    }
}
