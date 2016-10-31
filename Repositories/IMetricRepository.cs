using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;

namespace DatacircleAPI.Repositories
{
    public interface IMetricRepository
    {
        Metric Create(WidgetViewModel widgetVm);
        Metric Get(int metricId);
        void Update(WidgetViewModel widgetVm);
        void Delete(int metricId);
        int Save();
    }
}
