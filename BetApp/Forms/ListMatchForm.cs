using BetApp.Services;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ListMatchForm : Form
    {
        private List<BetApp.Models.SavedAsHistory.Match> _matchs;
        private bool _refreshing;
        private Thread _executeThread;



        public ListMatchForm()
        {
            InitializeComponent();
            Initialize();
            SeleniumService seleniumService = new SeleniumService();
            using (var driver = new ChromeDriver())
            {
                //seleniumService.Login(driver);
                driver.Navigate().GoToUrl("https://www.bong99.com/ty-le-ca-cuoc-bong-da.html");
                String scriptToExecute = "var performance = window.performance || window.mozPerformance || window.msPerformance || window.webkitPerformance || {}; var network = performance.getEntries() || {}; return network;";
                Thread.Sleep(20000);
                object netData = ((IJavaScriptExecutor)driver).ExecuteScript(scriptToExecute);
                string json = JsonConvert.SerializeObject(netData);

                System.IO.File.WriteAllText(@"test.txt", json);


            
            
            }

            //_executeThread = new Thread(() =>
            //{
            //    Execute();
            //});
            //_executeThread.IsBackground = true;
            //_executeThread.Start();

        }

        public List<BetApp.Models.SavedAsHistory.Match> Matchs
        {
            get
            {
                return this._matchs;
            }
        }

        private void Initialize()
        {
            _matchs = new List<BetApp.Models.SavedAsHistory.Match>();
        }

        public void Execute()
        {
            SeleniumService seleniumService = new SeleniumService();
            HistoryMatchService historyMatchService = new Services.HistoryMatchService();
            List<Models.ReadFromWeb.Match> matchReadFromWeb;
            this._refreshing = true;

            using (var driver = new ChromeDriver())
            {
                seleniumService.Login(driver);
                int errorCount = 0;
                while (true)
                {
                    try
                    {
                        matchReadFromWeb = new List<Models.ReadFromWeb.Match>();

                        ReadOnlyCollection<IWebElement> tables = driver.FindElementsByCssSelector("div.oddsTable.hdpou-a.sport1");

                        IWebElement liveTable = seleniumService.GetLiveTable(tables);

                        if (liveTable == null) continue;

                        ReadOnlyCollection<IWebElement> matchAreas = seleniumService.GetMatchAreasOfLiveTable(liveTable);

                        

                        Parallel.For(0, matchAreas.Count, new ParallelOptions() { MaxDegreeOfParallelism = 8 }, index => {
                            try
                            {
                                matchReadFromWeb.Add(seleniumService.DeserializeMatchArea(matchAreas[index]));
                            }
                            catch
                            {

                            }
                        });

                        //foreach (var matchArea in matchAreas)
                        //{
                        //    try
                        //    {
                        //        matchReadFromWeb.Add(seleniumService.DeserializeMatchArea(matchArea));
                        //    }
                        //    catch
                        //    {

                        //    }
                            
                        //}

                        historyMatchService.ProcessRecoredMatch(matchReadFromWeb);
                        _matchs = historyMatchService.HistoryMatchs;
                        if (this._refreshing)
                        {
                            UpdateSummaryDataGrid(historyMatchService.HistoryMatchs);
                        }
                        

                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                    }
                }
            }
        }

        private delegate void dlgUpdateDataGrid(List<BetApp.Models.SavedAsHistory.Match> matchs);
        private void UpdateSummaryDataGrid(List<BetApp.Models.SavedAsHistory.Match> matchs)
        {
            List<BetApp.Models.ViewModels.Match> matchsToShow = new List<BetApp.Models.ViewModels.Match>();

            for (int i=0; i<matchs.Count; i++)
            {
                BetApp.Models.ViewModels.Match matchToShow = new Models.ViewModels.Match() {
                    Away = matchs[i].Away,
                    Home = matchs[i].Home
                };

                try
                {
                    matchToShow.Score = matchs[i].Score;
                    matchToShow.TimePlaying = matchs[i].TimePlaying;
                }
                catch
                {

                }

                matchsToShow.Add(matchToShow);
            }

            dataGridView.AutoGenerateColumns = true;
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = BetApp.Common.Functions.ToDataTable(matchsToShow);

            if (this.dataGridView.InvokeRequired)
            {
                this.Invoke(new dlgUpdateDataGrid(UpdateSummaryDataGrid), matchs);
            }
            else
            {
                dataGridView.DataSource = bindingSource;
            }

        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _matchs.Count) return;

            using (BetRowForm betRowForm = new BetRowForm(this, _matchs[e.RowIndex].Home, _matchs[e.RowIndex].Away))
            {
                betRowForm.ShowDialog();
            }
        }

        private void stopRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void startRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ListMatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }

    }
}
