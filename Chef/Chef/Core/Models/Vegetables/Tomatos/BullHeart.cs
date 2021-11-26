using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Vegetables.Tomatos
{
    public class BullHeart : Tomato
    {
        const double fats = 0.2;

        const double proteins = 1.1;

        const double carbohydrates = 3.7;

        public override double CaloriePerHundred => 9 * fats + 4 * proteins + 4 * carbohydrates;

        public BullHeart(string name, DateTime ripeningDate, TomatoShape tomatoShape, Manufacturer manufacturer)
            : base(name, ripeningDate, tomatoShape, manufacturer)
        { }
    }
}
