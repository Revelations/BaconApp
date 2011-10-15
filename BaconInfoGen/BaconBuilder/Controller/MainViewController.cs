using System;
using System.IO;
using System.Windows.Forms;
using BaconBuilder.Model;
using BaconBuilder.View;

namespace BaconBuilder.Controller
{
	public class MainViewController
	{
		// Directory for local html content.
		//private const string HtmlDirectory = "./DataFiles";

		// Parser object to handle html to text conversion.
		private static readonly HtmlToTextParser HtmlToText = new HtmlToTextParser();
		private static readonly TextToHtmlParser TextToHtml = new TextToHtmlParser();
		private readonly BaconModel _model;
		private readonly IMainView _view;

		public MainViewController(BaconModel model, IMainView view)
		{
			_model = model;
			_view = view;
			_view.EnableRequiredControls();
		}

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

		/// <summary>
		/// Reloads the directory in the view.
		/// </summary>
		public void RefreshDirectory()
		{
			_view.Files.Clear();
			// Add each item to the list view.
			foreach (string fileName in _model.FileNames)
			{
				_view.Files.Add(fileName, 0);
			}
			// Enabled controls if need be.
			_view.EnableRequiredControls();
		}

		public void SelectFile(string value)
		{
			_model.CurrentFileNameWithExtension = value;
			_view.TitleText = _model.CurrentFileName;
			//_view.Contents = _model.CurrentContents;

			_view.Browser.Document.OpenNew(true);
			_view.Browser.Document.Write(File.ReadAllText(Common.Resources.ContentDirectory + value));
			_view.Browser.Url = new Uri(Common.Resources.ContentDirectory + value);

			// TODO: Re implement handling of point data.

			_view.EnableRequiredControls();
		}


		/// <summary>
		/// Returns the index of the listview item
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private int FindItem(string text)
		{
			for (int i = 0; i < _view.Files.Count; i++)
				if (_view.Files[i].Text.Equals(text)) return i;

			return -1;
		}

		public void ValidateTitle()
		{
			if (_model.CurrentFileNameWithExtension == null) return;
			// If the new title is invalid (e.g. blank, only spaces)
			if (_view.TitleText.Trim().Length == 0)
			{
				MessageBox.Show(@"Title cannot be blank or just spaces.");
				_view.TitleText = _model.CurrentFileName;
				return;
			}
			// If the text has changed.
			if (_view.TitleText.Equals(_model.CurrentFileName)) return;

			// Since the title is valid and changed, we rename it.
			int index = FindItem(_model.CurrentFileNameWithExtension);
			try
			{
				RenameFile(_model.CurrentFileName, _view.TitleText);
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
				MessageBox.Show(ex.Message, @"Error");
				_view.TitleText = _model.CurrentFileName;
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

		/// <summary>
		/// Removes the currently loaded file from the file system.
		/// </summary>
		public void RemoveCurrentFile()
		{
			_model.RemoveFile(_model.CurrentFileNameWithExtension);
			RefreshDirectory();
		}
	}
}