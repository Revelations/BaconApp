using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BaconBuilder.Model
{
    public class TextToHtmlParser : Parser
    {
        protected override sealed void InitialiseDictionary()
        {
            RegexDict.Add("<\\s*img\\s*=\\s*", "<img src='");
            RegexDict.Add("\\s*\\s*>", "' />");

            // TODO: Complete set of regex rules here.
        }

        public TextToHtmlParser()
        {
            InitialiseDictionary();
        }
    }
}
