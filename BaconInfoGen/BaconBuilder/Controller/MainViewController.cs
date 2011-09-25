using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BaconBuilder.Model;
using BaconBuilder.View;

namespace BaconBuilder.Controller
{
	public class MainViewController
	{
		private readonly IModel _model;
		private readonly IMainView _view;

		public MainViewController(IModel model, IMainView view)
		{
			_model = model;
			_view = view;
		}

		// Test directory. Needs to be removed at some point.
		private static readonly string HtmlDirectory = string.Format("C:/Users/{0}/test/", Environment.UserName);

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
			// Load files into the model.
			_model.LoadFiles();
			// Load the filenames to view.
			RefreshDirectory();
		}

		public void RefreshDirectory()
		{
			_view.Files.Clear();

			// Add each item to the list view.
			foreach (var fileName in _model.FileNames)
			{
				_view.Files.Add(fileName, 0);
			}
			_view.IsRemoveButtonEnabled = false;
		}

		public void SelectFile(string value)
		{
			_model.CurrentFileWithExtension = value;
			_view.TitleText = _model.CurrentFile;
			_view.Contents = LoadHtmlToText();

			Point coord;
			try
			{
				coord = HtmlToText.ExtractXY(_model.CurrentContents);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				coord = new Point(0, 0);
			}
			_view.XCoord = coord.X;
			_view.YCoord = coord.Y;
		}


		/// <summary>
		/// Returns the index of the listview item
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private int FindItem(string text)
		{
			for (var i = 0; i < _view.Files.Count; i++)
				if (_view.Files[i].Text.Equals(text)) return i;

			return -1;
		}

		public void ValidateTitle()
		{
			if (_model.CurrentFileWithExtension == null) return;
			// If the new title is invalid (e.g. blank, only spaces)
			if (_view.TitleText.Trim().Length == 0)
			{
				MessageBox.Show(@"Title cannot be blank or just spaces.");
				_view.TitleText = _model.CurrentFile;
				return;
			}
			// If the text has changed.
			if (_view.TitleText.Equals(_model.CurrentFile)) return;

			// Since the title is valid and changed, we rename it.
			var index = FindItem(_model.CurrentFileWithExtension);
			try
			{
				RenameFile(_model.CurrentFile, _view.TitleText);
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message); MessageBox.Show(ex.Message, @"Error");
				_view.TitleText = _model.CurrentFile;
				return;
			}
			RefreshDirectory();
			_view.Files[index].Selected = true;
		}

		public bool ContentsHaveChanged()
		{
			return !_model.CurrentContents.Equals(_view.Contents);
		}

		/// <summary>
		/// Gets the text content of an HTML file.
		/// 
		/// Uses an HtmlToTextParser to parse its content to plain text.
		/// </summary>
		/// <returns>String content (plain text) of the file.</returns>
		public string LoadHtmlToText()
		{
			Console.WriteLine(@"Loading from {0}", _model.CurrentFileWithExtension);
			// Return plain text version of the current contents.
			MessageBox.Show(_model.CurrentContents);
			//MessageBox.Show(HtmlToText.Parse(_model.CurrentContents));
			return HtmlToText.Parse(_model.CurrentContents);
		}

	    /// <summary>
	    /// Gets the html parsed verision of plain text content and saves it to a file.
	    /// </summary>
	    /// <param name="filename">The filename of the file to save to.</param>
	    public void SaveTextToHtml(string filename)
		{
			Console.WriteLine(@"Saving to {0}", filename);

		    TextToHtml.X = Convert.ToInt32(_view.XCoord);
            TextToHtml.Y = Convert.ToInt32(_view.YCoord);

			string htmlContent = TextToHtml.GenerateContent(_view.Contents);
			Console.WriteLine("saving htmlContents " + htmlContent);
			//_model.CurrentContents = htmlContent;
			File.WriteAllText(HtmlDirectory + filename, htmlContent);
			_model.LoadFiles();
		}

		/// <summary>
		/// Creates a new blank file.
		/// </summary>
		public void CreateNewFile()
		{
			_model.CreateNewFile("New File");
		}

		/// <summary>
		/// Rename the old file name to the new file name.
		/// </summary>
		/// <param name="oldName"></param>
		/// <param name="newName"></param>
		public void RenameFile(string oldName, string newName)
		{
			_model.RenameFile(oldName, newName);
		}

		public void RemoveCurrentFile()
		{
			_model.RemoveFile(_model.CurrentFileWithExtension);
			RefreshDirectory();
		}

		public void RemoveFile(string fileName)
		{
			var f = new FileInfo(HtmlDirectory + fileName);
			Console.WriteLine(@"Removing file " + f.Name);

			if (f.Exists)
				f.Delete();
		}

//		public void FetchFromServer()
//		{
//
//			//FtpDialog ftpDialog = new FtpDialog(new FtpDownloader(_model));
//			//ftpDialog.ShowDialog();
//			//_controller.InitialiseListView();
//			_model.FetchFromServer();
//			_model.LoadFiles();
//			Console.WriteLine("Fetched from server. {0} files loaded into memory", _model.FileNames.Count);
//		}
	}
}
