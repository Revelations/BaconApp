using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
	public static class Resources
	{
		private const string _workingDirectory = "C:/Test/";
		private const string _gameDirectory = _workingDirectory + "Game/";
		private const string _contentDirectory = _workingDirectory + "Content/";
		private const string _feedbackDirectory = _workingDirectory + "Feedback/";

		public static string GameDirectory
		{
			get
			{
				if (!Directory.Exists(_gameDirectory))
					Directory.CreateDirectory(_gameDirectory);
				return _gameDirectory;
			}
		}

		public static string FeedbackDirectory
		{
			get
			{
				if (!Directory.Exists(_feedbackDirectory))
					Directory.CreateDirectory(_feedbackDirectory);
				return _feedbackDirectory;
			}
		}

		public static string ContentDirectory
		{
			get
			{
				if (!Directory.Exists(_contentDirectory))
					Directory.CreateDirectory(_contentDirectory);
				return _contentDirectory;
			}
		}

		public static string WorkingDirectory
		{
			get
			{
				if (!Directory.Exists(_workingDirectory))
					Directory.CreateDirectory(_workingDirectory);
				return _workingDirectory;
			}
		}
	}
}
