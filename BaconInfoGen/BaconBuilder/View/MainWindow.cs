using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using BaconBuilder.Controller;
using BaconBuilder.Model;
using BaconBuilder.Model.Ftp;
using mshtml;
using NetworkCheckApp;

namespace BaconBuilder.View
{
	public partial class MainWindow : Form, IMainView
	{
		/// <summary>
		/// For reducing the stress on server. http://social.msdn.microsoft.com/Forums/is/clr/thread/a7a44123-937a-4b02-a918-042a881fa55f
		/// </summary>
		/// <returns></returns>
		[System.Diagnostics.Conditional("DEBUG")]
		private static void KeepServerOffline()
		{
			// TODO: Remove this before shipping off!!!
			MessageBox.Show(string.Format("Everytime this message is not shown, Ceiling Cat throttles the FTP server. Please, think of the kittens in {0}.", typeof(MainWindow)));
			OFFLINE = true;
		}
		private static bool OFFLINE = true;




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
			btnToggleEditMode.Enabled = true;
			btnToggleEditMode.Checked = HtmlBrowserEditable;
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

			//sets tooltips for buttons
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(btnAddFile,"Creates a new blank html for the editor");
			tooltip.SetToolTip(btnPreview, "Displays the QR code linked to the currently selected file");
			tooltip.SetToolTip(btnRemoveFile,"Deletes the selected file from the editor");
			tooltip.SetToolTip(btnPrintPreview, "Displays the print preview of the QR codes");

			mapBox.ZoomChanged += MapZoomChanged;
			mapBox.MapCoordinateChanged += MapCoordinateChanged;

			// Event binding.
			tsbImage.Click += btnImage_Click;
			tsbAudio.Click += btnAudio_Click;
			tsbBold.Click += btnBold_Click;
			tsbItalics.Click += btnItalics_Click;
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
			HTMLEditor.Navigating += HTMLEditor_Navigating;

			// Initialise MVP objects.
			_model = new BaconModel();
			_controller = new MainViewController(_model, this);
		}

