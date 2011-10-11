using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaconFeedback
{
    public class StatisticsPresenter
    {
        private readonly StatisticsForm _view;
        private readonly List<FeedbackFile> _selectedFiles;

        private readonly StatisticsCalculator _calculator;

        public StatisticsPresenter(StatisticsForm view, List<FeedbackFile> selectedFiles)
        {
            _view = view;
            _selectedFiles = selectedFiles;
            _calculator = new StatisticsCalculator(_selectedFiles);
        }

        public void ShowStatistics()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("Number of files selected: {0}", _calculator.FeedbackQuantity).AppendLine().AppendLine();

            builder.AppendFormat("Total number of visitors: {0}", _calculator.TotalVisitors).AppendLine();
            builder.AppendFormat("Estimated total number of visitors (excluding duplicates): {0}",
                                 _calculator.TotalVisitorsExcludingDuplicates).AppendLine().AppendLine();

            builder.AppendFormat("Average group size: {0}", _calculator.AverageGroupSize.ToString("0.0")).AppendLine();
            builder.AppendFormat("Largest single group: {0}", _calculator.LargestGroup).AppendLine().AppendLine();

            builder.AppendFormat("Most common visitor nationality: {0}", _calculator.MostCommonNationality.Key).AppendLine();

            _view.MainText = builder.ToString();
        }
    }
}
