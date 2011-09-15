using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BaconBuilder
{
	[TestFixture]
	public class HtmlDegenTest
	{
		private HtmlDegen _degen;
		
		[SetUp]
		public void SetUp()
		{
			_degen = new HtmlDegen();
			_degen.ReadHtml("file:///C:/Users/Noriko/Desktop/bacon/helloworld.html");
		}

		[TearDown]
		public void TearDown()
		{
			_degen = null;
		}

		/// <summary>
		/// See if we can get the contents of the body.
		/// </summary>
		[Test]
		public void TestGetBody()
		{
			List<string> body = _degen.Body;
			Assert.AreEqual("<p>Around the World</p>", body[0]);
			Assert.AreEqual("<p>Harder, Better, Faster, Stronger</p>", body[1]);
		}

	}
}
