using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.Swagger.Model;
using DatacircleAPI.Database;
using DatacircleAPI.Repositories;
using DatacircleAPI.Services;
// using Pomelo.EntityFrameworkCore.MySql;
using DatacircleAPI.Models;
using DatacircleAPI.Settings;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:MSSQLAzureConnection"]));
            // services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration["ConnectionStrings:DefaultConnection"]));

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

            // Settings files should come first.
            // Most services are going to depend on the Settings files being there.
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<MailTemplateSettings>(Configuration.GetSection("MailTemplateSettings"));

            // AddScoped -> most performant
            // AddTransient -> better for async
            // AddSingleton -> self explanatory
            services.AddScoped<IDatasourceRepository, DatasourceRepository>();
            services.AddScoped<IConnectionDetailsRepository, ConnectionDetailsRepository>();

            services.AddScoped<DatasourceService, DatasourceService>();
            services.AddScoped<MailTemplateService, MailTemplateService>();
            services.AddScoped<IEmailSender, AuthMessageSender>();

            services.AddOptions();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
