namespace LiveBetApp.Forms
{
    partial class BookmarksForm
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
            this.mainDgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.mainDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // mainDgv
            // 
            this.mainDgv.AllowUserToAddRows = false;
            this.mainDgv.AllowUserToDeleteRows = false;
            this.mainDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mainDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDgv.Location = new System.Drawing.Point(0, 0);
            this.mainDgv.Name = "mainDgv";
            this.mainDgv.ReadOnly = true;
            this.mainDgv.Size = new System.Drawing.Size(879, 433);
            this.mainDgv.TabIndex = 3;
            this.mainDgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainDgv_CellClick);
            this.mainDgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainDgv_CellDoubleClick);
            // 
            // BookmarksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 433);
            this.Controls.Add(this.mainDgv);
            this.Name = "BookmarksForm";
            this.Text = "BookmarksForm";
            this.Load += new System.EventHandler(this.BookmarksForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainDgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView mainDgv;
    }
}