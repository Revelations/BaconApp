using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BaconBuilder.Model.Ftp;

namespace BaconBuilder.Model
{
	public class LogGenerator
	{
		private static readonly string[] Allowed = new[] {".html", ".css", ".jpg", ".bmp", ".jpeg", ".jpe", ".gif", ".mp3", ".js", ".svg", ""};
		private const string Deli = "|";

		public static string FilePath
		{
			get { return FtpHelper.HtmlDirectory + "log.txt"; }
		}
		public static void createlog()
		{
			using (var writer = new StreamWriter(new FileStream(FilePath, FileMode.Create)))
			{
				var data = new ArrayList();
				// Loop through each file in the directory.
				GetFiles(data);
				//Write out to stream.
				writer.Write(String.Join(Deli, data));
			}
		}

		public static void GetFiles(IList data)
		{
			foreach (var f in FtpHelper.HtmlDirectory.GetFiles().Where(f => Allowed.Contains(f.Extension)))
			{
				// append the filename, and filesize to the builder
				data.Add(f.Name);
				data.Add(f.Length);
			}
		}
	}
}
