namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_8
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
            this.dgvWd8 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd8)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd8);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 487);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD8 - Open Xỉu âm, tiếp theo dương";
            // 
            // dgvWd8
            // 
            this.dgvWd8.AllowUserToAddRows = false;
            this.dgvWd8.AllowUserToDeleteRows = false;
            this.dgvWd8.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd8.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd8.Location = new System.Drawing.Point(3, 16);
            this.dgvWd8.Name = "dgvWd8";
            this.dgvWd8.ReadOnly = true;
            this.dgvWd8.Size = new System.Drawing.Size(719, 468);
            this.dgvWd8.TabIndex = 2;
            this.dgvWd8.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd8_CellClick);
            this.dgvWd8.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd8_CellDoubleClick);
            // 
            // AlertForm_8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 487);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD8 - Open Xỉu âm, tiếp theo dương";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_8_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_8_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd8;
    }
}