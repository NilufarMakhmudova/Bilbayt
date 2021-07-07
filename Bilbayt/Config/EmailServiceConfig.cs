using Bilbayt.Core.Interfaces.Email;
using Bilbayt.Infrastructure.AppSettings;
using Bilbayt.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Bilbayt.Config
{
    /// <summary>
    ///     Database related configurations
    /// </summary>
    public static class EmailServiceConfig
    {
        /// <summary>
        ///     Setup Cosmos DB
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void SetupEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("SendGridEmailSettings").Get<SendGridEmailSettings>();
            var options = Options.Create(settings);
            var emailService = new SendGridEmailService(options);
            services.AddSingleton<IEmailService>(emailService);
        }
    }
}
