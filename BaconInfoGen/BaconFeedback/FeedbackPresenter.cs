using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace BaconFeedback
{
	/// <summary>
	/// 
	/// </summary>
	public class FeedbackPresenter
	{
		#region Fields and Attributes

		private readonly ListView _folderView;
		private readonly ListView _fileView;
		private readonly TextBox[] _feedbackFields;

		private readonly List<string> _deletedFolders;
		private readonly List<string> _deletedFiles;

		private PrintHandler _printer;

		#endregion

		#region Public Methods

		/// <summary>
		/// Populates the folder view with subdirectories of the main feedback directory.
		/// 
		/// First clears and refreshes the view to remove any existing items.
		/// </summary>
		public void PopulateFolderView()
		{
			_folderView.Items.Clear();
			_folderView.Refresh();

			foreach (string s in FileHandler.GetSubfolders())
				_folderView.Items.Add(s, 0);
		}

		/// <summary>
		/// Adds the feedback files from a given directory into the file view.
		/// 
		/// Will not clear out files already present in the view.
		/// </summary>
		/// <param name="folder">The directory to add files from.</param>
		public void FileViewAddFolder(string folder)
		{
			foreach (string s in FileHandler.GetFeedbackFiles(folder))
			{
				_fileView.Items.Add(s, 2);

				string lmd = FileHandler.GetCreationDate(folder + '/' + s);
				_fileView.Items[_fileView.Items.Count - 1].SubItems.Add(lmd);
				_fileView.Items[_fileView.Items.Count - 1].SubItems.Add(folder);
			}
		}

		/// <summary>
		/// Removes the feedback files from a given directory from the file view.
		/// </summary>
		/// <param name="folder">The directory being excluded from the file view.</param>
		public void FileViewRemoveFolder(string folder)
		{
			foreach (string s in FileHandler.GetFeedbackFiles(folder))
				foreach (ListViewItem i in _fileView.Items)
					if (i.Text.Equals(s))
						_fileView.Items.Remove(i);
		}

		/// <summary>
		/// Clears and refreshes the contents of the file view.
		/// </summary>
		public void FileViewClear()
		{
			_fileView.Items.Clear();
			_fileView.Refresh();
		}

		/// <summary>
		/// Loads the contents of a feedback file into the text fields in the view.
		/// </summary>
		public void DisplayFeedbackFile()
		{
			string[] contents;

			// If there is a file selected, then get file contents.
			if (_fileView.SelectedItems.Count > 0)
				contents =
					FileHandler.GetFeedbackContents(_fileView.SelectedItems[0].SubItems[2].Text + '/' + _fileView.SelectedItems[0].Text);

				// Otherwise get a bunch of empty strings.
			else
				contents = new[] {string.Empty, string.Empty, string.Empty, string.Empty};

			// For each line of the file, insert data into the appropriate textboxes.
			if (contents.Length < 4)
			{
				MessageBox.Show(@"Bad feedback file");
				return;
			}
			Debug.Assert(contents.Length == 4, "Bad feedback file!");
			for (int i = 0; i < contents.Length; i++)
				_feedbackFields[i].Text = contents[i];
		}

		/// <summary>
		/// Deletes a single folder from the folder view and file system.
		/// </summary>
		/// <param name="folder">Path of the folder to delete.</param>
		public void DeleteFolder(string folder)
		{
			// Clear the folder view since it's contents no longer exist.
			FileViewClear();

			// Pass the buck.
			FileHandler.DeleteFolder(folder);

			// Add folder to the list of folders going to be removed from the server.
			_deletedFolders.Add(folder);
		}

		/// <summary>
		/// Deletes all selected files from both the file view and file system.
		/// </summary>
		public void DeleteFiles()
		{
			foreach (ListViewItem i in _fileView.SelectedItems)
			{
				string path = i.SubItems[2].Text + '/' + i.Text;
				_fileView.Items.Remove(i);

				FileHandler.DeleteFile(path);

				// Add the file to the list to be removed from the server.
				_deletedFiles.Add(path);
			}
		}

		/// <summary>
		/// Confirms the deletion of files/folders or notifies of the fact that none are selected.
		/// </summary>
		/// <param name="folder">Set this to true if confirming deletion of a folder, false if a file.</param>
		/// <returns>True if the user confirms, false otherwise.</returns>
		public bool ConfirmDelete(bool folder)
		{
			// Initi properties based on type of object to delete.
			ListView view = folder ? _folderView : _fileView;
			string type = folder ? "folder" : "file";

			// If nothing is selected, inform user and return.
			if (view.SelectedItems.Count == 0)
			{
				ShowErrorMessage(string.Format(@"No {0}s selected for deletion.", type));
				return false;
			}

			// Prompt for confirmation.
			string message = string.Format(@"Are you sure you wish to delete all selected {0}s in the {0} view? These {0}s will be gone forever.", type);
			DialogResult dialogResult = MessageBox.Show(message, @"Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.OK)
				return true;

			// If we get this far then the user did not confirm deletion.
			return false;
		}

		/// <summary>
		/// Helper method that shows a messagebox with a given error message.
		/// </summary>
		/// <param name="message">The error message to display.</param>
		public void ShowErrorMessage(string message)
		{
			MessageBox.Show(message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// Copies the selected text from one of the textboxes on the form to the clipboard.
		/// </summary>
		public void CopySelectedText()
		{
			// Check each textbox for focus.
			foreach (TextBox t in _feedbackFields)
			{
				if (t.ContainsFocus)
				{
					// Copy selection if it exists.
					if (t.SelectedText != string.Empty)
						Clipboard.SetText(t.SelectedText);
					return;
				}
			}
		}

		/// <summary>
		/// Invokes the print handler to create a print document.
		/// </summary>
		public void ConstructPrintDocument(object sender, PrintPageEventArgs e)
		{
			_printer.ConstructPrintDocument(e);
		}

		/// <summary>
		/// Initialises the print handler with a list of files to be printed/previewed.
		/// </summary>
		public void InitPrintHandler()
		{
			_printer = new PrintHandler(CreateFeedbackList());
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor accepting a single argument.
		/// </summary>
		/// <param name="f">The form that this will control presentation for.</param>
		public FeedbackPresenter(FeedbackMainForm f)
		{
			// Assign form elements to member variables.
			_folderView = f.FolderView;
			_fileView = f.FileView;
			_feedbackFields = f.FeedbackFields;

			_deletedFolders = new List<string>();
			_deletedFiles = new List<string>();
		}

		#endregion

		/// <summary>
		/// Gets data from all selected feedback and creates a list of feedback data for passing to other classes.
		/// </summary>
		/// <returns>List of all selected feedback files.</returns>
		public List<FeedbackFile> CreateFeedbackList()
		{
			// Initialise a list to store results.
			List<FeedbackFile> result = new List<FeedbackFile>();

			// For each selected feedback...
			foreach (ListViewItem i in _fileView.SelectedItems)
			{
				// Get the feedback file contents.
				string[] contents = FileHandler.GetFeedbackContents(i.SubItems[2].Text + '/' + i.Text);

				// Initialise a struct to store info in.
				FeedbackFile f = new FeedbackFile();

				// Hand info to struct properties.
				f.FileName = i.Text;
				f.Directory = i.SubItems[2].Text;
				f.CreatedDate = i.SubItems[1].Text;

				f.Number = contents[0];
				f.Nationality = contents[1];
				f.Sighted = contents[2];
				f.Misc = contents[3];

				// Add resulting struct to feedback list.
				result.Add(f);
			}
			return result;
		}
	}
}