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
			QrCode = _qrGen.GenerateCode(_model.CurrentFile);

			browser.DocumentCompleted += browser_DocumentCompleted;
		}

		private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{

			PreviewController previewController = new PreviewController(_model, this);
			previewController.PreviewDocument();

			browser.DocumentCompleted -= browser_DocumentCompleted;
		}

		public Image QrCode
		{
			get { return picboxQRCode.Image; }
			set { picboxQRCode.Image = value; }
		}

		//public WebBrowser Browser { get { return browser; } }

		public void BrowserText(string text)
		{
			if (browser.Document != null)
			{
				browser.Document.OpenNew(true);
				browser.Document.Write(text);
			}
		}
	}
}
