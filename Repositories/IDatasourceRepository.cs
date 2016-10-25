using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;

namespace DatacircleAPI.Repositories
{
    public interface IDatasourceRepository
    {
        Datasource Create(Datasource datasource);
        Datasource Get(int datasourceId);
        void Update(DatasourceViewModel datasource);
        void Delete(int datasourceId);
        int Save();
    }
}
