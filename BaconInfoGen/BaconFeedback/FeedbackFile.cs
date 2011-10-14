using System.Collections.Generic;

namespace BaconFeedback
{
	/// <summary>
	/// Struct that stores information about a feedback file.
	/// </summary>
	public struct FeedbackFile
	{
		// Filename of the file.
		public string FileName { get; set; }

		// Directory it resides in.
		public string Directory { get; set; }

		// Date that the feedback was created.
		public string CreatedDate { get; set; }

		// Number of group members specified in the feedback.
		public string Number { get; set; }

		// Dominant nationality of group members specified in the feedback.
		public string Nationality { get; set; }

		// What was seen according to the feedback.
		public string Sighted { get; set; }

		// Miscellaneous feedback specified in the file.
		public string Misc { get; set; }

		// Collection of all barcodes scanned.
		public List<string> Scanned { get; set; }
	}
}