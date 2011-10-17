using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Common;

namespace BaconBuilder.Model
{
	public class LogGenerator
	{
		public enum Purpose
		{
			Game,
			Info
		}

		private static readonly string[] ContentExtensions = new[] {".html", ".css", ".jpg", ".bmp", ".jpeg", ".jpe", ".gif", ".png", ".mp3", ".js", ".svg", ""};
		private static readonly string[] GameExtensions = new[] {".ques"};
		private const string Deli = "|";

		public static string FilePath
		{
			get { return Resources.ContentDirectory + "log.txt"; }
		}

		public static string QuizLogFilePath
		{
			get { return Resources.GameDirectory + "gamelog.txt"; }
		}

		public static void CreateGameLog()
		{
			Console.WriteLine("Creating game log...");
			using (var writer = new StreamWriter(new FileStream(QuizLogFilePath, FileMode.Create)))
			{
				var data = new List<object>();
				GetFiles(data, Purpose.Game);
				//Write out to stream.
				writer.Write(String.Join(Deli, data));
			}
			Console.WriteLine("Game log created.");
		}

		public static void CreateContentLog()
		{
			using (var writer = new StreamWriter(new FileStream(FilePath, FileMode.Create)))
			{
				var data = new List<object>();
				// Loop through each file in the directory.
				GetFiles(data, Purpose.Info);
				//Write out to stream.
				writer.Write(String.Join(Deli, data));
			}
		}

		public static void GetFiles(List<object> data, Purpose purpose)
		{
			DirectoryInfo dir = null;
			string[] allowed = null;

			switch (purpose)
			{
				case Purpose.Info:
					dir = new DirectoryInfo(Resources.ContentDirectory);
					allowed = ContentExtensions;
					break;
				case Purpose.Game:
					dir = new DirectoryInfo(Resources.GameDirectory);
					allowed = GameExtensions;
					break;
				default:
					return;
			}

			foreach (var f in dir.GetFiles().Where(f => allowed.Contains(f.Extension)))
			{
				Console.Write(purpose + " filename = " + f.Name);
				// append the filename, and filesize to the builder
				data.Add(f.Name);
				Console.WriteLine(" length = " +f.Length);
				data.Add(f.Length);
			}
		}
	}
}
