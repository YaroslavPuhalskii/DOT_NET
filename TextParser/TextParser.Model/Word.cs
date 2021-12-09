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

        private StringBuilder builder = new StringBuilder();

        private string value;

        public IEnumerable<ISymbol> Symbols => symbols;

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
            if (symbols == null)
            {
                throw new ArgumentException(nameof(symbols));
            }

            this.symbols = symbols;
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

        public override string ToString()
        {
            return value;
        }
    }
}
