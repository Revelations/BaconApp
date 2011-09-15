using NUnit.Framework;

namespace BaconApp
{
	[TestFixture]
	public class XmlBuilderTest
	{
		[Test]
		public void TestOpenCloseTags()
		{
			Assert.AreEqual("<html></html>", new XmlBuilder("html").ToXml());
			Assert.AreEqual("<head></head>", new XmlBuilder("head").ToXml());
		}

		[Test]
		public void TestTagWithValue()
		{
			var titleBuilder = new XmlBuilder("title");
			titleBuilder.AddValue("someValue");
			Assert.AreEqual("<title>someValue</title>", titleBuilder.ToXml());
		}
	}
}
