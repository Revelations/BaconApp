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
    /// Class that handles connection to an FTP server aid in upload/download of necessary files.
    /// </summary>
    public abstract class FtpHelper
    {
    	private readonly IModel _model;
    	// Address of the ftp server to connect to.
        protected const string _serverAddress = "ftp://revelations.webhop.org/";

        // Hard coded Html file directory. Obviously this is to be changed eventually.
        protected readonly string _htmlDirectory = "C:/Users/" + System.Environment.UserName + "/test/";

    	/// <summary>
        /// Connects to an ftp server and gets a listing of all files in the main directory.
        /// </summary>
        /// <returns>String list of all files present on the server.</returns>
        public List<string> ConnectAndGetFileList()
        {
            // Init request.
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(_serverAddress);

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

            response.Close();

            return result;
        }

        /// <summary>
        /// Checks if a copy of the file with the given name exists on the local filesystem.
        /// </summary>
        /// <param name="fileName">The file name to check for.</param>
        /// <returns>True if the file can be found in the html directory. False otherwise.</returns>
        public bool CheckIfLocalCopyExists(string fileName)
        {
            return (File.Exists(_htmlDirectory + fileName));
        }

        /// <summary>
        /// Gets the size of a named file on the local filesystem.
        /// </summary>
        /// <param name="fileName">The file name to check for.</param>
        /// <returns>The size of the local file in bytes.</returns>
        public long LocalVersionSize(string fileName)
        {
            FileInfo info = new FileInfo(_htmlDirectory + fileName);
            return info.Length;
        }

        /// <summary>
        /// Gets the size of a named file on the ftp server.
        /// </summary>
        /// <param name="fileName">The file name to check for.</param>
        /// <returns>The size of the remote file in bytes.</returns>
        public long RemoteVersionSize(string fileName)
        {
            // Init request.
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(_serverAddress + fileName);

            // Set request type to request file size.
            ftp.Method = WebRequestMethods.Ftp.GetFileSize;

            // Get the response, and it's length.
            FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            long result = response.ContentLength;

            // Close the response and return.
            response.Close();
            return result;
        }

        /// <summary>
        /// Deletes a given file from the ftp server.
        /// </summary>
        /// <param name="fileName">Name of the file to delete.</param>
        public void DeleteRemoteFile(string fileName)
        {
            // Init request.
            FtpWebRequest ftp = (FtpWebRequest) WebRequest.Create(_serverAddress + fileName);

            // Request type is delete file.
            ftp.Method = WebRequestMethods.Ftp.DeleteFile;

            // TODO: Store this value for error checking in future.
            ftp.GetResponse();
        }
    }
}
