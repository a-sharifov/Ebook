//using Api.Core.ServiceInstaller.Interfaces;
//using Asp.Versioning;
//using Asp.Versioning.ApiExplorer;
//using Microsoft.OpenApi.Models;

//namespace Api.Configurations;

//internal sealed class SwaggerServiceInstaller : IServiceInstaller
//{
//    public void Install(IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddEndpointsApiExplorer();
        
//        services.AddApiVersioning(options =>
//        {
//            options.DefaultApiVersion = new ApiVersion(1, 0);
//            options.AssumeDefaultVersionWhenUnspecified = true;
//            options.ReportApiVersions = true;
//            options.ApiVersionReader = ApiVersionReader.Combine(
//                new UrlSegmentApiVersionReader(),
//                new HeaderApiVersionReader("X-Api-Version"));
//        }).AddApiExplorer(options =>
//        {
//            options.GroupNameFormat = "'v'VVV";
//            options.SubstituteApiVersionInUrl = true;
//        });

//        services.AddSwaggerGen(options =>
//        {
//            options.SwaggerDoc("v1", new OpenApiInfo
//            {
//                Title = "Email API",
//                Version = "v1",
//                Description = "Email API for the Ebook system"
//            });

//            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//            {
//                Description = "JWT Authorization header using the Bearer scheme",
//                Name = "Authorization",
//                In = ParameterLocation.Header,
//                Type = SecuritySchemeType.ApiKey,
//                Scheme = "Bearer"
//            });

//            options.AddSecurityRequirement(new OpenApiSecurityRequirement
//            {
//                {
//                    new OpenApiSecurityScheme
//                    {
//                        Reference = new OpenApiReference
//                        {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                        }
//                    },
//                    Array.Empty<string>()
//                }
//            });
//        });
//    }
//}
