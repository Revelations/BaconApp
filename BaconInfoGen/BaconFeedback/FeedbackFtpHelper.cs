using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BaconFeedback
{
    public class FeedbackFtpHelper
    {
        private const string _serverLocation = "ftp://Revelations.webhop.org/feedback/";

        public FtpWebRequest InitRequest(string location, string method)
        {
            var request = WebRequest.Create(location) as FtpWebRequest;

            if (request != null)
                request.Method = method;

            return request;
        }

        public List<string> GetRemoteDirectoryListing()
        {
            List<string> result = new List<string>();

            FtpWebRequest request = InitRequest(_serverLocation, WebRequestMethods.Ftp.ListDirectory);

            try
            {
                Stream responseStream = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

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

        public void DeleteRemoteFile(string fileName)
        {
            try
            {
                InitRequest(_serverLocation + fileName, WebRequestMethods.Ftp.DeleteFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DownloadRemoteFile(string fileName, string localDirectory)
        {
            // Init request.
            FtpWebRequest request = InitRequest(_serverLocation + fileName, WebRequestMethods.Ftp.DownloadFile);

            try
            {
                // Connect and get bytestream from server.
                FtpWebResponse response = (FtpWebResponse) request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                // Initialise filestream to write to file.
                FileStream writer = new FileStream(localDirectory + fileName, FileMode.Create);

                // Create a read/write buffer.
                const int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];

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
            catch(Exception e)
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
