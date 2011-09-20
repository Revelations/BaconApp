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
        public void TestComplexReplace()
        {
            const string input = "<img src='example.jpg' />";
            const string expected = "<img = example.jpg>";

            Assert.AreEqual(expected, _parser.Parse(input));
        }
    }
}
