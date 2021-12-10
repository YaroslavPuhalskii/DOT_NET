using System.Collections.Generic;
using System.Linq;
using TextParser.Abstractions;
using TextParser.Core.Factory;

namespace TextParser.Core.Services
{
    public class TextService
    {
        private FactoryLetter factoryLetter;

        public IEnumerable<ISentence> SortSentencesByWordCount(IText text)
        {
            return text.Sentences.OrderBy(x => x.CountWord);
        }

        public IEnumerable<IWord> QuestionSentenceByWordLength(IText text, int length)
        {
            var questionSentences = text.Sentences.Where(x => x.Tokens.Last().Value.Equals("?"));

            IList<IWord> result = new List<IWord>();

            foreach (var item in questionSentences)
            {
                var words = item.GetWords.Where(x => x.Length == length).ToList();

                words.ForEach(x => result.Add(x));
            }

            return result.GroupBy(x => x.Value.ToLower()).Select(x => x.First()).ToList();
        }

        public void RemoveWordsFirstConsonantLetter(IText text, int length)
        {
            factoryLetter = factoryLetter ?? new FactoryLetter();

            foreach (var item in text.Sentences)
            {
                var wordsForDelete = item.GetWords.Where(x => x.Length == length && factoryLetter.IsConsonant(x)).ToList();

                wordsForDelete.ForEach(x => item.Remove(x));
            }
        }

        public void ReplaceWordByLength(IText text, int index, int length, IList<ISymbol> substring)
        {
            var sentence = text[index];

            var wordReplace = sentence.GetWords.Where(x => x.Length == length).ToList();

            wordReplace.ForEach(x => x.Replace(substring));
        }
    }
}
