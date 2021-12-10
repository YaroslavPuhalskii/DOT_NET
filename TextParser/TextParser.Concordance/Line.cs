using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Abstractions;
using TextParser.Abstractions.Concordance;

namespace TextParser.Concordance
{
    public class Line : ILine
    {
        private int maxLength;

        private int length;

        private readonly ICollection<IToken> tokens;

        private StringBuilder builder;

        public IEnumerable<IToken> Tokens => tokens;

        public int Length
        {
            get => length;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Length can't be less one");
                }

                length = value;
            }
        }

        public int MaxLength
        {
            get => maxLength;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Length can't be less 0");
                }

                maxLength = value;
            }
        }

        public Line(int length)
        {
            MaxLength = length;
            tokens = new List<IToken>();
        }

        public bool Add(IToken token)
        {
            if (length + token.Length <= maxLength)
            {
                if (token != null)
                {
                    length += token.Length;
                    tokens.Add(token);
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            builder = builder ?? new StringBuilder();
            builder.Clear();
            tokens.ToList().ForEach(x => builder.Append(x.Value));

            return builder.ToString();
        }
    }
}
