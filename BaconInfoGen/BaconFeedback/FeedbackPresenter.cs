using System.Windows.Forms;

namespace BaconFeedback
{
    public class FeedbackPresenter
    {
        private ListView _folderView;
        private ListView _fileView;
        private TextBox[] _feedbackFields;

        private string _lastSelectedFolder;

        public FeedbackPresenter(FeedbackMainForm f)
        {
            _folderView = f.FolderView;
            _fileView = f.FileView;
            _feedbackFields = f.FeedbackFields;
        }

        public void PopulateFolderView()
        {
            _folderView.Items.Clear();
            _folderView.Refresh();

            foreach (string s in FileHandler.GetSubdirectories())
                _folderView.Items.Add(s, 0);
        }

        public void PopulateFileView(string directory)
        {
            foreach (string s in FileHandler.GetFeedbackFiles(directory))
                _fileView.Items.Add(s, 2);
        }

        private void EmptyFileView()
        {
            _fileView.Items.Clear();
            _fileView.Refresh();
        }

        public void ChangeFolder()
        {
            if (_folderView.SelectedItems.Count == 0)
                EmptyFileView();
            else
            {
                PopulateFileView(_folderView.SelectedItems[0].Text);
                _lastSelectedFolder = _folderView.SelectedItems[0].Text;
            }
        }

        public void ChangeFile()
        {
            if (_fileView.SelectedItems.Count > 0)
                DisplayFeedbackFile(_fileView.SelectedItems[0].Text);
        }

        public void DisplayFeedbackFile(string fileName)
        {
            string[] contents = FileHandler.GetFeedbackContents(_lastSelectedFolder + "/" + fileName);

            for(int i = 0; i < 4; i++)
            {
                _feedbackFields[i].Text = contents[i];
            }
        }
    }
}
