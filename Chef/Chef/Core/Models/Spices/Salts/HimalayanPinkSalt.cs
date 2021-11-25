using Chef.Core.Specifications;

namespace Chef.Core.Models.Spices.Salts
{
    public class HimalayanPinkSalt : Salt
    {
        const double fats = 0.1;

        const double proteins = 0.1;

        const double carbohydrates = 0.7;

        public override double CaloriePerHundred => 9 * fats + 4 * proteins + 4 * carbohydrates;

        public HimalayanPinkSalt(string name, Manufacturer manufacturer) : base(name, manufacturer)
        { }
    }
}
