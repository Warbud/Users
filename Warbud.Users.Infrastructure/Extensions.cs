using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Shared.Queries;
using Warbud.Users.Application.Services;
using Warbud.Users.Infrastructure.EF;
using Warbud.Users.Infrastructure.Services;

namespace Warbud.Users.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPostgres(configuration);
            services.AddQueries();
            services.AddSingleton<IIdService, FakeUserIdService>();

            //services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
            
            return services;
        }
    }
}