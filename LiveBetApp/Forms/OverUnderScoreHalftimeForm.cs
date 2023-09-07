using LiveBetApp.Models.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms
{
    public partial class OverUnderScoreHalftimeForm : Form
    {
        private long _matchId;
        private int _columnCount = 91;
        private int _columnCountHalftime = 31;
        private Thread _executeThread;

        public OverUnderScoreHalftimeForm(long matchId)
        {
            _matchId = matchId;
            InitializeComponent();
        }

        private void OverUnderScoreHalftimeForm_Load(object sender, EventArgs e)
        {
            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        LoadMainGrid();
                        LoadOverUnderFtBeforeLiveGrid();
                        LoadOverUnderFhBeforeLiveGrid();
                        LoadHandicapFtBeforeLiveGridHome();
                        LoadHandicapFhBeforeLiveGridHome();
                        LoadOverUnderFtPriceDiff();
                        if (DataStore.Matchs.ContainsKey(_matchId))
                        {
                            Match match = DataStore.Matchs[_matchId];
                            if (match.LivePeriod < 0) break;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {

                    }
                    Thread.Sleep(60000);
                }
            });

            _executeThread.IsBackground = true;
            _executeThread.Start();
        }

        private delegate void dlgLoadOverUnderFtPriceDiff();

        private void LoadOverUnderFtPriceDiff()
        {
            if (this.dgvOverUnderFtPriceDiff.InvokeRequired)
            {
                this.Invoke(new dlgLoadOverUnderFtPriceDiff(LoadOverUnderFtPriceDiff));
                return;
            }

            this.dgvOverUnderFtPriceDiff.Rows.Clear();
            this.dgvOverUnderFtPriceDiff.ColumnCount = _columnCount;
            for (int i = 0; i < _columnCount; i++)
            {
                this.dgvOverUnderFtPriceDiff.Columns[i].Width = 22;
            }
            this.dgvOverUnderFtPriceDiff.Columns[0].Width = 28;

            Dictionary<int, List<OverUnderScoreTimesV3Item>> data;
            if (!DataStore.OverUnderScoreTimesBeforeLive.TryGetValue(_matchId, out data)) return;
            List<int> hdps = data.Keys.OrderByDescending(item => item).ToList();
            if (hdps.Count == 0) return;

            if (!DataStore.OverUnderScoreTimesBeforeLive.ContainsKey(_matchId)) return;

            Match match = DataStore.Matchs[_matchId];

            DateTime maxDt = DateTime.MinValue;
            for (int i = 0; i < hdps.Count - 1; i++)
            {
                DateTime maxDtRow = data[hdps[i]].Where(item => item.RecordedDatetime != DateTime.MaxValue).Max(item => item.RecordedDatetime);
                if (maxDt < maxDtRow) maxDt = maxDtRow;
            }

            List<string> emptyStrRow = new List<string>();
            for (int i = 1; i <= _columnCount; i++)
            {
                emptyStrRow.Add("");
            }

            for (int i = 0; i < hdps.Count - 1; i++)
            {
                List<OverUnderScoreTimesV3Item> row = data[hdps[i]].Where(item => maxDt.Subtract(item.RecordedDatetime).TotalMinutes <= _columnCount - 1 || item.RecordedDatetime == DateTime.MaxValue).ToList();
                List<OverUnderScoreTimesV3Item> rowBelow = data[hdps[i + 1]].Where(item => maxDt.Subtract(item.RecordedDatetime).TotalMinutes <= _columnCount - 1 || item.RecordedDatetime == DateTime.MaxValue).ToList();

                List<string> strRow = new List<string>();
                for (int j = 1; j <= _columnCount; j++)
                {
                    strRow.Add("");
                }
                for (int j = 0; j < row.Count; j++)
                {
                    int minute = (int)maxDt.Subtract(row[j].RecordedDatetime).TotalMinutes;
                    int colIndex = _columnCount - 1 - minute;
                    OverUnderScoreTimesV3Item itemBelow = rowBelow.FirstOrDefault(item => item.RecordedDatetime.ToString("yyyyMMddHHmm") == row[j].RecordedDatetime.ToString("yyyyMMddHHmm"));
                    if (itemBelow == null) continue;

                    if (row[j].RecordedDatetime == DateTime.MaxValue)
                    {
                        strRow[1] = CalculateSubtractValue(row[j].Over, itemBelow.Over, match.OverUnderMoneyLine).ToString();
                    }
                    else if (colIndex != 1 && row[j].RecordedDatetime != DateTime.MinValue)
                    {
                        strRow[colIndex] = CalculateSubtractValue(row[j].Over, itemBelow.Over, match.OverUnderMoneyLine).ToString();
                    }

                }
                strRow[0] = hdps[i].ToString();
                this.dgvOverUnderFtPriceDiff.Rows.Add(strRow.ToArray());
            }

            //List<int> maxHdps = Common.Functions.MaxHdpFhEveryMinute(_matchId);

            for (int i = 0; i < this.dgvOverUnderFtPriceDiff.RowCount; i++)
            {
                for (int j = 1; j < dgvOverUnderFtPriceDiff.ColumnCount; j++)
                {
                    if (this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvOverUnderFtPriceDiff.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Bold)
                                    | (this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        else
                        {
                            this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvOverUnderFtPriceDiff.DefaultCellStyle.Font, FontStyle.Regular
                                    | (this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Regular)
                                    | (this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Regular)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Value = this.dgvOverUnderFtPriceDiff.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }
                }
            }

            //for (int j = 1; j <= 45; j++)
            //{
            //    if (maxHdps[j] == 300)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.Red;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.SkyBlue;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.LightPink;
            //    }
            //    else if (maxHdps[j] == 275)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.SkyBlue;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.LightPink;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.LightYellow;
            //    }
            //    else if (maxHdps[j] == 250)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.LightPink;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.LightYellow;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.LightGreen;
            //    }
            //    else if (maxHdps[j] == 225)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.LightYellow;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.LightGreen;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.Orange;
            //    }
            //    else if (maxHdps[j] == 200)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.LightGreen;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.Orange;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.SeaGreen;
            //    }
            //    else if (maxHdps[j] == 175)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.Orange;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.SeaGreen;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.Red;
            //    }
            //    else if (maxHdps[j] == 150)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.SeaGreen;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.Red;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.SkyBlue;
            //    }
            //    else if (maxHdps[j] == 125)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.Red;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.SkyBlue;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.LightPink;
            //    }
            //    else if (maxHdps[j] == 100)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.SkyBlue;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.LightPink;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.LightYellow;
            //    }
            //    else if (maxHdps[j] == 75)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.LightPink;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.LightYellow;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.LightGreen;
            //    }
            //    else if (maxHdps[j] == 50)
            //    {
            //        if (this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[0].Cells[j + 1].Style.BackColor = Color.LightYellow;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[1].Cells[j + 1].Style.BackColor = Color.LightGreen;
            //        if (this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value != null && this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Value.ToString().Length > 0)
            //            this.dgvOverUnderFtPriceDiff.Rows[2].Cells[j + 1].Style.BackColor = Color.LightYellow;
            //    }
            //}



        }

        private int CalculateSubtractValue(int a, int b, int line)
        {
            return Common.Functions.OverStepPrice(a) - Common.Functions.OverStepPrice(b) - line;
        }

        private delegate void dlgLoadMainGrid();

        private void LoadMainGrid()
        {
            if (this.dgvMain.InvokeRequired)
            {
                this.Invoke(new dlgLoadMainGrid(LoadMainGrid));
            }
            else
            {
                ConstructMainGrid();
                LoadDataMainGrid();
                SetStyleMainGrid();
            }
        }

        private delegate void dlgLoadOverUnderFtBeforeLiveGrid();
        private void LoadOverUnderFtBeforeLiveGrid()
        {
            if (this.dgvOverUnderFt.InvokeRequired)
            {
                this.Invoke(new dlgLoadOverUnderFtBeforeLiveGrid(LoadOverUnderFtBeforeLiveGrid));
            }
            else
            {
                ConstructOverUnderFtBeforeLiveGrid();
                LoadDataOverUnderFtBeforeLiveGrid();
                SetStyleOverUnderFtBeforeLiveGrid();
            }

        }

        private delegate void dlgLoadOverUnderFhBeforeLiveGrid();
        private void LoadOverUnderFhBeforeLiveGrid()
        {
            if (this.dgvOverUnderFh.InvokeRequired)
            {
                this.Invoke(new dlgLoadOverUnderFhBeforeLiveGrid(LoadOverUnderFhBeforeLiveGrid));
            }
            else
            {
                ConstructOverUnderFhBeforeLiveGrid();
                LoadDataOverUnderFhBeforeLiveGrid();
                SetStyleOverUnderFhBeforeLiveGrid();
            }

        }

        private delegate void dlgLoadHandicapFtBeforeLiveGridHome();
        private void LoadHandicapFtBeforeLiveGridHome()
        {
            if (this.dgvHandicapFtHome.InvokeRequired)
            {
                this.Invoke(new dlgLoadHandicapFtBeforeLiveGridHome(LoadHandicapFtBeforeLiveGridHome));
            }
            else
            {
                ConstructHandicapFtBeforeLiveGrid_Home();
                LoadDataHandicapFtBeforeLiveGrid_Home();
                SetStyleHandicapFtBeforeLiveGridHome();
            }
        }

        private delegate void dlgLoadHandicapFhBeforeLiveGridHome();
        private void LoadHandicapFhBeforeLiveGridHome()
        {
            if (this.dgvHandicapFhHome.InvokeRequired)
            {
                this.Invoke(new dlgLoadHandicapFhBeforeLiveGridHome(LoadHandicapFhBeforeLiveGridHome));
            }
            else
            {
                ConstructHandicapFhBeforeLiveGrid_Home();
                LoadDataHandicapFhBeforeLiveGrid_Home();
                SetStyleHandicapFhBeforeLiveGrid_Home();
            }
        }

        private void SetStyleHandicapFtBeforeLiveGridHome()
        {
            for (int i = 0; i < this.dgvHandicapFtHome.Rows.Count; i++)
            {
                for (int j = 0; j < _columnCount; j++)
                {
                    this.dgvHandicapFtHome.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                }
            }

            for (int i = 1; i < dgvHandicapFtHome.RowCount; i++)
            {
                for (int j = 1; j < dgvHandicapFtHome.ColumnCount; j++)
                {
                    if (this.dgvHandicapFtHome.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvHandicapFtHome.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvHandicapFtHome.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvHandicapFtHome.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvHandicapFtHome.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvHandicapFtHome.Rows[i].Cells[j].Style.Font != null && this.dgvHandicapFtHome.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Bold)
                                    | (this.dgvHandicapFtHome.Rows[i].Cells[j].Style.Font != null && this.dgvHandicapFtHome.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        else
                        {
                            this.dgvHandicapFtHome.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvHandicapFtHome.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvHandicapFtHome.DefaultCellStyle.Font, FontStyle.Regular
                                    | (this.dgvHandicapFtHome.Rows[i].Cells[j].Style.Font != null && this.dgvHandicapFtHome.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Regular)
                                    | (this.dgvHandicapFtHome.Rows[i].Cells[j].Style.Font != null && this.dgvHandicapFtHome.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Regular)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        this.dgvHandicapFtHome.Rows[i].Cells[j].Value = this.dgvHandicapFtHome.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }
                }
            }
        }

        private void SetStyleHandicapFhBeforeLiveGrid_Home()
        {
            for (int i = 0; i < this.dgvHandicapFhHome.Rows.Count; i++)
            {
                for (int j = 0; j < _columnCount; j++)
                {
                    this.dgvHandicapFhHome.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                }
            }

            for (int i = 1; i < dgvHandicapFhHome.RowCount; i++)
            {
                for (int j = 1; j < dgvHandicapFhHome.ColumnCount; j++)
                {
                    if (this.dgvHandicapFhHome.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvHandicapFhHome.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvHandicapFhHome.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvHandicapFhHome.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvHandicapFhHome.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvHandicapFhHome.Rows[i].Cells[j].Style.Font != null && this.dgvHandicapFhHome.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Bold)
                                    | (this.dgvHandicapFhHome.Rows[i].Cells[j].Style.Font != null && this.dgvHandicapFhHome.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        else
                        {
                            this.dgvHandicapFhHome.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvHandicapFhHome.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvHandicapFhHome.DefaultCellStyle.Font, FontStyle.Regular
                                    | (this.dgvHandicapFhHome.Rows[i].Cells[j].Style.Font != null && this.dgvHandicapFhHome.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Regular)
                                    | (this.dgvHandicapFhHome.Rows[i].Cells[j].Style.Font != null && this.dgvHandicapFhHome.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Regular)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        this.dgvHandicapFhHome.Rows[i].Cells[j].Value = this.dgvHandicapFhHome.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }
                }
            }
        }

        private void LoadDataHandicapFtBeforeLiveGrid_Home()
        {
            Dictionary<int, List<HandicapLifeTimeHistoryV2>> data;
            if (!DataStore.HandicapScoreTimesBeforeLive.TryGetValue(_matchId, out data)) return;
            data = UpdateVeryFirstDataHdpBeforeLive(data, _matchId, Common.Enums.BetType.FullTimeHandicap);
            List<int> hdps = data.Keys.OrderBy(item => item).ToList();
            if (hdps.Count == 0) return;

            DateTime maxDt = DateTime.MinValue;
            for (int i = hdps.Count - 1; i >= 0; i--)
            {
                DateTime maxDtRow = data[hdps[i]].Where(item => 
                    item.RecordedDatetime != DateTime.MaxValue
                    && item.RecordedDatetime != DateTime.MaxValue.Subtract(TimeSpan.FromDays(1))
                ).Max(item => item.RecordedDatetime);
                if (maxDt < maxDtRow) maxDt = maxDtRow;
            }

            List<string> emptyStrRow = new List<string>();
            for (int i = 1; i <= _columnCount; i++)
            {
                emptyStrRow.Add("");
            }

            for (int i = hdps.Count - 1; i >= 0; i--)
            {
                List<HandicapLifeTimeHistoryV2> row = data[hdps[i]].Where(item => 
                    maxDt.Subtract(item.RecordedDatetime).TotalMinutes <= _columnCount - 1
                    || item.RecordedDatetime == DateTime.MaxValue
                    || item.RecordedDatetime == DateTime.MaxValue.Subtract(TimeSpan.FromDays(1))
                ).ToList();
                List<string> strRow = new List<string>();
                for (int j = 1; j <= _columnCount; j++)
                {
                    strRow.Add("");
                }
                for (int j = 0; j < row.Count; j++)
                {
                    int minute = (int)maxDt.Subtract(row[j].RecordedDatetime).TotalMinutes;
                    int colIndex = _columnCount - 1 - minute;
                    if (row[j].RecordedDatetime == DateTime.MaxValue)
                    {
                        strRow[2] = row[j].Odds1a.ToString();
                    }
                    else if (row[j].RecordedDatetime == DateTime.MaxValue.Subtract(TimeSpan.FromDays(1)))
                    {
                        strRow[1] = row[j].Odds1a.ToString();
                    }
                    else if (colIndex > 2 && row[j].RecordedDatetime != DateTime.MinValue)
                    {
                        strRow[colIndex] = row[j].Odds1a.ToString();
                    }

                }
                strRow[0] = hdps[i].ToString();
                this.dgvHandicapFtHome.Rows.Add(strRow.ToArray());
            }


        }

        private void LoadDataHandicapFhBeforeLiveGrid_Home()
        {
            Dictionary<int, List<HandicapLifeTimeHistoryV2>> data;
            if (!DataStore.HandicapScoreTimesFirstHalfBeforeLive.TryGetValue(_matchId, out data)) return;
            data = UpdateVeryFirstDataHdpBeforeLive(data, _matchId, Common.Enums.BetType.FirstHalfHandicap);
            List<int> hdps = data.Keys.OrderBy(item => item).ToList();
            if (hdps.Count == 0) return;

            DateTime maxDt = DateTime.MinValue;
            for (int i = hdps.Count - 1; i >= 0; i--)
            {
                DateTime maxDtRow = data[hdps[i]].Where(item => 
                    item.RecordedDatetime != DateTime.MaxValue
                    && item.RecordedDatetime != DateTime.MaxValue.Subtract(TimeSpan.FromDays(1))
                ).Max(item => item.RecordedDatetime);
                if (maxDt < maxDtRow) maxDt = maxDtRow;
            }

            List<string> emptyStrRow = new List<string>();
            for (int i = 1; i <= _columnCount; i++)
            {
                emptyStrRow.Add("");
            }

            for (int i = hdps.Count - 1; i >= 0; i--)
            {
                List<HandicapLifeTimeHistoryV2> row = data[hdps[i]].Where(item => 
                    maxDt.Subtract(item.RecordedDatetime).TotalMinutes <= _columnCount - 1
                    || item.RecordedDatetime == DateTime.MaxValue
                    || item.RecordedDatetime == DateTime.MaxValue.Subtract(TimeSpan.FromDays(1))
                ).ToList();
                List<string> strRow = new List<string>();
                for (int j = 1; j <= _columnCount; j++)
                {
                    strRow.Add("");
                }
                for (int j = 0; j < row.Count; j++)
                {
                    int minute = (int)maxDt.Subtract(row[j].RecordedDatetime).TotalMinutes;
                    int colIndex = _columnCount - 1 - minute;
                    if (row[j].RecordedDatetime == DateTime.MaxValue)
                    {
                        strRow[2] = row[j].Odds1a.ToString();
                    }
                    else if (row[j].RecordedDatetime == DateTime.MaxValue.Subtract(TimeSpan.FromDays(1)))
                    {
                        strRow[1] = row[j].Odds1a.ToString();
                    }
                    else if (colIndex > 2 && row[j].RecordedDatetime != DateTime.MinValue)
                    {
                        strRow[colIndex] = row[j].Odds1a.ToString();
                    }

                }
                strRow[0] = hdps[i].ToString();
                this.dgvHandicapFhHome.Rows.Add(strRow.ToArray());
            }

        }

        private void ConstructHandicapFtBeforeLiveGrid_Home()
        {
            this.dgvHandicapFtHome.Rows.Clear();

            this.dgvHandicapFtHome.ColumnCount = _columnCount;

            for (int i = 0; i < _columnCount; i++)
            {
                this.dgvHandicapFtHome.Columns[i].Width = 22;

            }
            this.dgvHandicapFtHome.Columns[0].Width = 28;

            ArrayList header = new ArrayList() { "" };
            for (int i = 1; i < _columnCount; i++)
            {
                if (i == 1 || i == 2)
                {
                    header.Add(".");
                }
                else
                {
                    header.Add(
                        (_columnCount - i).ToString()
                    );
                }
            }
            this.dgvHandicapFtHome.Rows.Add(header.ToArray());

        }

        private void ConstructHandicapFhBeforeLiveGrid_Home()
        {
            this.dgvHandicapFhHome.Rows.Clear();
            this.dgvHandicapFhHome.ColumnCount = _columnCount;
            for (int i = 0; i < _columnCount; i++)
            {
                this.dgvHandicapFhHome.Columns[i].Width = 22;
            }
            this.dgvHandicapFhHome.Columns[0].Width = 28;
            ArrayList header = new ArrayList() { "" };
            for (int i = 1; i < _columnCount; i++)
            {
                if (i == 1 || i == 2)
                {
                    header.Add(".");
                }
                else
                {
                    header.Add(
                        (_columnCount - i).ToString()
                    );
                }
            }
            this.dgvHandicapFhHome.Rows.Add(header.ToArray());
        }

        private void SetStyleOverUnderFhBeforeLiveGrid()
        {
            for (int i = 0; i < this.dgvOverUnderFh.Rows.Count; i++)
            {
                for (int j = 0; j < _columnCount; j++)
                {
                    this.dgvOverUnderFh.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                }
            }

            for (int i = 1; i < dgvOverUnderFh.RowCount; i++)
            {
                for (int j = 1; j < dgvOverUnderFh.ColumnCount; j++)
                {
                    if (this.dgvOverUnderFh.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvOverUnderFh.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvOverUnderFh.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvOverUnderFh.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvOverUnderFh.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvOverUnderFh.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFh.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Bold)
                                    | (this.dgvOverUnderFh.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFh.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        else
                        {
                            this.dgvOverUnderFh.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvOverUnderFh.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvOverUnderFh.DefaultCellStyle.Font, FontStyle.Regular
                                    | (this.dgvOverUnderFh.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFh.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Regular)
                                    | (this.dgvOverUnderFh.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFh.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Regular)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        this.dgvOverUnderFh.Rows[i].Cells[j].Value = this.dgvOverUnderFh.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }
                }
            }
        }

        private void LoadDataOverUnderFhBeforeLiveGrid()
        {
            Dictionary<int, List<OverUnderScoreTimesV3Item>> data;
            if (!DataStore.OverUnderScoreTimesFirstHalfBeforeLive.TryGetValue(_matchId, out data)) return;
            data = UpdateVeryFirstDataBeforeLive(data, _matchId, Common.Enums.BetType.FirstHalfOverUnder);
            List<int> hdps = data.Keys.OrderBy(item => item).ToList();
            if (hdps.Count == 0) return;

            DateTime maxDt = DateTime.MinValue;
            for (int i = hdps.Count - 1; i >= 0; i--)
            {
                DateTime maxDtRow = data[hdps[i]].Where(item => 
                    item.RecordedDatetime != DateTime.MaxValue
                    && item.RecordedDatetime != DateTime.MaxValue.Subtract(TimeSpan.FromDays(1))
                ).Max(item => item.RecordedDatetime);
                if (maxDt < maxDtRow) maxDt = maxDtRow;
            }

            List<string> emptyStrRow = new List<string>();
            for (int i = 1; i <= _columnCount; i++)
            {
                emptyStrRow.Add("");
            }

            for (int i = hdps.Count - 1; i >= 0; i--)
            {
                List<OverUnderScoreTimesV3Item> row = data[hdps[i]].Where(item => 
                    maxDt.Subtract(item.RecordedDatetime).TotalMinutes <= _columnCount - 1
                    || item.RecordedDatetime == DateTime.MaxValue
                    || item.RecordedDatetime == DateTime.MaxValue.Subtract(TimeSpan.FromDays(1))
                ).ToList();
                List<string> strRow = new List<string>();
                for (int j = 1; j <= _columnCount; j++)
                {
                    strRow.Add("");
                }
                for (int j = 0; j < row.Count; j++)
                {
                    int minute = (int)maxDt.Subtract(row[j].RecordedDatetime).TotalMinutes;
                    int colIndex = _columnCount - 1 - minute;
                    if (row[j].RecordedDatetime == DateTime.MaxValue)
                    {
                        strRow[2] = row[j].Over.ToString();
                    }
                    else if (row[j].RecordedDatetime == DateTime.MaxValue.Subtract(TimeSpan.FromDays(1)))
                    {
                        strRow[1] = row[j].Over.ToString();
                    }
                    else if (colIndex > 2 && row[j].RecordedDatetime != DateTime.MinValue)
                    {
                        strRow[colIndex] = row[j].Over.ToString();
                    }

                }
                strRow[0] = hdps[i].ToString();
                this.dgvOverUnderFh.Rows.Add(strRow.ToArray());
            }

        }

        private void ConstructOverUnderFhBeforeLiveGrid()
        {
            this.dgvOverUnderFh.Rows.Clear();
            this.dgvOverUnderFh.ColumnCount = _columnCount;
            for (int i = 0; i < _columnCount; i++)
            {
                this.dgvOverUnderFh.Columns[i].Width = 22;
            }
            this.dgvOverUnderFh.Columns[0].Width = 28;
            ArrayList header = new ArrayList() { "" };
            for (int i = 1; i < _columnCount; i++)
            {
                if (i == 1 || i == 2)
                {
                    header.Add(".");
                }
                else
                {
                    header.Add(
                        (_columnCount - i).ToString()
                    );
                }
            }
            this.dgvOverUnderFh.Rows.Add(header.ToArray());
        }

        private void SetStyleOverUnderFtBeforeLiveGrid()
        {
            for (int i = 0; i < this.dgvOverUnderFt.Rows.Count; i++)
            {
                for (int j = 0; j < _columnCount; j++)
                {
                    this.dgvOverUnderFt.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                }
            }

            for (int i = 1; i < dgvOverUnderFt.RowCount; i++)
            {
                for (int j = 1; j < dgvOverUnderFt.ColumnCount; j++)
                {
                    if (this.dgvOverUnderFt.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvOverUnderFt.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvOverUnderFt.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvOverUnderFt.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvOverUnderFt.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvOverUnderFt.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFt.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Bold)
                                    | (this.dgvOverUnderFt.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFt.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        else
                        {
                            this.dgvOverUnderFt.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvOverUnderFt.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvOverUnderFt.DefaultCellStyle.Font, FontStyle.Regular
                                    | (this.dgvOverUnderFt.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFt.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Regular)
                                    | (this.dgvOverUnderFt.Rows[i].Cells[j].Style.Font != null && this.dgvOverUnderFt.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Regular)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        this.dgvOverUnderFt.Rows[i].Cells[j].Value = this.dgvOverUnderFt.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }
                }
            }
        }


        private Dictionary<int, List<HandicapLifeTimeHistoryV2>> UpdateVeryFirstDataHdpBeforeLive(
            Dictionary<int, List<HandicapLifeTimeHistoryV2>> data,
            long matchId,
            Common.Enums.BetType betType)
        {
            if (DataStore.VeryFirstProducts.ContainsKey(matchId))
            {
                List<Product> products = DataStore.VeryFirstProducts[matchId].Where(item =>
                    item.Bettype == betType
                ).ToList();
                for (int i = 0; i < products.Count; i++)
                {
                    int hdp = (int)((products[i].Hdp1 + products[i].Hdp2) * 100);
                    if (!data.ContainsKey(hdp))
                    {
                        data.Add(hdp, Common.Functions.GetEmptyRowHandicapLifeTime());
                    }
                    data[hdp][1].RecordedDatetime = DateTime.MaxValue.Subtract(TimeSpan.FromDays(1));
                    data[hdp][1].Hdp1 = products[i].Hdp1;
                    data[hdp][1].Hdp2 = products[i].Hdp2;
                    data[hdp][1].Odds1a = products[i].Odds1a100;
                    data[hdp][1].Odds2a = products[i].Odds2a100;
                }
            }

            return data;
        }

        private Dictionary<int, List<OverUnderScoreTimesV3Item>> UpdateVeryFirstDataBeforeLive(
            Dictionary<int, List<OverUnderScoreTimesV3Item>> data, 
            long matchId,
            Common.Enums.BetType betType)
        {
            if (DataStore.VeryFirstProducts.ContainsKey(matchId))
            {
                List<Product> products = DataStore.VeryFirstProducts[matchId].Where(item => 
                    item.Bettype == betType
                ).ToList();
                for (int i = 0; i < products.Count; i++)
                {
                    int hdp = (int)(products[i].Hdp1 * 100);
                    if (!data.ContainsKey(hdp))
                    {
                        data.Add(hdp, Common.Functions.GetEmptyRowBeforeLiveOuv3());
                    }
                    data[hdp][1].RecordedDatetime = DateTime.MaxValue.Subtract(TimeSpan.FromDays(1));
                    data[hdp][1].Over = products[i].Odds1a100;
                    data[hdp][1].Under = products[i].Odds2a100;
                }
            }
            
            return data;
        }

        private void LoadDataOverUnderFtBeforeLiveGrid()
        {
            Dictionary<int, List<OverUnderScoreTimesV3Item>> data;
            if (!DataStore.OverUnderScoreTimesBeforeLive.TryGetValue(_matchId, out data)) return;
            data = UpdateVeryFirstDataBeforeLive(data, _matchId, Common.Enums.BetType.FullTimeOverUnder);
            List<int> hdps = data.Keys.OrderBy(item => item).ToList();
            if (hdps.Count == 0) return;

            DateTime maxDt = DateTime.MinValue;
            for (int i = hdps.Count - 1; i >= 0; i--)
            {
                DateTime maxDtRow = data[hdps[i]].Where(item => 
                    item.RecordedDatetime != DateTime.MaxValue
                    && item.RecordedDatetime != DateTime.MaxValue.Subtract(TimeSpan.FromDays(1))
                ).Max(item => item.RecordedDatetime);
                if (maxDt < maxDtRow) maxDt = maxDtRow;
            }

            List<string> emptyStrRow = new List<string>();
            for (int i = 1; i <= _columnCount; i++)
            {
                emptyStrRow.Add("");
            }

            for (int i = hdps.Count - 1; i >= 0; i--)
            {
                List<OverUnderScoreTimesV3Item> row = data[hdps[i]].Where(item => 
                    maxDt.Subtract(item.RecordedDatetime).TotalMinutes <= _columnCount - 1 
                    || item.RecordedDatetime == DateTime.MaxValue
                    || item.RecordedDatetime == DateTime.MaxValue.Subtract(TimeSpan.FromDays(1))
                ).ToList();

                List<string> strRow = new List<string>();
                for (int j = 1; j <= _columnCount; j++)
                {
                    strRow.Add("");
                }
                for (int j = 0; j < row.Count; j++)
                {
                    int minute = (int)maxDt.Subtract(row[j].RecordedDatetime).TotalMinutes;
                    int colIndex = _columnCount - 1 - minute;
                    if (row[j].RecordedDatetime == DateTime.MaxValue)
                    {
                        strRow[2] = row[j].Over.ToString();
                    }
                    else if (row[j].RecordedDatetime == DateTime.MaxValue.Subtract(TimeSpan.FromDays(1)))
                    {
                        strRow[1] = row[j].Over.ToString();
                    }
                    else if (colIndex > 2 && row[j].RecordedDatetime != DateTime.MinValue)
                    {
                        strRow[colIndex] = row[j].Over.ToString();
                    }

                }
                strRow[0] = hdps[i].ToString();
                this.dgvOverUnderFt.Rows.Add(strRow.ToArray());
            }

        }

        private void ConstructOverUnderFtBeforeLiveGrid()
        {
            this.dgvOverUnderFt.Rows.Clear();
            this.dgvOverUnderFt.ColumnCount = _columnCount;
            for (int i = 0; i < _columnCount; i++)
            {
                this.dgvOverUnderFt.Columns[i].Width = 22;
            }
            this.dgvOverUnderFt.Columns[0].Width = 28;
            ArrayList header = new ArrayList() { "" };
            for (int i = 1; i < _columnCount; i++)
            {
                if (i == 1 || i == 2)
                {
                    header.Add(".");
                }
                else
                {
                    header.Add(
                        (_columnCount - i).ToString()
                    );
                }

            }
            this.dgvOverUnderFt.Rows.Add(header.ToArray());
        }

        private void ConstructMainGrid()
        {
            this.dgvMain.Rows.Clear();
            this.dgvMain.ColumnCount = _columnCountHalftime;
            for (int i = 0; i < _columnCountHalftime; i++)
            {
                this.dgvMain.Columns[i].Width = 22;
            }
            this.dgvMain.Columns[0].Width = 28;
            ArrayList header = new ArrayList() { "" };
            for (int i = 1; i < _columnCountHalftime; i++)
            {
                header.Add(
                    i.ToString()
                );
            }
            this.dgvMain.Rows.Add(header.ToArray());
        }

        private void LoadDataMainGrid()
        {
            Dictionary<int, List<string>> data;

            if (!DataStore.OverUnderScoreHalfTimes.TryGetValue(_matchId, out data)) return;
            List<int> scores = data.Keys.OrderBy(item => item).ToList();
            if (scores.Count == 0) return;

            for (int i = scores.Count - 1; i >= 0; i--)
            {
                List<string> row = new List<string>();
                row.Add(scores[i].ToString());
                row.AddRange(data[scores[i]].ToList());
                this.dgvMain.Rows.Add(row.ToArray());
            }
        }

        private void SetStyleMainGrid()
        {
            for (int i = 0; i < this.dgvMain.Rows.Count; i++)
            {
                for (int j = 0; j < _columnCountHalftime; j++)
                {
                    this.dgvMain.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                }
            }

            for (int i = 1; i < dgvMain.RowCount; i++)
            {
                for (int j = 1; j < dgvMain.ColumnCount; j++)
                {
                    if (this.dgvMain.Rows[i].Cells[j].Value != null)
                    {
                        if (this.dgvMain.Rows[i].Cells[j].Value.ToString().Contains("-"))
                        {
                            this.dgvMain.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvMain.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvMain.DefaultCellStyle.Font, FontStyle.Bold
                                    | (this.dgvMain.Rows[i].Cells[j].Style.Font != null && this.dgvMain.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Bold)
                                    | (this.dgvMain.Rows[i].Cells[j].Style.Font != null && this.dgvMain.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Bold)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        else
                        {
                            this.dgvMain.Rows[i].Cells[j].Style = new DataGridViewCellStyle(this.dgvMain.Rows[i].Cells[j].Style)
                            {
                                Font = new Font(
                                    this.dgvMain.DefaultCellStyle.Font, FontStyle.Regular
                                    | (this.dgvMain.Rows[i].Cells[j].Style.Font != null && this.dgvMain.Rows[i].Cells[j].Style.Font.Underline ? FontStyle.Underline : FontStyle.Regular)
                                    | (this.dgvMain.Rows[i].Cells[j].Style.Font != null && this.dgvMain.Rows[i].Cells[j].Style.Font.Italic ? FontStyle.Italic : FontStyle.Regular)
                                ),
                                ForeColor = Color.Black
                            };
                        }
                        this.dgvMain.Rows[i].Cells[j].Value = this.dgvMain.Rows[i].Cells[j].Value.ToString().Replace("-", "");
                    }
                }
            }
        }

        private void OverUnderScoreHalftimeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }
    }
}
