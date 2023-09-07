namespace LiveBetApp.Forms.DirectBet
{
    partial class ConfirmDirectBet
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnA00 = new System.Windows.Forms.Button();
            this.btnA25 = new System.Windows.Forms.Button();
            this.btnA50 = new System.Windows.Forms.Button();
            this.btnA75 = new System.Windows.Forms.Button();
            this.numStake = new System.Windows.Forms.NumericUpDown();
            this.btnStake3 = new System.Windows.Forms.Button();
            this.btnStake100 = new System.Windows.Forms.Button();
            this.btnStake500 = new System.Windows.Forms.Button();
            this.btnStake1000 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numStake)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stake";
            // 
            // btnA00
            // 
            this.btnA00.Location = new System.Drawing.Point(12, 89);
            this.btnA00.Name = "btnA00";
            this.btnA00.Size = new System.Drawing.Size(234, 23);
            this.btnA00.TabIndex = 2;
            this.btnA00.Text = "A_00";
            this.btnA00.UseVisualStyleBackColor = true;
            this.btnA00.Click += new System.EventHandler(this.btnA00_Click);
            // 
            // btnA25
            // 
            this.btnA25.Location = new System.Drawing.Point(12, 118);
            this.btnA25.Name = "btnA25";
            this.btnA25.Size = new System.Drawing.Size(234, 23);
            this.btnA25.TabIndex = 2;
            this.btnA25.Text = "A_25";
            this.btnA25.UseVisualStyleBackColor = true;
            this.btnA25.Click += new System.EventHandler(this.btnA25_Click);
            // 
            // btnA50
            // 
            this.btnA50.Location = new System.Drawing.Point(12, 147);
            this.btnA50.Name = "btnA50";
            this.btnA50.Size = new System.Drawing.Size(234, 23);
            this.btnA50.TabIndex = 2;
            this.btnA50.Text = "A_50";
            this.btnA50.UseVisualStyleBackColor = true;
            this.btnA50.Click += new System.EventHandler(this.btnA50_Click);
            // 
            // btnA75
            // 
            this.btnA75.Location = new System.Drawing.Point(12, 176);
            this.btnA75.Name = "btnA75";
            this.btnA75.Size = new System.Drawing.Size(234, 25);
            this.btnA75.TabIndex = 2;
            this.btnA75.Text = "A_75";
            this.btnA75.UseVisualStyleBackColor = true;
            this.btnA75.Click += new System.EventHandler(this.btnA75_Click);
            // 
            // numStake
            // 
            this.numStake.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numStake.Location = new System.Drawing.Point(12, 32);
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
            this.numStake.Size = new System.Drawing.Size(234, 22);
            this.numStake.TabIndex = 2;
            this.numStake.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnStake3
            // 
            this.btnStake3.Location = new System.Drawing.Point(12, 60);
            this.btnStake3.Name = "btnStake3";
            this.btnStake3.Size = new System.Drawing.Size(54, 23);
            this.btnStake3.TabIndex = 29;
            this.btnStake3.Text = "100";
            this.btnStake3.UseVisualStyleBackColor = true;
            this.btnStake3.Click += new System.EventHandler(this.btnStake3_Click);
            // 
            // btnStake100
            // 
            this.btnStake100.Location = new System.Drawing.Point(72, 60);
            this.btnStake100.Name = "btnStake100";
            this.btnStake100.Size = new System.Drawing.Size(54, 23);
            this.btnStake100.TabIndex = 28;
            this.btnStake100.Text = "300";
            this.btnStake100.UseVisualStyleBackColor = true;
            this.btnStake100.Click += new System.EventHandler(this.btnStake100_Click);
            // 
            // btnStake500
            // 
            this.btnStake500.Location = new System.Drawing.Point(132, 60);
            this.btnStake500.Name = "btnStake500";
            this.btnStake500.Size = new System.Drawing.Size(54, 23);
            this.btnStake500.TabIndex = 27;
            this.btnStake500.Text = "500";
            this.btnStake500.UseVisualStyleBackColor = true;
            this.btnStake500.Click += new System.EventHandler(this.btnStake500_Click);
            // 
            // btnStake1000
            // 
            this.btnStake1000.Location = new System.Drawing.Point(192, 60);
            this.btnStake1000.Name = "btnStake1000";
            this.btnStake1000.Size = new System.Drawing.Size(54, 23);
            this.btnStake1000.TabIndex = 26;
            this.btnStake1000.Text = "1000";
            this.btnStake1000.UseVisualStyleBackColor = true;
            this.btnStake1000.Click += new System.EventHandler(this.btnStake1000_Click);
            // 
            // ConfirmDirectBet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 220);
            this.Controls.Add(this.btnStake3);
            this.Controls.Add(this.btnStake100);
            this.Controls.Add(this.btnStake500);
            this.Controls.Add(this.btnStake1000);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numStake);
            this.Controls.Add(this.btnA75);
            this.Controls.Add(this.btnA50);
            this.Controls.Add(this.btnA25);
            this.Controls.Add(this.btnA00);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmDirectBet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfirmDirectBet";
            this.Load += new System.EventHandler(this.ConfirmDirectBet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numStake)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnA00;
        private System.Windows.Forms.Button btnA25;
        private System.Windows.Forms.Button btnA50;
        private System.Windows.Forms.Button btnA75;
        private System.Windows.Forms.NumericUpDown numStake;
        private System.Windows.Forms.Button btnStake3;
        private System.Windows.Forms.Button btnStake100;
        private System.Windows.Forms.Button btnStake500;
        private System.Windows.Forms.Button btnStake1000;
    }
}