﻿using System.Collections.Generic;

namespace WebSales.DAL.Models
{
    public class Manager
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}