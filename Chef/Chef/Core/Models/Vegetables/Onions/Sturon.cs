using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Vegetables.Onions
{
    public class Sturon : Onion
    {
        const double fats = 0.0;

        const double proteins = 1.4;

        const double carbohydrates = 10.4;

        public override double CaloriePerHundred => 9 * fats + 4 * proteins + 4 * carbohydrates;

        public Sturon(string name, DateTime ripeningDate, Manufacturer manufcturer) : base(name, ripeningDate, manufcturer)
        { }
    }
}
