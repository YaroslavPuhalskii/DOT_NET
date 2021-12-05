using System;
using System.Collections.Generic;

namespace TextParser.Abstractions
{
    public interface IText
    {
        IEnumerable<ISentence> Sentences { get; }

        ISentence this[int index] { get; }

        void Add(ISentence sentence);

        string ToString();
    }
}
