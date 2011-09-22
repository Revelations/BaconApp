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
            RegexDict.Add(@"</p>\s*<p>", "\n\n");

            // <body><p> should match, </p><p> shouldn't match
            RegexDict.Add(@"(?!</p>)\s*<p>", "");

            //// Replace html image tag with pseudo tag equivalent.
            RegexDict.Add(@"<\s*img\s+src\s*=\s*""([^""\s]*)""\s*/>", "<img>$1</img>");

            // Replace html audio tag with pseudo tag equivalent. NEEDS A BIT MORE WORK. SEEMS TO WORK!
            RegexDict.Add(@"<audio (?:.*\s+)*src=""([^""]*)""(?:\s+.*\s*)*>[^<]*</audio>", "<audio>$1</audio>");
            //RegexDict.Add(@"<\s*audio\s+(?:[\w]+=""[^""]*""\s+)*src\s*=\s*""([^""\s]*)"">[^<]*<\s*/\s*audio\s*>", "<audio>$1</audio>");

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
