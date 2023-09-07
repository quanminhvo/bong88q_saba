namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_15
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvWd15 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd15)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd15);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 487);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD15 - Tăng, giãm kèo trước trận";
            // 
            // dgvWd15
            // 
            this.dgvWd15.AllowUserToAddRows = false;
            this.dgvWd15.AllowUserToDeleteRows = false;
            this.dgvWd15.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd15.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd15.Location = new System.Drawing.Point(3, 16);
            this.dgvWd15.Name = "dgvWd15";
            this.dgvWd15.ReadOnly = true;
            this.dgvWd15.Size = new System.Drawing.Size(719, 468);
            this.dgvWd15.TabIndex = 2;
            this.dgvWd15.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd15_CellClick);
            this.dgvWd15.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd15_CellDoubleClick);
            // 
            // AlertForm_15
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 487);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_15";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD15 - Tăng, giãm kèo trước trận";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_15_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_15_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd15)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd15;
    }
}