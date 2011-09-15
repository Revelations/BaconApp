using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace BaconApp
{
	[TestFixture]
	public class FileHandlerTest
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			_handler = new FileHandler(".txt");
		}

		[TearDown]
		public void TearDown()
		{
			_handler = null;
		}

		#endregion

		private FileHandler _handler;
		private readonly FileInfo _info = new FileInfo("C:/bin/satisfaction.txt");

		private readonly string[] _satisfactionLyrics = new[]
		                                                	{
		                                                		"Push me",
		                                                		"And then just touch me",
		                                                		"Till I can get my satisfaction",
		                                                		"PathForTesting, satisfaction, satisfaction, satisfaction"
		                                                	};

		[Test]
		public void TestFileHasBeenModified()
		{
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
				var test1 = new FileInfo("C:/bin/test.txt");
				var test2 = new FileInfo("C:/bin/ThisFileDoesNotExist");

				_handler.LoadFile(test1);
				notNullFile = _handler.GetFileFromMemory(test1);

				_handler.LoadFile(test2);
				nullFile = _handler.GetFileFromMemory(test2);
			}
			catch (FileNotFoundException)
			{
				Assert.IsNotNull(notNullFile);
				Assert.IsNull(nullFile);
			}

			IEnumerator<string> enumerator = notNullFile.GetEnumerator();

			enumerator.MoveNext();
			Assert.AreEqual("All work and no play makes Johnny a very lazy boy.", enumerator.Current);

			enumerator.MoveNext();
			Assert.AreEqual("She sells sea shells on the sea shore.", enumerator.Current);
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
			Assert.IsFalse(_handler.IsFileInMemory(_info));

			var directory = new DirectoryInfo("C:/bin");
			foreach (FileInfo fileInfo in directory.GetFiles())
			{
				try
				{
					_handler.LoadFile(fileInfo);
				}
				catch (IOException)
				{
				}
			}

			Assert.IsTrue(_handler.IsFileInMemory(_info));
			Assert.IsFalse(_handler.HasFileBeenModified(_info));
		}
	}
}