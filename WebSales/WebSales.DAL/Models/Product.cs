using System.Collections.Generic;

namespace WebSales.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public Product()
        {
            Sales = new HashSet<Sale>();
        }
    }
}
