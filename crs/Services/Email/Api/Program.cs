using Api.Core.ServiceInstaller;
using Asp.Versioning.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLog();

builder.Configuration.AddYamlFile("appsettings.yml", optional: true, reloadOnChange: true);
builder.Services.InstallServicesFromAssembly(builder.Configuration, AssemblyReference.Assembly);

var app = builder.Build();

//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    using var scope = app.Services.CreateScope();
//    var apiVersionDescriptionProvider =
//        scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

//    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
//    {
//        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
//            description.GroupName.ToUpperInvariant());
//    }
//});

//app.MapPrometheusScrapingEndpoint();
//app.MapHealthChecks("/health", new HealthCheckOptions
//{
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//});

app.Run();
