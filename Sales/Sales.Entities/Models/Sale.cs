using System;

namespace Sales.Entities.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public Client Client { get; set; }

        public Product Product { get; set; }

        public Manager Manager { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Sum { get; set; }
    }
}
