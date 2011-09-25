using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace BaconBuilder.Model
{
	public class BaconModel : IModel
	{
		private const string HtmlExtension = ".html";
		private static readonly string HtmlDirectory = "C:/Users/" + Environment.UserName + "/test/";

		private const string NewHtmlFileName = "New File";
		private DirectoryInfo _directory = new DirectoryInfo(HtmlDirectory);
		private readonly Dictionary<string, string> _fileContents = new Dictionary<string, string>();
		private string _currentFileWithExtensionName;

//		private FtpDownloader _ftpDownloader;
//		private FtpUploader _ftpUploader;

		/// <summary>
		/// Get or set the image url, obtained from an image selection dialog.
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// Gets a new file name not already present in the Html directory.
		/// 
		/// Checks for existing files present in the Html directory with the new file name. Iterates and
		/// appends integer values to the filename until it finds on that is unused.
		/// </summary>
		/// <returns>Unused filname with the lowest possible appended integer.</returns>
		public static string GetLowestUnusedNewFileName()
		{
			var name = HtmlDirectory + NewHtmlFileName;
			var fileName = name + HtmlExtension;
			// Otherwise iterate to find the lowest number available to append.
			for (int i = 2; File.Exists(fileName); i++)
			{
				fileName = name + i.ToString(" 0#") + HtmlExtension;
			}
			return fileName;
		}

		public void RemoveFile(string fileName)
		{
			_fileContents.Remove(fileName);
			File.Delete(HtmlDirectory + fileName);
			_currentFileWithExtensionName = null;
		}

		public void CreateNewFile(string fileName)
		{
			Console.WriteLine(@"Creating new file");
			File.WriteAllText(GetLowestUnusedNewFileName(), Properties.Resources.Blank);
		}

		public void RenameFile(string oldName, string newName)
		{
			var oldHtmlName = oldName + HtmlExtension;
			var newHtmlName = newName + HtmlExtension;

			var oldInfo = new FileInfo(HtmlDirectory + oldHtmlName);
			var newInfo = new FileInfo(HtmlDirectory + newHtmlName);
			Console.WriteLine(@"Renaming file {0} to {1}", oldHtmlName, newHtmlName);
			
			if (newInfo.Exists)
			{
				throw new IOException(string.Format("Cannot rename {0} to {1}: File already exists", oldHtmlName, newHtmlName));
			}
			oldInfo.MoveTo(newInfo.FullName);
			_fileContents.Add(newHtmlName, _fileContents[oldHtmlName]);
			_fileContents.Remove(oldHtmlName);
			_currentFileWithExtensionName = null;
		}

		public string CurrentFileWithExtension
		{
			get { return _currentFileWithExtensionName; }
			set { _currentFileWithExtensionName = value; }
		}

		public string CurrentFile
		{
			get { return StripExtension(_currentFileWithExtensionName); }
		}

		/// <summary>
		/// Reloads the files into memory.
		/// </summary>
		/// <returns></returns>
		public void LoadFiles()
		{
			if (!_directory.Exists) _directory.Create();

			var f = _directory.GetFiles("*.html");
			_fileContents.Clear();
			foreach (var file in f)
			{
				_fileContents.Add(file.Name, File.ReadAllText(file.FullName));
			}
		}

		public Dictionary<string, string>.KeyCollection FileNames
		{
			get { return _fileContents.Keys; }
		}

		public string ReadFile(string p)
		{
			return _fileContents[p];
		}

		public string CurrentContents
		{
			get { return _fileContents[_currentFileWithExtensionName]; }
			set { _fileContents[_currentFileWithExtensionName] = value; }
		}

		public Image QrCode(string file)
		{
			throw new NotImplementedException();
		}

		public void ChangeDirectory(string newDir)
		{
			_directory = new DirectoryInfo(newDir);
		}

		public static string StripExtension(string name)
		{
			if (name != null && name.EndsWith(HtmlExtension, StringComparison.Ordinal))
			{
				return name.Remove(name.LastIndexOf(HtmlExtension));
			}
			return name;
		}
	}
}
