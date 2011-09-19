using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using mshtml;
using System.IO;
using System.Xml;

namespace BaconBuilder.Model
{
	/// <summary>
	/// An HTML Degenerator
	/// </summary>
	public class HtmlDegen
	{
		IEnumerable<string> contents;

		/// <summary>
		/// Get the contents of the body of the page
		/// </summary>
		public List<string> Body { get; private set; }

		public HtmlDegen()
		{
			Body = new List<string>();
		}

		/// <summary>
		/// Loads the html page.
		/// </summary>
		/// <param name="p"></param>
		public void ReadHtml(string url)
		{
			FileInfo info = new FileInfo(url);
			FileHandler fh = new FileHandler(info.Extension);
			fh.LoadFile(info);
			contents = fh.GetFileFromMemory(info);
			IEnumerator<string> iter = contents.GetEnumerator();


			using (XmlTextReader reader = new XmlTextReader(url))
			{
				reader.DtdProcessing = DtdProcessing.Ignore;
				reader.WhitespaceHandling = WhitespaceHandling.None;

				reader.MoveToContent();
				while (reader.Read())
				{
					reader.MoveToContent();
					if (reader.NodeType == XmlNodeType.Element)
					{
						Console.Write("<" + reader.LocalName);
						if (reader.HasAttributes)
						{
							while (reader.MoveToNextAttribute())
							{
								Console.Write(@" {0}=""{1}""", reader.Name, reader.Value);

								//Console.WriteLine("Name:{0} Type:{1} Value:{2}", reader.Name, reader.NodeType, reader.Value);
							}
							reader.MoveToElement();
						}
						if (reader.IsEmptyElement)
						{
							Console.Write(" /");
						}
						Console.WriteLine(">");
					}
					if (reader.HasValue)
						Console.WriteLine(reader.Value);
					if (reader.NodeType == XmlNodeType.EndElement)
						Console.WriteLine("</" + reader.LocalName + ">");

					//Console.WriteLine(reader.NodeType + " " + ((reader.NodeType == XmlNodeType.Text) ? ("= " + reader.Value) : ": "+reader.LocalName));
				}
			}
			HtmlNode<string> node = null;
			using (XmlTextReader reader = new XmlTextReader(url))
			{
				reader.DtdProcessing = DtdProcessing.Ignore;
				reader.WhitespaceHandling = WhitespaceHandling.None;

				reader.MoveToContent();
				XmlReader subTree = reader.ReadSubtree();
				node = ParseXML(subTree);
			}
			Console.WriteLine(node);
		}

		private HtmlNode<string> ParseXML(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					if (reader.LocalName == "body")
					{
						return ParseBody(reader);
					}

					if (reader.LocalName == "p")
					{
						return ParsePar(reader);
					}
				}
			}
			return null;
		}

		private HtmlNode<string> ParsePar(XmlReader reader)
		{
			reader.Read();
			var myString = reader.Value as string;
			HtmlNode<string> node = new HtmlNode<string>("p");
			node.Children.Add(myString);
			return node;
		}

		private HtmlNode<string> ParseBody(XmlReader reader)
		{
			HtmlNode<string> node = new HtmlNode<string>(reader.LocalName);

			using (XmlReader subReader = reader.ReadSubtree())
			{
				while (subReader.Read())
				{
					HtmlNode<string> subControl = ParseXML(subReader);
					if (subControl != null)
					{
						node.Children.Add(subControl.Name);
					};
				}
			}
			return node;
		}

		/// <summary>
		/// Decomposes the HTML page into extractable sections
		/// </summary>
		/// <param name="contents"></param>
		public void Decompose(IEnumerable<string> contents)
		{

			IEnumerator<string> iter = contents.GetEnumerator();
			while (iter.MoveNext())
			{
				if (iter.Current.Contains("<body>"))
				{
					int i = iter.Current.IndexOf("<body>") + "<body>".Length;
					Body.Add(iter.Current.Substring(i));
					break;
				}
			}

			while (iter.MoveNext())
			{
				if (iter.Current.Contains("</body>"))
				{
					int i = iter.Current.IndexOf("</body>");
					Body.Add(iter.Current.Substring(0, i));
					break;
				}
				else
				{
					Body.Add(iter.Current);
				}
			}
		}
	}
}
