namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_20
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
            this.dgvWd20 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd20)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd20);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(851, 487);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD20 - ID Alert";
            // 
            // dgvWd20
            // 
            this.dgvWd20.AllowUserToAddRows = false;
            this.dgvWd20.AllowUserToDeleteRows = false;
            this.dgvWd20.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd20.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd20.Location = new System.Drawing.Point(4, 19);
            this.dgvWd20.Margin = new System.Windows.Forms.Padding(4);
            this.dgvWd20.Name = "dgvWd20";
            this.dgvWd20.ReadOnly = true;
            this.dgvWd20.RowHeadersWidth = 51;
            this.dgvWd20.Size = new System.Drawing.Size(843, 464);
            this.dgvWd20.TabIndex = 2;
            this.dgvWd20.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd20_CellClick);
            this.dgvWd20.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd20_CellDoubleClick);
            // 
            // AlertForm_20
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 487);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AlertForm_20";
            this.Text = "AlertForm_20";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_20_FormClosing);
            this.Load += new System.EventHandler(this.AlertForm_20_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd20)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd20;
    }
}