using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Users.Api.Validators;
using Warbud.Users.Application.Commands.User;
using Warbud.Users.Application.Commands.WarbudApp;
using Warbud.Users.Application.Commands.WarbudClaim;

namespace Warbud.Users.Api.Installers
{
    public class ValidationInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidation();
            services.AddTransient<IValidator<AddUser>, UserInputValidator>();
            services.AddTransient<IValidator<AddWarbudApp>, WarbudAppInputValidator>();
            services.AddTransient<IValidator<AddWarbudClaim>, WarbudClaimInputValidator>();
        }
    }
}