namespace LiveBetApp.Forms.Alerts
{
    partial class AlertForm_1_2
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvWd1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvWd2 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd2)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(963, 420);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvWd1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 414);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "WD1 - Giải lao 0-0";
            // 
            // dgvWd1
            // 
            this.dgvWd1.AllowUserToAddRows = false;
            this.dgvWd1.AllowUserToDeleteRows = false;
            this.dgvWd1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd1.Location = new System.Drawing.Point(3, 16);
            this.dgvWd1.Name = "dgvWd1";
            this.dgvWd1.ReadOnly = true;
            this.dgvWd1.Size = new System.Drawing.Size(469, 395);
            this.dgvWd1.TabIndex = 2;
            this.dgvWd1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgbWd1_CellClick);
            this.dgvWd1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgbWd1_CellDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWd2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(484, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(476, 414);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WD2- H2 0-0";
            // 
            // dgvWd2
            // 
            this.dgvWd2.AllowUserToAddRows = false;
            this.dgvWd2.AllowUserToDeleteRows = false;
            this.dgvWd2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWd2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWd2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWd2.Location = new System.Drawing.Point(3, 16);
            this.dgvWd2.Name = "dgvWd2";
            this.dgvWd2.ReadOnly = true;
            this.dgvWd2.Size = new System.Drawing.Size(470, 395);
            this.dgvWd2.TabIndex = 2;
            this.dgvWd2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd2_CellClick);
            this.dgvWd2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWd2_CellDoubleClick);
            // 
            // AlertForm_1_2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 420);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AlertForm_1_2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD1 - WD2 - Tỉ số 0-0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertFormA_FormClosing);
            this.Load += new System.EventHandler(this.AlertFormA_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWd2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvWd1;
        private System.Windows.Forms.DataGridView dgvWd2;
    }
}