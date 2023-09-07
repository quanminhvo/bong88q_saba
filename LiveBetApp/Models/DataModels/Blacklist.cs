using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class BlackList
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string League { get; set; }
        
    }
}
