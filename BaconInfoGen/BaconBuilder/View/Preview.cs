using System;
using System.Drawing;
using System.Windows.Forms;
using BaconBuilder.Controller;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class Preview : Form, IPreviewView
	{
		private readonly IModel _model;
		private readonly PreviewController _controller;
		public Preview(IModel model)
		{
			InitializeComponent();

			_model = model;
			_controller = new PreviewController(_model, this);
			_controller.QrCode();

			browser.DocumentCompleted += browser_DocumentCompleted;
		}

		#region IPreviewView Members

		public Image QrCodeImage
		{
			get { return picboxQRCode.Image; }
			set { picboxQRCode.Image = value; }
		}

		public void SetBrowserText(string text)
		{
			if (browser.Document != null)
			{
				browser.Document.OpenNew(true);
				browser.Document.Write(text);
			}
		}

		#endregion

		private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			_controller.PreviewDocument();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}