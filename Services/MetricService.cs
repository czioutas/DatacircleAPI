using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;
using DatacircleAPI.Repositories;
using System.Threading.Tasks;
using System;

namespace DatacircleAPI.Services
{
    public class MetricService
    {
        private readonly IMetricRepository _metricRepository;
        private readonly IDatasourceRepository _datasourceRepository;

        public MetricService(IMetricRepository metricRepository, IDatasourceRepository datasourceRepository)
        {
            this._metricRepository = metricRepository;
            this._datasourceRepository = datasourceRepository;
        }

        public  async Task Create(Metric metric)
        {           
            this._metricRepository.Create(metric);
            this._datasourceRepository.Save();  
            return;          
        }

        public Metric Get(int id)
        {
            return this._metricRepository.Get(id);
        }

        public Metric Update(Metric metric) 
        {
            Metric newMetricEntity = this._metricRepository.Update(metric);
            this._metricRepository.Save();
            return newMetricEntity;            
        }

        public void Delete(int id)
        {
            this._metricRepository.Delete(id);
            this._metricRepository.Save();
        }
    }
}
