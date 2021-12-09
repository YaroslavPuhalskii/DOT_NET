﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextParser.Abstractions.Concordance;

namespace TextParser.Concordance
{
    public class Book : IBook
    {
        private ICollection<IPage> pages;

        private StringBuilder builder = new StringBuilder();

        public IEnumerable<IPage> Pages => pages;

        public Book()
        {
            pages = new List<IPage>();
        }

        public void Add(IPage page)
        {
            if (page != null)
            {
                pages.Add(page);
            }
        }

        public override string ToString()
        {
            builder.Clear();
            pages.ToList().ForEach(x => builder.Append(x.ToString()));

            return builder.ToString();
        }
    }
}