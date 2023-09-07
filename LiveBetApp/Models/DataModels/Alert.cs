using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBetApp.Models.DataModels
{
    public class Alert
    {
        public Alert(bool deleted, bool isNew)
        {
            Deleted = deleted;
            IsNew = isNew;
            this.CreateTime = DateTime.Now;
            this.CustomValue = "0";
        }

        public bool Deleted { get; set; }
        public bool IsNew { get; set; }
        public DateTime CreateTime { get; set; }
        public string CustomValue { get; set; }

        public TimeSpan TimeSpanFromCreate
        {
            get
            {
                return DateTime.Now.Subtract(CreateTime);
            }
        }
    }
}
