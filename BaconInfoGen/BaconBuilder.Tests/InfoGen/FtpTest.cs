using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaconBuilder.Model.Ftp;
using NUnit.Framework;

namespace BaconBuilder.InfoGen
{
	[TestFixture]
	class FtpTest
	{
		[Test]
		public void TestUri()
		{
			Assert.AreEqual("ftp://revelations.webhop.org:1234/", FtpHelper.FtpUri());
			Assert.AreEqual("ftp://revelations.webhop.org:1234/Hello", FtpHelper.FtpUri("Hello"));
		}
	}
}
