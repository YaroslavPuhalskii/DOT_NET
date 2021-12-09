using System.Collections.Generic;
using System.Linq;
using TextParser.Abstractions;
using TextParser.Core.Factory;

namespace TextParser.Core.Services
{
    public static class TextService
    {
        public static IEnumerable<ISentence> SortSentencesByWordCount(this IText text)
        {
            return text.Sentences.OrderBy(x => x.CountWord);
        }

        public static void RemoveWordsStartWithConsonantByLength(this IText text, int length)
        {
            var factoryLetter = new FactoryLetter();

            foreach (var item in text.Sentences)
            {
                var wordsForDelete = item.GetWords.Where(x => x.Length == length && factoryLetter.IsConsonant(x)).ToList();

                wordsForDelete.ForEach(x => item.Remove(x));
            }
        }

        public static void ReplaceWordByLength(this IText text, int index, int length, IList<ISymbol> substring)
        {
            var sentence = text[index];

            var wordReplace = sentence.GetWords.Where(x => x.Length == length).ToList();

            wordReplace.ForEach(x => x.Replace(substring));
        }
    }
}
