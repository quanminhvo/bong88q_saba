using LiveBetApp.Forms;
using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LiveBetApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Common.Functions.InitAutoMapper();
            Common.Functions.InitDataStoreOneTime();
            Common.Functions.InitDataStore();
            DateTime now = DateTime.Now;
            if (now.Hour <= DataStore.HourToBackUp
                && now.Minute <= DataStore.MinuteToBackUp)
            {
                now = now.AddDays(-1);
            }
            Common.Functions.RestoreDatastore(now);

            if (DataStore.Blacklist.Count == 0)
            {
                Common.Functions.InitBlackList();
            }
            
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PasswordForm());
            }
            catch (Exception ex)
            {
                Common.Functions.WriteExceptionLog(ex);
            }
            finally
            {
                Common.Functions.BackUpDatastore(DateTime.Now);
                Environment.Exit(Environment.ExitCode);
            }
        }


    }
}
