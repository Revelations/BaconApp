using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Common
{
	/// <summary>
	/// Implements various methods involved in communication with an ftp server.
	/// </summary>
	public class SyncHelper
	{
		// Location of the server. Refer to common resource file.
		private static readonly string _serverLocation = Resources.ServerLocation;

		// Contains methods for getting remote file or directory information.
		#region Remote Information

		/// <summary>
		/// Gets a listing of all the files in the root directory of the FTP server.
		/// </summary>
		/// <returns>List of file names.</returns>
		public static List<string> GetRemoteDirectoryListing()
		{
			return GetRemoteDirectoryListing(string.Empty);
		}

		/// <summary>
		/// Gets a listing of all the files in a subdirectory of the FTP server.
		/// </summary>
		/// <param name="subDirectory">Name of the subdirectory to index.</param>
		/// <returns>List of file names.</returns>
		public static List<string> GetRemoteDirectoryListing(string subDirectory)
		{
			List<string> directorySimple = GetDirectoryDetails(subDirectory, WebRequestMethods.Ftp.ListDirectory);
			List<string> directoryDetail = GetDirectoryDetails(subDirectory, WebRequestMethods.Ftp.ListDirectoryDetails);

			var result = new List<string>();
			foreach (string item in directoryDetail.Where(ItemIsFile))
			{
				result.AddRange(directorySimple.Where(item.EndsWith));
			}

			return result;
		}

		/// <summary>
		/// Gets a directory listing from a remote server in a format according to the web request method used.
		/// </summary>
		/// <param name="directory">Name of subdirectory to retrieve listing for. Use empty string if retrieving root dir.</param>
		/// <param name="method">Web request method to use. Acceptable values are ListDirectory or ListDirectoryDetails.</param>
		/// <returns>List of files in the specified directory.</returns>
		private static List<string> GetDirectoryDetails(string directory, string method)
		{
			var result = new List<string>();

			FtpWebRequest request = InitRequest(directory, method);

			try
			{
				Stream responseStream = request.GetResponse().GetResponseStream();
				var reader = new StreamReader(responseStream);

				string line;
				while ((line = reader.ReadLine()) != null)
				{
					// Get file name and add it to list.
					result.Add(line);
				}

				reader.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("GetDirectoryDetails" + e.Message);
			}

			return result;
		}

		/// <summary>
		/// Checks whether a specified string represents a file or a directory.
		/// </summary>
		/// <param name="item">Input string to check.</param>
		/// <returns>True if item is a file. False if it's a directory.</returns>
		private static bool ItemIsFile(string item)
		{
			return !item.ToLower().Contains("<dir>") && !(item.StartsWith("d") && !item.EndsWith("."));
		}

		/// <summary>
		/// Gets the size of a file stored on the remote server.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="remoteDirectory">Directory it is located in. Relative to the root directory.</param>
		/// <returns>Length of the file in bytes.</returns>
		public static long? GetRemoteFileSize(string fileName, string remoteDirectory = "")
		{
			FtpWebRequest request = InitRequest(remoteDirectory + fileName,
			                                    WebRequestMethods.Ftp.GetFileSize);
			try
			{
				FtpWebResponse response = (FtpWebResponse) request.GetResponse();
				return response.ContentLength;
			}
			catch (Exception e)
			{
				Console.WriteLine("GetRemoteFileSize: " + e.Message);
				return null;
			}
		}

		/// <summary>
		/// Gets the time at which a remote file was last modified.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="remoteDirectory">Directory it is located in. Relative to the root directory.</param>
		/// <returns>DateTime object corresponding to when the file was last modified.</returns>
		public static DateTime? GetRemoteLastModified(string fileName, string remoteDirectory = "")
		{
			FtpWebRequest request = InitRequest(remoteDirectory + fileName,
			                                    WebRequestMethods.Ftp.GetDateTimestamp);
			try
			{
				FtpWebResponse response = (FtpWebResponse) request.GetResponse();
				return response.LastModified;
			}
			catch (Exception e)
			{
				Console.WriteLine("GetRemoteLastModified: " + e.Message);
				return null;
			}
		}

		#endregion

		// Contains methods for retrieving information about locally stored files.
		#region Local Information

		/// <summary>
		/// Gets a list of files stored in a given local directory.
		/// </summary>
		/// <param name="directory">Optional subdirectory to retrieve list for instead.</param>
		/// <returns>List of all files stored in the given directory.</returns>
		public static List<string> GetLocalDirectoryListing(string directory = "")
		{
			List<string> result = new List<string>();

			if (Directory.Exists(directory))
			{
				DirectoryInfo d = new DirectoryInfo(directory);
				foreach (FileInfo f in d.GetFiles())
					result.Add(f.Name);
			}

			return result;
		}

		public static List<string>  GetLocalDirectoryListing(string directory, bool subdirectories)
		{
			List<string> result = new List<string>();

			if (Directory.Exists(directory))
			{
				DirectoryInfo dir = new DirectoryInfo(directory);
				foreach (var d in dir.GetDirectories())
				{
					var subDirResult = GetLocalDirectoryListing(d.FullName, true);
					foreach (string s in subDirResult)
					{
						result.Add(s);
					}
				}
				foreach (var f in dir.GetFiles())
					result.Add(f.Name);
			}

			return result;
		}

		/// <summary>
		/// Gets the size of a file stored on the local file system.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="localDirectory">Directory it is located in. Relative to the working directory.</param>
		/// <returns>Length of the file in bytes.</returns>
		public static long? GetLocalFileSize(string fileName, string localDirectory = "")
		{
			try
			{
				FileInfo f = new FileInfo(localDirectory + fileName);
				return f.Length;
			}
			catch(Exception e)
			{
				Console.WriteLine("GetLocalFileSize: " + e.Message);
				return null;
			}
		}

		/// <summary>
		/// Gets the time at which a local file was last modified.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="localDirectory">Directory it is located in. Relative to the working directory.</param>
		/// <returns>DateTime object corresponding to when the file was last modified.</returns>
		public static DateTime? GetLocalLastModified(string fileName, string localDirectory = "")
		{
			try
			{
				FileInfo f = new FileInfo(localDirectory + fileName);
				return f.LastWriteTime;
			}
			catch (Exception e)
			{
				Console.WriteLine("GetLocalLastModified: " + e.Message);
				return null;
			}
		}

		#endregion

		// Contains methods for transferring files to and from a remote server.
		#region Uploading and Downloading

		/// <summary>
		/// Downloads a single file from a remote FTP server.
		/// </summary>
		/// <param name="fileName">Name of the file to download.</param>
		/// <param name="localDirectory">Directory relative to the working directory to store it in once downloaded.</param>
		/// <param name="remoteDirectory">Subdirectory the file is located in on the server.</param>
		public static void DownloadFile(string fileName, string localDirectory = "", string remoteDirectory = "")
		{
			FtpWebRequest request = InitRequest(remoteDirectory + fileName,
												WebRequestMethods.Ftp.DownloadFile);

			Stream responseStream = null;
			FileStream writer = null;

			try
			{
				// Connect and get bytestream from server.
				var response = (FtpWebResponse)request.GetResponse();
				responseStream = response.GetResponseStream();

				// Initialise filestream to write to file.
				writer = new FileStream(localDirectory + fileName, FileMode.Create);

				// Create a read/write buffer.
				const int bufferLength = 2048;
				var buffer = new byte[bufferLength];

				// Get byte data from server stream for as long as it is available.
				int bytes = responseStream.Read(buffer, 0, bufferLength);
				while (bytes > 0)
				{
					// Write byte data to file.
					writer.Write(buffer, 0, bytes);
					bytes = responseStream.Read(buffer, 0, bufferLength);
				}

				// Close streams once file transfer is complete.
				writer.Close();
				response.Close();
			}
			catch (Exception e)
			{
				if (responseStream != null) responseStream.Close();
				if (writer != null) writer.Close();

				Console.WriteLine("DownloadFile: " + e.Message);
			}
		}

		/// <summary>
		/// Uploads a single file to a remote FTP server.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="localDirectory">Directory relative to the working directory that file is currently stored in.</param>
		/// <param name="remoteDirectory">Remote directory to store the file in once uploaded.</param>
		public static void UploadFile(string fileName, string localDirectory = "", string remoteDirectory = "")
		{
			FtpWebRequest request = InitRequest(remoteDirectory + fileName,
												WebRequestMethods.Ftp.UploadFile);

			Stream ftpstream = null;

			try
			{
				// Create a byte array and store file data.
				byte[] contents = File.ReadAllBytes(localDirectory + fileName);

				// Set ftp content length to file length.
				request.ContentLength = contents.Length;

				// Get the ftp request stream and write the file to it.
				ftpstream = request.GetRequestStream();

				ftpstream.Write(contents, 0, contents.Length);
				ftpstream.Close();
			}
			catch (Exception e)
			{
				if (ftpstream != null) ftpstream.Close();

				Console.WriteLine("UploadFile: " + e.Message);
			}
		}

		#endregion

		// Contains methods for deleting files from either remote server or local filesystem.
		#region Deleting

		/// <summary>
		/// Deletes a file from a remote FTP server.
		/// </summary>
		/// <param name="fileName">Name of the file to delete.</param>
		/// <param name="remoteDirectory">Optional subdirectory it is located in.</param>
		public static void DeleteRemoteFile(string fileName, string remoteDirectory = "")
		{
			try
			{
				InitRequest(remoteDirectory + fileName, WebRequestMethods.Ftp.DeleteFile).GetResponse();
			}
			catch (Exception e)
			{
				Console.WriteLine("DeleteRemoteFile: " + e.Message);
			}
		}

		/// <summary>
		/// Deletes a local file from the file system.
		/// </summary>
		/// <param name="fileName">Name of the file to delete.</param>
		/// <param name="localDirectory">Directory relative to working directory that it is stored in.</param>
		public static void DeleteLocalFile(string fileName, string localDirectory)
		{
			try
			{
				File.Delete(localDirectory + fileName);
			}
			catch (Exception e)
			{
				Console.WriteLine("DeleteLocalFile: " + e.Message);
			}
		}

		#endregion

		// Contains methods for comparing files on the server with their local versions.
		#region Comparing

		/// <summary>
		/// Compares a local version of a file with its remote counterpart to check whether or not they are the same size.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="localDirectory">Directory that local version is stored in. Relative to working directory.</param>
		/// <param name="remoteDirectory">Directory that remote version is stored in.</param>
		/// <returns>True if file sizes match. False if not.</returns>
		public static bool CompareFileSize(string fileName, string localDirectory = "", string remoteDirectory = "")
		{
			return (GetLocalFileSize(fileName, localDirectory).Equals(GetRemoteFileSize(fileName, remoteDirectory)));
		}

		/// <summary>
		/// Compares a local version of a file with its remote counterpart to check whether or not they have the same last modified date.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="localDirectory">Directory that local version is stored in. Relative to working directory.</param>
		/// <param name="remoteDirectory">Directory that remote version is stored in.</param>
		/// <returns>True if last modified dates match. False if not.</returns>
		public static bool CompareLastModified(string fileName, string localDirectory = "", string remoteDirectory = "")
		{
			return (GetLocalLastModified(fileName, localDirectory).Equals(GetRemoteLastModified(fileName, remoteDirectory)));
		}

		/// <summary>
		/// Checks if there is a given file present on a remote FTP server.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="remoteDirectory">Remote subdirectory it resides in.</param>
		/// <returns>True if the file exists, false otherwise.</returns>
		public static bool RemoteVersionExists(string fileName, string remoteDirectory = "")
		{
			FtpWebRequest request = InitRequest(remoteDirectory + fileName, WebRequestMethods.Ftp.GetFileSize);

			try
			{
				request.GetResponse();
				return true;
			}
			catch (WebException e)
			{
				FtpWebResponse response = (FtpWebResponse) e.Response;
				if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
					return false;
			}

			return true;
		}

		/// <summary>
		/// Checks if there is a given file present on the local filesystem.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="localDirectory">Directory it is located in. Relative to working directory.</param>
		/// <returns>True if the file exists, false otherwise.</returns>
		public static bool LocalVersionExists(string fileName, string localDirectory = "")
		{
			return File.Exists(localDirectory + fileName);
		}

		/// <summary>
		/// Determines if a file needs to be downloaded, based on differences between the server and filesystem.
		/// </summary>
		/// <param name="filename">Name of the file.</param>
		/// <param name="localDirectory">Optional additional directory it is stored in.</param>
		/// <param name="remoteDirectory">Directory it is located in on the server.</param>
		/// <returns>True if the file needs downloading. False if the server version matches the local one.</returns>
		public static bool NeedsDownload(string filename, string localDirectory = "", string remoteDirectory = "")
		{
			// If the file does not exist locally, flag it for download.
			if (!LocalVersionExists(filename, localDirectory))
				return true;

			// If the remote version is a different size to the local version, flag it for download.
			if (!CompareFileSize(filename, localDirectory, remoteDirectory))
				return true;

			// If the remote version has a different last modified date, flag it for download.
			return !CompareLastModified(filename, localDirectory, remoteDirectory);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="files"></param>
		/// <returns></returns>
		public static bool NeedsFeedbackDownload(string filename, List<string> files)
		{
			return !files.Contains(filename);
		}

		/// <summary>
		/// Determines if a file needs to be downloaded, based on differences between the server and filesystem.
		/// </summary>
		/// <param name="filename">Name of the file.</param>
		/// <param name="localDirectory">Optional additional directory it is stored in.</param>
		/// <param name="remoteDirectory">Directory it is located in on the server.</param>
		/// <returns>True if the file needs uploading. False if the server version matches the local one.</returns>
		public static bool NeedsUpload(string filename, string localDirectory = "", string remoteDirectory = "")
		{
			// If the file does not exist remotely, flag it for upload.
			if (!RemoteVersionExists(filename, remoteDirectory))
				return true;

			// If the local version is a different size to the remote version, flag it for upload.
			if (!CompareFileSize(filename, localDirectory, remoteDirectory))
				return true;

			// If the local version has a different last modified date, flag it for upload.
			return !CompareLastModified(filename, localDirectory, remoteDirectory);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="files"></param>
		/// <param name="localDirectory"></param>
		/// <param name="remoteDirectory"></param>
		/// <returns></returns>
		public static bool NeedsUpload(string filename, List<string> files, string localDirectory = "", string remoteDirectory = "")
		{
			if (!files.Contains(filename))
				return true;

			// If the local version is a different size to the remote version, flag it for upload.
			if (!CompareFileSize(filename, localDirectory, remoteDirectory))
				return true;

			// If the local version has a different last modified date, flag it for upload.
			return !CompareLastModified(filename, localDirectory, remoteDirectory);
		}

		#endregion

		/// <summary>
		///  Initialises an FTPWebRequest to perform operations on a remote FTP server.
		/// </summary>
		/// <param name="location">Web address of the server.</param>
		/// <param name="method">Web request method to generate the request for.</param>
		/// <returns>Web request, ready for connecting.</returns>
		private static FtpWebRequest InitRequest(string location, string method)
		{
			var request = WebRequest.Create(_serverLocation + location) as FtpWebRequest;

			if (request != null)
				request.Method = method;

			return request;
		}
	}
}