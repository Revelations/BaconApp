using System;
using System.Windows.Forms;

namespace BaconFeedback
{
	public partial class FeedbackMainForm : Form
	{
		private readonly FeedbackPresenter _presenter;

		public FeedbackMainForm()
		{
			InitializeComponent();

			_presenter = new FeedbackPresenter(this);

			printDocument.PrintPage += _presenter.ConstructPrintDocument;
		}

		public ListView FolderView
		{
			get { return folderView; }
		}

		public ListView FileView
		{
			get { return fileView; }
		}

		public TextBox[] FeedbackFields
		{
			get { return new[] {textBoxNumber, textBoxNationality, textBoxSighted, textBoxMisc}; }
		}

		private void folderView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			e.Item.ImageIndex = e.IsSelected ? 1 : 0;

			if (e.Item.Selected)
				_presenter.FileViewAddFolder(e.Item.Text);
			else
				_presenter.FileViewRemoveFolder(e.Item.Text);
		}

		private void fileView_SelectedIndexChanged(object sender, EventArgs e)
		{
			_presenter.DisplayFeedbackFile();
		}

		private void splitter_SplitterMoved(object sender, SplitterEventArgs e)
		{
			colHeaderFolder.Width = folderView.Width;
		}

		#region Toolbar Button Events

		private void toolStripDeleteFolder_Click(object sender, EventArgs e)
		{
			if (_presenter.ConfirmDelete(true))
			{
				foreach (ListViewItem i in folderView.SelectedItems)
					_presenter.DeleteFolder(i.Text);
				_presenter.PopulateFolderView();
			}
		}

		private void toolStripDeleteFile_Click(object sender, EventArgs e)
		{
			if (_presenter.ConfirmDelete(false))
			{
				_presenter.DeleteFiles();
				_presenter.DisplayFeedbackFile();
			}
		}

		private void Copy_Click(object sender, EventArgs e)
		{
			_presenter.CopySelectedText();
		}

		private void Export_Click(object sender, EventArgs e)
		{
		}

		private void toolStripPreview_Click(object sender, EventArgs e)
		{
			if (fileView.SelectedItems.Count > 0)
			{
				_presenter.InitPrintHandler();
				printPreviewDialog.Width = 800;
				printPreviewDialog.Height = 600;
				printPreviewDialog.ShowDialog();
			}
			else
				_presenter.ShowErrorMessage(@"No files selected. Cannot display preview.");
		}

		private void toolStripPrint_Click(object sender, EventArgs e)
		{
		}

		private void toolStripStats_Click(object sender, EventArgs e)
		{
			if (fileView.SelectedItems.Count > 0)
			{
				var stats = new StatisticsForm(_presenter.CreateFeedbackList());
				stats.ShowDialog();
			}
			else
				_presenter.ShowErrorMessage(@"No files selected. Cannot display statistics.");
		}

		private void toolStripSync_Click(object sender, EventArgs e)
		{
			_presenter.DownloadSync();
			_presenter.ListViewClear(fileView);
			_presenter.PopulateFolderView();
		}

		private void toolStripConfig_Click(object sender, EventArgs e)
		{
		}

		#endregion

		#region Menustrip Button Events

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		#endregion

		private void FeedbackMainForm_Shown(object sender, EventArgs e)
		{
			_presenter.DownloadSync();
			_presenter.PopulateFolderView();
		}

		private void FeedbackMainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			_presenter.UploadSyncExit();
		}
	}
}