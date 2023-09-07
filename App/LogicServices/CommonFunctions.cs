using App.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.LogicServices
{
    public static class CommonFunctions
    {
        public static CustomCompairResult CompairTimeOfMatch (string timeA, string timeB)
        {
            int setOfTimeA = (int)timeA[0];
            int setOfTimeB =  (int)timeB[0];
            int minuteOfTimeA;
            int minuteOfTimeB;
            int.TryParse(timeA.Split(' ')[1].Split('\'')[0], out minuteOfTimeA);
            int.TryParse(timeB.Split(' ')[1].Split('\'')[0], out minuteOfTimeB);

            if (timeA == timeB) return CustomCompairResult.Equal;
            else if (setOfTimeA > setOfTimeB) return CustomCompairResult.LeftGreater;
            else if (setOfTimeA < setOfTimeB) return CustomCompairResult.RightGreater;
            else if (minuteOfTimeA > minuteOfTimeB) return CustomCompairResult.LeftGreater;
            else if (minuteOfTimeA < minuteOfTimeB) return CustomCompairResult.RightGreater;
            else return CustomCompairResult.Equal;
        }
    }
}
