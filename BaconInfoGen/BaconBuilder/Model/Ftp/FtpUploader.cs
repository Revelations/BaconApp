using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using BaconBuilder.Properties;

namespace BaconBuilder.Model
{
    /// <summary>
    /// Class that handles connection to an FTP server to upload necessary files.
    /// </summary>
    public class FtpUploader : FtpHelper
    {
		private static readonly string HtmlDirectory = "C:/Users/" + Environment.UserName + "/test/";
        /// <summary>
        /// A list of all the remote files present on the server.
        /// </summary>
        private List<string> _remoteFiles;

        public List<string> RemoteFiles { get { return _remoteFiles; } } 

        /// <summary>
        /// Connects to the server and uploads all files necessary.
        /// </summary>
        public void ConnectAndUploadAll()
        {
            DirectoryInfo info = new DirectoryInfo(HtmlDirectory);

            foreach (FileInfo f in info.GetFiles())
                if(FileNeedsUpload(f.Name))
                    UploadSingleFile(f.Name);
        }

        /// <summary>
        /// Uploads a single file to the remote server.
        /// </summary>
        /// <param name="fileName">Name of the file to upload.</param>
        public void UploadSingleFile(string fileName)
        {
            // Init request.
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(Resources.ServerLocation + fileName);
            
            // Set request type to upload.
            ftp.Method = WebRequestMethods.Ftp.UploadFile;

            ftp.UseBinary = true;

            // Create a byte array and store file data.
            byte[] contents = File.ReadAllBytes(HtmlDirectory + fileName);

            // Set ftp content length to file length.
            ftp.ContentLength = contents.Length;

            // Get the ftp request stream and write the file to it.
            Stream ftpstream = ftp.GetRequestStream();

            ftpstream.Write(contents, 0, contents.Length);
            ftpstream.Close();
        }

        /// <summary>
        /// Checks whether or not a file needs to be uploaded to the remote server.
        /// 
        /// If it does not exists remotely or it's remote version is a different size to the local one,
        /// then this will return true.
        /// </summary>
        /// <param name="fileName">Name of the file to check.</param>
        /// <returns>Whether or not the file needs to be uploaded.</returns>
        public bool FileNeedsUpload(string fileName)
        {
            return (!CheckIfRemoteCopyExists(fileName) || LocalVersionSize(fileName) != RemoteVersionSize(fileName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool CheckIfRemoteCopyExists(string fileName)
        {
            foreach (string s in _remoteFiles)
                if (s.Equals(fileName))
                    return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public FtpUploader()
        {
            _remoteFiles = ConnectAndGetFileList();
        }
    }
}
