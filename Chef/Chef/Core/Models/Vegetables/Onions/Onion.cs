using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Vegetables.Onions
{
    public abstract class Onion : Vegetable
    {
        public OnionSort OnionSort { get; }

        public Onion(string name, DateTime ripeningDate, Manufacturer manufacturer) : base(name, ripeningDate, manufacturer)
        { }
    }
}
