using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Abstractions;

namespace TextParser.Model
{
    public class Text : IText
    {
        private IList<ISentence> sentences;

        private StringBuilder builder = new StringBuilder();

        public IEnumerable<ISentence> Sentences => sentences;

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

        public Text()
        {
            sentences = new List<ISentence>();
        }

        public void Add(ISentence sentence)
        {
            sentences.Add(sentence);
        }

        public override string ToString()
        {
            builder.Clear();
            sentences.ToList().ForEach(x => builder.Append(x.ToString()));

            return builder.ToString();
        }
    }
}
