using Api.Core.ServiceInstaller.Interfaces;
using Infrastructure.BackgroundJobs;
using Quartz;

namespace Api.Configurations;

internal sealed class BackgroundServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(configure =>
        {
            var cartExpirationCleanupKey = new JobKey(nameof(CartExpirationCleanupJob));
            var outboxBackgroundJobKey = new JobKey(nameof(OutboxBackgroundJob));

            configure
            .AddJob<CartExpirationCleanupJob>(cartExpirationCleanupKey)
            .AddTrigger(
                trigger => trigger
                .ForJob(cartExpirationCleanupKey)
                .WithSimpleSchedule(
                    schedule => schedule
                    //TODO: move in env
                    .WithIntervalInSeconds(15)
                    .RepeatForever()));
           //.AddJob<OutboxBackgroundJob>(outboxBackgroundJobKey)
           // .AddTrigger(
           //     trigger => trigger
           //     .ForJob(outboxBackgroundJobKey)
           //     .WithSimpleSchedule(
           //         schedule => schedule
           //         .WithIntervalInSeconds(5)
           //         .RepeatForever()));
        });

        services.AddQuartzHostedService(configure =>
        configure.WaitForJobsToComplete = true);
    }
}
