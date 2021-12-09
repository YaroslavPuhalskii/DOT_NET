using System;
using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Symbol : ISymbol
    {
        private char value;

        public char Value
        {
            get => value;
            set
            {
                this.value = value;
            }
        }

        public Symbol(char value)
        {
            Value = value;
        }
    }
}
