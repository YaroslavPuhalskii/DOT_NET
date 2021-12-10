using System.Collections.Generic;

namespace TextParser.Abstractions
{
    public interface IToken
    {
        string Value { get; }

        int Length { get; }

        void Add(ISymbol symbol);
    }
}
