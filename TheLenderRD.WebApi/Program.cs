using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheLenderRD.Persistence.DbContext;
using TheLenderRD.WebApi.Extencions;

namespace TheLenderRD.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var dbConfigContext = services.GetRequiredService<TheLenderRD_DBContext>();

                if (!dbConfigContext.Database.CanConnect())
                {
                    try
                    {
                        dbConfigContext.Database.Migrate();
                    }
                    catch (System.Exception ex)
                    {

                    }
                }

                if (dbConfigContext.IsDataFetched() != DBState.Fetched)
                {
                    dbConfigContext.FetchDataBase();
                }
            }

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
