namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_14
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
            this.dgvWd14 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd14)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd14);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(708, 457);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD14 - Chạm mốc thời gian (7, 16, 25, 34)";
            // 
            // dgvWd14
            // 
            this.dgvWd14.AllowUserToAddRows = false;
            this.dgvWd14.AllowUserToDeleteRows = false;
            this.dgvWd14.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd14.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd14.Location = new System.Drawing.Point(3, 16);
            this.dgvWd14.Name = "dgvWd14";
            this.dgvWd14.ReadOnly = true;
            this.dgvWd14.Size = new System.Drawing.Size(702, 438);
            this.dgvWd14.TabIndex = 2;
            this.dgvWd14.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd14_CellClick);
            this.dgvWd14.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd14_CellDoubleClick);
            // 
            // AlertForm_14
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 457);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_14";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD14 - Chạm mốc thời gian (7, 16, 25, 34)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_14_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_14_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd14)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd14;
    }
}