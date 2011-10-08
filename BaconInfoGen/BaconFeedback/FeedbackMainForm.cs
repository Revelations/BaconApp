using System;
using System.Windows.Forms;

namespace BaconFeedback
{
    public partial class FeedbackMainForm : Form
    {
        public ListView FolderView { get { return folderView; } }
        public ListView FileView { get { return fileView; } }

        public TextBox[] FeedbackFields { get { return new [] { textBoxNumber, textBoxNationality, textBoxSighted, textBoxMisc }; } }

        private readonly FeedbackPresenter _presenter;

        public FeedbackMainForm()
        {
            InitializeComponent();

            _presenter = new FeedbackPresenter(this);

            _presenter.PopulateFolderView();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void folderView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            e.Item.ImageIndex = e.IsSelected ? 1 : 0;

            if (e.Item.Selected)
                _presenter.FileViewAddDirectory(e.Item.Text);
            else
                _presenter.FileViewRemoveDirectory(e.Item.Text);
        }

        private void fileView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileView.SelectedItems.Count > 0)
                _presenter.DisplayFeedbackFile();
        }

        private void splitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            colHeaderFolder.Width = folderView.Width - 4;
        }
    }
}
