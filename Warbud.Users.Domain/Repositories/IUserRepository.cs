using System;
using System.Threading.Tasks;
using Warbud.Users.Domain.Entities;
using Warbud.Users.Domain.ValueObjects;

namespace Warbud.Users.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(UserId id);
        Task<User> GetAsync(Email email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(User user);
    }
}