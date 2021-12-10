using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Symbol : ISymbol
    {
        public char Value { get; }

        public Symbol(char value)
        {
            Value = value;
        }
    }
}
