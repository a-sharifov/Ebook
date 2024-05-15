using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Api.Core.Extensions;

/// <summary>
/// Extension methods for <see cref="IApplicationBuilder"/>.
/// </summary>
public static class ApplicationBuilderExtension
{
    /// <summary>
    /// Migrates the database for the given <typeparamref name="TDbContext"/>.
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="builder"> The <see cref="IApplicationBuilder" />.</param>
    /// <returns> The <see cref="IApplicationBuilder" />.</returns>
    public static IApplicationBuilder MigrateDbContext<TDbContext>(this IApplicationBuilder builder)
        where TDbContext : DbContext
    {
        using var scope = builder.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        Policy
            .Handle<Exception>()
            .WaitAndRetry(
            retryCount: 3,
            _ => TimeSpan.FromSeconds(15))
            .Execute(dbContext.Database.Migrate);

        return builder;
    }
}
