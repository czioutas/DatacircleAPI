using System;
using System.Collections;
using System.Threading.Tasks;
using DatacircleAPI.Models;
using DatacircleAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatacircleAPI.Controllers
{
    [Route("api/[controller]")]
    public class DataControllerController : Controller
    {
        private readonly MetricService _metricService;

        private readonly DatasourceService _datasourceService;
        private readonly DataEngineService _dataEngineService;

        public DataControllerController(MetricService metricService, DatasourceService datasourceService, DataEngineService dataEngineService)
        {
            this._metricService = metricService;
            this._datasourceService = datasourceService;
            this._dataEngineService = dataEngineService;
        }

        //
        // GET api/dataenginetest/{metricId}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            int CompanyFk = int.Parse(HttpContext.User.FindFirst("CompanyFk")?.Value);
            Metric metricContainer = new Metric { ID = id, CompanyId = CompanyFk };

            Metric metric = this._metricService.Get(metricContainer);

            int DatasourceId = metric.DatasourceId.GetValueOrDefault();

            if (DatasourceId == 0)
            {
                return this.BadRequest("Invalid Datasource Id.");
            }

            Datasource datasourceContainer = new Datasource { ID = DatasourceId, CompanyId = CompanyFk };
            Datasource datasource = this._datasourceService.Get(datasourceContainer);

            try
            {
                ArrayList data = await this._dataEngineService.GetData(metric, datasource);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest("Error processing request.");
            }
        }
    }
}
