namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_6
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
            this.dgvWd6 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd6)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 487);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD6 - Chỉ chạy 01 kèo";
            // 
            // dgvWd6
            // 
            this.dgvWd6.AllowUserToAddRows = false;
            this.dgvWd6.AllowUserToDeleteRows = false;
            this.dgvWd6.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd6.Location = new System.Drawing.Point(3, 16);
            this.dgvWd6.Name = "dgvWd6";
            this.dgvWd6.ReadOnly = true;
            this.dgvWd6.Size = new System.Drawing.Size(719, 468);
            this.dgvWd6.TabIndex = 2;
            this.dgvWd6.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd6_CellClick);
            this.dgvWd6.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd6_CellDoubleClick);
            // 
            // AlertForm_6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 487);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_6";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD6 - Chỉ chạy 01 kèo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_6_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_6_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd6;
    }
}