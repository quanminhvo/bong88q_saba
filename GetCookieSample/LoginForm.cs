using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetCookieSample
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLoginAndGetConfig_Click(object sender, EventArgs e)
        {
            var driver = AppWebDriver.AppChromeDriver;
            driver.Navigate().GoToUrl(txtUrl.Text);
            driver.FindElementById("txtID").SendKeys(txtUsername.Text);
            driver.FindElementById("txtPW").SendKeys(txtPassword.Text);
            driver.FindElementByCssSelector("a.login_btn > span").Click();

            Thread.Sleep(30000);

            try
            {
                List<OpenQA.Selenium.Cookie> cookies = driver.Manage().Cookies.AllCookies.ToList();
                string cookieString = "";

                if (cookies != null)
                {
                    foreach (OpenQA.Selenium.Cookie cookie in cookies)
                    {
                        cookieString += (cookie.Name + "=" + cookie.Value + "; ");
                    }
                }

                Uri currentUri = new Uri(driver.Url);

                ConfigResultForm form = new ConfigResultForm("", currentUri.Scheme + "://" + currentUri.Authority, cookieString);
                form.Show();

            }
            catch
            {
                MessageBox.Show("Lỗi");
            }
            
        }
    }
}
