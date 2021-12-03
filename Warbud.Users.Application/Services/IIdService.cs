using System.Threading.Tasks;
using Warbud.Users.Application.DTO.External;

namespace Warbud.Users.Application.Services
{
    public interface IIdService
    {
        Task<UserIdDto> GenerateIdAsync();
    }
}