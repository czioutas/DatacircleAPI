using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;
using DatacircleAPI.Services;


namespace DatacircleAPI.Controllers
{

    [Route("api/[controller]")]
    public class DatasourceController : Controller
    {
        private readonly ILogger _logger;

        private readonly DatasourceService _datasourceService;

        public DatasourceController(ILogger<DatasourceController> logger, DatasourceService datasourceService)
        {            
            this._logger = logger;
            this._datasourceService = datasourceService;
        }

        // GET api/datasource
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Datasource datasource = this._datasourceService.Get(id);
         
            if (datasource == null)
            {
                return this.NotFound();
            }
            
            return this.Ok(datasource);
        }

        // POST api/datasource
        [HttpPost]
        public IActionResult Create(DatasourceViewModel datasourceViewModel)
        {            
            this._datasourceService.Create(datasourceViewModel.datasource, datasourceViewModel.connectionDetails);

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

            if (id == 0 || datasourceVm.datasource.ID == 0) {
                return this.BadRequest();
            }

            this._datasourceService.Update(datasourceVm);

            return this.Ok();        
        }

        // DELETE api/datasource/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {            
            this._datasourceService.Delete(id);

            return this.Ok();
        }
    }
}
