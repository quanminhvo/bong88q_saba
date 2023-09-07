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
    public partial class SoundForm : Form
    {
        public SoundForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataStore.KeepPlaySoundAl32 = this.cbAlert32.Checked;
            DataStore.KeepPlaySoundAl31 = this.cbAlert31.Checked;
            DataStore.KeepPlaySoundAl29 = this.cbAlert29.Checked;
            DataStore.KeepPlaySoundAl30 = this.cbAlert30.Checked;
            DataStore.KeepPlaySoundAl27 = this.cbAlert27.Checked;
            DataStore.KeepPlaySoundAl26 = this.cbAlert26.Checked;
            DataStore.KeepPlaySoundAl25 = this.cbAlert25.Checked;
            DataStore.KeepPlaySoundAl24 = this.cbAlert24.Checked;
            DataStore.KeepPlaySoundAl22 = this.cbAlert22.Checked;
            DataStore.KeepPlaySoundAl34 = this.cbAlert34.Checked;
            DataStore.KeepPlaySoundAl36_37 = this.cbAlert36_37.Checked;
            DataStore.KeepPlaySoundAl38 = this.cbAlert38.Checked;
            DataStore.KeepPlaySoundAl39 = this.cbAlert39.Checked;
            DataStore.KeepPlaySoundAl40_41 = this.cbAlert40_41.Checked;
            DataStore.KeepPlaySoundAlUpdatePhOuLive = this.cbUpdatePhOuLive.Checked;
            DataStore.KeepPlaySoundHasStatusClosePrice = this.cbHasClosePriceStatus.Checked;
            this.Close();
        }

        private void SoundForm_Load(object sender, EventArgs e)
        {
            this.cbAlert32.Checked = DataStore.KeepPlaySoundAl32;
            this.cbAlert31.Checked = DataStore.KeepPlaySoundAl31;
            this.cbAlert29.Checked = DataStore.KeepPlaySoundAl29;
            this.cbAlert30.Checked = DataStore.KeepPlaySoundAl30;
            this.cbAlert27.Checked = DataStore.KeepPlaySoundAl27;
            this.cbAlert26.Checked = DataStore.KeepPlaySoundAl26;
            this.cbAlert25.Checked = DataStore.KeepPlaySoundAl25;
            this.cbAlert24.Checked = DataStore.KeepPlaySoundAl24;
            this.cbAlert22.Checked = DataStore.KeepPlaySoundAl22;
            this.cbAlert34.Checked = DataStore.KeepPlaySoundAl34;
            this.cbAlert36_37.Checked = DataStore.KeepPlaySoundAl36_37;
            this.cbAlert38.Checked = DataStore.KeepPlaySoundAl38;
            this.cbAlert39.Checked = DataStore.KeepPlaySoundAl39;
            this.cbAlert40_41.Checked = DataStore.KeepPlaySoundAl40_41;

            this.cbUpdatePhOuLive.Checked = DataStore.KeepPlaySoundAlUpdatePhOuLive;
            this.cbHasClosePriceStatus.Checked = DataStore.KeepPlaySoundHasStatusClosePrice;
        }

        private void btnTestHasClosePriceStatus_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alarm-fast-high-pitch-a3.wav");
        }

        private void btnTestUpdatePhOuLive_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\oven-tick.wav");
        }

        private void btnTestAlert22_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alarm-fast-a1.wav");
        }

        private void btnTestAlert24_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alert.wav");
        }

        private void btnTestAlert25_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\whistle.wav");
        }

        private void btnTestAlert38_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\mixkit-arabian-mystery-harp-notification.wav");
        }

        private void btnTestAlert34_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\bome.wav");
        }

        private void btnTestAlert27_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alarm-fast-high-pitch.wav");
        }

        private void btnTestAlert30_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\metallic-238.wav");
        }

        private void btnTestAlert29_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\oringz-w429-349.wav");
        }

        private void btnTestAlert32_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\mixkit-clear-announce.wav");
        }

        private void btnTestAlert31_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\bip-bip.wav");
        }

        private void btnTestAlert40_41_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\alarm-1-with-reverberation.wav");
        }

        private void btnTestAlert40_41_Away_Click(object sender, EventArgs e)
        {
            Common.Functions.PlaySound(Application.StartupPath + "\\sounds\\emergency-alarm-with-reverb.wav");
        }
    }
}
