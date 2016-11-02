using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;
using DatacircleAPI.Repositories;

namespace DatacircleAPI.Services
{
    public class DatasourceService
    {
        private readonly IDatasourceRepository _datasourceRepository;
        private readonly IConnectionDetailsRepository _connectionRepository;
        public DatasourceService(IDatasourceRepository datasourceRepository, IConnectionDetailsRepository connectionDetails)
        {
            this._datasourceRepository = datasourceRepository;
            this._connectionRepository = connectionDetails;
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
            return this._datasourceRepository.Get(id);
        }

        public Datasource Update(DatasourceViewModel datasourceVm) 
        {
            Datasource updatedDatasource = this._datasourceRepository.Update(datasourceVm);
            if (this._datasourceRepository.Save() > 0) {
                return updatedDatasource;
            }

            return null;
        }

        public void Delete(int id)
        {
            this._datasourceRepository.Delete(id);
            this._datasourceRepository.Save();
        }
    }
}
