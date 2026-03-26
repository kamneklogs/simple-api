using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleApi.Data;

namespace SimpleApi.Tests;

public class TestBaseFixture : IDisposable
{
    public readonly ServiceProvider ServiceProvider;

    public readonly SimpleApiDbContext dbContext;

    public TestBaseFixture()
    {
        ServiceProvider = BuildServiceProvider();

        dbContext = ServiceProvider
            .GetRequiredService<IDbContextFactory<SimpleApiDbContext>>()
            .CreateDbContext();
    }

    private static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        // Add in memory db
        services.AddPooledDbContextFactory<SimpleApiDbContext>(o =>
        {
            o.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            o.EnableSensitiveDataLogging();
        });

        services.AddPooledDbContextFactory<SimpleApiDbContext>(o =>
        {
            o.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            o.EnableSensitiveDataLogging();
        });

        return services.BuildServiceProvider();
    }

    public void Dispose()
    {
        dbContext.Dispose();
        ServiceProvider.Dispose();
        GC.SuppressFinalize(this);
    }
}
