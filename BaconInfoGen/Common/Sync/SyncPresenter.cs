using System;
using System.ComponentModel;

namespace Common
{
	/// <summary>
	/// Class that controls the presentation of the Sync Dialog, linking it to the model.
	/// </summary>
	public class SyncPresenter
	{
		/// <summary>
		/// The form that this controls presentation for.
		/// </summary>
		private readonly ISyncDialog _view;

		/// <summary>
		/// Information about the requested sync operation.
		/// </summary>
		private readonly SyncInfo _info;

		/// <summary>
		/// Contains jobs for the worker to perform.
		/// </summary>
		private readonly SyncJobs _ftpJobs;

		/// <summary>
		/// Constructor for this presenter accepting two arguments.
		/// </summary>
		/// <param name="view">Form that this controls presentation for.</param>
		/// <param name="info">Information about the requested sync operation.</param>
		public SyncPresenter(ISyncDialog view, SyncInfo info)
		{
			// Hand args to private members.
			_view = view;
			_info = info;

			// Introduce worker to all possible jobs.
			_ftpJobs = new SyncJobs(_view.Worker);

			// Give the worker its job.
			SetWorkerJob();

			// Bind a progress changed event.
			_view.Worker.ProgressChanged += ProgressChanged;
		}

		/// <summary>
		/// Binds the view's background worker to a specific method, depending on the type of sync requested.
		/// </summary>
		private void SetWorkerJob()
		{
			// Hand worker the requested type of job.
			switch(_info.JobType)
			{
				case SyncJobType.Download:
					_view.Worker.DoWork += _ftpJobs.DownloadAll;
					break;
				case SyncJobType.Upload:
					_view.Worker.DoWork += _ftpJobs.UploadAll;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// Starts the view's background worker on its job.
		/// </summary>
		public void StartWorker()
		{
			_view.Worker.RunWorkerAsync(_info);
		}

		/// <summary>
		/// Called when the worker reports progress has been made.
		/// </summary>
		private void ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			_view.ProgressBar.Value = e.ProgressPercentage;
		}
	}
}
