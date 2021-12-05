using System;
using System.Collections.Generic;
using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Word : IWord
    {
        private ICollection<ISymbol> symbols;

        public IEnumerable<ISymbol> Symbols => symbols;

        public string Value { get; set; }

        public int Length => Value.Length;

        public Word()
        {
            symbols = new List<ISymbol>();
        }

        public void Add(ISymbol symbol)
        {
            symbols.Add(symbol);
            Value += symbol.Value;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as IWord);
        }

        private bool Equals(IWord that)
        {
            if (that == null)
                return false;

            return object.Equals(this.Value.ToLower(), that.Value.ToLower());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Value.ToLower());
        }
    }
}
