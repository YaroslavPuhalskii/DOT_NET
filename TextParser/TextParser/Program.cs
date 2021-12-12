using System;
using System.Collections.Generic;
using System.Configuration;
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
            var path = ConfigurationManager.AppSettings.Get("inputFile");

            ITextBuilder textBuilder = new TextBuilder();
            ITextParser textParser = new Core.Parse.TextParser(textBuilder);
            ITextReader reader = new TextReader(textParser);
            
            reader.Read(path);
            
            IText text = textBuilder.GetText;

            Concordance(text);

            Services(text);

            Console.ReadLine();
        }

        private static void Concordance(IText text)
        {
            IPageParser parse = new PageParser(new Paginator(lineLength: 20, pageSize: 3));
            IBook book = parse.Parse(text);

            var concordance = new TextParser.Concordance.Concordance();
            concordance.WriteConcordance(book);

            Console.WriteLine(book.ToString());
        }

        private static void Services(IText text)
        {
            Console.WriteLine(text.ToString());

            var textService = new TextService();

            int i = 0;
            foreach (var item in textService.SortSentencesByWordCount(text))
            {
                Console.WriteLine($"{++i}) {item} - {item.CountWord}");
            }

            i = 0;
            foreach (var item in textService.QuestionSentenceByWordLength(text, 5))
            {
                Console.WriteLine($"{++i}] {item.Value}");
            }

            textService.RemoveWordsFirstConsonantLetter(text, 5);
            Console.WriteLine(text.ToString());

            var substring = new List<ISymbol>
            {
                new Symbol('A'),
                new Symbol('p'),
                new Symbol('p'),
                new Symbol('l'),
                new Symbol('e')
            };

            textService.ReplaceWordByLength(text, 2, 7, substring);

            Console.WriteLine(text.ToString());
        }
    }
}
