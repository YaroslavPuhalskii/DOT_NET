using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Vegetables.Onions
{
    public class Leeks : Onion
    {
        const double fats = 0.2;

        const double proteins = 2.0;

        const double carbohydrates = 6.3;

        public override double CaloriePerHundred => CalculateCalories(fats, proteins, carbohydrates);

        public Leeks(string name, DateTime ripeningDate, OnionSort onionSort, Manufacturer manufacturer)
            : base(name, ripeningDate, onionSort, manufacturer)
        { }

    }
}
