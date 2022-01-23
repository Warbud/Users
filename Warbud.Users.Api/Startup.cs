using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Warbud.Shared;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Shared.Abstraction.Interfaces;
using Warbud.Shared.Services;
using Warbud.Users.Api.Authentication;
using Warbud.Users.Application;
using Warbud.Users.Infrastructure;
using Warbud.Users.Api.Installers;

namespace Warbud.Users.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddShared();
            services.AddApplication();
            services.AddInfrastructure(_config);
            services.AddControllers();
            
            services
                .AddControllersWithViews()
                .AddNewtonsoftJson();
            
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.Name.VerifiedUser, policy =>
                    policy.RequireClaim(Claim.Name.Role, Role.Name.Admin, Role.Name.BasicUser));
                options.AddPolicy(Policy.Name.AdminOrOwner,
                    policy => policy.Requirements.Add(new AdminOrOwnerRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, AdminOrOwnerRequirementHandler>();

            services.AddSwaggerGen(c =>
            {
                
                c.SwaggerDoc("v0.0.1", new OpenApiInfo {Title = "Warbud.Api", Version = "v0.0.1"});
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        System.Array.Empty<string>()
                    }
                });
            });

            
            services.InstallServicesInAssembly(_config);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v0.0.1/swagger.json", "Warbud.Api v0.0.1"));
            }
            
            app.UseShared();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}