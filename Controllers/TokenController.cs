using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatacircleAPI.Models;
using DatacircleAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DatacircleAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly TokenService _tokenService;

        public TokenController(UserManager<User> userManager, TokenService tokenService)
        {
            this._userManager = userManager;
            this._tokenService = tokenService;
        }

        //
        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> GetActionToken()
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null) {
                return this.BadRequest();
            }
            
            return this.Ok(this._tokenService.GenerateToken(HttpContext, user));
        }
    }
}
