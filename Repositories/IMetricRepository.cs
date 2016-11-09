using System.Collections.Generic;
using System.Threading.Tasks;
using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DatacircleAPI.Repositories
{
    public interface IMetricRepository
    {
        Metric Create(Metric metric);
        Metric Get(int metricId);
        IEnumerable<Metric> GetAll(int companyFk);
        IList<Metric> GetAllByDatasource(Datasource datasource);
        Metric Update(Metric metric);
        void Delete(Metric metric);
        int Save();
    }
}
