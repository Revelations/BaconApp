using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaconBuilder.View;
using BaconBuilder.Model;
using System.IO;
using NUnit.Framework;

namespace BaconBuilder
{
    [TestFixture]
    class MainWindowTest
    {
        TreeView currentTreeDir;
        string testPath;
        FileHandler fH;
        #region Setup/Teardown
        [SetUp]
        public void SetUp()
        {

            
            currentTreeDir = new TreeView();
            testPath = @"./testFiles/";
            Directory.CreateDirectory(testPath);
            FileInfo tp = new FileInfo(testPath + "satisfaction.html");
            string test = "Push me" + System.Environment.NewLine + "And then just touch me" + System.Environment.NewLine + "Till I can get my satisfaction" + System.Environment.NewLine + "Satisfaction, satisfaction, satisfaction,satifaction";
            System.IO.File.WriteAllText(tp.FullName, test);
            fH = new FileHandler(".html");
        }

        [TearDown]
        public void TearDown() 
        {
            currentTreeDir = null;
            Directory.Delete(testPath, true);
            testPath = "";
        }
        #endregion

        [Test]
        public void TestTreeDirHasLoaded()
        {
            Assert.IsEmpty(currentTreeDir.Nodes, "current dir is not empty");
            List<string> content = fH.LoadDirectory(new DirectoryInfo(testPath));
            Assert.That(content.Count == 1,"Content Count:" + content.Count + " does not equal 1");
            foreach (string s in content)
                currentTreeDir.Nodes.Add(s);
            Assert.That(currentTreeDir.Nodes.Count == 1, "TreeNodes Count:" + currentTreeDir.Nodes.Count + " does not equal 1");
            Assert.That(currentTreeDir.Nodes[0].Text == "satisfaction.html", "Current Contents[0]:" + currentTreeDir.Nodes[0].Text + " does not equal satisfaction.html");
        }
    
    }
}
