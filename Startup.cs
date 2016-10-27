using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.Swagger.Model;
using Pomelo.EntityFrameworkCore.MySql;
using DatacircleAPI.Settings;
using System.Text;
using DatacircleAPI.Database;
using DatacircleAPI.Repositories;
using DatacircleAPI.Services;
using DatacircleAPI.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DatacircleAPI
{
    public class Startup
    {
        public readonly SymmetricSecurityKey _signingKey;
        public readonly SigningCredentials _sc;
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build(); 

            var secretKey = "mysupersecret_secretkey!123";
            this._signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));            
            this._sc = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);       
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:MSSQLAzureConnection"]));
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration["ConnectionStrings:DefaultConnection"]));

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "DataCircle API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Chris Zioutas", Email = "chriszioutas@datacirlce.io", Url = "http://twitter.com/drakoumel"}
                });
            });

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext,int>();
                //.AddDefaultTokenProviders();

            services.AddMvc();

            // Settings files should come first.
            // Most services are going to depend on the Settings files being there.
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<MailTemplateSettings>(Configuration.GetSection("MailTemplateSettings"));

            // AddScoped -> most performant
            // AddTransient -> better for async
            // AddSingleton -> self explanatory
            services.AddScoped<IDatasourceRepository, DatasourceRepository>();
            services.AddScoped<IConnectionDetailsRepository, ConnectionDetailsRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            // instanciate TokenService with specific TokenProviderOptions            
            TokenProviderOptions _tpo = new TokenProviderOptions { SigningCredentials = this._sc };

            //services.
            services.AddSingleton<TokenProviderOptions>(_tpo);
            services.AddScoped<TokenService>();
            services.AddScoped<DatasourceService>();
            services.AddScoped<AccountService>();
            services.AddScoped<TokenService>();            
            services.AddScoped<MailTemplateService>();
            services.AddScoped<IEmailSender, AuthMessageSender>();

            services.AddOptions();

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
                        
            // JwtTokenAuthetication on [Authorized] Requests            
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "ExampleIssuer",
                    ValidateAudience = true,
                    ValidAudience = "ExampleAudience",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = this._signingKey,            
                    ValidateLifetime = true
                }
            });

            app.UseIdentity(); 
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "api/{controller}/{action}/{id}");
            });

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}