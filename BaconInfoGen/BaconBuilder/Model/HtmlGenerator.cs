using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml;

namespace BaconBuilder.Model
{
	public class HtmlGenerator
	{
		private readonly string _pageName;
		List<string> body;

		// Try a tree.
		public HtmlGenerator(string pageName)
		{
			_pageName = pageName;
			body = new List<string>();
		}

		public string ToHtml()
		{
		    var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter, string.Empty))
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

			return @"<!DOCTYPE HTML>
" + stringWriter.ToString();//.Replace(System.Environment.NewLine, string.Empty);
		}

		public void AddContent(string content)
		{
			body.Add(content);
		}

		public object Body()
		{
			return HtmlTextWriterTag.Body.ToString();
		}
	}
}
