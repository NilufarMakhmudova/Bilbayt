using Bilbayt.Core.Interfaces.Password;
using Bilbayt.Infrastructure.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace Bilbayt.Config
{
    public static class PasswordHashingConfig
    {
        /// <summary>
        ///     Configure password hashing implementation
        /// </summary>
        /// <param name="services"></param>
        public static void SetupPasswordHasher(this IServiceCollection services)
        {
            services.AddScoped<IPasswordManager, PasswordManager>();
        }
    }
}
