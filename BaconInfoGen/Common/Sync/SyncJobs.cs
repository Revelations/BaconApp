using System.Collections.Generic;
using System.ComponentModel;

namespace Common
{
	/// <summary>
	/// Contains methods for syncronising remote and local directories with the aid of a progress-reporting background worker.
	/// </summary>
	public class SyncJobs
	{
		/// <summary>
		/// The worker used to perform a job.
		/// </summary>
		private readonly BackgroundWorker _worker;

		/// <summary>
		/// Constructor for this object accepting a single argument.
		/// </summary>
		/// <param name="worker">Background worker to perform a job.</param>
		public SyncJobs(BackgroundWorker worker)
		{
			_worker = worker;
		}

		/// <summary>
		/// Downloads all needed files from a server folder and places them in a requested folder locally.
		/// 
		/// Will not download files already present in the local directory.
		/// </summary>
		public void DownloadAll(object sender, DoWorkEventArgs e)
		{
			SyncInfo info = (SyncInfo) e.Argument;
			
			List<string> files = SyncHelper.GetRemoteDirectoryListing(info.RemoteDirectory);

			// TODO: Set text to increment files downloaded.

			// Iterate through the list of files, download each one if needed, and increment progress.
			for (int i = 0; i < files.Count; i++)
			{
				if (SyncHelper.NeedsDownload(files[i], info.LocalDirectory, info.RemoteDirectory))
					SyncHelper.DownloadFile(files[i], info.LocalDirectory, info.RemoteDirectory);

				float progress = (float) (i + 1) / files.Count * 100;
				_worker.ReportProgress((int) progress);
			}

			// TODO: Set text to mention cleanup.

			// Iterate through all local files in the directory, and delete them if they are not present in the remote directory.
			foreach(string s in SyncHelper.GetLocalDirectoryListing(info.LocalDirectory))
			{
				if (!files.Contains(s))
					SyncHelper.DeleteLocalFile(s, info.LocalDirectory);
			}
		}

		/// <summary>
		/// Uploads all needed files from a local directory to a remote one.
		/// 
		/// Will not upload files already present in the remote directory.
		/// </summary>
		public void UploadAll(object sender, DoWorkEventArgs e)
		{
			SyncInfo info = (SyncInfo)e.Argument;

			List<string> files = SyncHelper.GetLocalDirectoryListing(info.LocalDirectory);

			for (int i = 0; i < files.Count; i++)
			{
				if (SyncHelper.NeedsUpload(files[i], info.LocalDirectory, info.RemoteDirectory))
					SyncHelper.UploadFile(files[i], info.LocalDirectory, info.RemoteDirectory);

				float progress = (float) (i + 1) / files.Count * 100;
				_worker.ReportProgress((int) progress);
			}

			foreach (string s in SyncHelper.GetRemoteDirectoryListing(info.RemoteDirectory))
			{
				if (!files.Contains(s))
					SyncHelper.DeleteRemoteFile(s, info.RemoteDirectory);
			}
		}
	}
}
