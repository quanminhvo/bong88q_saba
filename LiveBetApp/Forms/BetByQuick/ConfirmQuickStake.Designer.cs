namespace LiveBetApp.Forms.BetByQuick
{
    partial class ConfirmQuickStake
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
            this.Stake = new System.Windows.Forms.Label();
            this.numStake = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbCareGoal = new System.Windows.Forms.CheckBox();
            this.btnStake3 = new System.Windows.Forms.Button();
            this.btnStake100 = new System.Windows.Forms.Button();
            this.btnStake500 = new System.Windows.Forms.Button();
            this.btnStake1000 = new System.Windows.Forms.Button();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lblPriceDesc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numStake)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // Stake
            // 
            this.Stake.AutoSize = true;
            this.Stake.Location = new System.Drawing.Point(12, 9);
            this.Stake.Name = "Stake";
            this.Stake.Size = new System.Drawing.Size(42, 16);
            this.Stake.TabIndex = 0;
            this.Stake.Text = "Stake";
            this.Stake.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numStake
            // 
            this.numStake.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numStake.Location = new System.Drawing.Point(60, 7);
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
            this.numStake.TabIndex = 3;
            this.numStake.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(60, 117);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(234, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cbCareGoal
            // 
            this.cbCareGoal.AutoSize = true;
            this.cbCareGoal.Location = new System.Drawing.Point(60, 91);
            this.cbCareGoal.Name = "cbCareGoal";
            this.cbCareGoal.Size = new System.Drawing.Size(90, 20);
            this.cbCareGoal.TabIndex = 6;
            this.cbCareGoal.Text = "Care Goal";
            this.cbCareGoal.UseVisualStyleBackColor = true;
            // 
            // btnStake3
            // 
            this.btnStake3.Location = new System.Drawing.Point(60, 62);
            this.btnStake3.Name = "btnStake3";
            this.btnStake3.Size = new System.Drawing.Size(54, 23);
            this.btnStake3.TabIndex = 42;
            this.btnStake3.Text = "100";
            this.btnStake3.UseVisualStyleBackColor = true;
            this.btnStake3.Click += new System.EventHandler(this.btnStake3_Click);
            // 
            // btnStake100
            // 
            this.btnStake100.Location = new System.Drawing.Point(120, 62);
            this.btnStake100.Name = "btnStake100";
            this.btnStake100.Size = new System.Drawing.Size(54, 23);
            this.btnStake100.TabIndex = 41;
            this.btnStake100.Text = "300";
            this.btnStake100.UseVisualStyleBackColor = true;
            this.btnStake100.Click += new System.EventHandler(this.btnStake100_Click);
            // 
            // btnStake500
            // 
            this.btnStake500.Location = new System.Drawing.Point(180, 62);
            this.btnStake500.Name = "btnStake500";
            this.btnStake500.Size = new System.Drawing.Size(54, 23);
            this.btnStake500.TabIndex = 40;
            this.btnStake500.Text = "500";
            this.btnStake500.UseVisualStyleBackColor = true;
            this.btnStake500.Click += new System.EventHandler(this.btnStake500_Click);
            // 
            // btnStake1000
            // 
            this.btnStake1000.Location = new System.Drawing.Point(240, 62);
            this.btnStake1000.Name = "btnStake1000";
            this.btnStake1000.Size = new System.Drawing.Size(54, 23);
            this.btnStake1000.TabIndex = 39;
            this.btnStake1000.Text = "1000";
            this.btnStake1000.UseVisualStyleBackColor = true;
            this.btnStake1000.Click += new System.EventHandler(this.btnStake1000_Click);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(12, 36);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(38, 16);
            this.lblPrice.TabIndex = 0;
            this.lblPrice.Text = "Price";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numPrice
            // 
            this.numPrice.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPrice.Location = new System.Drawing.Point(60, 34);
            this.numPrice.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(234, 22);
            this.numPrice.TabIndex = 3;
            this.numPrice.Value = new decimal(new int[] {
            25,
            0,
            0,
            -2147483648});
            // 
            // lblPriceDesc
            // 
            this.lblPriceDesc.AutoSize = true;
            this.lblPriceDesc.Location = new System.Drawing.Point(300, 36);
            this.lblPriceDesc.Name = "lblPriceDesc";
            this.lblPriceDesc.Size = new System.Drawing.Size(151, 16);
            this.lblPriceDesc.TabIndex = 0;
            this.lblPriceDesc.Text = "(Chỉ áp dụng tài cuối H1)";
            this.lblPriceDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfirmQuickStake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 151);
            this.Controls.Add(this.btnStake3);
            this.Controls.Add(this.btnStake100);
            this.Controls.Add(this.btnStake500);
            this.Controls.Add(this.btnStake1000);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.numStake);
            this.Controls.Add(this.cbCareGoal);
            this.Controls.Add(this.lblPriceDesc);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.Stake);
            this.MinimumSize = new System.Drawing.Size(202, 107);
            this.Name = "ConfirmQuickStake";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfirmQuickStake";
            this.Load += new System.EventHandler(this.ConfirmQuickStake_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numStake)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Stake;
        private System.Windows.Forms.NumericUpDown numStake;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbCareGoal;
        private System.Windows.Forms.Button btnStake3;
        private System.Windows.Forms.Button btnStake100;
        private System.Windows.Forms.Button btnStake500;
        private System.Windows.Forms.Button btnStake1000;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblPriceDesc;
    }
}