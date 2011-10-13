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
	/// <summary>
	/// Dialog form that displays the progress of a background worker as it synchronises a local directory with a remote one.
	/// </summary>
	public partial class SyncDialog : Form, ISyncDialog
	{
		/// <summary>
		/// Presenter that controls this form's layout.
		/// </summary>
		private SyncPresenter _presenter;

		/// <summary>
		/// Constructor for the class accepting a single argument.
		/// </summary>
		/// <param name="info">SyncInfo object containing information about the requested sync operation.</param>
		public SyncDialog(SyncInfo info)
		{
			InitializeComponent();

			// Init a presenter.
			_presenter = new SyncPresenter(this, info);
		}

		/// <summary>
		/// Called when the dialog is first shown.
		/// </summary>
		private void SyncDialog_Shown(object sender, EventArgs e)
		{
			// Start the worker.
			_presenter.StartWorker();
		}

		/// <summary>
		/// Called when the background worker has finished syncing directories.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// Close this form.
			Close();
		}

		// Contains interface members for a presenter to interact with.
		#region Interface Members

		public ProgressBar ProgressBar
		{
			get { return progressBar; }
		}

		public BackgroundWorker Worker
		{
			get { return worker; }
		}

		#endregion
	}
}
