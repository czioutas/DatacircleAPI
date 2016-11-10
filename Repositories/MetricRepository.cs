using System;
using System.Collections.Generic;
using System.Linq;
using DatacircleAPI.Database;
using DatacircleAPI.Models;

namespace DatacircleAPI.Repositories
{
    public class MetricRepository : IMetricRepository
    {
        private readonly ApplicationDbContext _context;

        public MetricRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        Metric IMetricRepository.Create(Metric metric)
        {                        
            DateTime now = DateTime.Now;
            
            metric.CreatedAt = now;
            metric.UpdatedAt = now;
            
            return this._context.Metric.Add(metric).Entity;            
        }

        Metric IMetricRepository.Get(Metric metric)
        {
            return this._context.Metric
                .Where(m => m.ID == metric.ID)
                .Where(m => m.CompanyId == metric.CompanyId)
                .FirstOrDefault();
        }

        IEnumerable<Metric> IMetricRepository.GetAll(int companyFk)
        {
            return this._context.Metric.Where(m => m.Company.ID == companyFk);
        }

        IList<Metric> IMetricRepository.GetAllByDatasource(Datasource datasource)
        {
            return this._context.Metric.Where(m => m.Datasource.ID == datasource.ID).ToList();
        }

        Metric IMetricRepository.Update(Metric metric)
        {            
            Metric _metric = this._context.Metric            
            .Where(m => m.ID == metric.ID).FirstOrDefault();

            if (_metric == null) {
                return null;
            }

            _metric.Description = metric.Description ?? _metric.Description;

            _metric.Name = metric.Name ?? _metric.Name;
            _metric.Query = metric.Query ?? _metric.Query;

            if(Enum.IsDefined(typeof(ChartType), metric.ChartType)) {
                _metric.ChartType = metric.ChartType;
            }

            _metric.DatasourceId = metric.DatasourceId > 0 ? metric.DatasourceId : _metric.DatasourceId;
            
            _metric.UpdatedAt = DateTime.Now;
             
            return this._context.Metric.Update(_metric).Entity;
        }

        void IMetricRepository.Delete(Metric metric)
        {
            // var test = this._context.Metric.FromSql("SELECT * FROM Metric where ID = {0} and CompanyFk = {1}", metric.ID, metric.CompanyFk).ToList();            
            Metric _metric = this._context.Metric
                    .Where(m => m.ID == metric.ID)
                    .Where(m => m.CompanyId == metric.CompanyId).FirstOrDefault();

            if (_metric != null) {
                this._context.Remove(_metric);            
            }
        }

        int IMetricRepository.Save()
        {
           return this._context.SaveChanges();
        }
    }
}
