using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bilbayt.Core.Entities;
using Bilbayt.Core.Interfaces.Password;
using Bilbayt.Core.Interfaces.Persistence;
using Bilbayt.Core.Specifications;
using Bilbayt.Infrastructure.Identity.Models.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Bilbayt.Infrastructure.Identity.Services
{
    /// <inheritdoc cref="ITokenService" />
    public class TokenService : ITokenService
    {
        private readonly Token _token;
        private readonly IAppUserRepository _repo;
        private readonly IPasswordManager _passwordManager;

        /// <inheritdoc cref="ITokenService" />
        public TokenService(
            IOptions<Token> tokenOptions, 
            IAppUserRepository repo, 
            IPasswordManager passwordManager)
        {
            _repo = repo;
            _passwordManager = passwordManager;
            _token = tokenOptions.Value;
        }

        /// <inheritdoc cref="ITokenService.Authenticate(TokenRequest, string)"/>
        public async Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress)
        {
            if (await IsValidUser(request.Username, request.Password))
            {
                AppUser user = await GetUserByEmail(request.Username);

                if (user != null)
                {
                    string jwtToken = await GenerateJwtToken(user);
                    return new TokenResponse(user, jwtToken);
                }
            }

            return null;
        }


        /// <inheritdoc cref="ITokenService.IsValidUser(string, string)" />
        public async Task<bool> IsValidUser(string username, string password)
        {
            AppUser user = await GetUserByEmail(username);

            if (user == null)
            {
                // Username or password was incorrect.
                return false;
            }

            //compare password
            return _passwordManager.ValidatePassword(password, user.Password);
        }

        /// <inheritdoc cref="ITokenService.GetUserByEmail(string)" />
        public async Task<AppUser> GetUserByEmail(string email)
        {
            AppUserSearchSpecification specification = new AppUserSearchSpecification(email,
                exactSearch: true);

            System.Collections.Generic.IEnumerable<AppUser> entities = await _repo.GetItemsAsync(specification);
            return entities.FirstOrDefault();
        }

        /// <summary>
        ///     Issue JWT token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<string> GenerateJwtToken(AppUser user)
        {
          byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id),
                    new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
