using System.Drawing;
using BaconBuilder.Model;
using NUnit.Framework;

namespace BaconBuilder
{
	[TestFixture]
	public class HtmlToTextParserTest
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			_parser = new HtmlToTextParser();
		}

		[TearDown]
		public void TearDown()
		{
			_parser = null;
		}

		#endregion

		private HtmlToTextParser _parser;

		[Test]
		public void TestAudioReplace()
		{
			var inputs = new[]
			             	{
			             		@"<audio></audio>",
			             		@"<audio src=""example.mp3"" controls=""controls"" style=""float:left;""></audio>",
			             		@"<audio src=""example.mp3"" controls=""controls"" style=""float:left;"">Your browser does not support audio tags</audio>"
			             		,
			             		@"<audio controls=""controls"" src=""example.mp3"" style=""float:left;"">Your browser does not support audio tags. I think someone might alter this externally....</audio>"
			             		,
			             		@"<audio controls src=""example.mp3"" style=""float:left;"">Your browser does not support audio tags</audio>"
			             	};
			var outputs = new[]
			              	{
			              		"<audio></audio>",
			              		"<audio>example.mp3</audio>",
			              		"<audio>example.mp3</audio>",
			              		"<audio>example.mp3</audio>",
			              		"<audio>example.mp3</audio>"
			              	};

			for (int i = 0; i < inputs.Length; i++)
			{
				string expected = outputs[i];
				string actual = _parser.Parse(inputs[i]);

				Assert.AreEqual(expected, actual, "Expected {0} but was {1}, index {2}", expected, actual, i);
			}
		}

		[Test]
		public void TestExtractXY()
		{
			string input = "<!--x =  600 --><!-- y = 300 -->";
			var expected = new Point(600, 300);

			Assert.AreEqual(expected, _parser.ExtractXY(input));
		}

		[Test]
		public void TestImageReplace()
		{
			const string input = "<img src=\"example.jpg\" />";
			const string expected = "<img>example.jpg</img>";

			Assert.AreEqual(expected, _parser.Parse(input));
		}

		[Test]
		public void TestParagraphParse()
		{
			//Assert.AreEqual(@"Hello World", _parser.Parse(@"<body><p>Hello World</p></body>"));
			Assert.AreEqual(@"Hello World\n\nI like bacon", _parser.Parse(@"<body><p>Hello World</p><p>I like bacon</p></body>"));
		}

		[Test]
		public void TestParseImage()
		{
			var inputs = new[]
			             	{
			             		@"<img src=""example.jpg""/>",
			             		@"   < img     src = ""example.jpg""/> ",
			             		@"<   img src = ""example.jpg""/>",
			             		@" <imgsrc = ""example.jpg""/> "
			             	};
			var outputs = new[]
			              	{
			              		@"<img>example.jpg</img>",
			              		@"   <img>example.jpg</img> ",
			              		@"<img>example.jpg</img>",
			              		@" <imgsrc = ""example.jpg""/> "
			              	};
			for (int i = 0; i < inputs.Length; i++)
			{
				string expected = outputs[i];
				string actual = _parser.Parse(inputs[i]);

				Assert.AreEqual(expected, actual, "Expected {0} but was {1}, index {2}", expected, actual, i);
			}
		}
	}
}