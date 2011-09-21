using System;
using System.Windows.Forms;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class Preview : Form
	{
		public Preview(string html, int x, int y)
		{
			InitializeComponent();

			browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
		}

		private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			browser.Document.OpenNew(true);
			browser.Document.Write("<html><body><p>Hello</p></body></html>");

			((WebBrowser)sender).DocumentCompleted -= browser_DocumentCompleted;
		}


	}
}
