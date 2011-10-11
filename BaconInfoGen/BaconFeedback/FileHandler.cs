using System;
using System.Collections.Generic;
using System.IO;

namespace BaconFeedback
{
	/// <summary>
	/// 
	/// </summary>
	public class FileHandler
	{
		private const string _feedbackExtension = ".fbk";
		private static readonly string Dir = "C:/Users/" + Environment.UserName + "/FeedbackTest/";

		// We all recognize this, amirite?
		private static string FeedbackDirectory
		{
			get
			{
				if (!Directory.Exists(Dir))
					Directory.CreateDirectory(Dir);
				return Dir;
			}
		}

		// Feedback file extension.

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

				if (f.Extension == _feedbackExtension)
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
	}
}