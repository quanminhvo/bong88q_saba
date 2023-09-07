using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetCookieSample
{
    public partial class ConfigResultForm : Form
    {
        private string _getSidUrl;
        private string _bong88Url;
        private string _cookie;

        public ConfigResultForm(string getSidUrl, string bong88Url, string cookie)
        {
            _getSidUrl = getSidUrl;
            _bong88Url = bong88Url;
            _cookie = cookie;
            InitializeComponent();
        }

        private void ConfigResultForm_Load(object sender, EventArgs e)
        {
            this.txtGetSidUrl.Text = _getSidUrl;
            this.txtBong88Url.Text = _bong88Url;
            this.txtCookie.Text = _cookie;
            
        }
    }
}
