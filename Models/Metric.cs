using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatacircleAPI.Models
{
    public enum ChartType
    {
        Line = 1,
        Bar = 2,
        Pie = 3,
        Donut = 4
    }

    public class Metric
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Query { get; set; }
        public ChartType ChartType { get; set; }
        public Datasource Datasource { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
