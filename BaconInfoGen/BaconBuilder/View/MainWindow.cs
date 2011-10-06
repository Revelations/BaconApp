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

        public IHTMLDocument2 Document { get; set; }

		#region Constructors

		public MainWindow()
		{
			InitializeComponent();



			// Event binding
			tsbImage.Click += btnImage_Click;
			tsbAudio.Click += btnAudio_Click;
			tsbBold.Click += tsbBold_Click;
			tsbItalics.Click += tsbItalics_Click;
			btnPreview.Click += btnPreview_Click;

			printToolStripMenuItem.Click += btnPrintPreview_Click;

			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;

			listViewContents.SelectedIndexChanged += listViewContents_SelectedIndexChanged;


            browser.DocumentText = "<html><body></body></html>";
		    Document = browser.Document.DomDocument as IHTMLDocument2;

		    Document.designMode = "on";

            _model = new BaconModel();
            _controller = new MainViewController(_model, this);
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


        /// <summary>
        /// Wrap the selected text in bold tags.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbBold_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Wrap the selected text in italics.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbItalics_Click(object sender, EventArgs e)
        {

        }

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

		}

		/// <summary>
		/// Inserts an image wrapped in tags. TODO: Implement importing of foreign images.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnImage_Click(object sender, EventArgs e)
		{
		    
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
				browser.Focus();
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
			else if (_controller.ContentsHaveChanged())
			{
                _model.CurrentContents = Contents;
                _model.SaveFile(e.Item.Text);
                _model.LoadFiles();
			}
		}

		/// <summary>
		/// Shows a print preview.
		/// Russell
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrintPreview_Click(object sender, EventArgs e)
		{
			printPreviewDialog.Document = printDocument;
			printPreviewDialog.ShowDialog();
		}

		/// <summary>
		/// Print pages. TODO: Handle multi-page
		/// Russell
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			 // For text
           
            /*[PageSettings: Color=True,
             * Landscape=False,
             * Margins=[Margins Left=100 Right=100 Top=100 Bottom=100],
             * PaperSize=[PaperSize A4 Kind=A4 Height=1169 Width=827],
             * PaperSource=[PaperSource Default tray Kind=Upper],
             * PrinterResolution=[PrinterResolution X=300 Y=300]]
             */
			
            ArrangeQrCode(e,100,100);
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
            int pageheight = 1169 - my*2,
                pagewidth = 827 - mx*2, 
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
            float right2 = 827/2 + mx;
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
	           
	            if (j%2 == 1)
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
                e.Graphics.DrawString(text, font, fontColor, new RectangleF(xi, (float)((int)ys[j] + 90),500,100));
                e.Graphics.DrawImage(code,x,(float)( (int)ys[j] + 34.5));
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