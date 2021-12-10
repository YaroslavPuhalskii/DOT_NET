using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using TextParser.Abstractions;
using TextParser.Abstractions.Concordance;

namespace TextParser.Concordance
{
    public class Concordance
    {
        private readonly string path = ConfigurationManager.AppSettings.Get("concordanceFile");

        private StringBuilder builder;

        public struct PositionInfo
        {
            public int Counter { get; set; }

            public ISet<int> Lines { get; set; }
        }

        public void WriteConcordance(IBook book)
        {
            try
            {
                using (var writer = new StreamWriter(path))
                {
                    foreach (var group in BreakWordsIntoLines(book))
                    {
                        writer.WriteLine(group.Key);
                        foreach (var item in group)
                        {
                            builder = builder ?? new StringBuilder();
                            builder.Append($"{item.Key}---------{item.Value.Counter} : ");
                            item.Value.Lines.ToList().ForEach(x => builder.Append($"{x} "));
                            writer.WriteLine(builder.ToString());
                            builder.Clear();
                        }
                        writer.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(nameof(ex));
            }
        }

        public IEnumerable<IGrouping<char, KeyValuePair<string, PositionInfo>>> BreakWordsIntoLines(IBook book)
        {
            var words = from page in book.Pages
                            from line in page.Lines
                                from token in line.Tokens
                                   where token is IWord
                                    select new
                                 {
                                     Word = token.Value.ToLower(),
                                     NumberPage = page.Number
                                 };

            var dictionary = new Dictionary<string, PositionInfo>();
            foreach (var word in words)
            {
                if (!dictionary.ContainsKey(word.Word))
                {
                    var info = new PositionInfo();
                    info.Lines = new HashSet<int>();
                    info.Lines.Add(word.NumberPage);
                    info.Counter = 1;
                    dictionary.Add(word.Word, info);
                }
                else
                {
                    var info = dictionary[word.Word];
                    info.Counter += 1;
                    info.Lines.Add(word.NumberPage);
                    dictionary[word.Word] = info;
                }
            }

            var groups = from w in dictionary
                         orderby w.Key
                         group w by char.ToUpper(w.Key.First());

            foreach (var group in groups)
            {
                yield return group;
            }
        }
    }
}
