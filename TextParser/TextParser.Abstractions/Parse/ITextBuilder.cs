namespace TextParser.Abstractions.Parse
{
    public interface ITextBuilder
    {
        IText GetText { get; }

        bool IsKeySign(ISymbol symbol);

        bool IsFullKeySign(IPunctuation punctuation);

        void Action(IWord word, IPunctuation punctuation);
    }
}
