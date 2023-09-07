namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_16
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
            this.dgvWd16 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd16)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd16);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 487);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD16 - H2, Over/Under vừa mở kèo 01 trái (được 03 phút)";
            // 
            // dgvWd16
            // 
            this.dgvWd16.AllowUserToAddRows = false;
            this.dgvWd16.AllowUserToDeleteRows = false;
            this.dgvWd16.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd16.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd16.Location = new System.Drawing.Point(3, 16);
            this.dgvWd16.Name = "dgvWd16";
            this.dgvWd16.ReadOnly = true;
            this.dgvWd16.Size = new System.Drawing.Size(719, 468);
            this.dgvWd16.TabIndex = 2;
            this.dgvWd16.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd16_CellClick);
            this.dgvWd16.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd16_CellDoubleClick);
            // 
            // AlertForm_16
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 487);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_16";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD16 - H2, Over/Under vừa mở kèo 01 trái (được 03 phút)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_16_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_16_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd16)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd16;
    }
}