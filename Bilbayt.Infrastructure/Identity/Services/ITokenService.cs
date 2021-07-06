using System.Threading.Tasks;
using Bilbayt.Core.Entities;
using Bilbayt.Infrastructure.Identity.Models.Authentication;

namespace Bilbayt.Infrastructure.Identity.Services
{
    /// <summary>
    ///     A collection of token related services
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        ///     Validate the credentials entered when logging in.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress);

       
        /// <summary>
        ///     Check if the credentials passed in are valid.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <param name="password">The matching password to verify.</param>
        /// <returns>If the credentials are valid or not.</returns>
        Task<bool> IsValidUser(string username, string password);

        /// <summary>
        ///     Find an <see cref="AppUser" /> by their email.
        /// </summary>
        /// <param name="email">
        ///     <see cref="AppUser.Email" />
        /// </param>
        /// <returns>
        ///     <see cref="AppUser" />
        /// </returns>
        Task<AppUser> GetUserByEmail(string email);
    }
}
