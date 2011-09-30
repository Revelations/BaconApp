using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BaconBuilder.Controller;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class MainWindow : Form, IMainView
	{
		private readonly MainViewController _controller;
		private readonly BaconModel _model;

		#region Constructors

		public MainWindow()
		{
			InitializeComponent();

			_model = new BaconModel();
			_controller = new MainViewController(_model, this);

			// Event binding
			tsbImage.Click += btnImage_Click;
			tsbAudio.Click += btnAudio_Click;
			tsbBold.Click += tsbBold_Click;
			tsbItalics.Click += tsbItalics_Click;
			btnMapPreview.Click += btnMapPreview_Click;
			btnPreview.Click += btnPreview_Click;

			printToolStripMenuItem.Click += btnPrintPreview_Click;

			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;

			listViewContents.SelectedIndexChanged += listViewContents_SelectedIndexChanged;
		}

		/// <summary>
		/// Wrap the selected text in bold tags.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbBold_Click(object sender, EventArgs e)
		{
			txtBoxMain.SelectedText = "<b>" + txtBoxMain.SelectedText + "</b>";
		}

		/// <summary>
		/// Wrap the selected text in italics.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbItalics_Click(object sender, EventArgs e)
		{
			txtBoxMain.SelectedText = "<i>" + txtBoxMain.SelectedText + "</i`>";
		}

		#endregion

		#region IMainView Members

		/// <summary>
		/// Get or set the title text.
		/// </summary>
		public string TitleText
		{
			get { return txtTitle.Text; }
			set { txtTitle.Text = value; }
		}

		/// <summary>
		/// Get or set the X-coordinates.
		/// </summary>
		public decimal XCoord
		{
			get { return txtX.Value; }
			set { txtX.Value = value; }
		}

		/// <summary>
		/// Get or set the Y-coordinates.
		/// </summary>
		public decimal YCoord
		{
			get { return txtY.Value; }
			set { txtY.Value = value; }
		}

		/// <summary>
		/// get or set the contents of the textbox;
		/// </summary>
		public string Contents
		{
			get { return txtBoxMain.Text; }
			set { txtBoxMain.Text = value; }
		}

		/// <summary>
		/// Returns the items in the listview control.
		/// </summary>
		public ListView.ListViewItemCollection Files
		{
			get { return listViewContents.Items; }
		}

		/// <summary>
		/// Enable controls if need be. For example, if no files are selected in the list view, disable the remove file button.
		/// Call this as often as needed!
		/// </summary>
		public void EnableRequiredControls()
		{
			btnRemoveFile.Enabled = listViewContents.SelectedItems.Count != 0;
			btnPreview.Enabled = _model.CurrentFileName != null;
			toolContents.Enabled = _model.CurrentFileName != null;
			txtBoxMain.Enabled = _model.CurrentFileName != null;
		}

		#endregion

		/// <summary>
		/// Exit the program.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Preview the contents as HTML. TODO: Draw "you are here" marker.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPreview_Click(object sender, EventArgs e)
		{
			_model.CurrentContents = Contents;
			var preview = new Preview(_model);

			preview.ShowDialog();
		}

		/// <summary>
		/// Inserts an audio wrapped in tags. TODO: Implement importing of foreign images.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAudio_Click(object sender, EventArgs e)
		{
			_model.CurrentContents = Contents;
			// Stores the current caret position and length of selection.
			int caretPos = txtBoxMain.SelectionStart;
			//int selectionLength = txtBoxMain.SelectionLength;

			var dialog = new MediaSelectionDialog(_model, ContentType.Audio);
			if (dialog.ShowDialog() != DialogResult.Cancel)
			{
				txtBoxMain.SelectionStart = caretPos;
				txtBoxMain.SelectedText = string.Format("<audio>{0}</audio>", _model.AudioUrl);
			}
		}

		/// <summary>
		/// Inserts an image wrapped in tags. TODO: Implement importing of foreign images.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnImage_Click(object sender, EventArgs e)
		{
			// Stores the current caret position and length of selection.
			int caretPos = txtBoxMain.SelectionStart;
			//int selectionLength = txtBoxMain.SelectionLength;

			var dialog = new MediaSelectionDialog(_model, ContentType.Image);
			if (dialog.ShowDialog() != DialogResult.Cancel)
			{
				txtBoxMain.SelectionStart = caretPos;
				txtBoxMain.SelectedText = string.Format("<img>{0}</img>", _model.ImageUrl);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnMapPreview_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException("btnMapPreview_Click");
		}

		/// <summary>
		/// Called when the form is first loaded.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Load(object sender, EventArgs e)
		{
			_controller.InitialiseListView();
		}

		/// <summary>
		/// Create and add a new bacon to freezer.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddFile_Click(object sender, EventArgs e)
		{
			_controller.CreateNewFile();
			_controller.InitialiseListView();
		}

		/// <summary>
		/// Dispose of unwanted bacon from freezer with confirmation of disposal.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRemoveFile_Click(object sender, EventArgs e)
		{
			string message = string.Format(@"Are you sure you wish to delete the file ""{0}""?", _model.CurrentFileName);
			if (MessageBox.Show(message, @"Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				_controller.RemoveCurrentFile();

				_controller.RefreshDirectory();
			}
		}

		private void txtTitle_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				txtTitle_FocusLeft(null, e);
			else if (e.KeyCode == Keys.Escape)
			{
				TitleText = _model.CurrentFileName;
				txtBoxMain.Focus();
			}
		}

		/// <summary>
		/// When the user leaves the title textbox, handle any name changes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtTitle_FocusLeft(object sender, EventArgs e)
		{
			_controller.ValidateTitle();
		}

		/// <summary>
		/// Selecting a different bacon. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewContents_SelectedIndexChanged(object sender, EventArgs e)
		{
			EnableRequiredControls();
		}

		/// <summary>
		/// Gets called for each bacon that was selected and deselected.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewContents_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
				_controller.SelectFile(e.Item.Text);
			}
			else
			{
				if (_controller.ContentsHaveChanged())
				{
					_controller.SaveTextToHtml(e.Item.Text);
				}
			}
		}

		/// <summary>
		/// Shows a print preview.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrintPreview_Click(object sender, EventArgs e)
		{
			printPreviewDialog.Document = printDocument;
			printPreviewDialog.ShowDialog();
		}

		/// <summary>
		/// Print pages. TODO: Handle multipage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			var qr = new QrCodeGenerator();

			var font = new Font(Font.FontFamily, 20);
			var fontColor = new SolidBrush(Color.Black); // For text

			int j = 1;
			for (int i = 0; i < Files.Count; i++)
			{
				string text = Files[i].Text;
				//code per page counter
				if (j > 4)
				{
					j = 1; /*create new page*/
					break;
				}
				Image code = qr.GenerateCode(text);
				//Even on left. Odd on right
				Rectangle layoutRectangle;
				if (j%2 == 1)
				{
					layoutRectangle = new Rectangle(400, 180*j, 500, 100);
					e.Graphics.DrawImage(code, 100, 150*j);
				}
				else
				{
					layoutRectangle = new Rectangle(200, 180*j, 500, 100);
					e.Graphics.DrawImage(code, 500, 150*j);
				}
				e.Graphics.DrawString(text, font, fontColor, layoutRectangle);

				j++;
			}
		}

		/// <summary>
		/// Synchronise bacon in freezer with the bacon in the butchery.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripSync_Click(object sender, EventArgs e)
		{
			var ftpDialog = new FtpDialog(new FtpUploader());
			ftpDialog.ShowDialog();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Shown(object sender, EventArgs e)
		{
			var ftpDialog = new FtpDialog(new FtpDownloader(_model));
			ftpDialog.ShowDialog();
			_controller.InitialiseListView();
		}

		/// <summary>
		/// Called when the form is closing. Synchronise with butchery if need be.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result =
				MessageBox.Show(
					@"Would you like to synchronise your content with the distribution server before exiting?",
					@"Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

			if (result == DialogResult.Yes)
			{
				var ftpDialog = new FtpDialog((new FtpUploader()));
				ftpDialog.ShowDialog();
			}
			if (result == DialogResult.Cancel)
				e.Cancel = true;
		}
	}
}