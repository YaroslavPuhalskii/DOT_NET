using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Punctuation : IPunctuation
    {
        private readonly ICollection<ISymbol> symbols;

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

        public Punctuation()
        {
            symbols = new List<ISymbol>();
        }

        public void Add(ISymbol symbol)
        {
            symbols.Add(symbol);
        }

        public void Remove(ISymbol symbol)
        {
            symbols.Remove(symbol);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
