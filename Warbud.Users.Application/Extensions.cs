using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Shared.Commands;
using Warbud.Users.Domain.Entities;
using Warbud.Users.Domain.Factories.User;
using Warbud.Users.Domain.Factories.WarbudApp;
using Warbud.Users.Domain.Factories.WarbudClaim;

namespace Warbud.Users.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCommands();
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddSingleton<IWarbudAppFactory, WarbudAppFactory>();
            services.AddSingleton<IWarbudClaimFactory, WarbudClaimFactory>();
            
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            return services;
        }
    }
}