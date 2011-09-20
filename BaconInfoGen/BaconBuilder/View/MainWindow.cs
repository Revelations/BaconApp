using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Windows.Forms;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class MainWindow : Form
	{
		private BaconModel model;

		public MainWindow(BaconModel model)
		{
			InitializeComponent();

			this.model = model;

			myRTB.Rtf = @"{\rtf1\ansi{\fonttbl\f0\fswiss Helvetica;}\f0\pard
This is some \b\f1 bold\b0\f0  text.\par
\par
This is some {\i italic} {\b bold} text.\par
}";
			/*@"{\rtf
{\fonttbl {\f0 Times New Roman;}}
\f0\fs60 Hello, World!
} ";*/

			// Event binding
			this.Load += new EventHandler(MainWindow_Load);
			openFileToolStripMenuItem.Click += new EventHandler(openFileToolStripMenuItem_Click);
			btnPreview.Click += new System.EventHandler(btnPreview_Click);
			tsbImage.Click += new EventHandler(tsbImage_Click);
			tsbBold.Click += new EventHandler(tsbBold_Click);
			tsbItalics.Click += new EventHandler(tsbItalics_Click);
			btnMapPreview.Click += new EventHandler(btnMapPreview_Click);
		}

		private void MainWindow_Load(object sender, System.EventArgs e)
		{
			if (!Directory.Exists("./bin/dataFiles")) 
			{
				Directory.CreateDirectory("./bin/dataFiles");
			}

			//var directory = new DirectoryInfo("./html");
			var directory = new DirectoryInfo("./bin/dataFiles");
			FileHandler fh = new FileHandler(".html");
			List<string> dirPaths = fh.LoadDirectory(directory);

			foreach (string path in dirPaths)
			{
				treeDirectory.Nodes.Add(path);
			}
		}

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

		private void tsbImage_Click(object sender, EventArgs e)
		{
			// Stores the current caret position and length of selection.
			int caretPos = myRTB.SelectionStart;
			int selectionLength = myRTB.SelectionLength;

			model.SelectedRtf = myRTB.SelectedRtf;

			ImageSelectionDialog dialog = new ImageSelectionDialog(model);
			if (dialog.ShowDialog() != DialogResult.Cancel)
			{
				myRTB.SelectionStart = caretPos;
				myRTB.SelectedText = string.Format(@"<img src=""{0}"" />", model.ImageUrl);
			}
				
		}

		private void tsbBold_Click(object sender, EventArgs e)
		{
			myRTB.SelectionFont = GetSelectionFont(myRTB.SelectionFont, FontStyle.Bold, myRTB.SelectionFont.Bold);
		}

		private void tsbItalics_Click(object sender, EventArgs e)
		{
			myRTB.SelectionFont = GetSelectionFont(myRTB.SelectionFont, FontStyle.Italic, myRTB.SelectionFont.Italic);
		}

		/// <summary>
		/// Gets a font that has the style enabled or disabled.
		/// </summary>
		/// <param name="currFont">Font of currently selected text</param>
		/// <param name="style">Type of style</param>
		/// <param name="enabled">Whether the new font is to have this style enabled</param>
		/// <returns></returns>
		private Font GetSelectionFont(Font currFont, FontStyle style, bool enabled)
		{
			if (currFont == null)
			{
				Console.WriteLine("Current Selection Font is null : " + currFont);

				return null;
			}
			else
			{
				FontStyle newFont = (enabled)
					? currFont.Style & ~style
					: currFont.Style | style;

				return new Font(currFont, newFont);
			}
		}

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			model.Contents = myRTB.Text;
			//Console.WriteLine(rtbContents.Rtf);
			Preview preview = new Preview(model);
			preview.ShowDialog();
		}
	}
}


