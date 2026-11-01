using MedicalApi.Database;
using Microsoft.EntityFrameworkCore;

namespace MedicalApi.Migration;

public class MigrationWorker(IServiceProvider serviceProvider, IHostApplicationLifetime lifetime, ILogger<MigrationWorker> logger) 
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MedicalDbContext>();

            logger.LogInformation("Migration started.");

            await db.Database.MigrateAsync(stoppingToken);

            logger.LogInformation("Migration completed successfully.");
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to migrate.");
            throw;
        }
        finally
        {
            lifetime.StopApplication();
        }
    }
}
