using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Shared.Options;
using Warbud.Users.Application.Services;
using Warbud.Users.Domain.Repositories;
using Warbud.Users.Infrastructure.EF.Contexts;
using Warbud.Users.Infrastructure.EF.Options;
using Warbud.Users.Infrastructure.EF.Repositories;
using Warbud.Users.Infrastructure.EF.Services;

namespace Warbud.Users.Infrastructure.EF
{
    internal static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, PostgresUserRepository>();
            services.AddScoped<IUserReadService, PostgresUserReadService>();
            
            services.AddScoped<IWarbudAppRepository, PostgresWarbudAppRepository>();
            services.AddScoped<IWarbudAppReadService, PostgresWarbudAppReadService>();
            
            services.AddScoped<IWarbudClaimRepository, PostgresWarbudClaimRepository>();
            services.AddScoped<IWarbudClaimReadService, PostgresWarbudClaimReadService>();

            var options = configuration.GetOptions<PostgresOptions>("Postgres");
            services.AddDbContext<ReadDbContext>(ctx => 
                ctx.UseNpgsql(options.ConnectionString));
            services.AddDbContext<WriteDbContext>(ctx => 
                ctx.UseNpgsql(options.ConnectionString));
            /*.EnableSensitiveDataLogging()*/
            return services;
        }
    }
}