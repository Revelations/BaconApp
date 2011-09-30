using BaconBuilder.Properties;
using NUnit.Framework;

namespace BaconBuilder
{
	[TestFixture]
	public class XmlReaderTest
	{
		#region Setup/Teardown

		[SetUp]
		public void Setup()
		{
			_reader = new Reader();
		}

		#endregion

		private Reader _reader;

		[Test]
		public void EnsureTestHtmlIsCorrect()
		{
			const string expected = 
				@"<!DOCTYPE HTML><html><head><link href=""style.css"" rel=""stylesheet"" />" +
				@"<title></title><!-- x = 42 --><!-- y = 13 --></head><body><p>hello world</p><img src=""sample.jpg"" /><p>hello you</p></body></html>";

			Assert.AreNotEqual("", Resources.TestFile);
			//Assert.AreEqual(expected, Resources.TestFile);
		}

		[Test]
		public void TestCoords()
		{
			Assert.AreEqual("x = 42", _reader.GetProperties("x"));
			Assert.AreEqual("y = 13", _reader.GetProperties("y"));
			Assert.AreEqual("x = 42", _reader.GetProperties("x"));
			Assert.AreEqual("Push Button = Receive Bacon", _reader.GetProperties("push button"));
		}

		[Test]
		public void TestBody()
		{
			Assert.AreEqual("<p>hello world</p><img src=\"sample.jpg\" /><p>hello you</p>", _reader.GetBody());
		}

		[Test]
		public void TestPara()
		{
			Assert.AreEqual("hello world", _reader.GetBodyContents()[0], "Tags were not split");
			Assert.AreEqual("<img>sample.jpg</img>", _reader.GetBodyContents()[1]);
			Assert.AreEqual("hello you", _reader.GetBodyContents()[2]);
		}
	}
}