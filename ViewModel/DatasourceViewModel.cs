using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatacircleAPI.Models;

namespace DatacircleAPI.ViewModel
{
    public class DatasourceViewModel
    {
        public Datasource datasource { get; set; }
        public ConnectionDetails connectionDetails { get; set; }
    }
}
