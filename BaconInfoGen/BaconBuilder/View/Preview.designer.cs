
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
		[System.Diagnostics.DebuggerNonUserCode]
		[System.CodeDom.Compiler.GeneratedCode("Winform Designer", "VS2010 SP1")]
		private void InitializeComponent()
		{
            this.btnClose = new System.Windows.Forms.Button();
            this.picboxQRCode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picboxQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(12, 228);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // picboxQRCode
            // 
            this.picboxQRCode.BackColor = System.Drawing.Color.White;
            this.picboxQRCode.Location = new System.Drawing.Point(12, 12);
            this.picboxQRCode.Margin = new System.Windows.Forms.Padding(3, 3, 3, 13);
            this.picboxQRCode.Name = "picboxQRCode";
            this.picboxQRCode.Size = new System.Drawing.Size(200, 200);
            this.picboxQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picboxQRCode.TabIndex = 12;
            this.picboxQRCode.TabStop = false;
            // 
            // Preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(224, 263);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.picboxQRCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Preview";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QR Code Preview";
            ((System.ComponentModel.ISupportInitialize)(this.picboxQRCode)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox picboxQRCode;

    }
}