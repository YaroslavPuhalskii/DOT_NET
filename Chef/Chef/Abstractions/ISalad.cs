using Chef.Core.Models;
using Chef.Core.Models.Vegetables;
using System;
using System.Collections.Generic;

namespace Chef.Abstractions
{
    public interface ISalad
    {
        string Name { get; set; }

        double TotalCaloric { get; }

        void Add(Ingredient ingredient);

        void Remove(Ingredient ingredient);

        IEnumerable<Vegetable> FindBy(Func<Vegetable, bool> func);

        IEnumerable<Vegetable> SortBy<TKey>(Func<Vegetable, TKey> func);

        IEnumerable<Vegetable> SortByDescending<TKey>(Func<Vegetable, TKey> func);
    }
}