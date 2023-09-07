using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataModels
{

    public class TimeOfMatch
    {
        public SetOfMatch Set { get; set; }
        public int Minute { get; set; }
    }

    public enum SetOfMatch
    {
        First = 1,
        Second = 2
    }
}
