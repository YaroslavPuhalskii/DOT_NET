using System;
using System.Linq;
using TextParser.Abstractions;

namespace TextParser.Core.Factory
{
    public class FactoryLetter
    {
        private static string[] EndLetters { get; } = { "!", ".", "?", "..", "...", "?!", "!?" };

        private static string[] SeparativeSymbol { get; } = { "<", "(", "[", "{", "„", "«", "‘", ")", ">", "]", "}", "“", "»", "’", ",", ";", ":" };

        private static string[] ConconantLetter { get; } = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };

        private readonly string space = " ";

        public bool IsSpace(IPunctuation punctuation)
        {
            if (punctuation?.Value == null)
            {
                throw new ArgumentNullException();
            }

            return space.Contains(punctuation.Value);
        }

        public bool IsPunctuation(IPunctuation punctuation)
        {
            if (punctuation?.Value == null)
            {
                throw new ArgumentNullException();
            }

            return SeparativeSymbol.Contains(punctuation.Value);
        }

        public bool IsPunctuation(ISymbol symbol)
        {
            if (symbol == null)
            {
                throw new ArgumentNullException();
            }

            return SeparativeSymbol.Contains(symbol.Value.ToString());
        }

        public bool IsEnd(IPunctuation punctuation)
        {
            if (punctuation?.Value == null)
            {
                throw new ArgumentNullException();
            }

            return EndLetters.Contains(punctuation.Value);
        }

        public bool IsEnd(ISymbol symbol)
        {
            if (symbol == null)
            {
                throw new ArgumentNullException();
            }

            return EndLetters.Contains(symbol.Value.ToString());
        }

        public bool IsConsonant(IWord word)
        {
            if (word?.Value == null)
            {
                throw new ArgumentNullException();
            }

            return ConconantLetter.Contains(word.Value[0].ToString().ToLower());
        }
    }
}
