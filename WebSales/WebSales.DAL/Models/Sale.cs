using System;

namespace WebSales.DAL.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public int? ManagerId { get; set; }

        public virtual Manager Manager { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public DateTime Date { get; set; }

        public int Sum { get; set; }
    }
}
