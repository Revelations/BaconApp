using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaconBuilder.View;
using NUnit.Framework;

namespace BaconBuilder
{
    [TestFixture]
    class MainWindowTest
    {
        TreeView currentDir;
        #region Setup/Teardown
        [SetUp]
        public void SetUp()
        {
            //MainWindow testWindow = new MainWindow();
            currentDir = new TreeView();
        }

        [TearDown]
        public void TearDown() 
        {
        }
        #endregion

        [Test]
        public void TestTreeDirHasLoaded()
        {
            Assert.IsEmpty(currentDir.Nodes, "current dir is not empty");
        }
    
    }
}
