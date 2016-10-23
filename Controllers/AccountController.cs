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
    public class AccountController : Controller
    {
        private readonly IEmailSender _emailSender;

        public AccountController(IEmailSender emailSender)
        {
            this._emailSender = emailSender;
        }
    }
}
