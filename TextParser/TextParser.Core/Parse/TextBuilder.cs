using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Abstractions;
using TextParser.Core.Factory;
using TextParser.Model;

namespace TextParser.Core.Parse
{
    public class TextBuilder
    {
        private FactoryLetter factoryLetter;

        private IText text;
        private ISentence sentence;

        public TextBuilder()
        {
            factoryLetter = new FactoryLetter();
            text = new Text();
            sentence = new Sentence();
        }

        public IText GetText => text;

        public void Add()
        {
            text.Add(sentence);
            sentence = new Sentence();
        }

        public void Add(IWord word)
        {
            if (word != null)
            {
                if (!string.IsNullOrEmpty(word.Value))
                {
                    sentence.Add(word);
                }
            }
        }

        public void Add(IPunctuation punctuation)
        {
            if (punctuation != null)
            {
                if (!string.IsNullOrEmpty(punctuation.Value))
                {
                    sentence.Add(punctuation);
                }
            }
        }

        public bool IsKeySign(IPunctuation punctuation)
        {
            return factoryLetter.IsEnd(punctuation) || factoryLetter.IsPunctuation(punctuation);
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
                Add();
            }
            else if (char.IsWhiteSpace(Convert.ToChar(punctuation.Value)))
            {
                Add(word);
                Add(punctuation);
            }
        }
    }
}
