using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaconApp
{
	public class HtmlGenerator
	{
		private readonly string _pageName;

		private readonly Node _doc;

		// Try a tree.
		public HtmlGenerator(string pageName)
		{
			_pageName = pageName;

			_doc = new Node(null,
			                new object[]
			                	{
									"<!DOCTYPE HTML>",
			                		new Node("html", new object[]
			                		                 	{
			                		                 		new Node("head",
			                		                 		         new object[]
			                		                 		         	{
			                		                 		         		new Node("title", _pageName)
			                		                 		         	}),
			                		                 		new Node("body")
			                		                 	}
			                			)
			                	});
		}

		public string ToHtml()
		{
			var builder = new StringBuilder();
			foreach (var child in _doc.Children)
			{
				builder.Append(child.ToString());
			}
			return builder.ToString();
		}

		private IEnumerable<Node> ListComp(Func<object, bool> predicate)
		{
			return _doc.Children.Where(predicate).Cast<Node>();
		}

		public Node GetTag(string name)
		{
			return ListComp(
				tag =>
					tag is Node
					&& ((Node)tag).Name.Equals(name)
				).FirstOrDefault();
		}

		public Node GetTag(string name, string parent)
		{
			if (parent == null)
				return GetTag(name);
			return ListComp(
				tag =>
					tag is Node
					&& ((Node) tag).Name.Equals(name)
					&& ((Node) tag).Parent.Name.Equals(parent)
				).FirstOrDefault();
		}

		public void AddContent(string content)
		{
			//var body = GetTag("body", "html");
		}

		public void Display()
		{
			_doc.Display();
			
		}
	}
}
