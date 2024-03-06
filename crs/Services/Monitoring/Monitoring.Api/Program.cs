var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddYamlFile(
        "appsettings.yml", optional: true, reloadOnChange: true);

builder.Services
    .AddHealthChecksUI()
    .AddInMemoryStorage();

// You can add it to the APIs that will use it, and
// then retrieve the result from there through some endpoint, instead of writing it here.
builder.Services.AddHealthChecks()
    .AddRedis("")
    .AddNpgSql("");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapHealthChecksUI(options =>
{
    options.ApiPath = "/healthchecks-api";
    options.UIPath = "/healthchecks-ui";
    options.ResourcesPath = "/healthchecks-ui-resources";
});

app.Run();