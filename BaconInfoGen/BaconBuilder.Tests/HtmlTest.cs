
using NUnit.Framework;

namespace BaconApp
{
	[TestFixture]
	public class HtmlTest
	{
		private HtmlGenerator _kiwiBuilder;
		private HtmlGenerator _tuiBuilder;

		[SetUp]
		public void SetUp()
		{
			_kiwiBuilder = new HtmlGenerator("kiwi");
			_tuiBuilder = new HtmlGenerator("tui");
			
		}

		[Test]
		public void TestTags()
		{
			Assert.AreEqual("<!DOCTYPE HTML><html><head><title>kiwi</title></head><body></body></html>", _kiwiBuilder.ToHtml());
			Assert.AreEqual("<!DOCTYPE HTML><html><head><title>tui</title></head><body></body></html>", _tuiBuilder.ToHtml());
		}

		[Test]
		public void TestBody()
		{
			_kiwiBuilder.Display();
			Assert.IsNotNull(_kiwiBuilder.GetTag("html"));
			Assert.AreEqual("<body></body>", _kiwiBuilder.GetTag("body").ToString());
		}

//		[Test]
//		public void TestAddingContent()
//		{
//			_kiwiBuilder.AddContent("A kiwi is a kiwi");
//			//Assert.AreEqual("<body><p>A kiwi is a kiwi</p></body>", _kiwiBuilder.GetTag(child => child is Node && ((Node)child).Name.Equals("body"), "body"));
//		}
	}
}
