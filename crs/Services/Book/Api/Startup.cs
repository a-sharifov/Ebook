namespace Api;

public sealed class Startup(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddControllers();
        //services.AddSwaggerGen(c =>
        //{
        //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
        //});

        //services.AddDbContext<BookDbContext>(options =>
        //{
        //    options.UseNpgsql(_configuration.GetConnectionString("BookDbContext"));
        //});

        //services.AddScoped<ILanguageRepository, LanguageRepository>();
        //services.AddScoped<IGenreRepository, GenreRepository>();
        //services.AddScoped<ICartRepository, CartRepository>();
        //services.AddScoped<IBookRepository, BookRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //if (env.IsDevelopment())
        //{
        //    app.UseDeveloperExceptionPage();
        //    app.UseSwagger();
        //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
        //}

        //app.UseHttpsRedirection();

        //app.UseRouting();

        //app.UseAuthorization();

        //app.UseEndpoints(endpoints =>
        //{
        //    endpoints.MapControllers();
        //});
    }

}
