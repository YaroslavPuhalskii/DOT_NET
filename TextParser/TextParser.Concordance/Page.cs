using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Abstractions.Concordance;

namespace TextParser.Concordance
{
    public class Page : IPage
    {
        private int size;
        private int number;

        private readonly ICollection<ILine> lines;

        private StringBuilder builder;

        public IEnumerable<ILine> Lines => lines;

        public int Size
        {
            get => size;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Size can't be less one");
                }

                size = value;
            }
        }

        public int Number
        {
            get => number;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Number can't be less one");
                }

                number = value;
            }
        }

        public Page(int size, int number)
        {
            Size = size;
            Number = number;

            lines = new List<ILine>();
        }

        public bool Add(ILine line)
        {
            if (lines.Count < Size)
            {
                if (line != null)
                {
                    lines.Add(line);
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            builder = builder ?? new StringBuilder();
            builder.Clear();
            builder.AppendLine(Number.ToString());
            lines.ToList().ForEach(x => builder.AppendLine(x.ToString()));

            return builder.ToString();
        }
    }
}
