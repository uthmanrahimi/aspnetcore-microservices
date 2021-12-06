using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost SeedDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Start seeding Database...");

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:Connectionstring"));
                    connection.Open();

                    var command = new NpgsqlCommand() { Connection = connection };
                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon (
                                                         Id SERIAL PRIMARY KEY,
                                                         ProductName VARCHAR(24) NOT NULL,
                                                         Description TXT,
                                                         Amount INT)";
                    command.ExecuteNonQuery();


                    command.CommandText = @"INSERT INTO Coupon(ProductName,Description,Amount) 
                                                        VALUES ('Iphone X','Iphone Inc',100)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO Coupon(ProductName,Description,Amount) 
                                                        VALUES ('Samsung 10','Samsung Inc',100)";

                    command.ExecuteNonQuery();

                    logger.LogInformation("Database Migerated Successfully.");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occured while migerateing postgresql database.");
                    logger.LogInformation("Retry migerating one more time...");
                    SeedDatabase<TContext>(host);
                }
            }

            return host;
        }
    }
}
