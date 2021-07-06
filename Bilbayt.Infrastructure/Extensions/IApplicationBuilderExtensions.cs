using Bilbayt.Infrastructure.CosmosDbData.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bilbayt.Infrastructure.Extensions
{
    /// <summary>
    ///     Extension methods for IApplicationBuilder 
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        ///     Ensure Cosmos DB is created
        /// </summary>
        /// <param name="builder"></param>
        public static void EnsureCosmosDbIsCreated(this IApplicationBuilder builder)
        {
            using (IServiceScope serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                ICosmosDbContainerFactory factory = serviceScope.ServiceProvider.GetService<ICosmosDbContainerFactory>();

                factory.EnsureDbSetupAsync().Wait();
            }
        }
    }
}
