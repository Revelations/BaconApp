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
			tsbImage.Click += tsbImage_Click;
			tsbAudio.Click += btnAudio_Click;
			btnMapPreview.Click += btnMapPreview_Click;
			btnPreview.Click += btnPreview_Click;

			printToolStripMenuItem.Click += btnPrintPreview_Click;

			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;

			listViewContents.SelectedIndexChanged += listViewContents_SelectedIndexChanged;
		}

		#endregion

		#region IMainView Members

		public string TitleText
		{
			get { return txtTitle.Text; }
			set { txtTitle.Text = value; }
		}

		public decimal XCoord
		{
			get { return txtX.Value; }
			set { txtX.Value = value; }
		}

		public decimal YCoord
		{
			get { return txtY.Value; }
			set { txtY.Value = value; }
		}

		public string Contents
		{
			get { return textBoxMain.Text; }
			set { textBoxMain.Text = value; }
		}

		public ListView.ListViewItemCollection Files
		{
			get { return listViewContents.Items; }
		}

		/// <summary>
		/// Enable controls if need be. For example, if no files are selected in the list view, disable the remove file button.
		/// </summary>
		public void EnableRequiredControls()
		{
			btnRemoveFile.Enabled = listViewContents.SelectedItems.Count != 0;
			btnPreview.Enabled = _model.CurrentFileName != null;
		}

		#endregion

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPreview_Click(object sender, EventArgs e)
		{
			_model.CurrentContents = Contents;
			var preview = new Preview(_model);

			preview.ShowDialog();
		}

		private void btnAudio_Click(object sender, EventArgs e)
		{
			_model.CurrentContents = Contents;
			var ctrlAudio = new AudioSelectionController(_model, new AudioSelectionDialog());
			ctrlAudio.InsertAudio();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbImage_Click(object sender, EventArgs e)
		{
			// Stores the current caret position and length of selection.
			int caretPos = textBoxMain.SelectionStart;
			//int selectionLength = textBoxMain.SelectionLength;

			var dialog = new ImageSelectionDialog(_model);
			if (dialog.ShowDialog() != DialogResult.Cancel)
			{
				textBoxMain.SelectionStart = caretPos;
				textBoxMain.SelectedText = string.Format("<img>{0}</img>", _model.ImageUrl);
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

		private void btnAddFile_Click(object sender, EventArgs e)
		{
			_controller.CreateNewFile();
			_controller.InitialiseListView();
		}

		/// <summary>
		/// 
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
				textBoxMain.Focus();
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

		private void listViewContents_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnRemoveFile.Enabled = ((ListView) sender).SelectedIndices.Count != 0;
		}

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

		private void btnPrintPreview_Click(object sender, EventArgs e)
		{
			printPreviewDialog.Document = printDocument;
			printPreviewDialog.ShowDialog();
		}

		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			var qr = new QrCodeGenerator();

			string text;
			Image code;
			var font = new Font(Font.FontFamily, 20);
			var fontColor = new SolidBrush(Color.Black); // For text
			Rectangle layoutRectangle;

			int j = 1;
			for (int i = 0; i < Files.Count; i++)
			{
				text = Files[i].Text;
				//code per page counter
				if (j > 4)
				{
					j = 1; /*create new page*/
					break;
				}
				code = qr.GenerateCode(text);
				//Even on left. Odd on right
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

		private void toolStripSync_Click(object sender, EventArgs e)
		{
			var ftpDialog = new FtpDialog(new FtpUploader());
			ftpDialog.ShowDialog();
		}

		private void MainWindow_Shown(object sender, EventArgs e)
		{
			var ftpDialog = new FtpDialog(new FtpDownloader(_model));
			ftpDialog.ShowDialog();
			_controller.InitialiseListView();
		}

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