﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Eryan.Wrappers
{
    /// <summary>
    /// Wraps a target entry
    /// </summary>
    public class TargetEntry : InterfaceEntry
    {
        List<string> sections;
        Regex tokenizer;


        /// <summary>
        /// Takes an unparsed target entry and tokenize its elements
        /// </summary>
        /// <param name="unparsedEntry">Tbe entry</param>
        /// <param name="absoluteTop">Top y value of the entry icon</param>
        /// <param name="absoluteLeft">Leftmost x value of the entry icon</param>
        /// <param name="height">Height of the entry icon</param>
        /// <param name="width">Width of the entry icon</param>
        public TargetEntry(string unparsedEntry, int absoluteTop, int absoluteLeft, int height, int width)
        {
            sections = new List<string>();
            this.x = absoluteLeft;
            this.y = absoluteTop;
            this.height = height;
            this.width = width;
            parseEntry(unparsedEntry);
        }

        /// <summary>
        /// Tokenize the entry into sections
        /// </summary>
        /// <param name="unparsedEntry">The unparsed Target entry from the client</param>
        public void parseEntry(string unparsedEntry)
        {
            //tokenizer = new Regex(@"<t>");
            //string[] splitString = tokenizer.Split(unparsedEntry);
            //tokenizer = new Regex("<right>");


            //splitString[1] = tokenizer.Split(splitString[1])[1];


            Console.WriteLine(unparsedEntry);


            /*
            foreach (string split in splitString)
            {
                if (split.Equals(""))
                    continue;
                Console.WriteLine(split);
                sections.Add(split);
            }
           */


        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(300);
            foreach (string section in sections)
            {
                sb.Append(section);
            }
            return sb.ToString();
        }
    }     
}
