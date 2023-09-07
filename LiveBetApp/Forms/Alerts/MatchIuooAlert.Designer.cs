namespace LiveBetApp.Forms.Alerts
{
    partial class MatchIuooAlert
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.numPriceOverFt = new System.Windows.Forms.NumericUpDown();
            this.numHdpOverFt = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numPriceOverFh = new System.Windows.Forms.NumericUpDown();
            this.numHdpOverFh = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.numPriceHandicapFt = new System.Windows.Forms.NumericUpDown();
            this.numHdpHandicapFt = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceOverFt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHdpOverFt)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceOverFh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHdpOverFh)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceHandicapFt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHdpHandicapFt)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 28);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(31, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "Price";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(27, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "Hdp";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 75);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Over Fulltime";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.05383F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.94618F));
            this.tableLayoutPanel3.Controls.Add(this.numPriceOverFt, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.numHdpOverFt, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(209, 56);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // numPriceOverFt
            // 
            this.numPriceOverFt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numPriceOverFt.Location = new System.Drawing.Point(3, 31);
            this.numPriceOverFt.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numPriceOverFt.Name = "numPriceOverFt";
            this.numPriceOverFt.Size = new System.Drawing.Size(155, 20);
            this.numPriceOverFt.TabIndex = 27;
            // 
            // numHdpOverFt
            // 
            this.numHdpOverFt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numHdpOverFt.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numHdpOverFt.Location = new System.Drawing.Point(3, 3);
            this.numHdpOverFt.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numHdpOverFt.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numHdpOverFt.Name = "numHdpOverFt";
            this.numHdpOverFt.Size = new System.Drawing.Size(155, 20);
            this.numHdpOverFt.TabIndex = 18;
            this.numHdpOverFt.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnOk, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(221, 273);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 75);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Over Firsthalf";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.05383F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.94618F));
            this.tableLayoutPanel1.Controls.Add(this.numPriceOverFh, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numHdpOverFh, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(209, 56);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // numPriceOverFh
            // 
            this.numPriceOverFh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numPriceOverFh.Location = new System.Drawing.Point(3, 31);
            this.numPriceOverFh.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numPriceOverFh.Name = "numPriceOverFh";
            this.numPriceOverFh.Size = new System.Drawing.Size(155, 20);
            this.numPriceOverFh.TabIndex = 27;
            // 
            // numHdpOverFh
            // 
            this.numHdpOverFh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numHdpOverFh.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numHdpOverFh.Location = new System.Drawing.Point(3, 3);
            this.numHdpOverFh.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numHdpOverFh.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numHdpOverFh.Name = "numHdpOverFh";
            this.numHdpOverFh.Size = new System.Drawing.Size(155, 20);
            this.numHdpOverFh.TabIndex = 18;
            this.numHdpOverFh.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(27, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "Hdp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 28);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(31, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "Price";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 165);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(215, 75);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Handicap Fulltime";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.05383F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.94618F));
            this.tableLayoutPanel4.Controls.Add(this.numPriceHandicapFt, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.numHdpHandicapFt, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label6, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(209, 56);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // numPriceHandicapFt
            // 
            this.numPriceHandicapFt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numPriceHandicapFt.Location = new System.Drawing.Point(3, 31);
            this.numPriceHandicapFt.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numPriceHandicapFt.Name = "numPriceHandicapFt";
            this.numPriceHandicapFt.Size = new System.Drawing.Size(155, 20);
            this.numPriceHandicapFt.TabIndex = 27;
            // 
            // numHdpHandicapFt
            // 
            this.numHdpHandicapFt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numHdpHandicapFt.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numHdpHandicapFt.Location = new System.Drawing.Point(3, 3);
            this.numHdpHandicapFt.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numHdpHandicapFt.Name = "numHdpHandicapFt";
            this.numHdpHandicapFt.Size = new System.Drawing.Size(155, 20);
            this.numHdpHandicapFt.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(27, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "Hdp";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 28);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label6.Size = new System.Drawing.Size(31, 18);
            this.label6.TabIndex = 14;
            this.label6.Text = "Price";
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOk.Location = new System.Drawing.Point(3, 246);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(215, 24);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // MatchIuooAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 273);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "MatchIuooAlert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MatchIuooAlert";
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceOverFt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHdpOverFt)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceOverFh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHdpOverFh)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceHandicapFt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHdpHandicapFt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.NumericUpDown numPriceOverFt;
        private System.Windows.Forms.NumericUpDown numHdpOverFt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown numPriceOverFh;
        private System.Windows.Forms.NumericUpDown numHdpOverFh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.NumericUpDown numPriceHandicapFt;
        private System.Windows.Forms.NumericUpDown numHdpHandicapFt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOk;


    }
}