		void HTMLEditor_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.ToString() != "about:blank")
				e.Cancel = true;
		}

		private void MapZoomChanged(object sender, EventArgs e)
		{
			lblZoom.Text = string.Format("{0}%", mapBox.Zoom);
		}

		private void MapCoordinateChanged(object sender, EventArgs e)
		{
			txtY.Value = mapBox.Y;
			txtX.Value = mapBox.X;
		}

		#endregion

		#region Toolbar Button and Other Button Events

		private void btnToggleEditMode_Click(object sender, EventArgs e)
		{
			HtmlBrowserEditable = !HtmlBrowserEditable;
			EnableRequiredControls();
		}

		/// <summary>
		/// Makes the selected HTMLEditor text bold, or toggles new text being typed in bold.
		/// </summary>
		private void btnBold_Click(object sender, EventArgs e)
		{
			Debug.Assert(HTMLEditor.Document != null, "HTMLEditor.Document != null");
			HTMLEditor.Document.ExecCommand("Bold", false, null);
		}

		/// <summary>
		/// Makes the selected HTMLEditor text italic, or toggles new text being typed in italic.
		/// </summary>
		private void btnItalics_Click(object sender, EventArgs e)
		{
			Debug.Assert(HTMLEditor.Document != null, "HTMLEditor.Document != null");
			HTMLEditor.Document.ExecCommand("Italic", false, null);
		}

		/// <summary>
		/// Makes the selected HTMLEditor text underlined, or toggles new text being typed underlined.
		/// </summary>
		private void btnUnderline_Click(object sender, EventArgs e)
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
			LogGenerator.createlog();
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

		private void MainWindow_Load(object sender, EventArgs e)
		{
			string uri;
			_hasConnection = ConnectionExists(out uri);
			if (!_hasConnection)
				MessageBox.Show(string.Format("Could not connect to {0}", uri), "Error");
		}

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
			string uri;
			_hasConnection = ConnectionExists(out uri);
			if (!_hasConnection) {
				MessageBox.Show(string.Format("Could not connect to {0}", uri), "Error");
				return;
			}
			// Promp user.
			DialogResult result =
				MessageBox.Show(
					@"Would you like to synchronise your content with the distribution server before exiting?",
					@"Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

			// Sync at user behest.
			if (result == DialogResult.Yes)
			{
				LogGenerator.createlog();
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
			var x = (int)Math.Min(Math.Max(0, txtX.Value), mapBox.Image.Width);
			var y = (int)Math.Min(Math.Max(0, txtY.Value), mapBox.Image.Height);

			mapBox.MoveTo(x, y);
			mapBox.Invalidate();
		}

		#endregion

		private static bool ConnectionExists(out string uri)
		{
//			String location = Resources.ServerLocation;
//			foreach (ConnectStatus.Method m in Enum.GetValues(typeof(ConnectStatus.Method)))
//			{
//				long start = System.DateTime.Now.Millisecond;
//				ConnectStatus.Check(m, Resources.ServerLocation, 81);
//				long end = System.DateTime.Now.Millisecond;
//				Console.WriteLine(@"{0} for {1} took {2}", m, location, (end - start));
//			}
//			return false;

			uri = FtpHelper.FtpUri();
			KeepServerOffline();
			if (OFFLINE)
				return false;
			return ConnectStatus.Check() && ConnectStatus.Check(ConnectStatus.Method.TcpSocket, uri, FtpHelper.FtpPort);
		}

//		private static void TestConnection()
//		{
//			string http = "http://revelations.webhop.org/";
//			foreach (ConnectStatus.Method m in Enum.GetValues(typeof(ConnectStatus.Method)))
//			{
//				Console.WriteLine(m.ToString());
//				DateTime start;
//				if (m.Equals(ConnectStatus.Method.Ping))
//				{
//					var adds = Dns.GetHostAddresses(new Uri(http).Host);
//
//					foreach (var t in adds)
//					{
//						start = DateTime.Now;
//						bool b = ConnectStatus.Check(m, t.ToString(), 81);
//						Console.WriteLine(@"{0} for {1} took {2}ms. Status = {3}", m, t.ToString(), (DateTime.Now - start).TotalMilliseconds, b);
//					}
//				}
//				else if (m.Equals(ConnectStatus.Method.WebRequest))
//				{
//					var loc = new Uri(http).AbsoluteUri;
//					start = DateTime.Now;
//					bool b = ConnectStatus.Check(m, loc, 81);
//					Console.WriteLine(@"{0} for {1} took {2}ms. Status = {3}", m, loc, (DateTime.Now - start).TotalMilliseconds, b);
//				}
//				else
//				{
//					var loc = new Uri(http).AbsoluteUri;
//					start = DateTime.Now;
//					bool b = ConnectStatus.Check(m, loc, 81);
//					Console.WriteLine(@"{0} for {1} took {2}ms. Status = {3}", m, loc, (DateTime.Now - start).TotalMilliseconds, b);
//				}
//			}
//
//			string ftp = Resources.ServerLocation;
//			foreach (ConnectStatus.Method m in Enum.GetValues(typeof(ConnectStatus.Method)))
//			{
//				Console.WriteLine(m.ToString());
//				DateTime start;
//				if (m.Equals(ConnectStatus.Method.Ping))
//				{
//					var adds = Dns.GetHostAddresses(new Uri(ftp).Host);
//
//					foreach (var t in adds)
//					{
//						start = DateTime.Now;
//						bool b = ConnectStatus.Check(m, t.ToString(), 81);
//						Console.WriteLine(@"{0} for {1} took {2}ms. Status = {3}", m, t.ToString(), (DateTime.Now - start).TotalMilliseconds, b);
//					}
//				}
//				else if (m.Equals(ConnectStatus.Method.WebRequest))
//				{
//					var loc = new Uri(ftp).AbsoluteUri;
//					start = DateTime.Now;
//					bool b = ConnectStatus.Check(m, loc, 81);
//					Console.WriteLine(@"{0} for {1} took {2}ms. Status = {3}", m, loc, (DateTime.Now - start).TotalMilliseconds, b);
//				}
//				else
//				{
//					var loc = new Uri(ftp).AbsoluteUri;
//					start = DateTime.Now;
//					bool b = ConnectStatus.Check(m, loc, 81);
//					Console.WriteLine(@"{0} for {1} took {2}ms. Status = {3}", m, loc, (DateTime.Now - start).TotalMilliseconds, b);
//				}
//			}
//
//		}

		#region Russell's Print Stuff.

		// TODO: Refactor the below to a method that takes margins
		private int _codecount;

		/// <summary>
		/// Print multiple pages.
		/// Russell
		/// </summary>
		private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			//Page width = 827
			//Page height = 1169

			int mx = e.MarginBounds.Left;
			var qr = new QrCodeGenerator();
			var font = new Font(Font.FontFamily, 20);
			var fontColor = new SolidBrush(Color.Black);

			//drawing lines for now
			int pageheight = e.MarginBounds.Height;
			float right = e.PageBounds.Width - 172 - mx;
			float left = e.PageBounds.Width / 2 + mx;
			float yline = e.MarginBounds.Top;
			int linediff = pageheight / 4;
			for (; _codecount < Files.Count; _codecount++)
			{
				float qrImageX, qrTextX;
				string text = Files[_codecount].Text;
				text = text.Substring(0, text.Length - 5);

				if (yline + linediff > e.MarginBounds.Bottom)
				{
					e.HasMorePages = true;
					return;
				}
				Image code = qr.GenerateCode(text);
				//Even on left. Odd on right
				if (_codecount % 2 == 0)
				{
					qrImageX = mx;
					qrTextX = left;
				}
				else
				{
					qrImageX = right;
					qrTextX = mx;
				}

				e.Graphics.DrawString(text, font, fontColor, new RectangleF(qrTextX, ((int)yline + 90), 500, 100));
				e.Graphics.DrawImage(code, qrImageX, (float)((int)yline + 34.5));

				yline += linediff;
			}

			e.HasMorePages = false;
		}

		#endregion
	}
}