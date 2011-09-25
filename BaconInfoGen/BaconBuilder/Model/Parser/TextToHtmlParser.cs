
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BaconBuilder.Properties;

namespace BaconBuilder.Model
{
	/// <summary>
	/// Class used to convert a plain text string into html content.
	/// </summary>
	public class TextToHtmlParser : Parser
	{
		/// <summary>
		/// Populates the dictionary with a set of find/replace key value pairs appropriate
		/// for converting plain text content and 'bacon psuedo tags' to html content.
		/// 
		/// Rules are parsed in the order they are added here.
		/// </summary>
		protected override sealed void InitialiseDictionary()
		{
			// Replace image pseudo tag with html equivalent.
			RegexDict.Add(@"<\s*img\s*>\s*([^""\s]*)\s*<\s*/\s*img\s*>", @"<img src=""$1"" />");

			// Replace audio pseudo tag with html equivalent.
			RegexDict.Add(@"<\s*audio\s*>\s*([^""\s]*)\s*<\s*/\s*audio\s*>", @"<audio src=""$1"" controls=""controls"" style=""float:left;""></audio>");

			// TODO: Complete set of regex rules here.

			// TODO: \n\n ==> </p><p>
			// TODO: First paragraph starts with <p>, last ends with </p>
			// TODO: Begin document with <!DOCTYPE HTML><html>......<body>
			// TODO: End document with </body></html>
		}

        /// <summary>
        /// Constructor for this class. Populates the dictionary.
        /// </summary>
        public TextToHtmlParser()
        {
            InitialiseDictionary();
        }

        public string Parse2(string input)
        {
            string doc = Resources.Blank;

            doc = InsertXY(Location, doc);

            return base.Parse(doc);
        }

	    /// <summary>
	    /// TODO: Jordan is working on this.
	    /// </summary>
	    /// <param name="p"></param>
	    /// <param name="input"></param>
	    public string InsertXY(Point p, string input)
        {
            StringBuilder builder = new StringBuilder();

            int i = input.IndexOf("</title>") + "</title>".Length;

            builder.Append(input.Substring(0, i));
            //builder.AppendLine();
            builder.Append(string.Format(@"<!-- x = {0} -->", p.X));
            //builder.AppendLine();
            builder.Append(string.Format(@"<!-- y = {0} -->", p.Y));
            //builder.AppendLine();

            builder.Append(input.Substring(i, input.Length - i));

            return builder.ToString();
        }
    }
}
