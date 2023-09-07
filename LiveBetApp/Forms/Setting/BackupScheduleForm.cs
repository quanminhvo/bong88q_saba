using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms.Setting
{
    public partial class BackupScheduleForm : Form
    {

        //public static int HourToBackUp { get { return 7; } }
        //public static int MinuteToBackUp { get { return 20; } }

        public BackupScheduleForm()
        {
            InitializeComponent();
            DateTime now = DateTime.Now;
            this.dtpTimeToBackup.Value = new DateTime(
                now.Year, 
                now.Month,
                now.Day,
                DataStore.HourToBackUp,
                DataStore.MinuteToBackUp,
                now.Second
            );
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            DataStore.MinuteToBackUp = this.dtpTimeToBackup.Value.Minute;
            DataStore.HourToBackUp = this.dtpTimeToBackup.Value.Hour;
            this.Close();
        }

        private void BackupScheduleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
