using System.Collections.Generic;

namespace WebSales.Models.Chart
{
    public class ProductChartModel
    {
        public ICollection<string> Categories { get; set; }

        public ICollection<int> Counts { get; set; }

        public ProductChartModel()
        {
            Categories = new List<string>();
            Counts = new List<int>();
        }
    }
}