namespace LiveBetApp.Forms.BetFirstHalfByFulltime
{
    partial class AddBetFirstHalfByFulltimeForm
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
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnStake3 = new System.Windows.Forms.Button();
            this.btnStake100 = new System.Windows.Forms.Button();
            this.btnStake500 = new System.Windows.Forms.Button();
            this.btnStake1000 = new System.Windows.Forms.Button();
            this.rdCareGoal_4 = new System.Windows.Forms.RadioButton();
            this.rdCareGoal_3 = new System.Windows.Forms.RadioButton();
            this.rdCareGoal_2 = new System.Windows.Forms.RadioButton();
            this.rdCareGoal_1 = new System.Windows.Forms.RadioButton();
            this.cbCareGoal = new System.Windows.Forms.CheckBox();
            this.cbbHdp = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numFulltimeHdp = new System.Windows.Forms.NumericUpDown();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbOU = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.numStake = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFulltimeHdp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStake)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvMain, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1133, 574);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteAll);
            this.groupBox1.Controls.Add(this.btnStake3);
            this.groupBox1.Controls.Add(this.btnStake100);
            this.groupBox1.Controls.Add(this.btnStake500);
            this.groupBox1.Controls.Add(this.btnStake1000);
            this.groupBox1.Controls.Add(this.rdCareGoal_4);
            this.groupBox1.Controls.Add(this.rdCareGoal_3);
            this.groupBox1.Controls.Add(this.rdCareGoal_2);
            this.groupBox1.Controls.Add(this.rdCareGoal_1);
            this.groupBox1.Controls.Add(this.cbCareGoal);
            this.groupBox1.Controls.Add(this.cbbHdp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numFulltimeHdp);
            this.groupBox1.Controls.Add(this.numPrice);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbbOU);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.numStake);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(259, 566);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Form";
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(2, 529);
            this.btnDeleteAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(251, 28);
            this.btnDeleteAll.TabIndex = 35;
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnStake3
            // 
            this.btnStake3.Location = new System.Drawing.Point(4, 230);
            this.btnStake3.Name = "btnStake3";
            this.btnStake3.Size = new System.Drawing.Size(54, 23);
            this.btnStake3.TabIndex = 34;
            this.btnStake3.Text = "100";
            this.btnStake3.UseVisualStyleBackColor = true;
            this.btnStake3.Click += new System.EventHandler(this.btnStake3_Click);
            // 
            // btnStake100
            // 
            this.btnStake100.Location = new System.Drawing.Point(64, 230);
            this.btnStake100.Name = "btnStake100";
            this.btnStake100.Size = new System.Drawing.Size(54, 23);
            this.btnStake100.TabIndex = 33;
            this.btnStake100.Text = "300";
            this.btnStake100.UseVisualStyleBackColor = true;
            this.btnStake100.Click += new System.EventHandler(this.btnStake100_Click);
            // 
            // btnStake500
            // 
            this.btnStake500.Location = new System.Drawing.Point(124, 230);
            this.btnStake500.Name = "btnStake500";
            this.btnStake500.Size = new System.Drawing.Size(54, 23);
            this.btnStake500.TabIndex = 32;
            this.btnStake500.Text = "500";
            this.btnStake500.UseVisualStyleBackColor = true;
            this.btnStake500.Click += new System.EventHandler(this.btnStake500_Click);
            // 
            // btnStake1000
            // 
            this.btnStake1000.Location = new System.Drawing.Point(184, 230);
            this.btnStake1000.Name = "btnStake1000";
            this.btnStake1000.Size = new System.Drawing.Size(54, 23);
            this.btnStake1000.TabIndex = 31;
            this.btnStake1000.Text = "1000";
            this.btnStake1000.UseVisualStyleBackColor = true;
            this.btnStake1000.Click += new System.EventHandler(this.btnStake1000_Click);
            // 
            // rdCareGoal_4
            // 
            this.rdCareGoal_4.AutoSize = true;
            this.rdCareGoal_4.Location = new System.Drawing.Point(203, 340);
            this.rdCareGoal_4.Margin = new System.Windows.Forms.Padding(4);
            this.rdCareGoal_4.Name = "rdCareGoal_4";
            this.rdCareGoal_4.Size = new System.Drawing.Size(35, 20);
            this.rdCareGoal_4.TabIndex = 27;
            this.rdCareGoal_4.TabStop = true;
            this.rdCareGoal_4.Text = "4";
            this.rdCareGoal_4.UseVisualStyleBackColor = true;
            // 
            // rdCareGoal_3
            // 
            this.rdCareGoal_3.AutoSize = true;
            this.rdCareGoal_3.Location = new System.Drawing.Point(137, 340);
            this.rdCareGoal_3.Margin = new System.Windows.Forms.Padding(4);
            this.rdCareGoal_3.Name = "rdCareGoal_3";
            this.rdCareGoal_3.Size = new System.Drawing.Size(35, 20);
            this.rdCareGoal_3.TabIndex = 28;
            this.rdCareGoal_3.TabStop = true;
            this.rdCareGoal_3.Text = "3";
            this.rdCareGoal_3.UseVisualStyleBackColor = true;
            // 
            // rdCareGoal_2
            // 
            this.rdCareGoal_2.AutoSize = true;
            this.rdCareGoal_2.Location = new System.Drawing.Point(68, 340);
            this.rdCareGoal_2.Margin = new System.Windows.Forms.Padding(4);
            this.rdCareGoal_2.Name = "rdCareGoal_2";
            this.rdCareGoal_2.Size = new System.Drawing.Size(35, 20);
            this.rdCareGoal_2.TabIndex = 29;
            this.rdCareGoal_2.TabStop = true;
            this.rdCareGoal_2.Text = "2";
            this.rdCareGoal_2.UseVisualStyleBackColor = true;
            // 
            // rdCareGoal_1
            // 
            this.rdCareGoal_1.AutoSize = true;
            this.rdCareGoal_1.Location = new System.Drawing.Point(5, 340);
            this.rdCareGoal_1.Margin = new System.Windows.Forms.Padding(4);
            this.rdCareGoal_1.Name = "rdCareGoal_1";
            this.rdCareGoal_1.Size = new System.Drawing.Size(35, 20);
            this.rdCareGoal_1.TabIndex = 30;
            this.rdCareGoal_1.TabStop = true;
            this.rdCareGoal_1.Text = "1";
            this.rdCareGoal_1.UseVisualStyleBackColor = true;
            // 
            // cbCareGoal
            // 
            this.cbCareGoal.AutoSize = true;
            this.cbCareGoal.Checked = true;
            this.cbCareGoal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCareGoal.Location = new System.Drawing.Point(12, 311);
            this.cbCareGoal.Margin = new System.Windows.Forms.Padding(4);
            this.cbCareGoal.Name = "cbCareGoal";
            this.cbCareGoal.Size = new System.Drawing.Size(90, 20);
            this.cbCareGoal.TabIndex = 26;
            this.cbCareGoal.Text = "Care Goal";
            this.cbCareGoal.UseVisualStyleBackColor = true;
            this.cbCareGoal.CheckedChanged += new System.EventHandler(this.cbCareGoal_CheckedChanged);
            // 
            // cbbHdp
            // 
            this.cbbHdp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbHdp.FormattingEnabled = true;
            this.cbbHdp.Location = new System.Drawing.Point(4, 278);
            this.cbbHdp.Margin = new System.Windows.Forms.Padding(4);
            this.cbbHdp.Name = "cbbHdp";
            this.cbbHdp.Size = new System.Drawing.Size(249, 24);
            this.cbbHdp.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 256);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Size = new System.Drawing.Size(33, 22);
            this.label1.TabIndex = 24;
            this.label1.Text = "Hdp";
            // 
            // numFulltimeHdp
            // 
            this.numFulltimeHdp.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numFulltimeHdp.Location = new System.Drawing.Point(4, 46);
            this.numFulltimeHdp.Margin = new System.Windows.Forms.Padding(4);
            this.numFulltimeHdp.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFulltimeHdp.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numFulltimeHdp.Name = "numFulltimeHdp";
            this.numFulltimeHdp.Size = new System.Drawing.Size(251, 22);
            this.numFulltimeHdp.TabIndex = 23;
            this.numFulltimeHdp.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numPrice
            // 
            this.numPrice.Location = new System.Drawing.Point(4, 97);
            this.numPrice.Margin = new System.Windows.Forms.Padding(4);
            this.numPrice.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(251, 22);
            this.numPrice.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 71);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label6.Size = new System.Drawing.Size(41, 22);
            this.label6.TabIndex = 21;
            this.label6.Text = "Price:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 175);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(42, 22);
            this.label4.TabIndex = 18;
            this.label4.Text = "Stake";
            // 
            // cbbOU
            // 
            this.cbbOU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOU.FormattingEnabled = true;
            this.cbbOU.Location = new System.Drawing.Point(4, 149);
            this.cbbOU.Margin = new System.Windows.Forms.Padding(4);
            this.cbbOU.Name = "cbbOU";
            this.cbbOU.Size = new System.Drawing.Size(249, 24);
            this.cbbOU.TabIndex = 17;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(4, 368);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(251, 28);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // numStake
            // 
            this.numStake.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numStake.Location = new System.Drawing.Point(4, 201);
            this.numStake.Margin = new System.Windows.Forms.Padding(4);
            this.numStake.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numStake.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numStake.Name = "numStake";
            this.numStake.Size = new System.Drawing.Size(251, 22);
            this.numStake.TabIndex = 19;
            this.numStake.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 127);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Size = new System.Drawing.Size(77, 22);
            this.label3.TabIndex = 16;
            this.label3.Text = "Over/Under";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label2.Size = new System.Drawing.Size(85, 22);
            this.label2.TabIndex = 15;
            this.label2.Text = "Fulltime Hdp:";
            // 
            // dgvMain
            // 
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(271, 4);
            this.dgvMain.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.RowHeadersWidth = 51;
            this.dgvMain.Size = new System.Drawing.Size(858, 566);
            this.dgvMain.TabIndex = 1;
            // 
            // AddBetFirstHalfByFulltimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 574);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddBetFirstHalfByFulltimeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddBetFirstHalfByFulltimeForm";
            this.Load += new System.EventHandler(this.AddBetFirstHalfByFulltimeForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFulltimeHdp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStake)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.NumericUpDown numFulltimeHdp;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbOU;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.NumericUpDown numStake;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbHdp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbCareGoal;
        private System.Windows.Forms.RadioButton rdCareGoal_4;
        private System.Windows.Forms.RadioButton rdCareGoal_3;
        private System.Windows.Forms.RadioButton rdCareGoal_2;
        private System.Windows.Forms.RadioButton rdCareGoal_1;
        private System.Windows.Forms.Button btnStake3;
        private System.Windows.Forms.Button btnStake100;
        private System.Windows.Forms.Button btnStake500;
        private System.Windows.Forms.Button btnStake1000;
        private System.Windows.Forms.Button btnDeleteAll;
    }
}