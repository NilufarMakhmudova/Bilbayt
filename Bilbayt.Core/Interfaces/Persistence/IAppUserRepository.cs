using System.Collections.Generic;
using System.Threading.Tasks;
using Bilbayt.Core.Entities;

namespace Bilbayt.Core.Interfaces.Persistence
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<IEnumerable<AppUser>> GetUsersAsyncByUsername(string username);
    }
}
