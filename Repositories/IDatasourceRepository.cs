using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatacircleAPI.Models;

namespace DatacircleAPI.Repositories
{
    public interface IDatasourceRepository
    {
        Datasource Create(Datasource datasource);
        Datasource Get(int datasourceId);
        void Update(Datasource datasource);
        void Delete(int datasourceId);
        int Save();
    }
}
