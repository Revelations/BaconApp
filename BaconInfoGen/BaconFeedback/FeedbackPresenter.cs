using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Common;
using Resources = Common.Resources;

namespace BaconFeedback
{
	/// <summary>
	/// 
	/// </summary>
	public class FeedbackPresenter
	{
		#region Fields and Attributes

		private readonly List<string> _deletedFiles;
		private readonly List<string> _deletedFolders;
		private readonly FeedbackMainForm _view;

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
			ListViewClear(_view.FolderView);

			foreach (string s in FileHandler.GetSubfolders())
				_view.FolderView.Items.Add(s, 0);
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
				_view.FileView.Items.Add(s, 2);

				string lmd = FileHandler.GetCreationDate(folder + '/' + s);
				_view.FileView.Items[_view.FileView.Items.Count - 1].SubItems.Add(lmd);
				_view.FileView.Items[_view.FileView.Items.Count - 1].SubItems.Add(folder);
			}
		}

		/// <summary>
		/// Removes the feedback files from a given directory from the file view.
		/// </summary>
		/// <param name="folder">The directory being excluded from the file view.</param>
		public void FileViewRemoveFolder(string folder)
		{
			foreach (string s in FileHandler.GetFeedbackFiles(folder))
				foreach (ListViewItem i in _view.FileView.Items)
					if (i.Text.Equals(s))
						_view.FileView.Items.Remove(i);
		}

		/// <summary>
		/// Clears and refreshes the contents of the file view.
		/// </summary>
		private void ListViewClear(ListView listView)
		{
			listView.Items.Clear();
			listView.Refresh();
		}

		/// <summary>
		/// Loads the contents of a feedback file into the text fields in the view.
		/// </summary>
		public void DisplayFeedbackFile()
		{
			DisplayFeedbackFile((_view.FileView.SelectedItems.Count > 0) ? _view.FileView.SelectedItems[0] : null);
		}

		private void DisplayFeedbackFile(ListViewItem item)
		{
			// If there is a file selected, then get file contents.
			string[] contents = item != null
			                    ? FileHandler.GetFeedbackContents(item.SubItems[2].Text + '/' + item.Text)
			                    : new[] {string.Empty, string.Empty, string.Empty, string.Empty};

			// For each line of the file, insert data into the appropriate textboxes.
			if (contents.Length != 4)
			{
				ShowErrorMessage(@"Bad feedback file");
			}
			else
			{
				for (int i = 0; i < contents.Length; i++)
					_view.FeedbackFields[i].Text = contents[i];
			}
		}

		/// <summary>
		/// Deletes a single folder from the folder view and file system.
		/// </summary>
		/// <param name="folder">Path of the folder to delete.</param>
		public void DeleteFolder(string folder)
		{
			// Clear the folder view since its contents no longer exist.
			ListViewClear(_view.FileView);

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
			foreach (ListViewItem i in _view.FileView.SelectedItems)
			{
				string path = i.SubItems[2].Text + '/' + i.Text;
				_view.FileView.Items.Remove(i);

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
			ListView view = folder
				? _view.FolderView
				: _view.FileView;
			string type = folder
				? "folder"
				: "file";

			// If nothing is selected, inform user and return.
			if (view.SelectedItems.Count == 0)
			{
				ShowErrorMessage(string.Format(@"No {0}s selected for deletion.", type));
				return false;
			}

			// Prompt for confirmation.
			return DialogResult.OK == MessageBox.Show(
				string.Format(@"Are you sure you wish to delete all selected {0}s in the {0} view? These {0}s will be gone forever.", type),
				@"Confirm!",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Stop,
				MessageBoxDefaultButton.Button2);
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
			foreach (TextBox t in _view.FeedbackFields)
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
		/// <param name="view">The form that this will control presentation for.</param>
		public FeedbackPresenter(FeedbackMainForm view)
		{
			_view = view;

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
			// For each selected feedback...
			return (from ListViewItem i in _view.FileView.SelectedItems
			        // Get the feedback file contents.
			        let contents = FileHandler.GetFeedbackContents(i.SubItems[2].Text + '/' + i.Text)
			        // Add resulting struct to feedback list.
			        select new FeedbackFile
			               	{
			               		FileName = i.Text,
			               		Directory = i.SubItems[2].Text,
			               		CreatedDate = i.SubItems[1].Text,
			               		Number = contents[0],
			               		Nationality = contents[1],
			               		Sighted = contents[2],
			               		Misc = contents[3]
			               	}).ToList();
		}

		public void DownloadSync()
		{
			SyncDialog dialog = new SyncDialog(new SyncInfo(Resources.FeedbackDirectory, "Feedback/", SyncJobType.Download));
			dialog.ShowDialog();
		}
	}
}