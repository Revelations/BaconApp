using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;
using BaconBuilder.Controller;
using BaconBuilder.Model;
using BaconBuilder.Properties;
using mshtml;

namespace BaconBuilder.View
{
	public partial class MainWindow : Form, IMainView
	{
		private readonly MainViewController _controller;
		private readonly BaconModel _model;
		private bool _hasConnection;

		private bool HtmlBrowserEditable
		{
			get
			{
				Debug.Assert(HTMLEditor.Document != null, "HTMLEditor.Document != null");
				var doc = HTMLEditor.Document.DomDocument as IHTMLDocument2;
				Debug.Assert(doc != null, "doc != null");
				string mode = doc.designMode.ToLower();
				return (mode.Equals("on") && !mode.Equals("off"));
			}
			set
			{
				Debug.Assert(HTMLEditor.Document != null, "HTMLEditor.Document != null");
				var doc = HTMLEditor.Document.DomDocument as IHTMLDocument2;
				Debug.Assert(doc != null, "doc != null");
				if (value)
				{
					string contents = HTMLEditor.DocumentText;
					doc.designMode = "on";
					doc.write(contents);
				}
				else
				{
					string contents = HTMLEditor.DocumentText;
					doc.designMode = "off";
					if (HTMLEditor.Document != null)
					{
						HTMLEditor.Document.OpenNew(false);
						HTMLEditor.Document.Write(contents);
					}
				}
			}
		}

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
			get { return HTMLEditor.DocumentText; }
			set
			{
				Debug.Assert(HTMLEditor.Document != null, "HTMLEditor.Document != null");
				HTMLEditor.Document.OpenNew(true);
				HTMLEditor.Document.Write(value);
			}
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
			foreach (ToolStripButton item in toolContents.Items.OfType<ToolStripButton>())
			{
				item.Enabled = _model.CurrentFileName != null && HtmlBrowserEditable;
			}
			toolStripButton1.Enabled = true;
			toolStripButton1.Checked = HtmlBrowserEditable;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor for the main form.
		/// </summary>
		public MainWindow()
		{
			// Initialise form controls.
			InitializeComponent();
			//txtX.Minimum = 0;
			//txtY.Minimum = 0;

            mapBox.ZoomChanged += MapZoomChanged;
			mapBox.MapCoordinateChanged += MapCoordinateChanged;

			// Event binding.
			tsbImage.Click += btnImage_Click;
			tsbAudio.Click += btnAudio_Click;
			tsbBold.Click += tsbBold_Click;
			tsbItalics.Click += tsbItalics_Click;
			btnPreview.Click += btnPreview_Click;

			printToolStripMenuItem.Click += btnPrintPreview_Click;
			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
			listViewContents.SelectedIndexChanged += listViewContents_SelectedIndexChanged;

			Load += MainWindow_Load;

			// Initialise HTMLEditor.
			HTMLEditor.Navigate("about:blank");
			Debug.Assert(HTMLEditor.Document != null, "HTMLEditor.Document != null");
			HtmlDocument htmlDocument = HTMLEditor.Document.OpenNew(false);
			Debug.Assert(htmlDocument != null, "htmlDocument != null");
			htmlDocument.Write("<html><body></body></html>");
			var doc = HTMLEditor.Document.DomDocument as IHTMLDocument2;
			Debug.Assert(doc != null, "doc != null");
			doc.designMode = "on";

			// Initialise MVP objects.
			_model = new BaconModel();
			_controller = new MainViewController(_model, this);
		}

        private void MapZoomChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = string.Format("{0}%", mapBox.Zoom);
        }

		private void MapCoordinateChanged(object sender, EventArgs e)
		{
            txtY.Value = mapBox.Y;
            txtX.Value = mapBox.X;
		}

		#endregion

		#region Toolbar Button Events

		/// <summary>
		/// Makes the selected HTMLEditor text bold, or toggles new text being typed in bold.
		/// </summary>
		private void tsbBold_Click(object sender, EventArgs e)
		{
			Debug.Assert(HTMLEditor.Document != null, "HTMLEditor.Document != null");
			HTMLEditor.Document.ExecCommand("Bold", false, null);
		}

		/// <summary>
		/// Makes the selected HTMLEditor text italic, or toggles new text being typed in italic.
		/// </summary>
		private void tsbItalics_Click(object sender, EventArgs e)
		{
			Debug.Assert(HTMLEditor.Document != null, "HTMLEditor.Document != null");
			HTMLEditor.Document.ExecCommand("Italic", false, null);
		}

