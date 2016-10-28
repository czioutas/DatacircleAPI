using System;
using Microsoft.IdentityModel.Tokens;


namespace DatacircleAPI.Models
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/api/token/";
        public string Issuer { get; set; } = "ExampleIssuer";
        public string Audience { get; set; } = "ExampleAudience";
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(1);
        public SigningCredentials SigningCredentials { get; set; }
    }
}
