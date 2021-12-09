using TextParser.Abstractions.Concordance;
using TextParser.Abstractions.Concordance.Helper;

namespace TextParser.Concordance.Helper
{
    public class Paginator : IPaginator
    {
        private int counterPage = 0;

        public int LineLength { get; }

        public int PageSize { get; }

        public Paginator(int lineLength, int pageSize)
        {
            LineLength = lineLength;
            PageSize = pageSize;
        }

        public ILine CreateLine()
        {
            return new Line(LineLength);
        }

        public IPage CreatePage()
        {
            return new Page(PageSize, ++counterPage);
        }

        public IBook CreateBook()
        {
            return new Book();
        }
    }
}
