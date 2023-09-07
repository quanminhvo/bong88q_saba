namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_4
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
            this.dgvWd4 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd4)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvWd4
            // 
            this.dgvWd4.AllowUserToAddRows = false;
            this.dgvWd4.AllowUserToDeleteRows = false;
            this.dgvWd4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd4.Location = new System.Drawing.Point(3, 16);
            this.dgvWd4.Name = "dgvWd4";
            this.dgvWd4.ReadOnly = true;
            this.dgvWd4.Size = new System.Drawing.Size(635, 462);
            this.dgvWd4.TabIndex = 2;
            this.dgvWd4.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd4_CellClick);
            this.dgvWd4.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd4_CellDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(641, 481);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD4 - H2, Over/Under vừa mở kèo 100, tổng banh <= open Max";
            // 
            // AlertForm_4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 481);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD4 - H2, Over/Under vừa mở kèo 100, tổng banh <= open Max";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_4_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd4)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWd4;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}