using System.Reflection;
using DKZKV.BookStore.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DKZKV.BookStore.Extensions;

internal static class BookStoreApiExtension
{
    public static void ConfigureApi(this IServiceCollection services)
    {
        services.AddApiVersioning();
        services.AddVersionedApiExplorer();

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddControllers();
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        });
    }

    public static void AddBookStoreMediator(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddCommandsBehavior();
    }
}