using System.Collections.Generic;
using System.IO;
using BaconBuilder.Model;
using NUnit.Framework;

namespace BaconBuilder
{
    [TestFixture]
    public class HtmlToTextParserTest
    {
        private Parser _parser;

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

        [Test]
        public void TestImageReplace()
        {
            const string input = "<img src=\"example.jpg\" />";
            const string expected = "<img>example.jpg</img>";

            Assert.AreEqual(expected, _parser.Parse(input));
        }

        [Test]
        public void TestAudioReplace()
        {
            const string input = "<audio src=\"example.mp3\" controls=\"controls\" style=\"float:left;\"></audio>";
            const string expected = "<audio>example.mp3</audio>";

            Assert.AreEqual(expected, _parser.Parse(input));
        }

        [Test]
        public void TestParseImage()
        {
            var inputs = new string[]
                             {
                                 @"<img src=""example.jpg""/>",
                                 @"   < img     src = ""example.jpg""/> ",
                                 @"<   img src = ""example.jpg""/>",
                                 @" <imgsrc = ""example.jpg""/> "
                             };
            var outputs = new string[]
                              {
                                  @"<img>example.jpg</img>",
                                  @"   <img>example.jpg</img> ",
                                  @"<img>example.jpg</img>",
                                  @" <imgsrc = ""example.jpg""/> "
                              };
            for (var i = 0; i < inputs.Length; i++)
            {
                var expected = outputs[i];
                var actual = _parser.Parse(inputs[i]);

                Assert.AreEqual(expected, actual, "Expected {0} but was {1}, index {2}", expected, actual, i);
            }
        }
    }
}
