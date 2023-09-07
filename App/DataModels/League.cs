using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModels
{
    public class League
    {
        public League()
        {
            this.Maths = new List<Match>();
        }

        public string Name { get; set; }
        public List<Match> Maths { get; set; }
    }
}
