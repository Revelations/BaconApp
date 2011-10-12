using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace BaconFeedback
{
	/// <summary>
	/// Class used to layout print documents prior to preview or printing.
	/// </summary>
	public class PrintHandler
	{
		// Create fonts and brush for drawing strings.
		private readonly List<FeedbackFile> _files;
		private readonly Font _fontHeader = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
		// Not really used at this point in time. Will be if multiple feedback reports are printed on a single page.
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

			Print(e, SkeletonFile(file));
		}

		private void Print(PrintPageEventArgs e, IEnumerable<Tuple<int, string, string>> pairs)
		{
			int yOffset = e.MarginBounds.Top;
			foreach (var tuple in pairs)
			{
				switch (tuple.Item1)
				{
					case 0:
						PrintEmptyLine(e, ref yOffset);
						break;
					case 1:
						PrintTwoColumn(e, tuple.Item2, tuple.Item3, ref yOffset);
						break;
					case 2:
						PrintSingleColumn(e, tuple.Item2, tuple.Item3, ref yOffset);
						break;
				}
			}
		}

		private IEnumerable<Tuple<int, string, string>> SkeletonFile(FeedbackFile file)
		{
			var lineBreak = new Tuple<int, string, string>(0, "", "");
			return new List<Tuple<int, string, string>>
			       	{
			       		new Tuple<int, string, string>(1, "Filename:", file.FileName),
			       		new Tuple<int, string, string>(1, "Directory:", file.Directory),
			       		new Tuple<int, string, string>(1, "Creation Date:", file.CreatedDate),
			       		lineBreak,
			       		new Tuple<int, string, string>(1, "Group Number:", file.Number),
			       		new Tuple<int, string, string>(1, "Nationality:", file.Nationality),
			       		lineBreak,
			       		new Tuple<int, string, string>(2, "What was seen:", file.Sighted),
			       		lineBreak,
			       		new Tuple<int, string, string>(2, "Miscelleneous:", file.Misc),
			       		lineBreak
			       	};
		}

		private void PrintEmptyLine(PrintPageEventArgs e, ref int yOffset)
		{
			if (e == null) return;
			yOffset += (int) e.Graphics.MeasureString(" ^________________^ ", _fontHeader).Height;
		}

		private void PrintTwoColumn(PrintPageEventArgs e, string head, string body, ref int yOffset)
		{
			int headOffset = yOffset;
			int bodyOffset = yOffset;
			PrintHead(e, head, ref headOffset);
			PrintBody(e, body, ref bodyOffset, 200);

			yOffset = Math.Max(headOffset, bodyOffset);
		}

		private void PrintSingleColumn(PrintPageEventArgs e, string head, string body, ref int yOffset)
		{
			PrintHead(e, head, ref yOffset);
			PrintBody(e, body, ref yOffset, 0);
		}

		private void PrintHead(PrintPageEventArgs e, string head, ref int yOffset)
		{
			e.Graphics.DrawString(head, _fontHeader, Brushes.Black, 
				new Rectangle(e.MarginBounds.X, yOffset, e.MarginBounds.Width, e.MarginBounds.Height));

			yOffset += (int) e.Graphics.MeasureString(head, _fontHeader, e.MarginBounds.Width).Height;
		}

		private void PrintBody(PrintPageEventArgs e, string body, ref int yOffset, int xOffset)
		{
			var stringFormat = new StringFormat();
			stringFormat.SetTabStops(xOffset, new float[1]);

			var font = new Font(FontFamily.GenericSansSerif, 12);
			e.Graphics.DrawString(body, font, Brushes.Black,
				new Rectangle(e.MarginBounds.X + xOffset, yOffset, e.MarginBounds.Width, e.MarginBounds.Height), stringFormat);

			yOffset += (int) e.Graphics.MeasureString(body, font, e.MarginBounds.Width, stringFormat).Height;
		}
	}
}