using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Spices
{
    public abstract class Spice : Product
    {
        public DateTime ManufacturingDate { get; }

        public Spice(string name, DateTime manufacturingDate, Manufacturer manufacturer) : base(name, manufacturer)
        {
            ManufacturingDate = manufacturingDate;
        }
    }
}
