using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common;

namespace BaconFeedback
{
	/// <summary>
	/// 
	/// </summary>
	public class FileHandler
	{
		private const string FeedbackExtension = ".fbk";

		private static readonly string FeedbackDirectory = Resources.FeedbackDirectory;

		public static IEnumerable<string> GetSubfolders()
		{
			String[] s = Directory.GetDirectories(FeedbackDirectory);
			for (int i = 0; i < s.Length; i++)
			{
				s[i] = GetShortDirectoryName(s[i]);
			}

			return s;
		}

		public static IEnumerable<string> GetFeedbackFiles(string directory)
		{
			var result = new List<string>();

			foreach (string s in Directory.GetFiles(FeedbackDirectory + directory))
			{
				var f = new FileInfo(s);

				if (f.Extension == FeedbackExtension)
				{
					result.Add(f.Name);
				}
			}

			return result;
		}

		private static string GetShortDirectoryName(string directory)
		{
			string[] split = directory.Split('/');
			return split[split.Length - 1];
		}

		public static string GetCreationDate(string path)
		{
			var f = new FileInfo(FeedbackDirectory + path);
			return f.CreationTime.ToString();
		}

		public static string[] GetFeedbackContents(string path)
		{
			return File.ReadAllLines(FeedbackDirectory + path);
		}

		public static void DeleteFolder(string folder)
		{
			string[] files = Directory.GetFiles(FeedbackDirectory + folder);
			foreach (string file in files)
			{
				File.SetAttributes(file, FileAttributes.Normal);
				File.Delete(file);
			}

			Directory.Delete(FeedbackDirectory + folder);
		}

		public static void DeleteFile(string path)
		{
			Console.WriteLine(FeedbackDirectory + path);
			File.Delete(FeedbackDirectory + path);
		}

		public static void SortFiles()
		{
			DirectoryInfo d = new DirectoryInfo(FeedbackDirectory);

			foreach (FileInfo f in d.GetFiles())
			{
				string newDir = string.Format("{0}{1} - {2}/", FeedbackDirectory, f.LastWriteTime.Month.ToString("##"),
				                          f.LastWriteTime.Year);
				if (!Directory.Exists(newDir))
					Directory.CreateDirectory(newDir);
				if (!File.Exists(newDir + f.Name))
					f.MoveTo(newDir + f.Name);
			}
		}

		public static void Export(string contents, string fileName)
		{
			File.WriteAllText(fileName, contents);
			System.Diagnostics.Process.Start(fileName);
		}
	}
}