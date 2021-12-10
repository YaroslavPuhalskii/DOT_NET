using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Sentence : ISentence
    {
        private readonly ICollection<IToken> tokens;

        private StringBuilder builder;

        public IEnumerable<IToken> Tokens => tokens;

        public Sentence()
        {
            tokens = new List<IToken>();
        }

        public void Add(IToken token)
        {
            tokens.Add(token);
        }

        public void Remove(IToken token)
        {
            tokens.Remove(token);
        }

        public IEnumerable<IWord> GetWords => tokens.OfType<IWord>();

        public int CountWord => GetWords.Count();

        public override string ToString()
        {
            builder = builder ?? new StringBuilder();
            builder.Clear();
            tokens.ToList().ForEach(x => builder.Append(x.Value));

            return builder.ToString();
        }
    }
}
