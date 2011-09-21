namespace BaconBuilder.View
{
	partial class Preview
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.picboxQRCode = new System.Windows.Forms.PictureBox();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.picboxMap = new System.Windows.Forms.PictureBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxMap)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.splitter4);
            this.panel1.Controls.Add(this.picboxMap);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.browser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(608, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 376);
            this.panel1.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.picboxQRCode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(616, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 376);
            this.panel3.TabIndex = 12;
            // 
            // picboxQRCode
            // 
            this.picboxQRCode.BackColor = System.Drawing.Color.White;
            this.picboxQRCode.Location = new System.Drawing.Point(0, 0);
            this.picboxQRCode.Margin = new System.Windows.Forms.Padding(3, 3, 3, 13);
            this.picboxQRCode.Name = "picboxQRCode";
            this.picboxQRCode.Size = new System.Drawing.Size(200, 150);
            this.picboxQRCode.TabIndex = 10;
            this.picboxQRCode.TabStop = false;
            // 
            // splitter4
            // 
            this.splitter4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter4.Enabled = false;
            this.splitter4.Location = new System.Drawing.Point(608, 0);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(8, 376);
            this.splitter4.TabIndex = 11;
            this.splitter4.TabStop = false;
            // 
            // picboxMap
            // 
            this.picboxMap.BackColor = System.Drawing.Color.White;
            this.picboxMap.BackgroundImage = global::BaconBuilder.Properties.Resources.map;
            this.picboxMap.Dock = System.Windows.Forms.DockStyle.Left;
            this.picboxMap.Location = new System.Drawing.Point(308, 0);
            this.picboxMap.Margin = new System.Windows.Forms.Padding(3, 3, 3, 23);
            this.picboxMap.MaximumSize = new System.Drawing.Size(300, 360);
            this.picboxMap.MinimumSize = new System.Drawing.Size(300, 60);
            this.picboxMap.Name = "picboxMap";
            this.picboxMap.Size = new System.Drawing.Size(300, 360);
            this.picboxMap.TabIndex = 9;
            this.picboxMap.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter2.Enabled = false;
            this.splitter2.Location = new System.Drawing.Point(300, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(8, 376);
            this.splitter2.TabIndex = 10;
            this.splitter2.TabStop = false;
            // 
            // browser
            // 
            this.browser.AllowWebBrowserDrop = false;
            this.browser.Dock = System.Windows.Forms.DockStyle.Left;
            this.browser.Location = new System.Drawing.Point(0, 0);
            this.browser.MaximumSize = new System.Drawing.Size(300, 360);
            this.browser.MinimumSize = new System.Drawing.Size(300, 360);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(300, 360);
            this.browser.TabIndex = 8;
            this.browser.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // Preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(831, 376);
            this.Controls.Add(this.panel1);
            this.Name = "Preview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preview";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picboxQRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.PictureBox picboxQRCode;
		private System.Windows.Forms.Splitter splitter4;
		private System.Windows.Forms.PictureBox picboxMap;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.WebBrowser browser;
	}
}