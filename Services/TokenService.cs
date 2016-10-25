using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using DatacircleAPI.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

namespace DatacircleAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TokenService
    {
        private readonly TokenProviderOptions _options;

        public TokenService(IOptions<TokenProviderOptions> options)
        {
            _options = options.Value;
        }

        public async Task GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];            
        
            // var identity = await GetIdentity(username, password);
            // if (identity == null)
            // {
            //     context.Response.StatusCode = 400;
            //     await context.Response.WriteAsync("Invalid username or password.");
            //     return;
            // }
        
            var nowDt = DateTime.UtcNow;
            var nowDto = DateTimeOffset.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            Claim usernameClaim = new Claim(JwtRegisteredClaimNames.Sub, username);
            Claim jtiClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            Claim iatClaim = new Claim(JwtRegisteredClaimNames.Iat, nowDto.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64);
            var claims = new Claim[]
            {
                usernameClaim,
                jtiClaim,
                iatClaim
            };
        
            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: nowDt,
                expires: nowDt.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds
            };
        
            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}
