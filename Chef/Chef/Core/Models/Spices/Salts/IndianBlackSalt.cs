using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Spices.Salts
{
    public class IndianBlackSalt : Salt
    {
        const double fats = 0.0;

        const double proteins = 0.0;

        const double carbohydrates = 0.1;

        public override double CaloriePerHundred => CalculateCalories(fats, proteins, carbohydrates);

        public IndianBlackSalt(string name, DateTime manufacturingDate, Manufacturer manufacturer)
            : base(name, manufacturingDate, manufacturer)
        { }
    }
}
