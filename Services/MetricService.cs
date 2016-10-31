using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;
using DatacircleAPI.Repositories;

namespace DatacircleAPI.Services
{
    public class MetricService
    {
        private readonly IMetricRepository _metricRepository;

        public MetricService(IMetricRepository metricRepository)
        {
            this._metricRepository = metricRepository;
        }

        public void Create(Datasource datasource, ConnectionDetails connectionDetails)
        {
            ConnectionDetails _connectionDetails = this._connectionRepository.Create(connectionDetails);
            datasource.ConnectionDetailsFk = connectionDetails.ID;
            
            this._datasourceRepository.Create(datasource);
            this._datasourceRepository.Save();            
        }

        public Datasource Get(int id)
        {
            return this._metricRepository.Get(id);
        }

        public void Update(DatasourceViewModel datasourceVm) 
        {
            this._metricRepository.Update(datasourceVm);
            this._metricRepository.Save();
        }

        public void Delete(int id)
        {
            this._metricRepository.Delete(id);
            this._metricRepository.Save();
        }
    }
}
