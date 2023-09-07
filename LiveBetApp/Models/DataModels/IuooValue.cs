using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class IuooValue
    {
        public int TX { get; set; }
    }
    public class IuooValue_2 
    {
        public int Hdp { get; set; }
        public int K { get; set; }
        public int GX { get; set; }
        public int TGX { get; set; }
        public int R { get; set; }
        public int TX { get; set; }
        public int TCLOSE { get; set; }
        public int TGT { get; set; }

        public int TW_a { get; set; }
        public int TWL_a { get; set; }

        public int TW_b { get; set; }
        public int TWL_b { get; set; }
    }

    public class IuooValue_3
    {
        public int Hdp { get; set; }
        public int Index { get { return (Hdp / 25) - 1; } }
        public int K { get; set; }
        public int GX { get; set; }
        public int TGX { get; set; }
        public int R { get; set; }
        public int TX { get; set; }
        public int TCLOSE { get; set; }
        public int TGT { get; set; }

        public int TW_a { get; set; }
        public int TWL_a { get; set; }

        public int TW_b { get; set; }
        public int TWL_b { get; set; }

        public int TW_c { get; set; }
        public int TWL_c { get; set; }
    }
}
