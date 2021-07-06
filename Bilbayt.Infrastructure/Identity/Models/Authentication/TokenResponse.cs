using Bilbayt.Core.Entities;

namespace Bilbayt.Infrastructure.Identity.Models.Authentication
{
    public class TokenResponse
    {
        public TokenResponse(AppUser user, 
                             string token
                            )
        {
            Id = user.Id;
            FullName = $"{user.FirstName} {user.LastName}";
            EmailAddress = user.UserName;
            Token = token;
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
    }
}
