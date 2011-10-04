using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace BaconBuilder.Model
{
	public class Reader
	{
		public FileInfo Page { get; set; }

		public string GetProperties(string property)
		{
			using (var reader = new XmlTextReader(Page.FullName))
			{
				reader.DtdProcessing = DtdProcessing.Ignore;

				reader.MoveToContent();
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Comment)
					{
						if (reader.Value.Split('=')[0].Trim().ToLower().Equals(property.ToLower()))
							return reader.Value.Trim();
					}
				}
			}
			return null;
		}

		public string GetBody()
		{
			using (var reader = new XmlTextReader(Page.FullName))
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

		/*
		 * We need recursive search down the tree
		 */
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

		public List<object> GetBodyContents()
		{
			var list = new List<object>();
			var d = new XmlDocument();
			d.Load(Page.FullName);
			var body = d.GetElementsByTagName("body");
			Recursive(body, list, 0);

			return list;
		}
	}
}