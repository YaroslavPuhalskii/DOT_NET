using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Punctuation : IPunctuation
    {
        private ICollection<ISymbol> symbols;

        private StringBuilder builder = new StringBuilder();

        private string value;

        public string Value
        {
            get
            {
                builder.Clear();
                symbols.ToList().ForEach(x => builder.Append(x.Value));

                return builder.ToString();
            }
            set
            {
                this.value = value;
            }
        }

        public int Length => Value.Length;

        public IEnumerable<ISymbol> Symbols => symbols;

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
            return value;
        }
    }
}
