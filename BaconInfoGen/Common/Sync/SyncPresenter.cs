using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common
{
	public class SyncPresenter
	{
		private readonly ISyncDialog _view;

		private readonly SyncInfo _info;

		private readonly SyncWorker _ftpWorker;

		public SyncPresenter(ISyncDialog view, SyncInfo info)
		{
			_view = view;
			_info = info;

			_ftpWorker = new SyncWorker(_view.Worker);

			SetWorkerJob();

			_view.Worker.ProgressChanged += ProgressChanged;
		}

		private void ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			_view.ProgressBar.Value = e.ProgressPercentage;
		}

		public void StartWorker()
		{
			_view.Worker.RunWorkerAsync(_info);
		}

		public void SetWorkerJob()
		{
			switch(_info.JobType)
			{
				case SyncJobType.Download:
					_view.Worker.DoWork += _ftpWorker.DownloadAll;
					break;
				case SyncJobType.Upload:
					_view.Worker.DoWork += _ftpWorker.UploadAll;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
