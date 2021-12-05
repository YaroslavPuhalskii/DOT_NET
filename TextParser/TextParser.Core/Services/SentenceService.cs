using System.Collections.Generic;
using System.Linq;
using TextParser.Abstractions;

namespace TextParser.Core.Services
{
    public static class SentenceService
    {
        public static IEnumerable<IWord> QuestionSentenceByWordLengthDistinct(this IEnumerable<ISentence> tokens, int length)
        {
            var questioneSentences = tokens.Where(x => x.Tokens.Last().Value.Equals("?"));

            var result = new HashSet<IWord>();

            foreach (var item in questioneSentences)
            {
                var words = item.GetWords.Where(x => x.Length == length).ToList();

                words.ForEach(x => result.Add(x));
            }

            return result.GroupBy(x => x.Value).Select(x => x.First()).ToList();
        }
    }
}
