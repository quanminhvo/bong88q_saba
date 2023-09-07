namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_19
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
            this.dgvWd19 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd19)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd19);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(687, 423);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD19 - Giá tài trước banh .. giá xỉu sau banh bất kỳ";
            // 
            // dgvWd19
            // 
            this.dgvWd19.AllowUserToAddRows = false;
            this.dgvWd19.AllowUserToDeleteRows = false;
            this.dgvWd19.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd19.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd19.Location = new System.Drawing.Point(3, 16);
            this.dgvWd19.Name = "dgvWd19";
            this.dgvWd19.ReadOnly = true;
            this.dgvWd19.Size = new System.Drawing.Size(681, 404);
            this.dgvWd19.TabIndex = 2;
            this.dgvWd19.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd19_CellClick);
            this.dgvWd19.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd19_CellDoubleClick);
            // 
            // AlertForm_19
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 423);
            this.Controls.Add(this.groupBox2);
            this.Name = "AlertForm_19";
            this.Text = "WD19 - Giá tài trước banh .. giá xỉu sau banh bất kỳ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_19_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_19_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd19)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd19;
    }
}