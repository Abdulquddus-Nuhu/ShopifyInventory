using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShopifyInventory.Data;
using ShopifyInventory.Identity;

namespace ShopifyInventory.Config
{
    public static class Auth
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {

            services.AddDefaultIdentity<Persona>(options => options.SignIn.RequireConfirmedAccount = true)
           .AddEntityFrameworkStores<ShopifyDbContext>()
           .AddDefaultTokenProviders();

            return services;
        }
    }
}
