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
        Metric Update(Metric metric);
        void Delete(int metricId);
        int Save();
    }
}
