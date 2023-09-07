using App.DataModels;
using App.Forms.Configs;
using App.LogicServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class MainForm : Form
    {
        private List<MatchToWatch> _matchsToWatch;
        private Thread _executeThread;

        public MainForm()
        {
            InitializeComponent();
            Initialize();
            _executeThread = new Thread(() =>
            {
                Execute();
            });

            _executeThread.IsBackground = true;
            _executeThread.Start();

        }

        public List<MatchToWatch> MatchsToWatch
        {
            get
            {
                return _matchsToWatch;
            }

            set
            {
                this._matchsToWatch = value;
            }
        }

        private void Initialize()
        {
            _matchsToWatch = new List<MatchToWatch>();
        }

        private void Execute()
        {
            // Initialize the Chrome Driver
            using (var driver = new ChromeDriver())
            {

                SeleniumService.Login(driver);
                int errorCount = 0;

                while (true)
                {
                    try
                    {
                        ReadOnlyCollection<IWebElement> tables = driver.FindElementsByCssSelector("div.oddsTable.hdpou-a.sport1");
                        if (tables.Count < 2) continue;

                        IWebElement liveTable = SeleniumService.GetLiveTable(tables);
                        ReadOnlyCollection<IWebElement> matchAreas = SeleniumService.GetMatchAreasOfLiveTable(liveTable);
                        List<DataModels.Match> recoredMatchs = MatchService.GetSelectedMatchFromLiveTable(_matchsToWatch, matchAreas);

                        _matchsToWatch = MatchService.ProcessRecoredMatch(recoredMatchs, _matchsToWatch);
                        UpdateSummaryDataGrid(_matchsToWatch);

                    }
                    catch
                    {
                        errorCount++;
                    }
                }
            }
        }

        private delegate void dlgUpdateSummaryDataGrid(List<MatchToWatch> matchsToWatch);
        private void UpdateSummaryDataGrid(List<MatchToWatch> matchsToWatch)
        {
            List<MatchWithLastestBet> matchsWithLastestBet = new List<MatchWithLastestBet>();
            for (int i=0; i<matchsToWatch.Count; i++)
            {
                MatchWithLastestBet match = new MatchWithLastestBet()
                {
                    TeamHost = matchsToWatch[i].TeamHost,
                    TeamGuest = matchsToWatch[i].TeamGuest,
                    TeamStronger = matchsToWatch[i].TeamStronger
                };

                if (matchsToWatch[i].BetChangeHistory != null && matchsToWatch[i].BetChangeHistory.Count > 0)
                {
                    Bet bet = matchsToWatch[i].BetChangeHistory[matchsToWatch[i].BetChangeHistory.Count - 1];
                    match.Score = bet.Score;
                    match.TimePlaying = bet.TimePlaying;
                    match.RecordedDateTime = bet.RecordedDateTime;
                    match.TimeSpanFromLastChanges = bet.TimeSpanFromLastChanges;
                    match.FullTimeHandicap = bet.FullTimeHandicap;
                    match.FullTimeHandicap_HostRate = bet.FullTimeHandicap_HostRate;
                    match.FullTimeHandicap_GuestRate = bet.FullTimeHandicap_GuestRate;
                    match.FullTimeOverUnder = bet.FullTimeOverUnder;
                    match.FullTimeOverUnder_HostRate = bet.FullTimeOverUnder_HostRate;
                    match.FullTimeOverUnder_GuestRate = bet.FullTimeOverUnder_GuestRate;
                }
                matchsWithLastestBet.Add(match);
            }

            dataGridView.AutoGenerateColumns = true;

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Common.ToDataTable(matchsWithLastestBet);

            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgUpdateSummaryDataGrid(UpdateSummaryDataGrid), matchsToWatch);
            }
            else
            {
                dataGridView.DataSource = bindingSource;
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _matchsToWatch.Count) return;

            MatchToWatch selectedMatch = _matchsToWatch[e.RowIndex];
            using (DetailForm detailForm = new DetailForm(this, selectedMatch.TeamHost, selectedMatch.TeamGuest))
            {
                detailForm.Text = selectedMatch.TeamHost + " - " + selectedMatch.TeamGuest;
                detailForm.ShowDialog();
            }
            
        }

        private void matchsToWatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ListMatchToWatchForm form = new ListMatchToWatchForm(this._matchsToWatch.ToList()))
            {
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    this._matchsToWatch = form.Model;
                }
            }
        }
    }
}
