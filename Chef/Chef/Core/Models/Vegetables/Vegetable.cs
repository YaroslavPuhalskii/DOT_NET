using Chef.Core.Specifications;
using System;

namespace Chef.Core.Models.Vegetables
{
    public abstract class Vegetable : Product
    {
        public DateTime RipeningDate { get; }

        public Vegetable(string name, DateTime ripeningDate, Manufacturer manufacturer) : base(name, manufacturer)
        {
            RipeningDate = ripeningDate;
        }
    }
}
