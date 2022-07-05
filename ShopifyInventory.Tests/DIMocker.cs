using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopifyInventory.Data;

namespace ShopifyInventory.Tests
{
    public static class DIMocker
    {
        public static IServiceCollection GetCollection()
        {
            IServiceCollection services = new ServiceCollection();

            //Logging
            services.AddLogging();

            //Infrastructure
            var dbName = Guid.NewGuid().ToString();
            services.AddDbContext<ShopifyDbContext>(ctx =>
            {
                ctx.UseInMemoryDatabase(dbName);
            });
            services.AddDistributedMemoryCache();


            return services;
        }

    }
}
