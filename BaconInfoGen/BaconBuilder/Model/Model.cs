using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BaconBuilder.Model
{
	public class BaconModel
	{
		HtmlGenerator builder = new HtmlGenerator("example");
		FileHandler fh = new FileHandler(".html");
		HtmlDegen degen = new HtmlDegen();

	    public string Contents { get; set; }
		
		internal string GetPageCode()
		{
			builder.AddContent(Contents);
			return builder.ToHtml();
		}

		internal IEnumerable<string> OpenFile(string p)
		{
			FileInfo info = new FileInfo(p);
			fh.LoadFile(info);
			return fh.GetFileFromMemory(info);
		}

		internal List<string> GetBody(string url)
		{
			degen.ReadHtml(url);
			return degen.Body;
		}

		/// <summary>
		/// Get or set the image url, obtained from an image selection dialog.
		/// </summary>
		public string ImageUrl { get; set; }
	}
}
