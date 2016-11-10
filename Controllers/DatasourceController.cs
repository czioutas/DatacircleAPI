using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;
using DatacircleAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace DatacircleAPI.Controllers
{

    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
    // [Authorize]
    public class DatasourceController : Controller
    {
        private readonly ILogger _logger;

        private readonly DatasourceService _datasourceService;
        private readonly MetricService _metricService;

        public DatasourceController(ILogger<DatasourceController> logger, DatasourceService datasourceService, MetricService metricService)
        {
            this._logger = logger;
            this._datasourceService = datasourceService;
            this._metricService = metricService;
        }

        // GET api/datasource
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            int CompanyFk = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);

            Datasource datasourceContainer = new Datasource { ID = id, CompanyId = CompanyFk };
            Datasource datasource = this._datasourceService.Get(datasourceContainer);

            if (
                datasource == null ||
                datasource.CompanyId != int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value)
            )
            {
                return this.NotFound();
            }

            return this.Ok(datasource);
        }

        // GET api/datasource/all
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            int CompanyFk = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);

            IEnumerable<Datasource> datasourceCollection = this._datasourceService.GetAll(CompanyFk);

            if (datasourceCollection == null)
            {
                return this.NotFound();
            }

            return this.Ok(datasourceCollection);
        }

        // POST api/datasource
        [HttpPost]
        public IActionResult Create(DatasourceViewModel datasourceVm)
        {
            datasourceVm.datasource.CompanyId = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);
            datasourceVm.datasource.Owner = int.Parse(HttpContext.User.FindFirst("UserId")?.Value);

            this._datasourceService.Create(datasourceVm.datasource, datasourceVm.connectionDetails);

            return this.Ok();
        }

        // PUT api/datasource/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, DatasourceViewModel datasourceVm)
        {
            if (id != datasourceVm.datasource.ID)
            {
                return this.NotFound();
            }

            datasourceVm.datasource.CompanyId = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);
            Datasource updatedDatasource = this._datasourceService.Update(datasourceVm);

            if (updatedDatasource == null)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        // DELETE api/datasource/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Datasource datasource)
        {
            datasource.CompanyId = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);

            IList<Metric> _metricCollection = this._metricService.GetAllByDatasource(datasource);

            if (_metricCollection.Count > 0)
            {
                return this.BadRequest("Cannot delete a datasource that has Metrics.");
            }

            this._datasourceService.Delete(datasource);

            return this.Ok();
        }
    }
}
