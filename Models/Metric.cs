using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key, Column("Id")]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Query { get; set; }
        public ChartType ChartType { get; set; }
        public int DatasourceFk { get; set; }
        [ForeignKey("DatasourceFk")]
        public Datasource Datasource { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
