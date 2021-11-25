using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models
{
    public abstract class Product
    {
        private string _name;

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException();
                }

                _name = value;
            }
        }

        public Manufacturer Manufacturer { get; set; }

        public Product(string name, Manufacturer manufacturer)
        {
            Name = name;
            Manufacturer = manufacturer;
        }

        public abstract double CaloriePerHundred { get; }
    }
}
