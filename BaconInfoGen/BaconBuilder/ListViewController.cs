using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaconBuilder
{
    public class ListViewController
    {
        private ListView _listView;

        public void PopulateWithDirectory(string directory)
        {
            DirectoryInfo info = new DirectoryInfo(directory);
            foreach (FileInfo file in info.GetFiles())
            {
                ListViewItem item = new ListViewItem(file.Name, 1);

                _listView.Items.Add(item);
            }
        }

        public ListViewController(ListView listView)
        {
            _listView = listView;
        }
    }
}
