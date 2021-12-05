using System.Collections.Generic;
using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Punctuation : IPunctuation
    {
        private ICollection<ISymbol> symbols;

        public string Value { get; set; }

        public int Length => Value.Length;

        public IEnumerable<ISymbol> Symbols => symbols;

        public Punctuation()
        {
            symbols = new List<ISymbol>();
        }

        public void Add(ISymbol symbol)
        {
            symbols.Add(symbol);
            Value += symbol.Value;
        }

        public void Remove(ISymbol symbol)
        {
            symbols.Remove(symbol);
        }
    }
}
