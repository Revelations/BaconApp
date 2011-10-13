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
			using (StreamWriter writer = new StreamWriter(new FileStream(FilePath, FileMode.Create)))
			{
				StringBuilder sb = new StringBuilder();
				//List<string> data = new List<string>();
				// loop thru each file in the directory
				foreach (var f in FtpHelper.HtmlDirectory.GetFiles())
				{
					// if file extension is permitted
					if (Allowed.Contains(f.Extension))
					{
						// append the filename, delimiter, filesize, and delimiter to the builder
						sb.Append(f.Name).Append(Deli).Append(f.Length).Append(Deli);
					}
				}
				//truncate the last delimiter
				sb.Remove(sb.Length - 1, 1);
				//Write out to stream.
				writer.Write(sb);

			}
		}

		public static void createlog2()
		{
			using (var writer = new StreamWriter(new FileStream(FilePath, FileMode.Create)))
			{
				//StringBuilder sb = new StringBuilder();
				var data = new ArrayList();
				// loop thru each file in the directory
				GetFiles(data);
				//Write out to stream.
				writer.Write(String.Join(Deli, data));

			}
		}

		public static void GetFiles(ArrayList data)
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
