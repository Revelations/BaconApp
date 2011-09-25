using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

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
			// TODO: Complete set of regex rules here.
			RegexDict.Add(@"\s*<!DOCTYPE\s+[hH][tT][mM][lL]>\s*", "");
			RegexDict.Add(@"\s*<html>\s*", "");
			RegexDict.Add(@"\s*<head>\s*", "");
			RegexDict.Add(@"\s*<link\s+href=""style.css""\s+rel=""stylesheet""\s+/>\s*", "");
			RegexDict.Add(@"\s*<title></title>\s*", "");
			RegexDict.Add(@"\s*<!--\s*[xX]\s*=\s*(?:[0-9]+)\s*-->\s*", "");
			RegexDict.Add(@"\s*<!--\s*[yY]\s*=\s*(?:[0-9]+)\s*-->\s*", "");
			RegexDict.Add(@"\s*</head>\s*", "");
			
			RegexDict.Add(@"\s*</*body>\s*", "");
			// Replace </p><p> with \n\n
			RegexDict.Add(@"</p>\s*<p>", @"\n\n");
			// Remove lone <p> and </p> tags
			RegexDict.Add(@"(?!</p>)\s*<p>", "");
			RegexDict.Add(@"</p>\s*(?!<p>)", "");
			
			// Replace html image tag with pseudo tag equivalent.
			RegexDict.Add(@"<\s*img\s+src\s*=\s*""([^""\s]*)""\s*/>", "<img>$1</img>");

			// Replace html audio tag with pseudo tag equivalent. NEEDS A BIT MORE WORK. SEEMS TO WORK!
			RegexDict.Add(@"<audio (?:.*\s+)*src=""([^""]*)""(?:\s+.*\s*)*>[^<]*</audio>", "<audio>$1</audio>");
			//RegexDict.Add(@"<\s*audio\s+(?:[\w]+=""[^""]*""\s+)*src\s*=\s*""([^""\s]*)"">[^<]*<\s*/\s*audio\s*>", "<audio>$1</audio>");
			RegexDict.Add(@"</html>\s*", "");			
		}

		/// <summary>
		/// Constructor for this class. Populates the dictionary.
		/// </summary>
		public HtmlToTextParser()
		{
			InitialiseDictionary();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public Point ExtractXY(string input)
		{
			Match m = Regex.Match(input, @"<!--\s*[xX]\s*=\s*([0-9]+)\s*-->");

			int x = Convert.ToInt32(m.Groups[1].Value);

			m = Regex.Match(input, @"<!--\s*[yY]\s*=\s*([0-9]+)\s*-->");
			int y = Convert.ToInt32(m.Groups[1].Value);

			return new Point(x, y);
		}
	}
}
