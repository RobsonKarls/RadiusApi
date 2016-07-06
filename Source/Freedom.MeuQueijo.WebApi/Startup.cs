using Freedom.Domain.Entities;
using Freedom.Infrastructure.DataAccess;
using Freedom.Infrastructure.DataAccess.Base;
using Freedom.Infrastructure.DataAccess.Factories;
using Freedom.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Freedom.MeuQueijo.WebApi
{
    public class Startup
    {

        protected static UnitOfWork UnitOfWork;
        protected static FreedomDbContext _context;

        protected readonly string ConnectionString = "MadeNaRoca";


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            _context = new DataContextFactory(Configuration["Data:ConnectionString"]).GetContext();

            services.AddScoped( (_) => _context);

            services.AddMvc().AddJsonOptions(
                j => j.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            
            //injections
            services.AddScoped<IDbContext, FreedomDbContext>();
            services.AddTransient<IRepository<Product>>( s => new Repository<Product>(_context));
            services.AddTransient<IRepository<Category>>(s => new Repository<Category>(_context));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();
        }
    }
}
