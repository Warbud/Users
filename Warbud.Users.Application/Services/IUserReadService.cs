using System;
using System.Threading.Tasks;

namespace Warbud.Users.Application.Services
{
    public interface IUserReadService
    {
        Task<bool> ExistsByEmailAsync(string email);
        
        Task<bool> ExistsByIdAsync(Guid id);
    }
}