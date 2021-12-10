using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Word : IWord
    {
        private ICollection<ISymbol> symbols;

        private StringBuilder builder;

        public string Value
        {
            get
            {
                builder = builder ?? new StringBuilder();
                builder.Clear();
                symbols.ToList().ForEach(x => builder.Append(x.Value));

                return builder.ToString();
            }
        }

        public int Length => Value.Length;

        public Word()
        {
            symbols = new List<ISymbol>();
        }

        public void Add(ISymbol symbol)
        {
            symbols.Add(symbol);
        }

        public void Replace(ICollection<ISymbol> symbols)
        {
            this.symbols = symbols ?? throw new ArgumentException(nameof(symbols));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
