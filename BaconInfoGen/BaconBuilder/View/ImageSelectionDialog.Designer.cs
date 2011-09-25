
namespace BaconBuilder.View
{
	partial class ImageSelectionDialog
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
			this.pnlContainer = new System.Windows.Forms.Panel();
			this.tlp = new System.Windows.Forms.TableLayoutPanel();
			this.lblImageLocation = new System.Windows.Forms.Label();
			this.txtImageURL = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.flp = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.pnlContainer.SuspendLayout();
			this.tlp.SuspendLayout();
			this.flp.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlContainer
			// 
			this.pnlContainer.Controls.Add(this.tlp);
			this.pnlContainer.Controls.Add(this.flp);
			this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContainer.Location = new System.Drawing.Point(0, 0);
			this.pnlContainer.Name = "pnlContainer";
			this.pnlContainer.Padding = new System.Windows.Forms.Padding(20);
			this.pnlContainer.Size = new System.Drawing.Size(494, 172);
			this.pnlContainer.TabIndex = 0;
			// 
			// tlp
			// 
			this.tlp.ColumnCount = 3;
			this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlp.Controls.Add(this.lblImageLocation, 0, 0);
			this.tlp.Controls.Add(this.txtImageURL, 1, 0);
			this.tlp.Controls.Add(this.comboBox1, 1, 1);
			this.tlp.Controls.Add(this.btnBrowse, 2, 0);
			this.tlp.Controls.Add(this.label1, 0, 1);
			this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlp.Location = new System.Drawing.Point(20, 20);
			this.tlp.Name = "tlp";
			this.tlp.RowCount = 3;
			this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlp.Size = new System.Drawing.Size(454, 103);
			this.tlp.TabIndex = 0;
			// 
			// lblImageLocation
			// 
			this.lblImageLocation.AutoSize = true;
			this.lblImageLocation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblImageLocation.Location = new System.Drawing.Point(3, 0);
			this.lblImageLocation.Name = "lblImageLocation";
			this.lblImageLocation.Size = new System.Drawing.Size(83, 29);
			this.lblImageLocation.TabIndex = 0;
			this.lblImageLocation.Text = "Image Location:";
			this.lblImageLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtImageURL
			// 
			this.txtImageURL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtImageURL.Location = new System.Drawing.Point(92, 3);
			this.txtImageURL.Name = "txtImageURL";
			this.txtImageURL.Size = new System.Drawing.Size(278, 20);
			this.txtImageURL.TabIndex = 0;
			// 
			// comboBox1
			// 
			this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(92, 32);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(278, 21);
			this.comboBox1.TabIndex = 2;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(376, 3);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "Options:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// flp
			// 
			this.flp.AutoSize = true;
			this.flp.Controls.Add(this.btnCancel);
			this.flp.Controls.Add(this.btnOK);
			this.flp.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.flp.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flp.Location = new System.Drawing.Point(20, 123);
			this.flp.Name = "flp";
			this.flp.Size = new System.Drawing.Size(454, 29);
			this.flp.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(376, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(295, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// ImageSelectionDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(494, 172);
			this.Controls.Add(this.pnlContainer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ImageSelectionDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Image";
			this.pnlContainer.ResumeLayout(false);
			this.pnlContainer.PerformLayout();
			this.tlp.ResumeLayout(false);
			this.tlp.PerformLayout();
			this.flp.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlContainer;
		private System.Windows.Forms.TableLayoutPanel tlp;
		private System.Windows.Forms.Label lblImageLocation;
		private System.Windows.Forms.TextBox txtImageURL;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.FlowLayoutPanel flp;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;

	}
}