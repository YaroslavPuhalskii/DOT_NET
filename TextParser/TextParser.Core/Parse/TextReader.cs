using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using TextParser.Abstractions;
using TextParser.Abstractions.Parse;
using TextParser.Model;

namespace TextParser.Core.Parse
{
    public class TextReader : ITextReader
    {
        private readonly ITextParser textParser;

        public TextReader(ITextParser textParser)
        {
            this.textParser = textParser;
        }

        public void Read(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        textParser.Parse(line);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("The file doesn't exist!", nameof(ex));
            }
        }
    }
}
