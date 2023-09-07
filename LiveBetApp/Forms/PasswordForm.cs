using LiveBetApp.Services;
using LiveBetApp.Services.AutoBet;
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

namespace LiveBetApp.Forms
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
            InitCbbFtHt();
            InitCbbVersion();
            this.lblMessage.Visible = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //DataStore.Theme = (LiveBetApp.Common.Enums.VisualThemes)cbbThemes.SelectedValue;
            //new MainForm().Show();
            //this.Hide();
            //return;

            this.lblMessage.Visible = true;
            this.lblMessage.Text = "Đang kiểm tra Password...";
            int cbbValue = (int)cbbVersion.SelectedValue;
            DataStore.SemiAutoBetOnly = cbSemiAutoBet.Checked;

            //if (AuthorizationService.Check())
            //if (new MailService().CheckPassword(this.txtPassword.Text))
            {
                DataStore.Theme = (LiveBetApp.Common.Enums.VisualThemes)cbbThemes.SelectedValue;
                DataStore.AppStartLog.Add(long.Parse(DateTime.Now.ToString(Common.Constants.NumDtFormat)));
                new MainForm().Show();
                
                if (cbbValue == 1)
                {
                    new Services.CoreService().Execute();
                    new HasStreamingService().Execute(60000);
                }
                else if (cbbValue == 2)
                {
                    new Services.CoreServiceV2().Execute();
                    new HasStreamingService().Execute(60000);
                }
                else if (cbbValue == 3)
                {
                    new Services.CoreServiceSbo().Execute();
                }

                if (!DataStore.SemiAutoBetOnly)
                {
                    new UpdateOverUnderScoreTimeService().Execute(60000);
                    new UpdateOverUnderScoreTimeService_Fast().Execute(20000);
                    new UpdateOverUnderScoreTimeFirstHalfService().Execute(60000);
                    new UpdateHandicapScoreTimeService().Execute(60000);
                    new UpdateMaxBetNoneRequestService().Execute(5000);
                    new AlertService().Execute(60000);
                    new AlertService20().Execute(60000);
                    new UpdateMaxBetService(1000).Execute();
                }


                if (Common.Functions.CanBetting() && cbbValue != 3)
                {
                    new TimmingBetOverUnderService().Execute(5000);
                    new BetByHdpPricesService().Execute(5000);
                    new BetFirstHalfByFulltimeService().Execute(5000);
                    new BetByHdpCloseService().Execute(5000);
                    new BetByTimmingService().Execute(5000);
                    new BetAfterGoodPriceService().Execute(5000);
                    new BetDirectService().Execute(5000);
                    new BetByQuickService().Execute(5000);
                    new BetHandicapByPriceService().Execute(5000);
                    //if (Common.Functions.IsDecoy())
                    //{
                    //    new BetInUnderOutOverService().Execute(60000);
                    //}

                    //if (Common.Functions.CanRunAutoBet()
                    //    && !DataStore.SemiAutoBetOnly)
                    //{
                    //    new Many_Goal_1H_Under_100_V1().Execute(60000);
                    //    new Open250_Line18_1_2().Execute(60000);
                    //    //new Open250_Line18_3().Execute(60000);
                    //}
                }
                this.Hide();
            }
            //else
            //{
            //    this.lblMessage.Text = "Password sai!!!";
            //}
        }

        private void InitCbbFtHt()
        {
            List<LiveBetApp.Models.ViewModels.ComboboxItem> items = new List<LiveBetApp.Models.ViewModels.ComboboxItem>();

            
            items.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "TV (1920 x 1080)", Value = LiveBetApp.Common.Enums.VisualThemes.TV_FULL_HD });
            items.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "PC", Value = LiveBetApp.Common.Enums.VisualThemes.PC });
            items.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "TV (1360 x 768)", Value = LiveBetApp.Common.Enums.VisualThemes.TV_HD });
            items.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "TV (1280 x 720)", Value = LiveBetApp.Common.Enums.VisualThemes.TV_HDTV });

            cbbThemes.DataSource = items;
            cbbThemes.DisplayMember = "Text";
            cbbThemes.ValueMember = "Value";
        }

        private void InitCbbVersion()
        {
            List<LiveBetApp.Models.ViewModels.ComboboxItem> items = new List<LiveBetApp.Models.ViewModels.ComboboxItem>();


            items.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "v2", Value = 2 });
            items.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "v1", Value = 1 });
            items.Add(new LiveBetApp.Models.ViewModels.ComboboxItem { Text = "sbo", Value = 3 });


            cbbVersion.DataSource = items;
            cbbVersion.DisplayMember = "Text";
            cbbVersion.ValueMember = "Value";
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
