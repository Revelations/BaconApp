using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using mshtml;

namespace BaconBuilder
{
	/// <summary>
	/// An HTML Degenerator
	/// </summary>
	public class HtmlDegen
	{
		WebBrowser browser; // TODO: (MVC) We'll use the view browser later.
		/// <summary>
		/// Get the contents of the body of the page
		/// </summary>
		public List<string> Body
		{
			get
			{
				return new List<string>();
			}

			set 
			{
			}
		}

		public HtmlDegen()
		{
			browser = new WebBrowser();
			browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
		}


		/// <summary>
		/// Once the page loads, we can use the DOM
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			IHTMLDocument2 HtmlDocument = (IHTMLDocument2)browser.Document.DomDocument;
			IHTMLElementCollection elements = HtmlDocument.all;
			HTMLBody body = (HTMLBody)HtmlDocument.body;
			//Body = body.;

		}

		/// <summary>
		/// Loads the html page.
		/// </summary>
		/// <param name="p"></param>
		internal void ReadHtml(string p)
		{
			string url = "file:///C:/Users/Noriko/Desktop/bacon/test.html";
			browser.Navigate(url);
		}
	}
}
