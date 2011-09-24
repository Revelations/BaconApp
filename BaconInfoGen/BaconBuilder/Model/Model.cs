using System;
using System.Collections.Generic;
using System.IO;

namespace BaconBuilder.Model
{
	public class BaconModel : IModel
	{
		private const string HtmlExtension = ".html";
		private static readonly string HtmlDirectory = "C:/Users/" + Environment.UserName + "/test/";

		private readonly string _blankFilePath = HtmlDirectory + "Blank" + HtmlExtension;
		private const string BlankContent =
@"<!DOCTYPE HTML>
<html>
<head>
<link href=""style.css"" />
<title></title>
</head>
<body>
</body>
</html>";

		private const string NewHtmlFileName = "New File";
		readonly FileHandler _fh = new FileHandler(".html");

/*
		private DirectoryInfo _directory = new DirectoryInfo(HtmlDirectory);
*/

	    public string Contents { get; set; }

		internal IEnumerable<string> OpenFile(string p)
		{
			var info = new FileInfo(p);
			_fh.LoadFile(info);
			return _fh.GetFileFromMemory(info);
		}

		/// <summary>
		/// Get or set the image url, obtained from an image selection dialog.
		/// </summary>
		public string ImageUrl { get; set; }

		public void CreateBlankFile()
		{
			if (!File.Exists(_blankFilePath))
			{
				File.WriteAllText(_blankFilePath, BlankContent);
			}
		}

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
			throw new NotImplementedException();
		}

		public void CreateNewFile(string fileName)
		{
			throw new NotImplementedException();
		}

		public void RenameFile(string oldName, string newName)
		{
			throw new NotImplementedException();
		}

		public void LoadFiles()
		{
			throw new NotImplementedException();
		}
	}
}
