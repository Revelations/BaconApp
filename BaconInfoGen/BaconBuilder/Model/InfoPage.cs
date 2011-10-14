using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace BaconBuilder.Model
{
	/// <summary>
	/// 
	/// </summary>
	public class InfoPage
	{
		private string page = "<html><head></head><body></body></html>";
		private Dictionary<string, string> _properties;

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

		public Dictionary<string, string> Props
		{
			get
			{
				if (_properties == null)
				{
					using (var reader = new XmlTextReader(new StringReader(page)))
					{
						reader.DtdProcessing = DtdProcessing.Ignore;
						reader.ReadToFollowing("head");

						_properties = Properties(reader.ReadSubtree());
					}
				}

				return _properties;
			}
		}

		private Dictionary<string, string> Properties(XmlReader reader)
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
			var props = Props;

			props["x"] = X.ToString();
			props["y"] = Y.ToString();

			var builder = new StringBuilder();
			builder.Append("<head>").AppendLine();
			foreach (var p in props)
			{
				builder.AppendFormat("<!-- {0}={1} -->", p.Key, p.Value);
			}
			builder.AppendLine()
				.Append(ConstructStyleSheet()).AppendLine()
				.AppendFormat("<title>{0}</title>",Title).AppendLine()
				.Append("</head>").AppendLine();

			return builder.ToString();
		}

		private string ConstructStyleSheet()
		{
			const string href = "style.css";
			return string.Format(@"<link rel=""stylesheet"" href=""{0}"" />", href);
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
}