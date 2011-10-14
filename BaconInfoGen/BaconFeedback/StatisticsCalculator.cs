using System;
using System.Collections.Generic;
using System.Linq;

namespace BaconFeedback
{
	/// <summary>
	/// Class that calculates various statistics about a group of feedback files.
	/// </summary>
	public class StatisticsCalculator
	{
		// List of files to aggregate statistics for.
		private readonly List<FeedbackFile> _files;

		/// <summary>
		/// Constructor accepting a single argument.
		/// </summary>
		/// <param name="files">List of files to aggregate statistics for.</param>
		public StatisticsCalculator(List<FeedbackFile> files)
		{
			_files = files;
		}

		/// <summary>
		/// Returns the number of feedback files present in the selection.
		/// </summary>
		/// <returns></returns>
		public int FeedbackQuantity
		{
			get { return _files.Count; }
		}

		/// <summary>
		/// Gets the total number of visitors over the set of feedback files.
		/// </summary>
		public int TotalVisitors
		{
			get { return _files.Sum(file => Convert.ToInt32(file.Number)); }
		}

		/// <summary>
		/// Gets the total number of codes scanned over the set of files.
		/// </summary>
		public int TotalScanned
		{
			get { return _files.Sum(file => file.Scanned.Count); }
		}

		/// <summary>
		/// Gets the average number of codes scanned over the set of files.
		/// </summary>
		public float AverageScanned
		{
			get { return (float)TotalScanned / _files.Count; }
		}

		public int LeastScanned
		{
			get { return _files.Min(file => file.Scanned.Count); }
		}

		public int MostScanned
		{
			get { return _files.Max(file => file.Scanned.Count); }
		}

		/// <summary>
		/// Gets the number of visitors in the requested feedback files. Attempts to exclude duplicates by looking for similar files and ignoring them.
		/// </summary>
		public int TotalVisitorsExcludingDuplicates
		{
			// TODO: Implementation.
			get { return 0; }
		}

		/// <summary>
		/// Gets the average group size from a group of files.
		/// </summary>
		public float AverageGroupSize
		{
			get { return (float) TotalVisitors/_files.Count; }
		}

		/// <summary>
		/// Gets the size of the largest group of people from the selected files.
		/// </summary>
		public int LargestGroup
		{
			get { return _files.Select(f => Convert.ToInt32(f.Number)).Concat(new[] {0}).Max(); }
		}

		private IEnumerable<KeyValuePair<string, int>> MostCommonNationalities
		{
			get
			{
				// Init a dictionary to store quantities of people of varying nationalities.
				Dictionary<string, int> dictionary = GetNationalityDictionary();
				int max = dictionary.Values.Max();

				return dictionary.Where(kvp => max == kvp.Value);
			}
		}

		/// <summary>
		/// Gets a key value pair containing the most common nationality present in a group of feedback files, as well as the
		/// quantity of visitors specifying that same nationality.
		/// </summary>
		/// <returns>Most common nationality of visitors, quantity of that nationality.</returns>
		public KeyValuePair<string, int> MostCommonNationality
		{
			get { return MostCommonNationalities.First(); }
		}

		private Dictionary<string, int> GetNationalityDictionary()
		{
			var dictionary = new Dictionary<string, int>();

			// For each file...
			foreach (FeedbackFile f in _files)
			{
				int old;
				dictionary.TryGetValue(f.Nationality, out old);
				dictionary[f.Nationality] = old + Convert.ToInt32(f.Number);
			}

			return dictionary;
		}

		public Dictionary<string, int> GetScannedDictionary()
		{
			var dictionary = new Dictionary<string, int>();

			foreach (FeedbackFile f in _files)
			{
				foreach (string s in f.Scanned)
				{
					int old;
					dictionary.TryGetValue(s, out old);
					dictionary[s] = old + 1;
				}
			}

			return dictionary;
		}

		public string GetNthMostScanned(int n)
		{
			return n >= 0 && n < MostCommonScanned.Count() ? MostCommonScanned.ElementAt(n).Key : string.Empty;
		}

		private IEnumerable<KeyValuePair<string, int>> _mostCommonScanned;
		private IEnumerable<KeyValuePair<string, int>> MostCommonScanned
		{
			get
			{
				if (_mostCommonScanned != null)
					return _mostCommonScanned;

				// Init a dictionary to store quantities of people of varying nationalities.
				Dictionary<string, int> dictionary = GetScannedDictionary();
				int max = dictionary.Values.Max();

				_mostCommonScanned = dictionary.Where(kvp => max == kvp.Value);
				return _mostCommonScanned;
			}
		}
	}
}