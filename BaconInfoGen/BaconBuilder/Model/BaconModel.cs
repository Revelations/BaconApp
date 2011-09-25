using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using BaconBuilder.Properties;

namespace BaconBuilder.Model
{
	public class BaconModel : IModel
	{
		private const string HtmlExtension = ".html";

		private const string NewHtmlFileName = "New File";

		private readonly Dictionary<string, string> _fileContents = new Dictionary<string, string>();
		private string _currentFileNameWithExtensionName;
		private DirectoryInfo _directory = new DirectoryInfo(Resources.HtmlDirectory);

//		private FtpDownloader _ftpDownloader;
//		private FtpUploader _ftpUploader;

		/// <summary>
		/// Get or set the image url, obtained from an image selection dialog.
		/// </summary>
		public string ImageUrl { get; set; }

		#region IModel Members

		public void RemoveFile(string fileName)
		{
			_fileContents.Remove(fileName);
			File.Delete(Resources.HtmlDirectory + fileName);
			_currentFileNameWithExtensionName = null;
		}

		public void CreateNewFile(string fileName)
		{
			Console.WriteLine(@"Creating new file");
			File.WriteAllText(GetLowestUnusedNewFileName(), Resources.Blank);
		}

		public void RenameFile(string oldName, string newName)
		{
			string oldHtmlName = oldName + HtmlExtension;
			string newHtmlName = newName + HtmlExtension;

			var oldInfo = new FileInfo(Resources.HtmlDirectory + oldHtmlName);
			var newInfo = new FileInfo(Resources.HtmlDirectory + newHtmlName);
			Console.WriteLine(@"Renaming file {0} to {1}", oldHtmlName, newHtmlName);

			if (newInfo.Exists)
			{
				throw new IOException(string.Format("Cannot rename {0} to {1}: File already exists", oldHtmlName, newHtmlName));
			}
			oldInfo.MoveTo(newInfo.FullName);
			_fileContents.Add(newHtmlName, _fileContents[oldHtmlName]);
			_fileContents.Remove(oldHtmlName);
			_currentFileNameWithExtensionName = null;
		}

		public void SaveFile(string fileName)
		{
			File.WriteAllText(Resources.HtmlDirectory + fileName, CurrentContents);
		}

		public string CurrentFileNameWithExtension
		{
			get { return _currentFileNameWithExtensionName; }
			set { _currentFileNameWithExtensionName = value; }
		}

		/// <summary>
		/// Gets or sets the current filename without extension.
		/// </summary>
		public string CurrentFileName
		{
			get { return StripExtension(_currentFileNameWithExtensionName); }
			set { _currentFileNameWithExtensionName = value + HtmlExtension; }
		}

		/// <summary>
		/// Reloads the files into memory.
		/// </summary>
		/// <returns></returns>
		public void LoadFiles()
		{
			if (!_directory.Exists) _directory.Create();

			FileInfo[] f = _directory.GetFiles("*.html");
			_fileContents.Clear();
			foreach (FileInfo file in f)
			{
				_fileContents.Add(file.Name, File.ReadAllText(file.FullName));
			}
		}

		/// <summary>
		/// Returns a collection of the filenames opened in memory.
		/// </summary>
		public Dictionary<string, string>.KeyCollection FileNames
		{
			get { return _fileContents.Keys; }
		}

		/// <summary>
		/// Gets or sets the contents of the currently opened file.
		/// </summary>
		public string CurrentContents
		{
			get { return _fileContents[_currentFileNameWithExtensionName]; }
			set { _fileContents[_currentFileNameWithExtensionName] = value; }
		}

		public Image QrCode(string file)
		{
			throw new NotImplementedException();
		}

		#endregion

		/// <summary>
		/// Gets a new file fileName not already present in the Html directory.
		/// 
		/// Checks for existing files present in the Html directory with the new file fileName. Iterates and
		/// appends integer values to the filename until it finds on that is unused.
		/// </summary>
		/// <returns>Unused filname with the lowest possible appended integer.</returns>
		public static string GetLowestUnusedNewFileName()
		{
			string name = Resources.HtmlDirectory + NewHtmlFileName;
			string result = name + HtmlExtension;
			// Otherwise iterate to find the lowest number available to append.
			for (int i = 2; File.Exists(result); i++)
			{
				result = name + i.ToString(" 0#") + HtmlExtension;
			}
			return result;
		}

		public void ChangeDirectory(string newDir)
		{
			_directory = new DirectoryInfo(newDir);
		}

		/// <summary>
		/// Removes a defined extension from filename and returns it. Does not affect null strings or other filenames.
		/// </summary>
		/// <param name="fileName">The filename to strip the extension from.</param>
		/// <returns>The filename without the predefined extension</returns>
		public static string StripExtension(string fileName)
		{
			if (fileName != null && fileName.EndsWith(HtmlExtension, StringComparison.Ordinal))
			{
				return fileName.Remove(fileName.LastIndexOf(HtmlExtension));
			}
			return fileName;
		}
	}
}