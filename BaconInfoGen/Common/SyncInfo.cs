using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	/// <summary>
	/// Extendibility measure in case more options are needed later.
	/// </summary>
	public enum FtpJobType
	{
		Download,
		Upload
	}

	public struct SyncInfo
	{
		public string LocalDirectory { get; set; }
		public string RemoteDirectory { get; set; }

		public FtpJobType JobType { get; set; }

		public SyncInfo(string localDirectory, string remoteDirectory, FtpJobType jobtype) : this()
		{
			LocalDirectory = localDirectory;
			RemoteDirectory = remoteDirectory;
			JobType = jobtype;
		}
	}
}
