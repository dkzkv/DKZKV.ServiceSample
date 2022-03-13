using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace DKZKV.BookStore.ComponentTests.Infrastructure;

public class DefaultWebApplicationFactory : WebApplicationFactory<Startup>
{
    private readonly BookStoreInfrastructureConfigurator _configurator;

    public DefaultWebApplicationFactory()
    {
        _configurator = new BookStoreInfrastructureConfigurator();
        SetEnvironmentVariables();
        _configurator.Configure();
    }

    private void SetEnvironmentVariables()
    {
        var environmentVariables = ConfigureEnvironmentVariables();
        foreach (var (envName, envValue) in environmentVariables)
            Environment.SetEnvironmentVariable(envName, envValue);
    }

    private protected virtual IDictionary<string, string> ConfigureEnvironmentVariables()
    {
        var environments = new Dictionary<string, string>
        {
            ["ASPNETCORE_ENVIRONMENT"] = "INTEGRATION_TESTING"
        };

        return environments;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices((_, services) => { }
            )
            .UseTestServer();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _configurator.Dispose();
    }
}