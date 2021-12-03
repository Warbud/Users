using System.Threading.Tasks;

namespace Warbud.Users.Application.Services
{
    public interface IWarbudAppReadService
    {
        Task<bool> ExistsAsync(string appName, string moduleName);
        Task<bool> ExistsAsync(int id);
    }
}