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
        private const string HtmlPath = "C:\\Users/jlm47/test";
        //private const string HtmlPath = "./bin/DataFiles";

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
    }
}
