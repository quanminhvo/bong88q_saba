namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_13
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
            this.dgvWd13 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd13)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd13);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(967, 599);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD13 - Biến động Tx từ 03 giá trị trở lên";
            // 
            // dgvWd13
            // 
            this.dgvWd13.AllowUserToAddRows = false;
            this.dgvWd13.AllowUserToDeleteRows = false;
            this.dgvWd13.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd13.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd13.Location = new System.Drawing.Point(4, 19);
            this.dgvWd13.Margin = new System.Windows.Forms.Padding(4);
            this.dgvWd13.Name = "dgvWd13";
            this.dgvWd13.ReadOnly = true;
            this.dgvWd13.Size = new System.Drawing.Size(959, 576);
            this.dgvWd13.TabIndex = 2;
            this.dgvWd13.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd13_CellClick);
            this.dgvWd13.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd13_CellDoubleClick);
            // 
            // AlertForm_13
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 599);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AlertForm_13";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD13 - Biến động Tx từ 03 giá trị trở lên";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_13_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_13_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd13)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd13;
    }
}