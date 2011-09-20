using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BaconBuilder.Model
{
    /// <summary>
    /// Parent class used for parsers of text to html and vice versa.
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// X coordinate of the map position associated with this file.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate of the map position associated with this file.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Coordinates of the map position associated with this file.
        /// </summary>
        public Point Location
        {
            get { return new Point(X, Y); }
        }

        /// <summary>
        /// Set of find / replace rules for parsing text to html and vice versa.
        /// </summary>
        public Dictionary<string, string> RegexDict { get; set; }

        /// <summary>
        /// Does nothing in the parent class. Overridden in child classes to populate the dictionary
        /// with a ruleset appropriate to the type of parsing.
        /// </summary>
        protected virtual void InitialiseDictionary() { }

        /// <summary>
        /// Replaces each matching regex key in the dictionary with it's associated value for a given string.
        /// </summary>
        /// <param name="input">String to perform substitutions on.</param>
        /// <returns>Output, with all regex substitutions completed.</returns>
        public string Parse(string input)
        {
            string output = input;

            foreach (KeyValuePair<string, string> kvp in RegexDict)
                foreach (Match m in Regex.Matches(input, kvp.Key))
                    output = output.Replace(m.Value, kvp.Value);

            return output;
        }

        /// <summary>
        /// Constructor for a parser object. Initialises the find / replace dictionary.
        /// </summary>
        public Parser()
        {
            RegexDict = new Dictionary<string, string>();
        }
    }
}