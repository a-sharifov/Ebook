using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddYamlFile(
        "appsettings.yml", optional: true, reloadOnChange: true);

builder.Services
      .AddOpenTelemetry()
      .WithMetrics(options => options
      .AddPrometheusExporter()
      .AddHttpClientInstrumentation()
      .AddRuntimeInstrumentation());

builder.Services
    .AddHealthChecksUI()
    .AddInMemoryStorage();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapPrometheusScrapingEndpoint();

app.MapHealthChecksUI(options =>
{
    options.ApiPath = "/healthchecks-api";
    options.UIPath = "/healthchecks-ui";
    options.ResourcesPath = "/healthchecks-ui-resources";
});

app.Run();