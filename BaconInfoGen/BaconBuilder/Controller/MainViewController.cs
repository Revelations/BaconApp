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
        private const string HtmlPath = "C:\\Users/jlm47/test/";
        //private const string HtmlPath = "./bin/DataFiles";

        private static HtmlToTextParser _parser = new HtmlToTextParser();

        public static void InitialiseListView(ListView listView)
        {
            if (!Directory.Exists(HtmlPath))
            {
                Directory.CreateDirectory(HtmlPath);
            }

            var directory = new DirectoryInfo(HtmlPath);

            foreach (FileInfo f in directory.GetFiles())
            {
                if (f.Extension.Equals(".html"))
                    listView.Items.Add(f.Name, 0);
            }
        }

        public static string GetFileText(ListView listView)
        {
            if (listView.SelectedItems.Count > 0)
            {
                string htmlContent = File.ReadAllText(HtmlPath + listView.SelectedItems[0].Text);
                return _parser.Parse(htmlContent);
            }
            else
                return string.Empty;
        }
    }
}
