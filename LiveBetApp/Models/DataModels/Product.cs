using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class Product
    {
        public Product()
        {
        }
        
        public long OddsId { get; set; }
        public long MatchId { get; set; }
        public Common.Enums.BetType Bettype { get; set; }
        public string OddsStatus { get; set; }
        public float Hdp1 { get; set; }
        public float Hdp2 { get; set; }
        public int MaxBet { get; set; }

        public float Odds1a { get; set; }
        public float Odds2a { get; set; }
        public float Com1 { get; set; }
        public float Comx { get; set; }
        public float Com2 { get; set; }

        public int Odds1a100
        {
            get { return (int)Math.Round(Odds1a * 100); }
        }
        public int Odds2a100
        {
            get { return (int)Math.Round(Odds2a * 100); }
        }

    }
}
