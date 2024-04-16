using Serilog;

namespace Api.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseLog(this IHostBuilder hostBuilder) =>
        hostBuilder.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration));
}
