using DKZKV.BookStore.ComponentTests.Infrastructure.DataUploader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace DKZKV.BookStore.ComponentTests.Infrastructure;

public class BookStoreWithDataWebApplicationFactory : DefaultWebApplicationFactory
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices((_, services) =>
                services.AddHostedService<AuthorsAndBooksUploader>()
            )
            .UseTestServer();
    }
}