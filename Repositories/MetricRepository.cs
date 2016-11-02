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

        Metric IMetricRepository.Create(Metric metric)
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

        Metric IMetricRepository.Update(Metric metric)
        {            
            var _metric = this._context.Metric            
            .Where(m => m.ID == metric.ID).FirstOrDefault<Metric>();

            Console.WriteLine("input: " + metric.Description);
            Console.WriteLine("current: " + _metric.Description);
            
            
            _metric.Description = metric.Description ?? _metric.Description;


            Console.WriteLine("output: " + metric.Description);

            _metric.Name = metric.Name ?? _metric.Name;
            _metric.Query = metric.Query ?? _metric.Query;

            if(Enum.IsDefined(typeof(ChartType), metric.ChartType)) {
                _metric.ChartType = metric.ChartType;
            }

            _metric.DatasourceFk = metric.DatasourceFk > 0 ? metric.DatasourceFk : _metric.DatasourceFk;
            
            _metric.UpdatedAt = DateTime.Now;

            Console.WriteLine("as");
             
            return this._context.Metric.Update(_metric).Entity;
        }

        int IMetricRepository.Save()
        {
           return this._context.SaveChanges();
        } 
    }
}
