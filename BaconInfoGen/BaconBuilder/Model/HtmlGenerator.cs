using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace BaconBuilder.Model
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
		    var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter, ""))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Html); // <html>
                writer.RenderBeginTag(HtmlTextWriterTag.Head); // <head>

                writer.AddAttribute(HtmlTextWriterAttribute.Href, "style.css"); // Adds attribute to link
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
                writer.RenderBeginTag(HtmlTextWriterTag.Link); // <link>
                writer.RenderEndTag(); // </link>

				writer.RenderBeginTag(HtmlTextWriterTag.Title);
				writer.Write(_pageName);
				writer.RenderEndTag(); // </title>
                
                writer.RenderEndTag(); // </head>
                writer.RenderBeginTag(HtmlTextWriterTag.Body); // <body>

                writer.RenderEndTag(); // </body>
                writer.RenderEndTag(); // </html>
            }

		    return "<!DOCTYPE HTML>" + stringWriter.ToString().Replace(System.Environment.NewLine, "");

//		    var builder = new StringBuilder();
//			foreach (var child in _doc.Children)
//			{
//				builder.Append(child.ToString());
//			}
//			return builder.ToString();
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
