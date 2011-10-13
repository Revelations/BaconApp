using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using BaconBuilder.Properties;

namespace BaconBuilder.Model.Ftp
{
	// TODO: DEALING WITH A LOT OF STREAMS HERE -> ERRORS NEED TO BE HANDLED.

	/// <summary>
	/// Class that handles connection to an FTP server aid in upload/download of necessary files.
	/// </summary>
	public abstract class FtpHelper
	{
		private static DirectoryInfo _directoryInfo;
		//private readonly IModel _model;
		
		public static DirectoryInfo HtmlDirectory
		{
			get
			{
				_directoryInfo = new DirectoryInfo("C:/Users/" + Environment.UserName + "/test/");
				if (!_directoryInfo.Exists)
					_directoryInfo.Create();
				return _directoryInfo;
			}
			set
			{
				_directoryInfo = value;
			}
		}

		public static int FtpPort
		{
			get { return Convert.ToInt32(Resources.FtpPort); }
		}

		public static string FtpUri(string fileName = "")
		{
			return new UriBuilder("ftp", Resources.ServerHost, FtpPort, fileName).ToString();
		}

		/// <summary>
		/// Initialises a web request with the method.
		/// </summary>
		/// <param name="requestUriString"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		protected static FtpWebRequest InitRequest(string requestUriString, string method)
		{
			// init request
			var ftp = (FtpWebRequest) WebRequest.Create(requestUriString);
			// set request type
			ftp.Method = method;

			return ftp;
		}

		public static WebResponse Response(string root, string method)
		{
			return InitRequest(root, method).GetResponse();
		}

		public static Stream ResponseStream(string root, string method)
		{
			return Response(root, method).GetResponseStream();
		}


		/// <summary>
		/// Connects to an ftp server and gets a listing of all files in the main directory.
		/// </summary>
		/// <returns>String list of all files present on the server.</returns>
		public List<string> ConnectAndGetFileList()
		{
			return GetDirectoryTuple(FtpUri()).Item2;
		}

		/// <summary>
		/// Gets the size of a named file on the local filesystem.
		/// </summary>
		/// <param name="fileName">The file name to check for.</param>
		/// <returns>The size of the local file in bytes.</returns>
		public long LocalVersionSize(string fileName)
		{
			return new FileInfo(HtmlDirectory + fileName).Length;
		}

		/// <summary>
		/// Gets the size of a named file on the ftp server.
		/// </summary>
		/// <param name="fileName">The file name to check for.</param>
		/// <returns>The size of the remote file in bytes.</returns>
		public long RemoteVersionSize(string fileName)
		{
			return Response(FtpUri(fileName), WebRequestMethods.Ftp.GetFileSize).ContentLength;
		}

		/// <summary>
		/// Deletes a given file from the ftp server.
		/// </summary>
		/// <param name="fileName">Name of the file to delete.</param>
		public void DeleteRemoteFile(string fileName)
		{
			// TODO: Store this value for error checking in future.
			Response(FtpUri(fileName), WebRequestMethods.Ftp.DeleteFile);
		}

		#region Shii's fix for separating directories from files.

		private static List<string> GetDirectoryDetail(string root, string method)
		{
			// Connect and get bytestream from server.
			// Create a read/write buffer.
			// Get byte data from server stream for as long as it is available.
			using (var reader = new StreamReader(ResponseStream(root, method)))
			{
				var list = new List<string>();
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					list.Add(line);
				}
				return list;
			}
		}

		// TODO: Check boolean to ensure the logic is correct. Results so far do not exhibit "drw-rw-rw-" properties.
		private static bool ItemIsSubFile(string item)
		{
			return !item.ToLower().Contains("<dir>") && !(item.StartsWith("d") && !item.EndsWith("."));
		}

		// TODO: Check boolean to ensure the logic is correct. Results so far do not exhibit "drw-rw-rw-" properties.
		private static bool ItemIsSubDirectory(string item)
		{
			return item.ToLower().Contains("<dir>") || (item.StartsWith("d") && !item.EndsWith("."));
		}

		public Tuple<List<string>, List<string>> GetDirectoryTuple(string root)
		{
			List<string> directorySimple = GetDirectoryDetail(root, WebRequestMethods.Ftp.ListDirectory);
			List<string> directoryDetail = GetDirectoryDetail(root, WebRequestMethods.Ftp.ListDirectoryDetails);

			var subdir = new List<string>();
			var subfil = new List<string>();

			foreach (string item in directoryDetail)
			{
				if (ItemIsSubDirectory(item))
				{
					subdir.AddRange(directorySimple.Where(s => item.EndsWith(s)));
				}
				else if (ItemIsSubFile(item))
				{
					subfil.AddRange(directorySimple.Where(s => item.EndsWith(s)));
				}
			}

			return new Tuple<List<string>, List<string>>(subdir, subfil);
		}

		#endregion
	}
}