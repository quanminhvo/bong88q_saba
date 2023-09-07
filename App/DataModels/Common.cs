using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModels
{
    public enum CustomCompairResult
    {
        Equal = 0,
        LeftGreater = 1,
        RightGreater = 2
    }

    public enum enumTimmingBetUserControlTypeEnum
    {
        FullTimeHandicapHome = 1,
        FullTimeHandicapAway = 2,
        FullTimeOverUnderHome = 3,
        FullTimeOverUnderAway = 4
    }

    public class TimmingBetUserControlType
    {
        public TimmingBetUserControlType(string name, enumTimmingBetUserControlTypeEnum value)
        {
            this.Name = name;
            this.Value = value;
        }
        public string Name { get; set; }
        public enumTimmingBetUserControlTypeEnum Value { get; set; }
    }

    public class TimmingBetUserControlModel
    {
        public enumTimmingBetUserControlTypeEnum Type { get; set; }
        public int Amount { get; set; }
        public string BetTime { get; set; }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
