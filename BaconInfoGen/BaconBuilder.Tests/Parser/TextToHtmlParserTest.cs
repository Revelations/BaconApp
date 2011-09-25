using System.Collections.Generic;
using System.IO;
using BaconBuilder.Model;
using NUnit.Framework;
 
namespace BaconBuilder
{
    [TestFixture]
    public class TextToHtmlParserTest
    {
        private Parser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = new TextToHtmlParser();
        }

        [TearDown]
        public void TearDown()
        {
            _parser = null;
        }

        /*[Test]
        public void TestImageReplace()
        {
            List<string> input = new List<string>
                                     {
                                         "<img>example.jpg</img>",
                                         "<img>  example.jpg <  /img >",
                                         "<    img  >example.jpg  </  img     >"
                                     };

            const string expected = "<img src=\"example.jpg\" />";

            foreach (string s in input)
                Assert.AreEqual(expected, _parser.Parse(s));
        }

        [Test]
        public void TestAudioReplace()
        {
            List<string> input = new List<string>
                                     {
                                         "<audio>example.mp3</audio>",
                                         "<   audio >   example.mp3     </ audio  >",
                                         "<audio  >   example.mp3<  / audio>"
                                     };

            const string expected = "<audio src=\"example.mp3\" controls=\"controls\" style=\"float:left;\"></audio>";

            foreach (string s in input)
                Assert.AreEqual(expected, _parser.Parse(s));
        }*/

        [Test]
        public void TestParse()
        {
            _parser.X = 100;
            _parser.Y = 200;

            string expected = @"<!DOCTYPE HTML>
<html>
<head>
<link href=""style.css"" />
<title></title>
<!-- x = 100 -->
<!-- y = 200 -->
</head>
<body>
</body>
</html>".Replace(System.Environment.NewLine, "");

            string actual = _parser.Parse("").Replace(System.Environment.NewLine, "");

            Assert.AreEqual(expected, actual);
        }
    }
}
