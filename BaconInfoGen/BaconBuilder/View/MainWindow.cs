using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Windows.Forms;

using BaconBuilder.Controller;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class MainWindow : Form
	{
		private BaconModel model;

		public MainWindow()
		{
			InitializeComponent();

			this.model = new BaconModel();


			// Event binding
			this.Load += new EventHandler(MainWindow_Load);
			openFileToolStripMenuItem.Click += new EventHandler(openFileToolStripMenuItem_Click);
			btnPreview.Click += new System.EventHandler(btnPreview_Click);
			tsbImage.Click += new EventHandler(tsbImage_Click);

			btnMapPreview.Click += new EventHandler(btnMapPreview_Click);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void MainWindow_Load(object sender, System.EventArgs e)
		{
            MainViewController.InitialiseListView(listViewContents);
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

		private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() != DialogResult.Cancel)
			{
				model.OpenFile(ofd.FileName);
			}
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

			ImageSelectionDialog dialog = new ImageSelectionDialog(model);
			if (dialog.ShowDialog() != DialogResult.Cancel)
			{
				textBoxMain.SelectionStart = caretPos;
				textBoxMain.SelectedText = string.Format("<img>{0}</img>", model.ImageUrl);
			}
				
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			model.Contents = textBoxMain.Text;
			
			Preview preview = new Preview(model);
			preview.ShowDialog();
		}
	}
}


