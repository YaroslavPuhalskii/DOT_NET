using System.Collections.Generic;
using TextParser.Abstractions;
using TextParser.Abstractions.Parse;
using TextParser.Model;

namespace TextParser.Core.Parse
{
    public class TextParser : ITextParser
    {
        private readonly ITextBuilder textBuilder;

        public TextParser(ITextBuilder textBuilder)
        {
            this.textBuilder = textBuilder;
        }

        public void Parse(string line)
        {
            line = DeleteSpaces(line);

            IWord word = new Word();

            for (int i = 0; i < line.Length; i++)
            {
                ISymbol symbol = new Symbol(line[i]);

                if (textBuilder.IsKeySign(symbol))
                {
                    IPunctuation punctuation = new Punctuation();
                    punctuation.Add(symbol);

                    FindWholeSign(punctuation, line, ref i);

                    textBuilder.Action(word, punctuation);

                    word = new Word();
                    continue;
                }

                word.Add(symbol);
            }
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

        private void FindWholeSign(IPunctuation punctuation, string line, ref int i)
        {
            i++;
            for (; i < line.Length; i++)
            {
                ISymbol symbol = new Symbol(line[i]);
                punctuation.Add(symbol);
                if (textBuilder.IsFullKeySign(punctuation))
                {
                    continue;
                }

                i--;
                punctuation.Remove(symbol);
                break;
            }
        }
    }
}
