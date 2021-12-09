using System;
using System.Collections.Generic;
using TextParser.Abstractions;
using TextParser.Abstractions.Concordance;
using TextParser.Abstractions.Parse;
using TextParser.Concordance.Helper;
using TextParser.Core.Parse;
using TextParser.Core.Parse.Concordance;
using TextParser.Core.Services;
using TextParser.Model;

namespace TextParser
{
    class Program
    {
        static void Main(string[] args)
        {
            ITextReader reader = new TextReader(new TextBuilder());

            IText text = reader.Read();

            IPageParser parse = new PageParser(new Paginator(lineLength: 20, pageSize: 3));
            IBook book = parse.Parse(text);
            var concordance = new TextParser.Concordance.Concordance();
            concordance.WriteConcordance(book);

            Console.WriteLine(book.ToString());

            FirstTaskTest(text);

            Console.ReadLine();
        }


        private static void FirstTaskTest(IText text)
        {
            Console.WriteLine(text.ToString());

            int i = 0;
            foreach (var item in text.SortSentencesByWordCount())
            {
                Console.WriteLine($"{++i}) {item} - {item.CountWord}");
            }

            i = 0;
            foreach (var item in text.Sentences.QuestionSentenceByWordLengthDistinct(5))
            {
                Console.WriteLine($"{++i}] {item.Value}");
            }

            text.RemoveWordsStartWithConsonantByLength(5);
            Console.WriteLine(text.ToString());

            var substring = new List<ISymbol>();
            substring.Add(new Symbol('A'));
            substring.Add(new Symbol('p'));
            substring.Add(new Symbol('p'));
            substring.Add(new Symbol('l'));
            substring.Add(new Symbol('e'));

            text.ReplaceWordByLength(2, 7, substring);

            Console.WriteLine(text.ToString());
        }
    }
}
