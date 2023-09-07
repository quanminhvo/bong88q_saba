﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.ViewModels
{
    public class ProductStatusLog
    {
        public string STT { get; set; }
        public string Status { get; set; }
        public string ServerAction { get; set; }
        public Common.Enums.BetType Type { get; set; }
        public string TypeStr { get; set; }
        public float Hdp1 { get; set; }
        public float Hdp2 { get; set; }
        public int LivePeriod { get; set; }
        public bool IsHt { get; set; }
        public int Hdp100
        {
            get
            {
                return (int)((Hdp1 + Hdp2) * 100);
            }
        }
        public int Price { get; set; }
        public DateTime DtLog { get; set; }
        public string DtLogStr { get; set; }
    }
}
