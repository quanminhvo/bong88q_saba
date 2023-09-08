namespace LiveBetApp.Forms
{
    partial class HandicapHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvFt = new System.Windows.Forms.DataGridView();
            this.dgvFh = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFh)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgvFh, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvFt, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1449, 935);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvFt
            // 
            this.dgvFt.AllowUserToAddRows = false;
            this.dgvFt.AllowUserToDeleteRows = false;
            this.dgvFt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFt.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFt.Location = new System.Drawing.Point(4, 4);
            this.dgvFt.Margin = new System.Windows.Forms.Padding(4);
            this.dgvFt.Name = "dgvFt";
            this.dgvFt.ReadOnly = true;
            this.dgvFt.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvFt.RowHeadersVisible = false;
            this.dgvFt.RowHeadersWidth = 51;
            this.dgvFt.Size = new System.Drawing.Size(716, 927);
            this.dgvFt.TabIndex = 9;
            // 
            // dgvFh
            // 
            this.dgvFh.AllowUserToAddRows = false;
            this.dgvFh.AllowUserToDeleteRows = false;
            this.dgvFh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFh.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFh.Location = new System.Drawing.Point(728, 4);
            this.dgvFh.Margin = new System.Windows.Forms.Padding(4);
            this.dgvFh.Name = "dgvFh";
            this.dgvFh.ReadOnly = true;
            this.dgvFh.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvFh.RowHeadersVisible = false;
            this.dgvFh.RowHeadersWidth = 51;
            this.dgvFh.Size = new System.Drawing.Size(717, 927);
            this.dgvFh.TabIndex = 10;
            // 
            // HandicapHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1449, 935);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HandicapHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HandicapHistory";
            this.Load += new System.EventHandler(this.HandicapHistory_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvFh;
        private System.Windows.Forms.DataGridView dgvFt;
    }
}