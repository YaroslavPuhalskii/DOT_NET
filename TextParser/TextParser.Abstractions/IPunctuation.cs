namespace TextParser.Abstractions
{
    public interface IPunctuation : IToken
    {
        void Remove(ISymbol symbol);
    }
}
