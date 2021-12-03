using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Warbud.Shared;
using Warbud.Shared.Abstraction.Constants;
using Warbud.Users.Authentication;
using Warbud.Users.Application;
using Warbud.Users.Infrastructure;
using Warbud.Users.Installers;
using Warbud.Users.Services;

namespace Warbud.Users
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
            
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.Name.VerifiedUser,
                    policy => policy.Requirements.Add(new VerifiedUserRequirement()));
                options.AddPolicy(Policy.Name.AdminOrOwner,
                    policy => policy.Requirements.Add(new AdminOrOwnerRequirement()));
            });
            
            services.AddSingleton<IAuthorizationHandler, VerifiedUserRequirementsHandler>();
            services.AddSingleton<IAuthorizationHandler, AdminOrOwnerRequirementHandler>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Warbud.Api", Version = "v1"});
            });

            
            services.InstallServicesInAssembly(_config);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            app.UseShared();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warbud.Api v1"));
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
            //app.UseGraphQLVoyager();
        }
    }
}