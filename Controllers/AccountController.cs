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
        private readonly SignInManager<User> _signInManager;

        public AccountController(AccountService accountService, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _accountService = accountService;
        }

        // //
        // // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            var result = await _signInManager.PasswordSignInAsync(loginVm.Email, loginVm.Password, loginVm.RememberMe, lockoutOnFailure: true);
            
            if (!result.Succeeded)
            {
                throw new ResponseException("A combination of those credentials did not match.");                 
            }
                
            if (result.IsLockedOut)
            {
                return this.StatusCode(429);                    
            }

            return this.Ok();                    
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
