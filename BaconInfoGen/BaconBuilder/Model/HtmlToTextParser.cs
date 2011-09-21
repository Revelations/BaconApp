using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BaconBuilder.Model
{
    /// <summary>
    /// Class used to convert html content to a plain text string.
    /// </summary>
    public class HtmlToTextParser : Parser
    {
        /// <summary>
        /// Populates the dictionary with a set of find/replace key value pairs appropriate
        /// for converting html tags to plain text content and 'bacon psuedo tags'.
        /// 
        /// Rules are parsed in the order they are added here.
        /// </summary>
        protected override sealed void InitialiseDictionary()
        {
            // Replace html img tag with pseudo tag equivalent.
            RegexDict.Add("<\\s*img\\s*src\\s*=\\s*\"", "<img>");
            RegexDict.Add(".bmp\"\\s*/\\s*>", ".bmp</img>");
            RegexDict.Add(".jpg\"\\s*/\\s*>", ".jpg</img>");
            RegexDict.Add(".jpeg\"\\s*/\\s*>", ".jpeg</img>");
            RegexDict.Add(".gif\"\\s*/\\s*>", ".gif</img>");
            RegexDict.Add(".png\"\\s*/\\s*>", ".png</img>");

            // Replace html audio tag with pseudo tag equivalent.
            RegexDict.Add("<\\s*audio\\s*src\\s*=\\s*\"", "<audio>");
            RegexDict.Add("\" controls=\"controls\" style=\"float:left;\"></audio>", "</audio>");

            // TODO: Complete set of regex rules here.
        }

        /// <summary>
        /// Constructor for this class. Populates the dictionary.
        /// </summary>
        public HtmlToTextParser()
        {
            InitialiseDictionary();
        }
    }
}
