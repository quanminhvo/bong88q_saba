namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_9
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
            this.dgvWd9 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd9)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd9);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 487);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD9 - 2 ô liền kề biến động >= 9";
            // 
            // dgvWd9
            // 
            this.dgvWd9.AllowUserToAddRows = false;
            this.dgvWd9.AllowUserToDeleteRows = false;
            this.dgvWd9.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd9.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd9.Location = new System.Drawing.Point(3, 16);
            this.dgvWd9.Name = "dgvWd9";
            this.dgvWd9.ReadOnly = true;
            this.dgvWd9.Size = new System.Drawing.Size(719, 468);
            this.dgvWd9.TabIndex = 2;
            this.dgvWd9.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd9_CellClick);
            this.dgvWd9.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd9_CellDoubleClick);
            // 
            // AlertForm_9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 487);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_9";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD9 - 2 ô liền kề biến động >= 9";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_9_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_9_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd9;
    }
}