using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.DataModels;

namespace App.Forms.Configs
{
    public partial class TimmingBetUserControl : UserControl
    {
        public TimmingBetUserControl()
        {
            InitializeComponent();
            InitCbbTimmingBetType();
            InitCbbSet();
            InitCbbMinute();
        }

        public void SetGbTimeColor(Color color)
        {
            this.groupBox1.BackColor = color;
        }

        public TimmingBetUserControlModel Model
        {
            get
            {
                return new TimmingBetUserControlModel()
                {
                    Amount = (int)this.numericBetAmount.Value,
                    BetTime = this.cbbSet.SelectedItem.ToString() + " " + this.cbbMinute.SelectedItem.ToString(),
                    Type = (enumTimmingBetUserControlTypeEnum)this.cbbType.SelectedValue
                };
            }
            set
            {
                this.numericBetAmount.Value = value.Amount;
                if (value.BetTime != null && value.BetTime.Length > 1)
                {
                    this.cbbSet.SelectedValue = value.BetTime.Split(' ')[0];
                    this.cbbSet.SelectedText = value.BetTime.Split(' ')[0];

                    //string test = value.BetTime.Split(' ')[1];

                    this.cbbMinute.SelectedValue = value.BetTime.Split(' ')[1];
                    this.cbbMinute.SelectedText = value.BetTime.Split(' ')[1];
                    //this.cbbMinute.SelectedIndex = 10;
                }
                this.cbbType.SelectedValue = value.Type;
            }
        }

        private void InitCbbSet()
        {
            cbbSet.DataSource = new List<ComboboxItem>() {
                new ComboboxItem { Text = "1H", Value = "1H"},
                new ComboboxItem { Text = "2H", Value = "2H"}
            };
            cbbSet.DisplayMember = "Text";
            cbbSet.ValueMember = "Value";
        }

        private void InitCbbMinute()
        {
            List<ComboboxItem> listMinute = new List<ComboboxItem>();
            for (int i = 1; i <= 50; i++)
            {
                listMinute.Add(new ComboboxItem { Text = i + "'", Value = i + "'" });
            }
            cbbMinute.DataSource = listMinute;
            cbbMinute.DisplayMember = "Text";
            cbbMinute.ValueMember = "Value";
        }

        private void InitCbbTimmingBetType()
        {
            List<TimmingBetUserControlType> timmingBetType = new List<TimmingBetUserControlType>
            {
                new TimmingBetUserControlType("Full time handicap - Home", enumTimmingBetUserControlTypeEnum.FullTimeHandicapHome),
                new TimmingBetUserControlType("Full time handicap - Away", enumTimmingBetUserControlTypeEnum.FullTimeHandicapAway),
                new TimmingBetUserControlType("Full time over under - Home", enumTimmingBetUserControlTypeEnum.FullTimeOverUnderHome),
                new TimmingBetUserControlType("Full time over under - Away", enumTimmingBetUserControlTypeEnum.FullTimeOverUnderAway)
            };

            cbbType.DataSource = timmingBetType;
            cbbType.DisplayMember = "Name";
            cbbType.ValueMember = "Value";
        }
    }
}
