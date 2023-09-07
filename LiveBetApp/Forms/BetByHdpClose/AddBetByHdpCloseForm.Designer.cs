namespace LiveBetApp.Forms.BetByHdpClose
{
    partial class AddBetByHdpCloseForm
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
            this.numHdp = new System.Windows.Forms.NumericUpDown();
            this.numUptoMinute = new System.Windows.Forms.NumericUpDown();
            this.numMinuteAfterClose = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbBetOption = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.numStake = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHdp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUptoMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinuteAfterClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStake)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1055, 564);
            this.tableLayoutPanel1.TabIndex = 2;
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
            this.groupBox1.Controls.Add(this.numHdp);
            this.groupBox1.Controls.Add(this.numUptoMinute);
            this.groupBox1.Controls.Add(this.numMinuteAfterClose);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbbBetOption);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.numStake);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(259, 556);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CreateForm";
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(4, 519);
            this.btnDeleteAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(251, 28);
            this.btnDeleteAll.TabIndex = 47;
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnStake3
            // 
            this.btnStake3.Location = new System.Drawing.Point(8, 283);
            this.btnStake3.Name = "btnStake3";
            this.btnStake3.Size = new System.Drawing.Size(54, 23);
            this.btnStake3.TabIndex = 46;
            this.btnStake3.Text = "100";
            this.btnStake3.UseVisualStyleBackColor = true;
            this.btnStake3.Click += new System.EventHandler(this.btnStake3_Click);
            // 
            // btnStake100
            // 
            this.btnStake100.Location = new System.Drawing.Point(68, 283);
            this.btnStake100.Name = "btnStake100";
            this.btnStake100.Size = new System.Drawing.Size(54, 23);
            this.btnStake100.TabIndex = 45;
            this.btnStake100.Text = "300";
            this.btnStake100.UseVisualStyleBackColor = true;
            this.btnStake100.Click += new System.EventHandler(this.btnStake100_Click);
            // 
            // btnStake500
            // 
            this.btnStake500.Location = new System.Drawing.Point(128, 283);
            this.btnStake500.Name = "btnStake500";
            this.btnStake500.Size = new System.Drawing.Size(54, 23);
            this.btnStake500.TabIndex = 44;
            this.btnStake500.Text = "500";
            this.btnStake500.UseVisualStyleBackColor = true;
            this.btnStake500.Click += new System.EventHandler(this.btnStake500_Click);
            // 
            // btnStake1000
            // 
            this.btnStake1000.Location = new System.Drawing.Point(188, 283);
            this.btnStake1000.Name = "btnStake1000";
            this.btnStake1000.Size = new System.Drawing.Size(54, 23);
            this.btnStake1000.TabIndex = 43;
            this.btnStake1000.Text = "1000";
            this.btnStake1000.UseVisualStyleBackColor = true;
            this.btnStake1000.Click += new System.EventHandler(this.btnStake1000_Click);
            // 
            // rdCareGoal_4
            // 
            this.rdCareGoal_4.AutoSize = true;
            this.rdCareGoal_4.Location = new System.Drawing.Point(209, 342);
            this.rdCareGoal_4.Margin = new System.Windows.Forms.Padding(4);
            this.rdCareGoal_4.Name = "rdCareGoal_4";
            this.rdCareGoal_4.Size = new System.Drawing.Size(35, 20);
            this.rdCareGoal_4.TabIndex = 18;
            this.rdCareGoal_4.TabStop = true;
            this.rdCareGoal_4.Text = "4";
            this.rdCareGoal_4.UseVisualStyleBackColor = true;
            // 
            // rdCareGoal_3
            // 
            this.rdCareGoal_3.AutoSize = true;
            this.rdCareGoal_3.Location = new System.Drawing.Point(141, 342);
            this.rdCareGoal_3.Margin = new System.Windows.Forms.Padding(4);
            this.rdCareGoal_3.Name = "rdCareGoal_3";
            this.rdCareGoal_3.Size = new System.Drawing.Size(35, 20);
            this.rdCareGoal_3.TabIndex = 19;
            this.rdCareGoal_3.TabStop = true;
            this.rdCareGoal_3.Text = "3";
            this.rdCareGoal_3.UseVisualStyleBackColor = true;
            // 
            // rdCareGoal_2
            // 
            this.rdCareGoal_2.AutoSize = true;
            this.rdCareGoal_2.Location = new System.Drawing.Point(71, 342);
            this.rdCareGoal_2.Margin = new System.Windows.Forms.Padding(4);
            this.rdCareGoal_2.Name = "rdCareGoal_2";
            this.rdCareGoal_2.Size = new System.Drawing.Size(35, 20);
            this.rdCareGoal_2.TabIndex = 20;
            this.rdCareGoal_2.TabStop = true;
            this.rdCareGoal_2.Text = "2";
            this.rdCareGoal_2.UseVisualStyleBackColor = true;
            // 
            // rdCareGoal_1
            // 
            this.rdCareGoal_1.AutoSize = true;
            this.rdCareGoal_1.Location = new System.Drawing.Point(8, 342);
            this.rdCareGoal_1.Margin = new System.Windows.Forms.Padding(4);
            this.rdCareGoal_1.Name = "rdCareGoal_1";
            this.rdCareGoal_1.Size = new System.Drawing.Size(35, 20);
            this.rdCareGoal_1.TabIndex = 21;
            this.rdCareGoal_1.TabStop = true;
            this.rdCareGoal_1.Text = "1";
            this.rdCareGoal_1.UseVisualStyleBackColor = true;
            // 
            // cbCareGoal
            // 
            this.cbCareGoal.AutoSize = true;
            this.cbCareGoal.Checked = true;
            this.cbCareGoal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCareGoal.Location = new System.Drawing.Point(8, 314);
            this.cbCareGoal.Margin = new System.Windows.Forms.Padding(4);
            this.cbCareGoal.Name = "cbCareGoal";
            this.cbCareGoal.Size = new System.Drawing.Size(90, 20);
            this.cbCareGoal.TabIndex = 15;
            this.cbCareGoal.Text = "Care Goal";
            this.cbCareGoal.UseVisualStyleBackColor = true;
            this.cbCareGoal.CheckedChanged += new System.EventHandler(this.cbCareGoal_CheckedChanged);
            // 
            // numHdp
            // 
            this.numHdp.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numHdp.Location = new System.Drawing.Point(4, 46);
            this.numHdp.Margin = new System.Windows.Forms.Padding(4);
            this.numHdp.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numHdp.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numHdp.Name = "numHdp";
            this.numHdp.Size = new System.Drawing.Size(251, 22);
            this.numHdp.TabIndex = 14;
            this.numHdp.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numUptoMinute
            // 
            this.numUptoMinute.Location = new System.Drawing.Point(4, 151);
            this.numUptoMinute.Margin = new System.Windows.Forms.Padding(4);
            this.numUptoMinute.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numUptoMinute.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUptoMinute.Name = "numUptoMinute";
            this.numUptoMinute.Size = new System.Drawing.Size(251, 22);
            this.numUptoMinute.TabIndex = 13;
            this.numUptoMinute.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // numMinuteAfterClose
            // 
            this.numMinuteAfterClose.Location = new System.Drawing.Point(4, 97);
            this.numMinuteAfterClose.Margin = new System.Windows.Forms.Padding(4);
            this.numMinuteAfterClose.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMinuteAfterClose.Name = "numMinuteAfterClose";
            this.numMinuteAfterClose.Size = new System.Drawing.Size(251, 22);
            this.numMinuteAfterClose.TabIndex = 13;
            this.numMinuteAfterClose.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 126);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Size = new System.Drawing.Size(81, 22);
            this.label1.TabIndex = 12;
            this.label1.Text = "Upto Minute:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 71);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label6.Size = new System.Drawing.Size(114, 22);
            this.label6.TabIndex = 12;
            this.label6.Text = "Minute after close:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 228);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(42, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "Stake";
            // 
            // cbbBetOption
            // 
            this.cbbBetOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbBetOption.FormattingEnabled = true;
            this.cbbBetOption.Location = new System.Drawing.Point(4, 202);
            this.cbbBetOption.Margin = new System.Windows.Forms.Padding(4);
            this.cbbBetOption.Name = "cbbBetOption";
            this.cbbBetOption.Size = new System.Drawing.Size(249, 24);
            this.cbbBetOption.TabIndex = 6;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(4, 370);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(251, 28);
            this.btnOk.TabIndex = 9;
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
            this.numStake.Location = new System.Drawing.Point(4, 254);
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
            this.numStake.TabIndex = 8;
            this.numStake.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 180);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Size = new System.Drawing.Size(46, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "Option";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label2.Size = new System.Drawing.Size(94, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Full Time Hdp:";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(271, 4);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(780, 556);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // AddBetByHdpCloseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 564);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddBetByHdpCloseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddBetByHdpCloseForm";
            this.Load += new System.EventHandler(this.AddBetByHdpCloseForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHdp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUptoMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinuteAfterClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStake)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numHdp;
        private System.Windows.Forms.NumericUpDown numMinuteAfterClose;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbBetOption;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.NumericUpDown numStake;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.NumericUpDown numUptoMinute;
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