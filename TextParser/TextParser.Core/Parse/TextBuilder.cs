using System;
using TextParser.Abstractions;
using TextParser.Abstractions.Parse;
using TextParser.Core.Factory;
using TextParser.Model;

namespace TextParser.Core.Parse
{
    public class TextBuilder : ITextBuilder
    {
         private FactoryLetter factoryLetter;

        private IText text;
        private ISentence sentence;

        public IText GetText => text;

        public TextBuilder()
        {
            factoryLetter = new FactoryLetter();
            text = new Text();
            sentence = new Sentence();
        }

        private void AddSentence()
        {
            text.Add(sentence);
            sentence = new Sentence();
        }

        private void Add(IWord word)
        {
            if (word != null)
            {
                if (!string.IsNullOrEmpty(word.Value))
                {
                    sentence.Add(word);
                }
            }
        }

        private void Add(IPunctuation punctuation)
        {
            if (punctuation != null)
            {
                if (!string.IsNullOrEmpty(punctuation.Value))
                {
                    sentence.Add(punctuation);
                }
            }
        }

        public bool IsFullKeySign(IPunctuation punctuation)
        {
            return factoryLetter.IsEnd(punctuation);
        }

        public bool IsKeySign(ISymbol symbol)
        {
            return factoryLetter.IsEnd(symbol)
                || factoryLetter.IsPunctuation(symbol)
                || char.IsWhiteSpace(symbol.Value);
        }

        public void Action(IWord word, IPunctuation punctuation)
        {
            if (factoryLetter.IsPunctuation(punctuation))
            {
                Add(word);
                Add(punctuation);
            }
            else if (factoryLetter.IsEnd(punctuation))
            {
                Add(word);
                Add(punctuation);
                AddSentence();
            }
            else if (factoryLetter.IsSpace(punctuation))
            {
                Add(word);
                Add(punctuation);
            }
        }
    }
}
