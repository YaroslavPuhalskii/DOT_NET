using System.Collections.Generic;

namespace TextParser.Abstractions.Concordance
{
    public interface ILine
    {
        IEnumerable<IToken> Tokens { get; }

        bool Add(IToken token);
    }
}
