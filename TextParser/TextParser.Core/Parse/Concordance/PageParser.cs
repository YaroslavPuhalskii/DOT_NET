using TextParser.Abstractions;
using TextParser.Abstractions.Concordance;
using TextParser.Abstractions.Concordance.Helper;
using TextParser.Abstractions.Parse;

namespace TextParser.Core.Parse.Concordance
{
    public class PageParser : IPageParser
    {
        private IPaginator paginator;

        private IBook book;
        private IPage page;
        private ILine line;

        public PageParser(IPaginator paginator)
        {
            this.paginator = paginator;
        }

        public IBook Parse(IText text)
        {
            book = paginator.CreateBook();
            page = paginator.CreatePage();
            line = paginator.CreateLine();

            foreach (var item in text.Sentences)
            {
                foreach (var token in item.Tokens)
                {
                    AddToken(token);
                }
            }

            AddLine(line);
            AddPage(page);

            return book;
        }

        private void AddToken(IToken token)
        {
            if (!line.Add(token))
            {
                AddLine(line);
                line = paginator.CreateLine();
                line.Add(token);
            }
        }

        private void AddLine(ILine line)
        {
            if (!page.Add(line))
            {
                AddPage(page);
                page = paginator.CreatePage();
                page.Add(line);
            }
        }

        private void AddPage(IPage page)
        {
            book.Add(page);
        }
    }
}
