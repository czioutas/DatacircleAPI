using System;
using System.Linq;
using DatacircleAPI.Database;
using DatacircleAPI.Models;

namespace DatacircleAPI.Repositories
{
    public class DatasourceRepository : IDatasourceRepository
    {
        private readonly ApplicationDbContext _context;

        public DatasourceRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        Datasource IDatasourceRepository.Create(Datasource datasource)
        {
            DateTime now = DateTime.Now;
            
            datasource.CreatedAt = now;
            datasource.UpdatedAt = now;
            
            return this._context.Datasource.Add(datasource).Entity;            
        }

        void IDatasourceRepository.Delete(int datasourceId)
        {
            var datasource = this._context.Datasource.SingleOrDefault(ds => ds.ID == datasourceId);
            if (datasource != null) {
                this._context.Remove(datasource);
            }
        }

        Datasource IDatasourceRepository.Get(int datasourceId)
        {
            return this._context.Datasource.SingleOrDefault(ds => ds.ID == datasourceId);
        }

        void IDatasourceRepository.Update(Datasource datasource)
        {
            datasource.UpdatedAt = DateTime.Now;
            this._context.Datasource.Update(datasource);
        }

        int IDatasourceRepository.Save()
        {
           return this._context.SaveChanges();
        } 
    }
}
