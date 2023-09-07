namespace LiveBetApp.Forms.TimmingBet
{
    partial class AddTimmingBetOverUnderForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnStake3 = new System.Windows.Forms.Button();
            this.btnStake100 = new System.Windows.Forms.Button();
            this.btnStake500 = new System.Windows.Forms.Button();
            this.btnStake1000 = new System.Windows.Forms.Button();
            this.cbCareGoal = new System.Windows.Forms.CheckBox();
            this.cbbFtHt = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbOU = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.numStake = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbMinute = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbLivePeriod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1241, 583);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnStake3);
            this.groupBox1.Controls.Add(this.btnStake100);
            this.groupBox1.Controls.Add(this.btnStake500);
            this.groupBox1.Controls.Add(this.btnStake1000);
            this.groupBox1.Controls.Add(this.cbCareGoal);
            this.groupBox1.Controls.Add(this.cbbFtHt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbbOU);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.numStake);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbbMinute);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbbLivePeriod);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(259, 575);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CreateForm";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 538);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(251, 28);
            this.button1.TabIndex = 30;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStake3
            // 
            this.btnStake3.Location = new System.Drawing.Point(8, 265);
            this.btnStake3.Name = "btnStake3";
            this.btnStake3.Size = new System.Drawing.Size(54, 23);
            this.btnStake3.TabIndex = 29;
            this.btnStake3.Text = "100";
            this.btnStake3.UseVisualStyleBackColor = true;
            this.btnStake3.Click += new System.EventHandler(this.btnStake3_Click);
            // 
            // btnStake100
            // 
            this.btnStake100.Location = new System.Drawing.Point(68, 265);
            this.btnStake100.Name = "btnStake100";
            this.btnStake100.Size = new System.Drawing.Size(54, 23);
            this.btnStake100.TabIndex = 28;
            this.btnStake100.Text = "300";
            this.btnStake100.UseVisualStyleBackColor = true;
            this.btnStake100.Click += new System.EventHandler(this.btnStake100_Click);
            // 
            // btnStake500
            // 
            this.btnStake500.Location = new System.Drawing.Point(128, 265);
            this.btnStake500.Name = "btnStake500";
            this.btnStake500.Size = new System.Drawing.Size(54, 23);
            this.btnStake500.TabIndex = 27;
            this.btnStake500.Text = "500";
            this.btnStake500.UseVisualStyleBackColor = true;
            this.btnStake500.Click += new System.EventHandler(this.btnStake500_Click);
            // 
            // btnStake1000
            // 
            this.btnStake1000.Location = new System.Drawing.Point(188, 265);
            this.btnStake1000.Name = "btnStake1000";
            this.btnStake1000.Size = new System.Drawing.Size(54, 23);
            this.btnStake1000.TabIndex = 26;
            this.btnStake1000.Text = "1000";
            this.btnStake1000.UseVisualStyleBackColor = true;
            this.btnStake1000.Click += new System.EventHandler(this.btnStake1000_Click);
            // 
            // cbCareGoal
            // 
            this.cbCareGoal.AutoSize = true;
            this.cbCareGoal.Checked = true;
            this.cbCareGoal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCareGoal.Location = new System.Drawing.Point(8, 295);
            this.cbCareGoal.Margin = new System.Windows.Forms.Padding(4);
            this.cbCareGoal.Name = "cbCareGoal";
            this.cbCareGoal.Size = new System.Drawing.Size(90, 20);
            this.cbCareGoal.TabIndex = 16;
            this.cbCareGoal.Text = "Care Goal";
            this.cbCareGoal.UseVisualStyleBackColor = true;
            // 
            // cbbFtHt
            // 
            this.cbbFtHt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFtHt.FormattingEnabled = true;
            this.cbbFtHt.Location = new System.Drawing.Point(4, 46);
            this.cbbFtHt.Margin = new System.Windows.Forms.Padding(4);
            this.cbbFtHt.Name = "cbbFtHt";
            this.cbbFtHt.Size = new System.Drawing.Size(249, 24);
            this.cbbFtHt.TabIndex = 11;
            this.cbbFtHt.SelectedIndexChanged += new System.EventHandler(this.cbbFtHt_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label5.Size = new System.Drawing.Size(103, 22);
            this.label5.TabIndex = 10;
            this.label5.Text = "Fulltime/Firsthalf";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 215);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(42, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "Stake";
            // 
            // cbbOU
            // 
            this.cbbOU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOU.FormattingEnabled = true;
            this.cbbOU.Location = new System.Drawing.Point(4, 190);
            this.cbbOU.Margin = new System.Windows.Forms.Padding(4);
            this.cbbOU.Name = "cbbOU";
            this.cbbOU.Size = new System.Drawing.Size(249, 24);
            this.cbbOU.TabIndex = 6;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(4, 324);
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
            this.numStake.Location = new System.Drawing.Point(4, 241);
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
            this.label3.Location = new System.Drawing.Point(4, 167);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Size = new System.Drawing.Size(77, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "Over/Under";
            // 
            // cbbMinute
            // 
            this.cbbMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMinute.FormattingEnabled = true;
            this.cbbMinute.Location = new System.Drawing.Point(4, 142);
            this.cbbMinute.Margin = new System.Windows.Forms.Padding(4);
            this.cbbMinute.Name = "cbbMinute";
            this.cbbMinute.Size = new System.Drawing.Size(249, 24);
            this.cbbMinute.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 119);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label2.Size = new System.Drawing.Size(49, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Minute:";
            // 
            // cbbLivePeriod
            // 
            this.cbbLivePeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLivePeriod.FormattingEnabled = true;
            this.cbbLivePeriod.Location = new System.Drawing.Point(4, 94);
            this.cbbLivePeriod.Margin = new System.Windows.Forms.Padding(4);
            this.cbbLivePeriod.Name = "cbbLivePeriod";
            this.cbbLivePeriod.Size = new System.Drawing.Size(249, 24);
            this.cbbLivePeriod.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Size = new System.Drawing.Size(78, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Live Period:";
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
            this.dataGridView.Size = new System.Drawing.Size(966, 575);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // AddTimmingBetOverUnderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 583);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddTimmingBetOverUnderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Timming Bet Over/Under";
            this.Load += new System.EventHandler(this.AddTimmingBetOverForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStake)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox cbbLivePeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbMinute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numStake;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbOU;
        private System.Windows.Forms.ComboBox cbbFtHt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbCareGoal;
        private System.Windows.Forms.Button btnStake3;
        private System.Windows.Forms.Button btnStake100;
        private System.Windows.Forms.Button btnStake500;
        private System.Windows.Forms.Button btnStake1000;
        private System.Windows.Forms.Button button1;
    }
}