		/// <summary>
		/// Makes the selected HTMLEditor text underlined, or toggles new text being typed underlined.
		/// </summary>
		private void btn_Underline_Click(object sender, EventArgs e)
		{
			Debug.Assert(HTMLEditor.Document != null, "HTMLEditor.Document != null");
			HTMLEditor.Document.ExecCommand("Underline", false, null);
		}

		/// <summary>
		/// Opens a media selection dialog for the user to insert an audio file.
		/// </summary>
		private void btnAudio_Click(object sender, EventArgs e)
		{
			// TODO: Implement audio selection logic here.
			throw new NotImplementedException();
		}

		/// <summary>
		/// Opens a media selection dialog for the user to insert an image file.
		/// </summary>
		private void btnImage_Click(object sender, EventArgs e)
		{
			var msd = new MediaSelectionDialog(_model, ContentType.Image);
			if (msd.ShowDialog() == DialogResult.OK)
			{
				Debug.Assert(HTMLEditor.Document != null, "HTMLEditor.Document != null");
				HTMLEditor.Document.ExecCommand("InsertImage", false, _model.ImageUrl);
			}
		}

		#endregion

		#region Other Button Events

		/// <summary>
		/// Preview html, map page, and QR code in a dialog.
		/// </summary>
		private void btnPreview_Click(object sender, EventArgs e)
		{
			// TODO: Change preview dialog to show only QR code.
			_model.CurrentContents = Contents;
			var preview = new Preview(_model);

			preview.ShowDialog();
		}

		/// <summary>
		/// Brings up a print preview dialog for all QR codes in the listview.
		/// </summary>
		private void btnPrintPreview_Click(object sender, EventArgs e)
		{
			printPreviewDialog.Document = printDocument;
			_codecount = 0;
			printPreviewDialog.ShowDialog();
		}

		/// <summary>
		/// Create a new html file and add it to the list view.
		/// </summary>
		private void btnAddFile_Click(object sender, EventArgs e)
		{
			_controller.CreateNewFile();
			_controller.InitialiseListView();
		}

		/// <summary>
		/// Delete an html file from the file system and listview.
		/// </summary>
		private void btnRemoveFile_Click(object sender, EventArgs e)
		{
			// Prompt the user for confirmation.
			string message = string.Format(@"Are you sure you wish to delete the file ""{0}""?", _model.CurrentFileName);
			if (MessageBox.Show(message, @"Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				_controller.RemoveCurrentFile();
				_controller.RefreshDirectory();
				txtTitle.Text = "";
				txtX.Text = "";
				txtY.Text = "";
			}
		}

		/// <summary>
		/// Initialises an FTP uploader to synchronise the remote server directory with the local working folder.
		/// </summary>
		private void toolStripSync_Click(object sender, EventArgs e)
		{
			var ftpDialog = new FtpDialog(new FtpUploader());
			ftpDialog.ShowDialog();
		}

		/// <summary>
		/// Exits the program.
		/// </summary>
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		#endregion

		#region Other Events

		/// <summary>
		/// Called when the main window is first shown.
		/// 
		/// Creates an FTP downloader to sync the working directory with the webserver. Also initialises
		/// files in the listview.
		/// </summary>
		private void MainWindow_Shown(object sender, EventArgs e)
		{
			if (_hasConnection)
			{
				var ftpDialog = new FtpDialog(new FtpDownloader(_model));
				ftpDialog.ShowDialog();
			}
			_controller.InitialiseListView();
		}

		/// <summary>
		/// Called when the form is closing.
		/// 
		/// Prompts the user for synchronisation. Creates an FTP uploader to sync the webserver with the working directory.
		/// </summary>
		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Promp user.
			DialogResult result =
				MessageBox.Show(
					@"Would you like to synchronise your content with the distribution server before exiting?",
					@"Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

			// Sync at user behest.
			if (result == DialogResult.Yes)
			{
				var ftpDialog = new FtpDialog((new FtpUploader()));
				ftpDialog.ShowDialog();
			}

			// Cancel if this was all a horrible mistake.
			if (result == DialogResult.Cancel)
				e.Cancel = true;
		}

		/// <summary>
		/// Handle confirmation or denial of any name change in the title textbox.
		/// </summary>
		private void txtTitle_KeyDown(object sender, KeyEventArgs e)
		{
			// If enter is pushed, then attempt to push the name change.
			if (e.KeyCode == Keys.Enter)
				txtTitle_FocusLeft(null, e);

				// If escape is pushed, abort the name change.
			else if (e.KeyCode == Keys.Escape)
			{
				TitleText = _model.CurrentFileName;
				HTMLEditor.Focus();
			}
		}

		/// <summary>
		/// When the user leaves the title textbox, handle any name changes.
		/// </summary>
		private void txtTitle_FocusLeft(object sender, EventArgs e)
		{
			_controller.ValidateTitle();
		}

		/// <summary>
		/// Enables or disables various controls when the listview's selected index changes.
		/// </summary>
		private void listViewContents_SelectedIndexChanged(object sender, EventArgs e)
		{
			EnableRequiredControls();
		}

		/// <summary>
		/// Called once for each file selected or deselected when the listview's item selection is changed.
		/// </summary>
		private void listViewContents_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			// If this item is newly selected, load the file associated with it.
			if (e.IsSelected)
			{
				_controller.SelectFile(e.Item.Text);
			}

			// If this item is being unselected, then save it if necessary.
			else if (_controller.ContentsHaveChanged())
			{
				_model.CurrentContents = Contents;
				_model.SaveFile(e.Item.Text);
				_model.LoadFiles();
			}
		}

