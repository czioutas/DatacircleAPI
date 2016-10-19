using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatacircleAPI.Models;
using Microsoft.Extensions.Logging;

namespace DatacircleAPI.Controllers
{

    [Route("api/[controller]")]
    public class DatasourceController : Controller
    {
        private readonly ILogger logger;

        public DatasourceController(ILogger<DatasourceController> logger)
        {
            this.logger = logger;
        }

        // GET api/datasource
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "" + id;
        }

        // POST api/datasource
        [HttpPost]
        public IActionResult Create([FromBody]Datasource datasource)
        {            
            return this.Ok(datasource);
        }

        // PUT api/datasource/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/datasource/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
