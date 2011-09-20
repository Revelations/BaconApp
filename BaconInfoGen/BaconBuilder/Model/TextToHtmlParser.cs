using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
        /// </summary>
        protected override sealed void InitialiseDictionary()
        {
            // Replace img psuedo tags with html equivalent.
            RegexDict.Add("<\\s*img\\s*>\\s*", "<img src=\"");
            RegexDict.Add("\\s*<\\s*/\\s*img\\s*>", "\" />");

            // Replace audio psuedo tags with html equivalent.
            RegexDict.Add("<\\s*audio\\s*>\\s*", "<audio src=\"");
            RegexDict.Add("\\s*<\\s*/\\s*audio\\s*>", "\" controls=\"controls\" style=\"float:left;\"></audio>");

            // TODO: Complete set of regex rules here.
        }

        /// <summary>
        /// Constructor for this class. Populates the dictionary.
        /// </summary>
        public TextToHtmlParser()
        {
            InitialiseDictionary();
        }
    }
}
