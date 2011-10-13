using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaconBuilder.Model;
using BaconBuilder.Model.Ftp;
using NUnit.Framework;

namespace BaconBuilder
{
	[TestFixture]
	class LogGeneratorTest
	{
		[Test]
		public void TestFileLocation()
		{
			Assert.AreEqual("C:/Users/" + Environment.UserName + "/test/log.txt", LogGenerator.FilePath);
		}

		[Test]
		public void TestFileRetrieval()
		{
			LogGenerator.
		}
	}
}
