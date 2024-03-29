﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BaconFeedback
{
	public partial class StatisticsForm : Form
	{
		private readonly StatisticsPresenter _presenter;
		private List<FeedbackFile> _selectedFiles;

		public string MainText
		{
			get { return textBoxMain.Text; }
			set { textBoxMain.Text = value; }
		}

		public StatisticsForm(List<FeedbackFile> selectedFiles)
		{
			InitializeComponent();

			_selectedFiles = selectedFiles;
			_presenter = new StatisticsPresenter(this, selectedFiles);

			_presenter.ShowStatistics();
		}

		#region Toolbar Button Events

		private void buttonExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripPrint_Click(object sender, EventArgs e)
		{
			_presenter.Print();
		}

		private void toolStripExport_Click(object sender, EventArgs e)
		{
			_presenter.Export();
		}

		#endregion
	}
}
