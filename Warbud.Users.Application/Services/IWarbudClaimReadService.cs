using System;
using System.Threading.Tasks;

namespace Warbud.Users.Application.Services
{
    public interface IWarbudClaimReadService
    {
        Task<bool> ExistsByKeyAsync(Guid userId, int appId, int projectId);
    }
}