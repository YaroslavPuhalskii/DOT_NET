using System;
using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Symbol : ISymbol
    {
        private string value;

        public string Value
        {
            get => value;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }

                this.value = value;
            }
        }

        public int Length { get; set; }

        public Symbol(string value)
        {
            Value = value;
        }
    }
}
