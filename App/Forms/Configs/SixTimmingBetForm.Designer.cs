namespace App.Forms.Configs
{
    partial class SixTimmingBetForm
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
            App.DataModels.TimmingBetUserControlModel timmingBetUserControlModel1 = new App.DataModels.TimmingBetUserControlModel();
            App.DataModels.TimmingBetUserControlModel timmingBetUserControlModel2 = new App.DataModels.TimmingBetUserControlModel();
            App.DataModels.TimmingBetUserControlModel timmingBetUserControlModel3 = new App.DataModels.TimmingBetUserControlModel();
            App.DataModels.TimmingBetUserControlModel timmingBetUserControlModel4 = new App.DataModels.TimmingBetUserControlModel();
            App.DataModels.TimmingBetUserControlModel timmingBetUserControlModel5 = new App.DataModels.TimmingBetUserControlModel();
            App.DataModels.TimmingBetUserControlModel timmingBetUserControlModel6 = new App.DataModels.TimmingBetUserControlModel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.sixthTimmingBetUC = new App.Forms.Configs.TimmingBetUserControl();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.fifthTimmingBetUC = new App.Forms.Configs.TimmingBetUserControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.fourthTimmingBetUC = new App.Forms.Configs.TimmingBetUserControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.thirdTimmingBetUC = new App.Forms.Configs.TimmingBetUserControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.secondTimmingBetUC = new App.Forms.Configs.TimmingBetUserControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.firstTimmingBetUC = new App.Forms.Configs.TimmingBetUserControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox6, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1254, 159);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.sixthTimmingBetUC);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(1047, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(204, 153);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Sixth";
            // 
            // sixthTimmingBetUC
            // 
            this.sixthTimmingBetUC.Dock = System.Windows.Forms.DockStyle.Top;
            this.sixthTimmingBetUC.Location = new System.Drawing.Point(3, 16);
            this.sixthTimmingBetUC.Margin = new System.Windows.Forms.Padding(5);
            timmingBetUserControlModel1.Amount = 0;
            timmingBetUserControlModel1.BetTime = " ";
            timmingBetUserControlModel1.Type = App.DataModels.enumTimmingBetUserControlTypeEnum.FullTimeHandicapHome;
            this.sixthTimmingBetUC.Model = timmingBetUserControlModel1;
            this.sixthTimmingBetUC.Name = "sixthTimmingBetUC";
            this.sixthTimmingBetUC.Size = new System.Drawing.Size(198, 136);
            this.sixthTimmingBetUC.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.fifthTimmingBetUC);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(839, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(202, 153);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Fifth";
            // 
            // fifthTimmingBetUC
            // 
            this.fifthTimmingBetUC.Dock = System.Windows.Forms.DockStyle.Top;
            this.fifthTimmingBetUC.Location = new System.Drawing.Point(3, 16);
            this.fifthTimmingBetUC.Margin = new System.Windows.Forms.Padding(5);
            timmingBetUserControlModel2.Amount = 0;
            timmingBetUserControlModel2.BetTime = " ";
            timmingBetUserControlModel2.Type = App.DataModels.enumTimmingBetUserControlTypeEnum.FullTimeHandicapHome;
            this.fifthTimmingBetUC.Model = timmingBetUserControlModel2;
            this.fifthTimmingBetUC.Name = "fifthTimmingBetUC";
            this.fifthTimmingBetUC.Size = new System.Drawing.Size(196, 136);
            this.fifthTimmingBetUC.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.fourthTimmingBetUC);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(630, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(203, 153);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fourth";
            // 
            // fourthTimmingBetUC
            // 
            this.fourthTimmingBetUC.Dock = System.Windows.Forms.DockStyle.Top;
            this.fourthTimmingBetUC.Location = new System.Drawing.Point(3, 16);
            this.fourthTimmingBetUC.Margin = new System.Windows.Forms.Padding(5);
            timmingBetUserControlModel3.Amount = 0;
            timmingBetUserControlModel3.BetTime = " ";
            timmingBetUserControlModel3.Type = App.DataModels.enumTimmingBetUserControlTypeEnum.FullTimeHandicapHome;
            this.fourthTimmingBetUC.Model = timmingBetUserControlModel3;
            this.fourthTimmingBetUC.Name = "fourthTimmingBetUC";
            this.fourthTimmingBetUC.Size = new System.Drawing.Size(197, 136);
            this.fourthTimmingBetUC.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.thirdTimmingBetUC);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(421, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(203, 153);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Third";
            // 
            // thirdTimmingBetUC
            // 
            this.thirdTimmingBetUC.Dock = System.Windows.Forms.DockStyle.Top;
            this.thirdTimmingBetUC.Location = new System.Drawing.Point(3, 16);
            this.thirdTimmingBetUC.Margin = new System.Windows.Forms.Padding(5);
            timmingBetUserControlModel4.Amount = 0;
            timmingBetUserControlModel4.BetTime = " ";
            timmingBetUserControlModel4.Type = App.DataModels.enumTimmingBetUserControlTypeEnum.FullTimeHandicapHome;
            this.thirdTimmingBetUC.Model = timmingBetUserControlModel4;
            this.thirdTimmingBetUC.Name = "thirdTimmingBetUC";
            this.thirdTimmingBetUC.Size = new System.Drawing.Size(197, 136);
            this.thirdTimmingBetUC.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.secondTimmingBetUC);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(212, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(203, 153);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Second";
            // 
            // secondTimmingBetUC
            // 
            this.secondTimmingBetUC.Dock = System.Windows.Forms.DockStyle.Top;
            this.secondTimmingBetUC.Location = new System.Drawing.Point(3, 16);
            this.secondTimmingBetUC.Margin = new System.Windows.Forms.Padding(5);
            timmingBetUserControlModel5.Amount = 0;
            timmingBetUserControlModel5.BetTime = " ";
            timmingBetUserControlModel5.Type = App.DataModels.enumTimmingBetUserControlTypeEnum.FullTimeHandicapHome;
            this.secondTimmingBetUC.Model = timmingBetUserControlModel5;
            this.secondTimmingBetUC.Name = "secondTimmingBetUC";
            this.secondTimmingBetUC.Size = new System.Drawing.Size(197, 136);
            this.secondTimmingBetUC.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.firstTimmingBetUC);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 153);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "First";
            // 
            // firstTimmingBetUC
            // 
            this.firstTimmingBetUC.Dock = System.Windows.Forms.DockStyle.Top;
            this.firstTimmingBetUC.Location = new System.Drawing.Point(3, 16);
            this.firstTimmingBetUC.Margin = new System.Windows.Forms.Padding(5);
            timmingBetUserControlModel6.Amount = 0;
            timmingBetUserControlModel6.BetTime = " ";
            timmingBetUserControlModel6.Type = App.DataModels.enumTimmingBetUserControlTypeEnum.FullTimeHandicapHome;
            this.firstTimmingBetUC.Model = timmingBetUserControlModel6;
            this.firstTimmingBetUC.Name = "firstTimmingBetUC";
            this.firstTimmingBetUC.Size = new System.Drawing.Size(197, 136);
            this.firstTimmingBetUC.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnSave);
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Size = new System.Drawing.Size(1254, 188);
            this.splitContainer1.SplitterDistance = 159;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Location = new System.Drawing.Point(1104, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Location = new System.Drawing.Point(1179, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SixTimmingBetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 198);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1280, 220);
            this.Name = "SixTimmingBetForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Six Timming Bet";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox6;
        private TimmingBetUserControl sixthTimmingBetUC;
        private System.Windows.Forms.GroupBox groupBox5;
        private TimmingBetUserControl fifthTimmingBetUC;
        private System.Windows.Forms.GroupBox groupBox4;
        private TimmingBetUserControl fourthTimmingBetUC;
        private System.Windows.Forms.GroupBox groupBox3;
        private TimmingBetUserControl thirdTimmingBetUC;
        private System.Windows.Forms.GroupBox groupBox2;
        private TimmingBetUserControl secondTimmingBetUC;
        private System.Windows.Forms.GroupBox groupBox1;
        private TimmingBetUserControl firstTimmingBetUC;

    }
}