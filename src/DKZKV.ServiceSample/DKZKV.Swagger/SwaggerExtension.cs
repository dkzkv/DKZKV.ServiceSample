using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DKZKV.Swagger;

public static class SwaggerExtension
{
    public static void AddSwaggerGen(this IServiceCollection services, Assembly serviceAssembly, SwaggerTitle swaggerTitle)
    {
        services.AddSingleton(swaggerTitle);
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.UseInlineDefinitionsForEnums();

                var xmlFile = $"{serviceAssembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            })
            .AddSwaggerGenNewtonsoftSupport();
    }

    public static void ConfigureSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

            c.RoutePrefix = string.Empty;
            c.EnableDeepLinking();
        });
    }
}