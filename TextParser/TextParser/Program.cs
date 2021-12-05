using System;
using TextParser.Abstractions;
using TextParser.Core.Parse;
using TextParser.Core.Services;

namespace TextParser
{
    class Program
    {
        static void Main(string[] args)
        {
            TextReader reader = new TextReader();

            IText text = reader.Read();

            FirstTaskTest(text);

            Console.ReadLine();
        }


        private static void FirstTaskTest(IText text)
        {
            Console.WriteLine(text.ToString());

            Console.WriteLine("Sentences in ascending order of word count.");
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

            Console.WriteLine("--------------------------------------");

            text.RemoveWordsStartWithConsonantByLength(5);
            Console.WriteLine(text.ToString());

            Console.WriteLine("-----------------------------------------");

            text.ReplaceWordByLength(2, 7, "Apple");
            Console.WriteLine(text.ToString());
        }
    }
}
