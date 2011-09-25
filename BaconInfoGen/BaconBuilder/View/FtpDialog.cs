using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaconBuilder.Controller;
using BaconBuilder.Model;
using BaconBuilder.Properties;

namespace BaconBuilder.View
{
    /// <summary>
    /// Class that manages the GUI for synchronising with an ftp server.
    /// </summary>
    public partial class FtpDialog : Form
    {
        #region Properties and Attributes

        // Ftp uploader or downloader.
        private FtpHelper _helper;

        // Hard coded html directory. Obviously this is to soon be removed.
        private static readonly string _htmlDirectory = "C:/Users/" + System.Environment.UserName + "/test/";

        // Delegate method to handle thread safe changing of label text.
        delegate void SetTextCallback(string text);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor accepting a single FtpHelper object.
        /// 
        /// The type of this FtpHelper determines whether this dialog box is used for uploading or downloading.
        /// </summary>
        /// <param name="helper">Ftphelper object to use for handling uploads/downloads.</param>
        public FtpDialog(FtpHelper helper)
        {
            InitializeComponent();
            _helper = helper;

            // Set the method used by the worker for doing work. Adjust information label accordingly.
            if (helper is FtpDownloader)
            {
                worker.DoWork += downloadWorker_DoWork;
                labelText.Text = @"Synchronising local directory with server.";
                Text = @"Retrieving Files.";
            }
            else if (helper is FtpUploader)
            {
                worker.DoWork += uploadWorker_DoWork;
                labelText.Text = @"Synchronising server with local directory.";
                Text = @"Sending Files.";
            }
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Called when the form is shown as a dialog or otherwise.
        /// </summary>
        private void FtpDialog_Show(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Threadsafe method for changing the text in the label just above the progress bar.
        /// </summary>
        /// <param name="text"></param>
        private void SetLabelText(string text)
        {
            if (labelProgress.InvokeRequired)
            {
                SetTextCallback stc = SetLabelText;
                Invoke(stc, new object[] { text });
            }
            else
                labelProgress.Text = text;
        }

        #endregion

        #region Background Worker Thread Methods

        /// <summary>
        /// Used by a BackgroundWorker to download all files from a server and update UI accordingly.
        /// </summary>
        private void downloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the helper object to perform transfers.
            FtpDownloader helper = (FtpDownloader)_helper;

            SetLabelText(@"Getting list of files from server.");

            // Get a list of all files on the server, and instantiate a list to store names of those needing download.
            List<string> fileList = helper.ConnectAndGetFileList();

            DirectoryInfo d = new DirectoryInfo(_htmlDirectory);
            foreach (FileInfo f in d.GetFiles())
            {
                if(!fileList.Contains(f.Name))
                    f.Delete();
            }

            List<string> neededFiles = new List<string>();

            SetLabelText(@"Checking file versions.");

            // Determine if each file on the server needs to be downloaded.
            for (int i = 0; i < fileList.Count; i++)
            {
                if (helper.FileNeedsDownload(fileList[i]))
                    neededFiles.Add(fileList[i]);

                // Update progress. Use float to maintain fraction.
                float progress = (float)(i + 1) / fileList.Count * 100;
                worker.ReportProgress((int)progress);
            }

            SetLabelText(@"Downloading required files.");

            // Download each file that needs it.
            for (int i = 0; i < neededFiles.Count; i++)
            {
                helper.DownloadSingleFile(neededFiles[i]);

                // Update progress. Use float to maintain fraction.
                float progress = (float)(i + 1) / neededFiles.Count * 100;
                worker.ReportProgress((int)progress);
            }
        }

        /// <summary>
        /// Used by a BackgroundWorker to upload all files to a server and update UI accordingly.
        /// </summary>
        private void uploadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the helper object to perform transfers.
            FtpUploader helper = (FtpUploader)_helper;

            SetLabelText(@"Getting list of files from server.");

            // Instantiate a list to store files needing transfer.
            List<string> neededFiles = new List<string>();

            // Get a list of all files in the html directory.
            // TODO: Embed html directory into the resources file.
            DirectoryInfo info = new DirectoryInfo(_htmlDirectory);
            FileInfo[] files = info.GetFiles();

            foreach (string s in helper.RemoteFiles)
            {
                bool presentLocal = false;

                foreach (FileInfo f in files)
                {
                    if (f.Name.Equals(s))
                    {
                        presentLocal = true;
                        break;
                    }
                }

                if (!presentLocal)
                {
                    helper.DeleteRemoteFile(s);

                }
                   
            }

            SetLabelText(@"Checking file versions.");

            // Check each file in the directory to see if upload is needed.
            for (int i = 0; i < files.Length; i++)
            {
                if (helper.FileNeedsUpload(files[i].Name))
                    neededFiles.Add(files[i].Name);

                // Update progress. Use float to maintain fraction.
                float progress = (float)(i + 1) / files.Length * 100;
                worker.ReportProgress((int)progress);
            }

            SetLabelText(@"Uploading required files.");

            // Upload each file that requires it.
            for (int i = 0; i < neededFiles.Count; i++)
            {
                helper.UploadSingleFile(neededFiles[i]);

                // Update progress. Use float to maintain fraction.
                float progress = (float)(i + 1) / neededFiles.Count * 100;
                worker.ReportProgress((int)progress);
            }
        }

        /// <summary>
        /// Called when the worker reports that progress has changed on it's task.
        /// </summary>
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Called when the worker has completed it's task.
        /// </summary>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}
