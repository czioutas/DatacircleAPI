using System;
using System.Linq;
using DatacircleAPI.Database;
using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

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
            var datasource = this._context.Datasource.FirstOrDefault(ds => ds.ID == datasourceId);
            if (datasource != null) {
                this._context.Remove(datasource);
            }
        }

        Datasource IDatasourceRepository.Get(int datasourceId)
        {
            return this._context.Datasource
                        .Include(ds => ds.ConnectionDetails)
                        .FirstOrDefault(ds => ds.ID == datasourceId);
        }

        void IDatasourceRepository.Update(DatasourceViewModel datasourceVm)
        {            
            var _datasource = this._context.Datasource
            .Include(ds => ds.ConnectionDetails)
            .Where(ds => ds.ID == datasourceVm.datasource.ID).FirstOrDefault<Datasource>();

            _datasource.Title = datasourceVm.datasource.Title != null ? datasourceVm.datasource.Title : _datasource.Title;                        
            _datasource.UpdatedAt = DateTime.Now;

            _datasource.ConnectionDetails.UpdatedAt = _datasource.UpdatedAt;

            if (datasourceVm.connectionDetails.Host != null) {
                _datasource.ConnectionDetails.Host =  datasourceVm.connectionDetails.Host;
            }
             
            this._context.Datasource.Update(_datasource);
        }

        int IDatasourceRepository.Save()
        {
           return this._context.SaveChanges();
        } 
    }
}
