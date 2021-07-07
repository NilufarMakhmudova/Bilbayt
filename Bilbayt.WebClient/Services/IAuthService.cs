using System.Threading.Tasks;
using Bilbayt.WebClient.Models;

namespace Bilbayt.WebClient.Services
{
    public interface IAuthService
    {
        Task<AuthenticateResult> Login(AuthenticateModel authenticateModel);
        Task Logout();
        Task<CreateUserResult> Register(CreateUserModel createUserModel);
        Task<UserProfileResult> GetProfile(string id);
    }
}
