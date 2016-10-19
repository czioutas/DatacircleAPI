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

namespace DatacircleAPI.Controllers
{

    [Route("api/[controller]")]
    public class DatasourceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger logger;

        public DatasourceController(ApplicationDbContext context, ILogger<DatasourceController> logger)
        {
            this._context = context;
            this.logger = logger;
        }

        // GET api/datasource
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return this.BadRequest();                
            }
               
            Datasource datasource = this._context.Datasource.SingleOrDefault(ds => ds.ID == id);
         
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
            DateTime now = DateTime.Now;

            datasourceViewModel.datasource.CreatedAt = now;
            datasourceViewModel.datasource.UpdatedAt = now;
            datasourceViewModel.connectionDetails.CreatedAt = now;
            datasourceViewModel.connectionDetails.UpdatedAt = now;

            var connectionDetailsEntity = this._context.ConnectionDetails.Add(datasourceViewModel.connectionDetails);
            datasourceViewModel.datasource.ConnectionDetailsFk = connectionDetailsEntity.Entity.ID;
            
            this._context.Datasource.Add(datasourceViewModel.datasource);
            this._context.SaveChanges();

            return this.Ok();
        }

        // PUT api/datasource/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Datasource datasource)
        {
            if (id == null || datasource.ID != id)
            {
                return this.BadRequest();                
            }
            
            if (this._context.Datasource.Single(ds => ds.ID == id) == null)
            {
                return this.NotFound();
            }

            this._context.Datasource.Update(datasource);
            return new NoContentResult();        
        }

        // DELETE api/datasource/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
