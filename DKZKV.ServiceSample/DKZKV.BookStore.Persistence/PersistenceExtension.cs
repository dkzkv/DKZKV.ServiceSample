using System.Reflection;
using DKZKV.BookStore.Domain.AggregatesModel.Author;
using DKZKV.BookStore.Domain.Repositories;
using DKZKV.BookStore.Domain.SeedWork;
using DKZKV.BookStore.Persistence.Repositories;
using DKZKV.BookStore.Persistence.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DKZKV.BookStore.Persistence;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistenceStorage(this IServiceCollection serviceCollection,
        Func<IServiceProvider, string> settingsProvider)
    {
        return serviceCollection
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddScoped<IBookStoreRepository<Author, Guid>, AuthorRepository>()
            .AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>()
            .AddDbContext<BookStoreBdContext>(
                (provider, builder) =>
                {
                    var dbSettings = settingsProvider.Invoke(provider);
                    builder.UseSqlServer(dbSettings);
                });
    }

    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<BookStoreBdContext>();
        if (context is null)
            throw new InvalidOperationException("Db context is not added to service collection");
        context.Database.Migrate();
        context.SaveChanges();
    }
}