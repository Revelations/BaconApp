using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BaconBuilder.Model
{
	class LogGenerator
	{
		public static void createlog()
		{
			string HtmlDirectory = "C:/Users/" + Environment.UserName + "/test/";
			FileStream log = new FileStream(HtmlDirectory + "log.txt", FileMode.Create);
			StreamWriter writer = new StreamWriter(log);

			/*
			loop thru each file in the directory
			get filesname
			get size in bytes
				if file is not named "log.txt"
				append the filename to the stream
				append , to the stream
				append the filesize to the stream
			endloop
			*/
			string[] allowed = { ".html", ".css", ".jpg", ".bmp", ".jpeg", ".jpe", ".gif", ".mp3", ".js", ".svg", "" };
			StringBuilder sb = new StringBuilder();
			char deli = '|';
			foreach (var f in new DirectoryInfo(HtmlDirectory).GetFiles())
			{
				if (allowed.Contains(f.Extension))
				{
					sb.Append(f.Name).Append(deli);
					sb.Append(f.Length).Append(deli);
				}
			}
			sb.Remove(sb.Length - 1, 1);
			writer.Write(sb);

			writer.Close();
		}
	}
}
