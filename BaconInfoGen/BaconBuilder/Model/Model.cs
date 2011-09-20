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

		private string contents;
		
		internal string GetPageCode()
		{
			builder.AddContent(Contents);
			return builder.ToHtml();
		}

		public string Contents
		{
			get
			{
				return contents;
			}
			set
			{
				contents = value;
			}
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

		/// <summary>
		/// Selected RTF in the content editor.
		/// </summary>
		public string SelectedRtf { get; set; }
	}
}
