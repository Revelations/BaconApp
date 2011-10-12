using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using BaconBuilder.Properties;

namespace BaconBuilder.Model
{
	// TODO: DEALING WITH A LOT OF STREAMS HERE -> ERRORS NEED TO BE HANDLED.

	/// <summary>
	/// Class that handles connection to an FTP server aid in upload/download of necessary files.
	/// </summary>
	public abstract class FtpHelper
	{
		//private readonly IModel _model;
		private static readonly string HtmlDirectory = "C:/Users/" + Environment.UserName + "/test/";

		public static string FtpUriString(string fileName = "")
		{
			return string.Format("ftp://{0}:{1}/{2}", Resources.ServerHost, Resources.FtpPort, fileName);
		}

		/// <summary>
		/// Initialises a web request with the method.
		/// </summary>
		/// <param name="requestUriString"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		private static FtpWebRequest InitRequest(string requestUriString, string method)
		{
			// init request
			var ftp = WebRequest.Create(requestUriString) as FtpWebRequest;
			if (ftp == null) return null;

			// set request type
			ftp.Method = method;

			return ftp;
		}

		/// <summary>
		/// Connects to an ftp server and gets a listing of all files in the main directory.
		/// </summary>
		/// <returns>String list of all files present on the server.</returns>
		public List<string> ConnectAndGetFileList()
		{
			string uriString = FtpUriString();
			Tuple<List<string>, List<string>> tuple = GetDirectoryTuple(uriString);
			List<string> files = tuple.Item2;
			List<string> dirs = tuple.Item1;

			return files;
		}

		/// <summary>
		/// Checks if a copy of the file with the given name exists on the local filesystem.
		/// </summary>
		/// <param name="fileName">The file name to check for.</param>
		/// <returns>True if the file can be found in the html directory. False otherwise.</returns>
		public bool CheckIfLocalCopyExists(string fileName)
		{
			return (File.Exists(HtmlDirectory + fileName));
		}

		/// <summary>
		/// Gets the size of a named file on the local filesystem.
		/// </summary>
		/// <param name="fileName">The file name to check for.</param>
		/// <returns>The size of the local file in bytes.</returns>
		public long LocalVersionSize(string fileName)
		{
			var info = new FileInfo(HtmlDirectory + fileName);

			return info.Length;
		}

		/// <summary>
		/// Gets the size of a named file on the ftp server.
		/// </summary>
		/// <param name="fileName">The file name to check for.</param>
		/// <returns>The size of the remote file in bytes.</returns>
		public long RemoteVersionSize(string fileName)
		{
			string uriString = FtpUriString(fileName);
			const string method = WebRequestMethods.Ftp.GetFileSize;
			FtpWebRequest request = InitRequest(uriString, method);

			using (WebResponse response = request.GetResponse())
			{
				long result = response.ContentLength;

				return result;
			}
		}

		/// <summary>
		/// Deletes a given file from the ftp server.
		/// </summary>
		/// <param name="fileName">Name of the file to delete.</param>
		public void DeleteRemoteFile(string fileName)
		{
			string uriString = FtpUriString(fileName);
			const string methods = WebRequestMethods.Ftp.DeleteFile;
			FtpWebRequest request = InitRequest(uriString, methods);

			// TODO: Store this value for error checking in future.
			WebResponse response = request.GetResponse();
		}

		#region Shii's fix for separating directories from files.

		private List<string> GetDirectoryDetail(string root, string method)
		{
			var list = new List<string>();

			//Get Directory Details
			var ftp = (FtpWebRequest) WebRequest.Create(root);
			ftp.Method = method;

			// Connect and get bytestream from server.
			using (WebResponse response = ftp.GetResponse())
			{
				Stream responseStream = response.GetResponseStream();

				// Create a read/write buffer.
				// Get byte data from server stream for as long as it is available.
				Debug.Assert(responseStream != null, "responseStream != null");
				using (var reader = new StreamReader(responseStream))
				{
					string line = reader.ReadLine();
					while (line != null)
					{
						list.Add(line);
						line = reader.ReadLine();
					}
				}
			}
			return list;
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

			foreach (string item in directoryDetail.Where(ItemIsSubDirectory))
			{
				subdir.AddRange(directorySimple.Where(s => item.EndsWith(s)));
			}

			foreach (string item in directoryDetail.Where(ItemIsSubFile))
			{
				subfil.AddRange(directorySimple.Where(s => item.EndsWith(s)));
			}

			return new Tuple<List<string>, List<string>>(subdir, subfil);
		}

		#endregion
	}
}