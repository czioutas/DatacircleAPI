using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DatacircleAPI.Models
{
    public class Role : IdentityRole<int> 
    {
        public Boolean Admin { get; set; }
        public Boolean MetricRead { get; set; }
        public Boolean MetricWrite { get; set; }
        public Boolean WidgetRead { get; set; }
        public Boolean WidgetWrite { get; set; }
        public Boolean DatasourceRead { get; set; }
        public Boolean DatasourceWrite { get; set; }
        public Boolean DashboardRead { get; set; }
        public Boolean DashboardWrite { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
