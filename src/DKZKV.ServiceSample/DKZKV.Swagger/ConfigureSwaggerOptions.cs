using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DKZKV.Swagger;

internal class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly SwaggerTitle _swaggerTitle;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, SwaggerTitle swaggerTitle)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _swaggerTitle = swaggerTitle ?? throw new ArgumentNullException(nameof(swaggerTitle));
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions) options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Version = description.ApiVersion.ToString(),
            Title = _swaggerTitle.Title,
            Description = _swaggerTitle.Description
        };

        if (description.IsDeprecated) info.Description += " This API version has been deprecated.";

        return info;
    }
}