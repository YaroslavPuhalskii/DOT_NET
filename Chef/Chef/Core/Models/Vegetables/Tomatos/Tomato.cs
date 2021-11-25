using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Vegetables.Tomatos
{
    public abstract class Tomato : Vegetable
    {
        public TomatoShape TomatoShape { get;  }

        public Tomato(string name, DateTime ripeningDate, Manufacturer manufacturer) : base(name, ripeningDate, manufacturer)
        { }
    }
}
