namespace LiveBetApp.Forms
{
    partial class ExportInspectorForm
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
            this.btnExport = new System.Windows.Forms.Button();
            this.dtFilterFrom = new System.Windows.Forms.DateTimePicker();
            this.dtFilterTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(139, 64);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dtFilterFrom
            // 
            this.dtFilterFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFilterFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtFilterFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFilterFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFilterFrom.Location = new System.Drawing.Point(54, 10);
            this.dtFilterFrom.Name = "dtFilterFrom";
            this.dtFilterFrom.Size = new System.Drawing.Size(160, 21);
            this.dtFilterFrom.TabIndex = 2;
            // 
            // dtFilterTo
            // 
            this.dtFilterTo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFilterTo.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtFilterTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFilterTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFilterTo.Location = new System.Drawing.Point(54, 37);
            this.dtFilterTo.Name = "dtFilterTo";
            this.dtFilterTo.Size = new System.Drawing.Size(160, 21);
            this.dtFilterTo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "To";
            // 
            // ExportInspectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 100);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtFilterTo);
            this.Controls.Add(this.dtFilterFrom);
            this.Controls.Add(this.btnExport);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(244, 139);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 139);
            this.Name = "ExportInspectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExportInspectorForm";
            this.Load += new System.EventHandler(this.ExportInspectorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DateTimePicker dtFilterFrom;
        private System.Windows.Forms.DateTimePicker dtFilterTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}