using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCookieSample
{
    public static class AppWebDriver
    {
        private static ChromeDriver _appChromeDriver = null;
        public static ChromeDriver AppChromeDriver
        {
            get
            {
                if (_appChromeDriver == null) _appChromeDriver = new ChromeDriver();
                return _appChromeDriver;
            }
        }


        public static void Dispose()
        {
            if (_appChromeDriver != null)
            {
                _appChromeDriver.Dispose();
            }
        }
    }
}
