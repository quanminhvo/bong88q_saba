namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_12
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
            this.dgvWd12 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd12)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd12);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(762, 474);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD12 - Biến động TX";
            // 
            // dgvWd12
            // 
            this.dgvWd12.AllowUserToAddRows = false;
            this.dgvWd12.AllowUserToDeleteRows = false;
            this.dgvWd12.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd12.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd12.Location = new System.Drawing.Point(3, 16);
            this.dgvWd12.Name = "dgvWd12";
            this.dgvWd12.ReadOnly = true;
            this.dgvWd12.Size = new System.Drawing.Size(756, 455);
            this.dgvWd12.TabIndex = 2;
            this.dgvWd12.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd12_CellClick);
            this.dgvWd12.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd12_CellDoubleClick);
            // 
            // AlertForm_12
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 474);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_12";
            this.Text = "WD12 - Biến động TX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_12_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_12_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd12)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd12;
    }
}