using System.Collections.Generic;

namespace TextParser.Abstractions
{
    public interface IToken
    {
        IEnumerable<ISymbol> Symbols { get; }

        string Value { get; set; }

        int Length { get; }

        void Add(ISymbol symbol);
    }
}
