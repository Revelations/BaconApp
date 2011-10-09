using System;
using System.Collections.Generic;
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

        private List<string> _deletedFolders;
        private List<string> _deletedFiles;

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
                    FileHandler.GetFeedbackContents(_fileView.SelectedItems[0].SubItems[2].Text + '/' +
                                                    _fileView.SelectedItems[0].Text);

            // Otherwise get a bunch of empty strings.
            else
                contents = new[] {string.Empty, string.Empty, string.Empty, string.Empty};

            // For each line of the file, insert data into the appropriate textboxes.
            for (int i = 0; i < 4; i++)
                _feedbackFields[i].Text = contents[i];
        }

        public void DeleteFolder(string folder)
        {
            FileViewClear();

            FileHandler.DeleteFolder(folder);
            _deletedFolders.Add(folder);
        }

        public void DeleteFiles()
        {
            foreach (ListViewItem i in _fileView.SelectedItems)
            {
                string path = i.SubItems[2].Text + '/' + i.Text;
                _fileView.Items.Remove(i);

                FileHandler.DeleteFile(path);
                _deletedFiles.Add(path);
            }
            
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

        public bool ConfirmDelete(bool folder)
        {
            ListView view = folder ? _folderView : _fileView;
            string type = folder ? "folder" : "file";

            if (view.SelectedItems.Count == 0)
            {
                MessageBox.Show(string.Format(@"No {0}s selected for deletion.", type), @"Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }

            if (MessageBox.Show(string.Format(@"Are you sure you wish to delete all selected {0}s in the {0} view? These {0}s will be gone forever.", type),
                @"Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                return true;

            return false;
        }

        public void CopySelectedText()
        {
            foreach(TextBox t in _feedbackFields)
            {
                if (t.ContainsFocus)
                {
                    Clipboard.SetText(t.SelectedText);
                    return;
                }
            }
        }
    }
}
