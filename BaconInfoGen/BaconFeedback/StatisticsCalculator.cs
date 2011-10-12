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
			get { return _files.Sum(f => Convert.ToInt32(f.Number)); }
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

		public List<KeyValuePair<string, int>> MostCommonNationalities
		{
			get
			{
				// Init a dictionary to store quantities of people of varying nationalities.
				Dictionary<string, int> dictionary = GetConsolidatedNationalityDictionary();

				// Initialise an object to store the largest nationality found.
				var largest = new List<KeyValuePair<string, int>> {new KeyValuePair<string, int>("None", 0)};
				int max = 0;

				// Iterate and find maximum value.
				foreach (var kvp in dictionary)
				{
					if (kvp.Value > max)
					{
						largest.Clear();
						max = kvp.Value;
					}
					if (kvp.Value == max)
						largest.Add(kvp);
				}

				return largest;
			}
		}

		/// <summary>
		/// Gets a key value pair containing the most common nationality present in a group of feedback files, as well as the
		/// quantity of visitors specifying that same nationality.
		/// </summary>
		/// <returns>Most common nationality of visitors, quantity of that nationality.</returns>
		public KeyValuePair<string, int> MostCommonNationality
		{
			get { return MostCommonNationalities[0]; }
		}

		/* // Jordan's method
		public KeyValuePair<string, int> MostCommonNationalityOld
		{
			get
			{
				// Init a dictionary to store quantities of people of varying nationalities.
				var dictionary = new Dictionary<string, int>();

				// For each file...
				foreach (FeedbackFile f in _files)
				{
					// If the dictionary contains the nationality specified by that file...
					if (dictionary.ContainsKey(f.Nationality))
					{
						// Update the value.
						int old = dictionary[f.Nationality];
						dictionary.Remove(f.Nationality);
						dictionary.Add(f.Nationality, old + Convert.ToInt32(f.Number));
					}
					// Otherwise add that nationality to the dictionary.
					else
						dictionary.Add(f.Nationality, Convert.ToInt32(f.Number));
				}

				// Initialise an object to store the largest nationality found.
				var max = new KeyValuePair<string, int>("None", 0);

				// Iterate and find maximum value.
				foreach (var kvp in dictionary)
				{
					if (kvp.Value > max.Value)
						max = kvp;
				}

				return max;
			}
		}
		*/

		private Dictionary<string, int> GetConsolidatedNationalityDictionary()
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
	}
}