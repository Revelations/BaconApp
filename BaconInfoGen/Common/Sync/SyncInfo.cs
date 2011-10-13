namespace Common
{
	/// <summary>
	/// Contains information about a synchronisation request with a remote server.
	/// </summary>
	public struct SyncInfo
	{
		/// <summary>
		/// Gets or sets the local directory to synchronise. Relative to the working directory.
		/// </summary>
		public string LocalDirectory { get; set; }

		/// <summary>
		/// Gets or sets the remote directory to synchronise. Relative to the remote root directory.
		/// </summary>
		public string RemoteDirectory { get; set; }

		/// <summary>
		/// Gets or sets the job type for the sync.
		/// </summary>
		public SyncJobType JobType { get; set; }

		/// <summary>
		/// Constructor for a SyncInfo accepting 3 arguments.
		/// </summary>
		/// <param name="localDirectory">Local directory to synchronise.</param>
		/// <param name="remoteDirectory">Remote directory to synchronise.</param>
		/// <param name="jobtype">Type of sync job to perform.</param>
		public SyncInfo(string localDirectory, string remoteDirectory, SyncJobType jobtype) : this()
		{
			LocalDirectory = localDirectory;
			RemoteDirectory = remoteDirectory;
			JobType = jobtype;
		}
	}
}
