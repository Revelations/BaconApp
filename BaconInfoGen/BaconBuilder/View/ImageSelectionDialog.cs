using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaconBuilder.View
{
	public partial class ImageSelectionDialog : Form
	{
		OpenFileDialog openFileDialog;
		const string bmp = "*.bmp";
		const string gif = "*.gif";
		const string[] jpeg = new [] {"*.jpg","*.jpe","*.jpg;"};

		public ImageSelectionDialog()
		{
			InitializeComponent();

			openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "All Image Files|*.bmp; *.gif; *.jpg; *.jpe; *.jpg; *.png|Bitmap Files|*.bmp|";

			this.CancelButton = btnCancel;
			this.AcceptButton = btnOK;

			this.btnBrowse.Click += new System.EventHandler(this.btnBrowser_Click);
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		}

		private void btnBrowser_Click(object sender, EventArgs e)
		{

		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Tag = txtImageURL.Text;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			
		}
	}
}
