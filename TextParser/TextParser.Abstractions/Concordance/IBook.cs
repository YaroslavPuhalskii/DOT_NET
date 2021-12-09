using System.Collections.Generic;

namespace TextParser.Abstractions.Concordance
{
    public interface IBook
    {
        IEnumerable<IPage> Pages { get; }

        void Add(IPage page);
    }
}
