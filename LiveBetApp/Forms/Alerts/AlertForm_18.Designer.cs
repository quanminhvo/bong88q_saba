namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_18
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
            this.dgvWd18 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd18)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd18);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 506);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD18 - Thêm ID trước trận lệch từ 13 phút";
            // 
            // dgvWd18
            // 
            this.dgvWd18.AllowUserToAddRows = false;
            this.dgvWd18.AllowUserToDeleteRows = false;
            this.dgvWd18.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd18.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd18.Location = new System.Drawing.Point(3, 16);
            this.dgvWd18.Name = "dgvWd18";
            this.dgvWd18.ReadOnly = true;
            this.dgvWd18.Size = new System.Drawing.Size(734, 487);
            this.dgvWd18.TabIndex = 2;
            this.dgvWd18.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd18_CellClick);
            this.dgvWd18.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd18_CellDoubleClick);
            // 
            // AlertForm_18
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 506);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_18";
            this.Text = "WD18 - Thêm ID trước trận lệch từ 13 phút";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_18_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_18_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd18)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd18;
    }
}