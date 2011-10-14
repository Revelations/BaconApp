using System;
using System.IO;
using System.Windows.Forms;
using BaconBuilder.Controller;
using BaconBuilder.Model;
using BaconBuilder.View;
using NUnit.Framework;

namespace BaconBuilder
{
	[TestFixture]
	internal class MainWindowTest
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			_view = new MainWindow();
			_model = new BaconModel();
			_controller = new MainViewController(_model, _view);
			_currentDirNodes = new TreeView().Nodes;
			Directory.CreateDirectory(TestPath);
			//_model.ChangeDirectory(TestPath);
			var tp = new FileInfo(TestPath + TestFile);
			string test = "Push me" + Environment.NewLine + "And then just touch me" + Environment.NewLine +
			              "Till I can get my satisfaction" + Environment.NewLine +
			              "Satisfaction, satisfaction, satisfaction, satifaction";
			File.WriteAllText(tp.FullName, test);
		}

		[TearDown]
		public void TearDown()
		{
			_view = null;
			_model = null;
			_controller = null;
			_currentDirNodes = null;
			Directory.Delete(TestPath, true);
		}

		#endregion

		private MainWindow _view;
		private BaconModel _model;
		private MainViewController _controller;
		private TreeNodeCollection _currentDirNodes;
		private const string TestPath = @"./testFiles/";
		private const string TestFile = @"satisfaction.html";

		[Test]
		public void TestTreeDirHasLoaded()
		{
			Assert.IsEmpty(_currentDirNodes, "Current dir is not empty");

			_controller.InitialiseListView();

			Assert.That(_view.Files.Count == 1, "Content Count:" + _view.Files.Count + " does not equal 1");
			foreach (ListViewItem s in _view.Files)
				_currentDirNodes.Add(s.Text);

			int expectedCount = _currentDirNodes.Count;
			Assert.That(expectedCount == 1, "TreeNodes Count:{0} does not equal 1", expectedCount);

			string expected = _currentDirNodes[0].Text;
			Assert.That(expected == TestFile, "Current Contents[0]:{0} does not equal satisfaction.html", expected);
		}
	}
}