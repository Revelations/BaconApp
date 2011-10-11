using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BaconBuilder.Feedback
{
	[TestFixture]
	class StatisticsCalculatorTest
	{
		[Test]
		public void UpdateDictionaryValuesIfExistsAddIfElse()
		{
			var d = new Dictionary<string, int>();

			string[] strings = "a b c d e".Split(' ');
			int[] values = {42, 7, 36, 98, 402};

			for (int i = 0; i < strings.Length; i++ )
			{
				int old;
				d.TryGetValue(strings[i], out old);
				d[strings[i]] = old + values[i];
			}

			for (int i = 0; i < strings.Length; i++)
			{
				Assert.AreEqual(values[i], d[strings[i]]);
			}
		}

		[Test]
		public void GetLargestGroups()
		{
			var d = new Dictionary<string, int>();

			string[] strings = "a b c d e".Split(' ');
			int[] values = { 7, 42, 36, 42, 1 };

			for (int i = 0; i < strings.Length; i++)
			{
				int old;
				d.TryGetValue(strings[i], out old);
				d[strings[i]] = old + values[i];
			}

			for (int i = 0; i < strings.Length; i++)
			{
				Assert.AreEqual(values[i], d[strings[i]]);
			}

			var largest = new List<KeyValuePair<string, int>> { new KeyValuePair<string, int>("None", 0) };
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
	}
}
