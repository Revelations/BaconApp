using System.Collections.Generic;
using System.Text;

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
			var builder = new StringBuilder()
				.AppendFormat("Number of files selected: {0}", _calculator.FeedbackQuantity).AppendLine()
				.AppendLine()
				.AppendFormat("Total number of visitors: {0}", _calculator.TotalVisitors).AppendLine()
				.AppendFormat("Estimated total number of visitors (excluding duplicates): {0}", _calculator.TotalVisitorsExcludingDuplicates).AppendLine()
				.AppendLine()
				.AppendFormat("Average group size: {0}", _calculator.AverageGroupSize.ToString("0.0")).AppendLine()
				.AppendFormat("Largest single group: {0}", _calculator.LargestGroup).AppendLine()
				.AppendLine()
				.AppendFormat("Most common visitor nationality: {0}", _calculator.MostCommonNationality.Key).AppendLine();

			_view.MainText = builder.ToString();
		}
	}
}