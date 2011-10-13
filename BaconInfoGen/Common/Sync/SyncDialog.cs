using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common
{
	public partial class SyncDialog : Form, ISyncDialog
	{
		private SyncPresenter _presenter;

		public SyncDialog(SyncInfo info)
		{
			InitializeComponent();

			_presenter = new SyncPresenter(this, info);
		}

		public ProgressBar ProgressBar
		{
			get { return progressBar; }
		}

		public BackgroundWorker Worker
		{
			get { return worker; }
		}

		private void SyncDialog_Shown(object sender, EventArgs e)
		{
			_presenter.StartWorker();
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Close();
		}
	}
}
