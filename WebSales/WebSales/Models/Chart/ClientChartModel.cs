using System.Collections.Generic;

namespace WebSales.Models.Chart
{
    public class ClientChartModel
    {
        public ICollection<int> Ages { get; set; }

        public ICollection<int> Counts { get; set; }

        public ClientChartModel()
        {
            Ages = new List<int>();
            Counts = new List<int>();
        }
    }
}