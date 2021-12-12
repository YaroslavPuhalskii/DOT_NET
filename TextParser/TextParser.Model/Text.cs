using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Text : IText
    {
        private readonly IList<ISentence> sentences;

        private StringBuilder builder;

        public IEnumerable<ISentence> Sentences => sentences;

        public Text()
        {
            sentences = new List<ISentence>();
        }

        public ISentence this[int index]
        {
            get
            {
                if (index < 0 || index >= Sentences.Count())
                {
                    throw new IndexOutOfRangeException("index");
                }

                return sentences[index];
            }
        }

        public void Add(ISentence sentence)
        {
            sentences.Add(sentence);
        }

        public override string ToString()
        {
            builder = builder ?? new StringBuilder();
            builder.Clear();
            sentences.ToList().ForEach(x => builder.Append($"{x} "));

            return builder.ToString();
        }
    }
}
