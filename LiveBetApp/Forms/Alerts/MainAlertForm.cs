using LiveBetApp.Models.DataModels;
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

namespace LiveBetApp.Forms.Alerts
{
    public partial class MainAlertForm : Form
    {
        private Thread _executeThread;

        public MainAlertForm()
        {
            InitializeComponent();
        }

        private void MainAlertForm_Load(object sender, EventArgs e)
        {
            LoadCheckBoxRunning();
            this.btnWd1.Enabled = true;
            this.btnWd2.Enabled = true;
            this.btnWd3.Enabled = true;
            this.btnWd4.Enabled = true;
            this.btnWd5.Enabled = true;
            this.btnWd6.Enabled = true;
            this.btnWd7.Enabled = true;
            this.btnWd8.Enabled = true;
            this.btnWd9.Enabled = true;
            this.btnWd10.Enabled = true;
            this.btnWd11.Enabled = true;
            this.btnWd12.Enabled = true;
            this.btnWd13.Enabled = true;
            this.btnWd14.Enabled = true;
            this.btnWd15.Enabled = true;
            this.btnWd16.Enabled = true;
            this.btnWd17.Enabled = true;
            this.btnWd18.Enabled = true;
            this.btnWd19.Enabled = true;
            this.btnWd20.Enabled = true;

            _executeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (DataStore.Alert_Wd1.Values.Any(item => item.IsNew))
                        {
                            this.btnWd1.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd1.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd2.Values.Any(item => item.IsNew))
                        {
                            this.btnWd2.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd2.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd3.Values.Any(item => item.IsNew))
                        {
                            this.btnWd3.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd3.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd4.Values.Any(item => item.IsNew))
                        {
                            this.btnWd4.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd4.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd5.Values.Any(item => item.IsNew))
                        {
                            this.btnWd5.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd5.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd6.Values.Any(item => item.IsNew))
                        {
                            this.btnWd6.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd6.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd7.Values.Any(item => item.IsNew))
                        {
                            this.btnWd7.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd7.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd8.Values.Any(item => item.IsNew))
                        {
                            this.btnWd8.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd8.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd9.Values.Any(item => item.IsNew))
                        {
                            this.btnWd9.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd9.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd10.Values.Any(item => item.IsNew))
                        {
                            this.btnWd10.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd10.BackColor = SystemColors.Control;
                        }


                        if (DataStore.Alert_Wd11.Values.Any(item => item.IsNew))
                        {
                            this.btnWd11.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd11.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd12.Values.Any(item => item.IsNew))
                        {
                            this.btnWd12.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd12.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd13.Values.Any(item => item.IsNew))
                        {
                            this.btnWd13.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd13.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd14.Values.Any(item => item.IsNew))
                        {
                            this.btnWd14.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd14.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd15.Values.Any(item => item.IsNew))
                        {
                            this.btnWd15.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd15.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd16.Values.Any(item => item.IsNew))
                        {
                            this.btnWd16.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd16.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd17.Values.Any(item => item.IsNew))
                        {
                            this.btnWd17.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd17.BackColor = SystemColors.Control;
                        }


                        if (DataStore.Alert_Wd18.Values.Any(item => item.IsNew))
                        {
                            this.btnWd18.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd18.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd19.Values.Any(item => item.IsNew))
                        {
                            this.btnWd19.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd19.BackColor = SystemColors.Control;
                        }

                        if (DataStore.Alert_Wd20.Values.Any(item => item.IsNew))
                        {
                            this.btnWd20.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.btnWd20.BackColor = SystemColors.Control;
                        }

                        UpdateTotalCount();
                        
                    }
                    catch
                    {

                    }
                    Thread.Sleep(30000);
                }
            });

            _executeThread.IsBackground = true;
            _executeThread.Start();
        }

        private bool HasNewItem(Dictionary<long, Alert> data)
        {
            return data.Values.Any(item => item.IsNew);
        }

        private void HighlightButton(Button btn)
        {
            int flashCount = 10 * 2;
            Thread executeThread;

            executeThread = new Thread(() =>
            {
                int count = 0;
                while (count < flashCount)
                {
                    btn.BackColor = count % 2 == 0 ? Color.Yellow : SystemColors.Control;
                    Thread.Sleep(300);
                    count++;
                }
                Thread.CurrentThread.Abort();
            });

            executeThread.IsBackground = true;
            executeThread.Start();
        }

        private delegate void dlgUpdateTotalCount();
        private void UpdateTotalCount()
        {
            if (this.lbWd1.InvokeRequired)
            {
                this.Invoke(new dlgUpdateTotalCount(UpdateTotalCount));
                return;
            }
            lbWd1.Text = DataStore.Alert_Wd1.Count(item => !item.Value.Deleted).ToString();
            lbWd2.Text = DataStore.Alert_Wd2.Count(item => !item.Value.Deleted).ToString();
            lbWd3.Text = DataStore.Alert_Wd3.Count(item => !item.Value.Deleted).ToString();
            lbWd4.Text = DataStore.Alert_Wd4.Count(item => !item.Value.Deleted).ToString();
            lbWd5.Text = DataStore.Alert_Wd5.Count(item => !item.Value.Deleted).ToString();
            lbWd6.Text = DataStore.Alert_Wd6.Count(item => !item.Value.Deleted).ToString();
            lbWd7.Text = DataStore.Alert_Wd7.Count(item => !item.Value.Deleted).ToString();
            lbWd8.Text = DataStore.Alert_Wd8.Count(item => !item.Value.Deleted).ToString();
            lbWd9.Text = DataStore.Alert_Wd9.Count(item => !item.Value.Deleted).ToString();
            lbWd10.Text = DataStore.Alert_Wd10.Count(item => !item.Value.Deleted).ToString();
            lbWd11.Text = DataStore.Alert_Wd11.Count(item => !item.Value.Deleted).ToString();
            lbWd12.Text = DataStore.Alert_Wd12.Count(item => !item.Value.Deleted).ToString();
            lbWd13.Text = DataStore.Alert_Wd13.Count(item => !item.Value.Deleted).ToString();
            lbWd14.Text = DataStore.Alert_Wd14.Count(item => !item.Value.Deleted).ToString();
            lbWd15.Text = DataStore.Alert_Wd15.Count(item => !item.Value.Deleted).ToString();
            lbWd16.Text = DataStore.Alert_Wd16.Count(item => !item.Value.Deleted).ToString();
            lbWd17.Text = DataStore.Alert_Wd17.Count(item => !item.Value.Deleted).ToString();
            lbWd18.Text = DataStore.Alert_Wd18.Count(item => !item.Value.Deleted).ToString();
            lbWd19.Text = DataStore.Alert_Wd19.Count(item => !item.Value.Deleted).ToString();
            lbWd20.Text = DataStore.Alert_Wd20.Count(item => !item.Value.Deleted).ToString();
            //lbWd19.Text = DataStore.Alert_Wd19.Count.ToString();
            //lbWd20.Text = DataStore.Alert_Wd20.Count.ToString();
        }

        private void LoadCheckBoxRunning()
        {
            cbWd1.Checked = DataStore.AlertSetting[1];
            cbWd2.Checked = DataStore.AlertSetting[2];
            cbWd3.Checked = DataStore.AlertSetting[3];
            cbWd4.Checked = DataStore.AlertSetting[4];
            cbWd5.Checked = DataStore.AlertSetting[5];
            cbWd6.Checked = DataStore.AlertSetting[6];
            cbWd7.Checked = DataStore.AlertSetting[7];
            cbWd8.Checked = DataStore.AlertSetting[8];
            cbWd9.Checked = DataStore.AlertSetting[9];
            cbWd10.Checked = DataStore.AlertSetting[10];
            cbWd11.Checked = DataStore.AlertSetting[11];
            cbWd12.Checked = DataStore.AlertSetting[12];
            cbWd13.Checked = DataStore.AlertSetting[13];
            cbWd14.Checked = DataStore.AlertSetting[14];
            cbWd15.Checked = DataStore.AlertSetting[15];
            cbWd16.Checked = DataStore.AlertSetting[16];
            cbWd17.Checked = DataStore.AlertSetting[17];
            cbWd18.Checked = DataStore.AlertSetting[18];
            cbWd19.Checked = DataStore.AlertSetting[19];
            cbWd20.Checked = DataStore.AlertSetting[20];
        }

        private void btnWd1_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_1_2 form = new AlertForm_1_2();
            form.Show();
        }

        private void btnWd2_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_1_2 form = new AlertForm_1_2();
            form.Show();
        }

        private void btnWd3_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_3 form = new AlertForm_3();
            form.Show();
        }

        private void btnWd4_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_4 form = new AlertForm_4();
            form.Show();
        }

        private void btnWd5_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_5 form = new AlertForm_5();
            form.Show();
        }

        private void btnWd6_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_6 form = new AlertForm_6();
            form.Show();
        }

        private void btnWd7_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_7 form = new AlertForm_7();
            form.Show();
        }

        private void btnWd8_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_8 form = new AlertForm_8();
            form.Show();
        }

        private void btnWd9_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_9 form = new AlertForm_9();
            form.Show();
        }

        private void btnWd10_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_10 form = new AlertForm_10();
            form.Show();
        }

        private void btnWd11_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_11 form = new AlertForm_11();
            form.Show();
        }

        private void btnWd12_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_12 form = new AlertForm_12();
            form.Show();
        }
        private void btnWd13_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_13 form = new AlertForm_13();
            form.Show();
        }

        private void btnWd14_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_14 form = new AlertForm_14();
            form.Show();
        }


        private void MainAlertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_executeThread != null && _executeThread.IsAlive) _executeThread.Abort();
        }

        private void btnWd15_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_15 form = new AlertForm_15();
            form.Show();
        }

        private void btnWd16_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_16 form = new AlertForm_16();
            form.Show();
        }

        private void btnWd17_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_17 form = new AlertForm_17();
            form.Show();
        }

        private void cbWd1_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[1] = cbWd1.Checked;
        }

        private void cbWd2_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[2] = cbWd2.Checked;
        }

        private void cbWd3_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[3] = cbWd3.Checked;
        }

        private void cbWd4_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[4] = cbWd4.Checked;
        }

        private void cbWd5_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[5] = cbWd5.Checked;
        }

        private void cbWd6_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[6] = cbWd6.Checked;
        }

        private void cbWd7_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[7] = cbWd7.Checked;
        }

        private void cbWd8_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[8] = cbWd8.Checked;
        }

        private void cbWd9_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[9] = cbWd9.Checked;
        }

        private void cbWd10_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[10] = cbWd10.Checked;
        }

        private void cbWd11_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[11] = cbWd11.Checked;
        }

        private void cbWd12_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[12] = cbWd12.Checked;
        }

        private void cbWd13_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[13] = cbWd13.Checked;
        }

        private void cbWd14_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[14] = cbWd14.Checked;
        }

        private void cbWd15_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[15] = cbWd15.Checked;
        }

        private void cbWd16_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[16] = cbWd16.Checked;
        }

        private void cbWd17_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[17] = cbWd17.Checked;
        }

        private void cbWd18_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[18] = cbWd18.Checked;
        }

        private void cbWd19_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[19] = cbWd19.Checked;
        }

        private void cbWd20_CheckedChanged(object sender, EventArgs e)
        {
            DataStore.AlertSetting[20] = cbWd20.Checked;
        }

        private void btnWd18_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_18 form = new AlertForm_18();
            form.Show();
        }

        private void btnWd19_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_19 form = new AlertForm_19();
            form.Show();
        }

        private void btnWd20_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.Control;
            AlertForm_20 form = new AlertForm_20();
            form.Show();
        }

    }
}
