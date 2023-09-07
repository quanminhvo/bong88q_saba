
namespace LiveBetApp.Forms
{
    partial class ProductIdHistoryDetailForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvStatusHistoryInsert = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatusHistoryInsert)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStatusHistoryInsert
            // 
            this.dgvStatusHistoryInsert.AllowUserToAddRows = false;
            this.dgvStatusHistoryInsert.AllowUserToDeleteRows = false;
            this.dgvStatusHistoryInsert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStatusHistoryInsert.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStatusHistoryInsert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStatusHistoryInsert.Location = new System.Drawing.Point(0, 0);
            this.dgvStatusHistoryInsert.Margin = new System.Windows.Forms.Padding(2);
            this.dgvStatusHistoryInsert.Name = "dgvStatusHistoryInsert";
            this.dgvStatusHistoryInsert.ReadOnly = true;
            this.dgvStatusHistoryInsert.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvStatusHistoryInsert.RowHeadersVisible = false;
            this.dgvStatusHistoryInsert.RowHeadersWidth = 51;
            this.dgvStatusHistoryInsert.RowTemplate.Height = 15;
            this.dgvStatusHistoryInsert.Size = new System.Drawing.Size(1020, 410);
            this.dgvStatusHistoryInsert.TabIndex = 11;
            // 
            // ProductIdHistoryDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 410);
            this.Controls.Add(this.dgvStatusHistoryInsert);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ProductIdHistoryDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductIdHistoryDetailForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductIdHistoryDetailForm_FormClosing);
            this.Load += new System.EventHandler(this.ProductIdHistoryDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatusHistoryInsert)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStatusHistoryInsert;

    }
}