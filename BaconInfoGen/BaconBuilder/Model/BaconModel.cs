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
		private static readonly string HtmlDirectory = "C:/Users/" + Environment.UserName + "/test/";

		private readonly Dictionary<string, string> _fileContents = new Dictionary<string, string>();
		private DirectoryInfo _directory = new DirectoryInfo(HtmlDirectory);

		/// <summary>
		/// Get or set the image url, obtained from an image selection dialog.
		/// </summary>
		public string ImageUrl { get; set; }

		#region IModel Members

		/// <summary>
		/// Remove the file from the wroking directory.
		/// </summary>
		/// <param name="fileName"></param>
		public void RemoveFile(string fileName)
		{
			_fileContents.Remove(fileName);
			File.Delete(HtmlDirectory + fileName);
			CurrentFileNameWithExtension = null;
		}

		/// <summary>
		/// Creates a new file with the name.
		/// </summary>
		/// <param name="fileName">Name of file</param>
		public void CreateNewFile(string fileName)
		{
			Console.WriteLine(@"Creating new file {0}", fileName);
			File.WriteAllText(GetLowestUnusedNewFileName(), Resources.Blank);
		}

		/// <summary>
		/// Renames the file to a new name.
		/// </summary>
		/// <param name="oldName">The current name.</param>
		/// <param name="newName">The new name.</param>
		public void RenameFile(string oldName, string newName)
		{
			string oldHtmlName = oldName + HtmlExtension;
			string newHtmlName = newName + HtmlExtension;

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
			CurrentFileNameWithExtension = null;
		}

		/// <summary>
		/// Saves the file.
		/// </summary>
		/// <param name="fileName">The filename of the file to save.</param>
		public void SaveFile(string fileName)
		{
			File.WriteAllText(HtmlDirectory + fileName, CurrentContents);
		}

		/// <summary>
		/// Currently loaded filename INcluding extension.
		/// </summary>
		public string CurrentFileNameWithExtension { get; set; }

		/// <summary>
		/// Currently loaded filename EXcluding extension.
		/// </summary>
		public string CurrentFileName
		{
			get { return StripExtension(CurrentFileNameWithExtension); }
			set { CurrentFileNameWithExtension = value + HtmlExtension; }
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
		/// Get the collection of filenames.
		/// </summary>
		public Dictionary<string, string>.KeyCollection FileNames
		{
			get { return _fileContents.Keys; }
		}

		/// <summary>
		/// Contents of currently loaded file.
		/// </summary>
		public string CurrentContents
		{
			get { return _fileContents[CurrentFileNameWithExtension]; }
			set { _fileContents[CurrentFileNameWithExtension] = value; }
		}

		public string AudioUrl { get; set; }

		public Image QrCode(string file)
		{
			return new QrCodeGenerator().GenerateCode(file);
		}

		/// <summary>
		/// Change the working directory.
		/// </summary>
		/// <param name="newDir">The name of the new working directory.</param>
		public void ChangeDirectory(string newDir)
		{
			_directory = new DirectoryInfo(newDir);
		}

		#endregion

		/// <summary>
		/// Gets a new file fileName not already present in the Html directory.
		/// 
		/// Checks for existing files present in the Html directory with the new file fileName. Iterates and
		/// appends integer values to the filename until it finds on that is unused.
		/// </summary>
		/// <returns>Unused filname with the lowest possible appended integer.</returns>
		private static string GetLowestUnusedNewFileName()
		{
			string name = HtmlDirectory + NewHtmlFileName;
			string result = name + HtmlExtension;
			// Otherwise iterate to find the lowest number available to append.
			for (int i = 2; File.Exists(result); i++)
			{
				result = name + i.ToString(" 0#") + HtmlExtension;
			}
			return result;
		}

		/// <summary>
		/// Removes a defined extension from filename and returns it. Does not affect null strings or other filenames.
		/// </summary>
		/// <param name="fileName">The filename to strip the extension from.</param>
		/// <returns>The filename without the predefined extension</returns>
		private static string StripExtension(string fileName)
		{
			if (fileName != null && fileName.EndsWith(HtmlExtension, StringComparison.Ordinal))
			{
				return fileName.Remove(fileName.LastIndexOf(HtmlExtension));
			}
			return fileName;
		}
	}
}