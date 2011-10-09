using System;
using System.Collections.Generic;
using System.IO;

namespace BaconFeedback
{
    /// <summary>
    /// 
    /// </summary>
    public class FileHandler
    {
        public FileHandler()
        {
            if (!Directory.Exists(_feedbackDirectory))
                Directory.CreateDirectory(_feedbackDirectory);
        }
        // We all recognize this, amirite?
        private static readonly string _feedbackDirectory = "C:/Users/" + System.Environment.UserName + "/FeedbackTest/";

        // Feedback file extension.
        private const string _feedbackExtension = ".fbk";

        public static IEnumerable<string> GetSubfolders()
        {
            String[] s = Directory.GetDirectories(_feedbackDirectory);
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = GetShortDirectoryName(s[i]);
            }

            return s;
        }

        public static IEnumerable<string> GetFeedbackFiles(string directory)
        {
            List<string> result = new List<string>();

            foreach(string s in Directory.GetFiles(_feedbackDirectory + directory))
            {
                FileInfo f = new FileInfo(s);

                if(f.Extension == _feedbackExtension)
                {
                    result.Add(f.Name);
                }
            }

            return result;
        }

        private static string GetShortDirectoryName(string directory)
        {
            string[] split = directory.Split('/');
            return split[split.Length - 1];
        }

        public static string GetCreationDate(string path)
        {
            FileInfo f = new FileInfo(_feedbackDirectory + path);
            return f.CreationTime.ToString();
        }

        public static string[] GetFeedbackContents(string path)
        {
            return File.ReadAllLines(_feedbackDirectory + path);
        }

        public static void DeleteFolder(string folder)
        {
            string[] files = Directory.GetFiles(_feedbackDirectory + folder);
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            Directory.Delete(_feedbackDirectory + folder);
        }

        public static void DeleteFile(string path)
        {
            Console.WriteLine(_feedbackDirectory + path);
            File.Delete(_feedbackDirectory + path);
        }
    }
}
