using System;
using System.IO;

using BaconBuilder.Model;
using BaconBuilder.View;

namespace BaconBuilder.Controller
{
	public class MainViewController : IMainViewController
	{
		private readonly BaconModel _model;
		private readonly IMainView _view;

		public MainViewController(BaconModel model, IMainView view)
		{
			_model = model;
			_view = view;
		}

		// Test directory. Needs to be removed at some point.
		private const string HtmlExtension = ".html";
		private static readonly string HtmlDirectory = string.Format("C:/Users/{0}/test/", Environment.UserName);

		private const string BlankHtmlFileName = "Blank";

		// Directory for local html content.
		//private const string HtmlDirectory = "./DataFiles";

		// Parser object to handle html to text conversion.
		private static readonly HtmlToTextParser HtmlToText = new HtmlToTextParser();
		private static readonly TextToHtmlParser TextToHtml = new TextToHtmlParser();

		/// <summary>
		/// Initialises and populates a listview with the html files in a directory.
		/// </summary>
		public void InitialiseListView()
		{
			_view.Files.Clear();

			// Create the directory if it doesn't exist.
			if (!Directory.Exists(HtmlDirectory))
				Directory.CreateDirectory(HtmlDirectory);

			// Get a directory info object for the directory.
			var directory = new DirectoryInfo(HtmlDirectory);

			// Add each item to the list view.
			foreach (FileInfo f in directory.GetFiles())
			{
				// Get only html files, and not the blank one for initialising new files.
				if (f.Extension.Equals(HtmlExtension) && f.Name != BlankHtmlFileName + HtmlExtension)
					_view.Files.Add(f.Name, 0);
			}
		}

		/// <summary>
		/// Gets the text content of an HTML file.
		/// 
		/// Uses an HtmlToTextParser to parse its content to plain text.
		/// </summary>
		/// <param name="fileName">Name of file to get data from.</param>
		/// <returns>String content (plain text) of the file.</returns>
		public string LoadHtmlToText(string fileName)
		{
			Console.WriteLine(@"Loading from HTML");

			// Read the selected HTML file and store it.
			string htmlContent = File.ReadAllText(HtmlDirectory + fileName);

			// Return plain text version of the above.
			return HtmlToText.Parse(htmlContent);
		}

		/// <summary>
		/// Gets the html parsed verision of plain text content and saves it to a file.
		/// </summary>
		/// <param name="filename">The filename of the file to save to.</param>
		/// <param name="text">The input string to parse and write to file.</param>
		public void SaveTextToHtml(string filename, string text)
		{
			Console.WriteLine(@"Saving to {0}", filename);

			string htmlContent = TextToHtml.Parse(text);
			File.WriteAllText(HtmlDirectory + filename, htmlContent);
		}

		/// <summary>
		/// Creates a new blank file.
		/// </summary>
		public void CreateNewFile()
		{
			Console.WriteLine(@"Creating new file");
			const string content = 
@"<!DOCTYPE HTML>
<html>
<head>
<link href=""style.css"" />
<title></title>
</head>
<body>
</body>
</html>";
			File.WriteAllText(HtmlDirectory + BlankHtmlFileName + HtmlExtension, content);
			new FileInfo(HtmlDirectory + BlankHtmlFileName + HtmlExtension).CopyTo(BaconModel.GetLowestUnusedNewFileName());
		}

		/// <summary>
		/// Rename the old file name to the new file name.
		/// </summary>
		/// <param name="oldName"></param>
		/// <param name="newName"></param>
		public void RenameFile(string oldName, string newName)
		{
			Console.WriteLine(@"Renaming file {0} to {1}", oldName, newName);
			var oldInfo = new FileInfo(HtmlDirectory + oldName + HtmlExtension);
			var newInfo = new FileInfo(HtmlDirectory + newName + HtmlExtension);
			if (File.Exists(newInfo.FullName))
			{
				throw new IOException("File already exists");
			}
			oldInfo.MoveTo(newInfo.FullName);
		}

		public void RemoveFile(string fileName)
		{
			var f = new FileInfo(HtmlDirectory + fileName);
			Console.WriteLine(@"Removing file " + f.Name);

			if (f.Exists)
				f.Delete();
		}
	}
}
