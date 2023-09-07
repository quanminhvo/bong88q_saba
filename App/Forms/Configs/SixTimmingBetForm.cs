using App.DataModels;
using App.LogicServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Forms.Configs
{
    public partial class SixTimmingBetForm : Form
    {
        private SixTimingBetButtons _model;
        public SixTimingBetButtons Model 
        {
            set { this._model = value; }
            get 
            {
                if (this._model == null) this._model = new SixTimingBetButtons();
                return this._model; 
            }
        }

        public SixTimmingBetForm(SixTimingBetButtons model)
        {
            this.Model = model;
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            this.firstTimmingBetUC.Model = ConstructTimmingBetUserControlModel(this.Model.FirstTimingBetButton);
            this.secondTimmingBetUC.Model = ConstructTimmingBetUserControlModel(this.Model.SecondTimingBetButton);
            this.thirdTimmingBetUC.Model = ConstructTimmingBetUserControlModel(this.Model.ThirdTimingBetButton);
            this.fourthTimmingBetUC.Model = ConstructTimmingBetUserControlModel(this.Model.FourthTimingBetButton);
            this.fifthTimmingBetUC.Model = ConstructTimmingBetUserControlModel(this.Model.FifthTimingBetButton);
            this.sixthTimmingBetUC.Model = ConstructTimmingBetUserControlModel(this.Model.SixthTimingBetButton);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateForm()) return;
            ConstructModel();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ConstructModel()
        {
            this._model = new SixTimingBetButtons();

            this._model.FirstTimingBetButton = ConstructBetButtonOfMatch(firstTimmingBetUC.Model);
            this._model.SecondTimingBetButton = ConstructBetButtonOfMatch(secondTimmingBetUC.Model);
            this._model.ThirdTimingBetButton = ConstructBetButtonOfMatch(thirdTimmingBetUC.Model);
            this._model.FourthTimingBetButton = ConstructBetButtonOfMatch(fourthTimmingBetUC.Model);
            this._model.FifthTimingBetButton = ConstructBetButtonOfMatch(fifthTimmingBetUC.Model);
            this._model.SixthTimingBetButton = ConstructBetButtonOfMatch(sixthTimmingBetUC.Model);
            
        }

        private TimmingBetUserControlModel ConstructTimmingBetUserControlModel(BetButtonOfMatch betButtonOfMatch)
        {
            TimmingBetUserControlModel result = new TimmingBetUserControlModel();
            
            if (betButtonOfMatch.FullTimeHandicap_TeamGuest.TimeToBet != null)
            {
                result.Type = enumTimmingBetUserControlTypeEnum.FullTimeHandicapAway;
                result.Amount = betButtonOfMatch.FullTimeHandicap_TeamGuest.BetAmount;
                result.BetTime = betButtonOfMatch.FullTimeHandicap_TeamGuest.TimeToBet;
            }
            else if (betButtonOfMatch.FullTimeOverUnder_TeamGuest.TimeToBet != null)
            {
                result.Type = enumTimmingBetUserControlTypeEnum.FullTimeOverUnderAway;
                result.Amount = betButtonOfMatch.FullTimeOverUnder_TeamGuest.BetAmount;
                result.BetTime = betButtonOfMatch.FullTimeOverUnder_TeamGuest.TimeToBet;
            }
            else if (betButtonOfMatch.FullTimeOverUnder_TeamHost.TimeToBet != null)
            {
                result.Type = enumTimmingBetUserControlTypeEnum.FullTimeOverUnderHome;
                result.Amount = betButtonOfMatch.FullTimeOverUnder_TeamHost.BetAmount;
                result.BetTime = betButtonOfMatch.FullTimeOverUnder_TeamHost.TimeToBet;
            }
            else //if (betButtonOfMatch.FullTimeHandicap_TeamHost.TimeToBet != null)
            {
                result.Type = enumTimmingBetUserControlTypeEnum.FullTimeHandicapHome;
                result.Amount = betButtonOfMatch.FullTimeHandicap_TeamHost.BetAmount;
                result.BetTime = betButtonOfMatch.FullTimeHandicap_TeamHost.TimeToBet;
            }
            return result;
        }

        private BetButtonOfMatch ConstructBetButtonOfMatch(TimmingBetUserControlModel ucModel)
        {
            BetButtonOfMatch result = new BetButtonOfMatch();
            TimmingBet timmingBet = new TimmingBet() {
                BetAmount = (int)ucModel.Amount,
                TimeToBet = ucModel.BetTime
            };

            if(ucModel.Type == enumTimmingBetUserControlTypeEnum.FullTimeHandicapAway)
            {
                result.FullTimeHandicap_TeamGuest = timmingBet;
            }
            else if (ucModel.Type == enumTimmingBetUserControlTypeEnum.FullTimeHandicapHome)
            {
                result.FullTimeHandicap_TeamHost = timmingBet;
            }
            else if (ucModel.Type == enumTimmingBetUserControlTypeEnum.FullTimeOverUnderAway)
            {
                result.FullTimeOverUnder_TeamGuest = timmingBet;
            }
            else
            {
                result.FullTimeOverUnder_TeamHost = timmingBet;
            }

            return result;
        }

        private bool ValidateForm()
        {
            bool result = true;

            string firstTime = this.firstTimmingBetUC.Model.BetTime;
            string secondTime = this.secondTimmingBetUC.Model.BetTime;
            string thirdTime = this.thirdTimmingBetUC.Model.BetTime;
            string fourthTime = this.fourthTimmingBetUC.Model.BetTime;
            string fifthTime = this.fifthTimmingBetUC.Model.BetTime;
            string sixthTime = this.sixthTimmingBetUC.Model.BetTime;

            this.firstTimmingBetUC.SetGbTimeColor(SystemColors.Control);
            this.secondTimmingBetUC.SetGbTimeColor(SystemColors.Control);
            this.thirdTimmingBetUC.SetGbTimeColor(SystemColors.Control);
            this.fourthTimmingBetUC.SetGbTimeColor(SystemColors.Control);
            this.fifthTimmingBetUC.SetGbTimeColor(SystemColors.Control);
            this.sixthTimmingBetUC.SetGbTimeColor(SystemColors.Control);

            if(CommonFunctions.CompairTimeOfMatch(firstTime, secondTime) !=  CustomCompairResult.RightGreater)
            {
                this.secondTimmingBetUC.SetGbTimeColor(Color.Red);
                result = false;
            }

            if (CommonFunctions.CompairTimeOfMatch(secondTime, thirdTime) != CustomCompairResult.RightGreater)
            {
                this.thirdTimmingBetUC.SetGbTimeColor(Color.Red);
                result = false;
            }

            if (CommonFunctions.CompairTimeOfMatch(thirdTime, fourthTime) != CustomCompairResult.RightGreater)
            {
                this.fourthTimmingBetUC.SetGbTimeColor(Color.Red);
                result = false;
            }

            if (CommonFunctions.CompairTimeOfMatch(fourthTime, fifthTime) != CustomCompairResult.RightGreater)
            {
                this.fifthTimmingBetUC.SetGbTimeColor(Color.Red);
                result = false;
            }

            if (CommonFunctions.CompairTimeOfMatch(fifthTime, sixthTime) != CustomCompairResult.RightGreater)
            {
                this.sixthTimmingBetUC.SetGbTimeColor(Color.Red);
                result = false;
            }

            return result;
        }
    }
}
