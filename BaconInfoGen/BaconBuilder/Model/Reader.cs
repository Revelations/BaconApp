using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace BaconBuilder.Model
{
	/// <summary>
	/// 
	/// </summary>
	public class InfoPage
	{
		Reader _reader = new Reader();
		private string page = "<html><head></head><body></body></html>";

		public int X { get; set; }
		public int Y { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }

		public string SourceCode
		{
			get { return page; }
		}

		public void ConstructFullPage(int x, int y, string title, string body)
		{
			X = x;
			Y = y;
			Title = title;
			Body = body;
			ConstructFullPage();
		}

		public void ConstructFullPage()
		{
			page = new StringBuilder("<!DOCTYPE HTML>")
				.AppendLine()
				.Append("<html>")
				.AppendLine()
				.Append(ConstructHead())
				.AppendLine()
				.Append(ConstructBody())
				.AppendLine()
				.Append("</html>")
				.AppendLine()
				.ToString();
		}

		public Dictionary<string, string> Properties(XmlReader reader)
		{
			var result = new Dictionary<string, string>();
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Comment && reader.HasValue)
				{
					string[] data = reader.Value.Split('=');
					if (data.Length == 2)
					{
						result.Add(data[0].Trim(), data[1].Trim());
					}
				}
			}
			return result;
		}

		public string ConstructHead()
		{
			Dictionary<string, string> props;
			using (var reader = new XmlTextReader(new StringReader(page)))
			{
				reader.DtdProcessing = DtdProcessing.Ignore;
				reader.ReadToFollowing("head");

				props = Properties(reader.ReadSubtree());
			}

			props["x"] = X.ToString();
			props["y"] = Y.ToString();

			var builder = new StringBuilder();
			builder.Append("<head>").AppendLine();
			foreach (var p in props)
			{
				builder.AppendFormat("<!-- {0}={1} -->", p.Key, p.Value);
			}
			builder.AppendLine().AppendFormat("<title>{0}</title>",Title).AppendLine()
			.Append("</head>").AppendLine();

			return builder.ToString();
		}

		private string ConstructStyleSheet()
		{
			string href = "style.css";
			return string.Format(@"<style link=""{0}"" >", href);
		}

		public string ConstructBody()
		{
			using (var reader = new XmlTextReader(new StringReader(page)))
			{
				reader.DtdProcessing = DtdProcessing.Ignore;
				reader.ReadToFollowing("body");

				return reader.ReadOuterXml();
			}
		}
	}

	public class Reader
	{
		public string Body { get; set; }
		public FileInfo Info { get; set; }
		private Dictionary<string, string> _props;

		public Dictionary<string, string> Properties
		{
			get
			{
				if (_props == null)
				{
					using (var reader = new XmlTextReader(new StringReader(Body)))
					{
						reader.DtdProcessing = DtdProcessing.Ignore;
						reader.ReadToFollowing("head");

						_props = HeadProperties(reader.ReadSubtree());
					}
				}
				return _props;
			}
		}

		public Dictionary<string, string> HeadProperties(XmlReader reader)
		{
			var result = new Dictionary<string, string>();
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Comment && reader.HasValue)
				{
					string[] data = reader.Value.Split('=');
					if (data.Length == 2)
					{
						result.Add(data[0].Trim(), data[1].Trim());
					}
				}
			}
			return result;
		}

		public void SetProperties(string property, string value)
		{
			
			Properties[property] = value;

			UpdateDocument();

			#region Junk

			// http://stackoverflow.com/questions/4971440/how-to-parse-html-to-modify-all-words
			/*
			
			XmlTextReader r = new XmlTextReader(Info.FullName);
			r.DtdProcessing = DtdProcessing.Ignore;
			r.MoveToContent();
			XmlReader nodeReader = XmlReader.Create(new StringReader(r.ReadOuterXml()));
			XDocument xRoot = XDocument.Load(nodeReader, LoadOptions.SetLineInfo);
			foreach (XElement e in xRoot.Elements("<html>").DescendantsAndSelf())
				Console.WriteLine("{0}{1}{2}",
				    ("".PadRight(e.Ancestors().Count() * 2) + e.Name).PadRight(20),
				    ((IXmlLineInfo)e).LineNumber.ToString().PadRight(5),
				    ((IXmlLineInfo)e).LinePosition);

			StringBuilder builder = new StringBuilder();
			using (var reader = new XmlTextReader(Info.FullName))
			{
				reader.DtdProcessing = DtdProcessing.Ignore;

				//reader.MoveToContent();
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Comment)
					{
						if (reader.Value.Split('=')[0].Trim().ToLower().Equals(property.ToLower()))
							return reader.Value.Trim();
					} else
					{
						builder.Append()
					}
				}
			}
			return null;
			
			*/

			#endregion
		}

		public void UpdateDocument()
		{
			var builder = new StringBuilder();

			builder.Append("<!DOCTYPE HTML><html><head>");
			foreach (var p in Properties)
			{
				builder.AppendFormat("<!-- {0}={1} -->", p.Key, p.Value);
			}
			builder.Append("<title></title></head><body></body></html>");

			Body = builder.ToString();
		}


		public string GetBody()
		{
			using (var reader = new XmlTextReader(new StringReader(Body)))
			{
				reader.DtdProcessing = DtdProcessing.Ignore;
				reader.WhitespaceHandling = WhitespaceHandling.None;

				reader.ReadToFollowing("body");
				var builder = new StringBuilder();
				var sub = reader.ReadSubtree();
				while (sub.Read())
				{
					builder.Append(sub.ReadInnerXml());
				}
				return builder.ToString();
			}
		}
		public List<object> GetBodyContents()
		{
			var list = new List<object>();

			var d = new XmlDocument();
			d.Load(Body);
			var body = d.GetElementsByTagName("body");
			Recursive(body, list, 0);

			return list;
		}

		private static void Recursive(IEnumerable d, ICollection<object> list, int level)
		{
			foreach (XmlNode node in d)
			{
				Console.WriteLine("".PadRight(level, ' ') + @"<{0}>", node.LocalName);
				switch (node.NodeType)
				{
					case XmlNodeType.Element:
						if (node.LocalName.Equals("img"))
						{
							list.Add("<" + node.LocalName + ">");
						}
						break;
					case XmlNodeType.Text:

						list.Add(node.Value);
						break;
				}
				if (node.HasChildNodes)
				{
					Recursive(node.ChildNodes, list, level + 1);
					//list.Add(node.LocalName);
				}
				//list.Add("</" + node.LocalName + ">");
			}
		}

	}
}