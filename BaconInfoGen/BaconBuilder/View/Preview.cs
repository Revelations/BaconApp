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
		private readonly QrCodeGenerator _qrGen;

		public Preview(IModel model)
		{
			_model = model;
			InitializeComponent();
			_qrGen = new QrCodeGenerator();
			QrCode = _qrGen.GenerateCode(_model.CurrentFileName);

			browser.DocumentCompleted += browser_DocumentCompleted;
		}

		#region IPreviewView Members

		public Image QrCode
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
			var previewController = new PreviewController(_model, this);
			previewController.PreviewDocument();

			browser.DocumentCompleted -= browser_DocumentCompleted;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}