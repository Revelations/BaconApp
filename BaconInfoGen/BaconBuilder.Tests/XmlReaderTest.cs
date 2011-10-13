using System;
using System.Collections.Generic;
using System.IO;
using BaconBuilder.Model;
using BaconBuilder.Properties;
using NUnit.Framework;

namespace BaconBuilder
{
	[TestFixture]
	public class XmlReaderTest
	{
		#region Setup/Teardown

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			Directory.CreateDirectory(Dir);
			
			File.WriteAllText(Dir + FileName,
Contents);
		}

		[SetUp]
		public void Setup()
		{
			_reader = new Reader {Page = new FileInfo(Dir + FileName)};
		}

		[TearDown]
		public void Teardown()
		{
			_reader = null;
		}

		[Ignore]
		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
//			File.Delete(Dir + FileName);
//			Directory.Delete(Dir);
		}

		#endregion

		private Reader _reader;
		private static readonly string Dir = "C:/Users/" + Environment.UserName + "/test/";
		private const string FileName = "ParseTest.html";
		private const string Contents = @"<!DOCTYPE HTML>
<html>
	<head>
		<link href=""style.css"" rel=""stylesheet"" />
		<title></title>
		<!-- Push Button=Receive Bacon --><!--  x=42 --><!-- y=13 -->
	</head>
	<body>
		<p>hello world</p>
		<p>ASDF</p>
		<div>
			<img src=""sample.jpg"" />
			<audio src=""sample.mp3"" controls=""controls""></audio>
		</div>
		<p>hello you</p>
	</body>
</html>";

		[Test]
		public void TestBody()
		{
			Assert.AreEqual("<p>hello world</p><p>ASDF</p><div><img src=\"sample.jpg\" /><audio src=\"sample.mp3\" controls=\"controls\"></audio></div><p>hello you</p>", _reader.GetBody());
		}

		[Test]
		public void TestCoords()
		{
			Assert.AreEqual("x=42", _reader.GetProperties("x"));
			Assert.AreEqual("y=13", _reader.GetProperties("y"));
			Assert.AreEqual("x=42", _reader.GetProperties("x"));
			Assert.AreEqual("Push Button=Receive Bacon", _reader.GetProperties("push button"));
			Assert.IsNull(_reader.GetProperties("property does not exist"));

		}

		[Test]
		public void TestSettingCoords()
		{
			Assert.AreEqual("x=42", _reader.GetProperties("x"));
			Assert.AreEqual("<!DOCTYPE HTML><html><head><!-- x=1337 --><title></title></head><body></body></html>", _reader.SetProperties("x", "1337", ""));
			//Assert.AreEqual("x=1337", _reader.GetProperties("x"));
		}

		[Test]
		public void TestPara()
		{
			Assert.AreEqual("hello world", _reader.GetBodyContents()[0], "Tags were not split");
			Assert.AreEqual("ASDF", _reader.GetBodyContents()[1]);
			//Assert.AreEqual("<img>sample.jpg</img>", _reader.GetBodyContents()[1]);
			//Assert.AreEqual("hello you", _reader.GetBodyContents()[2]);
		}
	}
}