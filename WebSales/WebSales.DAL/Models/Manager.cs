using System.Collections.Generic;

namespace WebSales.DAL.Models
{
    public class Manager
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public Manager()
        {
            Sales = new HashSet<Sale>();
        }
    }
}
