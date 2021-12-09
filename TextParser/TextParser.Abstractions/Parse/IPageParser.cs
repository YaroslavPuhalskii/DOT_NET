using TextParser.Abstractions.Concordance;

namespace TextParser.Abstractions.Parse
{
    public interface IPageParser
    {
        IBook Parse(IText text);
    }
}
