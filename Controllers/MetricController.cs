using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatacircleAPI.Services;
using DatacircleAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

namespace DatacircleAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
    public class MetricController : Controller
    {
        private readonly MetricService _metricService;
        private readonly DatasourceService _datasourceService;

        public MetricController(MetricService metricService, DatasourceService datasourceService)
        {
            this._metricService = metricService;
            this._datasourceService = datasourceService;
        }

        //
        // GET api/metric
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            int CompanyFk = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);

            Metric metricContainer = new Metric { ID = id, CompanyId = CompanyFk };
            Metric metric = this._metricService.Get(metricContainer);

            if (metric == null)
            {
                return this.NotFound();
            }

            return this.Ok(metric);
        }

        // GET api/metric/all
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            int CompanyFk = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);

            IEnumerable<Metric> metricCollection = this._metricService.GetAll(CompanyFk);

            if (metricCollection == null)
            {
                return this.Ok();
            }

            return this.Ok(metricCollection);
        }

        //
        // POST api/metric
        [HttpPost]
        public async Task<IActionResult> Create(Metric metric)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);                
            }

            int DatasourceId = metric.DatasourceId.GetValueOrDefault();
            
            if (DatasourceId == 0) {
                return this.BadRequest("Invalid Datasource Id.");
            }

            Datasource _datasource = this._datasourceService.Get(DatasourceId);

            if (_datasource == null) {
                return this.BadRequest("No such Datasource found.");
            }
            
            metric.CompanyId = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);
            metric.Owner = int.Parse(HttpContext.User.FindFirst("UserId")?.Value);

            await this._metricService.Create(metric);
            return this.Ok();        
        }

        //
        // PUT api/metric/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Metric metric)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != metric.ID)
            {
                return this.BadRequest();
            }

            if (id == 0 || metric.ID == 0)
            {
                return this.BadRequest();
            }

            int DatasourceId = metric.DatasourceId.GetValueOrDefault();
            
            if (DatasourceId == 0) {
                return this.BadRequest("Invalid Datasource Id.");
            }

            Datasource _datasource = this._datasourceService.Get(DatasourceId);

            if (_datasource == null) {
                return this.BadRequest("No such Datasource found.");
            }

            Metric newMetric = this._metricService.Update(metric);

            return this.Ok(newMetric);
        }

        //
        // DELETE api/metric/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Metric metric)
        {
            metric.CompanyId = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);

            if (this._metricService.Delete(metric))
            {
                return this.Ok();
            }
            else
            {
                return this.BadRequest("Metric not found.");
            }
        }
    }
}
