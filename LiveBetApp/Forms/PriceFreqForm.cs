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
    public partial class PriceFreqForm : Form
    {
        private long _matchId;
        private Thread _executeThread;
        public PriceFreqForm(long matchId)
        {
            InitializeComponent();
            _matchId = matchId;
        }

        private void PriceFreqForm_Load(object sender, EventArgs e)
        {
            LoadDataOverUnder();
            //_executeThread = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        try
            //        {

            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //        finally
            //        {

            //        }
            //        Thread.Sleep(60000);
            //    }
            //});

            //_executeThread.IsBackground = true;
            //_executeThread.Start();


        }

        private void ConstructBeforeLiveGrid(DataGridView dgv, int columnCount)
        {
            dgv.Rows.Clear();
            dgv.ColumnCount = columnCount;
            for (int i = 0; i < columnCount; i++)
            {
                dgv.Columns[i].Width = 22;
            }
            dgv.Columns[0].Width = 28;
            ArrayList header = new ArrayList() { "" };
            for (int i = 1; i < columnCount; i++)
            {

                header.Add(
                    (columnCount - i).ToString()
                );


            }
            dgv.Rows.Add(header.ToArray());
        }

        private void LoadDataBeforeLiveGrid(DataGridView dgv, Dictionary<int, List<int>> data, int columnCount)
        {
            List<int> hdps = data.Keys.OrderBy(item => item).ToList();
            if (hdps.Count == 0) return;

            List<string> emptyStrRow = new List<string>();
            for (int i = 1; i <= columnCount; i++)
            {
                emptyStrRow.Add("");
            }

            List<int> rowSum = new List<int>();
           
            for (int j = 0; j <= columnCount; j++)
            {
                rowSum.Add(0);
            }

            for (int i = hdps.Count - 1; i >= 0; i--)
            {
                List<int> row = data[hdps[i]];

                List<string> strRow = new List<string>();
                for (int j = 1; j <= columnCount; j++)
                {
                    strRow.Add("");
                }
                for (int j = 0; j < columnCount - 1; j++)
                {
                    int minute = columnCount - j - 1;
                    strRow[j + 1] = row[minute] > 0 ? row[minute].ToString() : "";
                    rowSum[j + 1] += row[minute];
                }
                strRow[0] = hdps[i].ToString();
                dgv.Rows.Add(strRow.ToArray());
            }

            List<string> rowSumStr = new List<string>();
            rowSumStr.Add("Sum");
            for (int j = 1; j <= columnCount; j++)
            {
                rowSumStr.Add(rowSum[j].ToString());
            }
            dgv.Rows.Add(rowSumStr.ToArray());

        }

        private void SetStyleBeforeLiveGrid(DataGridView dgv, int columnCount)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    dgv.Rows[i].Cells[j].Style.BackColor = (i % 2 == 0) ? System.Drawing.Color.LightGray : System.Drawing.Color.White;
                }
            }

            if (dgv.Rows.Count >= 2)
            {
                int rowSumIndex = dgv.Rows.Count - 1;
                int max = 1;
                for (int j = 1; j < columnCount; j++)
                {
                    string valueStr = dgv.Rows[rowSumIndex].Cells[j].Value.ToString();
                    if (valueStr != null && valueStr.Length > 0)
                    {
                        int valueInt = int.Parse(valueStr);
                        if (valueInt > max)
                        {
                            max = valueInt;
                        }
                    }
                }

                for (int j = 1; j < columnCount; j++)
                {
                    if(dgv.Rows[rowSumIndex].Cells[j].Value.ToString() == max.ToString())
                    {
                        dgv.Rows[rowSumIndex].Cells[j].Style.BackColor = Color.Red;
                    }
                }
            }

        }

        private void LoadDataOverUnder()
        {
            Dictionary<int, List<int>> dataOverUnderScoreTimesFreqBeforeLive;
            if (DataStore.OverUnderScoreTimesFreqBeforeLive.TryGetValue(_matchId, out dataOverUnderScoreTimesFreqBeforeLive))
            {
                ConstructBeforeLiveGrid(this.dgvOverUnderFt, 90);
                LoadDataBeforeLiveGrid(this.dgvOverUnderFt, dataOverUnderScoreTimesFreqBeforeLive, 90);
                SetStyleBeforeLiveGrid(this.dgvOverUnderFt, 90);
            }

            Dictionary<int, List<int>> dataOverUnderScoreTimesFreqFhBeforeLive;
            if (DataStore.OverUnderScoreTimesFreqFhBeforeLive.TryGetValue(_matchId, out dataOverUnderScoreTimesFreqFhBeforeLive))
            {
                ConstructBeforeLiveGrid(this.dgvOverUnderFh, 90);
                LoadDataBeforeLiveGrid(this.dgvOverUnderFh, dataOverUnderScoreTimesFreqFhBeforeLive, 90);
                SetStyleBeforeLiveGrid(this.dgvOverUnderFh, 90);
            }

            Dictionary<int, List<int>> dataHandicapFreqBeforeLive;
            if (DataStore.HandicapFreqBeforeLive.TryGetValue(_matchId, out dataHandicapFreqBeforeLive))
            {
                ConstructBeforeLiveGrid(this.dgvHandicapFt, 90);
                LoadDataBeforeLiveGrid(this.dgvHandicapFt, dataHandicapFreqBeforeLive, 90);
                SetStyleBeforeLiveGrid(this.dgvHandicapFt, 90);
            }

            Dictionary<int, List<int>> dataHandicapFreqFhBeforeLive;
            if (DataStore.HandicapFreqFhBeforeLive.TryGetValue(_matchId, out dataHandicapFreqFhBeforeLive))
            {
                ConstructBeforeLiveGrid(this.dgvHandicapFh, 90);
                LoadDataBeforeLiveGrid(this.dgvHandicapFh, dataHandicapFreqFhBeforeLive, 90);
                SetStyleBeforeLiveGrid(this.dgvHandicapFh, 90);
            }


            Dictionary<int, List<int>> dataOverUnderScoreTimesFreqHalfTime;
            if (DataStore.OverUnderScoreTimesFreqHalfTime.TryGetValue(_matchId, out dataOverUnderScoreTimesFreqHalfTime))
            {
                ConstructBeforeLiveGrid(this.dgvOverUnderHt, 30);
                LoadDataBeforeLiveGrid(this.dgvOverUnderHt, dataOverUnderScoreTimesFreqHalfTime, 30);
                SetStyleBeforeLiveGrid(this.dgvOverUnderHt, 30);
            }

            Dictionary<int, List<int>> dataHandicapFreqHalfTime;
            if (DataStore.HandicapFreqHalfTime.TryGetValue(_matchId, out dataHandicapFreqHalfTime))
            {
                ConstructBeforeLiveGrid(this.dgvHandicapHt, 30);
                LoadDataBeforeLiveGrid(this.dgvHandicapHt, dataHandicapFreqHalfTime, 30);
                SetStyleBeforeLiveGrid(this.dgvHandicapHt, 30);
            }
        }

        private void PriceFreqForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }
    }
}
