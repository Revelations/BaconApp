using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BaconFeedback
{
	public class FeedbackFtpHelper
	{
		private const string ServerLocation = "ftp://Revelations.webhop.org/feedback/";

		private static FtpWebRequest InitRequest(string location, string method)
		{
			var request = (FtpWebRequest)WebRequest.Create(location);
			request.Method = method;

			return request;
		}

		public List<string> GetRemoteDirectoryListing()
		{
			var result = new List<string>();

			try
			{
				Stream responseStream = InitRequest(ServerLocation, WebRequestMethods.Ftp.ListDirectory).GetResponse().GetResponseStream();
				if (responseStream != null)
					using (var reader = new StreamReader(responseStream))
					{
						string line;
						while ((line = reader.ReadLine()) != null)
						{
							// Get file name and add it to list.
							result.Add(line);
						}
					}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return result;
		}

		public void DeleteRemoteFile(string fileName)
		{
			try
			{
				InitRequest(ServerLocation + fileName, WebRequestMethods.Ftp.DeleteFile);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void DownloadRemoteFile(string fileName, string localDirectory)
		{
			try
			{
				// Init request, connect and get bytestream from server.
				using (var responseStream = InitRequest(ServerLocation + fileName, WebRequestMethods.Ftp.DownloadFile).GetResponse().GetResponseStream())
				// Initialise filestream to write to file.
				using (var writer = new FileStream(localDirectory + fileName, FileMode.Create))
				{
					// Create a read/write buffer.
					const int bufferLength = 2048;
					var buffer = new byte[bufferLength];

					// Get byte data from server stream for as long as it is available.
					int bytes;
					while ((bytes = responseStream.Read(buffer, 0, bufferLength)) > 0)
					{
						// Write byte data to file.
						writer.Write(buffer, 0, bytes);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public bool LocalCopyExists(string fileName)
		{
			return (File.Exists(fileName));
		}
	}
}