using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Warbud.Shared.Configurations;

namespace Warbud.Users.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    var port = UseUrlsConfiguration.Configure()
                        .SetConfigPath("C:/Users/afranczak/source/repos/Nairda015/Warbud/ports.json")
                        .GetPort("Identity");
                    webBuilder.UseUrls($"http://localhost:{port.ToString()}");
                });
    }
}