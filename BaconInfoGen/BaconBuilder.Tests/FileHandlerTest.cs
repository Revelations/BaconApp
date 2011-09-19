using System.Collections.Generic;
using System.IO;
using BaconBuilder.Model;
using NUnit.Framework;
using System;

namespace BaconBuilder
{
	[TestFixture]
	public class FileHandlerTest
	{
		#region Setup/Teardown
        #region vars
        FileHandler _handler;
        FileInfo _info, _fileNonExistent;
        string testDir;
        string[] _satisfactionLyrics = {
                                            "Push me",
		                                    "And then just touch me",
		                                    "Till I can get my satisfaction",
		                                    "Satisfaction, satisfaction, satisfaction, satisfaction"
		                               };
        #endregion
        [SetUp]
		public void SetUp()
		{
            
            testDir = @"./testFiles/";
            Directory.CreateDirectory(testDir);
			FileInfo tp = new FileInfo(testDir + "satisfaction.txt");
            //System.IO.File.WriteAllLines(tp.FullName, _satisfactionLyrics);
            string test = "Push me" + System.Environment.NewLine + "And then just touch me" + System.Environment.NewLine + "Till I can get my satisfaction" +System.Environment.NewLine + "Satisfaction, satisfaction, satisfaction,satifaction";
            System.IO.File.WriteAllText(tp.FullName, test);

            _handler = new FileHandler(".txt");
            _info = new FileInfo(testDir + "satisfaction.txt"); 
            _fileNonExistent = new FileInfo(testDir + "ThisFileDoesNotExist");
		}

		[TearDown]
		public void TearDown()
		{
            System.IO.File.Delete(testDir + "satisfaction.txt");
			_handler = null;
            _info = null;
		}

		#endregion

		[Test]
		public void TestFileHasBeenModified()
        {

  //          FileInfo tp = new FileInfo(testDir + "satisfaction.txt");
//            System.IO.File.WriteAllLines(tp.FullName, _satisfactionLyrics);

    //        System.Windows.Forms.MessageBox.Show(File.Exists(tp.FullName).ToString());
            Assert.IsTrue(File.Exists(_info.FullName));

			Assert.IsFalse(_handler.HasFileBeenModified(_info));

			_handler.LoadFile(_info);

			Assert.IsFalse(_handler.HasFileBeenModified(_info));

			var temp = new FileHandler(".txt");
			temp.UpdateFileContentInMemory(_info, new[] {"Hello World", "This is a test"});
			temp.SaveFile(_info);
			Assert.IsTrue(_handler.HasFileBeenModified(_info));
		}

		[Test]
		public void TestFileIsLoaded()
		{
			Assert.IsFalse(_handler.IsFileInMemory(_info));
			_handler.LoadFile(_info);
			Assert.IsTrue(_handler.IsFileInMemory(_info));
		}

		[Test]
		public void TestFileLoad()
		{
			IEnumerable<string> nullFile = null;
			IEnumerable<string> notNullFile = null;
			try
			{
				_handler.LoadFile(_info);
				notNullFile = _handler.GetFileFromMemory(_info);

				_handler.LoadFile(_fileNonExistent);
				nullFile = _handler.GetFileFromMemory(_fileNonExistent);
			}
			catch (FileNotFoundException)
			{
				Assert.IsNotNull(notNullFile);
				Assert.IsNull(nullFile);
			}

			IEnumerator<string> enumerator = notNullFile.GetEnumerator();

			enumerator.MoveNext();
            Assert.AreEqual("Push me", enumerator.Current);

			enumerator.MoveNext();
            Assert.AreEqual("And then just touch me", enumerator.Current);
		}

		[Test]
		public void TestFileSave()
		{
			var content = new List<string>(_satisfactionLyrics);

			_handler.UpdateFileContentInMemory(_info, content);
			_handler.SaveFile(_info);
			Assert.IsTrue(File.Exists(_info.FullName), "{0} should exist.", _info.Name);

			_handler.LoadFile(_info);
			IEnumerator<string> enumerator = _handler.GetFileFromMemory(_info).GetEnumerator();
			enumerator.MoveNext();

			const string expected = "Push me";
			string result = enumerator.Current;
			Assert.AreEqual(expected, result);

			Assert.IsFalse(_handler.HasFileBeenModified(_info));
		}

		[Test]
		public void TestLoadDirectory()
		{
			Assert.IsFalse(_handler.IsFileInMemory(_info), "_handler should not be in memory");

			var directory = new DirectoryInfo(testDir);
            _handler.LoadDirectory(directory);

			Assert.IsTrue(_handler.IsFileInMemory(_info), "_info should be in memory");
			Assert.IsFalse(_handler.HasFileBeenModified(_info), "File should not have been modified");
		}
	}
}