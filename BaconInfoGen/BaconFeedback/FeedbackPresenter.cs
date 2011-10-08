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

            foreach (string s in FileHandler.GetSubdirectories())
                _folderView.Items.Add(s, 0);
        }

        /// <summary>
        /// Adds the feedback files from a given directory into the file view.
        /// 
        /// Will not clear out files already present in the view.
        /// </summary>
        /// <param name="directory">The directory to add files from.</param>
        public void FileViewAddDirectory(string directory)
        {
            foreach (string s in FileHandler.GetFeedbackFiles(directory))
            {
                _fileView.Items.Add(s, 2);

                string lmd = FileHandler.GetCreationDate(directory + '/' + s);
                _fileView.Items[_fileView.Items.Count - 1].SubItems.Add(lmd);
                _fileView.Items[_fileView.Items.Count - 1].SubItems.Add(directory);
            }
        }

        /// <summary>
        /// Removes the feedback files from a given directory from the file view.
        /// </summary>
        /// <param name="directory">The directory being excluded from the file view.</param>
        public void FileViewRemoveDirectory(string directory)
        {
            foreach (string s in FileHandler.GetFeedbackFiles(directory))
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
            // Get the file contents.
            string[] contents =
                FileHandler.GetFeedbackContents(_fileView.SelectedItems[0].SubItems[2].Text + '/' +
                                                _fileView.SelectedItems[0].Text);

            // For each line of the file, insert data into the appropriate textboxes.
            for(int i = 0; i < 4; i++)
                _feedbackFields[i].Text = contents[i];
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
        }

        #endregion
    }
}
