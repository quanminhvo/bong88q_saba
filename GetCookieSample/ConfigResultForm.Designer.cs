namespace GetCookieSample
{
    partial class ConfigResultForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtGetSidUrl = new System.Windows.Forms.TextBox();
            this.lblCookie = new System.Windows.Forms.Label();
            this.txtCookie = new System.Windows.Forms.TextBox();
            this.lblBong88Url = new System.Windows.Forms.Label();
            this.txtBong88Url = new System.Windows.Forms.TextBox();
            this.lblGetSidUrl = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtGetSidUrl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCookie, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtCookie, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblBong88Url, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtBong88Url, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblGetSidUrl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(621, 100);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // txtGetSidUrl
            // 
            this.txtGetSidUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGetSidUrl.Location = new System.Drawing.Point(133, 3);
            this.txtGetSidUrl.Name = "txtGetSidUrl";
            this.txtGetSidUrl.ReadOnly = true;
            this.txtGetSidUrl.Size = new System.Drawing.Size(485, 20);
            this.txtGetSidUrl.TabIndex = 1;
            // 
            // lblCookie
            // 
            this.lblCookie.AutoSize = true;
            this.lblCookie.Location = new System.Drawing.Point(3, 71);
            this.lblCookie.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblCookie.Name = "lblCookie";
            this.lblCookie.Size = new System.Drawing.Size(39, 13);
            this.lblCookie.TabIndex = 0;
            this.lblCookie.Text = "cookie";
            // 
            // txtCookie
            // 
            this.txtCookie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCookie.Location = new System.Drawing.Point(133, 69);
            this.txtCookie.Name = "txtCookie";
            this.txtCookie.ReadOnly = true;
            this.txtCookie.Size = new System.Drawing.Size(485, 20);
            this.txtCookie.TabIndex = 1;
            // 
            // lblBong88Url
            // 
            this.lblBong88Url.AutoSize = true;
            this.lblBong88Url.Location = new System.Drawing.Point(3, 38);
            this.lblBong88Url.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblBong88Url.Name = "lblBong88Url";
            this.lblBong88Url.Size = new System.Drawing.Size(56, 13);
            this.lblBong88Url.TabIndex = 0;
            this.lblBong88Url.Text = "bong88Url";
            // 
            // txtBong88Url
            // 
            this.txtBong88Url.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBong88Url.Location = new System.Drawing.Point(133, 36);
            this.txtBong88Url.Name = "txtBong88Url";
            this.txtBong88Url.ReadOnly = true;
            this.txtBong88Url.Size = new System.Drawing.Size(485, 20);
            this.txtBong88Url.TabIndex = 1;
            // 
            // lblGetSidUrl
            // 
            this.lblGetSidUrl.AutoSize = true;
            this.lblGetSidUrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGetSidUrl.Location = new System.Drawing.Point(3, 5);
            this.lblGetSidUrl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblGetSidUrl.Name = "lblGetSidUrl";
            this.lblGetSidUrl.Size = new System.Drawing.Size(124, 13);
            this.lblGetSidUrl.TabIndex = 0;
            this.lblGetSidUrl.Text = "getSidUrl";
            // 
            // ConfigResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 100);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConfigResultForm";
            this.Text = "CookieResultForm";
            this.Load += new System.EventHandler(this.ConfigResultForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblBong88Url;
        private System.Windows.Forms.Label lblGetSidUrl;
        private System.Windows.Forms.TextBox txtGetSidUrl;
        private System.Windows.Forms.Label lblCookie;
        private System.Windows.Forms.TextBox txtCookie;
        private System.Windows.Forms.TextBox txtBong88Url;
    }
}