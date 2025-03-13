using Api.Core.Extensions;
using Api.Core.ServiceInstaller;
using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Persistence.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServicesFromAssembly(builder.Configuration, typeof(Program).Assembly);

var app = builder.Build();

app.MigrateDbContext<OrderDbContext>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    using var scope = app.Services.CreateScope();
    var apiVersionDescriptionProvider =
        scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }   
});

//app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.MapGrpcService<OrderGrpcService>();
//app.MapGrpcReflectionService();
app.MapPrometheusScrapingEndpoint();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
