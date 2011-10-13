using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BaconFeedback
{
	public class FeedbackFtpHelper
	{
		private const string ServerLocation = "ftp://Revelations.webhop.org/feedback/";
		private const int BufferLength = 2048;

		private static FtpWebRequest InitRequest(string location, string method)
		{
			var request = (FtpWebRequest) WebRequest.Create(location);
			request.Method = method;

			return request;
		}

		private static Stream ResponseStream(string uri, string method)
		{
			return InitRequest(uri, method).GetResponse().GetResponseStream();
		}

		public List<string> GetRemoteDirectoryListing()
		{
			try
			{
				using (var reader = new StreamReader(ResponseStream(ServerLocation, WebRequestMethods.Ftp.ListDirectory)))
				{
					var result = new List<string>();
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						// Get file name and add it to list.
						result.Add(line);
					}
					return result;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
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
				using (Stream responseStream = ResponseStream(ServerLocation + fileName, WebRequestMethods.Ftp.DownloadFile))
				// Initialise filestream to write to file.
				using (var writer = new FileStream(localDirectory + fileName, FileMode.Create))
				{
					// Create a read/write buffer.
					var buffer = new byte[BufferLength];

					// Get byte data from server stream for as long as it is available.
					int bytes;
					while ((bytes = responseStream.Read(buffer, 0, BufferLength)) > 0)
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