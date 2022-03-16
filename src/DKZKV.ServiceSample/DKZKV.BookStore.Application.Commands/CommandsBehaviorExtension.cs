using System.Reflection;
using DKZKV.BookStore.Application.Commands.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DKZKV.BookStore.Application.Commands;

public static class CommandsBehaviorExtension
{
    public static void AddCommandsBehavior(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionalBehavior<,>));
    }
}