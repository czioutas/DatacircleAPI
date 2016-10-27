using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace DatacircleAPI.Models
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/api/token/";
        public string Issuer { get; set; } = "ExampleIssuer";
        public string Audience { get; set; } = "ExampleAudience";
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(10);
        public SigningCredentials SigningCredentials { get; set; }
    }
}
