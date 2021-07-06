using Bilbayt.Core.Interfaces.Persistence;
using Bilbayt.Infrastructure.AppSettings;
using Bilbayt.Infrastructure.CosmosDbData.Repository;
using Bilbayt.Infrastructure.Extensions;
using Bilbayt.Infrastructure.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bilbayt.Config
{
    /// <summary>
    ///     Database related configurations
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        ///     Setup Cosmos DB
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void SetupCosmosDb(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind database-related bindings
            CosmosDbSettings cosmosDbConfig = configuration.GetSection("ConnectionStrings:Bilbayt").Get<CosmosDbSettings>();
            // register CosmosDB client and data repositories
            services.AddCosmosDb(cosmosDbConfig.Account,
                                 cosmosDbConfig.PrimaryKey,
                                 cosmosDbConfig.DatabaseName,
                                 cosmosDbConfig.Containers);

            services.AddScoped<IAppUserRepository, AppUserRepository>();
            
            // services required using Identity
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
