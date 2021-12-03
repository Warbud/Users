using System.Threading.Tasks;
using Warbud.Users.Domain.Entities;
using Warbud.Users.Domain.ValueObjects;

namespace Warbud.Users.Domain.Repositories
{
    public interface IWarbudClaimRepository
    {
        Task<WarbudClaim> GetAsync(UserId userId, int appId, int projectId);
        Task AddAsync(WarbudClaim claim);
        Task UpdateAsync(WarbudClaim claim);
        Task RemoveAsync(WarbudClaim claim);
    }
}