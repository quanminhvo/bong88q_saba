namespace LiveBetApp.Forms.Setting
{
    partial class BackupScheduleForm
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
            this.dtpTimeToBackup = new System.Windows.Forms.DateTimePicker();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpTimeToBackup
            // 
            this.dtpTimeToBackup.CustomFormat = "hh:mm tt";
            this.dtpTimeToBackup.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtpTimeToBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTimeToBackup.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeToBackup.Location = new System.Drawing.Point(0, 0);
            this.dtpTimeToBackup.Name = "dtpTimeToBackup";
            this.dtpTimeToBackup.Size = new System.Drawing.Size(252, 27);
            this.dtpTimeToBackup.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(0, 33);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(252, 29);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // BackupScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 63);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dtpTimeToBackup);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackupScheduleForm";
            this.Text = "BackupScheduleForm";
            this.Load += new System.EventHandler(this.BackupScheduleForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTimeToBackup;
        private System.Windows.Forms.Button btnOk;
    }
}