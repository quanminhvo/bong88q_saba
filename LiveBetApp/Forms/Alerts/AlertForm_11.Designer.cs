namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_11
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
            this.dgvWd11 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd11)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd11);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 487);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD11 - Có thẻ đỏ, Pen";
            // 
            // dgvWd11
            // 
            this.dgvWd11.AllowUserToAddRows = false;
            this.dgvWd11.AllowUserToDeleteRows = false;
            this.dgvWd11.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd11.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd11.Location = new System.Drawing.Point(3, 16);
            this.dgvWd11.Name = "dgvWd11";
            this.dgvWd11.ReadOnly = true;
            this.dgvWd11.Size = new System.Drawing.Size(719, 468);
            this.dgvWd11.TabIndex = 2;
            this.dgvWd11.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd11_CellClick);
            this.dgvWd11.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd11_CellDoubleClick);
            // 
            // AlertForm_11
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 487);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_11";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD11 - Có thẻ đỏ, Pen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_11_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_11_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd11;
    }
}