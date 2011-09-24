
using System.Drawing;
using System.Text;

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

        /// <summary>
        /// TODO: Jordan is working on this.
        /// </summary>
        /// <param name="p"></param>
        public void InsertXY(Point p)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<!-- x = {0} -->", p.X));
            builder.Append(string.Format("<!-- y = {0} -->", p.Y));
        }
    }
}
