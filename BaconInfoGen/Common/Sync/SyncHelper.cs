using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Common
{
	public class SyncHelper
	{
		private const string _serverLocation = "ftp://revelations.webhop.org/";

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
			foreach (string item in directoryDetail.Where(ItemIsSubFile))
			{
				result.AddRange(directorySimple.Where(item.EndsWith));
			}

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="directory"></param>
		/// <param name="method"></param>
		/// <returns></returns>
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
				Console.WriteLine(e.Message);
			}

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private static bool ItemIsSubFile(string item)
		{
			return !item.ToLower().Contains("<dir>") && !(item.StartsWith("d") && !item.EndsWith("."));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="remoteDirectory"></param>
		/// <returns></returns>
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
				Console.WriteLine(e.Message);
				return null;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="remoteDirectory"></param>
		/// <returns></returns>
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
				Console.WriteLine(e.Message);
				return null;
			}
		}

		#endregion

		#region Local Information

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
				Console.WriteLine(e.Message);
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
				Console.WriteLine(e.Message);
				return null;
			}
		}

		#endregion

		#region Uploading and Downloading

		/// <summary>
		/// Downloads a single file from a remote FTP server.
		/// </summary>
		/// <param name="fileName">Name of the file to download.</param>
		/// <param name="localDirectory">Directory relative to the working directory to store it in once downloaded.</param>
		/// <param name="remoteDirectory">Subdirectory the file is located in on the server.</param>
		public static void DownloadRemoteFile(string fileName, string localDirectory = "", string remoteDirectory = "")
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

				Console.WriteLine(e.Message);
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

				Console.WriteLine(e.Message);
			}
		}

		#endregion

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
				InitRequest(remoteDirectory + fileName, WebRequestMethods.Ftp.DeleteFile);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		/// <summary>
		/// Deletes a local file from the file system.
		/// </summary>
		/// <param name="fileName">Name of the file to delete.</param>
		/// <param name="localDirectory">Directory relative to working directory that it is stored in.</param>
		public static void DeleteLocalFile(string fileName, string localDirectory = "")
		{
			try
			{
				File.Delete(localDirectory + fileName);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		#endregion

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
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="localDirectory"></param>
		/// <param name="remoteDirectory"></param>
		/// <returns></returns>
		public static bool NeedsDownload(string filename, string localDirectory = "", string remoteDirectory = "")
		{
			if (!LocalVersionExists(filename, localDirectory))
				return true;

			if (!CompareFileSize(filename, localDirectory, remoteDirectory))
				return true;

			if (!CompareLastModified(filename, localDirectory, remoteDirectory))
				return true;

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="localDirectory"></param>
		/// <param name="remoteDirectory"></param>
		/// <returns></returns>
		public static bool NeedsUpload(string filename, string localDirectory = "", string remoteDirectory = "")
		{
			if (!RemoteVersionExists(filename, remoteDirectory))
				return true;

			if (!CompareFileSize(filename, localDirectory, remoteDirectory))
				return true;

			if (!CompareLastModified(filename, localDirectory, remoteDirectory))
				return true;

			return false;
		}

		#endregion

		/// <summary>
		///  Initialises an FTPWebRequest to perform operations on a remote FTP server.
		/// </summary>
		/// <param name="location"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		private static FtpWebRequest InitRequest(string location, string method)
		{
			var request = WebRequest.Create(_serverLocation + location) as FtpWebRequest;

			if (request != null)
				request.Method = method;

			return request;
		}
	}
}