using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using TextParser.Abstractions;
using TextParser.Model;

namespace TextParser.Core.Parse
{
    public class TextReader
    {
        private TextBuilder textBuilder;

        private string path = ConfigurationManager.AppSettings.Get("inputFile");

        public TextReader()
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException();
            }

            textBuilder = new TextBuilder();
        }

        public IText Read()
        {
            using (var reader = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    IWord word = new Word();

                    line = DeleteSpaces(line);

                    for (int i = 0; i < line.Length; i++)
                    {
                        ISymbol symbol = new Symbol(line[i].ToString());

                        if (!char.IsLetter(Convert.ToChar(symbol.Value)))
                        {
                            IPunctuation punctuation = new Punctuation();
                            punctuation.Add(symbol);

                            if (textBuilder.IsKeySign(punctuation))
                            {
                                punctuation.Value = FindWholeSign(punctuation, line, ref i);
                            }

                            textBuilder.Action(word, punctuation);

                            word = new Word();
                            continue;
                        }

                        word.Add(symbol);
                    }
                }
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

        public string FindWholeSign(IPunctuation punctuation, string line, ref int i)
        {
            string result = punctuation.Value;
            i++;
            for (; i < line.Length; i++)
            {
                ISymbol symbol = new Symbol(line[i].ToString());
                punctuation.Add(symbol);
                if (textBuilder.IsKeySign(punctuation))
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
