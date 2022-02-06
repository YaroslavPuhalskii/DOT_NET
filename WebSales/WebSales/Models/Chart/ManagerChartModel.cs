using System.Collections.Generic;

namespace WebSales.Models.Chart
{
    public class ManagerChartModel
    {
        public ICollection<int> Counts { get; set; }

        public ICollection<int> Ages { get; set; }

        public ManagerChartModel()
        {
            Counts = new List<int>();
            Ages = new List<int>();
        }
    }
}