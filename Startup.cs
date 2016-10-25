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

namespace DatacircleAPI
{
    public class Startup
    {
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

            services.AddScoped<DatasourceService, DatasourceService>();
            services.AddScoped<AccountService, AccountService>();            
            services.AddScoped<MailTemplateService, MailTemplateService>();
            services.AddScoped<IEmailSender, AuthMessageSender>();

            services.AddOptions();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            // Add JWT generation endpoint:
            var secretKey = "mysupersecret_secretkey!123";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var options = new TokenProviderOptions
            {
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };

            // JwtTokenAuthetication on [Authorized] Requests            
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,            
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
