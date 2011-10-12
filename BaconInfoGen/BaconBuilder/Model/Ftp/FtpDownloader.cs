using System;
using System.IO;
using System.Net;
using BaconBuilder.Properties;

namespace BaconBuilder.Model
{
	/// <summary>
	/// Class that handles connection to an FTP server to download necessary files.
	/// </summary>
	public class FtpDownloader : FtpHelper
	{
		private static readonly string HtmlDirectory = "C:/Users/" + Environment.UserName + "/test/";
		private readonly IModel _model;

		public FtpDownloader(IModel model)
		{
			_model = model;
		}

		/// <summary>
		/// Helper method that connects to a server and downloads every file present in the main directory that needs to be downloaded.
		/// </summary>
//		private void ConnectAndDownloadAll()
//		{
//			var tuple = GetDirectoryTuple(Resources.ServerLocation);
//			//foreach (string fileName in ConnectAndGetFileList())
//			Console.WriteLine("File to download is: ");
//			foreach (string fileName in tuple.Item2)
//			{
//				if (FileNeedsDownload(fileName))
//				{
//					Console.WriteLine(fileName);
//					DownloadSingleFile(fileName);
//				}
//			}
//		}

		/// <summary>
		/// Method that downloads a single file from an FTP server.
		/// </summary>
		/// <param name="fileName">Name of the file to download.</param>
		public void DownloadSingleFile(string fileName)
		{
			// Init request.
			var ftp = (FtpWebRequest) WebRequest.Create(FtpUriString(fileName));

			// Request type is download.
			ftp.Method = WebRequestMethods.Ftp.DownloadFile;

			// Connect and get bytestream from server.
			var response = (FtpWebResponse) ftp.GetResponse();
			Stream responseStream = response.GetResponseStream();

			// Initialise filestream to write to file.
			var writer = new FileStream(HtmlDirectory + fileName, FileMode.Create);

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

		/// <summary>
		/// Asserts whether or not a file needs to be downloaded.
		/// 
		/// If it does not exists locally or its remote version is a different size to the local one,
		/// then will return true.
		/// </summary>
		/// <param name="fileName">The name of the file to check.</param>
		/// <returns>Whether or not the file needs downloading.</returns>
		public bool FileNeedsDownload(string fileName)
		{
			return (!CheckIfLocalCopyExists(fileName) || LocalVersionSize(fileName) != RemoteVersionSize(fileName));
		}
	}
}