using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Warbud.Shared.Options;
using Warbud.Users.Authentication;

namespace Warbud.Users.Installers
{
    public class JwtInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var authSettings = new AuthenticationSettings();
            
            services.AddSingleton(authSettings);
            configuration.GetSection("Auth").Bind(authSettings);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Bearer";
                    options.DefaultScheme = "Bearer";
                    options.DefaultChallengeScheme = "Bearer";
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = authSettings.JwtIssuer,
                        ValidAudience = authSettings.JwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey)),
                        ClockSkew = new TimeSpan(authSettings.TicksPerSecond * authSettings.ClockSkew)
                    };
                });
        }
    }
}