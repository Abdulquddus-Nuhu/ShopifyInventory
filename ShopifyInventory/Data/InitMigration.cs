using Microsoft.EntityFrameworkCore;

namespace ShopifyInventory.Data
{
    public class InitMigration : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public InitMigration(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<ShopifyDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<InitMigration>>();
            try
            {
                logger.LogInformation("Applying Migration!");
                await context.Database.MigrateAsync(cancellationToken: cancellationToken);
                logger.LogInformation("Migration Successful!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to apply Migration!");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
