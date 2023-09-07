namespace LiveBetApp.Forms
{
    partial class ProductHistoryInspectorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.numFromMinute = new System.Windows.Forms.NumericUpDown();
            this.btnH2 = new System.Windows.Forms.Button();
            this.btnNgl = new System.Windows.Forms.Button();
            this.btnH1 = new System.Windows.Forms.Button();
            this.numToMinute = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFromMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToMinute)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvMain, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(819, 533);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(3, 48);
            this.dgvMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMain.RowHeadersWidth = 51;
            this.dgvMain.RowTemplate.Height = 20;
            this.dgvMain.Size = new System.Drawing.Size(813, 483);
            this.dgvMain.TabIndex = 13;
            this.dgvMain.Sorted += new System.EventHandler(this.dgvMain_Sorted);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 712F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.numFromMinute, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnH2, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnNgl, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnH1, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.numToMinute, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(811, 38);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Minute";
            // 
            // numFromMinute
            // 
            this.numFromMinute.Location = new System.Drawing.Point(71, 4);
            this.numFromMinute.Margin = new System.Windows.Forms.Padding(4);
            this.numFromMinute.Name = "numFromMinute";
            this.numFromMinute.Size = new System.Drawing.Size(59, 22);
            this.numFromMinute.TabIndex = 1;
            this.numFromMinute.Enter += new System.EventHandler(this.numFromMinute_Enter);
            // 
            // btnH2
            // 
            this.btnH2.Location = new System.Drawing.Point(420, 4);
            this.btnH2.Margin = new System.Windows.Forms.Padding(4);
            this.btnH2.Name = "btnH2";
            this.btnH2.Size = new System.Drawing.Size(93, 30);
            this.btnH2.TabIndex = 3;
            this.btnH2.Text = "H2";
            this.btnH2.UseVisualStyleBackColor = true;
            this.btnH2.Click += new System.EventHandler(this.btnH2_Click);
            // 
            // btnNgl
            // 
            this.btnNgl.Location = new System.Drawing.Point(317, 4);
            this.btnNgl.Margin = new System.Windows.Forms.Padding(4);
            this.btnNgl.Name = "btnNgl";
            this.btnNgl.Size = new System.Drawing.Size(93, 30);
            this.btnNgl.TabIndex = 3;
            this.btnNgl.Text = "NGL";
            this.btnNgl.UseVisualStyleBackColor = true;
            this.btnNgl.Click += new System.EventHandler(this.btnNgl_Click);
            // 
            // btnH1
            // 
            this.btnH1.Location = new System.Drawing.Point(216, 4);
            this.btnH1.Margin = new System.Windows.Forms.Padding(4);
            this.btnH1.Name = "btnH1";
            this.btnH1.Size = new System.Drawing.Size(92, 30);
            this.btnH1.TabIndex = 2;
            this.btnH1.Text = "H1";
            this.btnH1.UseVisualStyleBackColor = true;
            this.btnH1.Click += new System.EventHandler(this.btnH1_Click);
            // 
            // numToMinute
            // 
            this.numToMinute.Location = new System.Drawing.Point(144, 4);
            this.numToMinute.Margin = new System.Windows.Forms.Padding(4);
            this.numToMinute.Name = "numToMinute";
            this.numToMinute.Size = new System.Drawing.Size(59, 22);
            this.numToMinute.TabIndex = 1;
            this.numToMinute.Enter += new System.EventHandler(this.numToMinute_Enter);
            // 
            // ProductHistoryInspectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 533);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProductHistoryInspectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductHistoryInspectorForm";
            this.Load += new System.EventHandler(this.ProductHistoryInspectorForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFromMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToMinute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.NumericUpDown numFromMinute;
        private System.Windows.Forms.Button btnH1;
        private System.Windows.Forms.Button btnNgl;
        private System.Windows.Forms.Button btnH2;
        private System.Windows.Forms.NumericUpDown numToMinute;
    }
}