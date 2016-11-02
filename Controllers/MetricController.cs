using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatacircleAPI.Services;
using DatacircleAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DatacircleAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
    public class MetricController : Controller
    {
        private readonly MetricService _metricService;

        public MetricController(MetricService metricService)
        {
            this._metricService = metricService;
        }

        //
        // GET api/metric
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Metric metric = this._metricService.Get(id);
         
            if (metric == null)
            {
                return this.NotFound();
            }
            
            return this.Ok(metric);
        }

        //
        // POST api/metric
        [HttpPost]
        public async Task<IActionResult> Create(Metric metric)
        {            
            if (ModelState.IsValid)
            {
                metric.CompanyFk = HttpContext.Session.GetInt32("FkCompany").GetValueOrDefault();
                await this._metricService.Create(metric);
                return this.Ok();
            }

            return this.BadRequest(metric);
        }

        //
        // PUT api/metric/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Metric metric)
        {
            if (!ModelState.IsValid) {
                return this.BadRequest(ModelState);
            }
            
            if (id != metric.ID) 
            {
                return this.NotFound();
            }

            if (id == 0 || metric.ID == 0) {
                return this.BadRequest();
            }

            Metric newMetric = this._metricService.Update(metric);

            return this.Ok(newMetric);        
        }

        //
        // DELETE api/metric/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {            
            this._metricService.Delete(id);

            return this.Ok();
        }
    }
}
