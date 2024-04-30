using Api.Core.ServiceInstaller.Interfaces;
using Infrastructure.BackgroundJobs;
using Quartz;

namespace Api.Configurations;

internal sealed class BackgroundServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        //services.AddQuartz(configure =>
        //{
        //    var jobKey = new JobKey(nameof(CartExpirationCleanupJob));

        //    configure
        //    .AddJob<CartExpirationCleanupJob>(jobKey)
        //    .AddTrigger(
        //        trigger => trigger
        //        .ForJob(jobKey)
        //        .WithSimpleSchedule(
        //            schedule => schedule
        //            //TODO: move in env
        //            .WithIntervalInSeconds(10)
        //            .RepeatForever()));
        //});

        //services.AddQuartzHostedService(configure =>
        //configure.WaitForJobsToComplete = true);
    }
}
