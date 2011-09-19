using System.IO;
using System.Web.UI;
using System.Collections.Generic;
using System.Windows.Forms;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();
		}

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {

        }

        private void MainWindow_Load(object sender, System.EventArgs e)
        {
            if (!Directory.Exists("./bin/dataFiles")) 
            {
                Directory.CreateDirectory("./bin/dataFiles");
            }

            var directory = new DirectoryInfo("./html");
            FileHandler fh = new FileHandler(".html");

            foreach (FileInfo fileInfo in directory.GetFiles())
            {
                treeDirectory.Nodes.Add(fileInfo.Name);
                fh.LoadFile(fileInfo);
            }
        }
	}
}
