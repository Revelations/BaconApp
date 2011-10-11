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
			string content = string.Format(Resources.LayoutTextFile, file.FileName, file.Directory, file.CreatedDate,
			                               file.Number, file.Nationality, file.Sighted);

			// Setup tab stops to format the page.
			var stringFormat = new StringFormat();
			stringFormat.SetTabStops(200, new float[1]);

			// Draw the page skeleton in bold.
			e.Graphics.DrawString(Resources.SkeletonPrintFile, _boldFont, _brush, e.MarginBounds);

			// Draw the laid-out fields.
			e.Graphics.DrawString(content, _font, _brush, e.MarginBounds, stringFormat);

			// Draw the Misc heading and content.
			int verticalPos = (int) e.Graphics.MeasureString(content, _font).Height + e.MarginBounds.Top;
			e.Graphics.DrawString("\r\nMiscellaneous Feedback:", _boldFont, _brush,
			                      new RectangleF(e.MarginBounds.X, verticalPos, e.MarginBounds.Width, e.MarginBounds.Height));

			verticalPos += (int) e.Graphics.MeasureString("\r\nMiscellaneous Feedback:", _boldFont).Height;
			e.Graphics.DrawString(file.Misc, _font, _brush,
			                      new RectangleF(e.MarginBounds.X, verticalPos, e.MarginBounds.Width, e.MarginBounds.Height));
		}
	}
}