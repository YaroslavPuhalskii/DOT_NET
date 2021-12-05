using System.Collections.Generic;

namespace TextParser.Abstractions
{
    public interface ISentence
    {
        IEnumerable<IToken> Tokens { get; }

        int Length { get; }

        void Add(IToken token);

        void Remove(IToken token);

        IEnumerable<IWord> GetWords { get; }

        int CountWord { get; }

        string ToString();
    }
}
