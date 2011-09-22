using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BaconBuilder.Model;
using BaconBuilder.View;

namespace BaconBuilder.Controller
{
    class MainViewController
    {
        // Test directory. Needs to be removed at some point.
        //private static readonly string HtmlDirectory = "C:\\Users/"+System.Environment.UserName+"/test/";

        // Directory for local html content.
        private const string HtmlDirectory = "./DataFiles";

        // Parser object to handle html to text conversion.
        private static readonly HtmlToTextParser HtmlToText = new HtmlToTextParser();
        private static readonly TextToHtmlParser TextToHtml = new TextToHtmlParser();

        /// <summary>
        /// Initialises and populates a listview with the html files in a directory.
        /// </summary>
        /// <param name="listView">The listview to initialise.</param>
        public static void InitialiseListView(ListView listView)
        {
            // Create the directory if it doesn't exist.
            if (!Directory.Exists(HtmlDirectory))
                Directory.CreateDirectory(HtmlDirectory);

            // Get a directory info object for the directory.
            var directory = new DirectoryInfo(HtmlDirectory);

            // Add each item to the list view.
            foreach (FileInfo f in directory.GetFiles())
            {
                if (f.Extension.Equals(".html"))
                    listView.Items.Add(f.Name, 0);
            }
        }

        /// <summary>
        /// Gets the text content of an HTML file.
        /// 
        /// Uses an HtmlToTextParser to parse its content to plain text.
        /// </summary>
        /// <param name="filename">Name of file to get data from.</param>
        /// <returns>String content (plain text) of the file.</returns>
        public static string GetFileText(string filename)
        {
                // Read the selected HTML file and store it.
                string htmlContent = File.ReadAllText(HtmlDirectory + filename);

                // Return plain text version of the above.
                return HtmlToText.Parse(htmlContent);
        }

        /// <summary>
        /// Gets the html parsed verision of plain text content and saves it to a file.
        /// </summary>
        /// <param name="filename">The filename of the file to save to.</param>
        /// <param name="input">The input string to parse and write to file.</param>
        public static void SaveFileHtml(string filename, string input)
        {
            string htmlContent = TextToHtml.Parse(input);

            File.WriteAllText(HtmlDirectory + filename, htmlContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetHtmlContent(string input)
        {
            return HtmlToText.Parse(input);
        }
    }
}
