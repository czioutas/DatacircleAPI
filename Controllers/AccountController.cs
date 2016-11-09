using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DatacircleAPI.Application;
using DatacircleAPI.Models;
using DatacircleAPI.Services;
using DatacircleAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DatacircleAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(AccountService accountService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpGet]
        // [Authorize(ActiveAuthenticationSchemes = "Bearer, Identity.Application")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);

            return this.Ok(user);
        }

        [HttpGet]
        // [Authorize(ActiveAuthenticationSchemes = "Bearer, Identity.Application")]
        [Authorize]
        public async Task<IActionResult> GetUserSession()
        {
            String str = "";

            foreach (var item in HttpContext.User.Claims)
            {
                str += item + Environment.NewLine;
            }

            return this.Ok(str);
        }

        // //
        // // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if (ModelState.IsValid)
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

            return this.BadRequest();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            try
            {
                await _accountService.Register(registerVm);
            }
            catch (ResponseException exception)
            {
                return this.BadRequest(exception.Message);
            }

            return this.Ok();
        }

        //
        // GET: /Account/Logout
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return this.Ok();
        }

        //
        // GET: /Account/ConfirmEmail/{verificationKey}
        [HttpGet("{verificationKey}")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string verificationKey)
        {
            if (verificationKey.Length == 0)
            {
                return this.BadRequest();
            }

            this._accountService.ConfirmEmail(verificationKey);
            return this.Ok();
        }
    }
}
