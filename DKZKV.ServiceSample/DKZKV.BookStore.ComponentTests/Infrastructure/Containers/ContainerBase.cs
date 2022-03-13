using System;
using Ductus.FluentDocker;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Extensions;

namespace DKZKV.BookStore.ComponentTests.Infrastructure.Containers;

public abstract class ContainerBase
{
    private readonly IContainerService _containerService;
    private readonly int _innerPort;

    protected ContainerBase(string name, string image, int innerPort, params string[] environments)
    {
        _innerPort = innerPort;
        var builder = Fd.UseContainer()
            .RemoveVolumesOnDispose(true)
            .UseImage(image)
            .ExposePort(_innerPort)
            .WithName($"{ConfigurationConstants.NamePrefix}{name}-{Guid.NewGuid():N}")
            .WithEnvironment(environments);

        _containerService = builder.Build();
        _containerService.StopOnDispose = true;
        _containerService.RemoveOnDispose = true;
        _containerService.Start();
    }


    protected ContainerBase(string image, int innerPort, Action<ContainerBuilder> configureBuilder)
    {
        _innerPort = innerPort;
        var builder = Fd.UseContainer()
            .UseImage(image);

        configureBuilder(builder);

        _containerService = builder.Build();
        _containerService.Start();
    }

    protected int GetOuterPort()
    {
        var ipEndpoint = _containerService.ToHostExposedEndpoint($"{_innerPort}/tcp");
        return ipEndpoint.Port;
    }

    public abstract void SetupEnvironmentVariables();
}