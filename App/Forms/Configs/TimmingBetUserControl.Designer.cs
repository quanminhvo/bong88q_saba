namespace App.Forms.Configs
{
    partial class TimmingBetUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cbbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericBetAmount = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbbSet = new System.Windows.Forms.ComboBox();
            this.cbbMinute = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericBetAmount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(44, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type:";
            // 
            // cbbType
            // 
            this.cbbType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbType.FormattingEnabled = true;
            this.cbbType.Location = new System.Drawing.Point(0, 23);
            this.cbbType.Name = "cbbType";
            this.cbbType.Size = new System.Drawing.Size(217, 21);
            this.cbbType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 44);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(71, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Bet amount";
            // 
            // numericBetAmount
            // 
            this.numericBetAmount.Dock = System.Windows.Forms.DockStyle.Top;
            this.numericBetAmount.Location = new System.Drawing.Point(0, 67);
            this.numericBetAmount.Name = "numericBetAmount";
            this.numericBetAmount.Size = new System.Drawing.Size(217, 20);
            this.numericBetAmount.TabIndex = 3;
            this.numericBetAmount.ThousandsSeparator = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 47);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time to Bet";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cbbSet, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbMinute, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(211, 28);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cbbSet
            // 
            this.cbbSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSet.FormattingEnabled = true;
            this.cbbSet.Location = new System.Drawing.Point(3, 3);
            this.cbbSet.Name = "cbbSet";
            this.cbbSet.Size = new System.Drawing.Size(99, 21);
            this.cbbSet.TabIndex = 0;
            // 
            // cbbMinute
            // 
            this.cbbMinute.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMinute.FormattingEnabled = true;
            this.cbbMinute.Location = new System.Drawing.Point(108, 3);
            this.cbbMinute.Name = "cbbMinute";
            this.cbbMinute.Size = new System.Drawing.Size(100, 21);
            this.cbbMinute.TabIndex = 1;
            // 
            // TimmingBetUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numericBetAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbType);
            this.Controls.Add(this.label1);
            this.Name = "TimmingBetUserControl";
            this.Size = new System.Drawing.Size(217, 142);
            ((System.ComponentModel.ISupportInitialize)(this.numericBetAmount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericBetAmount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cbbSet;
        private System.Windows.Forms.ComboBox cbbMinute;
    }
}
