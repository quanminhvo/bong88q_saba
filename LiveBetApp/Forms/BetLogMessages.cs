using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveBetApp.Forms
{
    public partial class BetLogMessages : Form
    {
        private List<string> _data;
        public BetLogMessages(List<string> data)
        {
            _data = data;
            InitializeComponent();
        }

        private void BetLogMessages_Load(object sender, EventArgs e)
        {
            if (_data != null && _data.Count > 0)
            {
                dataGridView.DataSource = _data.Select(x => new { Message = x }).ToList();
            }
            
        }
    }
}
