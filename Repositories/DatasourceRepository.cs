using System;
using System.Collections.Generic;
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

        Datasource IDatasourceRepository.Get(int datasourceId)
        {
            return this._context.Datasource
                        .Include(ds => ds.ConnectionDetails)
                        .FirstOrDefault(ds => ds.ID == datasourceId);
        }

        IEnumerable<Datasource> IDatasourceRepository.GetAll(int companyFk)
        {
            return this._context.Datasource
                        .Include(ds => ds.ConnectionDetails)
                        .Where(ds => ds.CompanyFk == companyFk);
        }

        Datasource IDatasourceRepository.Update(DatasourceViewModel datasourceVm)
        {
            Datasource _datasource = this._context.Datasource
            .Include(ds => ds.ConnectionDetails)
            .Where(ds => ds.CompanyFk == datasourceVm.datasource.CompanyFk)
            .FirstOrDefault(ds => ds.ID == datasourceVm.datasource.ID);

            if (_datasource == null)
            {
                return null;
            }

            if (Enum.IsDefined(typeof(DatacircleAPI.Models.Type), datasourceVm.datasource.Type))
            {
                _datasource.Type = datasourceVm.datasource.Type;
            }

            Console.WriteLine(datasourceVm.datasource.Type);
            Console.WriteLine(_datasource.Type);
            Console.WriteLine(Enum.IsDefined(typeof(DatacircleAPI.Models.Type), datasourceVm.datasource.Type));

            _datasource.Description = datasourceVm.datasource.Description ?? _datasource.Description;
            _datasource.Title = datasourceVm.datasource.Title ?? _datasource.Title;
            _datasource.UpdatedAt = DateTime.Now;

            _datasource.ConnectionDetails.UpdatedAt = _datasource.UpdatedAt;

            if (datasourceVm.connectionDetails.Host != null)
            {
                _datasource.ConnectionDetails.Host = datasourceVm.connectionDetails.Host;
            }

            this._context.Datasource.Update(_datasource);
            return _datasource;
        }

        void IDatasourceRepository.Delete(Datasource datasource)
        {
            var _datasource = this._context.Datasource
                .Include(ds => ds.ConnectionDetails)
                .FirstOrDefault(ds => ds.ID == datasource.ID);

            if (_datasource != null)
            {
                this._context.Remove(_datasource);
            }
        }

        int IDatasourceRepository.Save()
        {
            return this._context.SaveChanges();
        }
    }
}
