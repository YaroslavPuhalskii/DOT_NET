using System;

namespace Sales.Entities.Models
{
    public class FileData
    {
        public int Id { get; set; }

        public Manager Manager { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
