using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
//using Microsoft.VisualStudio.TestTools.UnitTesting
using BaconBuilder.Model;

namespace BaconBuilder
{
	[TestFixture]
	public class HtmlDegenTest
	{
		string testdir = "testFiles//";
		private HtmlDegen _degen;
		
		[SetUp]
		public void SetUp()
		{
			_degen = new HtmlDegen();
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
			_degen.ReadHtml(testdir + "helloworld.html");
			List<string> body = _degen.Body;

			Assert.IsNotNull(_degen.Body);
			
			Assert.AreEqual("<p>Around the World</p>", body[0]);
			Assert.AreEqual("<p>Harder, Better, Faster, Stronger</p>", body[1]);
		}

	}
}
