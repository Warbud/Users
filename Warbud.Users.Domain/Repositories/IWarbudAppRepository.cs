using System;
using System.Threading.Tasks;
using Warbud.Users.Domain.Entities;

namespace Warbud.Users.Domain.Repositories
{
    public interface IWarbudAppRepository
    {
        Task<WarbudApp> GetAsync(int id);
        Task AddAsync(WarbudApp app);
        Task UpdateAsync(WarbudApp app);
        Task RemoveAsync(WarbudApp app);
    }
}