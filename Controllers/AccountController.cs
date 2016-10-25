using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatacircleAPI.Application;
using DatacircleAPI.Models;
using DatacircleAPI.Services;
using DatacircleAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DatacircleAPI.Controllers
{   
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        // //
        // // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
            // if (result.Succeeded)
            // {
            //     // generate and send Token
            //     return this.Ok();                    
            // }
                
            // if (result.IsLockedOut)
            // {
            //     return this.StatusCode(429);                    
            // }

            return this.BadRequest("Credentials do not match.");                    
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]        
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            try {
                await _accountService.Register(registerVm);
            } 
            catch(ResponseException exception) {
                return this.BadRequest(exception.Message);
            }
            
            return this.Ok();
        }
    }
}
