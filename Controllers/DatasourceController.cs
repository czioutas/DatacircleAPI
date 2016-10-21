using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatacircleAPI.Models;
using Microsoft.Extensions.Logging;
using DatacircleAPI.Database;
using DatacircleAPI.ViewModel;
using DatacircleAPI.Repositories;

namespace DatacircleAPI.Controllers
{

    [Route("api/[controller]")]
    public class DatasourceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDatasourceRepository _datasourceRepository;
        private readonly IConnectionDetailsRepository _connectionRepository;
        private readonly ILogger _logger;

        public DatasourceController(IDatasourceRepository datasourceRepository, IConnectionDetailsRepository connectionRepository, ApplicationDbContext context, ILogger<DatasourceController> logger)
        {
            this._connectionRepository = connectionRepository;
            this._datasourceRepository = datasourceRepository;
            this._context = context;
            this._logger = logger;
        }

        // GET api/datasource
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Datasource datasource = this._datasourceRepository.Get(id);
         
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
            ConnectionDetails connectionDetails = this._connectionRepository.Create(datasourceViewModel.connectionDetails);
            datasourceViewModel.datasource.ConnectionDetailsFk = connectionDetails.ID;
            
            this._datasourceRepository.Create(datasourceViewModel.datasource);            
            this._context.SaveChanges();

            return this.Ok();
        }

        // PUT api/datasource/5
        [HttpPut("{id}")]
        public IActionResult Put(Datasource datasource)
        {
            this._datasourceRepository.Update(datasource);
            this._datasourceRepository.Save();

            return this.Ok();        
        }

        // DELETE api/datasource/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {            
            this._datasourceRepository.Delete(id);
            this._datasourceRepository.Save();

            return this.Ok();
        }
    }
}
