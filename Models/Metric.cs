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
        public int? DatasourceId { get; set; }
        public Datasource Datasource { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? Owner { get; set; }
        [ForeignKey("Owner")]
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
