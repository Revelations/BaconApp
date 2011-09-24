using System;
using System.IO;
using System.Windows.Forms;

using BaconBuilder.Controller;
using BaconBuilder.Model;
using System.Drawing;

namespace BaconBuilder.View
{
    public partial class MainWindow : Form
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
            _controller.InitialiseListView(listViewContents);
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            _controller.CreateNewFile();
            _controller.InitialiseListView(listViewContents);
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

                _controller.InitialiseListView(listViewContents);
            }
        }

        private void txtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTitle_FocusLeft(null, e);
            else if (e.KeyCode == Keys.Escape)
            {
                txtTitle.Text = _cleanTitle;
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
            _cleanTitle = txtTitle.Text;
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
            if (txtTitle.Text.Trim().Length == 0)
            {
                MessageBox.Show("Title cannot be blank or just spaces.");
                txtTitle.Text = _cleanTitle;
                return;
            }
            //Check if the text has changed.
            if (txtTitle.Text.Equals(_cleanTitle))
            {
                return;
            }


            // Since the title is valid and changed, we rename it.
            int index = FindItem(_cleanTitle + ".html");
            Console.WriteLine("Clean title = " + _cleanTitle);
            try
            {
                _controller.RenameFile(_cleanTitle, txtTitle.Text);
            }
            catch (IOException ex)
            {
                System.Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message, "Error");
                txtTitle.Text = _cleanTitle;
                return;
            }
            listViewContents.Items[index].Text = txtTitle.Text + ".html";

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
                txtTitle.Text = BaconModel.HtmlFileName(_cleanTitle);
                textBoxMain.Text = _cleanContents;
                _currentFile = _cleanTitle;
            }
            else
            {
                // If contents have changed
                if (!_cleanContents.Equals(textBoxMain.Text))
                {
                    _controller.SaveTextToHtml(BaconModel.HtmlFileName(e.Item.Text), textBoxMain.Text);
                }
            }
        }

        /// <summary>
        /// Called when the user changes selection in the list view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewContents_SelectedIndexChanged(object sender, EventArgs e)
        {
            //			//Deselected
            //			if (listViewContents.SelectedItems.Count == 0)
            //			{
            //			}
            //			// Make sure that something is selected.
            //			if (listViewContents.SelectedItems.Count == 1)
            //			{
            //				//_controller.ChangeSelection(listViewContents);
            //                
            //				// Make sure a file has been selected.
            //				if (_currentlyOpenedFile != null)
            //				{
            //					// Save the old file.
            //					_controller.SaveTextToHtml(_currentlyOpenedFile, textBoxMain.Text);
            //				}
            //				else
            //				{
            //					Console.WriteLine("WTF!");
            //				}
            //
            //				// If the file needs renaming, then do it.
            //                if (!string.IsNullOrWhiteSpace(_renamedFile))
            //                {
            //                    _controller.RenameFile(_currentlyOpenedFile, _renamedFile);
            //                    _renamedFile = string.Empty;
            //                }
            //
            //                // Get the name of the new file.
            //                _currentlyOpenedFile = listViewContents.SelectedItems[0].Text;
            //
            //                // Get the title of the new file and store it in the title textbox.
            //                txtTitle.Text = BaconModel.HtmlFileName(_currentlyOpenedFile);
            //
            //                // Load the new file.
            //                textBoxMain.Text = _controller.LoadHtmlToTextContents(_currentlyOpenedFile);
            //
            //				startingContents = textBoxMain.Text;
            //			}
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
    }
}


