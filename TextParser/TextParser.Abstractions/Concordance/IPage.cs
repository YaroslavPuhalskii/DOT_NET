using System.Collections.Generic;

namespace TextParser.Abstractions.Concordance
{
    public interface IPage
    {
        IEnumerable<ILine> Lines { get; }

        int Size { get; set; }

        int Number { get; set; }

        bool Add(ILine line);
    }
}
