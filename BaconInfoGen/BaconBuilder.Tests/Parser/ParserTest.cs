using System.Collections.Generic;
using System.IO;
using BaconBuilder.Model;
using NUnit.Framework;

namespace BaconBuilder
{
    [TestFixture]
    public class ParserTest
    {
        private Parser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = new Parser();
        }

        [TearDown]
        public void TearDown()
        {
            _parser = null;
        }

        [Test]
        public void TestEmptyDict()
        {
            const string input = "The quick brown fox jumped over the lazy dog.";
            const string expected = "The quick brown fox jumped over the lazy dog.";

            Assert.AreEqual(expected, _parser.Parse(input));
        }

        [Test]
        public void TestSimpleReplace()
        {
            _parser.RegexDict.Add("brown", "purple");

            const string input = "The quick brown fox jumped over the lazy dog.";
            const string expected = "The quick purple fox jumped over the lazy dog.";

            Assert.AreEqual(expected, _parser.Parse(input));
        }
    }
}
