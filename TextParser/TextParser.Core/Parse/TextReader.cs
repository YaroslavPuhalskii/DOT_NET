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
        private ITextBuilder textBuilder;

        private string path = ConfigurationManager.AppSettings.Get("inputFile");

        public TextReader(ITextBuilder textBuilder)
        {
            this.textBuilder = textBuilder;
        }

        public IText Read()
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        IWord word = new Word();

                        line = DeleteSpaces(line);

                        for (int i = 0; i < line.Length; i++)
                        {
                            ISymbol symbol = new Symbol(line[i]);

                            if (textBuilder.IsKeySign(symbol))
                            {
                                IPunctuation punctuation = new Punctuation();
                                punctuation.Add(symbol);

                                punctuation.Value = FindWholeSign(punctuation, line, ref i);

                                textBuilder.Action(word, punctuation);

                                word = new Word();
                                continue;
                            }

                            word.Add(symbol);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("The file doesn't exist!", nameof(ex));
            }

            return textBuilder.GetText;
        }

        private string DeleteSpaces(string line)
        {
            var list = new List<string>();

            foreach (var item in line.Split(' '))
            {
                if (!item.Equals(""))
                {
                    list.Add(item);
                }
            }

            return string.Join(" ", list);
        }

        private string FindWholeSign(IPunctuation punctuation, string line, ref int i)
        {
            string result = punctuation.Value;
            i++;
            for (; i < line.Length; i++)
            {
                ISymbol symbol = new Symbol(line[i]);
                punctuation.Add(symbol);
                if (textBuilder.IsFullKeySign(punctuation))
                {
                    result += line[i];
                    continue;
                }
                else
                {
                    i--;
                    punctuation.Remove(symbol);
                    break;
                }
            }

            return result;
        }
    }
}
