using DKZKV.BookStore.Domain.Exceptions;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace DKZKV.BookStore.Extensions;

internal static class ProblemDetailsExtension
{
    public static void ConfigureExceptionHandler(this IServiceCollection services)
    {
        services.AddProblemDetails(x =>
        {
            x.Map<ValidationException>(exception =>
                new ValidationProblemDetails(exception.Errors.GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(g => g.ErrorMessage).ToArray()))
                {
                    Status = StatusCodes.Status400BadRequest
                });

            x.Map<DomainObjectNoFountException>(ex => new ProblemDetails
            {
                Title = ex.Title,
                Detail = ex.Description,
                Status = StatusCodes.Status404NotFound
            });
            x.Map<DomainException>(ex => new ProblemDetails
            {
                Title = ex.Title,
                Detail = ex.Description,
                Status = StatusCodes.Status400BadRequest
            });
        });
    }
}