using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Vegetables.Tomatos
{
    public class Cherry : Tomato
    {
        const double fats = 0.1;

        const double proteins = 0.8;

        const double carbohydrates = 2.8;

        public override double CaloriePerHundred => 9 * fats + 4 * proteins + 4 * carbohydrates;

        public Cherry(string name, DateTime ripeningDate, Manufacturer manufacturer) : base(name, ripeningDate, manufacturer)
        { }
    }
}
