using System.Collections.Generic;
using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;

namespace DatacircleAPI.Repositories
{
    public interface IDatasourceRepository
    {
        Datasource Create(Datasource datasource);
        Datasource Get(int datasourceId);
        IEnumerable<Datasource> GetAll(int companyFk);
        Datasource Update(DatasourceViewModel datasource);
        void Delete(Datasource datasource);
        int Save();
    }
}
