using System.Reflection;
using AutoMapper;
using Bilbayt.Infrastructure.Extensions;
using Bilbayt.Infrastructure.Identity.Models.Authentication;
using Bilbayt.Config;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bilbayt
{
    /// <summary>
    ///     Start up
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Strongly-typed configurations using IOptions
            services.Configure<Token>(Configuration.GetSection("token"));

            //Define password hashing implementation
            services.SetupPasswordHasher();

            // Authentication and Authorization
            services.SetupAuthentication(Configuration);
            services.SetAuthorization();

            // Cosmos DB for application data
            services.SetupCosmosDb(Configuration);
          

            // API controllers
            services.SetupControllers();

            // HttpContext
            services.AddHttpContextAccessor();

            // AutoMapper, this will scan and register everything that inherits AutoMapper.Profile
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // MediatR for Command/Query pattern and pipeline behaviours
            services.SetupMediatr();

           
            // NSwag Swagger
            services.SetupNSwag();

            // OData 
            services.SetupOData();

            //SendGrid email service
            services.SetupEmailService(Configuration);
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // ONLY automatically create development databases
                app.EnsureCosmosDbIsCreated();
            }

            // NSwag Swagger
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapControllers();

                // OData configuration
                endpointRouteBuilder.EnableDependencyInjection();
                endpointRouteBuilder.Filter().Select().Count().OrderBy();
            });
        }
    }
}
