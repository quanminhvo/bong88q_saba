namespace LiveBetApp.Forms.BetFirstHalfByFulltime
{
    partial class AllBetFirstHalfByFulltimeForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvRunning = new System.Windows.Forms.DataGridView();
            this.dgvFinished = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinished)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(889, 559);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvRunning);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(889, 274);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Running";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvFinished);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(889, 281);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Finished";
            // 
            // dgvRunning
            // 
            this.dgvRunning.AllowUserToAddRows = false;
            this.dgvRunning.AllowUserToDeleteRows = false;
            this.dgvRunning.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRunning.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRunning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRunning.Location = new System.Drawing.Point(3, 16);
            this.dgvRunning.Name = "dgvRunning";
            this.dgvRunning.ReadOnly = true;
            this.dgvRunning.RowHeadersVisible = false;
            this.dgvRunning.Size = new System.Drawing.Size(883, 255);
            this.dgvRunning.TabIndex = 1;
            this.dgvRunning.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRunning_CellClick);
            // 
            // dgvFinished
            // 
            this.dgvFinished.AllowUserToAddRows = false;
            this.dgvFinished.AllowUserToDeleteRows = false;
            this.dgvFinished.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFinished.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFinished.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFinished.Location = new System.Drawing.Point(3, 16);
            this.dgvFinished.Name = "dgvFinished";
            this.dgvFinished.ReadOnly = true;
            this.dgvFinished.RowHeadersVisible = false;
            this.dgvFinished.Size = new System.Drawing.Size(883, 262);
            this.dgvFinished.TabIndex = 1;
            this.dgvFinished.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFinished_CellClick);
            // 
            // AllBetFirstHalfByFulltimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 559);
            this.Controls.Add(this.splitContainer1);
            this.Name = "AllBetFirstHalfByFulltimeForm";
            this.Text = "AllBetFirstHalfByFulltimeForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AllBetFirstHalfByFulltimeForm_FormClosing);
            this.Load += new System.EventHandler(this.AllBetFirstHalfByFulltimeForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinished)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvRunning;
        private System.Windows.Forms.DataGridView dgvFinished;
    }
}