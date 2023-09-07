namespace LiveBetApp.Forms.Setting
{
    partial class MatchFilter
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
            this.label4 = new System.Windows.Forms.Label();
            this.chbKeepRefreshing = new System.Windows.Forms.CheckBox();
            this.chbPlaningMatchs = new System.Windows.Forms.CheckBox();
            this.dtFilterFrom = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chbShowSaba = new System.Windows.Forms.CheckBox();
            this.chbShowCorners = new System.Windows.Forms.CheckBox();
            this.rtbLeagues = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdNoneLive = new System.Windows.Forms.RadioButton();
            this.rdLive = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.chbRunningMatchsH1 = new System.Windows.Forms.CheckBox();
            this.chbFinishedMatchs = new System.Windows.Forms.CheckBox();
            this.chbSpecialFilter = new System.Windows.Forms.CheckBox();
            this.chbNoShowMatchs = new System.Windows.Forms.CheckBox();
            this.chbRunningMatchsH2 = new System.Windows.Forms.CheckBox();
            this.chbRunningMatchsHt = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(465, 367);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(68, 22);
            this.label4.TabIndex = 14;
            this.label4.Text = "League(s)";
            // 
            // chbKeepRefreshing
            // 
            this.chbKeepRefreshing.AutoSize = true;
            this.chbKeepRefreshing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbKeepRefreshing.Location = new System.Drawing.Point(12, 611);
            this.chbKeepRefreshing.Margin = new System.Windows.Forms.Padding(4);
            this.chbKeepRefreshing.Name = "chbKeepRefreshing";
            this.chbKeepRefreshing.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chbKeepRefreshing.Size = new System.Drawing.Size(144, 22);
            this.chbKeepRefreshing.TabIndex = 5;
            this.chbKeepRefreshing.Text = "Keep Refreshing";
            this.chbKeepRefreshing.UseVisualStyleBackColor = true;
            // 
            // chbPlaningMatchs
            // 
            this.chbPlaningMatchs.AutoSize = true;
            this.chbPlaningMatchs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbPlaningMatchs.Location = new System.Drawing.Point(13, 75);
            this.chbPlaningMatchs.Margin = new System.Windows.Forms.Padding(4);
            this.chbPlaningMatchs.Name = "chbPlaningMatchs";
            this.chbPlaningMatchs.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chbPlaningMatchs.Size = new System.Drawing.Size(144, 22);
            this.chbPlaningMatchs.TabIndex = 4;
            this.chbPlaningMatchs.Text = "Planning Matchs";
            this.chbPlaningMatchs.UseVisualStyleBackColor = true;
            // 
            // dtFilterFrom
            // 
            this.dtFilterFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFilterFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFilterFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFilterFrom.Location = new System.Drawing.Point(13, 13);
            this.dtFilterFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dtFilterFrom.Name = "dtFilterFrom";
            this.dtFilterFrom.Size = new System.Drawing.Size(444, 24);
            this.dtFilterFrom.TabIndex = 1;
            this.dtFilterFrom.CloseUp += new System.EventHandler(this.dtFilter_CloseUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(465, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label5.Size = new System.Drawing.Size(36, 22);
            this.label5.TabIndex = 14;
            this.label5.Text = "Date";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 641);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(526, 26);
            this.button1.TabIndex = 21;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chbShowSaba
            // 
            this.chbShowSaba.AutoSize = true;
            this.chbShowSaba.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbShowSaba.Location = new System.Drawing.Point(12, 581);
            this.chbShowSaba.Margin = new System.Windows.Forms.Padding(4);
            this.chbShowSaba.Name = "chbShowSaba";
            this.chbShowSaba.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chbShowSaba.Size = new System.Drawing.Size(145, 22);
            this.chbShowSaba.TabIndex = 22;
            this.chbShowSaba.Text = "Show Saba Only";
            this.chbShowSaba.UseVisualStyleBackColor = true;
            // 
            // chbShowCorners
            // 
            this.chbShowCorners.AutoSize = true;
            this.chbShowCorners.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbShowCorners.Location = new System.Drawing.Point(12, 551);
            this.chbShowCorners.Margin = new System.Windows.Forms.Padding(4);
            this.chbShowCorners.Name = "chbShowCorners";
            this.chbShowCorners.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chbShowCorners.Size = new System.Drawing.Size(153, 22);
            this.chbShowCorners.TabIndex = 18;
            this.chbShowCorners.Text = "Show CORNERS";
            this.chbShowCorners.UseVisualStyleBackColor = true;
            // 
            // rtbLeagues
            // 
            this.rtbLeagues.Location = new System.Drawing.Point(12, 367);
            this.rtbLeagues.Margin = new System.Windows.Forms.Padding(4);
            this.rtbLeagues.Name = "rtbLeagues";
            this.rtbLeagues.Size = new System.Drawing.Size(445, 176);
            this.rtbLeagues.TabIndex = 13;
            this.rtbLeagues.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdNoneLive);
            this.groupBox1.Controls.Add(this.rdLive);
            this.groupBox1.Controls.Add(this.rdAll);
            this.groupBox1.Location = new System.Drawing.Point(12, 256);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(445, 105);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Livesteam";
            // 
            // rdNoneLive
            // 
            this.rdNoneLive.AutoSize = true;
            this.rdNoneLive.Location = new System.Drawing.Point(9, 75);
            this.rdNoneLive.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdNoneLive.Name = "rdNoneLive";
            this.rdNoneLive.Size = new System.Drawing.Size(89, 20);
            this.rdNoneLive.TabIndex = 0;
            this.rdNoneLive.TabStop = true;
            this.rdNoneLive.Text = "None Live";
            this.rdNoneLive.UseVisualStyleBackColor = true;
            // 
            // rdLive
            // 
            this.rdLive.AutoSize = true;
            this.rdLive.Location = new System.Drawing.Point(9, 48);
            this.rdLive.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdLive.Name = "rdLive";
            this.rdLive.Size = new System.Drawing.Size(53, 20);
            this.rdLive.TabIndex = 0;
            this.rdLive.TabStop = true;
            this.rdLive.Text = "Live";
            this.rdLive.UseVisualStyleBackColor = true;
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Location = new System.Drawing.Point(9, 21);
            this.rdAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(43, 20);
            this.rdAll.TabIndex = 0;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "All";
            this.rdAll.UseVisualStyleBackColor = true;
            // 
            // chbRunningMatchsH1
            // 
            this.chbRunningMatchsH1.AutoSize = true;
            this.chbRunningMatchsH1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbRunningMatchsH1.Location = new System.Drawing.Point(13, 105);
            this.chbRunningMatchsH1.Margin = new System.Windows.Forms.Padding(4);
            this.chbRunningMatchsH1.Name = "chbRunningMatchsH1";
            this.chbRunningMatchsH1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chbRunningMatchsH1.Size = new System.Drawing.Size(175, 22);
            this.chbRunningMatchsH1.TabIndex = 4;
            this.chbRunningMatchsH1.Text = "Running Matchs (H1)";
            this.chbRunningMatchsH1.UseVisualStyleBackColor = true;
            // 
            // chbFinishedMatchs
            // 
            this.chbFinishedMatchs.AutoSize = true;
            this.chbFinishedMatchs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbFinishedMatchs.Location = new System.Drawing.Point(12, 195);
            this.chbFinishedMatchs.Margin = new System.Windows.Forms.Padding(4);
            this.chbFinishedMatchs.Name = "chbFinishedMatchs";
            this.chbFinishedMatchs.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chbFinishedMatchs.Size = new System.Drawing.Size(143, 22);
            this.chbFinishedMatchs.TabIndex = 4;
            this.chbFinishedMatchs.Text = "Finished Matchs";
            this.chbFinishedMatchs.UseVisualStyleBackColor = true;
            // 
            // chbSpecialFilter
            // 
            this.chbSpecialFilter.AutoSize = true;
            this.chbSpecialFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbSpecialFilter.Location = new System.Drawing.Point(13, 45);
            this.chbSpecialFilter.Margin = new System.Windows.Forms.Padding(4);
            this.chbSpecialFilter.Name = "chbSpecialFilter";
            this.chbSpecialFilter.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chbSpecialFilter.Size = new System.Drawing.Size(119, 22);
            this.chbSpecialFilter.TabIndex = 5;
            this.chbSpecialFilter.Text = "Special Filter";
            this.chbSpecialFilter.UseVisualStyleBackColor = true;
            // 
            // chbNoShowMatchs
            // 
            this.chbNoShowMatchs.AutoSize = true;
            this.chbNoShowMatchs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbNoShowMatchs.Location = new System.Drawing.Point(13, 225);
            this.chbNoShowMatchs.Margin = new System.Windows.Forms.Padding(4);
            this.chbNoShowMatchs.Name = "chbNoShowMatchs";
            this.chbNoShowMatchs.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chbNoShowMatchs.Size = new System.Drawing.Size(150, 22);
            this.chbNoShowMatchs.TabIndex = 5;
            this.chbNoShowMatchs.Text = "No Show Matchs";
            this.chbNoShowMatchs.UseVisualStyleBackColor = true;
            // 
            // chbRunningMatchsH2
            // 
            this.chbRunningMatchsH2.AutoSize = true;
            this.chbRunningMatchsH2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbRunningMatchsH2.Location = new System.Drawing.Point(13, 135);
            this.chbRunningMatchsH2.Margin = new System.Windows.Forms.Padding(4);
            this.chbRunningMatchsH2.Name = "chbRunningMatchsH2";
            this.chbRunningMatchsH2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chbRunningMatchsH2.Size = new System.Drawing.Size(175, 22);
            this.chbRunningMatchsH2.TabIndex = 4;
            this.chbRunningMatchsH2.Text = "Running Matchs (H2)";
            this.chbRunningMatchsH2.UseVisualStyleBackColor = true;
            // 
            // chbRunningMatchsHt
            // 
            this.chbRunningMatchsHt.AutoSize = true;
            this.chbRunningMatchsHt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbRunningMatchsHt.Location = new System.Drawing.Point(13, 165);
            this.chbRunningMatchsHt.Margin = new System.Windows.Forms.Padding(4);
            this.chbRunningMatchsHt.Name = "chbRunningMatchsHt";
            this.chbRunningMatchsHt.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.chbRunningMatchsHt.Size = new System.Drawing.Size(176, 22);
            this.chbRunningMatchsHt.TabIndex = 4;
            this.chbRunningMatchsHt.Text = "Running Matchs (HT)";
            this.chbRunningMatchsHt.UseVisualStyleBackColor = true;
            // 
            // MatchFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 677);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chbShowSaba);
            this.Controls.Add(this.chbPlaningMatchs);
            this.Controls.Add(this.chbShowCorners);
            this.Controls.Add(this.dtFilterFrom);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtbLeagues);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chbSpecialFilter);
            this.Controls.Add(this.chbKeepRefreshing);
            this.Controls.Add(this.chbRunningMatchsHt);
            this.Controls.Add(this.chbRunningMatchsH2);
            this.Controls.Add(this.chbRunningMatchsH1);
            this.Controls.Add(this.chbFinishedMatchs);
            this.Controls.Add(this.chbNoShowMatchs);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MatchFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Match Filter";
            this.Load += new System.EventHandler(this.MatchFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chbKeepRefreshing;
        private System.Windows.Forms.CheckBox chbPlaningMatchs;
        private System.Windows.Forms.DateTimePicker dtFilterFrom;
        private System.Windows.Forms.CheckBox chbShowCorners;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdNoneLive;
        private System.Windows.Forms.RadioButton rdLive;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chbShowSaba;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtbLeagues;
        private System.Windows.Forms.CheckBox chbRunningMatchsH1;
        private System.Windows.Forms.CheckBox chbFinishedMatchs;
        private System.Windows.Forms.CheckBox chbSpecialFilter;
        private System.Windows.Forms.CheckBox chbNoShowMatchs;
        private System.Windows.Forms.CheckBox chbRunningMatchsH2;
        private System.Windows.Forms.CheckBox chbRunningMatchsHt;
    }
}