using LiveBetApp.Models.DataModels;
using LiveBetApp.Services;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Internal;
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

namespace LiveBetApp.Forms.DirectBet
{
    public partial class ConfirmDirectBet : Form
    {
        private long _matchId;
        private bool _isFt;
        private Thread _executeThread;

        public ConfirmDirectBet(long matchId, bool isFt)
        {
            _matchId = matchId;
            _isFt = isFt;
            InitializeComponent();
        }

        private List<Product> GetOverUnderProducts()
        {
            List<Product> result = new List<Product>();
            if (_isFt)
            {
                result = DataStore.Products.Values.Where(item => item.MatchId == _matchId && item.Bettype == Common.Enums.BetType.FullTimeOverUnder).OrderByDescending(item => item.Hdp1).ToList();
            }
            else
            {
                result = DataStore.Products.Values.Where(item => item.MatchId == _matchId && item.Bettype == Common.Enums.BetType.FirstHalfOverUnder).OrderByDescending(item => item.Hdp1).ToList();
            }
            
            return result;
        }

        private Match GetMatch()
        {
            Match match = null;
            if (DataStore.Matchs.ContainsKey(_matchId))
            {
                match = DataStore.Matchs[_matchId];
            }
            return match;
        }

        private void btnA00_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure??", "Confirm Bet!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                PlaceBet(1);
            }
            this.Close();
        }

        private void btnA25_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure??", "Confirm Bet!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                PlaceBet(2);
            }
            this.Close();
        }

        private void btnA50_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure??", "Confirm Bet!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                PlaceBet(3);
            }
            this.Close();
        }

        private void btnA75_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure??", "Confirm Bet!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                PlaceBet(4);
            }
            this.Close();
        }

        private void PlaceBet(int order)
        {
            this.btnA00.Enabled = false;
            this.btnA25.Enabled = false;
            this.btnA50.Enabled = false;
            this.btnA75.Enabled = false;

            if (!DataStore.BetDirect.Any(item => item.MatchId == _matchId && item.IsFt == _isFt))
            {
                DataStore.BetDirect.Add(new BetDirect()
                {
                    Id = Guid.NewGuid(),
                    IsFt = _isFt,
                    MatchId = _matchId,
                    Order = order,
                    Stake = (int)this.numStake.Value,
                    CreateDateTime = DateTime.Now
                });
            }

        }

        private void Bet(int order, int stake, bool isFt, int errorCount)
        {
            if (errorCount >= 100) return;
            
            List<Product> products = GetOverUnderProducts();
            Match match = GetMatch();
            Product product = null;
            if (products.Count > order - 1 && match != null)
            {
                product = products[order - 1];
            }
            else
            {
                Thread.Sleep(1000);
                Bet(order, stake, isFt, errorCount + 1);
                return;
            }
            
            Thread.Sleep(5000);
            try
            {
                ProcessBetModel model = Common.Functions.ConstructProcessBetOverUnderModelV2(product, match, stake, false, isFt);
                JToken ticket = new ProcessBetService().GetTicket(model, DataStore.Config.cookie, DataStore.Config.getTicketUrl);

                if (ticket != null)
                {
                    if (ticket["ErrorCode"] != null && ticket["ErrorCode"].ToString() != "0")
                    {
                        Bet(order, stake, isFt, errorCount + 1);
                        return;
                    }
                    else if (ticket["Data"] != null && ticket["Data"][0] != null)
                    {
                        int maxbet = 10;
                        int currentBet = 10;
                        if (int.TryParse(ticket["Data"][0]["Maxbet"].ToString().Replace(",", ""), out maxbet)
                            && int.TryParse(model.Stake, out currentBet))
                        {
                            if (currentBet > maxbet)
                            {
                                model.Stake = maxbet.ToString();
                            }
                        }
                    }
                }

                string betResultString = new ProcessBetService().DoBet(model, DataStore.Config.cookie, DataStore.Config.processBetUrl);
                JToken betResultJToken = JToken.Parse(betResultString);

                if (Common.Functions.CheckBetResult(betResultJToken))
                {
                    string message =
                        (betResultJToken["Data"]["ItemList"][0]["Message"] != null ? betResultJToken["Data"]["ItemList"][0]["Message"].ToString() : "")
                        + " - Code "
                        + (betResultJToken["Data"]["ItemList"][0]["Code"] != null ? betResultJToken["Data"]["ItemList"][0]["Code"].ToString() : "");
                    Common.Functions.WriteJsonObjectData<JToken>(
                        "BetLog\\betResultJToken_ok_" + Common.Functions.GetDimDatetime(DateTime.Now),
                        betResultJToken
                    );
                    SetResultBetMessage(message, product, match, stake, _isFt);

                    return;
                }
                else
                {
                    Common.Functions.WriteJsonObjectData<JToken>(
                        "BetLog\\betResultJToken_fail_" + Common.Functions.GetDimDatetime(DateTime.Now),
                        betResultJToken
                    );

                    Bet(order, stake, isFt, errorCount + 1);
                    return;
                }

            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
                return;
            }
            
        }

        private void SetResultBetMessage(string message, Product product, Match match, int stake, bool isFt)
        {
            LiveBetApp.Models.DataModels.BetByHdpPrice selectedItem = new Models.DataModels.BetByHdpPrice();
            selectedItem.ResultMessage = message;
            selectedItem.Id = Guid.NewGuid();
            selectedItem.Stake = stake;
            selectedItem.IsFulltime = isFt;
            selectedItem.Hdp = (int)(product.Hdp1 * 100);
            selectedItem.Price = product.Odds1a100;
            DataStore.FinishedBetByHdpPrice.Add(selectedItem);
        }

        private void ConfirmDirectBet_Load(object sender, EventArgs e)
        {

        }

        private void btnStake3_Click(object sender, EventArgs e)
        {
            this.numStake.Value = 100;
        }

        private void btnStake100_Click(object sender, EventArgs e)
        {
            this.numStake.Value = 300;
        }

        private void btnStake500_Click(object sender, EventArgs e)
        {
            this.numStake.Value = 500;
        }

        private void btnStake1000_Click(object sender, EventArgs e)
        {
            this.numStake.Value = 1000;
        }
    }
}
