using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Abstractions.Concordance;

namespace TextParser.Concordance
{
    public class Book : IBook
    {
        private readonly ICollection<IPage> pages;

        private StringBuilder builder;

        public IEnumerable<IPage> Pages => pages;

        public Book()
        {
            pages = new List<IPage>();
        }

        public void Add(IPage page)
        {
            if (page != null)
            {
                pages.Add(page);
            }
        }

        public override string ToString()
        {
            builder = builder ?? new StringBuilder();
            builder.Clear();
            pages.ToList().ForEach(x => builder.Append(x));

            return builder.ToString();
        }
    }
}
