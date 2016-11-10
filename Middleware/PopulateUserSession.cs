using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatacircleAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DatacircleAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PopulateUserSession
    {
    //     private readonly RequestDelegate _next;
    //     private readonly UserManager<User> _userManager;
    //     private readonly SignInManager<User> _signInManager;


    //     public PopulateUserSession(RequestDelegate next, UserManager<User> userManager, SignInManager<User> signInManager)
    //     {
    //         _next = next;
    //         _userManager = userManager;
    //         _signInManager = signInManager;
    //     }

    //     public async Task Invoke(HttpContext httpContext)
    //     {
    //         if (_signInManager.IsSignedIn(httpContext.User) && !httpContext.Session.Keys.Contains("FkCompany"))
    //         {
    //             User _user = await _userManager.GetUserAsync(httpContext.User);
    //             if (_user != null)
    //             {
    //                 httpContext.Session.SetInt32("FkCompany", _user.CompanyFk);
    //                 return;
    //             }
    //         }

    //         await _next(httpContext);
    //     }
    }

    // // Extension method used to add the middleware to the HTTP request pipeline.
    // public static class PopulateUserSessionExtensions
    // {
    //     public static IApplicationBuilder UsePopulateUserSession(this IApplicationBuilder builder)
    //     {
    //         return builder.UseMiddleware<PopulateUserSession>();
    //     }
    // }
}
