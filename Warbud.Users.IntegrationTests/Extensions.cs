using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Warbud.Users.Api;
using Warbud.Users.Domain.Entities;
using Warbud.Users.Infrastructure.EF.Contexts;

namespace Warbud.Users.IntegrationTests
{
    internal static class Extensions
    {
        internal static HttpContent ToJsonContent(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        
        internal static HttpContent ToJsonPatchContent(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json-patch+json");
        }

        internal static async Task SeedUser(this WebApplicationFactory<Startup> factory, User user)
        {
            var scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var writeDbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
            var existingUser = await writeDbContext.Users.FindAsync(user.Id);
            if (existingUser is null)
            {
                await writeDbContext.Users.AddAsync(user);
                await writeDbContext.SaveChangesAsync();
            }
        }
        internal static async Task ClearUsers(this WebApplicationFactory<Startup> factory)
        {
            var scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var writeDbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
            if (writeDbContext.Users.Any())
            {
                writeDbContext.Users.RemoveRange(writeDbContext.Users);
                await writeDbContext.SaveChangesAsync();
            }
        }
    }
    
}