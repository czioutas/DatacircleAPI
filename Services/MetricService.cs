using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;
using DatacircleAPI.Repositories;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

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
            this._metricRepository.Save();  
            return;          
        }

        public Metric Get(Metric metric)
        {
            return this._metricRepository.Get(metric);
        }

        public IEnumerable<Metric> GetAll(int companyFk)
        {
            return this._metricRepository.GetAll(companyFk);
        }

        public IList<Metric> GetAllByDatasource(Datasource datasource)
        {
            return this._metricRepository.GetAllByDatasource(datasource);
        }

        public Metric Update(Metric metric) 
        {
            Metric newMetricEntity = this._metricRepository.Update(metric);
            this._metricRepository.Save();
            return newMetricEntity;            
        }

        public bool Delete(Metric metric)
        {
            this._metricRepository.Delete(metric);

            return Convert.ToBoolean(this._metricRepository.Save());
        }
    }
}