		/// <summary>
		/// Called when the numeric up/down boxes have their value changed.
		/// 
		/// Constrains those values to lie within a boundary given by the picture box size.
		/// Redraws and invalidates the picture box.
		/// </summary>
		private void txt_ValueChanged(object sender, EventArgs e)
		{
			int x = (int)Math.Min(Math.Max(0, txtX.Value), mapBox.Image.Width);
			int y = (int)Math.Min(Math.Max(0, txtY.Value), mapBox.Image.Height);

			mapBox.MoveTo(x, y);
			mapBox.Invalidate();
		}

		#endregion

		private static bool ConnectionExists()
		{
			try
			{
				var uri = new Uri(Resources.ServerLocation);
				var clnt = new TcpClient(uri.Host, 81);
				clnt.Close();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			_hasConnection = ConnectionExists();
            if (!_hasConnection)
                MessageBox.Show("Could not connect");
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			HtmlBrowserEditable = !HtmlBrowserEditable;
			EnableRequiredControls();
		}

		#region Russell's Print Stuff.
		
		// TODO: Refactor the below to a method that takes margins
		int _codecount = 0;
		/// <summary>
		/// Print multiple pages.
		/// Russell
		/// </summary>
		private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			int mx = 100, my =100;
			var qr = new QrCodeGenerator();
			var font = new Font(Font.FontFamily, 20);
			var fontColor = new SolidBrush(Color.Black);

			//drawing lines for now
			int pageheight = 1169 - my * 2,
				pagewidth = 827 - mx * 2;
/*
				x1 = mx,
				x2 = pagewidth + mx,
				y1 = my,
				y2 = pageheight + my,
				y3 = (pageheight/2) + my,
				y4 = pageheight/4 + my,
				y5 = pageheight - (pageheight/4) + my;
			lines drawn
			e.Graphics.DrawLines(Pens.Black,
								 new[]
									{new Point(x1, y1), new Point(x1, y2), new Point(x2, y2), new Point(x2, y1), new Point(x1, y1)});
			//draws the margins in

			e.Graphics.DrawLine(Pens.Black, x1, y3, x2, y3); //middleline
			e.Graphics.DrawLine(Pens.Black, x1, y4, x2, y4); //topmidline
			e.Graphics.DrawLine(Pens.Black, x1, y5, x2, y5); //bottommidline
			*/
			float right = 827 - 172 - mx;
			float left = 827/2 + mx;
			float yline = e.MarginBounds.Top;
			int linediff = pageheight / 4;
			for (; _codecount< Files.Count; _codecount++)
			{
				float x, xi;
				string text = Files[_codecount].Text;

				if (yline + linediff > e.MarginBounds.Bottom)
				{
					e.HasMorePages = true;
					return;
				}
				Image code = qr.GenerateCode(text);
				//Even on left. Odd on right
				if (_codecount%2 == 0)
				{
					x = mx;
					xi = left;
				}
				else
				{
					xi = mx;
					x = right;
				}
				e.Graphics.DrawString(text, font, fontColor, new RectangleF(xi, ((int) yline + 90), 500, 100));
				e.Graphics.DrawImage(code, x, (float) ((int) yline + 34.5));
				
				yline += linediff;   
			}

			e.HasMorePages = false;
		}

		#endregion
	}
}