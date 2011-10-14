using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BaconBuilder.Model;
using NUnit.Framework;

namespace BaconBuilder
{
	[TestFixture]
	internal class LogGeneratorTest
	{
		[Test]
		public void TestFileLocation()
		{
			Assert.AreEqual("C:/Users/" + Environment.UserName + "/test/log.txt", LogGenerator.FilePath);
		}

		[Test]
		public void TestFileRetrieval()
		{
			var info = new FileInfo(LogGenerator.FilePath);
			Assert.IsNotNull(info.Directory);
			var dir = new DirectoryInfo(info.Directory.FullName);
			Assert.AreNotEqual(0, dir.GetFiles().Length);
			var files = new List<object>();
			LogGenerator.GetFiles(files, LogGenerator.Purpose.Info);
			Assert.AreNotEqual(0, files.Count);
		}
	}
}