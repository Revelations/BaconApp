using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BaconBuilder.Model;

namespace BaconBuilder
{
	[TestFixture]
	public class NodeTest
	{
		[Test]
		public void TestStructure()
		{
			HtmlNode<string> root = new HtmlNode<string>("html");
			HtmlNode<string> temp, p;

			temp = root.Children.Add("head");
			temp.Children.Add("link");

			temp = root.Children.Add("body");
			temp.Children.Add("p");

			temp = temp.Children.Add("text");
			temp.Value = "Hello World";

			temp = temp.Parent;

			Console.WriteLine(root.ToString());


		}
	}
}
