using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BaconBuilder.Feedback
{
	[TestFixture]
	internal class StatisticsCalculatorTest
	{
		readonly string[] _strings = "a b c d e".Split(' ');
		readonly int[] _values = {7, 42, 36, 42, 1};
		private Dictionary<string, int> _dictionary;
		[SetUp]
		public void Setup()
		{
			_dictionary = new Dictionary<string, int>
			             	{
			             		{"a", 7}, {"b", 42}, {"c", 36}, {"d", 42}, {"e", 1}
			             	};
		}

		[Test]
		public void GetLargestGroups()
		{
			var d = new Dictionary<string, int>();


			for (int i = 0; i < _strings.Length; i++)
			{
				int old;
				d.TryGetValue(_strings[i], out old);
				d[_strings[i]] = old + _values[i];
			}

			for (int i = 0; i < _strings.Length; i++)
			{
				Assert.AreEqual(_values[i], d[_strings[i]]);
			}

			var largest = new List<KeyValuePair<string, int>> {new KeyValuePair<string, int>("None", 0)};
			int max = 0;

			// Iterate and find maximum values.
			foreach (var kvp in d)
			{
				if (kvp.Value > max)
				{
					largest.Clear();
					max = kvp.Value;
				}
				if (kvp.Value == max)
					largest.Add(kvp);
			}

			Assert.AreEqual(2, largest.Count);
			Assert.IsTrue(largest.Contains(new KeyValuePair<string, int>("b", 42)));
			Assert.IsTrue(largest.Contains(new KeyValuePair<string, int>("d", 42)));
		}

		[Test]
		public void UpdateDictionaryValuesIfExistsAddIfElse()
		{
			var d = new Dictionary<string, int>();

			for (int i = 0; i < _strings.Length; i++)
			{
				int old;
				d.TryGetValue(_strings[i], out old);
				d[_strings[i]] = old + _values[i];
			}

			for (int i = 0; i < _strings.Length; i++)
			{
				Assert.AreEqual(_values[i], d[_strings[i]]);
			}
		}

		[Test]
		public void TestMostCommon()
		{
			var largest = new Dictionary<string, int>();

			// Init a dictionary to store quantities of people of varying nationalities.
			Dictionary<string, int> dict = _dictionary;
			int max = dict.Values.Max();

			foreach (var kvp in dict)
			{
				if (max == kvp.Value)
					largest.Add(kvp.Key, kvp.Value);
			}

			Assert.AreEqual(42, max);
			Assert.Contains("b", largest.Keys);
			Assert.Contains("d", largest.Keys);
			largest.Clear();

			largest = dict.Where(kvp => max == kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

			Assert.AreEqual(42, max);
			Assert.Contains("b", largest.Keys);
			Assert.Contains("d", largest.Keys);
		}
	}
}