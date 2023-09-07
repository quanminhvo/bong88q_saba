namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_3
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
            this.dgvWd3 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1036, 713);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD3 - Đang H2 [total goal] <= [openmax]";
            // 
            // dgvWd3
            // 
            this.dgvWd3.AllowUserToAddRows = false;
            this.dgvWd3.AllowUserToDeleteRows = false;
            this.dgvWd3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd3.Location = new System.Drawing.Point(4, 19);
            this.dgvWd3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvWd3.Name = "dgvWd3";
            this.dgvWd3.ReadOnly = true;
            this.dgvWd3.RowHeadersWidth = 51;
            this.dgvWd3.Size = new System.Drawing.Size(1028, 690);
            this.dgvWd3.TabIndex = 2;
            this.dgvWd3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd3_CellClick);
            this.dgvWd3.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd3_CellDoubleClick);
            // 
            // AlertForm_3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 713);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AlertForm_3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD3 - Đang H2 [total goal] <= [openmax]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_3_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_3_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd3;
    }
}