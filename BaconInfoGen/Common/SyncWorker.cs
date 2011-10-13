using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common
{
	public class SyncWorker
	{
		private readonly BackgroundWorker _worker;

		public SyncWorker(BackgroundWorker worker)
		{
			_worker = worker;
		}

		public void DownloadAll(object sender, DoWorkEventArgs e)
		{
			SyncInfo info = (SyncInfo) e.Argument;
			
			List<string> files = SyncHelper.GetRemoteDirectoryListing(info.RemoteDirectory);

			// TODO: Set text to increment files downloaded.

			for (int i = 0; i < files.Count; i++)
			{
				if (SyncHelper.NeedsDownload(files[i], info.LocalDirectory, info.RemoteDirectory))
					SyncHelper.DownloadRemoteFile(files[i], info.LocalDirectory, info.RemoteDirectory);

				float progress = (float) (i + 1) / files.Count * 100;
				_worker.ReportProgress((int) progress);
			}

			// TODO: Set text to mention cleanup.

			foreach(string s in SyncHelper.GetLocalDirectoryListing(info.LocalDirectory))
			{
				if (!files.Contains(s))
					SyncHelper.DeleteLocalFile(s, info.LocalDirectory);
			}
		}

		public void UploadAll(object sender, DoWorkEventArgs e)
		{
			
		}
	}
}
