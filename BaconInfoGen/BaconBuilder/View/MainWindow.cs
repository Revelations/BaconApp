using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BaconBuilder.Controller;
using mshtml;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class MainWindow : Form, IMainView
	{
        private readonly MainViewController _controller;
        private readonly BaconModel _model;

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
            get { return browser.DocumentText; }
            set
            {
                browser.Document.OpenNew(true);
                browser.DocumentText = value;
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
            toolContents.Enabled = _model.CurrentFileName != null;
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

			// Event binding.
			tsbImage.Click += btnImage_Click;
			tsbAudio.Click += btnAudio_Click;
			tsbBold.Click += tsbBold_Click;
			tsbItalics.Click += tsbItalics_Click;
			btnPreview.Click += btnPreview_Click;

			printToolStripMenuItem.Click += btnPrintPreview_Click;
			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
			listViewContents.SelectedIndexChanged += listViewContents_SelectedIndexChanged;

            // Initialise browser.
            browser.DocumentText = "<html><body></body></html>";
            IHTMLDocument2 doc = browser.Document.DomDocument as IHTMLDocument2;
		    doc.designMode = "on";

            // Initialise MVP objects.
            _model = new BaconModel();
            _controller = new MainViewController(_model, this);
		}

		#endregion

        #region Toolbar Button Events

        /// <summary>
        /// Makes the selected browser text bold, or toggles new text being typed in bold.
        /// </summary>
        private void tsbBold_Click(object sender, EventArgs e)
        {
            browser.Document.ExecCommand("Bold", false, null);
        }

        /// <summary>
        /// Makes the selected browser text italic, or toggles new text being typed in italic.
        /// </summary>
        private void tsbItalics_Click(object sender, EventArgs e)
        {
            browser.Document.ExecCommand("Italic", false, null);
        }

        /// <summary>
        /// Makes the selected browser text underlined, or toggles new text being typed underlined.
        /// </summary>
        private void btn_Underline_Click(object sender, EventArgs e)
        {
            browser.Document.ExecCommand("Underline", false, null);
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
            MediaSelectionDialog msd = new MediaSelectionDialog(_model, ContentType.Image);
            if (msd.ShowDialog() == DialogResult.OK)
            {
                browser.Document.ExecCommand("InsertImage", false, _model.ImageUrl);
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
            var ftpDialog = new FtpDialog(new FtpDownloader(_model));
            ftpDialog.ShowDialog();
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
                browser.Focus();
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

        #endregion

        // TODO: Refactor the below to a new class.
        #region Russell's Print Stuff.

        /// <summary>
        /// Print pages. TODO: Handle multi-page
        /// Russell
        /// </summary>
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            ArrangeQrCode(e, 100, 100);
        }

        /// <summary>
        /// Arranges the qr codes on the page
        /// Russell
        /// </summary>
        /// <param name="e">printeventargs from the print document</param>
        /// <param name="mx">X margin size</param>
        /// <param name="my">Y margin size</param>
        private void ArrangeQrCode(PrintPageEventArgs e, int mx, int my)
        {
            var qr = new QrCodeGenerator();
            var font = new Font(Font.FontFamily, 20);
            var fontColor = new SolidBrush(Color.Black);

            int j = 1;
            //drawing lines for now
            int pageheight = 1169 - my * 2,
                pagewidth = 827 - mx * 2,
                x1 = mx,
                x2 = pagewidth + mx,

                y1 = my,
                y2 = pageheight + my,
                y3 = (pageheight / 2) + my,
                y4 = pageheight / 4 + my,
                y5 = pageheight - (pageheight / 4) + my;
            //makes and arraylist for my variables cos i can
            ArrayList ys = new ArrayList(5);
            ys.Add(0);
            ys.Add(y1);
            ys.Add(y4);
            ys.Add(y3);
            ys.Add(y5);
            //lines drawn
            e.Graphics.DrawLines(Pens.Black, new[] { new Point(x1, y1), new Point(x1, y2), new Point(x2, y2), new Point(x2, y1), new Point(x1, y1) });//draws the margins in


            e.Graphics.DrawLine(Pens.Black, x1, y3, x2, y3);//middleline
            e.Graphics.DrawLine(Pens.Black, x1, y4, x2, y4);//topmidline
            e.Graphics.DrawLine(Pens.Black, x1, y5, x2, y5);//bottommidline

            Rectangle layoutRectangle;
            float right = 827 - 172 - mx;
            float right2 = 827 / 2 + mx;
            for (int i = 0; i < Files.Count; i++)
            {
                float x, xi;
                string text = Files[i].Text;
                //code per page counter
                if (j > 4)
                {
                    j = 1; /*create new page*/
                    break;
                }
                Image code = qr.GenerateCode(text);
                //Even on left. Odd on right
                Console.WriteLine(code.Width);

                if (j % 2 == 1)
                {
                    //layoutRectangle = new Rectangle(350, 180*j, 500, 100);
                    x = mx;
                    xi = right2;
                }
                else
                {
                    //layoutRectangle = new Rectangle(250, 180*j, 500, 100);
                    xi = mx;
                    x = right;
                }
                e.Graphics.DrawString(text, font, fontColor, new RectangleF(xi, (float)((int)ys[j] + 90), 500, 100));
                e.Graphics.DrawImage(code, x, (float)((int)ys[j] + 34.5));
                j++;
            }
        }

        #endregion
	}
}