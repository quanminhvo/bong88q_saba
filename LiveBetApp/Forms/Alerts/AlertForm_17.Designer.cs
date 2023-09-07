namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_17
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
            this.dgvWd17 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd17)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd17);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 487);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD17 - Tăng, giảm kèo NGL";
            // 
            // dgvWd17
            // 
            this.dgvWd17.AllowUserToAddRows = false;
            this.dgvWd17.AllowUserToDeleteRows = false;
            this.dgvWd17.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd17.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd17.Location = new System.Drawing.Point(3, 16);
            this.dgvWd17.Name = "dgvWd17";
            this.dgvWd17.ReadOnly = true;
            this.dgvWd17.Size = new System.Drawing.Size(719, 468);
            this.dgvWd17.TabIndex = 2;
            this.dgvWd17.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd17_CellClick);
            this.dgvWd17.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd17_CellDoubleClick);
            // 
            // AlertForm_17
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 487);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_17";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD17 - Tăng, giảm kèo NGL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_17_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_17_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd17)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd17;
    }
}