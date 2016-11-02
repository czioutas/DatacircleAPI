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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Query { get; set; }
        [Required]
        public ChartType ChartType { get; set; }
        [Required]
        public int DatasourceFk { get; set; }
        [ForeignKey("DatasourceFk")]
        public Datasource Datasource { get; set; }
        public int CompanyFk { get; set; }
        [ForeignKey("CompanyFk")]
        public ConnectionDetails Company { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
