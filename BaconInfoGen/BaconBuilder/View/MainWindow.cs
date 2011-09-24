using System;
using System.IO;
using System.Windows.Forms;

using BaconBuilder.Controller;
using BaconBuilder.Model;
using System.Drawing;

namespace BaconBuilder.View
{
	public partial class MainWindow : Form, IMainView
	{
		private readonly BaconModel _model;
		private readonly IMainViewController _controller;

		// Title before modification
		private string _cleanTitle = null;
		// Contents before modification
		private string _cleanContents = null;


		private string _currentFile = null;

		#region Constructors

		public MainWindow()
		{
			InitializeComponent();

			this._model = new BaconModel();
			this._controller = new MainViewController(_model, this);

			// Event binding
			btnPreview.Click += new System.EventHandler(btnPreview_Click);
			tsbImage.Click += new EventHandler(tsbImage_Click);
			btnMapPreview.Click += new EventHandler(btnMapPreview_Click);
		}

		#endregion

		#region Dialog Handling

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			_model.Contents = textBoxMain.Text;

			Preview preview = new Preview(null, 0, 0);
			preview.ShowDialog();
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
			int selectionLength = textBoxMain.SelectionLength;

			ImageSelectionDialog dialog = new ImageSelectionDialog(_model);
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
			throw new NotImplementedException("btnMapPreview_Click");
		}

		#endregion

		/// <summary>
		/// Called when the form is first loaded.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Load(object sender, System.EventArgs e)
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
			if (MessageBox.Show("Are you sure you wish to delete the file \"" + _currentFile + "\"?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				_controller.RemoveFile(_currentFile);

				_currentFile = null;

				_controller.InitialiseListView();
			}
		}

		private void txtTitle_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				txtTitle_FocusLeft(null, e);
			else if (e.KeyCode == Keys.Escape)
			{
				TitleText = _cleanTitle;
				textBoxMain.Focus();
			}
		}

		/// <summary>
		/// When user enters the title textbox, save the current name.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtTitle_Enter(object sender, EventArgs e)
		{
			_cleanTitle = TitleText;
		}

		/// <summary>
		/// When the user leaves the title textbox, handle any name changes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtTitle_FocusLeft(object sender, EventArgs e)
		{
			if (_currentFile == null) return;
			//Check if the new title is invalid (e.g. blank, only spaces)
			if (TitleText.Trim().Length == 0)
			{
				MessageBox.Show("Title cannot be blank or just spaces.");
				TitleText = _cleanTitle;
				return;
			}
			//Check if the text has changed.
			if (TitleText.Equals(_cleanTitle))
			{
				return;
			}


			// Since the title is valid and changed, we rename it.
			int index = FindItem(_cleanTitle + ".html");
			Console.WriteLine("Clean title = " + _cleanTitle);
			try
			{
				_controller.RenameFile(_cleanTitle, TitleText);
			}
			catch (IOException ex)
			{
				System.Console.WriteLine(ex.Message);
				MessageBox.Show(ex.Message, "Error");
				TitleText = _cleanTitle;
				return;
			}
			Files[index].Text = TitleText + ".html";

		}

		/// <summary>
		/// Returns the index of the listview item
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private int FindItem(string text)
		{
			ListView.ListViewItemCollection items = listViewContents.Items;

			for (int i = 0; i < items.Count; i++)
			{
				Console.WriteLine("Finding: Comparing {0} with {1}", text, items[i].Text);
				if (items[i].Text.Equals(text))
				{
					Console.WriteLine("Found!");
					return i;
				}
			}

			return -1;
		}

		private void listViewContents_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
				_cleanTitle = e.Item.Text;
				_cleanContents = _controller.LoadHtmlToText(e.Item.Text);
				TitleText = Path.GetFileNameWithoutExtension(_cleanTitle);
				Contents = _cleanContents;
				_currentFile = _cleanTitle;
			}
			else
			{
				// If contents have changed
				if (!_cleanContents.Equals(Contents))
				{
					_controller.SaveTextToHtml(e.Item.Text, Contents);
				}
			}
		}

		private void btnPrintPreview_Click(object sender, EventArgs e)
		{

			printPreviewDialog.Document = printDocument;
			printPreviewDialog.ShowDialog();

		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			QrCodeGenerator qr = new QrCodeGenerator();
			string text;
			Image code; //= qr.GenerateCode("Newcode");
			//e.Graphics.DrawImage(i, 50, 50);
			Font font = new System.Drawing.Font(Font.FontFamily, 20);
			SolidBrush brush = new SolidBrush(Color.Black);
			Rectangle layoutRectangle;// = new Rectangle(500, 100, 500, 100);
			//e.Graphics.DrawString("Newcode",font,brush,layoutRectangle);

			int j = 1;

			for (int i = 0; i < listViewContents.Items.Count; i++)
			{
				text = listViewContents.Items[i].Text;
				//code per page counter
				if (j > 4) { j = 1;/*create new page*/ break; }
				code = qr.GenerateCode(text);
				if (j % 2 == 1)//every second on on left
				{
					layoutRectangle = new Rectangle(400, 180 * j, 500, 100);
					e.Graphics.DrawImage(code, 100, 150 * j);
				}
				else
				{//others on right
					layoutRectangle = new Rectangle(200, 180 * j, 500, 100);
					e.Graphics.DrawImage(code, 500, 150 * j);
				}
				e.Graphics.DrawString(text, font, brush, layoutRectangle);

				// e.Graphics.DrawImage(code, (j/2) * 300, 100*j);
				j++;
			}
		}

		public string TitleText
		{
			get { return txtTitle.Text; }
			set { txtTitle.Text = value; }
		}

		public string XCoord
		{
			get { return txtX.Text; }
			set { txtX.Text = value; }
		}

		public string YCoord
		{
			get { return txtY.Text; }
			set { txtY.Text = value; }
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
	}
}


