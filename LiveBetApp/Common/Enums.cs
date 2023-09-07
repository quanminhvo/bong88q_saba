using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Common
{
    public class Enums
    {
        public enum AlertWd15
        {
            None = 0,
            LightPink = 1,
            SkyBlue = 2,
            LightYellow = 3,
            LightGreen = 4
        }

        public enum LiveSteamSearch
        {
            All = 0,
            Live = 1,
            NoneLive = 2
        }

        public enum RunningFinishedSearch
        {
            Planing = 0,
            Running = 1,
            Finished = 2
        }

        public enum BetType
        {
            Unknown = -1,
            FullTimeHandicap = 1,
            FullTimeOverUnder = 3,
            FullTime1x2 = 5,
            FirstHalfHandicap = 7,
            FirstHalfOverUnder = 8,
            FirstHalf1x2 = 15,
        }

        public enum BetByHdpCloseOption
        {
            FIRST_HALF_MAX = 1,
            FIRST_HALF_MIN = 2,
            FULL_TIME_HDP_025 = 3,
            FULL_TIME_HDP_050 = 4,
            FULL_TIME_HDP_075 = 5,
            FULL_TIME_HDP_100 = 6
        }

        public enum BetByTimmingOption
        {
            FIRST_HALF_MAX = 1,
            FIRST_HALF_MIN = 2,
            FULL_TIME_TYPE_X00 = 3,
            FULL_TIME_TYPE_X25 = 4,
            FULL_TIME_TYPE_X50 = 5,
            FULL_TIME_TYPE_X75 = 6,

        }

        public enum VisualThemes
        {
            PC = 1,
            TV_FULL_HD = 2,
            TV_HD = 3,
            TV_HDTV = 4

        }

        public enum BetHandicapTeamStronger
        {
            Home = 1,
            Away = 2,
            Same = 0
        }
    }
}
