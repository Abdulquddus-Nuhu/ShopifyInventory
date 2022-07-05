using Microsoft.EntityFrameworkCore;
using ShopifyInventory.Data;

namespace ShopifyInventory.Config
{
    public static class Infrastructure
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IWebHostEnvironment environment)
        {
            string defaultConnectionString = GetConnectionStrings(environment);

            //Add DbContext service
            services.AddDbContext<ShopifyDbContext>(options => options.UseNpgsql(defaultConnectionString));

            return services;
        }

        private static string GetConnectionStrings(IWebHostEnvironment environment)
        {
            string defaultConnectionString;
            if (environment.EnvironmentName == "Development")
            {
                defaultConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=Onimisi;Database=ShopifyInventory";
            }
            else
            {
                // Use connection string provided at runtime by Heroku.
                var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                connectionUrl = connectionUrl!.Replace("postgres://", string.Empty);
                var userPassSide = connectionUrl.Split("@")[0];
                var hostSide = connectionUrl.Split("@")[1];

                var user = userPassSide.Split(":")[0];
                var password = userPassSide.Split(":")[1];
                var host = hostSide.Split("/")[0];
                var database = hostSide.Split("/")[1].Split("?")[0];

                defaultConnectionString = $"Host={host};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
            }

            return defaultConnectionString;
        }
    }
}