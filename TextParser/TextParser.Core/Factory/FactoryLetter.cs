using System.Linq;
using TextParser.Abstractions;

namespace TextParser.Core.Factory
{
    public class FactoryLetter
    {
        private string[] _endLetters { get; } = { "!", "! ", ".", ". ", "..", "?", "? ", "...", "... ", "?!", "!?", "?! ", "!? "};

        private string[] _separativeSymbol { get; } = { "<", "(", "[", "{", "„", "«", "‘", ")", ">", "]", "}", "“", "»", "’", ",", ";", ":" };

        private string[] _conconantLetter { get; } = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };

        public bool IsConsonant(IWord item)
        {
            return _conconantLetter.Contains(item.Value[0].ToString().ToLower());
        }

        public bool IsEnd(IPunctuation punctuation)
        {
            return _endLetters.Contains(punctuation.Value);
        }

        public bool IsPunctuation(IPunctuation punctuation)
        {
            return _separativeSymbol.Contains(punctuation.Value);
        }
    }
}
