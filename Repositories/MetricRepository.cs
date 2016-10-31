using System;
using System.Linq;
using DatacircleAPI.Database;
using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DatacircleAPI.Repositories
{
    public class MetricRepository : IMetricRepository
    {
        private readonly ApplicationDbContext _context;

        public MetricRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        Metric IMetricRepository.Create(WidgetViewModel widgetVm)
        {
            DateTime now = DateTime.Now;
            
            metric.CreatedAt = now;
            metric.UpdatedAt = now;
            
            return this._context.Metric.Add(metric).Entity;            
        }

        void IMetricRepository.Delete(int metricId)
        {
            var metric = this._context.Metric.FirstOrDefault(m => m.ID == metricId);
            if (metric != null) {
                this._context.Remove(metric);
            }
        }

        Metric IMetricRepository.Get(int metricId)
        {
            return this._context.Metric.FirstOrDefault(m => m.ID == metricId);
        }

        void IMetricRepository.Update(WidgetViewModel widgetVm)
        {            
            var _metric = this._context.Metric            
            //.Include(ds => ds.Datasource)
            .Where(m => m.ID == widgetVm.metric.ID).FirstOrDefault<Metric>();
            
            _metric.Description = widgetVm.metric.Description ?? _metric.Description;
            _metric.Name = widgetVm.metric.Name ?? _metric.Name;
            _metric.Query = widgetVm.metric.Query ?? _metric.Query;
            _metric.ChartType = _metric.ChartType;
            _metric.DatasourceFk = widgetVm.metric.DatasourceFk;
            
            _metric.UpdatedAt = DateTime.Now;
             
            this._context.Metric.Update(_metric);
        }

        int IMetricRepository.Save()
        {
           return this._context.SaveChanges();
        } 
    }
}
