using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BaconBuilder.Model
{
    public class HtmlToTextParser : Parser
    {
        protected override sealed void InitialiseDictionary()
        {
            RegexDict.Add("<\\s*img\\s*src\\s*=\\s*'", "<img = ");
            RegexDict.Add("'\\s*/\\s*>", ">");

            // TODO: Complete set of regex rules here.
        }

        public HtmlToTextParser()
        {
            InitialiseDictionary();
        }
    }
}
