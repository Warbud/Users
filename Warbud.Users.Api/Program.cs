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
                    try
                    {
                         var port = UseUrlsConfiguration.Configure()
                        .SetConfigPath((@"C:\WEBSITES\Warbud\ports.json")
                        .GetPort("Identity");
                        webBuilder.UseUrls($"http://localhost:{port.ToString()}");
                    }
                    catch (System.Exception)
                    {
                        webBuilder.UseUrls("http://localhost:1500");
                    }
                    
                });
    }
}
