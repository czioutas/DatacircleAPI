using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatacircleAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatacircleAPI.Controllers
{   
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IEmailSender _emailSender;

        public TestController(IEmailSender emailSender)
        {
            this._emailSender = emailSender;
        }

        // POST api/datasource
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmailTest(Email email)
        {            
            email.Template = "accountVerification";
            _emailSender.SendEmailAsync(email);
            return this.Ok(email);
        }
    }
}
