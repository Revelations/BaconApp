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

        [Test]
        public void TestComplexReplace()
        {
            const string input = "<img = example.jpg>";
            const string expected = "<img src='example.jpg' />";

            Assert.AreEqual(expected, _parser.Parse(input));
        }
    }
}
