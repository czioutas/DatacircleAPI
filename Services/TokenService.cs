using System;
using Microsoft.AspNetCore.Http;
using DatacircleAPI.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.Net;

namespace DatacircleAPI.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TokenService
    {
        private readonly TokenProviderOptions _options;

        public TokenService(TokenProviderOptions options)
        {
            _options = options;
        }

        public TokenProviderOptions getTokenProviderOptions()
        {
            return this._options;
        }

        public String GenerateToken(HttpContext httpContext, User user)
        {            
            return this.CreateToken(user.Email, user.Email, httpContext.Connection.RemoteIpAddress);
        }

        private String CreateToken(string username, string uniqueId, IPAddress IP)
        {
            var nowDt = DateTime.UtcNow;
            var nowDto = DateTimeOffset.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            Claim usernameClaim = new Claim(JwtRegisteredClaimNames.Sub, username + uniqueId + IP.ToString());
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
        
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });        
        }
    }
}
