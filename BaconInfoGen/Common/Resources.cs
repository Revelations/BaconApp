using System.IO;

namespace Common
{
	/// <summary>
	/// Class for centralised storage of resources. Superior to a static resource file.
	/// </summary>
	public static class Resources
	{
		private const string _workingDirectory = "C:/Test/";
		private const string _gameDirectory = _workingDirectory + "Game/";
		private const string _contentDirectory = _workingDirectory + "Content/";
		private const string _feedbackDirectory = _workingDirectory + "Feedback/";

		/// <summary>
		/// Gets the address of the web server used to syncronise with.
		/// </summary>
		public static string ServerLocation { get { return "ftp://Revelations.webhop.org/"; } }

		/// <summary>
		/// Gets the directory that all game content is stored in. Creates this directory if it does not exist.
		/// </summary>
		public static string GameDirectory
		{
			get { return CreateDirectoryAndReturn(_gameDirectory); }
		}

		/// <summary>
		/// Gets the directory that all feedback files are to be stored in. Creates this directory if it does not exist.
		/// </summary>
		public static string FeedbackDirectory
		{
			get { return CreateDirectoryAndReturn(_feedbackDirectory); }
		}

		/// <summary>
		/// Gets the directory that all web generator content is stored in. Creates this directory if it does not exist.
		/// </summary>
		public static string ContentDirectory
		{
			get { return CreateDirectoryAndReturn(_contentDirectory); }
		}

		/// <summary>
		/// Gets the main working parent directory for the other content folders. Creates this directory if it does not exist.
		/// </summary>
		public static string WorkingDirectory
		{
			get { return CreateDirectoryAndReturn(_workingDirectory); }
		}

		/// <summary>
		/// Creates a directory if it does not already exist. Returns that directory.
		/// </summary>
		/// <param name="directory">Directory path.</param>
		/// <returns>Directory path.</returns>
		public static string CreateDirectoryAndReturn(string directory)
		{
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);
			return directory;
		}
	}
}
