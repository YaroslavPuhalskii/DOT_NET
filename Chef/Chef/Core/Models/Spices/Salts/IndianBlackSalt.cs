using Chef.Core.Specifications;

namespace Chef.Core.Models.Spices.Salts
{
    public class IndianBlackSalt : Salt
    {
        const double fats = 0.0;

        const double proteins = 0.0;

        const double carbohydrates = 0.1;

        public override double CaloriePerHundred => 9 * fats + 4 * proteins + 4 * carbohydrates;

        public IndianBlackSalt(string name, Manufacturer manufacturer) : base(name, manufacturer)
        { }
    }
}
