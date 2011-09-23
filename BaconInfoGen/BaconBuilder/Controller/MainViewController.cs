using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BaconBuilder.Model;
using BaconBuilder.View;

namespace BaconBuilder.Controller
{
	public class MainViewController : IMainViewController
    {
		private readonly BaconModel _model;
		private readonly MainWindow _view;

		public MainViewController(BaconModel model, MainWindow view)
		{
			_model = model;
			_view = view;
		}

		// Test directory. Needs to be removed at some point.
		private const string HtmlExtension = ".html";
        private static readonly string HtmlDirectory = "C:/Users/"+System.Environment.UserName+"/test/";

		private const string BlankHtmlFileName = "Blank";

		private const string NewHtmlFileName = "New File";

		// Directory for local html content.
        //private const string HtmlDirectory = "./DataFiles";

        // Parser object to handle html to text conversion.
        private static readonly HtmlToTextParser HtmlToText = new HtmlToTextParser();
        private static readonly TextToHtmlParser TextToHtml = new TextToHtmlParser();

        /// <summary>
        /// Initialises and populates a listview with the html files in a directory.
        /// </summary>
        /// <param name="listView">The listview to initialise.</param>
        public void InitialiseListView(ListView listView)
        {
            listView.Items.Clear();

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
                    listView.Items.Add(f.Name, 0);
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

		/// <summary>
		/// Gets the text content of an HTML file.
		/// 
		/// Uses an HtmlToTextParser to parse its content to plain text.
		/// </summary>
		/// <param name="fileName">Name of file to get data from.</param>
		/// <returns>String content (plain text) of the file.</returns>
		public string LoadHtmlToText(string fileName)
		{
			Console.WriteLine("Loading from HTML");

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
			Console.WriteLine("Saving to {0}", filename + HtmlExtension);

			string htmlContent = TextToHtml.Parse(text);
			File.WriteAllText(HtmlDirectory + filename + HtmlExtension, htmlContent);
		}

		/// <summary>
		/// Creates a new blank file.
		/// </summary>
		public void CreateNewFile()
		{
			Console.WriteLine("Creating new file");
			new FileInfo(HtmlDirectory + BlankHtmlFileName + HtmlExtension).CopyTo(GetLowestUnusedNewFileName());
		}

		/// <summary>
		/// Rename the old file name to the new file name.
		/// </summary>
		/// <param name="oldName"></param>
		/// <param name="newName"></param>
		public void RenameFile(string oldName, string newName)
		{
			Console.WriteLine("Renaming file " + oldName + " to " + newName);
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
			Console.WriteLine("Removing file " + f.Name);

			if (f.Exists)
				f.Delete();
		}
	}
}
