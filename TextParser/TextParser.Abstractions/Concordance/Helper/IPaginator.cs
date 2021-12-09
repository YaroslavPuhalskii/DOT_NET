namespace TextParser.Abstractions.Concordance.Helper
{
    public interface IPaginator
    {
        ILine CreateLine();

        IPage CreatePage();

        IBook CreateBook();
    }
}
