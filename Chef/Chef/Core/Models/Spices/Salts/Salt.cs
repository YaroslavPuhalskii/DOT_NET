using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Spices.Salts
{
    public abstract class Salt : Spice
    {
        public Salt(string name, DateTime manufacturingDate, Manufacturer manufacturer) 
            : base(name, manufacturingDate, manufacturer)
        { }
    }
}
