using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Users.Api.Validators;
using Warbud.Users.Application.Commands.User;
using Warbud.Users.Application.Commands.WarbudApp;
using Warbud.Users.Application.Commands.WarbudClaim;
using Warbud.Users.Application.DTO;

namespace Warbud.Users.Api.Installers
{
    public class ValidationInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidation();
            services.AddTransient<IValidator<AddUser>, AddUserValidator>();
            services.AddTransient<IValidator<PatchUserDtoAggregate>, PatchUserDtoAggregateValidator>();
            services.AddTransient<IValidator<AddWarbudApp>, WarbudAppInputValidator>();
            services.AddTransient<IValidator<AddWarbudClaim>, WarbudClaimInputValidator>();
        }
    }
}