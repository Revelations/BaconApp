using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using BaconFeedback.Properties;

namespace BaconFeedback
{
	/// <summary>
	/// Class used to layout print documents prior to preview or printing.
	/// </summary>
	public class PrintHandler
	{
		// Create fonts and brush for drawing strings.
		private readonly Brush _brush = Brushes.Black;
		private readonly Font _font = new Font(FontFamily.GenericSansSerif, 12);
		private readonly Font _boldFont = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
		// Not really used at this point in time. Will be if multiple feedback reports are printed on a single page.
		private readonly List<FeedbackFile> _files;
		private readonly int _numPages;
		// The current page being laid out for printing.
		private int _currentPage = 1;

		// List of all feedback files requested for printing.

		/// <summary>
		/// Constructor accepting a single argument.
		/// </summary>
		/// <param name="files">List of feedback files requested for printing.</param>
		public PrintHandler(List<FeedbackFile> files)
		{
			// Assign args to private members.
			_files = files;
			_numPages = files.Count;
		}

		/// <summary>
		/// Builds a multi page print document for display in a print preview, or for printing directly.
		/// 
		/// This will be called as many times as there are pages in the print document.
		/// </summary>
		/// <param name="e"></param>
		public void ConstructPrintDocument(PrintPageEventArgs e)
		{
			// Layout a single page for printing.
			ConstructSingleFeedback(e);

			// Call this again.
			if (_currentPage < _numPages)
				e.HasMorePages = true;

			// Increment current page.
			_currentPage++;
		}

		/// <summary>
		/// Creates a print layout for a single feedback file.
		/// </summary>
		/// <param name="e">PrintPageEventArgs used for layout.</param>
		private void ConstructSingleFeedback(PrintPageEventArgs e)
		{
			// Get the feedback file that belongs on this page.
			FeedbackFile file = _files[_currentPage - 1];

			// Build the content and layout for the feedback data.
			// TODO: Reintroduce the skeletons and layout format to let the program decide columns -- Shii
			int yOffset = e.MarginBounds.Top;
			yOffset = PrintTwoColumn(e, "Filename:", file.FileName, yOffset);
			yOffset = PrintTwoColumn(e, "Directory:", file.Directory, yOffset);
			yOffset = PrintTwoColumn(e, "Creation Date:", file.CreatedDate, yOffset);
			yOffset = PrintTwoColumn(e, " ", " ", yOffset);
			yOffset = PrintTwoColumn(e, "Group number:", file.Number, yOffset);
			yOffset = PrintTwoColumn(e, "Nationality:", file.Nationality, yOffset);
			yOffset = PrintTwoColumn(e, " ", " ", yOffset);
			yOffset = PrintSingleColumn(e, "What was seen:", file.Sighted, yOffset);
			yOffset = PrintTwoColumn(e, " ", " ", yOffset);
			yOffset = PrintSingleColumn(e, "Misc", file.Misc, yOffset);
		}


		private int PrintTwoColumn(PrintPageEventArgs e, string head, string body, int yOffset)
		{
			int headOffset = PrintHead(e, head, yOffset);

			int bodyOffset = PrintBody(e, body, yOffset, 200);

			return Math.Max(headOffset, bodyOffset);
		}

		private int PrintSingleColumn(PrintPageEventArgs e, string head, string body, int yOffset)
		{
			int headOffset = PrintHead(e, head, yOffset);

			int bodyOffset = PrintBody(e, body, headOffset, 0);

			return bodyOffset;
		}

		private int PrintHead(PrintPageEventArgs e, string head, int yOffset)
		{
			SizeF textBounds = e.Graphics.MeasureString(head, _boldFont, e.MarginBounds.Width);
			var layoutRectangle = new Rectangle(e.MarginBounds.X, yOffset, e.MarginBounds.Width, e.MarginBounds.Height);
			var headOffset = textBounds.Height;
			e.Graphics.DrawString(head, _boldFont, _brush, layoutRectangle);

			return (int) (yOffset + headOffset);			
		}

		private int PrintBody(PrintPageEventArgs e, string body, int yOffset, int xOffset)
		{
			var stringFormat = new StringFormat();
			stringFormat.SetTabStops(xOffset, new float[1]);
			var layoutRectangle = new Rectangle(e.MarginBounds.X + xOffset, yOffset, e.MarginBounds.Width, e.MarginBounds.Height);
			var bodyOffset = e.Graphics.MeasureString(body, _font, e.MarginBounds.Width, stringFormat).Height;
			e.Graphics.DrawString(body, _font, _brush, layoutRectangle, stringFormat);

			return (int)(yOffset + bodyOffset);

		}


	}
}