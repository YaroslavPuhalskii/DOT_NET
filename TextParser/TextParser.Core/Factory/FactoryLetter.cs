using System;
using System.Linq;
using TextParser.Abstractions;

namespace TextParser.Core.Factory
{
    public class FactoryLetter
    {
        private string[] _endLetters { get; } = { "!", ".", "?", "..", "...", "?!", "!?" };

        private string[] _separativeSymbol { get; } = { "<", "(", "[", "{", "„", "«", "‘", ")", ">", "]", "}", "“", "»", "’", ",", ";", ":" };

        private string[] _conconantLetter { get; } = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };

        private string _space = " ";

        public bool IsSpace(IPunctuation punctuation)
        {
            if (punctuation == null || punctuation.Value == null)
            {
                throw new ArgumentNullException();
            }

            return _space.Contains(punctuation.Value);
        }

        public bool IsPunctuation(IPunctuation punctuation)
        {
            if (punctuation == null || punctuation.Value == null)
            {
                throw new ArgumentNullException();
            }

            return _separativeSymbol.Contains(punctuation.Value);
        }

        public bool IsEnd(IPunctuation punctuation)
        {
            if (punctuation == null || punctuation.Value == null)
            {
                throw new ArgumentNullException();
            }

            return _endLetters.Contains(punctuation.Value);
        }

        public bool IsConsonant(IWord item)
        {
            if (item == null || item.Value == null)
            {
                throw new ArgumentNullException();
            }

            return _conconantLetter.Contains(item.Value[0].ToString().ToLower());
        }

        public bool IsEnd(ISymbol symbol)
        {
            if (symbol == null)
            {
                throw new ArgumentNullException();
            }

            return _endLetters.Contains(symbol.Value.ToString());
        }

        public bool IsPunctuation(ISymbol symbol)
        {
            if (symbol == null)
            {
                throw new ArgumentNullException();
            }

            return _separativeSymbol.Contains(symbol.Value.ToString());
        }
    }
}
