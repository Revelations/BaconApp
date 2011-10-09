using System;
using System.Collections.Generic;
using System.IO;
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
            var result = new List<string>();

            string uriString = Resources.ServerLocation;
            const string method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebRequest request = InitRequest(uriString, method);

            // Connect to server.
            using (WebResponse response = request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            {
                if (responseStream == null) return result;

                // Instantiate a reader to handle the stream sent back from server.
                using (var reader = new StreamReader(responseStream))
                {
                    // Continue iterating for as long as needed.
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Get file name and add it to list.
                        result.Add(line);
                    }
                }
            }

            return result;
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
            string uriString = Resources.ServerLocation + fileName;
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
            string uriString = Resources.ServerLocation + fileName;
            const string methods = WebRequestMethods.Ftp.DeleteFile;
            FtpWebRequest request = InitRequest(uriString, methods);

            // TODO: Store this value for error checking in future.
            WebResponse response = request.GetResponse();
        }
    }
}