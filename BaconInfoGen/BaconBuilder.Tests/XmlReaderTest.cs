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
			//Directory.CreateDirectory(Dir);
			
			//File.WriteAllText(Dir + FileName, Contents);
		}

		[SetUp]
		public void Setup()
		{
			_reader = new Reader
			          	{
			          		//Page = new FileInfo(Dir + FileName),
						Body = Contents
			          	};
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

		[Test, ExpectedException(typeof(KeyNotFoundException))]
		public void TestCoords()
		{
			var actual = _reader.Properties;
			Assert.That(actual.ContainsKey("x"));
			Assert.AreEqual("42", actual["x"]);

			Assert.That(actual.ContainsKey("y"));
			Assert.AreEqual("13", actual["y"]);
			
			Assert.That(actual.ContainsKey("x"));
			Assert.AreEqual("42", actual["x"]);
			
			Assert.That(actual.ContainsKey("Push Button"));
			Assert.AreEqual("Receive Bacon", actual["Push Button"]);
			
			Assert.That(!actual.ContainsKey("missing"));
			Assert.AreEqual("HelloWorld", actual["missing"]);
		}

		[Test]
		public void TestSettingCoords()
		{
			Assert.AreEqual("42", _reader.Properties["x"]);
			Assert.AreEqual("13", _reader.Properties["y"]);

			Assert.AreEqual(Contents, _reader.Body);

			_reader.Properties["x"] = "1337";
			_reader.UpdateDocument();

			Assert.AreEqual("<!DOCTYPE HTML><html><head><!-- Push Button=Receive Bacon --><!-- x=1337 --><!-- y=13 --><title></title></head><body></body></html>", _reader.Body);

			Assert.AreEqual("1337", _reader.Properties["x"]);
			
			_reader.Properties["y"] = "101";
			_reader.UpdateDocument();

			Assert.AreEqual("<!DOCTYPE HTML><html><head><!-- Push Button=Receive Bacon --><!-- x=1337 --><!-- y=101 --><title></title></head><body></body></html>", _reader.Body);

			Assert.AreEqual("101", _reader.Properties["y"]);
		}

		[Ignore]
		[Test]
		public void TestPara()
		{
			Assert.AreEqual("hello world", _reader.GetBodyContents()[0], "Tags were not split");
			Assert.AreEqual("ASDF", _reader.GetBodyContents()[1]);
			Assert.AreEqual("<img>sample.jpg</img>", _reader.GetBodyContents()[1]);
			Assert.AreEqual("hello you", _reader.GetBodyContents()[2]);
		}
	}
}