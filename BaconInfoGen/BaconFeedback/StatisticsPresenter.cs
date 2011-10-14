using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BaconFeedback
{
	public class StatisticsPresenter
	{
		private readonly StatisticsCalculator _calculator;
		private readonly List<FeedbackFile> _selectedFiles;
		private readonly StatisticsForm _view;

		public StatisticsPresenter(StatisticsForm view, List<FeedbackFile> selectedFiles)
		{
			_view = view;
			_selectedFiles = selectedFiles;
			_calculator = new StatisticsCalculator(_selectedFiles);
		}

		public void ShowStatistics()
		{
			// Get only alphabetic characters from anything this is matched against.
			Regex r = new Regex("[A-Za-z]*");

			_view.MainText = new StringBuilder()
				.AppendFormat("Number of files selected: {0}", _calculator.FeedbackQuantity).AppendLine()
				.AppendLine()
				.AppendFormat("Average group size: {0}", _calculator.AverageGroupSize.ToString("0.0")).AppendLine()
				.AppendFormat("Largest single group: {0}", _calculator.LargestGroup).AppendLine()
				.AppendFormat("Total number of visitors: {0}", _calculator.TotalVisitors).AppendLine()
				.AppendFormat("Estimated total number of visitors (excluding duplicates): {0}",
				              _calculator.TotalVisitorsExcludingDuplicates).AppendLine()
				.AppendLine()
				.AppendFormat("Most common visitor nationality: {0}", _calculator.MostCommonNationality.Key).AppendLine()
				.AppendFormat("Number of {0} visitors: {1}", r.Match(_calculator.MostCommonNationality.Key).Value,
				              _calculator.MostCommonNationality.Value).AppendLine()
				.AppendLine()
				.AppendFormat("Most codes scanned: {0}", _calculator.MostScanned).AppendLine()
				.AppendFormat("Least codes scanned: {0}", _calculator.LeastScanned).AppendLine()
				.AppendFormat("Average codes scanned: {0}", _calculator.AverageScanned).AppendLine()
				.AppendFormat("Total codes scanned: {0}", _calculator.TotalScanned).AppendLine()
				.AppendLine()
				.AppendFormat("Most common codes scanned: {0}, {1}, {2}", _calculator.GetNthMostScanned(0),
				              _calculator.GetNthMostScanned(1), _calculator.GetNthMostScanned(2)).ToString();
		}

		public void Print()
		{
			throw new System.NotImplementedException();
		}

		public void Export()
		{
			throw new System.NotImplementedException();
		}
	}
}