using App.DataModels;
using App.LogicServices;
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

namespace App
{
    public partial class DetailForm : Form
    {
        private MainForm _mainForm = null;
        private Thread _executeThread;
        private string _teamHost;
        private string _teamGuest;

        public DetailForm(MainForm mainForm, string teamHost, string TeamGuest)
        {
            _mainForm = mainForm as MainForm;
            _teamHost = teamHost;
            _teamGuest = TeamGuest;
            InitializeComponent();

            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        UpdateMainGrid(_mainForm.MatchsToWatch);
                    }
                    catch
                    {

                    }
                }
            });

            _executeThread.IsBackground = true;
            _executeThread.Start();


        }

        private delegate void dlgUpdateMainGrid(List<MatchToWatch> matchsToWatch);
        private void UpdateMainGrid(List<MatchToWatch> matchsToWatch)
        {
            dataGridView.AutoGenerateColumns = true;
            BindingSource bindingSource = new BindingSource();
            MatchToWatch selectedMatchToWatch = MatchService.FindMatchToWatchByTeamName(matchsToWatch, _teamHost, _teamGuest);

            Bet[] reservedBetChangeHistory = new Bet[selectedMatchToWatch.BetChangeHistory.Count()];
            selectedMatchToWatch.BetChangeHistory.CopyTo(reservedBetChangeHistory);
            bindingSource.DataSource = Common.ToDataTable(reservedBetChangeHistory.Reverse().ToList());

            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgUpdateMainGrid(UpdateMainGrid), matchsToWatch);
            }
            else
            {
                dataGridView.DataSource = bindingSource;
            }
        }

        private void DetailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }
    }
}
