using System.Collections.Generic;

namespace WebSales.Models.Chart
{
    public class SaleChartModel
    {
        public ICollection<string> Name { get; set; }

        public ICollection<int> Count { get; set; }

        public SaleChartModel()
        {
            Name = new List<string>();
            Count = new List<int>();
        }
    }
}