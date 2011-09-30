using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace BaconBuilder
{
	public class Reader
	{
		private XmlNode node;
		private readonly FileInfo _page =
			new FileInfo(@"C:\Users\sk218\BaconApp\BaconInfoGen\BaconBuilder\Resources\test.html");

		public string GetProperties(string property)
		{
			using (var reader = new XmlTextReader(_page.FullName))
			{
				reader.DtdProcessing = DtdProcessing.Ignore;
				//reader.WhitespaceHandling = WhitespaceHandling.None;

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
			using (var reader = new XmlTextReader(_page.FullName))
			{
				reader.DtdProcessing = DtdProcessing.Ignore;
				reader.WhitespaceHandling = WhitespaceHandling.None;

				reader.MoveToContent();
				reader.ReadToFollowing("body");
				var builder = new StringBuilder();
				foreach (var line in Subtree(reader.ReadSubtree()))
				{
					builder.Append(line);
				}
				return builder.ToString();
			}
		}

		/*
		 * //We need recursive search down the tree
		 * public List<string> Recursive(List<string> list) {}
		 */

		public List<string> Subtree(XmlReader reader)
		{
			var result = new List<string>();
			while (reader.Read())
			{
				//if (reader.Name.Length > 0 || reader.HasValue) result.Add(string.Format("<{0}>{1}</{0}>", reader.Name, reader.Value));
				result.Add(reader.ReadInnerXml());
			}
			return result;
		}

		public List<string> GetBodyContents()
		{
			using (var reader = new XmlTextReader(_page.FullName))
			{
				reader.DtdProcessing = DtdProcessing.Ignore;
				reader.WhitespaceHandling = WhitespaceHandling.None;

				reader.MoveToContent();
				reader.ReadToFollowing("body");
				return Subtree(reader.ReadSubtree());
			}
		}
	}
}