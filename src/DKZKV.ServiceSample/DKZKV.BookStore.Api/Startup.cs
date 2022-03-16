using System.Reflection;
using DKZKV.BookStore.Extensions;
using DKZKV.BookStore.Options;
using DKZKV.BookStore.Persistence;
using DKZKV.MandatoryOptions;
using DKZKV.Swagger;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

namespace DKZKV.BookStore;

/// <summary>
///     U Know
/// </summary>
public class Startup
{
    private readonly Assembly _appAssembly;
    private readonly IConfiguration _configuration;

    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
        _appAssembly = Assembly.GetExecutingAssembly();
    }

    /// <summary>
    ///     Also u know
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureApi();
        services.AddMvcCore(x => x.EnableEndpointRouting = false).ConfigureApiBehaviorOptions(options => { options.SuppressMapClientErrors = true; })
            .AddControllersAsServices()
            .AddApiExplorer();

        services.AddHealthChecks();
        services.ConfigureExceptionHandler();
        services.AddAutoMapper(_appAssembly);
        services.ConfigureMandatoryOptions(_configuration);
        services.AddBookStoreMediator();
        services.AddSwaggerGen(_appAssembly,
            new SwaggerTitle("Book store", "Book store service, for tracking authors and theirs books"));

        services.AddPersistenceStorage(provider => provider.GetRequiredService<IOptions<BookStoreOptions>>().Value.DatabaseConnection);
    }

    /// <summary>
    ///     U know
    /// </summary>
    /// <param name="app"></param>
    /// <param name="apiVersionProvider"></param>
    public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionProvider)
    {
        app.ConfigureSwagger(apiVersionProvider);

        app.UseCors(builder => builder.AllowAnyOrigin());

        app.UseRouting();
        app.UseStaticFiles();
        app.UseProblemDetails();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });

        app.MigrateDatabase();
    }
}