using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatacircleAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatacircleAPI.Controllers
{
    public class MetricController : Controller
    {
        private readonly MetricService _datasourceService;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
