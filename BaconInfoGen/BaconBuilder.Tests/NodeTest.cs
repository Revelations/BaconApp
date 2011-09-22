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
            temp.Children.Add("link").IsEmpty = true;

            temp = root.Children.Add("body");
            p = temp.Children.Add("p").Children.Add("text");
            p.Value = "Hello World";
            p.Children.Add("span");
            temp.Children.Add("img").IsEmpty = true;

            Console.WriteLine(root.ToString());
        }
    }
}
