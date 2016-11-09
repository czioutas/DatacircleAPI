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

        // GET api/metric/all
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            int CompanyFk = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);

            IEnumerable<Metric> datasourceCollection = this._metricService.GetAll(CompanyFk);

            if (datasourceCollection == null)
            {
                return this.Ok();
            }

            return this.Ok(datasourceCollection);
        }

        //
        // POST api/metric
        [HttpPost]
        public async Task<IActionResult> Create(Metric metric)
        {
            if (ModelState.IsValid)
            {
                metric.CompanyFk = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);
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

            Metric newMetric = this._metricService.Update(metric);

            return this.Ok(newMetric);
        }

        //
        // DELETE api/metric/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Metric metric)
        {
            metric.CompanyFk = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);

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
