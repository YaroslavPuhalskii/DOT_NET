using Chef.Abstractions;
using Chef.Core.Models;
using Chef.Core.Models.Vegetables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chef.Core
{
    public class Salad : ISalad
    {
        private string _name;

        private ICollection<Ingredient> _ingredients;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name can't be empty or null!");
                }

                _name = value;
            }
        }

        private IEnumerable<Vegetable> Vegetables => _ingredients.Select(x => x.Product).OfType<Vegetable>();

        public double TotalCaloric => _ingredients.Sum(x => x.CaloricPerIngredient);

        public Salad(string name)
        {
            Name = name;
            _ingredients = new List<Ingredient>();
        }

        public void Add(Ingredient ingredient)
        {
            _ingredients.Add(ingredient);
        }

        public void Remove(Ingredient ingredient)
        {
            _ingredients.Remove(ingredient);
        }

        public IEnumerable<Vegetable> FindBy(Func<Vegetable, bool> func)
        {
            return Vegetables.Where(func);
        }

        public IEnumerable<Vegetable> SortBy<TKey>(Func<Vegetable, TKey> func)
        {
            return Vegetables.OrderBy(func);
        }

        public IEnumerable<Vegetable> SortByDescending<TKey>(Func<Vegetable, TKey> func)
        {
            return Vegetables.OrderByDescending(func);
        }
    }
}
