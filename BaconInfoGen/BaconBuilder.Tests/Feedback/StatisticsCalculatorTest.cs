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
	}
}
