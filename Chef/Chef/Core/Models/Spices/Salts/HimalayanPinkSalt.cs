using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Spices.Salts
{
    public class HimalayanPinkSalt : Salt
    {
        const double fats = 0.1;

        const double proteins = 0.1;

        const double carbohydrates = 0.7;

        public override double CaloriePerHundred => CalculateCalories(fats, proteins, carbohydrates);

        public HimalayanPinkSalt(string name, DateTime manufacturingDate, Manufacturer manufacturer) : base(name, manufacturingDate, manufacturer)
        { }
    }
}
