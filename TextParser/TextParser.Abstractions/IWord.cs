using System.Collections.Generic;

namespace TextParser.Abstractions
{
    public interface IWord : IToken
    {
        void Replace(ICollection<ISymbol> symbols);
    }
}
