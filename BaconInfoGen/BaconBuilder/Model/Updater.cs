using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BaconBuilder.Model
{
    // TODO: DEALING WITH A LOT OF STREAMS HERE -> ERRORS NEED TO BE HANDLED.

    /// <summary>
    /// Class that handles connection to an FTP server to synchronise html content against multiple machines.
    /// </summary>
    public class Updater
    {
        // Address of the ftp server to connect to.
        private string _serverAddress = "ftp://revelations.webhop.org/";

        // Hard coded Html file directory. Obviously this is to be changed eventually.
        private string _htmlDirectory = "C:/Users/" + System.Environment.UserName + "/test/";

        /// <summary>
        /// Connects to an ftp server and gets a listing of all files in the main directory.
        /// </summary>
        /// <returns>String list of all files present on the server.</returns>
        public List<string> ConnectAndGetList()
        {
            // Init request.
            FtpWebRequest ftp = (FtpWebRequest) WebRequest.Create(_serverAddress);

            // Request type is directory listing.
            ftp.Method = WebRequestMethods.Ftp.ListDirectory;

            // Connect to server.
            WebResponse response = ftp.GetResponse();

            // Instantiate a reader to handle the stream sent back from server.
            StreamReader reader = new StreamReader(response.GetResponseStream());

            List<string> result = new List<string>();

            // Continue iterating for as long as needed.
            string line = reader.ReadLine();
            while (line != null)
            {
                // Get file name and add it to list.
                result.Add(line);
                line = reader.ReadLine();
            }

            return result;
        }

        /// <summary>
        /// Helper method that connects to a server and downloads every file present in the main directory.
        /// </summary>
        public void ConnectAndDownloadAll()
        {
            foreach(string fileName in ConnectAndGetList())
            {
                DownloadSingleFile(fileName);
            }
        }

        /// <summary>
        /// Method that downloads a single file from an FTP server.
        /// </summary>
        /// <param name="fileName">Name of the file to download.</param>
        public void DownloadSingleFile(string fileName)
        {
            // Init request.
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(_serverAddress + fileName);

            // Request type is download.
            ftp.Method = WebRequestMethods.Ftp.DownloadFile;

            // Connect and get bytestream from server.
            FtpWebResponse response = (FtpWebResponse) ftp.GetResponse();
            Stream responseStream = response.GetResponseStream();

            // Initialise filestream to write to file.
            FileStream writer = new FileStream(_htmlDirectory + fileName, FileMode.Create);

            // Create a read/write buffer.
            int bufferLength = 2048;
            byte[] buffer = new byte[bufferLength];

            // Get byte data from server stream for as long as it is available.
            int bytes = responseStream.Read(buffer, 0, bufferLength);
            while(bytes > 0)
            {
                // Write byte data to file.
                writer.Write(buffer, 0, bytes);
                bytes = responseStream.Read(buffer, 0, bufferLength);
            }

            // Close streams once file transfer is complete.
            writer.Close();
            response.Close();
        }
    }
}
