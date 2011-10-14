namespace Common
{
	/// <summary>
	/// Extendibility measure in case more options are needed later. Provides options for controlling the type of job
	/// performed by a Sync Worker.
	/// </summary>
	public enum SyncJobType
	{
		/// <summary>
		/// This job will download all files from a remote directory to a local directory and delete anything in the
		/// local directory that is not present in the remote one.
		/// </summary>
		Download,

		/// <summary>
		/// 
		/// </summary>
		DownloadFeedback,

		/// <summary>
		/// 
		/// </summary>
		DeleteFeedback,

		/// <summary>
		/// This job will upload all files from a local directory to the remote directory, and delete anything in the
		/// remote directory that is not in the local one.
		/// </summary>
		Upload
	}
}