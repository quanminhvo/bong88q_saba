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
    public partial class BetCellHistoryForm : Form
    {
        private ListMatchForm _rootForm;
        private Thread _executeThread;
        private bool _refreshing;

        public BetCellHistoryForm(ListMatchForm rootForm, string homeName, string awayName, BetApp.Common.Enums.BetAreaIndex betAreaIndex, Common.Enums.BetAreaButton buttonIndex)
        {
            InitializeComponent();
            this.Text = homeName + " vs " + awayName + "(" + betAreaIndex.ToString() + " - " + buttonIndex + ")";
            dataGridView.AutoGenerateColumns = true;
            this._rootForm = rootForm;
            this._refreshing = true;
            int matchIndex = -1;

            _executeThread = new Thread(() =>
            {
                matchIndex = FindMatchIndex(homeName, awayName);
                while (matchIndex >= 0)
                {
                    Thread.Sleep(1000);
                    if (!this._refreshing) continue;
                    matchIndex = FindMatchIndex(homeName, awayName);
                    try
                    {
                        if(betAreaIndex == Common.Enums.BetAreaIndex.First)
                        {
                            UpdateMainGridByButtonIndex(rootForm.Matchs[matchIndex].BetArea.FirstBetRow, buttonIndex);
                        }
                        else if (betAreaIndex == Common.Enums.BetAreaIndex.Second)
                        {
                            UpdateMainGridByButtonIndex(rootForm.Matchs[matchIndex].BetArea.SecondBetRow, buttonIndex);
                        }
                        else if (betAreaIndex == Common.Enums.BetAreaIndex.Third)
                        {
                            UpdateMainGridByButtonIndex(rootForm.Matchs[matchIndex].BetArea.ThirdBetRow, buttonIndex);
                        }
                    }
                    catch
                    {

                    }
                }
            });

            _executeThread.IsBackground = true;
            _executeThread.Start();
        }

        private int FindMatchIndex(string homeName, string awayName)
        {
            for (int i = 0; i < _rootForm.Matchs.Count; i++)
            {
                if (_rootForm.Matchs[i].Home == homeName && _rootForm.Matchs[i].Away == awayName)
                {
                    return i;
                }
            }
            return -1;
        }

        private void UpdateMainGridByButtonIndex(BetApp.Models.SavedAsHistory.BetRow betRow, Common.Enums.BetAreaButton buttonIndex)
        {
            if (buttonIndex == Common.Enums.BetAreaButton.FullTimeHandicap)
            {
                UpdateMainGrid(betRow.FthdcBetCell);
            }

            if (buttonIndex == Common.Enums.BetAreaButton.FullTimeOverUnder)
            {
                UpdateMainGrid(betRow.FtouBetCell);
            }

            if (buttonIndex == Common.Enums.BetAreaButton.HalfTimeHandicap)
            {
                UpdateMainGrid(betRow.HthdcBetCell);
            }

            if (buttonIndex == Common.Enums.BetAreaButton.HalfTimeOverUnder)
            {
                UpdateMainGrid(betRow.HtouBetCell);
            }
        }

        private delegate void dlgUpdateMainGrid(BetApp.Models.SavedAsHistory.BetCellHistory betCellHistory);
        
        private void UpdateMainGrid(BetApp.Models.SavedAsHistory.BetCellHistory betCellHistory)
        {
            
            BindingSource bindingSource = new BindingSource();

            bindingSource.DataSource = BetApp.Common.Functions.ToDataTable(betCellHistory.History);

            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgUpdateMainGrid(UpdateMainGrid), betCellHistory);
            }
            else
            {
                dataGridView.DataSource = bindingSource;
            }
        }

        private void stopRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._refreshing = false;
        }

        private void startRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._refreshing = true;
        }

        private void BetCellHistoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }

        private void BetCellHistoryForm_Load(object sender, EventArgs e)
        {

        }
    }
}
