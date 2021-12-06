using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {

        public static IHost MigrateDataBase<TContext>(this IHost host,Action<TContext,IServiceProvider> seeder,int? retry=0) where TContext:DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope=host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetRequiredService<TContext>();

                try
                {
                    logger.LogInformation("Migrating OrderDataBase started...");
                    InvokeSeeder(seeder,context,services);
                    logger.LogInformation("Migrated OrderDatabase.");
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,TContext context,IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
