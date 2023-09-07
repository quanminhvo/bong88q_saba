using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.ViewModels
{
    public class ProductStatusLogExport
    {
        public DateTime DtLog { get; set; }
        public DateTime GlobalShowTime { get; set; }
        public string League { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public string Desc { get; set; }
    }
}
